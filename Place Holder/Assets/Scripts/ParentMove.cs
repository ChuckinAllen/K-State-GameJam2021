using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParentMove : MonoBehaviour
{
    [SerializeField]
    public float speed = .1f;
    [SerializeField]
    public Node CurrNode;
    [SerializeField]
    public float SearchTime = 60f;
    [SerializeField]
    public float NodeDetectionRange = .1f;
    
    private bool lookingAround;
    public float searchTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        lookingAround = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingAround)
        {
            searchTimer += Time.deltaTime;
        }
        if(searchTimer > SearchTime)
        {
            searchTimer = 0;
            CurrNode = CurrNode.NextNode;
            lookingAround = false;
        }

        if (Vector3.Distance(CurrNode.location, transform.position) > NodeDetectionRange)
        {
            transform.LookAt(CurrNode.location);
            transform.position = transform.position + transform.forward * speed;
        }
        else
        {
            lookingAround = true;
        }
    }
}
