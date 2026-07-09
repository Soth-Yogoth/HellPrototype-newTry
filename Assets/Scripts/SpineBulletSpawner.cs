using UnityEngine;

public class SpineBulletSpawner : LineBulletSpawner
{
    // Update is called once per frame
    void Update()
    {
        base.Update();
        directionAngle++;
        OnValidate();
    }
}
