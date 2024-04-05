namespace Game;

using Godot;

#if DEBUG
using System.Reflection;
using Chickensoft.GoDotTest;
#endif

public partial class Main : Node2D
{
#if DEBUG
    public TestEnvironment Environment = default!;
#endif

    public override void _Ready()
    {
#if DEBUG
        // If this is a debug build, use GoDotTest to examine the
        // command line arguments and determine if we should run tests.
        this.Environment = TestEnvironment.From(OS.GetCmdlineArgs());
        if (this.Environment.ShouldRunTests)
        {
            this.CallDeferred("RunTests");
            return;
        }
#endif
        // If we don't need to run tests, we can just switch to the game scene.
        this.GetTree().ChangeSceneToFile("res://Game/game_scene.tscn");
    }

#if DEBUG
    private void RunTests() =>
        _ = GoTest.RunTests(Assembly.GetExecutingAssembly(), this, this.Environment);
#endif
}
