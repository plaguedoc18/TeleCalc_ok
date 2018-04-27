using ITUniver.TeleCalc.Core;
using System;
using System.Linq;
using System.Reflection;

namespace ITUniver.TeleCalc.ConCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Calc action = new Calc();
                double x; double y;
                string opername;
                if (args.Length == 0)
                {
                    Console.WriteLine("Текущий список команд:");
                    foreach(string elem in action.GetOpers())
                    {
                        Console.Write(elem + " ");
                    }
                    Console.WriteLine("\nВведите команду из списка и данные через пробел:");
                    string[] buffer = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    opername = buffer[0].ToLower();
                    x = Convert.ToDouble(buffer[1]);
                    y = Convert.ToDouble(buffer[2]);
                }
                else
                {
                    x = Convert.ToDouble(args[1]);
                    y = Convert.ToDouble(args[2]);
                    opername = args[0].ToLower();
                }
                double? result = action.Exec(opername, x, y);
                Console.WriteLine($"{x} {opername} {y} = {result}");
                string[] argsclear = new string[0];
                Console.ReadKey();
                Main(argsclear);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Что-то пошло не так!\n" + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key for continue...");
                Console.ReadKey();
            }
        }
    }
}
