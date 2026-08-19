using System;
using Godot;

public partial class MapCamera : Camera2D
{
    [Export]
    public float PanSpeed = 800f;

    [Export]
    public float ZoomSpeed = 0.1f;

    [Export]
    public float MinZoom = 0.03f;

    [Export]
    public float MaxZoom = 1.5f;

    public override void _Ready()
    {
        Position = new Vector2(16384, 16384); // adjust to your actual (width * tileSize) / 2
        Zoom = new Vector2(0.05f, 0.05f); // zoomed out enough to see the whole island
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 direction = Vector2.Zero;

        if (Input.IsKeyPressed(Key.W) || Input.IsKeyPressed(Key.Up))
        {
            direction.Y -= 1;
        }
        if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left))
        {
            direction.X -= 1;
        }
        if (Input.IsKeyPressed(Key.S) || Input.IsKeyPressed(Key.Down))
        {
            direction.Y += 1;
        }
        if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right))
        {
            direction.X += 1;
        }

        if (direction != Vector2.Zero)
        {
            // pan speed scales with zoom so it feels consistent whether zoomed in or out
            Position += direction.Normalized() * PanSpeed * (float)delta / Zoom.X;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
            {
                AdjustZoom(ZoomSpeed);
            }
            else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
            {
                AdjustZoom(-ZoomSpeed);
            }
        }
    }

    private void AdjustZoom(float amount)
    {
        float newZoom = Mathf.Clamp(Zoom.X + amount, MinZoom, MaxZoom);
        Zoom = new Vector2(newZoom, newZoom);
    }
}
