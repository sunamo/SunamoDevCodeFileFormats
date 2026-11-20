namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Pair of values returned from asynchronous methods.
/// </summary>
public class OutRefDC<T, U>
{
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    public OutRefDC(T firstValue, U secondValue)
    {
        Item1 = firstValue;
        Item2 = secondValue;
    }
    /// <summary>
    /// Gets or sets the item 1.
    /// </summary>
    public T Item1 { get; set; }
    /// <summary>
    /// Gets or sets the item 2.
    /// </summary>
    public U Item2 { get; set; }
}
