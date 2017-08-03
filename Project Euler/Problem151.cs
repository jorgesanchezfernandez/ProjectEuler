using System.Collections.Generic;

namespace Project_Euler
{
    using System;

    class Problem151
    {
        static void Main(string[] args)
        {
            var totalNumberBranches = 0;
            var initialNumber = "2345";
            var equivalency = new Dictionary<string, string> { { "2", "345" }, { "3", "45" }, { "4", "5" }, { "5", " " } };

            // Number of branches found it for [index] matched 
            var fiveList = new List<int> { 0, 0, 0, 0 };
            var fourList = new List<int> { 1, 0, 0, 0 };
            var threeList = new List<int> { 2, 2, 0, 0};
            var twoList = new List<int>();

            var initialList = new Dictionary<string, List<int>> { { "5", fiveList }, { "4", fourList }, { "3", threeList }, { "2", twoList } };

            //Build the tree
            //Traverse taken the probability
            //Check in the same level if the number it was already calculated
            //Save this whole number of this level in a list.

        }
    }
}
