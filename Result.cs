namespace Countdown
{
    using System;

    public class Result
    {
        private readonly int value;
        private readonly string error;

        private Result(bool isSuccess, int value, string error)
        {
            this.value = value;
            this.error = error;
            this.IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }

        public int Value
        {
            get
            {
                if (!this.IsSuccess)
                {
                    throw new InvalidOperationException();
                }

                return this.value;
            }
        }

        public string Error
        {
            get
            {
                if (this.IsSuccess)
                {
                    throw new InvalidOperationException();
                }

                return this.error;
            }
        }

        public static Result Success(int value) => new Result(true, value, null);
        
        public static Result Failure(string error) => new Result(false, default, error);
    }
}