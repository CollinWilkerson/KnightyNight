using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knights : MonoBehaviour
{
    [SerializeField] float _mass = 1f;
    //1 knight, 2 archer, 3 paladin
    [SerializeField] int _type = 1;
    [SerializeField] GameObject arrow;
    [SerializeField] Sprite special;
    [SerializeField] Sprite queue;
    [SerializeField] Sprite launch;
    [SerializeField] Sprite splat;

    [SerializeField] float specialPower = 100;

    private Crank _crank;
    private Catapult _spoon;

    private Rigidbody2D _rb;
    private Rigidbody2D _arrowRb;
    private SpriteRenderer _sprite;

    private bool launched = false;
    private bool specialUsed = false;
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
        _arrowRb = arrow.GetComponent<Rigidbody2D>();
        _crank = GameObject.FindFirstObjectByType<Crank>();
        _spoon = GameObject.FindFirstObjectByType<Catapult>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vector2This = transform.position;
        if (_crank.Fire && !launched)
        {
            launched = true;
            Debug.Log("Knights Fire");
            _rb.freezeRotation = false;
            _rb.isKinematic = false;
            _rb.AddForce(_spoon.GetLaunchVector() * _crank.GetLaunchPower());
        }
        if (Input.GetMouseButtonDown(0) && launched && !specialUsed)
        {
            if(_type == 2)
            {
                Time.timeScale = 0.1f;
                _sprite.sprite = special;
                arrow.SetActive(true);
                arrow.transform.position = transform.position;
                _arrowRb.isKinematic = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && launched && !specialUsed)
        {
            if (_type == 2)
            {
                Time.timeScale = 1f;
                _sprite.sprite = launch;
                specialUsed = true;
                Vector2 direction = (mousePosition - vector2This).normalized;
                _arrowRb.isKinematic = false;
                _arrowRb.AddForce(direction * specialPower);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = splat;
        //starts co-routine
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);

        //Resets to the original position of the bird for launch
        Vector3 offset = new Vector2(0.1f, 0.2f);
        _rb.position = _spoon.transform.position + offset;
        //freezes the knight before launch
        _rb.isKinematic = true;
        //stops movement
        _rb.velocity = Vector2.zero;
        launched = false;

        //sets crank to starting values
        _crank.ResetCrank();

        //sets the spoon to starting values
        _spoon.ResetSpoon();

        specialUsed = false;

        _rb.SetRotation(0);
        _rb.freezeRotation = true;

        GetComponent<SpriteRenderer>().sprite = launch;
    }
}
