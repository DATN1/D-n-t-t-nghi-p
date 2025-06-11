using UnityEngine;

public class FakeJoystick : FixedJoystick
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public void SetInput(float h, float v)
    {
        Horizontal = h;
        Vertical = v;
    }
}
