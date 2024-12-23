using Godot;

public partial class Player : RigidBody2D
{
	//Arms and movement
	private const float ROTATION_TORQUE = 40000.0f;
	[Export] public RigidBody2D RightArm {get; private set;}
	[Export] public RigidBody2D LeftArm {get; private set;}
	//Groundchecks and groundcheck boolean
	[Export] public Area2D RightGroundCheck {get; private set;}
	[Export] public Area2D LeftGroundCheck {get; private set;}
	private bool canRightGrapple = false;
	private bool canLeftGrapple = false;
	private bool grappleRPlaced = false;
	private bool grappleLPlaced = false;
	private RigidBody2D rightGrappleBody = null;
	private RigidBody2D leftGrappleBody = null;
	//anchorpoints for hooks
	[Export] public PinJoint2D RightHook {get; private set;}
	[Export] public PinJoint2D LeftHook {get; private set;}
	[Export] public Marker2D RightGrapplePos {get; private set;}
	[Export] public Marker2D LeftGrapplePos {get; private set;}
	private PackedScene GrapplePoint = (PackedScene)ResourceLoader.Load("res://Scenes/Player/GrapplePoint.tscn");

	//Assigning Signals for each node
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
		CapAngularVelocity(RightArm);
		CapAngularVelocity(LeftArm);
		StickHook();
	}
	//Applies rotational torque based on input
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
		if (Input.IsActionPressed("rotateLeftArmPos"))
		{
			LeftArm.ApplyTorque(ROTATION_TORQUE);
		}
		else if (Input.IsActionPressed("rotateLeftArmNeg"))
		{
			LeftArm.ApplyTorque(-ROTATION_TORQUE);
		}
	}
	//Caps the speed at which it can rotate but not the force it rotates with'
	/* 
		If the absolute value angular velocity of given arm is greater than what we want
		set the angular velocity based on the direction gained from sign function which returns a value between -1 and 1 inclusive 
	*/
	private void CapAngularVelocity(RigidBody2D body)
	{
		const float maxAngularSpeed = 5.0f; // Limit angular speed
		if (Mathf.Abs(body.AngularVelocity) > maxAngularSpeed)
		{
			body.AngularVelocity = Mathf.Sign(body.AngularVelocity) * maxAngularSpeed;
		}
	}
	/*
		Check if the arm is in a position in which it should be able to grab and if the player is wanting to grab it
		If yes
			Spawn a frozen object with no collision and attach a joint it it
		If no and there is a joint and object remove them
		If no
			 and there is a joint and object remove them
	*/
	public void StickHook(){
		if(Input.IsActionPressed("stickRightArm") && canRightGrapple && !grappleRPlaced){
			rightGrappleBody = (RigidBody2D)GrapplePoint.Instantiate();
			rightGrappleBody.GlobalPosition = RightGrapplePos.GlobalPosition; // Correct position

			// Add the grapple point to the scene root, not RightArm
			GetTree().Root.AddChild(rightGrappleBody);

			// Attach the PinJoint to the grapple point
			RightHook.NodeB = rightGrappleBody.GetPath();
			grappleRPlaced = true;
		}
		else if(Input.IsActionJustReleased("stickRightArm")){
			if (rightGrappleBody != null)
			{
				GetTree().Root.RemoveChild(rightGrappleBody);
				RightHook.NodeB = null;
				rightGrappleBody.CallDeferred("queue_free");
				rightGrappleBody = null; // Clear reference
				grappleRPlaced = false;
			}
		}
		if(Input.IsActionPressed("stickLeftArm") && canLeftGrapple && !grappleLPlaced){
			leftGrappleBody = (RigidBody2D)GrapplePoint.Instantiate();
			leftGrappleBody.GlobalPosition = LeftGrapplePos.GlobalPosition; // Correct position

			// Add the grapple point to the scene root, not RightArm
			GetTree().Root.AddChild(leftGrappleBody);

			// Attach the PinJoint to the grapple point
			LeftHook.NodeB = leftGrappleBody.GetPath();
			grappleLPlaced = true;
		}
		else if(Input.IsActionJustReleased("stickLeftArm")){
			if (leftGrappleBody != null)
			{
				GetTree().Root.RemoveChild(leftGrappleBody);
				LeftHook.NodeB = null;
				leftGrappleBody.CallDeferred("queue_free");
				leftGrappleBody = null;
				grappleLPlaced = false;
			}
		}
	}
	//On enter and exit set the bool for if the can grab to be the opposite of itself
	private void AlterLeftGrapple(Node2D body)
	{
		canLeftGrapple = !canLeftGrapple;
	}
	private void AlterRightGrapple(Node2D body)
	{
		canRightGrapple = !canRightGrapple;
	}
}
