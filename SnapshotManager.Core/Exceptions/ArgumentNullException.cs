using SnapshotManager.Core.Attributes;

namespace SnapshotManager.Core.Exceptions;

public class ArgumentNullException : System.ArgumentNullException
{
    public ArgumentNullException() { }

    public ArgumentNullException(string paramName) : base(paramName) { }

    public ArgumentNullException(string paramName, string message) : base(paramName, message) { }

    /// <summary>Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null.</summary>
    /// <param name="argument">The reference type argument to validate as non-null.</param>
    /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
    public static void ThrowIfNull(object? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument is null)
        {
            throw new System.ArgumentNullException(paramName);
        }
    }
}