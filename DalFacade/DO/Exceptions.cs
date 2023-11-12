

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
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}




