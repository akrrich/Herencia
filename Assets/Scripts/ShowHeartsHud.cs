using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowHeartsHud : MonoBehaviour
{
    private MainCharacter character;


    [SerializeField] private GameObject heartImage1;
    [SerializeField] private GameObject heartImage2;
    [SerializeField] private GameObject heartImage3;

    private void Start()
    {
        character = GameObject.FindObjectOfType<MainCharacter>();
    }


    private void Update()
    {
        ShowTotalHearts();
    }

    private void ShowTotalHearts()
    {
        heartImage1.SetActive(character.Life >= 1);
        heartImage2.SetActive(character.Life >= 2);
        heartImage3.SetActive(character.Life >= 3);
    }
}
