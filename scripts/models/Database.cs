using Godot;
using System;

[GlobalClass]
public partial class Database : Resource
{
	// Csharp forces me to make a very long name idk why
	[Signal] public delegate void SceneNumberChangedEventHandler(int value);
    [Signal] public delegate void PlayerHandGestureChangedEventHandler(int value);
    [Signal] public delegate void EnemyHandGestureChangedEventHandler(int value);

	private int _sceneNumber;
	private int _playerHandGesture;
	private int _enemyHandGesture;
	[Export] public int SceneNumber
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

    [Export] public int PlayerHandGesture
    {
        get => _playerHandGesture;
        set
        {
            if (_playerHandGesture == value) return;
            _playerHandGesture = value;

            EmitSignal(SignalName.PlayerHandGestureChanged, value);
        }
    }

    [Export] public int EnemyHandGesture
    {
        get => _enemyHandGesture;
        set
        {
            if (_enemyHandGesture == value) return;
            _enemyHandGesture = value;

            EmitSignal(SignalName.EnemyHandGestureChanged, value);
        }
    }
}
