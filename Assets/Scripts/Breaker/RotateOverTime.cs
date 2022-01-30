using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 EulerRotation = Vector3.zero;
    public float rotationSpeed = 5;
    public bool randomDirection = true;
    public bool randomSpeed = true;
    void Start()
    {
        if (randomDirection)
        {
            EulerRotation = new Vector3(Random.Range(40, 360), Random.Range(40, 360), Random.Range(40, 360)).normalized;
        }

        if (randomSpeed)
        {
            rotationSpeed = Random.Range(60, 360);
        }
    }

    public void Randomize()
    {
        rotationSpeed = Random.Range(15, 30);
        EulerRotation = new Vector3(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180)).normalized;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(EulerRotation*rotationSpeed*Time.deltaTime);
    }
}
