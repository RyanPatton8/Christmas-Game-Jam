using Godot;

public partial class Player : RigidBody2D
{
	//Arms and movement
	private const float ROTATION_TORQUE = 25000.0f;
	[Export] public RigidBody2D RightArm {get; private set;}
	[Export] public RigidBody2D LeftArm {get; private set;}
	//Groundchecks and groundcheck boolean
	[Export] public Area2D RightGroundCheck {get; private set;}
	[Export] public Area2D LeftGroundCheck {get; private set;}
	private bool canRightGrapple = false;
	private bool canLeftGrapple = false;
	private RigidBody2D rightGrappleBody = null;
	private RigidBody2D leftGrappleBody = null;
	//anchorpoints for hooks
	[Export] public PinJoint2D RightHook {get; private set;}
	[Export] public PinJoint2D LeftHook {get; private set;}
	[Export] public Marker2D RightGrapplePos {get; private set;}
	[Export] public Marker2D LeftGrapplePos {get; private set;}
	private PackedScene GrapplePoint = (PackedScene)ResourceLoader.Load("res://Scenes/Player/GrapplePoint.tscn");
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
		if(Input.IsActionJustPressed("stickRightArm") && canRightGrapple){
			rightGrappleBody = (RigidBody2D)GrapplePoint.Instantiate();
			rightGrappleBody.GlobalPosition = RightGrapplePos.GlobalPosition; // Correct position

			// Add the grapple point to the scene root, not RightArm
			GetTree().Root.AddChild(rightGrappleBody);

			// Attach the PinJoint to the grapple point
			RightHook.NodeB = rightGrappleBody.GetPath();
		}
		else if(Input.IsActionJustReleased("stickRightArm") || !canRightGrapple){
			if (rightGrappleBody != null)
			{
				GetTree().Root.RemoveChild(rightGrappleBody);
				RightHook.NodeB = null;
				rightGrappleBody.CallDeferred("queue_free");
				rightGrappleBody = null; // Clear reference
			}
		}
		if(Input.IsActionJustPressed("stickLeftArm") && canLeftGrapple){
			leftGrappleBody = (RigidBody2D)GrapplePoint.Instantiate();
			leftGrappleBody.GlobalPosition = LeftGrapplePos.GlobalPosition; // Correct position

			// Add the grapple point to the scene root, not RightArm
			GetTree().Root.AddChild(leftGrappleBody);

			// Attach the PinJoint to the grapple point
			LeftHook.NodeB = leftGrappleBody.GetPath();
		}
		else if(Input.IsActionJustReleased("stickLeftArm") || !canLeftGrapple){
			if (leftGrappleBody != null)
			{
				GetTree().Root.RemoveChild(leftGrappleBody);
				LeftHook.NodeB = null;
				leftGrappleBody.CallDeferred("queue_free");
				leftGrappleBody = null;
			}
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
	}
	private void AlterRightGrapple(Node2D body)
	{
		canRightGrapple = !canRightGrapple;
	}
}
