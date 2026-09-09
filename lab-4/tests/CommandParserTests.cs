using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class CommandParserTests
{
    [Fact]
    public void DisconnectCommand_ReturnsCorrectCommandName()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("disconnect");

        Assert.Equal("disconnect", result.CommandName);
        Assert.Empty(result.Arguments);
        Assert.Empty(result.Flags);
    }

    [Fact]
    public void ConnectWithModeFlag_ParsesArgumentsAndFlags()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("connect C:/tests/path -m local");

        Assert.Equal("connect", result.CommandName);

        Assert.Single(result.Arguments);
        Assert.Equal("C:/tests/path", result.Arguments[0]);

        Assert.True(result.Flags.ContainsKey("m"));
        Assert.Equal("local", result.Flags["m"]);
    }

    [Fact]
    public void ConnectWithoutModeFlag_ParsesAddressOnly()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("connect C:/tests/path");

        Assert.Equal("connect", result.CommandName);

        Assert.Single(result.Arguments);
        Assert.Equal("C:/tests/path", result.Arguments[0]);

        Assert.Empty(result.Flags);
    }

    [Fact]
    public void TreeGotoWithRelativePath_ParsesCommandAndSubcommand()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("tree goto dir1/dir2");

        Assert.Equal("tree", result.CommandName);
        Assert.Equal("goto", result.Arguments[0]);

        Assert.Equal(2, result.Arguments.Count);
        Assert.Equal("dir1/dir2", result.Arguments[1]);

        Assert.Empty(result.Flags);
    }

    [Fact]
    public void TreeListWithDepthFlag_ParsesFlag()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("tree list -d 3");

        Assert.Equal("tree", result.CommandName);
        Assert.Equal("list", result.Arguments[0]);

        Assert.Single(result.Arguments);
        Assert.True(result.Flags.ContainsKey("d"));
        Assert.Equal("3", result.Flags["d"]);
    }

    [Fact]
    public void FileShowWithModeFlag_ParsesAllParts()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("file show path/to/file.txt -m console");

        Assert.Equal("file", result.CommandName);
        Assert.Equal("show", result.Arguments[0]);

        Assert.Equal(2, result.Arguments.Count);
        Assert.Equal("path/to/file.txt", result.Arguments[1]);

        Assert.True(result.Flags.ContainsKey("m"));
        Assert.Equal("console", result.Flags["m"]);
    }

    [Fact]
    public void FileMove_ParsesTwoArguments()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("file move scr/file.txt dest/dir");

        Assert.Equal("file", result.CommandName);
        Assert.Equal("move", result.Arguments[0]);

        Assert.Equal(3, result.Arguments.Count);
        Assert.Equal("scr/file.txt", result.Arguments[1]);
        Assert.Equal("dest/dir", result.Arguments[2]);

        Assert.Empty(result.Flags);
    }

    [Fact]
    public void FileCopy_ParsesSourceAndDestination()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("file copy first.txt second.txt");

        Assert.Equal("file", result.CommandName);
        Assert.Equal("copy", result.Arguments[0]);

        Assert.Equal(3, result.Arguments.Count);
        Assert.Equal("first.txt", result.Arguments[1]);
        Assert.Equal("second.txt", result.Arguments[2]);

        Assert.Empty(result.Flags);
    }

    [Fact]
    public void FileDelete_ParsesSinglePathArgument()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("file delete path/to/file.txt");

        Assert.Equal("file", result.CommandName);
        Assert.Equal("delete", result.Arguments[0]);

        Assert.Equal(2, result.Arguments.Count);
        Assert.Equal("path/to/file.txt", result.Arguments[1]);

        Assert.Empty(result.Flags);
    }

    [Fact]
    public void FileRename_ParsesPathAndNewName()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("file rename old.txt new.txt");

        Assert.Equal("file", result.CommandName);
        Assert.Equal("rename", result.Arguments[0]);

        Assert.Equal(3, result.Arguments.Count);
        Assert.Equal("old.txt", result.Arguments[1]);
        Assert.Equal("new.txt", result.Arguments[2]);

        Assert.Empty(result.Flags);
    }

    [Fact]
    public void FlagWithoutValue_ParsesFlagWithNullValue()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("tree list -d");

        Assert.Equal("tree", result.CommandName);
        Assert.Equal("list", result.Arguments[0]);

        Assert.Single(result.Arguments);

        Assert.True(result.Flags.ContainsKey("d"));
        Assert.Null(result.Flags["d"]);
    }

    [Fact]
    public void MultipleFlags_ParsesAllFlags()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("tree list -d 3 -x test");

        Assert.Equal("tree", result.CommandName);
        Assert.Equal("list", result.Arguments[0]);

        Assert.Single(result.Arguments);

        Assert.Equal("3", result.Flags["d"]);
        Assert.Equal("test", result.Flags["x"]);
    }

    [Fact]
    public void CommandWithExtraSpaces_IgnoresEmptyTokens()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("connect   C:/root    -m  local  ");

        Assert.Equal("connect", result.CommandName);

        Assert.Single(result.Arguments);

        Assert.Equal("C:/root", result.Arguments[0]);
        Assert.Equal("local", result.Flags["m"]);
    }

    [Fact]
    public void EmptyCommand_ThrowsArgumentNullException()
    {
        var parser = new CommandParser();

        Assert.Throws<ArgumentNullException>(() => parser.Parse(string.Empty));
    }

    [Fact]
    public void TreeWithoutSubcommand_ParsesCommandWithoutArguments()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("tree");
        Assert.Equal("tree", result.CommandName);

        Assert.Empty(result.Arguments);
        Assert.Empty(result.Flags);
    }

    [Fact]
    public void FileWithoutSubcommand_ParsesCommandWithoutArguments()
    {
        var parser = new CommandParser();

        ParserResult result = parser.Parse("file");

        Assert.Equal("file", result.CommandName);
        Assert.Empty(result.Arguments);
        Assert.Empty(result.Flags);
    }
}
