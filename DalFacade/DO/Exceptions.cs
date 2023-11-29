

namespace DO;
[Serializable]
public class DalIsNotExistException : Exception
{
    public DalIsNotExistException(string? message) : base(message) { }
}
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
public class DalXMLFileLoadCreateException:Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}
public class IsNotException: Exception
{
    public IsNotException(string? message): base(message) { }
}