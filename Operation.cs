namespace Countdown
{
    using System.Collections.Generic;

    public abstract class Operation
    {
        private Operation()
        {
        }

        public static Operation Addition { get; } = new AdditionOperation();
        
        public static Operation Subtraction { get; } = new SubtractionOperation();
        
        public static Operation Multiplication { get; } = new MultiplicationOperation();
        
        public static Operation Division { get; } = new DivisionOperation();

        public static IEnumerable<Operation> All { get; } = new[] { Addition, Subtraction, Multiplication, Division };

        public abstract bool IsCommutative { get; }

        public abstract int Evaluate(int left, int right);

        public abstract string Representation();

        private class AdditionOperation : Operation
        {
            public override bool IsCommutative => true;
            
            public override int Evaluate(int left, int right) => left + right;

            public override string Representation() => "+";
        }

        private class SubtractionOperation : Operation
        {
            public override bool IsCommutative => false;
            
            public override int Evaluate(int left, int right)
            {
                var result = left - right;

                if (result < 0)
                {
                    throw new OperationException($"{left} - {right} is negative");
                }

                return result;
            }

            public override string Representation() => "-";
        }

        private class MultiplicationOperation : Operation
        {
            public override bool IsCommutative => true;
            
            public override int Evaluate(int left, int right) => left * right;

            public override string Representation() => "*";
        }

        private class DivisionOperation : Operation
        {
            public override bool IsCommutative => false;
            
            public override int Evaluate(int left, int right)
            {
                if (right == 0)
                {
                    throw new OperationException($"{left} / {right} makes no sense");
                }

                if (left % right != 0)
                {
                    throw new OperationException($"{left} / {right} is not an integer");
                }

                return left / right;
            }

            public override string Representation() => "/";
        }
    }
}