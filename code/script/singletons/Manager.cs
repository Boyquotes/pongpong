using Godot;
using System;


public class Manager : Node
{

    public int _score = 0;

    public static Manager Instance;

    Manager instance = null;


    public override void _Ready()
    {
        Instance = this;

        initScore();
    }

    ConfigFile config = new ConfigFile();

    void initScore()
    {
        GD.Print("initScore", config.GetValue("user", "score", 0));

        var err = config.Load("user://user.cfg");
        if (err != Error.Ok)
        {
            GD.Print("err: " + err);
        }

        _score = (int)config.GetValue("user", "score", 0);
    }

    public void on_cup_disappear()
    {
        GD.Print("on_cup_disappear");
        _score++;

        config.SetValue("user", "score", _score);
        var err = config.Save("user://user.cfg");
        if (err != Error.Ok)
        {
            GD.Print("err: " + err);
        }
    }

}
