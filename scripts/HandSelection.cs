using Godot;
using System;

public partial class HandSelection : Control
{
	[Export] public AnimationPlayer AnimationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		VisibilityChanged += _OnVisibilityChanged;
		_OnVisibilityChanged();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _OnVisibilityChanged()
	{
		AnimationPlayer.Play("writer_anim");
	}
}
