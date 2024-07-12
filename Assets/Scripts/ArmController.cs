using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] GameObject armSprite;
    [SerializeField] Transform armPivot;
    private float localScaleRight;
    private float localScaleLeft;

    private void Start()
    {
        localScaleRight = armSprite.transform.localScale.y;
        localScaleLeft = -armSprite.transform.localScale.y;
    }
    void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;

        transform.position = armPivot.position;

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        /*
        Cuadrantes
                        90�
                        | 
                        |
                    4   |   1
            180�/       |
            -180�  ------|------- 0�
                        |
                    3   |   2
                        |
                    -90�

        */
        // Si el brazo est� en el cuadrante 3 o 4 est� mirando a la izquierda
        var direction = (angle > 90 && angle < 180 || angle > -180 && angle < -90) ? localScaleLeft : localScaleRight;
        armSprite.transform.localScale = new Vector3(armSprite.transform.localScale.x, direction, armSprite.transform.localScale.z);
    }
}
