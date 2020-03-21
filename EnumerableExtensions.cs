namespace Countdown
{
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static IEnumerable<int> Without(this IEnumerable<int> source, int itemToRemove)
        {
            var found = false;

            foreach (var item in source)
            {
                if (found || item != itemToRemove)
                {
                    yield return item;
                }
                else
                {
                    found = true;
                }
            }
        }
    }
}