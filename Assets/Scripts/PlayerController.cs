using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canJump;
    public float groundCheckRadius = 0.2f;
    public MonoBehaviour Player;
    public GameObject Menu;

    // Variable para controlar la dirección del jugador
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Llamamos a la función para manejar el flip
        HandleFlip(moveInput);

        // Verifica si el jugador está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Solo permitimos el salto si está en el suelo
        if (isGrounded)
        {
            canJump = true;
        }

        // Comprobamos si se presiona la tecla de salto y si puede saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false; // Desactiva el salto hasta que toque el suelo
        }

        // Verificar la tecla ESC para abrir el menú
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnableObject(Menu);
        }
    }

    // Función para voltear el sprite del jugador según la dirección del movimiento
    void HandleFlip(float moveInput)
    {
        // Si el jugador se está moviendo hacia la derecha pero no está mirando a la derecha
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        // Si el jugador se está moviendo hacia la izquierda pero no está mirando a la izquierda
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
            //ASdsldfkmdgñldfknghgñ
        }
    }

    // Función para invertir la escala en el eje X para hacer el flip
    void Flip()
    {
        isFacingRight = !isFacingRight; // Cambiamos la dirección
        Vector3 scaler = transform.localScale; // Obtenemos la escala actual
        scaler.x *= -1; // Invertimos el eje X
        transform.localScale = scaler; // Asignamos la nueva escalafgj
    }

    private void OnDrawGizmos()
    {
        // Visualización del GroundCheck en la vista de Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    // Función para desactivar un GameObject
    public void DisableScript(MonoBehaviour scriptToDisable)
    {
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
        }
    }

    // Función para activar un GameObject
    public void EnableObject(GameObject objectToEnable)
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }

    public void DisableObjects(GameObject[] objectsToDisable)
    {
        if (objectsToDisable != null)
        {
            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}

