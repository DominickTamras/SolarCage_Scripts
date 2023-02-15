using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyTracking : MonoBehaviour
{

    public Animator shootAnim;

    [Header("Settings")]
    public float maxSpeed;

    public float minSpeed;

    public float finalTime;

    public float thresholdCompare;

    public float speedThreshold;

    public float aggroThreshold;

    public Transform target; 

    [HideInInspector]
    public bool isIn;

    public float rotateSpeed;

    public float predictBuffer;

    [HideInInspector]
    public bool isOut;

    public ShootTip startShoot;

    private float dist;

    private float enemyPos;

    private Rigidbody rigid;

    private EnemyShoot shootSpeed;

    private PlayerMovement instance;

    private PlayerMovement currentSpeedz;

    private Vector3 predictPosition;

    private Quaternion ogRotation;
    
    RaycastHit checkHit;




    void Awake()
    {   
        startShoot.enabled = false;
        // isIn = true;
        rigid = target.GetComponent<Rigidbody>();
        ogRotation = transform.rotation;
        instance = FindObjectOfType<PlayerMovement>();
        currentSpeedz = FindObjectOfType<PlayerMovement>();
        shootSpeed = FindObjectOfType<EnemyShoot>();


    }

   
    void Update() // Throw this into a coroutine at some point?
    {
      

     // enemyPos = transform.position.x;
      
      dist = Vector3.Distance(target.position, transform.position);

      float seperation = Vector3.Distance(this.transform.position, target.transform.position);


        if (target.gameObject.activeSelf != false)
        {
            if (seperation <= aggroThreshold)
            {
                shootAnim.SetFloat("ShootSpeed", startShoot.timeBetweenShots);
                shootAnim.SetBool("Shooting", true);

                startShoot.enabled = true;

                if (currentSpeedz.currentSpeed < speedThreshold && dist > thresholdCompare)
                {
                    LookAt();
                }

                else
                {
                    StartCoroutine(LookAtCor());
                    StartCoroutine(ReturnCor());
                }

            }

            if (seperation >= aggroThreshold)
            {
                shootAnim.SetBool("Shooting", false);
                startShoot.enabled = false;
                ReturnPos();
            }
        }

        else 
        {
            shootAnim.SetBool("Shooting", false);
            startShoot.enabled = false;
            ReturnPos();
        }


    }

    void ReturnLook()
    {
       
        Vector3 directionMain = target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(directionMain);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, finalTime * Time.deltaTime * rotateSpeed);
    }

    void LookAt()
    {

        //Vector3 direction = target.transform.position - gameObject.transform.position;
        Vector3 predictPosition = instance.CalcFuturePost(Vector3.Distance(transform.position , instance.transform.position) * predictBuffer);
        
        Quaternion rotation = Quaternion.LookRotation(predictPosition - transform.position); // Or this please fix when awake again Dom

       // transform.LookAt(predictPosition); // This works, it seems that converting the predict position to a rotation is not a good thing. Alternative solution: Rotate toward position, NOT rotation.

        transform.rotation = Quaternion.RotateTowards(transform.rotation , rotation, finalTime * Time.deltaTime * rotateSpeed); // If I pass out this is the issue
    }

    void ReturnPos()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, ogRotation, Time.deltaTime * 4.0f);
    }


    private IEnumerator LookAtCor()
    {   
        float lerpTime = 0;
        float currentTime = finalTime;
        LookAt();
        while (dist > thresholdCompare)
        {

            //lerpTime += Time.deltaTime;
            finalTime = Mathf.Lerp(currentTime, minSpeed, lerpTime += Time.deltaTime * 0.5f);
            yield return null;

        }

    }

    private IEnumerator ReturnCor()
    {
        float lerpTime = 0;
        float currentTime = finalTime;
        ReturnLook();

        while (dist < thresholdCompare)
        {

            //lerpTime += Time.deltaTime;
            finalTime = Mathf.Lerp(currentTime, maxSpeed, lerpTime += Time.deltaTime * 0.8f);
            yield return null;

        }

    }

 

   /* private IEnumerator StartOthers()
    {
        while (currentSpeedz.currentSpeed > 12f)
        {
            StartCoroutine(LookAtCor());
            StartCoroutine(ReturnCor());
            yield return null;
        }
    }*/

    //Speed calc down here, wait for David.

}


