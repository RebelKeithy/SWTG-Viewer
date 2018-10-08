using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatable : MonoBehaviour {

    public bool moving;
    public bool kill;
    public Vector3 target;
    public Vector3 velocity;
    public float maxSpeed = 1f;
    public float acceleration = 0.1f;
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = gameObject.transform.position;
		if(moving)
        {
            if(Vector3.Distance(pos, target) < velocity.magnitude || Vector3.Distance(pos, target) < 0.001f)
            {
                gameObject.transform.position = target;
                moving = false;
                if(kill)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if(velocity.magnitude < maxSpeed)
                {
                    velocity += acceleration * (target - pos);
                }
                gameObject.transform.position = pos + velocity;
            }
        }
	}

    public void setTarget(Vector3 target, bool kill = false)
    {
        this.target = target;
        this.velocity = Vector3.zero;
        this.kill = kill;
        moving = true;
    }
}
