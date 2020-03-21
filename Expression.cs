namespace Countdown
{
    public abstract class Expression
    {
        public static Expression Number(int value) =>
            new NumberExpression(value);

        public static Expression Operation(Operation operation, Expression left, Expression right) =>
            new OperationExpression(operation, left, right);

        public abstract Result Evaluate();

        public abstract int NumericalInputs();

        public abstract string ToInfixNotation();

        public abstract string ToPostfixNotation();

        private class NumberExpression : Expression
        {
            private readonly int value;

            public NumberExpression(int value)
            {
                this.value = value;
            }

            public override Result Evaluate() => Result.Success(this.value);

            public override int NumericalInputs() => 1;

            public override string ToInfixNotation() => this.value.ToString();

            public override string ToPostfixNotation() => this.value.ToString();
        }

        private class OperationExpression : Expression
        {
            private readonly Operation operation;
            private readonly Expression left;
            private readonly Expression right;

            public OperationExpression(Operation operation, Expression left, Expression right)
            {
                this.operation = operation;
                this.left = left;
                this.right = right;
            }

            public override Result Evaluate()
            {
                var leftResult = this.left.Evaluate();

                if (!leftResult.IsSuccess)
                {
                    return Result.Failure(leftResult.Error);
                }

                var rightResult = this.right.Evaluate();

                if (!rightResult.IsSuccess)
                {
                    return Result.Failure(rightResult.Error);
                }

                return this.operation.Evaluate(leftResult.Value, rightResult.Value);
            }

            public override int NumericalInputs() =>
                this.left.NumericalInputs() + this.right.NumericalInputs();

            public override string ToInfixNotation() =>
                $"({this.left.ToInfixNotation()} {this.operation.Representation()} {this.right.ToInfixNotation()})";

            public override string ToPostfixNotation() =>
                $"{this.left.ToPostfixNotation()} {this.right.ToPostfixNotation()} {this.operation.Representation()}";
        }
    }
}