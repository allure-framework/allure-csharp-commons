namespace AllureCSharpCommons.Utils
{
    internal static class ArraysUtils
    {
        internal static T[] Add<T>(T[] array, T element)
        {
            if (element != null)
            {
                if (array == null || array.Length == 0)
                {
                    array = new[] { element };
                    return array;
                }
                var buffer = new T[array.Length + 1];
                for (int i = 0; i < array.Length; i++)
                {
                    buffer[i] = array[i];
                }
                buffer[array.Length] = element;
                array = buffer;
            }
            return array;
        }

        internal static T[] AddRange<T>(T[] array, T[] elements)
        {
            if (elements != null && elements.Length != 0)
            {
                if (array == null || array.Length == 0)
                {
                    array = new T[elements.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = elements[i];
                    }
                    return array;
                }
                var buffer = new T[array.Length + elements.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    buffer[i] = array[i];
                }
                for (int i = array.Length; i < array.Length + elements.Length; i++)
                {
                    buffer[i] = elements[i - array.Length];
                }
                array = buffer;
            }
            return array;
        }
    }
}
