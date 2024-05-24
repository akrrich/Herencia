using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMainCharacter : MonoBehaviour
{
    private MainCharacter character;


    [SerializeField] private AudioSource deafeatSound;


    private float counterForDeath = 0f;


    private bool canDoAnimations = true;

    private bool canShowDefeatScreen = false;

    private bool isSoundPlaying = false;

    private bool canHearSong = false;


    public bool CanDoAnimations
    {
        set
        {
            canDoAnimations = value;
        }
    }
    public bool CanShowDefeatScreen
    {
        get
        {
            return canShowDefeatScreen;
        }
    }


    private void Start()
    {
        character = GetComponentInParent<MainCharacter>();
    }

    private void Update()
    {
        if (isSoundPlaying == false && canHearSong == true)
        {
            deafeatSound.Play();

            isSoundPlaying = true;

        }

        if (canDoAnimations == true)
        {
            AnimationIdle();
            AnimationRightAndLeft();
        }

        AnimationDeath();

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
        if (character.Alive == true)
        {
            character.Anim.SetBool("walking", character.Rb.velocity.magnitude > 0.1f);
        }

        else
        {
            character.Anim.SetBool("walking", false);
        }
    }

    private void AnimationDeath()
    {
        if (character.Alive == false)
        {
            canHearSong = true;

            character.Anim.SetBool("death", true);

            counterForDeath += Time.deltaTime;

            if (counterForDeath > 0.8f)
            {
                character.Anim.SetBool("death", false);

                canShowDefeatScreen = true;
            }
        }
    }
}
