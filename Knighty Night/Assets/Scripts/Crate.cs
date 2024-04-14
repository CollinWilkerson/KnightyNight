using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    [SerializeField] float objectDurability = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //damages an object based on how fast it was hit
        objectDurability -= collision.relativeVelocity.magnitude;
        
        //Plays audio if a collision is fast enough
        if (collision.relativeVelocity.magnitude > 5f)
        {
            AudioClip clip = _clips[UnityEngine.Random.Range(0, _clips.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Too slow: " +  collision.relativeVelocity.magnitude);
        }

        //Destroys the Crate if its Durability runs out
        if (objectDurability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
