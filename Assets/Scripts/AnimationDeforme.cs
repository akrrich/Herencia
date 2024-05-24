using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AnimationDeforme : MonoBehaviour
{
    private DeformeController deforme;


    private void Update()
    {
        AnimationIdle();
        AnimationRightAndLeft();
    }

    private void Start()
    {
        deforme = GetComponent<DeformeController>();
    }

    private void AnimationIdle()
    {
        if (deforme.Rb.velocity.x == 0 && deforme.Rb.velocity.y == 0)
        {
            deforme.Anim.SetBool("idle", true);
        }

        else
        {
            deforme.Anim.SetBool("idle", false);
        }
    }

    private void AnimationRightAndLeft()
    {
        deforme.Anim.SetBool("walking", deforme.Rb.velocity.magnitude > 0.1f);
    }
}
