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
		Database.OnEndTurnBefore += OnEndTurnBefore;
		Database.EnemyHandGestureChanged += OnEnemyHandChanged;
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

	private async void OnEndTurnBefore(bool endTurn)
	{
		if (endTurn)
		{
			Animated = true;
			Database.EndTurnAfter = false;
			await ToSignal(GetTree().CreateTimer(2), "timeout");
			Animated = false;
			RandomNumberGenerator rng = new RandomNumberGenerator();
			rng.Randomize();
			int randomHand = rng.RandiRange(0, 2);
			Database.EnemyHandGesture = randomHand;
			Database.EndTurnBefore = false;
		}
	}

	private void OnEnemyHandChanged(int enemyHandGesture)
	{
		if (IsEnemy)
			TextureRect.Texture = Database.HandTexture[enemyHandGesture];

		GD.Print("Player win?: ", IsPlayerWin());
	}

	private bool? IsPlayerWin()
	{
		int player = Database.PlayerHandGesture;
		int enemy  = Database.EnemyHandGesture;

		// draw
		if (player == enemy)
			return null;
			
		// rock <- paper <- scissor <- rock <- ...
		// example:
		// player = 1 (rock), enemy = 2 (paper)
		// (1 - 2 + 3) % 3 == 1
		// 2 % 3 != 1
		return (player - enemy + 3) % 3 == 1;
	}
}
