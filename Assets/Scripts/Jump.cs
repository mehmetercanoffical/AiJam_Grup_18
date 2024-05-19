using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public void JumpByThickes()
    {
        playerMovement.Jump();
    }
}
