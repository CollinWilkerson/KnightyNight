using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] Sprite _damagedSprite;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] float _enemyHealth = 10;

    private float _enemyMaxHealth;

    bool _hasDied;

    private void Awake()
    {
        _enemyMaxHealth = _enemyHealth;
    }
    //plays audio after a random amount of time
    private IEnumerator Start()
    {
        while (_hasDied == false)
        {
            float delay = UnityEngine.Random.Range(5, 30);
            yield return new WaitForSeconds(delay);
            if (!_hasDied)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
    //plays audio on click
    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
    }

    //begins collision detections
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    //Kills the enemy if it recives enough damage
    private bool ShouldDieFromCollision(Collision2D collision)
    {
        //killing the enemy after they are dead delays their death and causes visual glitches
        if (_hasDied)
            return false;

        //enemy takes full damage if they are hit by the player, and half if they are hit by anything else
        if (collision.gameObject.CompareTag("Player"))
            _enemyHealth -= collision.relativeVelocity.magnitude;
        else
            _enemyHealth -= collision.relativeVelocity.magnitude / 2;

        //if the enemy has no health kill them
        if (_enemyHealth <= 0)
            return true;
        else if(_enemyHealth < _enemyMaxHealth / 2)
        {
            GetComponent<SpriteRenderer>().sprite = _damagedSprite;
            return false;
        }
        else
            return false;
    }

    private IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
