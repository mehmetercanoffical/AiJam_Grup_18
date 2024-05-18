using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    
    void Start()
    {
        
    }


    public void OnPlayerActive(bool isActive)
    {
        PlayerMove.Instance.isActive = isActive;
    }
}
