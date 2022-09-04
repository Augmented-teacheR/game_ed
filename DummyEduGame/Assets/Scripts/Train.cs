using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrainState
{
    running, finished
}
public class Train : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private TrainState state = TrainState.finished;

    private void Update()
    {
        if (state != TrainState.finished)
        {
            speed = 3;
            float x = gameObject.transform.position.x + speed * Time.deltaTime;
            float y = gameObject.transform.position.y;
            float z = gameObject.transform.position.z;

            gameObject.transform.position = new Vector3(x, y, z);
            if (x > 10) state = TrainState.finished;
        }
    }

    internal void Beggin()
    {
        state = TrainState.running;
    }
}
