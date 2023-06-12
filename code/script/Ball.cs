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

    public bool _moving = false;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _rigidBody2D = GetNode<RigidBody2D>(_rigidBody2DPath);
        // _kineBody2D = GetNode<KinematicBody2D>(_kineBody2DPath);

        Manager.Instance._ball = this;
    }

    // public void delayQueueFree()
    // {
    //     // queue free after 5 seconds
    //     var timer = new Timer();
    //     timer.WaitTime = 5f;
    //     timer.OneShot = true;
    //     AddChild(timer);
    //     timer.Connect("timeout", this, nameof(_on_Timer_timeout));
    //     timer.Start();
    // }

    void _on_Timer_timeout()
    {
        // QueueFree();
    }

    void _on_RigidBody2D_body_entered(Node body)
    {
        // if (body.GetParent() is Ball)
        // {
        //     return;
        // }

        // // GD.Print("body: " + body.Name, _rigidBody2D.PhysicsMaterialOverride.Absorbent);
        // var mat = _rigidBody2D.PhysicsMaterialOverride;

        // // clone the material
        // _rigidBody2D.PhysicsMaterialOverride = new PhysicsMaterial();
        // _rigidBody2D.PhysicsMaterialOverride.Friction = mat.Friction;
        // _rigidBody2D.PhysicsMaterialOverride.Rough = mat.Rough;
        // _rigidBody2D.PhysicsMaterialOverride.Bounce = mat.Bounce;
        // _rigidBody2D.PhysicsMaterialOverride.Absorbent = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }


    // var speed = 0
    long _speed = 0;
    // var direction = Vector2()
    Vector2 _direction = new Vector2();
    // var velocity = Vector2()
    Vector2 _velocity = new Vector2();

    // public override void _PhysicsProcess(float delta)
    // {
    //     if (_moving)
    //     {
    //         // var collision_info = move_and_collide(velocity * delta)
    //         var collisionInfo = _kineBody2D.MoveAndCollide(_velocity * delta);
    //         if (collisionInfo != null)
    //         {

    //             // velocity = velocity.bounce(collision_info.normal)
    //             _velocity = _velocity.Bounce(collisionInfo.Normal);

    //             // var body = collision_info.collider
    //             var body = collisionInfo.Collider;
    //             if (body is Brick brick)
    //             {
    //                 brick.free();
    //             }
    //             else
    //             {
    //                 _speed++;
    //             }

    //         }

    //     }
    // }

    public void go(Vector2 position)
    {
        // speed = 250
        _speed = 250;

        // direction = Vector2(200, -200).normalized()
        _direction = (position - this.Position).Normalized();

        _rigidBody2D.ApplyCentralImpulse(_direction * _speed);

        // velocity = speed * direction
        _velocity = _speed * _direction;

        _moving = true;
    }
}
