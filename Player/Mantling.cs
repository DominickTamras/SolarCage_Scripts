using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantling : MonoBehaviour
{
    [SerializeField] MantlingCollisionDetection obstruction;
    [SerializeField] MantlingCollisionDetection wall;
    [SerializeField] Transform wallClimbPosition;
    [SerializeField] float climbTime;

    private WallRunning wr;
    private Rigidbody rb;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool mantleNow = false;
    private float localClimbTime;
    private float t_mantle;

    bool canMantle = false;
    bool mountOnce = false;
    public bool isWallRunning;

    private void Start()
    {
        wr = GetComponent<WallRunning>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        getValues();
        mantleCheck();
        mantleController();
    }

    void mantleCheck()
    {
        if (wall.isColliding && !obstruction.isColliding && !isWallRunning && !canMantle && !mantleNow && Input.GetKey(KeyCode.W))
        {
            canMantle = true;
        }
    }

    void getValues()
    {
        if(canMantle)
        {
            canMantle = false;
            //rb.isKinematic = true;
            startPos = transform.position;
            endPos = wallClimbPosition.position;
            mantleNow = true;
            localClimbTime = climbTime;

            //vel reset
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }

    void mantleController()
    {
        if(mantleNow && t_mantle < 1f)
        {
            t_mantle += Time.deltaTime / localClimbTime;
            wr.isMantling = true;
            transform.position = Vector3.Lerp(startPos, endPos, t_mantle);

            if (mountOnce == false) // Plays sound once
            {
                FindObjectOfType<AudioManager>().PlaySound("Mount");

                mountOnce = true;

            }

            if (t_mantle >= 1f)
            {
                mantleNow = false;
                t_mantle = 0f;
                wr.isMantling = false;
                mountOnce = false;
                //rb.isKinematic = false;
            }
        }
    }

}
