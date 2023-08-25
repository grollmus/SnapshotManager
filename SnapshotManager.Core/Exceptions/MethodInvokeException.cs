using System;
using System.Reflection;

namespace SnapshotManager.Core.Exceptions;

public class MethodInvokeException : Exception
{
    public MethodInvokeException(MethodInfo methodInfo)
        : this(CreateMessage(methodInfo))
    {
        ArgumentNullException.ThrowIfNull(methodInfo);
    }

    public MethodInvokeException(MethodInfo methodInfo, Exception inner)
        : this(CreateMessage(methodInfo), inner)
    {
        ArgumentNullException.ThrowIfNull(methodInfo);
    }

    public MethodInvokeException(string message)
        : base(message)
    {
    }

    public MethodInvokeException(string message, Exception inner)
        : base(message, inner)
    {
    }

    private static string CreateMessage(MethodInfo methodInfo) => $"Error on invokation of method {methodInfo.ReflectedType.Name}.{methodInfo.Name}";
}