namespace Countdown
{
    public class ValidExpression
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