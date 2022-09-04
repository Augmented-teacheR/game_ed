using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    speed, distance, time
}
public class Block : MonoBehaviour
{
    [SerializeField]
    private BlockType type;
    [SerializeField]
    private float value;
}
