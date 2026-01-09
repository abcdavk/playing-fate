using Godot;
using System;

public partial class MainMenuButton : VBoxContainer
{
	private Button _playButton;
	private Button _settingsButton;
	private Button _exitButton;

	private Database db;
	public override void _Ready()
	{
		_playButton = GetNode<Button>("Play");
		_settingsButton = GetNode<Button>("Settings");
		_exitButton = GetNode<Button>("Exit");

		_playButton.Pressed += HandlePressPlayButton;
		_settingsButton.Pressed += HandlePressSettingsButton;
		_exitButton.Pressed += HandlePressExitButton;

		db = GD.Load<Database>("res://resources/database.tres");
	}
	public override void _Process(double delta)
	{
	}

	public void HandlePressPlayButton()
	{
		GD.Print("Play");
	}

	public void HandlePressSettingsButton()
	{
		GD.Print("Settings");
	}

	public void HandlePressExitButton()
	{
		GD.Print("Exit");
	}
}
