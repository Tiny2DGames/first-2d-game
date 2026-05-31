using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Player>("Player").Hit += OnPlayerHit;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnPlayerHit()
	{
		GetTree().ReloadCurrentScene();
	}

	private void OnMobTimerTimeout()
	{
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		var player = GetNode<Player>("Player");
		var mob = MobScene.Instantiate<Mob>();

		var direction = player.Position - mobSpawnLocation.Position;
		var angle = direction.Angle();
		var velocity = new Vector2(GD.RandRange(25, 400), 0).Rotated(angle);

		mob.Rotation = angle;
		mob.LinearVelocity = velocity;
		mob.Position = mobSpawnLocation.Position;

		AddChild(mob);
	}
}
