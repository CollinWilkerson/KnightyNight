using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    public bool Fire { get; private set; }

    [SerializeField] float _crankSpeed;

    private float power = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Fire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(power > 0.1)
            {
                power += 0.1f;
            }
        }
    }
}
