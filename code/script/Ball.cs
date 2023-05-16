using Godot;
using System;

public class Ball : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public NodePath _rigidBody2DPath;


    public RigidBody2D _rigidBody2D;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _rigidBody2D = GetNode<RigidBody2D>(_rigidBody2DPath);
    }


    public void delayQueueFree()
    {
        // queue free after 5 seconds
        var timer = new Timer();
        timer.WaitTime = 5f;
        timer.OneShot = true;
        AddChild(timer);
        timer.Connect("timeout", this, nameof(_on_Timer_timeout));
        timer.Start();
    }

    void _on_Timer_timeout()
    {
        QueueFree();
    }

    void _on_RigidBody2D_body_shape_entered(RID rid, Node body, int bodyIndex, int shapeIndex)
    {
        // GD.Print("body: " + body.Name, _rigidBody2D.PhysicsMaterialOverride.Absorbent);
        var mat = _rigidBody2D.PhysicsMaterialOverride;

        // clone the material
        _rigidBody2D.PhysicsMaterialOverride = new PhysicsMaterial();
        _rigidBody2D.PhysicsMaterialOverride.Friction = mat.Friction;
        _rigidBody2D.PhysicsMaterialOverride.Rough = mat.Rough;
        _rigidBody2D.PhysicsMaterialOverride.Bounce = mat.Bounce;
        _rigidBody2D.PhysicsMaterialOverride.Absorbent = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
