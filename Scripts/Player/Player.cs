using Godot;
using System;
using System.Dynamic;

public partial class Player : RigidBody2D
{
	private const float ROTATION_TORQUE = 80000.0f; // Adjust for desired rotation speed
	[Export] public RigidBody2D RightArm {get; private set;}
	[Export] public RigidBody2D LeftArm {get; private set;}
	[Export] public Area2D RightHook {get; private set;}
	[Export] public Area2D LeftHook {get; private set;}
	[Export] public RigidBody2D RightHookTip {get; private set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RightHook.BodyEntered += RightHookStick;
		RightHook.BodyExited += RightHookUnStick;
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _PhysicsProcess(double delta)
	{
		// Allow player to move right and left arm based on pivot at same time
		if (Input.IsActionPressed("rotateRightArmPos")){
            RightArm.ApplyTorque(ROTATION_TORQUE);
        }
        else if (Input.IsActionPressed("rotateRightArmNeg")){
            RightArm.ApplyTorque(-ROTATION_TORQUE);
        }
		if (Input.IsActionPressed("rotateLeftArmPos")){
            LeftArm.ApplyTorque(ROTATION_TORQUE);
        }
        else if (Input.IsActionPressed("rotateLeftArmNeg")){
            LeftArm.ApplyTorque(-ROTATION_TORQUE);
        }
		if (Input.IsActionPressed("straightenRightArm")){
			Vector2 force = (RightArm.GlobalPosition - GlobalPosition).Normalized();
			RightArm.ApplyCentralForce(force * 3000);
		}
		if (Input.IsActionPressed("straightenLeftArm")){
			Vector2 force = (LeftArm.GlobalPosition - GlobalPosition).Normalized();
			LeftArm.ApplyCentralForce(force * 4000);
		}
	}
    private void RightHookStick(Node2D body)
    {
        CallDeferred(nameof(FreezeRight));
    }
	
    private void RightHookUnStick(Node2D body)
    {
        CallDeferred(nameof(UnFreezeRight));
    }
	private void FreezeRight()
	{
		RightHookTip.Freeze = true;
	}
	private void UnFreezeRight()
	{
		RightHookTip.Freeze = false;
	}
}
