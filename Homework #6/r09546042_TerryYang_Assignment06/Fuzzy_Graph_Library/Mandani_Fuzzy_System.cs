﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuzzy_Graph_Library
{
    public class Mandani_Fuzzy_System
    {
        // data members
        If_Then_Fuzzy_Rule[] all_Rules;
        public Mandani_Fuzzy_System(List <If_Then_Fuzzy_Rule> all_Rules)
        {
            this.all_Rules = all_Rules.ToArray();
        }
        public Fuzzy_functions_collections Fuzzy_In_Fuzzy_Out_Ingerencing(Fuzzy_functions_collections[] conditions)
        {
            //all_Rules = new If_Then_Fuzzy_Rule[]
            return null;
        }

        public double Crisp_In_Crisp_Out_Ingerencing(double[] conditions, Defuzzification defuzzy_Type)
        {
            return null;
        }

        
    }
}