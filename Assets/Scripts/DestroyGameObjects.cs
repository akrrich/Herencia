using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjects : MonoBehaviour
{
    public UiController UiController;
    public VictorController victorController;
    public GameManager gameManager;

    // esto es temporal
    public Map bosqueMuerto;

    public void DestroyObjects()
    {
        Destroy(UiController.gameObject);
        Destroy(victorController.gameObject, 1f);
        Destroy(gameManager.gameObject);

        Destroy(bosqueMuerto.gameObject);
    }
}
