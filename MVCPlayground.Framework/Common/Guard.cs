using System;

namespace MVCPlayground.Framework.Common
{
    static class Guard
    {
        public static void AgainstNull<T>(T value, string nameOf)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameOf} cannot be null");
            }
        }
    }
}
