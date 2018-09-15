
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Config
    {
        public int Order
        {
            get
            {
                return OrderRoot * OrderRoot;
            }
        }

        public int OrderRoot = 3;
        public int BaseValue = 1;

        public int[] PossibleValues
        {
            get
            {
                return Enumerable.Range(0, Order)
                    .Select(i => i + BaseValue)
                    .ToArray();
            }
        }
    }
}