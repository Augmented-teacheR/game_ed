using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MovementType
{
    time, velocity, finished
}

public class Car : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 position;

    private float distance;
    private float time;
    private float velocity;
    private MovementType state = MovementType.finished;


    public void Go(float distance, float time, float velocity)
    {
        if (distance == 0) 
            this.distance = 20;
        if (time == 0) 
            this.time = 10;
        if (velocity == 0) 
            this.velocity = 10;
        this.speed = velocity;
        state = MovementType.velocity;
    }

    private void Update()
    {
        if(state != MovementType.finished)
        {
            float x = gameObject.transform.position.x;
            float y = gameObject.transform.position.y;
            float z = gameObject.transform.position.z + speed*Time.deltaTime;

            gameObject.transform.position = new Vector3(x,y,z);
            if (z > -5) state = MovementType.finished;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Entered");
        state = MovementType.finished;
    }
}
