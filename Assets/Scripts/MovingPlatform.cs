using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform PointA;
    public Transform PointB;
    public Transform Platform;
    public float Speed;

    Vector3 _targetPoint;

    void Start()
    {
        // reset position to A
        Platform.position = PointA.position;
        // start moving towards B
        _targetPoint = PointB.position;

        // disable renderer for A and B
        PointA.GetComponent<Renderer>().enabled = false;
        PointB.GetComponent<Renderer>().enabled = false;
    }


    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            float step = Speed * Time.fixedDeltaTime;

            // move between towards target
            Platform.position = Vector2.MoveTowards(Platform.position, _targetPoint, step);

            // if platform reached point B, change target to A
            if (Platform.position == PointB.position)
            {
                _targetPoint = PointA.position;
            }
            else if (Platform.position == PointA.position)
            {
                _targetPoint = PointB.position;
            }
        }
    }
}
