using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogues : MonoBehaviour
{
    [SerializeField] private List<GameObject> bosImages;
    [SerializeField] private List<string> conversationList;

    private TMP_Text conversationText;
    private GameObject panel;

    private int indexBosImage = 0;
    private int indexConversationList = 0;
    private bool dialogueMode = false;

    private void Start()
    {
        panel = transform.Find("Panel").gameObject;

        conversationText = panel.transform.Find("Text Dialogue").GetComponent<TMP_Text>();
    }


    private void Update()
    {
        panel.SetActive(dialogueMode);

        conversationText.text = conversationList[indexConversationList];
        bosImages[indexBosImage].SetActive(dialogueMode);

        ActiveDialogueMode();
        ChangeTextsWithClick();

        conditionForChangeBosImage(5, 0);
        conditionForChangeBosImage(9, 1);
        conditionForChangeBosImage(13, 2);
    }


    private void ActiveDialogueMode()
    {
        // aca iria la condicion para que comience el dialogo
        if (Input.GetKeyDown(KeyCode.H))
        {
            dialogueMode = true;
        }
    }


    private void ChangeTextsWithClick()
    {
        if (dialogueMode)
        {
                                                                       // limite de la cantidad de textos que hay ahora, si se agregan mas hay que cambiar el valor
            if (Input.GetMouseButtonDown(0) && indexConversationList < 12)
            {
                indexConversationList++;
            }
                                                                            // limite de la cantidad de textos que hay ahora, si se agregan mas hay que cambiar el valor
            else if (Input.GetMouseButtonDown(0) && indexConversationList > 11)
            {
                dialogueMode = false;

                Invoke("InvokeCharacterCondition", 0.25f);
            }
        }
    }

    /* esta es la logica para que cambie la imagen del bos cuando se vuelve a abrir el
       modo de dialogo reutilizando la funcion, si se agregan mas textos, hay que cambiar el conversation value */
    private void conditionForChangeBosImage(int conversationValue, int bosValue)
    {
        if (indexConversationList == conversationValue && indexBosImage == bosValue)
        {
            dialogueMode = false;

            bosImages[indexBosImage].SetActive(false);

            indexBosImage++;

        }
    }
}
