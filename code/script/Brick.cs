using Godot;
using System;

public class Brick : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public int _hlt = 1;

    [Export]
    public NodePath _hltPath;

    StaticBody2D _staticBody2D;

    public Label _hltLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _hltLabel = GetNode<Label>(_hltPath);

    }

    // public void QueueFree()
    // {

    // }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }

    // public void free()
    // {
    //     GetParent().GetParent().QueueFree();
    // }
}
