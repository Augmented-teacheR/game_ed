using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * Class to store blocks that can be voted for by the players.
 * The block with the most votes will be activated and its values will be read and saved here.
 * The GameMananger can use these values to configurate the level.
 * </summary>
 */
public class CodeBlock : MonoBehaviour
{
    // All blocks that can be voted
    public List<Block> blocks;

    [SerializeField]
    private float readDistance;
    [SerializeField]
    private float readTime;
    [SerializeField]
    private float readVelocity;

    // If multiple blocks have the same amount of votes, one block will be chosen randomly
    [SerializeField]
    private Block randomBlock;

    /**
     * <summary>
     * Determine which block should be activated and read its values
     * </summary>
     */
    public void ReadValues()
    {
        var maxValue = FindMaxVoteAmount();

        for(int i = 0; i < blocks.Count; i++) {
            Block block = blocks[i];
            int voteAmount = block.GetVoteAmount();
            if(voteAmount < maxValue) blocks.Remove(block);
        }

        if(blocks.Count > 1) randomBlock = blocks[Random.Range(0, blocks.Count-1)];
        
        else if(blocks.Count == 0) Debug.LogError("No Blocks with Maximum Value Found");
        
        else randomBlock = blocks[0];
        
        GetValue(randomBlock);
    }

    /**
     * Read the value of a block
     */
    private void GetValue(Block block)
    {
        switch (block.GetBlockType())
        {
            case BlockType.distance:
                this.readDistance = block.GetValue();
                break;
            case BlockType.time:
                this.readTime = block.GetValue();
                break;
            case BlockType.velocity:
                this.readVelocity = block.GetValue();
                break;
        }
    }

    /**
     * Find the maximum amount of votes for all blocks
     */
    private float FindMaxVoteAmount()
    {
        if (blocks.Count == 0) Debug.LogError("Empty List of Blocks");

        List<float> values = new List<float>();

        for (int i = 0; i < blocks.Count; i++)
        {
            values.Add(blocks[i].GetVoteAmount());
        }

        float maxValue = Mathf.Max(values.ToArray());

        return maxValue;
    }

    public float GetReadDistance()
    {
        return this.readDistance;
    }

    public float GetReadTime()
    {
        return this.readTime;
    }

    public float GetReadVelocity()
    {
        return this.readVelocity;
    }
}