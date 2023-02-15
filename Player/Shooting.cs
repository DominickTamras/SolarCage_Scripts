using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using EZCameraShake;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] MeleeAttack ma;
    [SerializeField] GameObject bulletIndicator;
    [SerializeField] float range = 100;
    public bool hasBullet = true;
    public GunSway kickback;
    public GunSway armKickBack;

   

    [Header("Reverse Gravity")]
    [SerializeField] float flipCamSpeed;
    public bool reverseGravity = false;
    float zVel;

    [Header("Camera Shake")]
    [SerializeField] float magnitude = 4f;
    [SerializeField] float roughness = 4f;
    [SerializeField] float fadeInTime = 0.1f;
    [SerializeField] float fadeOutTime = 0.5f;

    [Header("VFX")]

    public GameObject gunEffectCharge;
    public GameObject gunEffectFlash;
    public GameObject gunEffectCircle;
    public GameObject gunImpactEffect;
    public GameObject gunBurstAmmoEffect;
    public Animator gunKickBackAnim;
    public Image crossHairChange;
    public LineRenderer bulletTrail;
    public GameObject trailSpawnLocal;
    public GameObject trailMissEnd;

    private VisualEffect bulletCharge;
    private VisualEffect bulletFlash;
    private VisualEffect gunCircle;
    private VisualEffect gunBurstAmmo;

    [Header("SFX")]
    public AudioSource railgunShoot;



    CameraLook cl;
    PlayerMovement pm;

    private void Start()
    {
        
        cl = GetComponent<CameraLook>();
        pm = GetComponent<PlayerMovement>();

        bulletCharge = gunEffectCharge.GetComponent<VisualEffect>();
        bulletFlash = gunEffectFlash.GetComponent<VisualEffect>();
        gunCircle = gunEffectCircle.GetComponent<VisualEffect>();
        gunBurstAmmo = gunBurstAmmoEffect.GetComponent<VisualEffect>();
       


    }

    private void Update()
    { 
        RaycastHit crossChange;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out crossChange, range))
        {
            if(crossChange.transform.tag == "Enemy" || crossChange.transform.tag == "ReverseGravity")
            {
                crossHairChange.color = Color.red;
            }

            else
            { 
                crossHairChange.color = Color.white;
            }


        }

        else
        {
            crossHairChange.color = Color.white;
        }

        FlipView();

        if (!MenuManager.GameIsPaused)
        {
            BulletIndicator();

            if (Input.GetKeyDown(KeyCode.Mouse0) && !ma.isAttacking)
            {
                if (hasBullet)
                {
                    Shoot();
                    kickback.GunKickBack();
                    armKickBack.GunKickBack();
                    FindObjectOfType<AudioManager>().PlaySound("Bullet Hum");
                }
            }
        }
    }

    void Shoot()
    {
        //railgunShoot.Play();
        FindObjectOfType<AudioManager>().PlaySound("PlayerShoot");
        gunKickBackAnim.Play("GunKickBack", -1, 0f);
        bulletCharge.Play();
        gunCircle.Play();
        bulletFlash.Play();
        gunBurstAmmo.Play();
       // bulletTrail.Play();
        hasBullet = false;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            
            //VFX
            GameObject impactVFX = Instantiate(gunImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactVFX, 1);

           

            //Shooting Enemy
            EnemyDeath enemy = hit.transform.GetComponent<EnemyDeath>();
            if(enemy != null)
            {
                enemy.Die();
                hasBullet = true;
                FindObjectOfType<AudioManager>().PlaySound("Reload");

                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            }


            //Reverse Gravity
            if(hit.collider.CompareTag("ReverseGravity"))
            {
                ReverseGravity();
                hasBullet = true;
            }


            TrailSpawn(hit.point);
        }
       

        else
        {
           
            TrailSpawn(trailMissEnd.transform.position); //Not hitting anything
        }
         
       
    }
    
     void TrailSpawn(Vector3 hitPoint)
    {
        GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, trailSpawnLocal.transform.position, Quaternion.identity);

        LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();
        lineR.SetPosition(0, trailSpawnLocal.transform.position);
        lineR.SetPosition(1, hitPoint);
        Destroy(bulletTrailEffect, 0.2f);
    }
    void BulletIndicator()
    {
        if(hasBullet)
        {
            bulletIndicator.SetActive(true);
        }
        else if (!hasBullet)
        {
            bulletIndicator.SetActive(false);
        }
    }

    public void ReverseGravity()
    {
        if (!reverseGravity)
        {
            reverseGravity = true;
        }
        else
        {
            reverseGravity = false;
        }
    }

    void FlipView()
    {
        if (reverseGravity)
        {
            Vector3 currentRotation = transform.eulerAngles;
            if (currentRotation.z < 179.9f)
            {
                currentRotation.z = Mathf.SmoothDampAngle(currentRotation.z, 180f, ref zVel, flipCamSpeed);
            }
            else
            {
                currentRotation.z = 180f;
                zVel = 0f;
            }
            cl.zRotation = currentRotation.z;
            transform.eulerAngles = currentRotation;
        }
        else if(!reverseGravity)
        {
            Vector3 currentRotation = transform.eulerAngles;
            if(currentRotation.z > 0.1f)
            {
                currentRotation.z = Mathf.SmoothDampAngle(currentRotation.z, 0f, ref zVel, flipCamSpeed);
            }
            else
            {
                currentRotation.z = 0f;
                zVel = 0f;
            }
            cl.zRotation = currentRotation.z;
            transform.eulerAngles = currentRotation;
        }
    }

  
}
