using System;
using Godot;

public partial class Clock : Node
{
  [Signal]
  public delegate void TickEventHandler(int tickCount);

  public static Clock Instance { get; private set; }

  [Export]
  public float Interval = 1.0f;

  public int TickCount = 0;

  private float _accumulator = 0.0f;
  private bool _paused = false;

  // Called the instant it exists, doesnt wait for children (there are none)
  public override void _EnterTree()
  {
    Instance = this;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
    if (_paused || Interval <= 0.0f)
    {
      return;
    }

    _accumulator += (float)delta;

    while (_accumulator >= Interval)
    {
      _accumulator -= Interval;
      TickCount++;
      EmitSignal(SignalName.Tick, TickCount);
    }
  }

  public void SetInterval(float newInterval)
  {
    Interval = newInterval;
  }

  public void Pause()
  {
    _paused = true;
  }

  public void Resume()
  {
    _paused = false;
  }

  public void Reset()
  {
    TickCount = 0;
    _accumulator = 0.0f;
  }
}

// Hook into clock from another script
//
// public override void _Ready()
// {
//     GameClock.Instance.Tick += OnTick;
// }
//
// private void OnTick(int tickCount)
// {
//     GD.Print("Tick: " + tickCount);
// }
