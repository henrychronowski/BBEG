using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExitDirection
{
    North,
    East,
    South,
    West
}

public class Exit : MonoBehaviour
{

    public ExitDirection direction;
    [SerializeField] float loadingTriggerRadius = 5f;
    public RoomInfo connectedRoom;


    public static ExitDirection GetOpposingDirection(ExitDirection dir)
    {
        switch(dir)
        {
            case ExitDirection.North:
            {
                return ExitDirection.South;
            }
            case ExitDirection.East:
            {
                return ExitDirection.West;
            }
            case ExitDirection.South:
            {
                return ExitDirection.North;
            }
            case ExitDirection.West:
            {
                return ExitDirection.East;
            }

            default:
            {
                return ExitDirection.North;
            }
        }
    }

    public bool HasConnectingRoom()
    {
        return connectedRoom != null;
    }

    bool CheckForNearbyPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, loadingTriggerRadius, LayerMask.NameToLayer("Player"));
        return colliders.Length >= 1;
    }

    public void AddConnectingRoom(RoomInfo r)
    {
        connectedRoom = r;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Teleport to new room");
            // Change the active room to the new one
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Not really sure if this is necessary
        if(CheckForNearbyPlayer())
        {
            //Debug.Log("Player is close, enable room dependant characters/entities now");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, loadingTriggerRadius);
    }
}
