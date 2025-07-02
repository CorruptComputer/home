namespace Home;

public enum ExceptionClass
{
    Unrecoverable,
    BadProgrammer,
    BadUser,
    BadData,
}

public class SomeException(ExceptionClass exceptionClass) : Exception
{
    public ExceptionClass ExceptionClass { get; } = exceptionClass;
}
