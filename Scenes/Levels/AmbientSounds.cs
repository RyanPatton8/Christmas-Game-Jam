using Godot;
using System;

public partial class AmbientSounds : Node
{
	[Export] public AudioStreamPlayer Ambience {get; private set;}
	[Export] public Area2D Forest {get; private set;}
	[Export] public Area2D Mountain {get; private set;}
	[Export] public AudioStream ForestAmbience {get; private set;}
	[Export] public AudioStream MountainAmbience {get; private set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Forest.BodyEntered += PlayForestAmbience;
		Mountain.BodyEntered += PlayMountainAmbience;
		Ambience.Finished += Repeat;
	}
	public void PlayMountainAmbience(Node2D body)
	{
		Ambience.Stream = MountainAmbience;
		Ambience.Play();
	}
	public void PlayForestAmbience(Node2D body)
	{
		Ambience.Stream = ForestAmbience;
		Ambience.Play();
	}
	public void Repeat()
	{
		Ambience.Play();
	}
}
