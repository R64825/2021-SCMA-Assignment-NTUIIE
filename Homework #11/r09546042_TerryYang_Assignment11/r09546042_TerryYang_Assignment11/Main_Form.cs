﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace r09546042_TerryYang_Assignment11
{
    public partial class Main_Form : Form
    {
        BbackPropagationMLP the_MLP;
        Series series_RMSE;
        int[] n;
        public Main_Form()
        {
            InitializeComponent();
        }

        public void Update_UI()
        {
            splitContainer2.Panel2.Refresh();
            Main_Chart.ChartAreas[0].RecalculateAxesScale();

            LB_RMSE.Text = "RMSE: " + Math.Round( the_MLP.RMSE, 3).ToString();
        }
        private void BTN_Reset_NN_Click(object sender, EventArgs e)
        {
            int[] number_of_Layer = new int[LSB_Layers.Items.Count];
            for (int i = 0; i < LSB_Layers.Items.Count; i++)
                number_of_Layer[i] = int.Parse(LSB_Layers.Items[i].ToString());
            the_MLP.Reset_Weights_And_Initial_Condition(number_of_Layer);

            series_RMSE = the_MLP.Series_RMSE;
            Main_Chart.Series.Clear();
            Main_Chart.Series.Add(series_RMSE);
            PPTG.SelectedObject = the_MLP;
            splitContainer2.Panel2.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;
            the_MLP = new BbackPropagationMLP();
            the_MLP.Read_Data(dlg.FileName);

            PPTG.SelectedObject = the_MLP;
        }

        private void BTN_Train_One_Click(object sender, EventArgs e)
        {
            the_MLP.TrainAnEpoch();
            Update_UI();
            // update the RMSE progress line
        }

        private void BTN_Run_To_End_Click(object sender, EventArgs e)
        {
            the_MLP.Train_To_End();
            
            int[,] confusing_Table;
            float correctneww = the_MLP.TestingClassification();

            Update_UI();
            // display results on the form
        }


        public void Draw_MLP(Graphics g, Rectangle bounds)
        {
            Font my_Font = new Font("Arial", 10.0f);
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);
            int box_width = bounds.Height / 10;
            if (box_width < 10) box_width = 10;
            Rectangle box = new Rectangle(0, 0, box_width, box_width);
            box_width /= 2;

            if (n == null) return;
            int w = bounds.Width / n.Length;
            int x_off = w / 2;
            for (int l = 0; l < n.Length; l++)
            {
                int h = n[l] / n[l];
                int y_off = h / 2;
                int x = x_off + w * l;

                for (int r = 0; r < n[l]; r++)
                {
                    int y = y_off + r * h;
                    box.X = x - box_width;
                    box.Y = y - box_width;
                    g.FillEllipse(Brushes.White, box);
                    g.DrawEllipse(Pens.Black, box);
                }
            }

            
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {
            if (the_MLP == null) return;
            the_MLP.Draw_MLP(e.Graphics,e.ClipRectangle);
        }

        private void NUD_Layer_ValueChanged(object sender, EventArgs e)
        {
            if (NUD_Layer.Value>LSB_Layers.Items.Count)
            {
                LSB_Layers.Items.Add("4");
            }
            else if(NUD_Layer.Value < LSB_Layers.Items.Count)
            {
                LSB_Layers.Items.RemoveAt(LSB_Layers.Items.Count-1);
            }
            
        }

        private void NUD_Node_ValueChanged(object sender, EventArgs e)
        {
            if (LSB_Layers.SelectedItem == null) return;
            LSB_Layers.Items[ LSB_Layers.SelectedIndex] = NUD_Node.Value.ToString();
        }

        private void LSB_Layers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LSB_Layers.SelectedItem == null) return;
            NUD_Node.Value = decimal.Parse( LSB_Layers.SelectedItem.ToString());
        }

        private void MLP_Print_DOC_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (the_MLP == null) return;
            the_MLP.Draw_MLP(e.Graphics, e.PageBounds);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = MLP_Print_DOC;
            if (dlg.ShowDialog() != DialogResult.OK) return;

        }

        private void BTN_Classification_Click(object sender, EventArgs e)
        {          
            float correctneww = the_MLP.TestingClassification();
            string str = string.Empty;

            for (int i = 0; i < the_MLP.Dimension_Target; i++)
            {
                for (int j = 0; j < the_MLP.Dimension_Target; j++)
                {
                    str += the_MLP.ConfusingTable[i, j].ToString()+" ";
                }
                str +="\n ";
            }

            LB_Test.Text = "Correctness: " + Math.Round( (correctneww/the_MLP.Number_of_Testing_Data),3).ToString() + "\n" 
                + $"({correctneww}/{the_MLP.Number_of_Testing_Data})"+ "\n"+"\n"              
                + "Confusion matrix:" + "\n" + str;
        }
    }
}
