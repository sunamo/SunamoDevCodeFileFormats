namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

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
        for (int index = list.Count - 1; index >= 0; index--)
        {
            var currentItem = list[index];
            if (!uniqueItems.Contains(currentItem))
            {
                uniqueItems.Add(currentItem);
            }
            else
            {
                list.RemoveAt(index);
                foundedDuplicities.Add(currentItem);
            }
        }

        return uniqueItems;
    }
}
