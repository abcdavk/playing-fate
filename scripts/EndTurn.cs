using Godot;
using System;

public partial class EndTurn : Button
{
	[Export] public Database Database;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
		OnButtonPressed();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnButtonPressed()
	{
		Database.EndTurn = true;
	}
}
