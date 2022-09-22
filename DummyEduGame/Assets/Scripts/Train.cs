using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrainState
{
    running, finished
}
/**
 * Contains the behaviour of the train.
 * Can start and stop the train´s movement.
 * **/
public class Train : MonoBehaviour
{

    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private TrainState state = TrainState.finished;

    private void Update()
    {
        if (state != TrainState.finished)
        {
            float x = transform.localPosition.x + speed * Time.deltaTime;
            float y = transform.localPosition.y;
            float z = transform.localPosition.z;

            transform.localPosition = new Vector3(x, y, z);
            if (x > 10) state = TrainState.finished;
        }
    }

    internal void Begin()
    {
        state = TrainState.running;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Car")){
            Debug.Log("Collision Entered");
            state = TrainState.finished;
        }
    }
}
