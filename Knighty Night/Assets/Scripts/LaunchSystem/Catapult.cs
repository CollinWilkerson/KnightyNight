using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public bool _PositionSet = false;

    [SerializeField] GameObject _hinge;

    private Vector2 _hingePosition;
    private float _distanceFromHinge;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerLocation;

    private Vector3 _startPosition;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _startPosition = transform.position;
    }

    private void Start()
    {
        _hingePosition = _hinge.transform.position;
        _distanceFromHinge = Vector2.Distance(transform.position, _hingePosition);
        _playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (!_PositionSet)
        {
            Debug.Log("CLICKED");
            _spriteRenderer.color = Color.red;
        }
    }

    private void OnMouseDrag()
    {
        if (_PositionSet)
            return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, _hinge.transform.position);

        if (mousePosition.x > _hingePosition.x)
            mousePosition.x = _hingePosition.x;
        if (mousePosition.y < _hingePosition.y)
            mousePosition.y = _hingePosition.y;

        if (distance != _distanceFromHinge)
        {
            //gets the direction the player is dragging in
            Vector2 direction = (mousePosition - _hingePosition).normalized;
            //Resets the position in the bounds of our draging circle
            mousePosition = _hingePosition + (direction * _distanceFromHinge);
        }

        Vector2 ThreeToTwo = transform.position;
        Vector2 hingeToSpoon = (ThreeToTwo - _hingePosition);
        transform.position = mousePosition;
        transform.rotation = Quaternion.Euler(0,0,-Mathf.Asin(hingeToSpoon.y/hingeToSpoon.magnitude) * Mathf.Rad2Deg);
        Vector2 offset = new Vector2(0.1f, 0.2f);
        _playerLocation.position = mousePosition + offset;
    }
    private void OnMouseUp()
    {
        _spriteRenderer.color = Color.white;
        _PositionSet = true;
    }

    public Vector2 GetLaunchVector()
    {
        Vector2 spoonDirection = (new Vector2(transform.position.x,transform.position.y) - _hingePosition).normalized;
        return new Vector2(spoonDirection.y, -spoonDirection.x);
    }

    public void ResetSpoon()
    {
        transform.SetPositionAndRotation(_startPosition,transform.rotation);
        Vector2 ThreeToTwo = transform.position;
        Vector2 hingeToSpoon = (ThreeToTwo - _hingePosition);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Asin(hingeToSpoon.y / hingeToSpoon.magnitude) * Mathf.Rad2Deg);
        _PositionSet = false;
    }
}
