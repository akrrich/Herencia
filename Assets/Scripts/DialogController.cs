using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image leftCharacterContainer;
    [SerializeField] Image rightCharacterContainer;
    
    [SerializeField] Image leftCharacterImage;
    [SerializeField] Image rightCharacterImage;

    [SerializeField] GameObject centerObject;
    [SerializeField] Image centerObjectImage;
    [SerializeField] Image centerObjectImageShadow;
    [SerializeField] TMP_Text dialog;
    [SerializeField] TMP_Text pressAnyKeyToContinue;

    [Header("Pulse Effect")]
    public float amplitude = 0.5f;
    public float frequency = 1f;
    private Vector3 startScale;
    private Vector3 currentScale;

    // Efecto de tetxo tipeado
    [Header("Type Effect")]
    public float delay = 0.1f; // El retraso entre cada carácter

    [Header("Dialog List")]
    [SerializeField] List<DialoguesData> dialoguesList;
    private DialoguesData selectedDialogData;
    private int currentDialogStep;

    private bool isTyping = false; // Indica si la coroutine está corriendo
    private Coroutine typingCoroutine;

    private string fullText; // El texto completo que quieres mostrar

    public void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }

    void PulseCenterObject()
    {
        if (centerObject == null)
            return;

        float newSin = Mathf.Sin(Time.realtimeSinceStartup * frequency) * amplitude;
        float newScaleX = currentScale.x + newSin;
        float newScaleY = currentScale.y + newSin;

        centerObject.transform.localScale = new Vector3(newScaleX, newScaleY, centerObject.transform.localScale.z);
    }
    private IEnumerator TypeText()
    {
        isTyping = true;
        pressAnyKeyToContinue.gameObject.SetActive(false);
        dialog.text = ""; // Limpia el texto inicialmente
        foreach (char letter in fullText.ToCharArray())
        {
            dialog.text += letter; // Añade el siguiente carácter
            yield return new WaitForSecondsRealtime(delay); // Espera un poco antes de añadir el siguiente
        }
        isTyping = false;
        pressAnyKeyToContinue.gameObject.SetActive(true);
    }

    void CheckTypeEffect()
    {
        if (!(Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
            return;

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialog.text = fullText;
            isTyping = false;
            pressAnyKeyToContinue.gameObject.SetActive(true);
        }
        else
        {
            StartNextDialogStep();
        }
    }

    void StartNextDialogStep()
    {
        if(currentDialogStep < selectedDialogData.dialogList.Count)
        {
            fullText = selectedDialogData.dialogList[currentDialogStep];
            typingCoroutine = StartCoroutine(TypeText());
            currentDialogStep++;
        }

        else
        {
            GameManager.Instance.HideDialog();
        }
    }

    private void SetDialogUIImages()
    {
        leftCharacterContainer.gameObject.SetActive(false);
        rightCharacterContainer.gameObject.SetActive(false);
        centerObject.SetActive(false);

        if (selectedDialogData.leftCharacter != null) 
        {
            leftCharacterContainer.gameObject.SetActive(true);
            leftCharacterImage.sprite = selectedDialogData.leftCharacter;
        }

        if (selectedDialogData.rightCharacter != null)
        {
            rightCharacterContainer.gameObject.SetActive(true);
            rightCharacterImage.sprite = selectedDialogData.rightCharacter;
        }

        if (selectedDialogData.centerObjectImage == null)
            return;

        centerObject.SetActive(true);
        // Obtener la relación de aspecto del nuevo sprite.
        float aspectRatio = (float)selectedDialogData.centerObjectImage.rect.width / (float)selectedDialogData.centerObjectImage.rect.height;

        centerObjectImage.sprite = selectedDialogData.centerObjectImage;
        centerObjectImageShadow.sprite = selectedDialogData.centerObjectImage;
        currentScale = new Vector3(startScale.x * aspectRatio, startScale.y, startScale.z);
    }

    public void SetDialog(int dialogID)
    {
        selectedDialogData = dialoguesList[dialogID];
        currentDialogStep = 0;
        SetDialogUIImages();
        StartNextDialogStep();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (centerObject != null)
        {
            startScale = centerObject.transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PulseCenterObject();
        CheckTypeEffect();
    }
}
