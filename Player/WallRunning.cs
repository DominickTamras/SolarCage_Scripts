using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerMovement pm;
    private Mantling m;
    private Shooting s;

    public bool isMantling;

    [Header("Movement")]
    [SerializeField] Transform orientation;

    [Header("Detection")]
    [SerializeField] float wallDistance = 0.5f;
    [SerializeField] float minJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] float wallRunGravity = 0f;
    [SerializeField] float wallRunJumpForce;
    [SerializeField] LayerMask wallRunable;
    bool velReset = true;

    [Header("Camera")]
    [SerializeField] private List<Camera> camList = new List<Camera>();
    [SerializeField] private float fov;
    [SerializeField] private float wallRunFov;
    [SerializeField] private float wallRunFovTime;
    [SerializeField] private float cameraTilt;
    [SerializeField] private float cameraTiltTime;

    public float tilt { get; private set; }

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    GameObject previousWall;
    GameObject currentWall;

    public bool canWallRun;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        m = GetComponent<Mantling>();
        s = GetComponent<Shooting>();
    }

    private void Update()
    {
        if(pm.isGrounded)
        {
            previousWall = null;
        }

        CheckWall();

        if(CanWallRun())
        {
            if(wallLeft && !isMantling && (currentWall != previousWall))
            {
                StartWallRun();
            }
            else if (wallRight && !isMantling && (currentWall != previousWall))
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    bool CanWallRun()
    {
        if (!s.reverseGravity)
        {
            canWallRun = !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
            return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
        }
        else if(s.reverseGravity)
        {
            canWallRun = !Physics.Raycast(transform.position, Vector3.up, minJumpHeight);
            return !Physics.Raycast(transform.position, Vector3.up, minJumpHeight);
        }
        return false;
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance, wallRunable);
        if(wallLeft)
        {
            pm.wallHit = leftWallHit;
            currentWall = leftWallHit.collider.gameObject;
        }
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance, wallRunable);
        if (wallRight)
        {
            pm.wallHit = rightWallHit;
            currentWall = rightWallHit.collider.gameObject;
        }
    }

    void StartWallRun()
    {
        //vel reset
        if(!s.reverseGravity)
        {
            if (rb.velocity.y < 0 && velReset == true)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                velReset = false;
            }
        }
        else if(s.reverseGravity)
        {
            if (rb.velocity.y > 0 && velReset == true)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                velReset = false;
            }
        }


        //Wall run gravity
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        //PlayerMovement interactions
        pm.useGravity = false;
        pm.isWallRunning = true;
        m.isWallRunning = true;

        //Camera fov
        foreach(Camera cam in camList)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, wallRunFovTime * Time.deltaTime);
        }

        //Camera tilt
        if (wallLeft)
            tilt = Mathf.Lerp(tilt, -cameraTilt, cameraTiltTime * Time.deltaTime);
        else if (wallRight)
            tilt = Mathf.Lerp(tilt, cameraTilt, cameraTiltTime * Time.deltaTime);

        //Leave Wall
        if (wallLeft)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //store previous wall
                previousWall = leftWallHit.collider.gameObject;
                FindObjectOfType<AudioManager>().PlaySound("Jump");

                //jump off wall
                Vector3 wallrunJumpDirection = transform.up + leftWallHit.normal;
                rb.AddForce(wallrunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                //store previous wall
                previousWall = leftWallHit.collider.gameObject;

                rb.AddForce(leftWallHit.normal * 5, ForceMode.Impulse);
            }
        }
        else if (wallRight)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //store previous wall
                previousWall = rightWallHit.collider.gameObject;
                FindObjectOfType<AudioManager>().PlaySound("Jump");

                //jump off wall
                Vector3 wallrunJumpDirection = transform.up + rightWallHit.normal;
                rb.AddForce(wallrunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //store previous wall
                previousWall = rightWallHit.collider.gameObject;

                rb.AddForce(rightWallHit.normal * 5, ForceMode.Impulse);
            }
        }
    }

    void StopWallRun()
    {
        velReset = true;
        pm.useGravity = true;
        pm.isWallRunning = false;
        m.isWallRunning = false;
        foreach (Camera cam in camList)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFovTime * Time.deltaTime);
        }
        tilt = Mathf.Lerp(tilt, 0, cameraTiltTime * Time.deltaTime);
    }
}
