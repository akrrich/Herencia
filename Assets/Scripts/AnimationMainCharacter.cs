using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMainCharacter : MonoBehaviour
{
    private MainCharacter character;


    private void Start()
    {
        character = GetComponent<MainCharacter>();
    }

    private void Update()
    {
        AllAnimations();
    }

    public void AllAnimations()
    {
        AnimationIdle();
        AnimationRightAndLeft();
        AnimationUpAndDown();
    }

    private void AnimationIdle()
    {
        if (character.Rb.velocity.x == 0 && character.Rb.velocity.y == 0)
        {
            character.Anim.SetBool("idle", true);
        }

        else
        {
            character.Anim.SetBool("idle", false);
        }
    }

    private void AnimationRightAndLeft()
    {
        if (character.Rb.velocity.x < 0)
        {
            character.Anim.SetBool("runningLeft", true);
        }
        else
        {
            character.Anim.SetBool("runningLeft", false);
        }


        if (character.Rb.velocity.x > 0)
        {
            character.Anim.SetBool("runningRight", true);
        }
        else
        {
            character.Anim.SetBool("runningRight", false);
        }
    }

    private void AnimationUpAndDown()
    {
        if (character.Rb.velocity.y < 0)
        {
            character.Anim.SetBool("runningDown", true);
        }
        else
        {
            character.Anim.SetBool("runningDown", false);
        }


        if (character.Rb.velocity.y > 0)
        {
            character.Anim.SetBool("runningUp", true);
        }
        else
        {
            character.Anim.SetBool("runningUp", false);
        }
    }
}
