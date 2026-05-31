using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMobTimerTimeout()
	{
		var mob = MobScene.Instantiate<Mob>();
		var player = GetNode<Player>("Player");

		var screenSize = GetViewport().GetVisibleRect().Size;
		
		mob.Position = screenSize * .5f;
		mob.LinearVelocity = (player.Position - mob.Position).Normalized() * GD.RandRange(150, 250);

		AddChild(mob);
	}
}
