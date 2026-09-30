namespace SunamoDevCodeFileFormats._sunamo;

/// <summary>
/// Throws exceptions built by Exceptions.
/// </summary>
internal partial class ThrowEx
{

    /// <summary>
    /// Returns custom exception message.
    /// </summary>
    internal static bool Custom(string message, bool reallyThrow = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? str = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(str, reallyThrow);
    }

    /// <summary>
    /// Returns message with the exception passed as argument.
    /// </summary>
    internal static bool ExcAsArg(Exception ex, string message = "")
    { return ThrowIsNotNull(Exceptions.ExcAsArg, ex, message); }
    /// <summary>
    /// Returns message about text that does not contain the expected parts.
    /// </summary>
    internal static bool NotContains(string text, params string[] shouldContains)
    { return ThrowIsNotNull(Exceptions.NotContains(FullNameOfExecutedCode(), text, shouldContains)); }

    /// <summary>
    /// Returns message about not implemented case.
    /// </summary>
    internal static bool NotImplementedCase(object notImplementedName)
    { return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName); }

    /// <summary>
    /// Returns full name of the executed code.
    /// </summary>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type castedType)
        {
            typeFullName = castedType.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type actualType = type.GetType();
            typeFullName = actualType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws the exception when the message is not null.
    /// </summary>
    internal static bool ThrowIsNotNull(string? exception, bool reallyThrow = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (reallyThrow)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }


    /// <summary>
    /// Throws the exception when the message is not null.
    /// </summary>
    internal static bool ThrowIsNotNull<A, B>(Func<string, A, B, string?> exceptionFunction, A firstArgument, B secondArgument)
    {
        string? exception = exceptionFunction(FullNameOfExecutedCode(), firstArgument, secondArgument);
        return ThrowIsNotNull(exception);
    }

    /// <summary>
    /// Throws the exception when the message is not null.
    /// </summary>
    internal static bool ThrowIsNotNull<A>(Func<string, A, string?> exceptionFunction, A argument)
    {
        string? exception = exceptionFunction(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exception);
    }
}