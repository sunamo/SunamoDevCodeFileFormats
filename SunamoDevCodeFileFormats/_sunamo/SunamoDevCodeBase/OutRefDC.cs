namespace SunamoDevCodeFileFormats._sunamo;

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
    public T Item1 { get; set; }
    public U Item2 { get; set; }
}
