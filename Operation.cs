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

        public abstract Result Evaluate(int left, int right);

        public abstract string Representation();

        private class AdditionOperation : Operation
        {
            public override bool IsCommutative => true;

            public override Result Evaluate(int left, int right) => Result.Success(left + right);

            public override string Representation() => "+";
        }

        private class SubtractionOperation : Operation
        {
            public override bool IsCommutative => false;

            public override Result Evaluate(int left, int right)
            {
                var result = left - right;

                if (result < 0)
                {
                    return Result.Failure($"{left} - {right} is negative");
                }

                return Result.Success(result);
            }

            public override string Representation() => "-";
        }

        private class MultiplicationOperation : Operation
        {
            public override bool IsCommutative => true;

            public override Result Evaluate(int left, int right) => Result.Success(left * right);

            public override string Representation() => "*";
        }

        private class DivisionOperation : Operation
        {
            public override bool IsCommutative => false;

            public override Result Evaluate(int left, int right)
            {
                if (right == 0)
                {
                    return Result.Failure($"{left} / {right} makes no sense");
                }

                if (left % right != 0)
                {
                    return Result.Failure($"{left} / {right} is not an integer");
                }

                return Result.Success(left / right);
            }

            public override string Representation() => "/";
        }
    }
}