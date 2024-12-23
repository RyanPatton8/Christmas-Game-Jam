using Godot;
using System;

public partial class WorldBorder : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyExited += PlayerOutOfBounds;
	}

	private void PlayerOutOfBounds(Node2D body)
	{
		if(body is Player player){
			GD.Print($"{player} velocity = {player.LinearVelocity}");
		}
	}
}
