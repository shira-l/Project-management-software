

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlCannotBeDeletedExeption : Exception
{
    public BlCannotBeDeletedExeption(string? message) : base(message) { }
}

[Serializable]
public class BlIncorrectDateOrderExeption : Exception
{
    public BlIncorrectDateOrderExeption(string? message) : base(message) { }
}

[Serializable]
public class BlInvalidValueExeption : Exception
{
    public BlInvalidValueExeption(string? message) : base(message) { }
}


[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}
