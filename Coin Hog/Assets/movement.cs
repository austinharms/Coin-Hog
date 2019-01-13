using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    public CharacterController2D controler;
    public Animator animator;
    float xMove = 0f;
    bool jump = false;
    bool down = false;
    public float moveSpeed = 40f;
    private float timeOffset = 0.0f;

    void Start()
    {
        timeOffset = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - timeOffset) > 3)
        {
            xMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            animator.SetFloat("speed", Mathf.Abs(xMove));
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJump", true);
            }
            if (Input.GetButtonDown("Crouch"))
            {
                down = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                down = false;
            }
        }
    }

    public void crouhing(bool isCrouching)
    {

        animator.SetBool("crouched", isCrouching);

    }

    public void landing()
    {

        animator.SetBool("isJump", false);
    }

    void FixedUpdate()
    {
        down = false;
        controler.Move(xMove * Time.fixedDeltaTime, down, jump);
        jump = false;

    }
}
