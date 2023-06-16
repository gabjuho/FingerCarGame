using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    static float speed = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveEnemyCar(360);
        }
        if (speed <= 0.001f && speed > 0f)
        {
            speed = 0;
        }

        transform.Translate(speed, 0, 0);
        speed *= 0.96f;
    }

    public static void MoveEnemyCar(float swipeLength)
    {
        speed = swipeLength / 1300.0f;
    }
}
