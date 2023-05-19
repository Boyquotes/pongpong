using Godot;
using System;
using System.ComponentModel;

public class Cup : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // signal for cup disappear
    [Signal]
    public delegate void cupDisappear();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // get space width
        var space = GetViewport().Size;

        AddChild(_tween1);
        _tween1.Connect("tween_all_completed", this, nameof(on_tween_all_completed1));
        AddChild(_tween2);
        _tween2.Connect("tween_all_completed", this, nameof(on_tween_all_completed2));


        // random pick 1 or 0
        var random = new Random();
        var bingo = random.Next(0, 2);
        // GD.Print("bingo: " + bingo);
        if (bingo == 0)
        {
            on_tween_all_completed1();
        }
        else
        {
            on_tween_all_completed2();
        }

        this.Connect(nameof(cupDisappear), Manager.Instance, nameof(Manager.on_cup_disappear));
        this.Connect(nameof(cupDisappear), Manager.Instance._game, nameof(Manager.Instance._game.on_cup_disappear));
    }


    Tween _tween1 = new Tween();
    Tween _tween2 = new Tween();


    void on_tween_all_completed1()
    {
        var space = GetViewport().Size;
        space.x -= 30;

        _tween2.InterpolateProperty(this, "position", new Vector2(space.x / 2, Position.y), new Vector2(-space.x / 2, Position.y), 3f, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        _tween2.Start();


        // GD.Print("tween1_all_completed");
    }

    void on_tween_all_completed2()
    {
        var space = GetViewport().Size;
        space.x -= 30;

        _tween1.InterpolateProperty(this, "position", new Vector2(-space.x / 2, Position.y), new Vector2(space.x / 2, Position.y), 3f, Tween.TransitionType.Linear, Tween.EaseType.InOut);

        _tween1.Start();

        // GD.Print("tween2_all_completed");
    }


    void _on_Area2D_body_entered(Node body)
    {
        GD.Print("body: " + body);
        if (body.GetParent() is Ball)
        {
            this.QueueFree();
            body.QueueFree();

            // emit signal
            EmitSignal(nameof(cupDisappear), this.GlobalPosition);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
