namespace Countdown
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            {
                var postfixStack =
                    PostfixStack.Empty()
                        .AddValue(10)
                        .AddValue(7)
                        .AddValue(2)
                        .AddOperation(Operation.Subtraction)
                        .AddOperation(Operation.Subtraction);

                Display(postfixStack);
            }

            Console.WriteLine();

            {
                var postfixStack =
                    PostfixStack.Empty()
                        .AddValue(10)
                        .AddValue(7)
                        .AddOperation(Operation.Subtraction)
                        .AddValue(2)
                        .AddOperation(Operation.Subtraction);

                Display(postfixStack);
            }
        }

        private static void Display(PostfixStack postfixStack)
        {
            var expression = postfixStack.ToExpression();

            Console.WriteLine("Infix: " + expression.ToInfixNotation());
            Console.WriteLine("Postfix 1: " + expression.ToPostfixNotation());
            Console.WriteLine("Postfix 2: " + postfixStack.Representation());
            Console.WriteLine("Value: " + expression.Evaluate());
        }
    }
}
