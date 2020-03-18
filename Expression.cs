namespace Countdown
{
    using System;

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

                return this.operation switch
                {
                    Countdown.Operation.Addition => (evaluatedLeft + evaluatedRight),
                    Countdown.Operation.Subtraction => (evaluatedLeft - evaluatedRight), // TODO throw if negative
                    Countdown.Operation.Multiplication => (evaluatedLeft * evaluatedRight),
                    Countdown.Operation.Division => (evaluatedLeft / evaluatedRight), // TODO throw if fractional
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            public override string ToInfixNotation()
            {
                return $"({this.left.ToInfixNotation()} {this.GetOperatorSymbol()} {this.right.ToInfixNotation()})";
            }

            public override string ToPostfixNotation()
            {
                return $"{this.left.ToPostfixNotation()} {this.right.ToPostfixNotation()} {this.GetOperatorSymbol()}";
            }

            private string GetOperatorSymbol()
            {
                return this.operation switch
                {
                    Countdown.Operation.Addition => "+",
                    Countdown.Operation.Subtraction => "-",
                    Countdown.Operation.Multiplication => "*",
                    Countdown.Operation.Division => "/",
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}