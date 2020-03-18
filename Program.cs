namespace Countdown
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var expression = Expression.Operation(
                Operation.Addition,
                Expression.Operation(Operation.Multiplication, Expression.Number(2), Expression.Number(3)),
                Expression.Number(5));

            Console.WriteLine(expression.ToInfixNotation());
            Console.WriteLine(expression.ToPostfixNotation());
        }
    }
}
