using System.Linq;
using AllureCSharpCommons.AllureModel;

namespace AllureCSharpCommons.Utils
{
    /// <summary>
    /// Provides methods to work with arrays.
    /// </summary>
    internal static class ArraysUtils
    {
        /// <summary>
        /// Add element to array.
        /// <example>array = ArraysUtils.Add(array, element);</example>
        /// </summary>
        /// <typeparam name="T">original array type</typeparam>
        /// <param name="array">original array</param>
        /// <param name="element">new element</param>
        /// <returns>original array with new element</returns>
        internal static T[] Add<T>(T[] array, T element)
        {
            if (element != null)
            {
                if (array == null || array.Length == 0)
                {
                    array = new[] {element};
                    return array;
                }
                var buffer = new T[array.Length + 1];
                for (var i = 0; i < array.Length; i++)
                {
                    buffer[i] = array[i];
                }
                buffer[array.Length] = element;
                array = buffer;
            }
            return array;
        }

        /// <summary>
        /// Add element to array
        /// <example>array = ArraysUtils.AddAll(array, elements);</example>
        /// </summary>
        /// <typeparam name="T">original array type</typeparam>
        /// <param name="array">original array</param>
        /// <param name="elements">new element</param>
        /// <returns>original array with new elements</returns>
        internal static T[] AddAll<T>(T[] array, T[] elements)
        {
            if (elements != null && elements.Length != 0)
            {
                if (array == null || array.Length == 0)
                {
                    array = new T[elements.Length];
                    for (var i = 0; i < array.Length; i++)
                    {
                        array[i] = elements[i];
                    }
                    return array;
                }
                var buffer = new T[array.Length + elements.Length];
                for (var i = 0; i < array.Length; i++)
                {
                    buffer[i] = array[i];
                }
                for (var i = array.Length; i < array.Length + elements.Length; i++)
                {
                    buffer[i] = elements[i - array.Length];
                }
                array = buffer;
            }
            return array;
        }

        /// <summary>
        /// Add new label with specified name to labels array.
        /// <example>labels = ArraysUtils.AddLabel(labels, "newLabel", "newValue");</example>
        /// </summary>
        /// <param name="labels">original labels array</param>
        /// <param name="name">label name</param>
        /// <param name="value">label value</param>
        /// <returns></returns>
        internal static label[] AddLabel(label[] labels, string name, string value)
        {
            labels = Add(labels, new label(name, value));
            return labels;
        }
        
        /// <summary>
        /// Add new label with specified name to labels array.
        /// <example>labels = ArraysUtils.AddLabel(labels, "newLabel", "newValue", "newValue2", "newValue3");</example>
        /// </summary>
        /// <param name="labels">original labels array</param>
        /// <param name="name">label name</param>
        /// <param name="values">label values</param>
        /// <returns></returns>
        internal static label[] AddLabels(label[] labels, string name, string[] values)
        {
            var newLabels = values.Select(x => new label(name, x)).ToArray();
            labels = AddAll(labels, newLabels);
            return labels;
        }
    }
}