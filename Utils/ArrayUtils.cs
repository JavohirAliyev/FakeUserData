namespace FakeUserData.Utils
{
    public static class ArrayUtils
    {
        public static T[] RemoveFromArrayAtIndex<T>(this T[] originalArray, int indexToRemove)
        {
            if (originalArray == null || originalArray.Length < 1 || indexToRemove < 0)
            {
                return originalArray!;
            }

            if (indexToRemove == 0)
            {
                return [.. originalArray.Take(indexToRemove)];
            }

            T[] newArray = new T[originalArray.Length - 1];

            if (indexToRemove > 0)
            {
                Array.Copy(originalArray, 0, newArray, 0, indexToRemove);
            }

            if (indexToRemove < originalArray.Length)
            {
                Array.Copy(originalArray, indexToRemove + 1, newArray, indexToRemove, originalArray.Length - indexToRemove - 1);
            }

            return newArray;
        }
    }
}