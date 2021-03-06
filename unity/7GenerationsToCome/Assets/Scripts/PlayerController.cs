﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Cinemachine;
using UnityEditor.Animations;
using UnityEngine.AI;
using UnityEngineInternal.Input;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    CharacterController _controller;
    public float speed = 4;
    public float rotationSpeed = 4;
    public Animator anim;
    private NavMeshAgent _agent;
    private Rigidbody rb;
    private bool swim;
    
    
    
    private float thrust = 1000.0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();
        
    }

    private Vector3 move = Vector3.zero;
    
    void Update()
    {
        var hMove = Input.GetAxis("Horizontal");
        var rotation = rotationSpeed * hMove;
        var rotVector = new Vector3(0, rotation, 0);
        transform.Rotate(rotVector);

       // var forward = transform.TransformDirection(Vector3.forward);
        var vMove = Input.GetAxis("Vertical");
        vMove = Mathf.Clamp(vMove, 0, 1);
      //  var currentSpeed = speed * vMove;
      //  var move = currentSpeed * forward;
        //_controller.SimpleMove(move);
        move.z=speed * vMove;

        if (Input.GetKeyDown(KeyCode.K))
       //     if (_controller.isGrounded)
                move.y += 10;
        
        //if (!_controller.isGrounded)
         //   move.y -= 5 * Time.deltaTime;
        //else
          //  move.y = 0;
        
        //Debug.Log(_controller.isGrounded);
        
        transform.Rotate(0,rotation*Time.deltaTime,0);
        
        transform.Translate(move*Time.deltaTime);
        
        
        
        anim.SetFloat("Walk", vMove);
    }

    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.name);
        switch (coll.gameObject.name) { 
            case "TriggerGeyzer" :
                rb.AddForce(transform.up * thrust);
                break;

            
            case "TriggerWater" :
                swim = true;
                anim.SetBool("Swim", true);
                break;
            
            case "TriggerBeach":
                swim = false;
                break;
                
        }
    }

    
}
