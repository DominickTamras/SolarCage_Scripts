using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
public class EnemyDeath : MonoBehaviour
{
    [Header("Explosion Strength")]

    public float radius;

    public float power;

    public bool endingKill;

    public Transform explosionForce;


    [Header("Death Settings")]
    public static int enemyCount;

    public List<GameObject> activateRB = new List<GameObject>();

    public List<GameObject> disableVFX = new List<GameObject>();

    public GameObject deathObj;

    public GameObject ignorePlayer;

    public EnemyTracking trackerDisabled;

    private VisualEffect deathExplosion;
   
    private bool dead;

    public bool reverseDeath;
    
    private bool explode;


    private void Awake()
    {
        enemyCount++;

        deathExplosion = deathObj.GetComponent<VisualEffect>();
    }

    public void Die()
    {
        if(reverseDeath == true)
        {

            FindObjectOfType<AudioManager2>().PlaySound("Gravity_Switch");

        }
        else
        {
            FindObjectOfType<AudioManager2>().PlaySound("Enemy_Death");
        }
       
        if(endingKill == false)
        {
            
            deathExplosion.Play();
            Destroy(gameObject, 5.5f);
            enemyCount--;
            dead = true;
            explode = true;
        }

        if (endingKill == true)
        {
            deathExplosion.Play();
            Destroy(gameObject);
            enemyCount--;
            dead = true;
            explode = true;
        }

    }

    public void Update()
    {
        if(dead)
        {
           

            foreach (GameObject go in activateRB) // Loops in each object and activates RB same for VFX below
            {
                go.GetComponent<Rigidbody>().isKinematic = false;

                go.GetComponent<Collider>().enabled = true;

                if (ignorePlayer != null)
                {
                    Physics.IgnoreCollision(ignorePlayer.GetComponent<Collider>(), go.GetComponent<Collider>());
                }
                

            }

            foreach(GameObject vfx in disableVFX)
            {
                
                if (disableVFX != null)
                {
                    vfx.GetComponent<VisualEffect>().enabled = false;
                }
            }

            if(explode == true)
            {
                Explode();
                explode = false;
            }

            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            if(trackerDisabled != null)
            {
                trackerDisabled.enabled = false;
                trackerDisabled.shootAnim.enabled = false;
                trackerDisabled.startShoot.enabled = false;
            }
       
            
        }
    }

    void Explode() // Creates explosion
    {   
        Collider[] explosion = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearby in explosion)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            
            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionForce.position, radius);
            }
        }
    }


}
