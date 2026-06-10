using UnityEngine;

/// <summary>
/// Rotazione della telecamera con il mouse (stile prima persona).
/// Attacca questo script alla Camera, e indica come 'playerBody' il personaggio.
/// </summary>
public class MouseLook : MonoBehaviour
{
    [Tooltip("Sensibilità del mouse.")]
    public float mouseSensitivity = 100f;

    [Tooltip("Il transform del corpo del personaggio (ruota in orizzontale).")]
    public Transform playerBody;

    private float xRotation = 0f;

    private void Start()
    {
        // Se 'playerBody' non è stato assegnato nell'Inspector, prova a usare
        // il genitore della camera (di solito è il personaggio). Così la
        // rotazione destra/sinistra funziona anche senza configurazione manuale.
        if (playerBody == null && transform.parent != null)
        {
            playerBody = transform.parent;
        }

        // Blocca e nasconde il cursore al centro dello schermo.
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Premi ESC per liberare il cursore (utile per uscire dal Play).
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotazione verticale della camera (con limite per non ribaltarsi).
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotazione orizzontale: ruota tutto il corpo del personaggio.
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
