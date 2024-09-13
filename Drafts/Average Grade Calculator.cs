using System;

namespace averageMarks
{
    internal class Programm
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("For how many students do you want to calculate an average grade: ");
            int rowsCount = Convert.ToInt32(Console.ReadLine());

            Console.Write("How many marks do you want to add for each student: ");
            int columnsCount = Convert.ToInt32(Console.ReadLine());

            // initalize all neccesry arrays 
            string[] studentNames = new string[rowsCount];
            int[,] studentMarks = new int[rowsCount, columnsCount];
            double[] averageStudentMarks = new double[rowsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                Console.Write("Enter student name: ");
                studentNames[i] = Console.ReadLine();
                
                for (int j = 0; j < columnsCount; j++)
                {
                    Console.Write($"Enter the {j + 1} mark for {studentNames[i]}: ");
                    studentMarks[i , j] = Convert.ToInt32(Console.ReadLine());
                }
                Console.Clear();
            }

            for (int i = 0; i < rowsCount; i++)
            {
                int sum = 0;
                for (int j = 0; j < columnsCount; j++)
                {
                    sum += studentMarks[i, j];
                }
                averageStudentMarks[i] = (double)sum / columnsCount;
            }
            
            
            
            for (int i = 0; i < rowsCount; i++)
            {
                int averageMarkFormated = (int) Math.Round(averageStudentMarks[i]);
                string averageMarkLetter = getMarkLetter(averageMarkFormated);
               Console.WriteLine($"Student {studentNames[i]} has average grade of {averageMarkFormated} {averageMarkLetter}
            }
        }
        
        private static string getMarkLetter(int mark)
        {
            if (mark >= 97 && mark <= 100)
                return "A+";
            else if (mark >= 93 && mark <= 96)
                return "A";
            else if (mark >= 90 && mark <= 92)
                return "A-";
            else if (mark >= 87 && mark <= 89)
                return "B+";
            else if (mark >= 83 && mark <= 86)
                return "B";
            else if (mark >= 80 && mark <= 82)
                return "B-";
            else if (mark >= 77 && mark <= 79)
                return "C+";
            else if (mark >= 73 && mark <= 76)
                return "C";
            else if (mark >= 70 && mark <= 72)
                return "C-";
            else if (mark >= 67 && mark <= 69)
                return "D+";
            else if (mark >= 63 && mark <= 66)
                return "D";
            else if (mark >= 0 && mark <= 59)
                return "F";
            else
                return string.Empty;
            
        }
        
        
    }
}
