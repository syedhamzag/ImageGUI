using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.backend
{
    class plotter
    {
        public static List<List<double>> plotter_(List<string> before_dataset, List<string> after_dataset)      
        {
            List<double> r_plot =   new List<double>();
            List<double> g_plot =   new List<double>();
            List<double> b_plot =   new List<double>();
            List<double> r_plot_x = new List<double>();
            List<double> g_plot_x = new List<double>();
            List<double> b_plot_x = new List<double>();
            int dimension = 256;
            int scale = 255 / (dimension - 1);
            for (int i = 0; i < dimension; i++)
            {
                int initial = (i * scale);
                r_plot.Add(initial);
                g_plot.Add(initial);
                b_plot.Add(initial);
            }
            r_plot_x = r_plot.GetRange(0, r_plot.Count);
            g_plot_x = g_plot.GetRange(0, g_plot.Count);
            b_plot_x = b_plot.GetRange(0, b_plot.Count);

            //plt.plot(r_plot_x, r_plot_x, "gray");
            List<double> r_point_history = new List<double>();
            List<double> g_point_history = new List<double>();
            List<double> b_point_history = new List<double>();
            double max_num = Math.Round(double.Parse(r_plot.Count.ToString()) -1) * double.Parse(scale.ToString());

            r_point_history.Add(0);
            g_point_history.Add(0);
            b_point_history.Add(0);
            r_point_history.Add(max_num);
            g_point_history.Add(max_num);
            b_point_history.Add(max_num);
            for (int i = 0; i < before_dataset.Count; i++)
            {
                r_point_history.Sort();
                g_point_history.Sort();
                b_point_history.Sort();
                string rgb_start_ = before_dataset[i];
                string[] rgb_start = rgb_start_.Split(',');
                string rs = rgb_start[0];
                string gs = rgb_start[1];
                string bs = rgb_start[2];

                string rgb_new_ = after_dataset[i];
                string[] rgb_new = rgb_new_.Split(',');
                string r_s = rgb_new[0];
                string g_s = rgb_new[1];
                string b_s = rgb_new[2];
                double r, g, b = 0;
                double r_, g_, b_ = 0;
                r =  double.Parse(rs);
                g =  double.Parse(gs);
                b =  double.Parse(bs);
                r_ = double.Parse(r_s);
                g_ = double.Parse(g_s);
                b_ = double.Parse(b_s);

                int r_index_in_dimension = (int.Parse(r.ToString()) / scale);
                int g_index_in_dimension = (int.Parse(g.ToString()) / scale);
                int b_index_in_dimension = (int.Parse(b.ToString()) / scale);
                double r_higher_point = 255;
                double r_lower_point = 0;
                double g_higher_point = 255;
                double g_lower_point = 0;
                double b_higher_point = 255;
                double b_lower_point = 0;

                for (int j = 0; j < r_point_history.Count; j++)
                {
                    if (r_point_history[j] == r)
                    {
                        if (j < r_point_history.Count)
                            r_higher_point = r_point_history[j + 1];
                        if (j > 0)
                            r_lower_point = r_point_history[j - 1];
                        break;
                    }
                    else if (r_point_history[j] > r)
                    {
                        r_higher_point = r_point_history[j];
                        if (j > 0)
                            r_lower_point = r_point_history[j - 1];
                        break;
                    }
                    else if (r == 255)
                    {
                        r_higher_point = 255;
                        if (j > 0)
                            r_lower_point = r_point_history[j - 1];
                        break;
                    }
                    else if (r == 0)
                    {
                        r_higher_point = r_point_history[1];
                        r_lower_point = 0;
                        break;
                    }
                    else
                    {
                        r_higher_point = 255;
                        r_lower_point = 0;
                    }
                }
                for (int j = 0; j < g_point_history.Count; j++)
                {
                    
                    if (g_point_history[j] == g)
                    {
                        if (j < g_point_history.Count)
                            g_higher_point = g_point_history[j + 1];
                        if (j > 0)
                            g_lower_point = g_point_history[j - 1];
                        break;
                    }
                    else if (g_point_history[j] > g)
                    {
                        g_higher_point = g_point_history[j];
                        if (j > 0)
                            g_lower_point = g_point_history[j - 1];
                        break;
                    }
                    else if (g == 255)
                    {
                        g_higher_point = 255;
                        if (j > 0)
                            g_lower_point = g_point_history[j - 1];
                        break;
                    }
                    else if (g == 0)
                    {
                        g_higher_point = g_point_history[1];
                        g_lower_point = 0;
                        break;
                    }
                    else
                    {
                        g_higher_point = 255;
                        g_lower_point = 0;
                    }
                }
                for (int j = 0; j < b_point_history.Count; j++)
                {
                    
                    if (b_point_history[j] == b)
                    {
                        if (j < b_point_history.Count)
                            b_higher_point = b_point_history[j + 1];
                        if (j > 0)
                            b_lower_point = b_point_history[j - 1];
                        break;
                    }


                    else if (b_point_history[j] > b)
                    {
                        b_higher_point = b_point_history[j];
                        if (j > 0)
                            b_lower_point = b_point_history[j - 1];
                        break;
                    }


                    else if (b == 255)
                    {
                        b_higher_point = 255;
                        if (j > 0)
                            b_lower_point = b_point_history[j - 1];
                        break;
                    }
                    else if (b == 0)
                    {
                        b_higher_point = b_point_history[1];
                        b_lower_point = 0;
                        break;
                    }




                    else
                    {
                        b_higher_point = 255;
                        b_lower_point = 0;
                    }

                }
                double r_delta;
                double g_delta;
                double b_delta;
                if (!r_point_history.Contains(r_index_in_dimension))
                    r_delta = r_ - r_plot[r_index_in_dimension];
                else
                    r_delta = Math.Round((r_ + r_plot[r_index_in_dimension]) / 2) - r_plot[r_index_in_dimension];

                if (!g_point_history.Contains(g_index_in_dimension))
                    g_delta = g_ - g_plot[g_index_in_dimension];
                else
                    g_delta = Math.Round((g_ + g_plot[g_index_in_dimension]) / 2) - g_plot[g_index_in_dimension];

                if (!b_point_history.Contains(b_index_in_dimension))
                    b_delta = b_ - b_plot[b_index_in_dimension];
                else
                    b_delta = Math.Round((b_ + b_plot[b_index_in_dimension]) / 2) - b_plot[b_index_in_dimension];

                double r_point_in_the_right = Math.Round((r_higher_point - r) / double.Parse(scale.ToString()));
                double r_point_in_the_left = Math.Round((r - r_lower_point) / double.Parse(scale.ToString()));

                List<double> r_eq_list_r = open_file.inverse_quadratic(r_point_in_the_right);
                List<double> r_eq_list_l = open_file.inverse_quadratic(r_point_in_the_left);

                double g_point_in_the_right = Math.Round((g_higher_point - g) / double.Parse(scale.ToString()));
                double g_point_in_the_left = Math.Round((g - g_lower_point) / double.Parse(scale.ToString()));

                List<double> g_eq_list_r = open_file.inverse_quadratic(g_point_in_the_right);
                List<double> g_eq_list_l = open_file.inverse_quadratic(g_point_in_the_left);

                double b_point_in_the_right = Math.Round((b_higher_point - b) / double.Parse(scale.ToString()));
                double b_point_in_the_left = Math.Round((b - b_lower_point) / double.Parse(scale.ToString()));

                List<double> b_eq_list_r = open_file.inverse_quadratic(b_point_in_the_right);
                List<double> b_eq_list_l = open_file.inverse_quadratic(b_point_in_the_left);

                for (int x = 0; x < r_point_in_the_right; x++)
                {
                    try
                    {
                        double new_val = r_plot[r_index_in_dimension + x] + Math.Round(r_eq_list_r[x] * r_delta);
                        if (new_val >= max_num)
                            new_val = max_num;
                        else if (new_val <= 0)
                            new_val = 0;
                        r_plot[r_index_in_dimension + x] = new_val;
                    }
                    catch
                    {
                    }

                }

                for (int x = 0; x < r_point_in_the_left; x++)
                {
                    try
                    {
                        x += 1;
                        double new_val = r_plot[r_index_in_dimension - x] + Math.Round(r_eq_list_l[x] * r_delta);
                        if (new_val >= max_num)
                            new_val = max_num;
                        else if (new_val <= 0)
                            new_val = 0;
                        r_plot[r_index_in_dimension - x] = new_val;
                        x -= 1;
                    }
                    catch
                    {
                    }
                }
                for (int x = 0; x < g_point_in_the_right; x++)
                {
                    try
                    {
                        double new_val = g_plot[g_index_in_dimension + x] + Math.Round(g_eq_list_r[x] * g_delta);
                        if (new_val >= max_num)
                            new_val = max_num;
                        else if (new_val <= 0)
                            new_val = 0;
                        g_plot[g_index_in_dimension + x] = new_val;
                    }
                    catch
                    {
                    }

                }

                for (int x = 0; x < g_point_in_the_left; x++)
                {
                    try
                    {
                        x += 1;
                        double new_val = g_plot[g_index_in_dimension - x] + Math.Round(g_eq_list_l[x] * g_delta);
                        if (new_val >= max_num)
                            new_val = max_num;
                        else if (new_val <= 0)
                            new_val = 0;
                        g_plot[g_index_in_dimension - x] = new_val;
                        x -= 1;
                    }
                    catch
                    {
                    }
                }
                for (int x = 0; x < b_point_in_the_right; x++)
                {
                    try
                    {
                        double new_val = b_plot[b_index_in_dimension + x] + Math.Round(b_eq_list_r[x] * b_delta);
                        if (new_val >= max_num)
                            new_val = max_num;
                        else if (new_val <= 0)
                            new_val = 0;
                        b_plot[b_index_in_dimension + x] = new_val;
                    }
                    catch
                    {
                    }

                }

                for (int x = 0; x < b_point_in_the_left; x++)
                {
                    try
                    {
                        x += 1;
                        double new_val = b_plot[b_index_in_dimension - x] + Math.Round(b_eq_list_l[x] * b_delta);
                        if (new_val >= max_num)
                            new_val = max_num;
                        else if (new_val <= 0)
                            new_val = 0;
                        b_plot[b_index_in_dimension - x] = new_val;
                        x -= 1;
                    }
                    catch
                    {

                    }
                }
                if (!r_point_history.Contains(r))
                    r_point_history.Add(r);
                    
                if (!g_point_history.Contains(g))
                    g_point_history.Add(g);
                
                if (!b_point_history.Contains(b))
                    b_point_history.Add(b);
                
            }
            //plt.plot(r_plot_x, r_plot, "r")
            //plt.plot(g_plot_x, g_plot, "g")
            //plt.plot(b_plot_x, b_plot, "b")
            //plt.show()
            List<List<double>> list = new List<List<double>>();
            list.Add(r_plot_x);
            list.Add(g_plot_x);
            list.Add(b_plot_x);
            list.Add(r_plot);
            list.Add(g_plot);
            list.Add(b_plot);
            return list;
        }
    }
}
