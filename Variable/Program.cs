using System;
using static System.Console;
namespace Variable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] score = { 100, 90, 95 };
            //string[] student = { "James", "David", "George" };

            //for (int i = 0; i < 3; i++) {
            //    // David 점수를 10점 올리기
            //    if (student[i] == "David")
            //    {
            //        score[i] = score[i] + 10;
            //        WriteLine("student is {0}, score is {1}", student[i], score[i]);
            //    }
            //}
            //Console.WriteLine("Hello, World!");

            const int MAX_LEN = 10;
            int[] score = new int[MAX_LEN];
            string appendScore = new string(' ', 20);

            for (int i = 0; i < MAX_LEN; i++)
            {
                score[i] = i + 1;

                if (score[i] % 2 == 0)
                {
                    WriteLine("{0}은(는) 짝수입니다", score[i]);
                }
                else
                {
                    WriteLine("{0}은(는) 홀수입니다", score[i]);
                }

                appendScore = string.Concat(appendScore, score[i].ToString());
            }

            long numberAppendScore = 0;
            numberAppendScore = long.Parse(appendScore);

            WriteLine("배열의 값을 모두 붙여서 만든 숫자는 {0} 입니다", numberAppendScore);

        }

    }
}
