using Godot;
using System;

public partial class CheckPoints : Node
{
	[Export] public Area2D WorldBorder {get; private set;}
	[Export] public Area2D C1 {get; private set;}
	[Export] public Area2D C2 {get; private set;}
	[Export] public Area2D C3 {get; private set;}
	[Export] public Area2D C4 {get; private set;}
	[Export] public Area2D C5 {get; private set;}
	[Export] public Area2D C6 {get; private set;}
	[Export] public Area2D C7 {get; private set;}
	[Export] public Area2D C8 {get; private set;}
	private PackedScene Player = (PackedScene)ResourceLoader.Load("res://Scenes/Player/Player.tscn");

	private Vector2 spawnPos = Vector2.Zero;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldBorder.BodyExited += Respawn;
		C1.BodyEntered += SetSpawn1;
		C2.BodyEntered += SetSpawn2;
		C3.BodyEntered += SetSpawn3;
		C4.BodyEntered += SetSpawn4;
		C5.BodyEntered += SetSpawn5;
		C6.BodyEntered += SetSpawn6;
		C7.BodyEntered += SetSpawn7;
		C8.BodyEntered += SetSpawn8;
	}
	public void Respawn(Node2D body)
	{
		if (IsInstanceValid(body) && body is Player)
		{
			body.CallDeferred("queue_free");
			CallDeferred(nameof(SpawnPlayer));
		}
	}

	private void SpawnPlayer()
	{
		Player instance = (Player)Player.Instantiate();
		instance.GlobalPosition = spawnPos;
		GetTree().Root.AddChild(instance);
	}
	public void SetSpawn1(Node2D body)
	{
		spawnPos = C1.GlobalPosition;
	}
	public void SetSpawn2(Node2D body)
	{
		spawnPos = C2.GlobalPosition;
	}
	public void SetSpawn3(Node2D body)
	{
		spawnPos = C3.GlobalPosition;
	}
	public void SetSpawn4(Node2D body)
	{
		spawnPos = C4.GlobalPosition;
	}
	public void SetSpawn5(Node2D body)
	{
		spawnPos = C5.GlobalPosition;
	}
	public void SetSpawn6(Node2D body)
	{
		spawnPos = C6.GlobalPosition;
	}
	public void SetSpawn7(Node2D body)
	{
		spawnPos = C7.GlobalPosition;
	}
	public void SetSpawn8(Node2D body)
	{
		spawnPos = C8.GlobalPosition;
	}
}