using Godot;
using System;

public partial class Main : Node2D
{
	private AnimationPlayer AnimationPlayer;
	[Export] private Control Dialog;
	[Export] private Database Database;

	public override void _Ready()
	{
		// Dialog = GetNode<Control>("Dialog");
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		// Add OnSceneChange into the event listener
		Database.SceneNumberChanged += OnSceneChanged;
		OnSceneChanged(Database.SceneNumber);
	}

	private async void OnSceneChanged(int sceneNumber)
	{
		if (sceneNumber == 1)
		{
			AnimationPlayer.Play("disolve_anim");
			
			// Wait 2 sec before deleting child
			await ToSignal(GetTree().CreateTimer(2.0), "timeout");
			Dialog.QueueFree();			
		}
	}
}
