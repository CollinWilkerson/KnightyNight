using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > 5f)
        {
            AudioClip clip = _clips[UnityEngine.Random.Range(0, _clips.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Too slow: " +  collision.relativeVelocity.magnitude);
        }
    }
}
