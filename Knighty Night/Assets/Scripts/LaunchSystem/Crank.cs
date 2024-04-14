using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    public bool Fire { get; private set; }

    [SerializeField] float _crankSpeed;

    private float power = 0f;
    private bool powerUp = true;

    // Start is called before the first frame update
    void Awake()
    {
        Fire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !Fire)
        {
            if(power < 0.1f)
                powerUp = true;
            else if(power > 10)
                powerUp = false;
            if (powerUp)
                power += 0.1f * Time.deltaTime;
            else
                power -= 0.1f * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Hinge Fire");
            Fire = true;
        }
    }
}
