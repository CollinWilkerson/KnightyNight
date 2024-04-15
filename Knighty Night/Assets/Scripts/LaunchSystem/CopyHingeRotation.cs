using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyHingeRotation : MonoBehaviour
{
    [SerializeField] Crank hinge;

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, hinge.GetRotation()));
    }
}
