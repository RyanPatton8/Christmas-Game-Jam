using Godot;
using System;

public partial class ReturnHome : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += BackToMenu;
	}

	private void BackToMenu(Node2D body)
	{
		body.CallDeferred("queue_free");
		CallDeferred(nameof(ChangeScene));
	}
	private void ChangeScene()
	{
		GetTree().ChangeSceneToFile("res://Scenes/menus/canvas_layer_menu.tscn");
	}
}
