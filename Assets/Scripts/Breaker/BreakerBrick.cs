using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BreakerBrick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBallHit(Collision thisBrick)
    {
        Destroy(this.gameObject);
    }
}
