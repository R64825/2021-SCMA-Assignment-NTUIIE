﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COP;
using Particle_Swamp_Optimizer;
using ABC_library;
using TerryYang_GA_Library;
using System.Windows.Forms.DataVisualization.Charting;

namespace r09546042_TerryYang_FinalProject
{
    public partial class Form_Main : Form
    {
        COPBenchmark the_Problem;
        Artificial_Bee_Colony ABC_Solver;
        Directed_Artificial_Bee_Colony dABC_Solver;
        Real_Number_Encoded_GA GA_Solver;
        Particle_Swamp_Optimizer_Solver PSO_Solver;

        public Form_Main()
        {
            InitializeComponent();
        }
        public void Reset_UI()
        {
            CT_Main.Series.Clear();
            //int ca_index = 0;
            Pro_Bar.Minimum = 0;
            if (RDB_ABC.Checked)
            {
                Pro_Bar.Maximum = ABC_Solver.Iteration_Limit;
                //CT_Main.Series.Add(ABC_Solver.Series_Average);
                //CT_Main.Series.Add(ABC_Solver.Series_IterationTheBest);
                //CT_Main.Series.Add(ABC_Solver.Series_SoFarTheBest);
            }
            else if (RDB_dABC.Checked)
            {
                Pro_Bar.Maximum = dABC_Solver.Iteration_Limit;
                //CT_Main.Series.Add(dABC_Solver.Series_Average);
                //CT_Main.Series.Add(dABC_Solver.Series_IterationTheBest);
                //CT_Main.Series.Add(dABC_Solver.Series_SoFarTheBest);
            }                
            else if (RDB_PSO.Checked)
            {
                Pro_Bar.Maximum = PSO_Solver.Iteration_Limit;
                //CT_Main.Series.Add(PSO_Solver.Series_iteration_Average_Objective);
                //CT_Main.Series.Add(PSO_Solver.Series_iteration_The_Best_Objective);
                //CT_Main.Series.Add(PSO_Solver.Series_so_Far_The_Best_Objective);
            }                
            else if (RDB_GA.Checked)
            { 
                Pro_Bar.Maximum = GA_Solver.Iteration_Limit;
                //CT_Main.Series.Add(GA_Solver.Series_Average);
                //CT_Main.Series.Add(GA_Solver.Series_IterationTheBest);
                //CT_Main.Series.Add(GA_Solver.Series_SoFarTheBest);
            }

            Pro_Bar.Value = 0;
        }
        public void Update_UI()
        {                       
            PPTG_Solver.Refresh();
            CT_Main.Series.Clear();
            CT_Main.ChartAreas[0].RecalculateAxesScale();
            if (the_Problem == null) return;
            if (RDB_ABC.Checked)
            {
                if (ABC_Solver == null) return;
                the_Problem.DisplaySolutionsOnGraphics(ABC_Solver.Food_Source_Location);
                LB_SFTBS.Text = "Solution: " + ABC_Solver.Flatten_Solution(ABC_Solver.So_Far_Best_Sol);
                LB_SFTBV.Text = "Objective: " + Math.Round(ABC_Solver.So_Far_Best_OBJ, 3).ToString();
                Pro_Bar.Value = ABC_Solver.Current_Iteration;


                // series
                CT_Main.Series.Add(ABC_Solver.Series_Average);
                CT_Main.Series.Add(ABC_Solver.Series_IterationTheBest);
                CT_Main.Series.Add(ABC_Solver.Series_SoFarTheBest);

                // BTN enable
                if (ABC_Solver.Bees == null)
                {
                    BTN_Run_One.Enabled = false;
                    BTN_Run_to_End.Enabled = false;
                }
                PPTG_Solver.SelectedObject = ABC_Solver;
            }
            else if (RDB_dABC.Checked)
            {
                if (dABC_Solver == null) return;
                the_Problem.DisplaySolutionsOnGraphics(dABC_Solver.Food_Source_Location);
                LB_SFTBS.Text = "Solution: " + dABC_Solver.Flatten_Solution(dABC_Solver.So_Far_Best_Sol);
                LB_SFTBV.Text = "Objective: " + Math.Round(dABC_Solver.So_Far_Best_OBJ, 3).ToString();
                Pro_Bar.Value = dABC_Solver.Current_Iteration;

                // series
                CT_Main.Series.Add(dABC_Solver.Series_Average);
                CT_Main.Series.Add(dABC_Solver.Series_IterationTheBest);
                CT_Main.Series.Add(dABC_Solver.Series_SoFarTheBest);

                // BTN enable
                if (dABC_Solver.Bees == null)
                {
                    BTN_Run_One.Enabled = false;
                    BTN_Run_to_End.Enabled = false;
                }
                PPTG_Solver.SelectedObject = dABC_Solver;
            }
            else if (RDB_GA.Checked)
            {
                if (GA_Solver == null) return;
                PPTG_Solver.SelectedObject = GA_Solver;
                if (GA_Solver.Chromosomes == null) return;
                the_Problem.DisplaySolutionsOnGraphics(GA_Solver.Selected_Chromosomes);
                LB_SFTBS.Text = "Solution: " + GA_Solver.Flatten_Solution_to_String(GA_Solver.So_Far_The_Best_Soulution);
                LB_SFTBV.Text = "Objective: " + Math.Round(GA_Solver.So_Far_The_Best_Objective_Value, 3).ToString();
                Pro_Bar.Value = GA_Solver.Current_Iteration;

                // series
                CT_Main.Series.Add(GA_Solver.Series_Average);
                CT_Main.Series.Add(GA_Solver.Series_IterationTheBest);
                CT_Main.Series.Add(GA_Solver.Series_SoFarTheBest);

                // BTN enable
                if (GA_Solver.Chromosomes == null)
                {
                    BTN_Run_One.Enabled = false;
                    BTN_Run_to_End.Enabled = false;
                }
                PPTG_Solver.SelectedObject = GA_Solver;
            }
            else if (RDB_PSO.Checked)
            {
                if (PSO_Solver == null) return;
                the_Problem.DisplaySolutionsOnGraphics(PSO_Solver.Positions);
                LB_SFTBS.Text = "Solution: " + PSO_Solver.Flatten_Position(PSO_Solver.So_Far_the_Best_Position);
                LB_SFTBV.Text = "Objective: " + Math.Round(PSO_Solver.So_Far_the_Best_Objective, 3).ToString();
                Pro_Bar.Value = PSO_Solver.Current_Iteration;

                // series
                CT_Main.Series.Add(PSO_Solver.Series_iteration_Average_Objective);
                CT_Main.Series.Add(PSO_Solver.Series_iteration_The_Best_Objective);
                CT_Main.Series.Add(PSO_Solver.Series_so_Far_The_Best_Objective);

                // BTN enable
                if (PSO_Solver.Positions == null)
                {
                    BTN_Run_One.Enabled = false;
                    BTN_Run_to_End.Enabled = false;
                }
                PPTG_Solver.SelectedObject = PSO_Solver;
            }

            ChartArea ca1 = CT_Main.ChartAreas[0];
            Axis ax1 = ca1.AxisY;
            ax1.ScaleView.Zoomable = true;
            ca1.CursorY.IsUserSelectionEnabled = true;
        }
        #region BTN Function
        private void BTN_Openfile_Click(object sender, EventArgs e)
        {
            the_Problem = COPBenchmark.LoadAProblemFromAFile();
            if (the_Problem == null) return;
            the_Problem.DisplayOnPanel(splitContainer1.Panel1);
            the_Problem.DisplayObjectiveGraphics(splitContainer3.Panel2);
            //Reset_UI();

            // enable
            GB_Model.Enabled = true;
            GB_Action.Enabled = true;
        }

        private void BTN_Create_Solver_Click(object sender, EventArgs e)
        {
            //GA_Solver = null;
            //PSO_Solver = null;
            //ABC_Solver = null;
            //if (RDB_ABC.Checked)
            //{
            ABC_Solver = new Artificial_Bee_Colony(the_Problem);
            PPTG_Solver.SelectedObject = ABC_Solver;
            //}
            //else if (RDB_dABC.Checked)
            //{
            dABC_Solver = new Directed_Artificial_Bee_Colony(the_Problem);
            PPTG_Solver.SelectedObject = dABC_Solver;
            //}
            //else if (RDB_PSO.Checked)
            //{
            PSO_Solver = new Particle_Swamp_Optimizer_Solver(the_Problem);
            PPTG_Solver.SelectedObject = PSO_Solver;
            //}
            //else if(RDB_GA.Checked)
            //{
            GA_Optimization_Type type;
            if (the_Problem.OptimizationGoal == OptimizationType.Minimization)
                type = GA_Optimization_Type.Minimization;
            else
                type = GA_Optimization_Type.Maximization;
            GA_Solver = new Real_Number_Encoded_GA(the_Problem.Dimension, the_Problem.LowerBound, the_Problem.UpperBound, type, the_Problem.GetObjectiveValue);
            GA_Solver.Iteration_Limit = 300;
            PPTG_Solver.SelectedObject = GA_Solver;
            //}


            Reset_UI();
            Update_UI();
            GB_Model.Enabled = false;
            BTN_Reset_Solver.Enabled = true;
        }

        private void BTN_Reset_Solver_Click(object sender, EventArgs e)
        {
            Reset_UI();
            if (RDB_ABC.Checked)
            {
                ABC_Solver.Reset();
                CT_Main.Series.Add(ABC_Solver.Series_Average);
                CT_Main.Series.Add(ABC_Solver.Series_IterationTheBest);
                CT_Main.Series.Add(ABC_Solver.Series_SoFarTheBest);
            }
            else if (RDB_dABC.Checked)
            {
                dABC_Solver.Reset();
                CT_Main.Series.Add(dABC_Solver.Series_Average);
                CT_Main.Series.Add(dABC_Solver.Series_IterationTheBest);
                CT_Main.Series.Add(dABC_Solver.Series_SoFarTheBest);
            }
            else if (RDB_PSO.Checked)
            {
                PSO_Solver.Reset();
                CT_Main.Series.Add(PSO_Solver.Series_iteration_Average_Objective);
                CT_Main.Series.Add(PSO_Solver.Series_iteration_The_Best_Objective);
                CT_Main.Series.Add(PSO_Solver.Series_so_Far_The_Best_Objective);
            }
            else if (RDB_GA.Checked)
            {
                GA_Solver.Reset();             
                CT_Main.Series.Add(GA_Solver.Series_Average);
                CT_Main.Series.Add(GA_Solver.Series_IterationTheBest);
                CT_Main.Series.Add(GA_Solver.Series_SoFarTheBest);
            }
            BTN_Run_One.Enabled = true;
            BTN_Run_to_End.Enabled = true;           
            Update_UI();
        }
        private void BTN_Run_One_Click(object sender, EventArgs e)
        {
            bool reach = true;
            if (RDB_ABC.Checked)
            {
                if (ABC_Solver.Current_Iteration < ABC_Solver.Iteration_Limit)
                {
                    ABC_Solver.Run_One_Iteration();
                    reach = false;
                }
            }
            else if (RDB_dABC.Checked)
            {
                if (dABC_Solver.Current_Iteration < dABC_Solver.Iteration_Limit)
                {
                    dABC_Solver.Run_One_Iteration();
                    reach = false;
                }
            }
            else if (RDB_PSO.Checked)
            {
                if (PSO_Solver.Current_Iteration < PSO_Solver.Iteration_Limit)
                {
                    PSO_Solver.Run_One_Iteration();
                    reach = false;
                }
            }
            else if (RDB_GA.Checked)
            {
                if (GA_Solver.Current_Iteration < GA_Solver.Iteration_Limit)
                {
                    GA_Solver.Run_One_Iteration();
                    reach = false;
                }
            }
            

            if (reach)
            {
                BTN_Create_Solver.Enabled = true;
                BTN_Reset_Solver.Enabled = false;
                BTN_Run_One.Enabled = false;
                BTN_Run_to_End.Enabled = false;
                GB_Model.Enabled = true;
            }
            else
            {
                BTN_Create_Solver.Enabled = false;
                BTN_Reset_Solver.Enabled = false;
            }
            Update_UI();
        }

        #endregion

        private void BTN_Run_to_End_Click(object sender, EventArgs e)
        {
            if (CB_Animation.Checked)
            {
                // use animation
                Timer.Enabled = true;
                BTN_Create_Solver.Enabled = false;
                BTN_Reset_Solver.Enabled = false;
            }
            else
            {
                // no animation
                if (RDB_ABC.Checked)
                    ABC_Solver.Run_To_End();
                else if (RDB_dABC.Checked)
                    dABC_Solver.Run_To_End();
                else if (RDB_GA.Checked)
                    GA_Solver.Run_To_End();
                else if (RDB_PSO.Checked)
                    PSO_Solver.Run_To_End();

                BTN_Create_Solver.Enabled = true;
                BTN_Reset_Solver.Enabled = true;
                BTN_Run_One.Enabled = true;
                BTN_Run_to_End.Enabled = true;
                GB_Model.Enabled = true;
            }
            Update_UI();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            bool reach = true;
            if (RDB_ABC.Checked)
            {
                if (ABC_Solver.Current_Iteration < ABC_Solver.Iteration_Limit)
                {
                    ABC_Solver.Run_One_Iteration();
                    reach = false;
                }
            }
            else if (RDB_dABC.Checked)
            {
                if (dABC_Solver.Current_Iteration < dABC_Solver.Iteration_Limit)
                {
                    dABC_Solver.Run_One_Iteration();
                    reach = false;
                }
            }
            else if (RDB_GA.Checked)
            {
                if (GA_Solver.Current_Iteration < GA_Solver.Iteration_Limit)
                {
                    GA_Solver.Run_One_Iteration();
                    reach = false;
                }
            }
            else if (RDB_PSO.Checked)
            {
                if (PSO_Solver.Current_Iteration < PSO_Solver.Iteration_Limit)
                {
                    PSO_Solver.Run_One_Iteration();
                    reach = false;
                }
            }

            if (reach)
            {
                Timer.Enabled = false;
                CB_Animation.Checked = false;

                BTN_Create_Solver.Enabled = true;
                BTN_Reset_Solver.Enabled = true;
                BTN_Run_One.Enabled = false;
                BTN_Run_to_End.Enabled = false;
                GB_Model.Enabled = true;
            }
            Update_UI();
        }

        private void CB_Animation_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_Animation.Checked == false)
                Timer.Enabled = false;           
        }

        private void NUD_Interval_ValueChanged(object sender, EventArgs e)
        {
            Timer.Interval = decimal.ToInt32(NUD_Interval.Value);
        }

        private void PPTG_Solver_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PPTG_Solver.Refresh();
        }

        #region RDB Check Changed

        #endregion

        private void RDB_CheckedChanged(object sender, EventArgs e)
        {
            Update_UI();
        }

        private void BTN_Compare_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form2 class
            Form_Compare Compare_Form = new Form_Compare();

            // Show the settings form
            Compare_Form.Show();
        }

        private void Form_Main_SizeChanged(object sender, EventArgs e)
        {
            Pro_Bar.Width = this.Width - 50;
        }
    }
}
