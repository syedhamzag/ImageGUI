using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageGUI.backend;

namespace ImageGUI
{
    public partial class Form1 : Form
    {
        public static string referenceFolder;
        public static string targetFolder;
        public static string referencepathselected;
        public static string targetpathselected;
        FolderBrowserDialog reference_path;
        FolderBrowserDialog target_path;
        //FolderBrowserDialog output_path;
        public static string outputpath;
        public Form1()
        {
            InitializeComponent();
            //referencepathselected = @"D:\downloads\Python\8X8X8 cube and input_target data folders\8X8X8\1125_INPUT";
            //targetpathselected = @"D:\downloads\Python\8X8X8 cube and input_target data folders\8X8X8\1125_TARGET";
            //referencepathselected = @"I:\8X8X8\1125_INPUT";
            //targetpathselected = @"I:\8X8X8\1125_TARGET";
            //loadReferenceDataset(referencepathselected);
            //loadTargetDataset(targetpathselected);
            #region Designer
            panel1.BackColor = Color.FromArgb(26, 28, 30);
            panel2.BackColor = Color.FromArgb(34, 36, 38);
            button3.BackColor = Color.FromArgb(70, 70, 70);
            button4.BackColor = Color.FromArgb(70, 70, 70);
            button5.BackColor = Color.FromArgb(70, 70, 70);
            button6.BackColor = Color.FromArgb(70, 70, 70);
            button3.ForeColor = Color.FromArgb(140, 140, 140);
            button4.ForeColor = Color.FromArgb(140, 140, 140);
            button5.ForeColor = Color.FromArgb(140, 140, 140);
            button6.ForeColor = Color.FromArgb(140, 140, 140);
            button3.FlatAppearance.BorderColor = Color.FromArgb(140, 140, 140);
            button4.FlatAppearance.BorderColor = Color.FromArgb(140, 140, 140);
            button5.FlatAppearance.BorderColor = Color.FromArgb(140, 140, 140);
            button6.FlatAppearance.BorderColor = Color.FromArgb(140, 140, 140);
            button3.FlatAppearance.BorderSize = 1;
            button3.FlatAppearance.BorderSize = 1;
            button3.FlatAppearance.BorderSize = 1;
            button3.FlatAppearance.BorderSize = 1;

            #endregion



        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    using (output_path = new FolderBrowserDialog())
        //    {
        //        DialogResult result = output_path.ShowDialog();

        //        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(output_path.SelectedPath))
        //        {
        //            outputpath = output_path.SelectedPath;
        //        }
        //        else
        //        {
        //            lblMsg.ForeColor = Color.FromArgb(171, 171, 171);
        //            lblMsg.Text = "Please Select Output Path!!";
        //        }
        //    }
        //}
        private void inputbrowse_Click(object sender, EventArgs e)
        {
            referenceColor.Text = "";
            referenceGrayScale.Text = "";
            referenceDatasetLoadStatus.Text = "";
            referenceFolder = "";

            using (reference_path = new FolderBrowserDialog())
            {
                DialogResult result = reference_path.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(reference_path.SelectedPath))
                {
                    referencepathselected = reference_path.SelectedPath;
                    loadReferenceDataset(referencepathselected);
                }
                else
                {
                    referenceDatasetLoadStatus.ForeColor = Color.FromArgb(171, 171, 171);
                    referenceDatasetLoadStatus.Text = "NOT LOADED";
                }
            }

        }
        private void targetbrowse_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Target");
            targetColor.Text = "";
            targetGrayScale.Text = "";
            targetDatasetLoadStatus.Text = "";
            targetFolder = "";

            using (target_path = new FolderBrowserDialog())
            {
                DialogResult result = target_path.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(target_path.SelectedPath))
                {
                    targetpathselected = target_path.SelectedPath;
                    loadTargetDataset(targetpathselected);
                }
                else
                {
                    targetDatasetLoadStatus.ForeColor = Color.FromArgb(171, 171, 171);
                    targetDatasetLoadStatus.Text = "NOT LOADED";
                }
            }
        }
        private void loadReferenceDataset(string selectedPath)
        {
            bool isCorrect;
            string extension;
            string[] colorFiles;
            string[] greyScaleFiles;
            string[] folders = Directory.GetDirectories(selectedPath);
            if (folders.Length == 2)
            {
                if (folders[0].EndsWith("COLOR") && folders[1].EndsWith("GRAYSCALE"))
                {


                    Console.WriteLine(selectedPath);
                    colorFiles = Directory.GetFiles(folders[0].ToString());
                    isCorrect = true;
                    foreach (var filename in colorFiles)
                    {
                        extension = Path.GetExtension(filename);
                        if (extension != ".tif" && extension != ".png")
                        {
                            isCorrect = false;
                            break;
                        }

                    }

                    greyScaleFiles = Directory.GetFiles(folders[1].ToString());

                    foreach (var filename in greyScaleFiles)
                    {
                        extension = Path.GetExtension(filename);
                        if (extension != ".tif" && extension != ".png")
                        {
                            isCorrect = false;
                            break;
                        }

                    }



                    if (isCorrect)
                    {
                        referenceColor.Text = colorFiles.Length.ToString();
                        referenceGrayScale.Text = greyScaleFiles.Length.ToString();
                        referenceDatasetLoadStatus.ForeColor = Color.FromArgb(0, 139, 71);
                        referenceDatasetLoadStatus.Text = "LOADED";
                        referenceFolder = selectedPath;
                    }
                }
                else
                {
                    referenceDatasetLoadStatus.ForeColor = Color.FromArgb(182, 50, 112);
                    referenceDatasetLoadStatus.Text = "ERROR";
                }
            }

            else
            {
                referenceDatasetLoadStatus.ForeColor = Color.FromArgb(182, 50, 112);
                referenceDatasetLoadStatus.Text = "INVALID FOLDER";
            }

        }

        private void loadTargetDataset(string selectedPath)
        {
            bool isCorrect;
            string extension;
            string[] colorFiles;
            string[] greyScaleFiles;
            string[] folders = Directory.GetDirectories(selectedPath);
            if (folders.Length == 2)
            {
                if (folders[0].EndsWith("COLOR") && folders[1].EndsWith("GRAYSCALE"))
                {
                    Console.WriteLine(selectedPath);
                    colorFiles = Directory.GetFiles(folders[0].ToString());
                    isCorrect = true;
                    foreach (var filename in colorFiles)
                    {
                        extension = Path.GetExtension(filename);
                        if (extension != ".tif" && extension != ".png")
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    greyScaleFiles = Directory.GetFiles(folders[1].ToString());

                    foreach (var filename in greyScaleFiles)
                    {
                        extension = Path.GetExtension(filename);
                        if (extension != ".tif" && extension != ".png")
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    if (isCorrect)
                    {
                        targetColor.Text = colorFiles.Length.ToString();
                        targetGrayScale.Text = greyScaleFiles.Length.ToString();
                        targetDatasetLoadStatus.ForeColor = Color.FromArgb(0, 139, 71);
                        targetDatasetLoadStatus.Text = "LOADED";
                        targetFolder = selectedPath;
                    }
                }
                else
                {
                    targetDatasetLoadStatus.ForeColor = Color.FromArgb(182, 50, 112);
                    targetDatasetLoadStatus.Text = "ERROR";
                }
            }
            else
            {
                targetDatasetLoadStatus.ForeColor = Color.FromArgb(182, 50, 112);
                targetDatasetLoadStatus.Text = "INVALID FOLDER";
            }

        }

        private void run1DClick()
        {
            outputpath = "example_1D.csv";
            Console.WriteLine("1D");
            lblMsg.Text = "";
            rgbCurves.Text = "";
            referenceXYZ.Text = "";
            targetXYZ.Text = "";
            smoothTransform.Text = "";
            floatingPointsTransform.Text = "";
            double r, g, b, r_, g_, b_ = 0.0;
            List<List<string>> dataset = new List<List<string>>();
            List<string> dataset_A = new List<string>();
            List<string> dataset_B = new List<string>();
            if (!string.IsNullOrEmpty(outputpath))
            {
                try
                {
                    if (referencepathselected != "" && targetpathselected != "")
                    {
                        // Load dataset
                        if (int.Parse(referenceColor.Text) == int.Parse(targetColor.Text)
                            && int.Parse(referenceGrayScale.Text) == int.Parse(targetGrayScale.Text))
                        {
                            dataset = open_file.create_dataset(referencepathselected,
                                          targetpathselected);

                            dataset_A = dataset[0];
                            dataset_B = dataset[1];
                            var csv = new StringBuilder();
                            var head = string.Format("{0},{1},{2},{3},{4},{5}", "r in", "g in", "b in", "r out", "g out", "b out");
                            var line = "";
                            csv.AppendLine(head);
                            var plotter_ = plotter.plotter_(dataset_A, dataset_B);
                            var t_plotter = plotter_
                                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                                .GroupBy(i => i.index, i => i.item)
                                .Select(ga => ga.ToList())
                                .ToList();
                            foreach (var item in t_plotter)
                            {
                                r = item[0];
                                g = item[1];
                                b = item[2];
                                r_ = item[3];
                                g_ = item[4];
                                b_ = item[5];
                                line = string.Format("{0},{1},{2},{3},{4},{5}", r, g, b, r_, g_, b_);
                                csv.AppendLine(line);

                            }
                            File.WriteAllText(outputpath, csv.ToString());
                            rgbCurves.Text = "COMPLETE";
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.FromArgb(171, 171, 171);
                            lblMsg.Text = "FILES ARE NOT EQUAL!!";
                            Console.WriteLine("FILES ARE NOT EQUAL!!");
                        }
                    }
                    else
                    {
                        lblMsg.ForeColor = Color.FromArgb(171, 171, 171);
                        lblMsg.Text = "DATASET NOT SELECTED !!";
                    }

                }
                catch (Exception e)
                {
                    lblMsg.ForeColor = Color.FromArgb(171, 171, 171);
                    lblMsg.Text = "ERROR : " + e.Message;
                    Console.WriteLine(e.InnerException);
                }
            }
            else
            {
                lblMsg.ForeColor = Color.FromArgb(171, 171, 171);
                lblMsg.Text = "Please Select Output Path!!";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            run1DClick();
        }

    }
}
