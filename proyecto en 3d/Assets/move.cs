﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float rotX;
    public float rotY;
    public float rotZ;
    public int saltos;
    public Rigidbody rb;
    public GameObject camera;
    public Collider coll;  
    void Awake()
    {
        coll = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        saltos=4;
    }

    // Update is called once per frame
    void Update()
    {
        //moverse
        transform.Translate(moveSpeed*Input.GetAxis("Horizontal"),0f,moveSpeed*Input.GetAxis("Vertical"));
        rotX -= Input.GetAxis("Mouse Y")*Time.deltaTime*rotationSpeed;
        rotY += Input.GetAxis("Mouse X")*Time.deltaTime*rotationSpeed;
        //tener un limite al mirar arriba o abajo
        if (rotX< -90)
        {
            rotX=-90;
        }else if (rotX>90){
            rotX=90;
        }
        //que rote segun se mueva el mouse
        transform.rotation= Quaternion.Euler(0,rotY,0);
        //mover camara donde el mouse
        GameObject.FindGameObjectsWithTag("MainCamera")[0].transform.rotation=Quaternion.Euler(rotX,rotY,0);
        //saltar 
        if (Input.GetButtonDown("Jump"))
        {   if (Grounded())
            {   saltos=1;
                rb.velocity = new Vector3(0,5,0);
                saltos-=1;
            }else if(saltos>0){
                rb.velocity = new Vector3(0,5,0);
                saltos-=1;
            }
        }
    }

    void FixedUpdate(){

    }
    bool Grounded(){
        //pregunta si hay algo debajo del personaje
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
    }
}
