using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyShoot : MonoBehaviour
{ // Update tomorrow. See why ball doesn't go forward. Turn into rigidbody transform.rotation * Vector3.forward
    [SerializeField] LayerMask Player;
    public float speed;
 
    private Transform player;

    public bool isLockedOn;

    private bool playedOnce = false;

    private Vector3 target;

    public GameObject enemyBulletEffect;

    public GameObject enemyBulletEnd;

    private VisualEffect explodeEffect;

    private VisualEffect explodeEffectStart;

    public AudioSource firedShot;

    public AudioSource exploded;
    void Awake()
    {
        firedShot.Play();
        explodeEffect = enemyBulletEnd.GetComponent<VisualEffect>();
        explodeEffectStart = enemyBulletEffect.GetComponent<VisualEffect>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * Vector3.forward * speed * Time.deltaTime;
        
        if(isLockedOn) // work on better to follow. Taper off eventually
        {
            transform.position = Vector3.MoveTowards( transform.position, player.position, speed * Time.deltaTime);

        }

            
        StartCoroutine(JustDie());
      
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Enemy") && !other.CompareTag("IgnoreBullet") && !other.CompareTag("Dome"))
        {
            DestroyShot();
            exploded.Play();
        }



        
    }

    public void DestroyShot()

    {
        speed = 0;
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        explodeEffectStart.enabled = false; // Initial particle
        explodeEffect.Play(); //Explode on death
        Destroy(gameObject, 1f);
    }

    IEnumerator JustDie()
    {
        yield return new WaitForSeconds(2);

        if (isLockedOn == false && !playedOnce)

        {
             DestroyShot();
            playedOnce = true;
            exploded.Play();
        }
    }
        
}
