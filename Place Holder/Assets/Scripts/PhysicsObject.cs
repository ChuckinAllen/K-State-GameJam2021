using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float waitOnPickup = 0.2f;
    public float breakForce = 35f;
    [HideInInspector] public bool pickedUp = false;
    [HideInInspector] public PlayerInteractions playerInteractions;
    [SerializeField] private AudioClip[] CanSounds;

    private AudioSource can;

    private void Start()
    {
        can = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if(collision.relativeVelocity.magnitude > breakForce)
            {
                playerInteractions.BreakConnection();
            }
        }
        PlayCan();
    }

    public IEnumerator PickUp()
    {
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;
    }

    private void PlayCan()
    {
        int n = Random.Range(0, CanSounds.Length);
        can.clip = CanSounds[n];
        can.PlayOneShot(can.clip);
    }
}
