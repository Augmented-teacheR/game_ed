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
    [SerializeField]
    private int voteAmount;


    public void SetVoteAmount(int voteAmount)
    {
        this.voteAmount = voteAmount;
    }
    public BlockType GetBlockType()
    {
        return this.type;
    }

    public float GetValue()
    {
        return this.value;
    }

    public int GetVoteAmount()
    {
        return this.voteAmount;
    }
}