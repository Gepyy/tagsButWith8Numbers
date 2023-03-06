using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Variant19
{
    internal class Program
    {
        public static int counter = 0;
        public static bool CheckGameFinish(int[,] array, int[,]num )
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i,j] == num[i, j])
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static void Move(ref int[] ZeroPosition, ref int[,] array)
        {   
                switch (Console.ReadKey().Key)
                {
                    case (ConsoleKey.RightArrow)://Right
                        {
                          if (!(ZeroPosition[0] - 1 < 0))
                          {
                            array[ZeroPosition[0], ZeroPosition[1]] = array[ZeroPosition[0] - 1, ZeroPosition[1]];
                            array[ZeroPosition[0] - 1, ZeroPosition[1]] = 0;
                            ZeroPosition[0] -= 1;
                            OutputGameOnConsole(array);
                          }
                        }
                        break;
                    case (ConsoleKey.LeftArrow)://Left
                        {
                          if (!(ZeroPosition[0] + 1 > 2))
                          {
                            array[ZeroPosition[0], ZeroPosition[1]] = array[ZeroPosition[0] + 1, ZeroPosition[1]];
                            array[ZeroPosition[0] + 1, ZeroPosition[1]] = 0;
                            ZeroPosition[0] += 1;
                            OutputGameOnConsole(array);
                          }
                        }
                        break;
                    case (ConsoleKey.UpArrow)://Up
                        {
                          if (!(ZeroPosition[1] + 1 > 2))
                          {
                            array[ZeroPosition[0], ZeroPosition[1]] = array[ZeroPosition[0], ZeroPosition[1] + 1];
                            array[ZeroPosition[0], ZeroPosition[1] + 1] = 0;
                            ZeroPosition[1] += 1;
                            OutputGameOnConsole(array);
                          }
                        }
                        break;
                    case (ConsoleKey.DownArrow)://Down
                        {
                          if (!(ZeroPosition[1] - 1 < 0))
                          {
                            array[ZeroPosition[0], ZeroPosition[1]] = array[ZeroPosition[0], ZeroPosition[1] - 1];
                            array[ZeroPosition[0], ZeroPosition[1] - 1] = 0;
                            ZeroPosition[1] -= 1;
                            OutputGameOnConsole(array);
                          }
                        }
                        break;
                }
        }
        public static void GenerateNumbers(ref int[,] array, ref int[] ZeroPosition)
        {
            List<int> possibleNumbers = Enumerable.Range(0, 9).ToList();
            List<int> Listnumber = new List<int>();
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int index = random.Next(0,possibleNumbers.Count);
                    array[i,j] = possibleNumbers[index];
                    if (possibleNumbers[index] == 0)
                    {
                        ZeroPosition[0] = i;
                        ZeroPosition[1] = j;
                    }
                    possibleNumbers.RemoveAt(index);
                }
            }
        }
        public static void OutputGameOnConsole(int[,] array)
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    try
                    {
                        Console.SetCursorPosition(2+i*4,2+j*2);
                        Console.Write(array[i,j]);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                    }
                }
            }
            Console.WriteLine($"\n \n \nКiлькiсть ходiв:{counter}");
        }

        static void Main(string[] args)
        {
            int[,] gameField = new int[3, 3];
            int[] ZeroPosition = new int[2];
            int[,] nums = { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 0 } };
            OutputGameOnConsole(nums);
            GenerateNumbers(ref gameField, ref ZeroPosition);
            OutputGameOnConsole(gameField);
            bool checker = CheckGameFinish(gameField, nums);
            while (checker)
            {
                Move(ref ZeroPosition, ref gameField);
                counter++;
                checker = CheckGameFinish(gameField, nums);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ви перемогли!");
            Console.ReadKey();
        }
    }
}
