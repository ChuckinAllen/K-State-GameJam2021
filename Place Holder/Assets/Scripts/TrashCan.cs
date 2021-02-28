using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    //[SerializeField] GameObject trash;

    [SerializeField] float time;
    [SerializeField] bool debug;

    private Dictionary<GameObject, float> trash;// = new Dictionary<GameObject, float>();
    // Start is called before the first frame update
    void Start()
    {
        trash = new Dictionary<GameObject, float>();
    }

    // Update is called once per frame
    void Update()
    {
        Dictionary<GameObject, float> temp = new Dictionary<GameObject, float>();
        foreach (GameObject t in trash.Keys)
        {
            if (trash[t] >= time)
            {

                Destroy(t);
            }
            else
            {
                //if (temp == null) temp = new Dictionary<GameObject, float>();
                temp.Add(t, trash[t] + 1);
            }
        }
        trash = temp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            trash.Add(other.gameObject, 0);

            if(debug) Debug.Log("added to trash");
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            trash.Remove(other.gameObject);
            if(debug) Debug.Log("removed from trash");

        }
    }
}