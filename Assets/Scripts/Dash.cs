using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dassh : MonoBehaviour
{
    // Variables configurables
    public float dashDistance = 20f; // Distancia del dash
    public float dashDuration = 0.2f; // Duración del dash en segundos
    public float doubleTapTime = 0.3f; // Tiempo máximo entre las dos pulsaciones para considerarlo doble click
    public float dashCooldown = 2f; // Cooldown de 2 segundos entre dashes

    private Rigidbody2D rb; // Para mover el personaje
    private bool isDashing = false; // Si el personaje está haciendo dash
    private bool canDash = true; // Controla si el dash está disponible
    private float dashCooldownTimer = 0f; // Temporizador de cooldown
    private float dashTime; // Temporizador de la duración del dash
    private Vector2 dashStartPosition; // Posición inicial del dash
    private Vector2 dashTargetPosition; // Posición objetivo del dash

    private float lastTapTimeD = 0f;
    private float lastTapTimeA = 0f;
    private float lastTapTimeRightArrow = 0f;
    private float lastTapTimeLeftArrow = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtenemos el Rigidbody2D del personaje
    }

    void Update()
    {
        // Controlar el cooldown del dash
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f)
            {
                canDash = true; // Habilitar el dash nuevamente
            }
        }

        // Si el personaje está dashing, controlar el movimiento suave
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime > 0f)
            {
                // Interpolamos la posición entre el inicio del dash y la posición objetivo
                float progress = 1 - (dashTime / dashDuration); // Progreso entre 0 y 1
                Vector2 newPosition = Vector2.Lerp(dashStartPosition, dashTargetPosition, progress);
                rb.MovePosition(newPosition);
            }
            else
            {
                // Terminar el dash
                isDashing = false;
            }
        }

        // Solo permitir detectar un nuevo dash si no está en cooldown y no está dashing
        if (canDash && !isDashing)
        {
            DetectDash();
        }
    }

    void DetectDash()
    {
        // Para la tecla "D" (derecha)
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastTapTimeD < doubleTapTime)
            {
                StartDash(Vector2.right); // Dash hacia la derecha
            }
            lastTapTimeD = Time.time;
        }

        // Para la tecla "A" (izquierda)
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastTapTimeA < doubleTapTime)
            {
                StartDash(Vector2.left); // Dash hacia la izquierda
            }
            lastTapTimeA = Time.time;
        }

        // Para la flecha derecha
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Time.time - lastTapTimeRightArrow < doubleTapTime)
            {
                StartDash(Vector2.right); // Dash hacia la derecha
            }
            lastTapTimeRightArrow = Time.time;
        }

        // Para la flecha izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Time.time - lastTapTimeLeftArrow < doubleTapTime)
            {
                StartDash(Vector2.left); // Dash hacia la izquierda
            }
            lastTapTimeLeftArrow = Time.time;
        }
    }

    void StartDash(Vector2 direction)
    {
        // Configurar el dash
        isDashing = true;
        canDash = false; // Deshabilitar el dash hasta que termine el cooldowndfgdfg
        dashCooldownTimer = dashCooldown; // Iniciar el cooldown
        dashTime = dashDuration; // Establecer la duración del dash
        dashStartPosition = transform.position; // Guardar la posición inicial del dash
        dashTargetPosition = dashStartPosition + direction * dashDistance; // Calcular la posición objetivo

        // Desactivar cualquier otro movimiento (opcional, dependiendo de cómo controles el movimiento normal)
        rb.velocity = Vector2.zero;
    }
}
