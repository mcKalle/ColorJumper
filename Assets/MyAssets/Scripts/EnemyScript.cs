using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject rayObject;

    public float scaleFactor = 0.00000001f;

    public float minScale = 0.4f;
    public float maxScale = 1.2f;

    bool growing = true;

    private void FixedUpdate()
    {
        Vector3 newScale;

        if (growing)
        {
            newScale = new Vector3(rayObject.transform.localScale.x + scaleFactor, rayObject.transform.localScale.y + scaleFactor, rayObject.transform.localScale.z + scaleFactor);
        }
        else
        {
            newScale = new Vector3(rayObject.transform.localScale.x - scaleFactor, rayObject.transform.localScale.y - scaleFactor, rayObject.transform.localScale.z - scaleFactor);
        }

        if (rayObject.transform.localScale.x <= minScale)
        {
            growing = true;
        }
        else if (rayObject.transform.localScale.x >= maxScale)
        {
            growing = false;
        }

        rayObject.transform.localScale = newScale;
    }
}
