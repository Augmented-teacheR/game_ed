using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Class to create a sphere for a block, that recognizes vote targets inside its own sphere
 */
public class FieldOfView : MonoBehaviour
{
    // Radius of the sphere
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    // Targetmask for the sphere
    public LayerMask targetMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    // Block for which a sphere is created
    private Block block;

    private void Awake()
    {
        block = GetComponent<Block>();
    }

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.1f);
    }

    /**
     * Coroutine to find targets inside the sphere
     */
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    /**
     * Create sphere and add targets that are inside of it.
     * Send the current amount of vote targets to the block
     */
    private void FindVisibleTargets()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        int voteAmount = targetsInViewRadius.Length;
        block.SetVoteAmount(voteAmount);
    }
}