using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BreakerPaddle : MonoBehaviour
{
    public Vector2 DirectionModifier;

    private Vector3 _lastPosition;
    // Update is called once per frame
    void Start()
    {
        _lastPosition = transform.position;
    }
    void Update()
    {
        if (transform.position != _lastPosition)
        {
            if (this._lastPosition.x > transform.position.x)
            {
                this.DirectionModifier = new Vector2(-0.4F, 0F);
            }else if (_lastPosition.x < transform.position.x)
            {
                this.DirectionModifier = new Vector2(0.4F, 0F);
            }
            else
            {
                this.DirectionModifier = Vector2.zero;
            }
            
            _lastPosition = this.transform.position;
        }
    }
}
