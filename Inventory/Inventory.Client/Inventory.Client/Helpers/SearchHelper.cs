namespace Inventory.Client.Helpers
{
    using System;

    public static class SearchHelper
    {
        public static int Find(int length, Func<int, int> comparer)
        {
            var lo = 0;
            var hi = length - 1;
            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) >> 1);

                var c = comparer(mid);

                if (c == 0)
                {
                    return mid;
                }

                if (c < 0)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            return ~lo;
        }
    }
}
