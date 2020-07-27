using System;

namespace Navigation_solution_
{
    class Program
    {
        static string directoryPath = @"..\..\..\Data";
        static string filePath = directoryPath + @"\Navigation - Test1.csv";


        static void Main(string[] args)
        {
            Reader.ReadFile(filePath);



            Console.ReadLine();
        }
    }
}
