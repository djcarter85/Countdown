namespace Countdown
{
    using System.Collections.Generic;

    public abstract class Operation
    {
        private Operation()
        {
        }

        public static IComparer<Operation> Comparer { get; } = new OperationComparer();

        public static Operation Addition { get; } = new AdditionOperation();

        public static Operation Subtraction { get; } = new SubtractionOperation();

        public static Operation Multiplication { get; } = new MultiplicationOperation();

        public static Operation Division { get; } = new DivisionOperation();

        public static IEnumerable<Operation> All { get; } = new[] { Addition, Subtraction, Multiplication, Division };

        public abstract bool IsCommutative { get; }

        protected abstract int Ordering { get; }

        public abstract Result Evaluate(int left, int right);

        public abstract string Representation();

        private class AdditionOperation : Operation
        {
            public override bool IsCommutative => true;

            protected override int Ordering => 1;

            public override Result Evaluate(int left, int right) => Result.Success(left + right);

            public override string Representation() => "+";
        }

        private class SubtractionOperation : Operation
        {
            public override bool IsCommutative => false;

            protected override int Ordering => 2;

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

            protected override int Ordering => 3;

            public override Result Evaluate(int left, int right) => Result.Success(left * right);

            public override string Representation() => "*";
        }

        private class DivisionOperation : Operation
        {
            public override bool IsCommutative => false;

            protected override int Ordering => 4;

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

        private class OperationComparer : IComparer<Operation>
        {
            public int Compare(Operation x, Operation y)
            {
                return x.Ordering.CompareTo(y.Ordering);
            }
        }
    }
}