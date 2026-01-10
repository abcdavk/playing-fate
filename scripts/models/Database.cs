using Godot;
using System;

[GlobalClass]
public partial class Database : Resource
{
	[Export] public Texture2D[] HandTexture;

	// Event listener section
	// Csharp forces me to make a very long name idk why
	[Signal] public delegate void SceneNumberChangedEventHandler(int value);
	[Signal] public delegate void PlayerHandGestureChangedEventHandler(int value);
	[Signal] public delegate void EnemyHandGestureChangedEventHandler(int value);
	[Signal] public delegate void OnEndTurnBeforeEventHandler(bool value);
	[Signal] public delegate void OnEndTurnAfterEventHandler(bool value);

	private int _sceneNumber;
	private int _playerHandGesture;
	private int _enemyHandGesture;
	private bool _endTurnBefore;
	private bool _endTurnAfter;
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

	[Export] public bool EndTurnBefore
	{
		get => _endTurnBefore;
		set
		{
			if (!value) return;
			
			_endTurnBefore = value;
			EmitSignal(SignalName.OnEndTurnBefore, value);
		}
	}
	[Export] public bool EndTurnAfter
	{
		get => _endTurnAfter;
		set
		{
			if (value) return;
			
			_endTurnAfter = value;
			EmitSignal(SignalName.OnEndTurnAfter, value);
		}
	}
}
