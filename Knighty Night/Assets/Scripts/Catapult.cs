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

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _hingePosition = _hinge.transform.position;
        _distanceFromHinge = Vector2.Distance(transform.position, _hingePosition);
        
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

        if (distance != _distanceFromHinge)
        {
            //gets the direction the player is dragging in
            Vector2 direction = (mousePosition - _hingePosition).normalized;
            //Resets the position in the bounds of our draging circle
            mousePosition = _hingePosition + (direction * _distanceFromHinge);
        }

        transform.position = mousePosition;
    }
}
