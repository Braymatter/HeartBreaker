using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BreakerBrick : MonoBehaviour
{
    public Vector2 FieldPosition;
    public BrickField parent;
    public void OnBallHit(Collision thisBrick)
    {
        if (parent != null)
        {
            parent.DeleteBrick(this);
        }
    }
}
