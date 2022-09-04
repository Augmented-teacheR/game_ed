using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    velocity, distance, time
}
public class Block : MonoBehaviour
{
    [SerializeField]
    private BlockType type;
    [SerializeField]
    private float value;

    //private CodeBlock codeBlock;

    // TODO REMOVE LATER - JUST FOR TESTING
    public string key;

    private void Awake()
    {
        //this.codeBlock = FindObjectOfType<CodeBlock>();
    }

    public BlockType GetBlockType()
    {
        return this.type;
    }

    public float GetValue()
    {
        return this.value;
    }

    private void Update()
    {
        if (Input.GetKey(key))
        {
            float speed = -15.0f;
            float newX = transform.position.x + speed * Time.deltaTime;
            float y = transform.position.y;
            float z = transform.position.z;

            transform.position = new Vector3(newX, y, z);
        }
    }

/*    private void OnTriggerEnter(Collider other)
    {
        codeBlock.AddBlock(this);
    }

    private void OnTriggerExit(Collider other)
    {
        codeBlock.RemoveBlock(this);
    }*/
}