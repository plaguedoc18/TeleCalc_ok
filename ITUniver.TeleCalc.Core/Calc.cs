using ITUniver.TeleCalc.Core.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ITUniver.TeleCalc.Core
{
    public class Calc
    {
        private IOperation[] operations { get; set; }
        private string[] opernames { get; set; }
        public IEnumerable<string> GetOpers()
        {
            return operations.Select(o => o.Name);
        }
        private List<IOperation> GetLibrary(Assembly assembly)
        {
            var opers = new List<IOperation>();
            var classes = assembly.GetTypes();
            foreach (var item in classes)
            {
                //получаем интерфейсы, которые реализует класс
                var interfaces = item.GetInterfaces();
                var isOperation = interfaces.Any(i => i == typeof(IOperation));

                if (isOperation)
                {
                    var obj = System.Activator.CreateInstance(item) as IOperation;
                    if (obj != null)
                    {
                        opers.Add(obj);
                    }
                }
            }
            return opers;
        }
        public Calc()
        {
            var AllOpers = new List<IOperation>();
            var assemblies = new List<Assembly>() { Assembly.GetExecutingAssembly() };
            var assembly = Assembly.GetExecutingAssembly();
            const String _path = @"Z:\ITUniver\dlls\";
            if (Directory.Exists(_path))
            {
                var dlls = Directory.GetFiles(_path, "*.dll");
                assemblies.AddRange(dlls.Select(dll => Assembly.LoadFile(dll)));
            }
            foreach (var elem in assemblies)
            {
                AllOpers.AddRange(GetLibrary(elem));
            }
            operations = AllOpers.ToArray();
        }

        [Obsolete("Используйте метод Exec(operNames, args)")]
        public double Exec(string operName, double x, double y)
        {
            return Exec(operName, new double[] { x, y });
        }

        public double Exec(string operName, IEnumerable<double> Args)
        {
            IOperation operation = operations
                .FirstOrDefault(o => o.Name == operName);
            if (operation == null)
            {
                return double.NaN;
            }
            operation.Args = Args.ToArray();
            return (double)operation.Result;
        }

    #region old
        public double Sqr(double x)
        {
            return Math.Pow(x, 2);
        }
        //правильно оставить с интом
        public int Sum(int x, int y)
        {
            return x+y;
        }
        public double Sum(double x, double y)
        {
            var sum = new Sum();
            sum.Args = new double[] { x, y };
            return (double)sum.Result;
        }
        public double Sub(double x, double y)
        {
            return x - y;
        }
        public double Mult(double x, double y)
        {
            return x * y;
        }
        public double Div(double x, double y)
        {
           return x / y;
        }
        public double XOR(double x, double y)
        {
            return (int)x ^ (int)y;
        }
        #endregion
    }
}
