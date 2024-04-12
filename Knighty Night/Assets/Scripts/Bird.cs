using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    public bool IsDragging { get; private set; }

    //good for getting components attached to an object
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //stores the original position of the bird for launch
        _startPosition = _rigidbody2D.position;
        //freezes the bird before launch
        _rigidbody2D.isKinematic = true;
    }

    private void OnMouseDown()
    {
        //recolors our bird sprite to indicate click
        _spriteRenderer.color = Color.red;
        IsDragging = true;
    }
    
    private void OnMouseUp()
    {
        //gets the released position of the bird
        Vector2 currentPosition = _rigidbody2D.position;
        //gets the direction from currentPosition to _startPosition
        Vector2 direction = (_startPosition - currentPosition).normalized;

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);

        _audioSource.Play();

        //returns sprite to original color
        _spriteRenderer.color = Color.white;

        IsDragging = false;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);

        if(distance > _maxDragDistance)
        {
            //gets the direction the player is dragging in
            Vector2 direction = (desiredPosition - _startPosition).normalized;
            //Resets the position in the bounds of our draging circle
            desiredPosition = _startPosition + (direction * _maxDragDistance);

            Debug.Log("out of bounds");
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        transform.position = desiredPosition;
    }
    // Update is called once per frame
    void Update()
    {
        
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
        _rigidbody2D.position = _startPosition;
        //freezes the bird before launch
        _rigidbody2D.isKinematic = true;
        //stops movement
        _rigidbody2D.velocity = Vector2.zero;
    }
}
