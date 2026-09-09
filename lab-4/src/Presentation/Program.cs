using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.ConnectCommandStep;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.FileCommandStep;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.TreeCommandStep;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeFormatting;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public class Program
{
    private static void Main(string[] args)
    {
        var fileSystemStatus = new FileSystemStatus();

        IPathService pathService = new PathService();
        IFileSystem fileSystem = new LocalFileSystem();

        TreeFormatterOptions options = TreeFormatterOptions.Default;
        ITreeFormatter treeFormatter = new TreeFormatter(options, fileSystem);

        IParser parser = new CommandParser();

        var steps = new ICommandCreateStep[]
        {
            new ConnectStep(fileSystem),
            new DisconnectStep(),
            new TreeGotoStep(pathService, fileSystem),
            new TreeListStep(pathService, treeFormatter),
            new FileShowStep(pathService, fileSystem),
            new FileMoveStep(pathService, fileSystem),
            new FileCopyStep(pathService, fileSystem),
            new FileDeleteStep(pathService, fileSystem),
            new FileRenameStep(pathService, fileSystem),
        };
        ICommandFactory commandFactory = new CommandFactory(steps);

        var env = new CommandEnvironment(fileSystemStatus);

        var runner = new CommandRunner(parser, commandFactory, env);

        Console.WriteLine("Введите команду (или exit чтобы выйти)");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (input == null) break;

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase)) break;

            CommandResult result = runner.Run(input);

            if (!string.IsNullOrWhiteSpace(result.Message))
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}