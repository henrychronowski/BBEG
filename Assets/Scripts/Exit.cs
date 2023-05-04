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
    public Exit connectedExit;


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

    public static Vector2 DirectionToVector2(ExitDirection dir)
    {
        switch (dir)
        {
            case ExitDirection.North:
                {
                    return new Vector2(0, 1);
                }
            case ExitDirection.East:
                {
                    return new Vector2(1, 0);
                }
            case ExitDirection.South:
                {
                    return new Vector2(0, -1);
                }
            case ExitDirection.West:
                {
                    return new Vector2(-1, 0);
                }

            default:
                {
                    return new Vector2(0, 0);
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

    public static void ConnectExits(Exit a, Exit b)
    {
        a.connectedExit = b;
        b.connectedExit = a;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(PlayerCharacterManager.instance.party != PartyMovementState.Scripted 
                && IsPlayerMovingTowardsExit())
            {
                PlayerCharacterManager.instance.StartTransition(connectedExit.transform.position);
                CameraControl.Instance.ChangeFocus(connectedRoom.focus);
            }

            // Change the active room to the new one?
        }
    }

    bool IsPlayerMovingTowardsExit()
    {
        Vector2 playerAxis = PlayerCharacterManager.instance.leader.axis;

        if(playerAxis.x > 0 && direction == ExitDirection.East)
            return true;

        if (playerAxis.x < 0 && direction == ExitDirection.West)
            return true;

        if (playerAxis.y > 0 && direction == ExitDirection.North)
            return true;

        if (playerAxis.y < 0 && direction == ExitDirection.South)
            return true;

        return false;
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
        //Gizmos.DrawWireSphere(transform.position, loadingTriggerRadius);
    }
}
