using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMovementController : MonoBehaviour
{
    public float temp;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;

    public Rigidbody rb;
    
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    public bool isGrounded, canJump;
    [SerializeField] private float airMultiplier;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float groundDrag, airDrag, jumpCooldown;
    [SerializeField] private Camera _camera;
    private Vector3 forward;
    private Vector3 right;
    
    public Vector2 moveVector;

    private Transform originalParent;
    public Vector3 testVector;
    public bool testState;
    #region getInput

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    #endregion

    private void Start()
    {
        originalParent = transform.parent;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        SpeedControl();
        
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, temp, groundLayer);
        CheckGrounding(isGrounded);
        //isGrounded= Physics.Raycast()
        Debug.DrawRay(transform.position, Vector3.down * temp, Color.red);
        if (isGrounded && Input.GetButton("Jump") && canJump)
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(jump_reset), jumpCooldown);
        }
        Jump();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
        
    }
    
    private void PlayerMovement()
    {
        Vector3 movement = new Vector3(GetCameraDirection().x , 0, GetCameraDirection().z);
        testVector = movement;
        if (isGrounded)
        {
            rb.AddForce(movement.normalized*playerSpeed*10f,ForceMode.Force);
        }
        AirControl(movement);
        //handle Rotation
        if(movement.magnitude==0){return;}
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    }
    Vector3 GetCameraDirection()
    {
        forward = _camera.transform.forward;
        right = _camera.transform.right;
        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        
        Vector3 forwardDirection = moveVector.y *forward;
        Vector3 rightDirection = moveVector.x *right;
        Vector3 relativeDir = forwardDirection + rightDirection;
        
        return relativeDir;
    }
    
    void Jump()
    {
        if (rb.velocity.y < 0 && !isGrounded)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y>0 && !Input.GetButton("Jump") && !isGrounded)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void jump_reset()
    {
        canJump = true;
    }
    void SpeedControl()
    {
        //get flat velocity using only x and z axis.
        //limit this flat velocity if it exceeds a speed greater than what is assigned to the player
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVelocity = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void AirControl(Vector3 movementVector)
    {
        if (!isGrounded)
        {
            rb.AddForce(movementVector.normalized *playerSpeed*10f*airMultiplier,ForceMode.Force);
        }
    }

    public void SetParent(Transform newParent)
    {
        originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        transform.parent = originalParent;
    }

    bool CheckGrounding(bool TestBool)
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, temp, groundLayer);
        return isGrounded;
    }
}
