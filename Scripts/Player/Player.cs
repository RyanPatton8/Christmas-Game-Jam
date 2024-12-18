using Godot;
using System;
using System.Dynamic;
using System.Numerics;

public partial class Player : RigidBody2D
{
	//Arms and movement
	private const float ROTATION_TORQUE = 28000.0f;
	[Export] public RigidBody2D RightArm {get; private set;}
	[Export] public RigidBody2D LeftArm {get; private set;}

	//Groundchecks and groundcheck boolean
	[Export] public Area2D RightGroundCheck {get; private set;}
	[Export] public Area2D LeftGroundCheck {get; private set;}
	private bool canRightGrapple = false;
	private bool canLeftGrapple = false;

	//anchorpoints for hooks
	[Export] public RigidBody2D RightHook {get; private set;}
	[Export] public RigidBody2D LeftHook {get; private set;}
	public override void _Ready()
	{
		RightGroundCheck.BodyEntered += AlterRightGrapple;
		LeftGroundCheck.BodyEntered += AlterLeftGrapple;
		RightGroundCheck.BodyExited += AlterRightGrapple;
		LeftGroundCheck.BodyExited += AlterLeftGrapple;
	}
	public override void _PhysicsProcess(double delta)
	{
		ArmRotation();
		if(Input.IsActionPressed("stickRightArm") && canRightGrapple){
			HandleFreezeBody(RightHook);
		}
		else if(RightHook.Freeze){
			HandleFreezeBody(RightHook);
		}
		else{
			
		}
		if(Input.IsActionPressed("stickLeftArm") && canLeftGrapple){
			HandleFreezeBody(LeftHook);
		}
		else if(LeftHook.Freeze){
			HandleFreezeBody(LeftHook);
		}
		else{

		}
		CapAngularVelocity(RightArm);
		CapAngularVelocity(LeftArm);
	}

	private void ArmRotation()
	{
		if (Input.IsActionPressed("rotateRightArmPos"))
		{
			RightArm.ApplyTorque(ROTATION_TORQUE);
		}
		else if (Input.IsActionPressed("rotateRightArmNeg"))
		{
			RightArm.ApplyTorque(-ROTATION_TORQUE);
		}
		// Apply Torque for Left Arm Rotation
		if (Input.IsActionPressed("rotateLeftArmPos"))
		{
			LeftArm.ApplyTorque(ROTATION_TORQUE);
		}
		else if (Input.IsActionPressed("rotateLeftArmNeg"))
		{
			LeftArm.ApplyTorque(-ROTATION_TORQUE);
		}
	}
	private void CapAngularVelocity(RigidBody2D body)
	{
		const float maxAngularSpeed = 5.0f; // Limit angular speed
		if (Mathf.Abs(body.AngularVelocity) > maxAngularSpeed)
		{
			body.AngularVelocity = Mathf.Sign(body.AngularVelocity) * maxAngularSpeed;
		}
	}
	private void AlterLeftGrapple(Node2D body)
	{
		canLeftGrapple = !canLeftGrapple;
		GD.Print("LeftGrapple" + canLeftGrapple);
	}
	private void AlterRightGrapple(Node2D body)
	{
		canRightGrapple = !canRightGrapple;
		GD.Print("RightGrapple" + canRightGrapple);
	}

	private void HandleFreezeBody(RigidBody2D body){
		body.Freeze = !body.Freeze;
	}

}
