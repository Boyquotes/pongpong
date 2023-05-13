using Godot;
using System;
using System.CodeDom.Compiler;
using System.Collections;

public class Game : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public NodePath _BallPath;

    private Node MobileAds;

    public Ball _Ball;

    [Export]
    public NodePath _Label1Path, _Label2Path;


    Label _Label1, _Label2, _Label3;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _Ball = GetNode<Ball>(_BallPath);
        MobileAds = (Node)GetNode("/root/MobileAds");
        MobileAds.Call("request_user_consent");
        MobileAds.Connect("initialization_complete", this, nameof(_on_MobileAds_initialization_complete));


        _Label1 = GetNode<Label>(_Label1Path);
        _Label2 = GetNode<Label>(_Label2Path);
    }

    private void _on_MobileAds_initialization_complete(int status, String _adapter_name)
    {
        MobileAds.Call("load_banner");
    }

#if GODOT_ANDROID
    // process
    public override void _Process(float delta)
    {
        var v = Input.GetGravity();
        v.z = 0;
        v.y = -v.y;

        v.Normalized();

        this._Label1.Text = "Gravity: " + v.ToString();
        this._Label2.Text = "Gyroscope: " + Input.GetGyroscope().ToString();

        Physics2DServer.AreaSetParam(GetViewport().FindWorld2d().Space, Physics2DServer.AreaParameter.GravityVector, v);
    }
#endif

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            switch ((ButtonList)mouseEvent.ButtonIndex)
            {
                case ButtonList.Left:
                    // GD.Print("Left button was clicked at ", mouseEvent.Position);

                    // instantiate a new ball
                    var ball = (Ball)_Ball.Duplicate();
                    _Ball.GetParent().AddChild(ball);
                    // ball.Position = mouseEvent.Position;

                    ball._rigidBody2D.Mode = RigidBody2D.ModeEnum.Rigid;
                    ball._rigidBody2D.CollisionLayer = 2;
                    ball._rigidBody2D.CollisionMask = 2;


                    // add force to drop it to left
                    // ball._rigidBody2D.ApplyCentralImpulse(new Vector2(-100, 0));

                    // generate a random number between 1 and 100
                    var random = new Random();
                    var randomNumber = random.Next(1, 100);

                    GD.Print("randomNumber: ", randomNumber);

                    ball._rigidBody2D.ApplyCentralImpulse(new Vector2(-randomNumber, 0));

                    // ball._rigidBody2D.AddForce(Vector2.Zero, new Vector2(-100, 0));

                    break;

            }
        }
    }

}
