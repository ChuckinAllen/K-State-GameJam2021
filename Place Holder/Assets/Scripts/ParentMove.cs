using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    [SerializeField] GameObject text;
    [SerializeField] float timer = 5;

    // Start is called before the first frame update
    void Start()
    {
        lookingAround = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Body")
        {
            text.SetActive(true);

            StartCoroutine(ReloadLevels());
        }
    }
    private IEnumerator ReloadLevels()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(0);
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
