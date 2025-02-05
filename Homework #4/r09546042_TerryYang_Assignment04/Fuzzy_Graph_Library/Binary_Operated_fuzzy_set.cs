﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Fuzzy_Graph_Library
{
    public class Binary_Operated_fuzzy_set : Fuzzy_functions_collections
    {
        private Binary_Operaor the_Operator;
        Fuzzy_functions_collections the_Fuzzy_Set_01;
        Fuzzy_functions_collections the_Fuzzy_Set_02;
        static int count_Index = 1;
        private int the_Index;

        [CategoryAttribute("Opertor Info")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Binary_Operaor The_Operator { get => the_Operator; set => the_Operator = value; }

        //public event EventHandler FFC_Parameter_Changed;
        public Binary_Operated_fuzzy_set(Binary_Operaor the_Operator, Fuzzy_functions_collections the_Fuzzy_Set_01,  Fuzzy_functions_collections the_Fuzzy_Set_02) : base(the_Fuzzy_Set_01.Fda)
        {
            this.the_Operator = the_Operator;
            this.the_Fuzzy_Set_01 = the_Fuzzy_Set_01;
            this.the_Fuzzy_Set_02 = the_Fuzzy_Set_02;
            the_Fuzzy_Set_01.FFC_Parameter_Changed += Upper_FFC_Parameter_Change;
            the_Fuzzy_Set_02.FFC_Parameter_Changed += Upper_FFC_Parameter_Change;
            the_Operator.Parameter_Changed += Upper_FFC_Parameter_Change;
            the_Index = count_Index;
            Name = the_Operator.Name + "_" + String.Format("{0:00}", ++count_Index) + $"({the_Fuzzy_Set_01.Name}, {the_Fuzzy_Set_02.Name})";

            //the_Fuzzy_Set_01.Show = false;
            //the_Fuzzy_Set_02.Show = false;
        }
        protected void Upper_FFC_Parameter_Change(object sender, EventArgs e)
        {
            Name = the_Operator.Name + "_" + String.Format("{0:00}", the_Index) + $"({the_Fuzzy_Set_01.Name}, {the_Fuzzy_Set_02.Name})";
            if (fuzzy_series.Name != "")
            {
                Generate_Series();
            }
        }
        public override double Get_Function_Value(double x)
        {
            // return operator result
            double p = the_Operator.Calculate_Value(the_Fuzzy_Set_01.Get_Function_Value(x), the_Fuzzy_Set_02.Get_Function_Value(x));
            return p;
        }
    }
}