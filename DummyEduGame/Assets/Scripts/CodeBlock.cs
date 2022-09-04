using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlock : MonoBehaviour
{
    public List<Block> blocks;
    [SerializeField]
    private float readDistance;
    [SerializeField]
    private float readTime;
    [SerializeField]
    private float readVelocity;
    [SerializeField]
    private Block activeBlock;

    public void ReadValues()
    {
        var maxValue = FindMaxVoteAmount(blocks);

        foreach (Block block in blocks) 
        {
            int voteAmount = block.GetVoteAmount();
            if(voteAmount < maxValue) { blocks.Remove(block); }
        }

        if(blocks.Count > 1)
        {
            activeBlock = blocks[Random.Range(0, blocks.Count)];
            Debug.Log("ACTIVE BLOCK " + activeBlock.GetValue());
        }
        else if(blocks.Count == 0)
        {
            Debug.LogError("No Blocks Found");
        }
        else
        {
            activeBlock = blocks[0];
        }
        GetValue(activeBlock);
    }

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

    private float FindMaxVoteAmount(List<Block> blockList)
    {
        List<float> values = new List<float>();

        foreach (Block block in blocks)
        {
            values.Add(block.GetVoteAmount());
        }

        float maxValue = Mathf.Max(values.ToArray());

        return maxValue;
    }

    public float GetDistance()
    {
        return this.readDistance;
    }

    public float GetTime()
    {
        return this.readTime;
    }

    public float GetVelocity()
    {
        return this.readVelocity;
    }
}