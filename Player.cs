using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	override public void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Input.GetVector(
			"ui_left",
			"ui_right",
			"ui_up",
			"ui_down"
		);

		var viewport = GetViewportRect().Size;

		Position += velocity * Speed * (float)delta;
		Position = Position.Clamp(Vector2.Zero, viewport);

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity != Vector2.Zero)
		{
			animatedSprite2D.FlipH = velocity.X < 0;
			animatedSprite2D.FlipV = velocity.Y > 0;

			if (velocity.Y != 0)
			{
				animatedSprite2D.Animation = "up";
			}
			else if (velocity.X != 0)
			{
				animatedSprite2D.Animation = "walk";
			}

			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
	}
}
