using Godot;
using System;

public partial class CL : CanvasLayer
{
	[Export] private float TimerDuration = 10.0f; // Total timer duration in seconds
	private float elapsedTime = 0.0f;

	public TextureRect timerBar;

	public override void _Ready()
	{
		// Reference the TextureRect node for the timer bar
		timerBar = GetNode<TextureRect>("TimerBar");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Update elapsed time
		elapsedTime += (float)delta;

		// Calculate the remaining ratio (with a minimum of 0.05)
		double remainingRatio = Mathf.Max(1.0f - (elapsedTime / TimerDuration), 0.02f);

		// Update the scale of the timer bar
		Vector2 scale = timerBar.Scale;
		scale.X = (float)remainingRatio; // Adjust horizontal scale
		timerBar.Scale = scale;

		// Stop updating if the timer is complete
		if (elapsedTime >= TimerDuration)
		{
			elapsedTime = TimerDuration; // Clamp elapsed time
			SetProcess(false); // Stop the process loop
		}
	}
}
