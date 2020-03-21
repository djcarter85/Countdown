namespace Countdown
{
    public abstract class Expression
    {
        public static Expression Number(int value) =>
            new NumberExpression(value);

        public static Expression Operation(Operation operation, Expression left, Expression right) =>
            new OperationExpression(operation, left, right);

        public abstract int Evaluate();

        public abstract string ToInfixNotation();

        public abstract string ToPostfixNotation();

        private class NumberExpression : Expression
        {
            private readonly int value;

            public NumberExpression(int value)
            {
                this.value = value;
            }

            public override int Evaluate() => this.value;

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

            public override int Evaluate()
            {
                var evaluatedLeft = this.left.Evaluate();
                var evaluatedRight = this.right.Evaluate();

                return this.operation.Evaluate(evaluatedLeft, evaluatedRight);
            }

            public override string ToInfixNotation()
            {
                return $"({this.left.ToInfixNotation()} {this.operation.Representation()} {this.right.ToInfixNotation()})";
            }

            public override string ToPostfixNotation()
            {
                return $"{this.left.ToPostfixNotation()} {this.right.ToPostfixNotation()} {this.operation.Representation()}";
            }
        }
    }
}