using Godot;

public partial class Mob : CharacterBody3D {
	// Minimum speed of the mob in meters per second
    [Export] public int MinSpeed = 10;
    // Maximum speed of the mob in meters per second
    [Export] public int MaxSpeed = 18;
	// Emitted when the played jumped on the mob.
	[Signal] public delegate void SquashedEventHandler();

	private Vector3 _velocity = Vector3.Zero;	

	public override void _PhysicsProcess(double delta) {		
		Velocity = _velocity;
		MoveAndSlide();
	}

	// We will call this function from the Main scene
	public void Initialize(Vector3 startPosition, Vector3 playerPosition) {
		// Player position on ground
		Vector3 playerPositionOnGround = new Vector3(playerPosition.X, 0, playerPosition.Z);
		// We position the mob and turn it so that it looks at the player.		
		LookAtFromPosition(startPosition, playerPositionOnGround, Vector3.Up);
		// And rotate it randomly so it doesn't move exactly toward the player.
		RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));

		// We calculate a random speed.
		float randomSpeed = (float)GD.RandRange(MinSpeed, MaxSpeed);
		// We calculate a forward velocity that represents the speed.
		_velocity = Vector3.Forward * randomSpeed;
		// We then rotate the vector based on the mob's Y rotation to move in the direction it's looking
		_velocity = _velocity.Rotated(Vector3.Up, Rotation.Y);
	}

	public void OnVisibilityNotifierScreenExited(){
		QueueFree();
	}

	public void Squash() {
		EmitSignal(nameof(Squashed));
		QueueFree();
	}
}
