using System;
using Pandor;


class Rotate : ScriptComponent
{
    public float rotationSpeed = 2.0f;
    private float angle = 0;
    void OnUpdate(float dt)
    {
        angle += rotationSpeed * dt;

        gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}

    