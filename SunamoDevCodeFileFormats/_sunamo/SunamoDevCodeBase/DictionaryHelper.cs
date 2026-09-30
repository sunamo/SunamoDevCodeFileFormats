namespace SunamoDevCodeFileFormats._sunamo;

/// <summary>
/// Helpers for working with dictionaries.
/// </summary>
internal class DictionaryHelper
{

    /// <summary>
    /// Adds the key or overwrites its value.
    /// </summary>
    internal static void AddOrSet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, T2 value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }
}
