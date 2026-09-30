namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

// © www.sunamo.cz. All Rights Reserved.
/// <summary>
/// Builds texts of exceptions.
/// </summary>
internal sealed partial class Exceptions
{

    /// <summary>
    /// Normalizes the text before the message.
    /// </summary>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Returns text of the exception including inner exceptions.
    /// </summary>
    internal static string TextOfExceptions(Exception ex, bool alsoInner = true)
    {
        if (ex == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(ex.Message);
        if (alsoInner)
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                stringBuilder.AppendLine(ex.Message);
            }
        var result = stringBuilder.ToString();
        return result;
    }

    /// <summary>
    /// Returns type, method name and text of the place where the exception occurred.
    /// </summary>
    internal static Tuple<string, string, string> PlaceOfException(
bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceString = stackTrace.ToString();
        var lines = stackTraceString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var lineIndex = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; lineIndex < lines.Count; lineIndex++)
        {
            var item = lines[lineIndex];
            if (isFillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }
    /// <summary>
    /// Parses type and method name from the stack trace line.
    /// </summary>
    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var lineAfterAt = stackTraceLine.Split(new[] { "at " }, StringSplitOptions.None)[1].Trim();
        var methodFullName = lineAfterAt.Split('(')[0];
        var parts = methodFullName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }
    /// <summary>
    /// Returns name of the calling method.
    /// </summary>
    internal static string CallingMethod(int frameIndex = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(frameIndex)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    /// <summary>
    /// Returns custom exception message.
    /// </summary>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }
    /// <summary>
    /// Returns message with the exception passed as argument.
    /// </summary>
    internal static string? ExcAsArg(string before, Exception ex, string message)
    {
        return CheckBefore(before) + message + string.Empty + TextOfExceptions(ex);
    }
    /// <summary>
    /// Returns message about not implemented case.
    /// </summary>
    internal static string? NotImplementedCase(string before, object notImplementedName)
    {
        var suffix = string.Empty;
        if (notImplementedName != null)
        {
            suffix = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                suffix += ((Type)notImplementedName).FullName;
            else
                suffix += notImplementedName.ToString();
        }
        return CheckBefore(before) + "Not implemented case" + suffix + " . internal program error. Please contact developer" +
        ".";
    }
    /// <summary>
    /// Returns message about text that does not contain the expected parts.
    /// </summary>
    internal static string? NotContains(string before, string originalText, params string[] shouldContains)
    {
        List<string> notContained = [];
        foreach (var item in shouldContains)
            if (!originalText.Contains(item))
                notContained.Add(item);
        return notContained.Count == 0
        ? null
        : CheckBefore(before) + "Original text dont contains: " + string.Join(",", notContained) + ". Original text: " + originalText;
    }
}