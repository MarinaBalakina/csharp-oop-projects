namespace Itmo.ObjectOrientedProgramming.Lab2.Notifications;

public sealed class SoundSystem : INotificationSystem
{
    public void Notify(string presetMessage)
    {
        if (OperatingSystem.IsWindows())
        {
            Console.Beep(800, 200);
        }
        else
        {
            Console.WriteLine(presetMessage);
        }
    }
}