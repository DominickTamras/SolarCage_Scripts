using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{   
  
    [SerializeField] Animator anim;
    [SerializeField] Animator punchAnim;
    [SerializeField] Animator headBob;

    public void playPunch()
    {
        anim.SetTrigger("MeleeAttack");
    }

    public void playOfficalPunch()
    {
        //punchAnim.SetTrigger("MeleeAttacker");
        punchAnim.SetTrigger("MeleeAttack2");
    }

}
