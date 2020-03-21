namespace Countdown
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var numbers = new[] { 1, 2, 3, 4 };
            var expressionCalculator = new ExpressionCalculator();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var expressions = expressionCalculator.GetAllPossibleExpressions(numbers);

            var validExpressions = new List<ValidExpression>();

            foreach (var expression in expressions)
            {
                var result = expression.Evaluate();

                if (result.IsSuccess)
                {
                    validExpressions.Add(new ValidExpression(expression, result.Value));
                }
            }

            foreach (var validExpression in validExpressions.OrderBy(ve => ve.Result))
            {
                Console.WriteLine($"{validExpression.Result,-5} = {validExpression.Expression.ToInfixNotation()}");
            }

            Console.WriteLine();
            Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }

        private class ValidExpression
        {
            public ValidExpression(Expression expression, int result)
            {
                this.Expression = expression;
                this.Result = result;
            }

            public Expression Expression { get; }

            public int Result { get; }
        }
    }
}
