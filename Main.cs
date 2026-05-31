using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private uint _score = 0;

	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");

		player.Hit += OnPlayerHit;
		player.Position = startPosition.Position;
	}

	public override void _Process(double delta)
	{
	}

	private void OnStartTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	private void OnPlayerHit()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();

		foreach (var child in GetChildren())
		{
			if (child is Mob)
			{
				child.QueueFree();
			}
		}
	}

	private void OnMobTimerTimeout()
	{
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		var player = GetNode<Player>("Player");
		var mob = MobScene.Instantiate<Mob>();

		var direction = player.Position - mobSpawnLocation.Position;
		var angle = direction.Angle();
		var velocity = new Vector2(GD.RandRange(75, 400), 0).Rotated(angle);

		mob.Rotation = angle;
		mob.LinearVelocity = velocity;
		mob.Position = mobSpawnLocation.Position;

		AddChild(mob);
	}

	private void OnScoreTimeout()
	{
		_score++;
		GD.Print($"Score: {_score}");
	}
}
