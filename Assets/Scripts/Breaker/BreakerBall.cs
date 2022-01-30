using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakerBall : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    public Vector3 direction;
    private Rigidbody _rigidbody;
    private Vector3 _initialPosition;
    private bool _isColliding;
    private void OnCollisionEnter(Collision other)
    {
        if (_isColliding) return;
        _isColliding = true;

        GetComponentInChildren<RotateOverTime>().Randomize();
        //Redirect Velocity
        direction = Vector3.Reflect(direction, other.contacts[0].normal);
        
        //Handle Brick Collision
        var collidedBrick = other.gameObject.GetComponent<BreakerBrick>();
        if (collidedBrick != null)
        {
            collidedBrick.OnBallHit(other);
        }

        var breakerGate = other.gameObject.GetComponent<BreakerGate>();
        if (breakerGate != null)
        {
            breakerGate.OnBallHit();
            ResetBall();
            GetComponentInParent<BreakerController>().OnGateHit();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        var escapeMarker = other.gameObject.GetComponent<EscapeColliderMarker>();
        if (escapeMarker != null)
        {
            Debug.Log("Ball Escaped");
            ResetBall();
        }
    }

    public void ResetBall()
    {
        this.direction = new Vector3(1, 1, 0);
        this.transform.position = _initialPosition;
    }

    public void WinState()
    {
        this.direction = Vector3.zero;
        this.transform.position = _initialPosition;
    }
    
    void Start()
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this.direction= new Vector3(1, 1, 0).normalized;
        this._initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _isColliding = false;
        var translationVector = new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
        transform.Translate(translationVector);
    }

    private void FixedUpdate()
    {

        //_rigidbody.MovePosition(translationVector);
    }
}
