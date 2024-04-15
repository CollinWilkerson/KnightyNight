using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    public bool Fire { get; private set; }

    [SerializeField] float _crankSpeed = 0.1f;
    [SerializeField] float _powerMultiplier = 100;

    private float power = 0f;
    private bool powerUp = true;
    private bool charging;
    private float spinAngle;

    // Start is called before the first frame update
    void Awake()
    {
        Fire = false;
        spinAngle = 0;
        ResetCrank();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Fire)
        {
            charging = true;
        }
        if (charging)
        {
            if (power < 1)
            {
                //Debug.Log("power up");
                powerUp = true;
            }
            else if (power > 20)
            {
                //Debug.Log("power Down");
                powerUp = false;
            }
            if (powerUp)
            {
                spinAngle += 100 * Time.deltaTime;
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, spinAngle));
                power += _crankSpeed * Time.deltaTime;
                //Debug.Log("Rotation: " + transform.rotation.z + 180);            
            }
            else
            {
                spinAngle -= 100 * Time.deltaTime;
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, spinAngle));
                power -= _crankSpeed * Time.deltaTime;
            }
            //Debug.Log("Power: " + power);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            charging = false;
            Debug.Log("Hinge Fire");
            Fire = true;
        }
    }

    public float GetLaunchPower()
    {
        return power * _powerMultiplier;
    }

    public void ResetCrank()
    {
        Fire = false;
        power = 0;
        spinAngle = 0;
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
    }
}
