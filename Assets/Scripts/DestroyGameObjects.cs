using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjects : MonoBehaviour
{
    public UiController UiController;
    public GameManager gameManager;
    private VictorController victorController;

    private void HandlePersonajeInstanciado(VictorController vc)
    {
        victorController = vc;
    }

    private void OnEnable()
    {
        VictorController.OnPersonajeInstanciado += HandlePersonajeInstanciado;
    }

    private void OnDisable()
    {
        if (victorController != null)
        {
            VictorController.OnPersonajeInstanciado -= HandlePersonajeInstanciado;
        }
    }
    public void DestroyObjects()
    {
        Destroy(UiController.gameObject);
        Destroy(victorController.gameObject, 1f);
        Destroy(gameManager.gameObject);
    }
}
