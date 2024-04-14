using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knights : MonoBehaviour
{
    [SerializeField] float _mass = 1f;
    //1 knight, 2 archer, 3 paladin
    [SerializeField] int _type = 1;

    private Crank _crank;
    private Catapult _spoon;

    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;

    private bool launched = false;
    //handles components attached to the gameobject
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();

        _rb.isKinematic = true;
        _rb.mass = _mass;
    }

    // Start is called before the first frame update
    void Start()
    {
        _crank = GameObject.FindFirstObjectByType<Crank>();
        _spoon = GameObject.FindFirstObjectByType<Catapult>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_crank.Fire && !launched)
        {
            launched = true;
            Debug.Log("Knights Fire");
            _rb.isKinematic = false;
            _rb.AddForce(_spoon.GetLaunchVector() * _crank.GetLaunchPower());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //starts co-routine
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);

        //Resets to the original position of the bird for launch
        _rb.position = _spoon.transform.position;
        //freezes the knight before launch
        _rb.isKinematic = true;
        //stops movement
        _rb.velocity = Vector2.zero;
        launched = false;

        //sets crank to starting values
        _crank.ResetCrank();

        //sets the spoon to starting values
        _spoon.ResetSpoon();
    }
}
