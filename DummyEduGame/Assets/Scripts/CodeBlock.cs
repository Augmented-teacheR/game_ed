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
    private Block randomBlock;

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