using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private float speed = 300;

    [SerializeField] private Text textComponent; 

    private bool showText = false;

    private Vector2 position;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        textComponent.enabled = false;

        position = new Vector2(rectTransform.anchoredPosition.x, speed * Time.deltaTime);
    }

    private void Update()
    {
        ChangeScene();
        MoveText();
    }


    private void ChangeScene()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void MoveText()
    {
        if (!showText)
        {
            rectTransform.anchoredPosition += position;
        }

        if (rectTransform.anchoredPosition.y > 3080 && !showText)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 3080);

            showText = true;
            StartCoroutine(ShowAndHideText());
        }
    }

    private IEnumerator ShowAndHideText()
    {
        while (showText)
        {
            textComponent.enabled = !textComponent.enabled;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
