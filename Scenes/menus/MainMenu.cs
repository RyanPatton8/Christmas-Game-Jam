using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export] public TextureButton StartButton {get; private set;}
	[Export] public TextureButton QuitButton {get; private set;}
	public override void _Ready()
	{
		StartButton.ButtonDown += StartPressed;
		QuitButton.ButtonDown += QuitPressed;
	}
	public void StartPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Levels/main_level.tscn");
	}

	public void QuitPressed()
	{
		GetTree().Quit();
	}
}
