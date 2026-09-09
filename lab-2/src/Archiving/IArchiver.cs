using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archiving;

public interface IArchiver
{
    void Save(Message message);
}