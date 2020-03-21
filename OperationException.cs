namespace Countdown
{
    using System;

    public class OperationException : InvalidOperationException
    {
        public OperationException(string message): base(message)
        {
        }
    }
}