using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Sound.CanController
{
    [RequireComponent(typeof(AudioSource))]

    public class CanSound : MonoBehaviour
    {



        private AudioSource can;

        public AudioClip[] list;



        // Start is called before the first frame update
        void Start()
        {
            can = GetComponent<AudioSource>();


        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayCan();
        }

        public void PlayCan()
        {
            int n = Random.Range(0, list.Length);
            can.clip = list[n];
            can.PlayOneShot(can.clip);
            


        }

        

    }
}