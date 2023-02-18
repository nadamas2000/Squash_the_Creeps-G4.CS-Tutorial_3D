using Godot;

public partial class Main : Node {

#pragma warning disable 649
    // We assign this in the editor, so we don't need the warning about not being assigned.
    [Export] public PackedScene MobScene;
#pragma warning restore 649

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		GD.Randomize();
		GetNode<Control>("UserInterface/Retry").Hide();
	}

	public void OnMobTimerTimeout()	{
		// Create a new instance of the Mob scene.
		Mob mob = (Mob)MobScene.Instantiate();

		// Choose a random location on the SpawnPath.
		// We store the reference to the SpawnLocation node.
		var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		// And give it a random offset.
		mobSpawnLocation.ProgressRatio = GD.Randf();

		Vector3 playerPosition = GetNode<Player>("Player").Position;
		mob.Initialize(mobSpawnLocation.Position, playerPosition);

		AddChild(mob);

		 // We connect the mob to the score label to update the score upon squashing one.
    	mob.Squashed += GetNode<ScoreLabel>("UserInterface/ScoreLabel").OnMobSquashed;

	}

	// We also specified this function name in PascalCase in the editor's connection window
	public void OnPlayerHit() {
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Control>("UserInterface/Retry").Show();
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event.IsActionPressed("ui_accept") && GetNode<Control>("UserInterface/Retry").Visible)	{
			// This restarts the current scene.
			GetTree().ReloadCurrentScene();
		}
	}
}
