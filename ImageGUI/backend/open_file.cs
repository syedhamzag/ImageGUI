using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGUI.backend;

namespace ImageGUI.backend
{
    class open_file
    {
        public static string check_input_type(string input)
        {
            string type;
            FileAttributes attrA = File.GetAttributes(@"" + input);
            if ((attrA & FileAttributes.Directory) == FileAttributes.Directory)
                type = "dir";
            else
                type = "image";

            return type;
        }
        public static List<List<string>> create_dataset(string A, string B)
        {
            string type_A = check_input_type(A);
            string type_B = check_input_type(B);
            List<string> dataset_A = new List<string>();
            List<string> dataset_B = new List<string>();
            List<List<string>> dataset = new List<List<string>>();
            List<double> rgb;
            List<double> bgr;
            string r, g, b = "";
            if (type_A == "dir")
            {
                string[] data = Directory.GetFiles(A + "//GRAYSCALE");
                foreach (var item in data)
                {
                    rgb = new List<double>();
                    bgr = new List<double>();
                    string file_path = item;
                    using (Bitmap bmp = new Bitmap(file_path))
                    {
                        var color = bmp.GetPixel(bmp.Width / 2, bmp.Height / 2);
                        rgb.Add(color.R);
                        rgb.Add(color.G);
                        rgb.Add(color.B);
                    }
                    bgr = normalization(rgb);
                    b = bgr[2].ToString();
                    g = bgr[1].ToString();
                    r = bgr[0].ToString();
                    dataset_A.Add(r.ToString() + "," + g.ToString() + "," + b.ToString());
                }
                string[] data1 = Directory.GetFiles(A + "//COLOR");
                foreach (var item in data1)
                {
                    rgb = new List<double>();
                    bgr = new List<double>();
                    string file_path = item;
                    using (Bitmap bmp = new Bitmap(file_path))
                    {
                        var color = bmp.GetPixel(bmp.Width / 2, bmp.Height / 2);
                        rgb.Add(color.R);
                        rgb.Add(color.G);
                        rgb.Add(color.B);
                    }
                    bgr = normalization(rgb);
                    b = bgr[2].ToString();
                    g = bgr[1].ToString();
                    r = bgr[0].ToString();
                    dataset_A.Add(r.ToString() + "," + g.ToString() + "," + b.ToString());
                }

            }
            if (type_B == "dir")
            {
                string[] data = Directory.GetFiles(B + "//GRAYSCALE");
                foreach (var item in data)
                {
                    rgb = new List<double>();
                    bgr = new List<double>();
                    string file_path = item;
                    using (Bitmap bmp = new Bitmap(file_path))
                    {
                        var color = bmp.GetPixel(bmp.Width / 2, bmp.Height / 2);
                        rgb.Add(color.R);
                        rgb.Add(color.G);
                        rgb.Add(color.B);
                    }
                    bgr = normalization(rgb);
                    b = bgr[2].ToString();
                    g = bgr[1].ToString();
                    r = bgr[0].ToString();
                    dataset_B.Add(r.ToString() + "," + g.ToString() + "," + b.ToString());
                }
                string[] data1 = Directory.GetFiles(B + "//COLOR");
                foreach (var item in data1)
                {
                    rgb = new List<double>();
                    bgr = new List<double>();
                    string file_path = item;
                    using (Bitmap bmp = new Bitmap(file_path))
                    {
                        var color = bmp.GetPixel(bmp.Width / 2, bmp.Height / 2);
                        rgb.Add(color.R);
                        rgb.Add(color.G);
                        rgb.Add(color.B);
                    }
                    bgr = normalization(rgb);
                    b = bgr[2].ToString();
                    g = bgr[1].ToString();
                    r = bgr[0].ToString();
                    dataset_B.Add(r.ToString() + "," + g.ToString() + "," + b.ToString());
                }

            }
            dataset.Add(dataset_A);
            dataset.Add(dataset_B);
            return dataset;

        }
        public static List<double> normalization(List<double> list_input)
        {
            List<double> new_list = new List<double>();
            double newmax = 256 - 1;
            double newmin = 0;
            double max = 256 - 1;
            double min = 0;
            double scale = max / newmax;

            foreach(double i in list_input)
            {
                double x_point = Math.Round(i * (newmax - newmin) / (max - min));
                double newValue = (x_point * scale);
                new_list.Add(newValue);
            }
            return new_list;
        }
        public static void csv_export(string outputfile, List<double[]> rgbrgb)
        {
            var csv = new StringBuilder();
            var head = string.Format("{0},{1},{2},{3},{4},{5}", "r in", "g in", "b in", "r out", "g out", "b out");
            var line = "";
            csv.AppendLine(head);
            for (int i = 0; i < rgbrgb.Count; i++)
            {
                line = string.Format("{0},{1},{2},{3},{4},{5}", rgbrgb[i], rgbrgb[i], rgbrgb[i], rgbrgb[i], rgbrgb[i], rgbrgb[i]);
                csv.AppendLine(head);
            }
            File.WriteAllText(outputfile, csv.ToString());
        }
        public double[] inverse_log(double epsilon = 0.01)
        {
            double index = 0.0;
            double last_num = 100;
            double[] list_num = new double[1024];

            while (true)
            {
                double num = -1 * (Math.Log10(index + 1)) + 1;
                double diff = last_num - num;
                if (diff > epsilon && num != 0)
                {
                    index += 1;
                    last_num = num;
                }
                else
                    break;
                list_num.Append(num);
            }


            Console.WriteLine(string.Format("list_num = {0},{1}", list_num.Length, list_num));
            return list_num;
        }
        public static List<double> inverse_quadratic(double range_)
        {
            int index = 0;
            List<double> list_num = new List<double>();

            while (true)
            {
                try
                {
                    double num = -(1 / Math.Pow(range_, 2)) * Math.Pow(index , 2) + 1;
                    if (num >= 0)
                    {
                        index += 1;
                    }
                    else
                        break;
                    list_num.Add(num);
                }
                catch
                {
                    list_num.Insert(index, 0);
                    break;
                }
                
            }
            Console.WriteLine(string.Format("list_num = {0},{1}", list_num.Count, list_num));
            return list_num;
        }
    }
}
