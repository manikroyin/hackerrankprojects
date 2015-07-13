using System;
using System.Collections.Generic;
using System.IO;
namespace HackerRankAlgorithmicProjects
{
    public partial class Solution
    {
        public static void AVeryBigSum(String[] args)
        {
            string strNum = Console.ReadLine();
            int num = Convert.ToInt32(strNum);
            string strNumArray = Console.ReadLine();
            string[] numArray = strNumArray.Split(' ');
            long sum = 0;
            for (int i = 0; i < num; i++)
            {
                sum += Convert.ToInt32(numArray[i]);
            }
            Console.WriteLine(sum);
        }
    }
}