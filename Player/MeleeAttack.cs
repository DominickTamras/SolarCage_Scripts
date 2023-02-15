using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] PlayerAnimManager anim;
    [SerializeField] Shooting s;
    [SerializeField] float attackCD;
    float attackCDTimer;
    public bool isAttacking;

    [Header("Camera Shake")]
    [SerializeField] float magnitude = 4f;
    [SerializeField] float roughness = 4f;
    [SerializeField] float fadeInTime = 0.1f;
    [SerializeField] float fadeOutTime = 0.5f;

    Collider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    private void Update()
    {
        attackManager();
    }

    void attackManager()
    {
        if(attackCDTimer < 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                attackStart();

            }
        }

        //Attack CD
        if(attackCDTimer > -1)
        {
            attackCDTimer -= Time.deltaTime;
        }
    }

    void attackStart()
    {
        //anim.playPunch();
        anim.playOfficalPunch();
        FindObjectOfType<AudioManager>().PlaySound("Melee");
        col.enabled = true;
        isAttacking = true;
        attackCDTimer = attackCD;
        Invoke("attackEnd", 0.3f);
    }

    void attackEnd()
    {
        col.enabled = false;
        isAttacking = false;
        

    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyDeath enemy = other.GetComponent<EnemyDeath>();
        if(enemy != null)
        {
            enemy.Die();
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            s.hasBullet = true;
            FindObjectOfType<AudioManager>().PlaySound("Reload");

        }
        if (other.CompareTag("ReverseGravity"))
        {
            s.ReverseGravity();
            s.hasBullet = true;
            FindObjectOfType<AudioManager>().PlaySound("Reload");
        }
    }
}
