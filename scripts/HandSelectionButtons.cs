using Godot;
using System;

public partial class HandSelectionButtons : VBoxContainer
{
	[Export] public Database Database;
	[Export] public Button[] Buttons;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int index = 0;
		foreach (var button in Buttons)
		{
			int id = index;
			button.Pressed += () => OnButtonPressed(id, button);
			index++;
		}

	}

	private void OnButtonPressed(int id, Button button)
	{
		Database.PlayerHandGesture = id;
	}
}
