using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * Class to define a block. Each block has its own type and a specific value for that type.
 * The amount of votes one block received is also stored here.
 * </summary>
 */
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