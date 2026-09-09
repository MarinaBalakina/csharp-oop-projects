using Itmo.ObjectOrientedProgramming.Lab2.Archiving;
using Itmo.ObjectOrientedProgramming.Lab2.Formatting;
using Itmo.ObjectOrientedProgramming.Lab2.Messages;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients.Decorators;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients.Proxies;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class TestCases
{
    [Fact]
    public void Receive_WhenUserGetsMessage_StatusIsUnread()
    {
        var user = new User("User1");
        var recipient = new UserRecipient(user);
        var msg = new Message("H", "B", Importance.High);

        recipient.Deliver(msg);

        UserMessageRecord record = Assert.Single(user.AllMessage);
        Assert.Equal(ReadStatus.Unread, record.Status);
    }

    [Fact]
    public void MarkAsRead_WhenUnread_ChangesStatusToRead()
    {
        var user = new User("User1");
        var recipient = new UserRecipient(user);
        var msg = new Message("Header", "Body", Importance.Medium);
        recipient.Deliver(msg);
        Guid id = user.AllMessage[0].RecordId;

        user.MarkAsRead(id);

        Assert.Equal(ReadStatus.Read, user.AllMessage[0].Status);
    }

    [Fact]
    public void MarkAsRead_WhenAlreadyRead_Throws()
    {
        var user = new User("User1");
        var recipient = new UserRecipient(user);
        var message = new Message("Header", "Body", Importance.Medium);
        recipient.Deliver(message);
        Guid id = user.AllMessage[0].RecordId;

        user.MarkAsRead(id);

        Assert.Throws<InvalidOperationException>(() => user.MarkAsRead(id));
    }

    [Fact]
    public void BlocksDelivery_WhenImportanceBelowThreshold()
    {
        var recipient = new OwnMoq.RecordingRecipient();
        var proxy = new ImportanceProxy(recipient, Importance.High);
        var msg = new Message("Header", "Body", Importance.Low);

        proxy.Deliver(msg);

        Assert.Equal(0, recipient.DeliverCallCount);
        Assert.Empty(recipient.ReceivedMessages);
    }

    [Fact]
    public void WritesLog_OnDelivery()
    {
        var logger = new OwnMoq.RecordingLogger();
        var recipient = new OwnMoq.RecordingRecipient();
        var loggerdecorator = new LoggerDecorator(recipient, logger);
        var msg = new Message("Header", "Body", Importance.High);

        loggerdecorator.Deliver(msg);

        Assert.True(logger.LogCallCount >= 1);
        Assert.NotEmpty(logger.LogEntries);
        Assert.Equal(1, recipient.DeliverCallCount);
    }

    [Fact]
    public void CallsFormatterArchiver()
    {
        var storage = new OwnMoq.RecordingStorage();
        var wrapper = new FormattingWrapper(
            new HeaderFormatter(),
            new BodyFormatter(),
            storage);

        var archiver = new FormatterArchiver(wrapper);
        var msg = new Message("Header", "Body", Importance.Critical);

        archiver.Save(msg);

        Assert.Equal(2, storage.WriteCallCount);

        string first = storage.Lines[0];
        string second = storage.Lines[1];

        Assert.StartsWith("# ", first, StringComparison.Ordinal);
        Assert.Contains("Header", first, StringComparison.Ordinal);

        Assert.Contains("**Importance:** ", second, StringComparison.Ordinal);
        Assert.Contains("Critical", second, StringComparison.Ordinal);
        Assert.EndsWith("Body", second, StringComparison.Ordinal);
    }

    [Fact]
    public void Group_WithTwoRecipientsForSameUser_LowImportance_DeliveredOnce()
    {
        var user = new User("User1");
        var unfiltered = new UserRecipient(user);
        var filtered = new ImportanceProxy(unfiltered, Importance.High);

        var group = new RecipientsGroup(new List<IRecipient> { unfiltered, filtered });

        var msg = new Message("Header", "Body", Importance.Low);

        group.Deliver(msg);

        Assert.Single(user.AllMessage);
    }
}