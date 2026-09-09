namespace Itmo.ObjectOrientedProgramming.Lab2.Notifications;

public sealed class TextSystem : INotificationSystem
{
    public void Notify(string presetMessage)
    {
        if (string.IsNullOrEmpty(presetMessage))
        {
            presetMessage = "Notification";
        }

        Console.WriteLine(presetMessage);
    }
}