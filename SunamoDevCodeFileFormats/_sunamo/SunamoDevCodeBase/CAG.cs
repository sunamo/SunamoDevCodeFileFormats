namespace SunamoDevCodeFileFormats._sunamo;

/// <summary>
/// Generic helpers for working with collections.
/// </summary>
internal class CAG
{

    // Direct edit - Remove duplicities from list
    // In return value is from every one instance
    // In foundedDuplicities is every duplicities (maybe the same more times)
    /// <summary>
    /// Removes duplicated elements and returns the unique ones.
    /// </summary>
    internal static List<T> RemoveDuplicitiesList<T>(IList<T> list, out List<T> foundedDuplicities)
    {
        foundedDuplicities = new List<T>();
        var uniqueItems = new List<T>();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var currentItem = list[i];
            if (!uniqueItems.Contains(currentItem))
            {
                uniqueItems.Add(currentItem);
            }
            else
            {
                list.RemoveAt(i);
                foundedDuplicities.Add(currentItem);
            }
        }

        return uniqueItems;
    }
}
