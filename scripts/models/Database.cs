using Godot;
using System;

[GlobalClass]
public partial class Database : Resource
{
	// Csharp forces me to make a very long name idk why
	[Signal]
	public delegate void SceneNumberChangedEventHandler(int value);

	private int _sceneNumber;
	[Export]
	public int SceneNumber
	{
		get => _sceneNumber;
		set
		{
			if (_sceneNumber == value) return;
			_sceneNumber = value;
			// The signal name must be the same as the delegate name, just remove EventHandler word
			// Very interesting
			EmitSignal(SignalName.SceneNumberChanged, value);
		}
	}
}
