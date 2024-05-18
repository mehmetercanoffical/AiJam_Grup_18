using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{

    public float speed = 5f;
    public float jumpForce = 10f;
    public float gravity = 9.8f;
    public CharacterController controller;
    public Vector3 moveDirection;
    public bool isActive = false;


    void Start()
    {
        
    }

    void Update()
    {
        if (!isActive) return;
    }
}
