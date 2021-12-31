using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ledge_Checker : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPos, _StandPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeChecker"))
        {
            Player player = other.transform.parent.GetComponent<Player>();

            if(player != null)
            {
                player.GrabLedge(_handPos, this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        return _StandPos;
    }
}
