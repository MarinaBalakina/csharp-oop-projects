using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

public class SessionKey
{
    public Guid Value { get; }

    private SessionKey(Guid value)
    {
        Value = value;
    }

    public static SessionKey CreateNew()
    {
        return new(Guid.NewGuid());
    }

    public static OperationResultWithValue<SessionKey> FromGuid(Guid value)
    {
        if (value == Guid.Empty)
        {
            return OperationResultWithValue<SessionKey>.Fail(OperationError.InvalidSessionKey);
        }

        return OperationResultWithValue<SessionKey>.Success(new SessionKey(value));
    }
}