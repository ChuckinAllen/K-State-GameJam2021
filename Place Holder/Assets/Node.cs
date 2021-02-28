using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node NextNode;

    public Vector3 location;// = transform.position;

    private bool updated = false;
    private void Update()
    {
        if (!updated)
        {
            location = transform.position;
           // updated = true;
        }
    }
}
