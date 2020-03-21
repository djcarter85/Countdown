namespace Countdown
{
    using System;
    using System.Diagnostics;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var numbers = new[] { 1, 2, 3, 4 };
            var expressionCalculator = new ExpressionCalculator();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var expressions = expressionCalculator.GetAllPossibleExpressions(numbers);

            foreach (var expression in expressions)
            {
                string evaluatedExpression;
                string errorMessage;
                try
                {
                    evaluatedExpression = expression.Evaluate().ToString();
                    errorMessage = null;
                }
                catch (OperationException operationException)
                {
                    evaluatedExpression = "N/A";
                    errorMessage = $" [{operationException.Message}]";
                }

                Console.WriteLine($"{evaluatedExpression,-5} = {expression.ToInfixNotation()}{errorMessage}");
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
