using System.Linq;

namespace ITUniver.TeleCalc.Core.Operations
{
    public class Sum : IOperation
    {
        public string Name => "sum";
        public double[] Args
        {
            set { Result = value.Aggregate((x, y) => x + y); }
            get { return new double[0]; }
        }
        public double? Result { get; private set; }
        public string Error { get; }
    }
}
