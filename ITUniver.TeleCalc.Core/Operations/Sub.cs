using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.Core.Operations
{
    public class Sub : IOperation
    {
        public string Name => "sub";
        public double[] Args
        {
            set { Result = value.Aggregate((x, y) => x - y); }
            get { return new double[0]; }
        }
        public double? Result { get; private set; }
        public string Error { get; }
    }
}
