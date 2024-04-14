using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knights : MonoBehaviour
{
    [SerializeField] float _weight = 1f;
    //1 knight, 2 archer, 3 paladin
    [SerializeField] int _type = 1;

    private Crank _crank;
    private Catapult _spoon;

    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;

    //handles components attached to the gameobject
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();

        _rb.isKinematic = true;
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
        if (_crank.Fire)
        {
            Debug.Log("Knights Fire");
            _rb.isKinematic = false;
            _rb.AddForce(Vector2.up * 100);
        }
    }
}
