using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShootTip : MonoBehaviour
{

    public EnemyShoot_Settings settingFeature;

    public GameObject enemyMuzzle_go;

    private VisualEffect enemyMuzzle;

    /* private float timeBetweenShots;

     public float startTimeBetweenShots;

    //re-add if you remove scriptable object

     public GameObject projectile;*/

    private Transform enemyTransform;
    [HideInInspector]
    public float timeBetweenShots;

    void Start()
    {
        enemyMuzzle = enemyMuzzle_go.GetComponent<VisualEffect>();
        enemyTransform = GetComponentInParent<Transform>();
        timeBetweenShots = settingFeature.startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (timeBetweenShots <= 0)
        {
            enemyMuzzle.initialEventName = "OnPlay";

            Instantiate(settingFeature.projectile, gameObject.transform.position, enemyTransform.rotation);
            
            enemyMuzzle.Play();
    
            timeBetweenShots = settingFeature.startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
