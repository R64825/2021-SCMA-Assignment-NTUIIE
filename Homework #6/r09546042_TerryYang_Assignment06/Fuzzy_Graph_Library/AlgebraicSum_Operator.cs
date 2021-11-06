﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuzzy_Graph_Library
{
    public class AlgebraicSum_Operator : Binary_Operaor
    {
        public AlgebraicSum_Operator()
        {
            Name = "Algebraic Sum";
        }
        public override double Calculate_Value(double x, double y)
        {
            // return Intersection operator
            double p = x + y - x*y;
            return p;
        }
    }
}