using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMapRotation : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float speed = 2f; // Velocidad de movimiento del enemigo

    private void Update()
    {
        // Calcular la dirección desde el enemigo hacia el jugador
        Vector3 direction = player.position - transform.position;
        direction.z = 0; // Asegurarse de que la posición z sea 0 para evitar problemas en 2D

        // Calcular el ángulo en radianes y luego convertir a grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ajustar el ángulo para que la imagen apunte correctamente hacia el jugador
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Mover al enemigo hacia el jugador
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
