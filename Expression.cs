namespace Countdown
{
    using System;
    using System.Collections.Generic;

    public abstract class Expression
    {
        public static IComparer<Expression> Comparer { get; } = new ExpressionComparer();

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

            public static IComparer<NumberExpression> SpecificComparer { get; } = new NumberExpressionComparer();

            public override Result Evaluate() => Result.Success(this.value);

            public override int NumericalInputs() => 1;

            public override string ToInfixNotation() => this.value.ToString();

            public override string ToPostfixNotation() => this.value.ToString();

            private class NumberExpressionComparer : IComparer<NumberExpression>
            {
                public int Compare(NumberExpression x, NumberExpression y)
                {
                    return x.value.CompareTo(y.value);
                }
            }
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

            public static IComparer<OperationExpression> SpecificComparer { get; } = new OperationExpressionComparer();

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

            private class OperationExpressionComparer : IComparer<OperationExpression>
            {
                public int Compare(OperationExpression x, OperationExpression y)
                {
                    var operationComparison = Countdown.Operation.Comparer.Compare(x.operation, y.operation);

                    if (operationComparison != 0)
                    {
                        return operationComparison;
                    }

                    var leftComparison = Expression.Comparer.Compare(x.left, y.left);

                    if (leftComparison != 0)
                    {
                        return leftComparison;
                    }

                    var rightComparison = Expression.Comparer.Compare(x.right, y.right);

                    if (rightComparison != 0)
                    {
                        return rightComparison;
                    }

                    return 0;
                }
            }
        }

        private class ExpressionComparer : IComparer<Expression>
        {
            public int Compare(Expression x, Expression y)
            {
                if (x is NumberExpression numberExpressionX)
                {
                    if (y is NumberExpression numberExpressionY)
                    {
                        return NumberExpression.SpecificComparer.Compare(numberExpressionX, numberExpressionY);
                    }

                    if (y is OperationExpression)
                    {
                        return -1;
                    }

                    throw new InvalidOperationException();
                }

                if (x is OperationExpression operationExpressionX)
                {
                    if (y is NumberExpression)
                    {
                        return 1;
                    }

                    if (y is OperationExpression operationExpressionY)
                    {
                        return OperationExpression.SpecificComparer.Compare(operationExpressionX, operationExpressionY);
                    }

                    throw new InvalidOperationException();
                }

                throw new InvalidOperationException();
            }
        }
    }
}