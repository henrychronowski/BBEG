using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TargetComparer : IComparer<(Character, float)>
{
    public int Compare((Character, float) target1, (Character, float) target2)
    {
        return target1.Item2.CompareTo(target2.Item2);
    }
}

public class Enemy : Character
{
    public float minWanderTime;
    public float maxWanderTime;

    public float viewRange;
    public float attackingRange; // Find some way to calculate this based on the weapon?
    public List<(Character, float)> nearbyTargets;

    private void Start()
    {
        state = new IdleState(this);
        nearbyTargets = new List<(Character, float)>();
    }

    private void Update()
    {
        currentState = state.stateType;
        CheckForNearbyTargets();
        state.Update();

    }
    private void CheckForNearbyTargets()
    {
        nearbyTargets.Clear();
        // Get minions and leader in the list with their distance from this
        foreach(Character target in PlayerCharacterManager.instance.minions)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= viewRange)
            {
                nearbyTargets.Add((target, distance));
            }
        }
        
        float leaderDistance = Vector3.Distance(PlayerCharacterManager.instance.leader.transform.position, transform.position);
        if (leaderDistance <= viewRange)
        {
            nearbyTargets.Add((PlayerCharacterManager.instance.leader, leaderDistance));
        }
        
        nearbyTargets.Sort(new TargetComparer());
       
    }

    public Character GetClosestTarget()
    {
        if(nearbyTargets.Count == 0) return null;

        return nearbyTargets[0].Item1;
    }

    // Draw view range as gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
}
