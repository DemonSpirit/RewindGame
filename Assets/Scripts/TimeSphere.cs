using UnityEngine;
using System.Collections;

public class TimeSphere : Pickup
{
    [SerializeField] int timePickupAmount = 2;

    public override void PickupAction(Collider other)
    {
        PlayerController playerCtrl = other.GetComponent<PlayerController>();
        if (playerCtrl.team == 1) gameCtrl.timeLimitTeam1 += timePickupAmount;
        if (playerCtrl.team == 2) gameCtrl.timeLimitTeam2 += timePickupAmount;


    }
   
}
