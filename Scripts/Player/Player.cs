using Godot;
using System;
using System.Dynamic;

public partial class Player : RigidBody2D
{
	private const float ROTATION_TORQUE = 250000.0f;
	private const float MAX_ROTATION_DEGREES = 45.0f;
	[Export] public RigidBody2D RightArm {get; private set;}
	[Export] public RigidBody2D LeftArm {get; private set;}
	public override void _Ready()
	{

	}
    public override void _PhysicsProcess(double delta)
	{
		 // Apply Torque for Right Arm Rotation
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
		CapAngularVelocity(RightArm);
        CapAngularVelocity(LeftArm);
	}

	private void CapAngularVelocity(RigidBody2D body)
    {
        const float maxAngularSpeed = 5.0f; // Limit angular speed
        if (Mathf.Abs(body.AngularVelocity) > maxAngularSpeed)
        {
            body.AngularVelocity = Mathf.Sign(body.AngularVelocity) * maxAngularSpeed;
        }
    }
}
