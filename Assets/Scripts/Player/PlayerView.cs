using Framework.AMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : View<GamePlayApplication>
{
    PlayerController playerControler;

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        playerControler = app.controller.GetComponentInChildren<PlayerController>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        playerControler.UpdateLoop(); 
        playerControler.UpdateAnimator(); 
    }

    private void FixedUpdate()
    {
        //playerControler.Move();
    }

}
