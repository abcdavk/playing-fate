using Godot;
using System;

public partial class Hand : Control
{
	[Export] public Database Database;
	[Export] public AnimatedSprite2D AnimatedSprite;
	[Export] public TextureRect TextureRect;
	[Export] public bool Flip;
	[Export] public bool IsEnemy;
	[Signal] public delegate void HandModeChangedEventHandler(bool value);
	private bool _animated;

	[Export] public bool Animated
	{
		get => _animated;
		set
		{
			if (_animated == value) return;
			_animated = value;

			EmitSignal(SignalName.HandModeChanged, value);
		}
	}

	public override void _Ready()
	{
		AnimatedSprite.Play();

		HandModeChanged += OnHandModeChange;
		OnHandModeChange(_animated);
		Database.PlayerHandGestureChanged += OnPlayerHandChanged;
		OnPlayerHandChanged(Database.PlayerHandGesture);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AnimatedSprite.FlipH = Flip;
		TextureRect.FlipH = Flip;
	}

	private void OnHandModeChange(bool animated)
	{
		AnimatedSprite.Visible = animated;
		TextureRect.Visible = !animated;
	}

	private void OnPlayerHandChanged(int playerHandGesture)
	{
		if (!IsEnemy)
			TextureRect.Texture = Database.HandTexture[playerHandGesture];
	}
}
