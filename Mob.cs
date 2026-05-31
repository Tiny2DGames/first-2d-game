using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var animationNames = animatedSprite2D.SpriteFrames.GetAnimationNames();
		var name = animationNames[GD.Randi() % animationNames.Length];
		animatedSprite2D.Animation = name;
		animatedSprite2D.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnScreenExited()
	{
		QueueFree();
	}
}
