using Godot;
using System;

public partial class MainMenuButton : VBoxContainer
{
	private Button _playButton;
	private Button _settingsButton;
	private Button _exitButton;

	[Export] public Database Database;
	public override void _Ready()
	{
		_playButton = GetNode<Button>("Play");
		_settingsButton = GetNode<Button>("Settings");
		_exitButton = GetNode<Button>("Exit");

		_playButton.Pressed += HandlePressPlayButton;
		_settingsButton.Pressed += HandlePressSettingsButton;
		_exitButton.Pressed += HandlePressExitButton;
	}
	public override void _Process(double delta)
	{
	}

	private void HandlePressPlayButton()
	{
		Database.SceneNumber += 1; 
		GD.Print("Play");
	}

	private void HandlePressSettingsButton()
	{
		GD.Print("Settings");
	}

	private void HandlePressExitButton()
	{
		GD.Print("Exit");
	}
}
