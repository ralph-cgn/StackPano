// global settings dialog

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StackPano
{
    public partial class StackPano : Form
    {
        private string _rawExtension,
            _stackedDirectory,
            _heliconFocusPath,
            _cygwinRunPath,
            _jpgQuality,
            _verticalAjdustment,
            _horizontalAdjustment,
            _rotationAdjustment,
            _magnificationAdjustment,
            _interpolationMethod,
            _rawType;

        private bool _brightnessAdjustment, _tifFiles;
        private string radius, smoothing, rootDirectory;
        private int selectedMethod, numberSubdirs;
        TimeSpan duration;

        public StackPano()
        {
            InitializeComponent();

            progressLabel.Text = string.Empty;
            ReadAppConfig();
            methodComboBox.SelectedIndex = 2;

            String[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length == 2 && Directory.Exists(arguments[1]))
            {
                rootTextBox.Text = arguments[1];
            }

            startButton.Enabled = Directory.Exists(rootTextBox.Text);

            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;
        }

        #region Eventhandler

        private void startButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(rootTextBox.Text))
            {
                _rawType = GetRawFileType(rootTextBox.Text, _rawExtension);

                // in BackgroundWorker Thread  Access to Controls is not possible
                // copy Settings from Controls to Instance Variable
                radius = methodComboBox.SelectedIndex == 0 || methodComboBox.SelectedIndex == 1 ?
                    string.Format("-rp:{0}", radiusTextBox.Text) :
                    string.Empty;

                smoothing = smoothingTextBox.Text;

                rootDirectory = rootTextBox.Text;
                selectedMethod = methodComboBox.SelectedIndex;

                // disable the Controls
                ToggleControls(true);
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show(string.Format("The Path {0} does not exist", rootTextBox.Text));
            }
        }

        /// <summary>
        /// change Root Directory
        /// </summary>
        private void rootTextBox_TextChanged(object sender, EventArgs e)
        {
            startButton.Enabled = Directory.Exists(rootTextBox.Text);
        }

        /// <summary>
        /// change Root Directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rootButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    rootTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        /// <summary>
        /// change Radius
        /// </summary>
        private void radiusTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateEntry("Radius", ((TextBox)sender).Text);
        }

        /// <summary>
        /// create Subdirectory per Stack
        /// </summary>
        private void createSubdirsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (createSubdirsCheckBox.Checked)
            {
                compNrCheckBox.Checked = false;
            }

            stackDepthNumericUpDown.Enabled = createSubdirsCheckBox.Checked;
        }

        /// <summary>
        /// compare Number of Files per Stack
        /// </summary>
        private void compNrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (compNrCheckBox.Checked)
            {
                createSubdirsCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// change Smoothing
        /// </summary>
        private void smoothingTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateEntry("Smoothing", ((TextBox)sender).Text);
        }

        private void button1_Click(object sender, EventArgs e) // TODO just for merge testing
        {
            string merged = Path.Combine(rootTextBox.Text, "Merged");
            Directory.CreateDirectory(merged);
            MergeRgbChannels(rootTextBox.Text, merged, (int)stackDepthNumericUpDown.Value);
        }

        /// <summary>
        /// change Stacking Method
        /// </summary>
        private void methodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            radiusLabel.Enabled = methodComboBox.SelectedIndex == 0 || methodComboBox.SelectedIndex == 1;
            radiusTextBox.Enabled = radiusLabel.Enabled;
        }

        /// <summary>
        /// perform Stacking in Background (ortherwise the Form would become unresponsive)
        /// </summary>
        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            StackDirectories();
        }

        /// <summary>
        /// Show current Subdirectory
        /// </summary>
        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressLabel.Text = string.Format("Processed Subdirectory {0} of {1}", e.ProgressPercentage,
                numberSubdirs);
        }

        /// <summary>
        /// show Duration when finished
        /// </summary>
        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            progressLabel.Text = string.Format("Processing Finished in {0:D2}h:{1:D2}m:{2:D2}s",
                duration.Hours,
                duration.Minutes,
                duration.Seconds);

            // enable the Controls
            ToggleControls(false);
        }

        #endregion

        /// <summary>
        /// read Parameters from app.config
        /// </summary>
        private void ReadAppConfig()
        {
            _rawExtension = ConfigurationManager.AppSettings["RawExtensions"];
            _stackedDirectory = ConfigurationManager.AppSettings["StackedDirectory"];
            _heliconFocusPath = ConfigurationManager.AppSettings["HeliconFocusPath"];
            _cygwinRunPath = ConfigurationManager.AppSettings["CygwinRunPath"];
            _jpgQuality = ConfigurationManager.AppSettings["JpgQuality"];
            _verticalAjdustment = ConfigurationManager.AppSettings["VerticalAdjustment"];
            _horizontalAdjustment = ConfigurationManager.AppSettings["HorizontalAdjustment"];
            _rotationAdjustment = ConfigurationManager.AppSettings["RotationAdjustment"];
            _magnificationAdjustment = ConfigurationManager.AppSettings["MagnificationAdjustment"];
            _brightnessAdjustment = ConfigurationManager.AppSettings["BrightnessAjustment"] == "true";
            _interpolationMethod = ConfigurationManager.AppSettings["InterpolationMethod"];
        }

        /// <summary>
        /// disable/enable all Controls while Stacking
        /// </summary>
        /// <param name="stacking">Stacking is running</param>
        private void ToggleControls(bool stacking)
        {
            rootLabel.Enabled = !rootLabel.Enabled;
            rootTextBox.Enabled = !rootTextBox.Enabled;
            rootButton.Enabled = !rootButton.Enabled;

            methodLabel.Enabled = !methodLabel.Enabled;
            methodComboBox.Enabled = !methodComboBox.Enabled;
            smoothingLabel.Enabled = !smoothingLabel.Enabled;

            if (stacking && radiusTextBox.Enabled)
            {
                radiusTextBox.Enabled = false;
            }
            else if (!stacking && methodComboBox.SelectedIndex == 0)
            {
                radiusTextBox.Enabled = true;
            }

            if (stacking && smoothingTextBox.Enabled)
            {
                smoothingTextBox.Enabled = false;
            }
            else if (!stacking && methodComboBox.SelectedIndex == 0)
            {
                smoothingTextBox.Enabled = true;
            }

            startButton.Enabled = !startButton.Enabled;
            compNrCheckBox.Enabled = !compNrCheckBox.Enabled;
            createSubdirsCheckBox.Enabled = !createSubdirsCheckBox.Enabled;

            if (stacking && stackDepthNumericUpDown.Enabled || !stacking && createSubdirsCheckBox.Checked)
            {
                stackDepthLabel.Enabled = false;
                stackDepthNumericUpDown.Enabled = false;
            }
        }

        /// <summary>
        /// Iterate over Subdirectories and stack the JPG Files in each of them 
        /// </summary>
        private void StackDirectories()
        {
            DateTime begin = DateTime.Now;
            bool splitSuccess = true;

            // Number of Files in previous Subdirectory
            int prevFileCount = 0;

            // current Subdirectory Index
            int currentSubdir = 1;

            CreateDirectory(_stackedDirectory);

            if (createSubdirsCheckBox.Checked)
            {
                splitSuccess = SplitFilesIntoStacks(rootTextBox.Text, (uint)stackDepthNumericUpDown.Value);
            }

            if (splitSuccess)
            {
                DirectoryInfo root = new DirectoryInfo(rootTextBox.Text);

                // read all Directories under Root Directory
                DirectoryInfo[] subdirs = root.GetDirectories();

                // at least 2 Subdirectories beyond Directories RAW and Stacked which have been previously created
                if (subdirs.Length >= 3)
                {
                    // check if Input is TIF Format
                    _tifFiles = !string.IsNullOrEmpty(GetRawFileType(rootTextBox.Text, ".PNG|.TIF"));

                    // do not count the Directories "RAW" and "Stacked"
                    numberSubdirs = subdirs.Length - 1;

                    // iterate over all Subdirectories 
                    foreach (DirectoryInfo subdir in subdirs)
                    {
                        if (subdir.Name != _stackedDirectory)
                        {
                            // get JPEG Files in Subdirectory (RAW files have already been moved to other Directory)
                            FileInfo[] files = subdir.GetFiles();

                            if (files.Length < 2)
                            {
                                MessageBox.Show(string.Format("The Subdirectory {0} contains no/only one JPG Files", subdir.Name)); // TODO may be TIF also
                            }

                            // if not first Directory and Number of Files does not equal Number of files in previous Directory
                            if (compNrCheckBox.Checked && currentSubdir > 1 && files.Length != prevFileCount)
                            {
                                MessageBox.Show(string.Format(
                                    "Number of {0} files in current Directory \"{1}\" is different from the number {2} in previous Directory",
                                    files.Length, subdir.Name, prevFileCount));

                                break;
                            }

                            CallHeliconFocus(subdir);

                            prevFileCount = files.Length;
                            backgroundWorker.ReportProgress(currentSubdir);
                            currentSubdir++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Root Directory contains no Subdirectories");
                }

                duration = DateTime.Now.Subtract(begin);
            }
        }

        /// <summary>
        /// call Helicon Focus
        /// </summary>
        /// <param name="subdir">current Subdirectory</param>
        private void CallHeliconFocus(DirectoryInfo subdir)
        {
            CreateFileList(subdir.FullName, _rawType);

            string brightnessAdjustment = _brightnessAdjustment ? "-ba" : string.Empty;

            // Example: HeliconFocus.exe -silent -save:"outpath\name.jpg" -j:95 -mp:0 -rp:8 -sp:4 -va:3 -ha:3 -ra:1 -ma:5 -ba -im:1 "inpath"

            string fileList = Path.Combine(subdir.FullName, "input.txt");

            string outType = _tifFiles || !string.IsNullOrEmpty(_rawType) ? ".tif" : ".jpg";

            string param = string.Format("-silent -save:\"{0}\" -j:{1} -mp:{2} {3} -sp:{4} -va:{5} -ha:{6} -ra:{7} -ma:{8} {9} -im:{10} -i \"{11}\"",
                Path.Combine(rootDirectory, _stackedDirectory, subdir.Name + outType), // Name of Output File 
                _jpgQuality, // not necessary for TIF but harmless
                selectedMethod, // 0=A, 1=B, 2=C (C is best for IC Dies)
                radius,
                smoothing,
                _verticalAjdustment,
                _horizontalAdjustment,
                _rotationAdjustment,
                _magnificationAdjustment,
                brightnessAdjustment,
                _interpolationMethod,
                fileList);

            ProcessStartInfo processStart = new ProcessStartInfo
            {
                FileName = _heliconFocusPath,
                Arguments = param,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            try
            {
                // call Helicon Focus for current Subdirectory
                Process stacker = Process.Start(processStart);
                stacker.WaitForExit();
                File.Delete(fileList);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// create list with files of type
        /// </summary>
        /// <param name="subdirPath">path to stack</param>
        /// <param name="type">file type</param>
        private void CreateFileList(string subdirPath, string type)
        {
            try
            {
                // get files of type
                IEnumerable<string> inFiles = Directory.GetFiles(subdirPath).Where(s => Path.GetExtension(s).ToUpperInvariant() == type);

                string filesString = string.Empty;

                // iterate over input files
                foreach (string file in inFiles)
                {
                    filesString += file + Environment.NewLine;
                }

                // write list of input files to text file
                File.WriteAllText(Path.Combine(subdirPath, "input.txt"), filesString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// create RAW/Stacked Directories
        /// </summary>
        /// <param name="subdir">Directory Name</param>
        private void CreateDirectory(string subdir)
        {
            try
            {
                string path = Path.Combine(rootTextBox.Text, subdir);

                if (!Directory.Exists(path))
                {
                    // create Output Directory
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Create one Subdirectory per Stack and move the Files to that Subdirectory
        /// RAW Files have to be moved to other Directory before this Function is called
        /// </summary>
        /// <param name="rootPath">Directory containing all Files for Mosaic</param>
        /// <param name="filesPerStack">Depth (of all Stacks)</param>
        /// <returns>Success</returns>
        private bool SplitFilesIntoStacks(string rootPath, uint filesPerStack)
        {
            if (filesPerStack < 2)
            {
                MessageBox.Show("There should be at least 2 Files per Stack");
                return false;
            }

            string[] files = Directory.GetFiles(rootPath);

            if (files.Length % filesPerStack != 0)
            {
                MessageBox.Show(string.Format(
                    "Number of Files in Root Directory {0} is not divisible through Number of Files per Stack {1}", files.Length, filesPerStack));

                return false;
            }

            if (!string.IsNullOrEmpty(_rawType))
            {
                filesPerStack *= 2;
            }

            try
            {
                uint subdirNumber = 1;
                uint fileNumber = 0;

                // Iterate over all Files in Root Directory
                foreach (string file in files)
                {
                    // Begin of new Stack
                    if (fileNumber % filesPerStack == 0)
                    {
                        Directory.CreateDirectory(Path.Combine(rootPath, subdirNumber.ToString()));
                        subdirNumber++;
                    }

                    // move File from Root Directory to Stack Subdirectory
                    File.Move(file, Path.Combine(rootPath, (subdirNumber - 1).ToString(), Path.GetFileName(file)));
                    fileNumber++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }

        ///// <summary>
        ///// check if directory contains files of types in list
        ///// </summary>
        ///// <param name="rootPath">root path</param>
        ///// <param name="extensions">list of extensions separated by pipe</param>
        ///// <returns>directory conatains RAW files</returns>
        //private bool DirContainsFilesOfType(string rootPath, string extensions)
        //{
        //    // get RAW extensions
        //    string[] rawExtensions = extensions.Split('|');
            
        //    return Directory.GetFiles(rootPath).Where(s => rawExtensions.Contains(Path.GetExtension(s).ToUpperInvariant())).Any();
        //}

        /// <summary>
        /// get type of RAW files (null if there are no RAW files)
        /// </summary>
        /// <param name="rootPath">root path</param>
        /// <param name="extensions">list of extensions separated by pipe</param>
        /// <returns>directory conatains RAW files</returns>
        private string GetRawFileType(string rootPath, string extensions)
        {
            // get RAW extensions
            string[] rawExtensions = extensions.Split('|');

            return Path.GetExtension(Directory.GetFiles(rootPath).Where(s => rawExtensions.Contains(Path.GetExtension(s).ToUpperInvariant())).FirstOrDefault());
        }

        /// <summary>
        /// Validate Radius/Smoothing
        /// </summary>
        /// <param name="field">Fieildname</param>
        /// <param name="value">Value</param>
        private void ValidateEntry(string field, string value)
        {
            if (!int.TryParse(value, out int i))
            {
                MessageBox.Show(string.Format("{0} must be positive whole number", field));
            }

            if (i < 1 || i > 50) // TODO
            {
                MessageBox.Show(string.Format("Value of {0} out of range", field));
            }
        }

        /// <summary>
        /// merging before stacking is mandatory because when stacking before merging not all images will have the same size
        /// stack images with separate files per color channel (taken with mono camera and RGB color filter) into RGB color images
        /// all images to be processed should be contained in the same folder
        /// the filenames should be an ascending number sequence starting at 1 (prezeroes as parameter?)
        /// all files should be in TIF format with the the same grayscale depth (8 or 16)
        /// total number of images should be multiple of 3
        /// </summary>
        /// <param name="inPath"></param>
        /// <param name="outPath"></param>
        /// <param name="stackDepth">depth of focus stack</param>
        private void MergeRgbChannels(string inPath, string outPath, int stackDepth)
        {
            // check if input directory exists
            if (!Directory.Exists(inPath))
            {
                MessageBox.Show(string.Format("The Directory containing RGB channel images {0} does not exist", inPath));
            }

            string[] inFiles = null;

            try
            {
                // get input files
                inFiles = Directory.GetFiles(inPath, "*.tif", SearchOption.TopDirectoryOnly);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // check if number of files is a multiple of 3
            if (inFiles.Length % 3 != 0)
            {
                MessageBox.Show(string.Format("Number of RGB channel images {0} is not a multiple of 3", inFiles.Length));
            }

            // output directory does not exist
            if (!Directory.Exists(outPath))
            {
                MessageBox.Show(string.Format("Output Directory {0} does not exist", outPath));
            }

            if (stackDepth < 2)
            {
                MessageBox.Show(string.Format("Depth of Stack {0} is smaller than 2", stackDepth));
            }

            int outFileIdx = 1;

            // iteration over all tiles
            // stackDepth == 6, inFiles.Length == 60
            // R=1, G=6, B=11
            // ...
            // R=50, G=55, B=60
            // outFileIdx = 1..20
            // TODO show progress bar
            for (int tile = 1; tile <= inFiles.Length; tile += 3)
            {
                try
                {
                    // TODO use same process instance and just change arguments with each iteration? 
                    Process rgbMerge = new Process();
                    rgbMerge.StartInfo.FileName = _cygwinRunPath;
                    rgbMerge.StartInfo.WorkingDirectory = inPath;
                    rgbMerge.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    //rgbMerge.StartInfo.UseShellExecute = false;
                    //rgbMerge.StartInfo.RedirectStandardOutput = true;
                    //rgbMerge.StartInfo.RedirectStandardError = true;

                    string redFile = tile.ToString("D5"),
                        greenFile = (tile + stackDepth).ToString("D5"),
                        blueFile = (tile + 2 * stackDepth).ToString("D5"),
                        outFile = Path.Combine(outPath, string.Format("{0}.tif", outFileIdx));

                    // TODO output in unknown format (should be derived from extension in name)
                    // filesize is also to small
                    // output is written even when some input files are not found
                    // the following command works in cmd.exe
                    //c:\cygwin64\bin\run.exe convert C:\Users\rsche\Desktop\LRGB\00001.tif C:\Users\rsche\Desktop\LRGB\00006.tif C:\Users\rsche\Desktop\LRGB\00011.tif -combine -set colorspace sRGB C:\Users\rsche\Desktop\LRGB\Merged\1.tif
                    rgbMerge.StartInfo.Arguments = string.Format("convert {0}.tif {1}.tif {2}.tif -combine -set colorspace sRGB {3}",
                        redFile,
                        greenFile,
                        blueFile,
                        outFile);

                    rgbMerge.Start();

                    rgbMerge.WaitForExit();

                    //string line = string.Empty;

                    //while (!rgbMerge.StandardOutput.EndOfStream)
                    //{
                    //    line += rgbMerge.StandardOutput.ReadLine();
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                outFileIdx++;
            }
        }
    }
}
