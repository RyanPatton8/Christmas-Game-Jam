using Godot;
using System;
public partial class TimerOverlay : CanvasLayer
{
	[Export] private float TimerDuration = 10.0f; // Total timer duration in seconds
	private float elapsedTime = 0.0f;

	private TextureRect timerBar;

	public override void _Ready()
	{
		// Reference the TextureRect node for the timer bar
		timerBar = GetNode<TextureRect>("TextureRect_TimerBar");
	}

	public override void _Process(float delta)
	{
		// Update elapsed time
		elapsedTime += delta;

		// Calculate the remaining ratio (with a minimum of 0.05)
		float remainingRatio = Mathf.Max(1.0f - (elapsedTime / TimerDuration), 0.05f);

		// Update the scale of the timer bar
		Vector2 scale = timerBar.Scale;
		scale.x = remainingRatio; // Adjust horizontal scale
		timerBar.Scale = scale;

		// Stop updating if the timer is complete
		if (elapsedTime >= TimerDuration)
		{
			elapsedTime = TimerDuration; // Clamp elapsed time
			SetProcess(false); // Stop the process loop
		}
	}
}
