using Godot;
using System;

public partial class Main : Node2D
{
	private AnimationPlayer AnimationPlayer;
	[Export] private Control MainMenu;
	[Export] private Control HandSelection;
	[Export] private Database Database;
	[Export] private RichTextLabel Result;
	public override void _Ready()
	{
		// MainMenu = GetNode<Control>("MainMenu");
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		// Add OnSceneChange into the event listener
		Database.SceneNumberChanged += OnSceneChanged;
		Database.OnEndTurnAfter += OnEndTurnAfter;
	}

	private async void OnSceneChanged(int sceneNumber)
	{
		if (sceneNumber == 1)
		{
			AnimationPlayer.Play("disolve_anim");
			
			// Wait 2 sec before deleting child
			await ToSignal(GetTree().CreateTimer(1.0), "timeout");
			MainMenu.QueueFree();
			HandSelection.Visible = true;
		}
	}

	private async void OnEndTurnAfter(bool endTurn)
	{
		AnimationPlayer.Play("hand_selection_disolve");
		HandSelection.MouseFilter = Control.MouseFilterEnum.Ignore;

		await ToSignal(GetTree().CreateTimer(1.0), "timeout");
		
		HandSelection.Visible = false;

		await ToSignal(GetTree().CreateTimer(1.0), "timeout");

		bool? playerWin = IsPlayerWin();
		
		GD.Print($"Player: {Database.PlayerHandGesture}");
		GD.Print($"Enemy: {Database.EnemyHandGesture}");

		GD.Print("Result: ", playerWin);

		Result.Visible = true;
		if (playerWin == true) Result.Text = "You Win!";
		else if (playerWin == false) Result.Text = "You Lose!";
		else Result.Text = "Draw!";
	}

	private bool? IsPlayerWin()
	{
		int player = Database.PlayerHandGesture;
		int enemy  = Database.EnemyHandGesture;

		int result = (player - enemy + 3) % 3;
		// draw
		if (result == 0) return null;
		if (result == 1) return true;
		return false;
	}
}
