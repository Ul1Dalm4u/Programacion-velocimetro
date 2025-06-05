using UnityEngine;


public class CameraZone : Simulator
{
    [Header("Cámaras")]
    [Tooltip("Todas las cámaras de seguridad disponibles en el mapa")]
    public Camera[] securityCameras;        

    [Tooltip("La cámara principal del jugador/vehículo")]
    public Camera playerCamera;             

    [Header("Teclas de activación")]
    [Tooltip("Tecla para activar la vista de cámaras de seguridad")]
    public KeyCode activationKey = KeyCode.E;

    [Tooltip("Tecla para volver a la cámara del jugador")]
    public KeyCode returnKey = KeyCode.Q;

    private bool playerInZone = false;
    private int currentCamIndex = 0;
    private bool viewingSecurityCams = false;

    [Header("Datos de Creación")]
    [Tooltip("Selecciona quién es el creador de este CameraZone")]
    public Creadores creador;  // Proviene de Creadores.cs :contentReference[oaicite:0]{index=0}

    void Start()
    {
        // Asignamos el creador al Simulator interno
        AsignarCreador(creador);

        // Al inicio, asegurarse de que solo esté activa la cámara del jugador
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        for (int i = 0; i < securityCameras.Length; i++)
        {
            if (securityCameras[i] != null)
                securityCameras[i].enabled = false;
        }
    }

    void Update()
    {
        if (!playerInZone) return;

        // Si el jugador está en la zona, permitir activar/desactivar cámaras
        if (Input.GetKeyDown(activationKey) && !viewingSecurityCams)
        {
            SwitchToSecurityCam();
        }
        else if (Input.GetKeyDown(returnKey) && viewingSecurityCams)
        {
            ReturnToPlayerCam();
        }
        else if (viewingSecurityCams && Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextCamera();
        }
        else if (viewingSecurityCams && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousCamera();
        }
    }

    private void SwitchToSecurityCam()
    {
        if (securityCameras == null || securityCameras.Length == 0) return;
        playerCamera.enabled = false;
        securityCameras[currentCamIndex].enabled = true;
        viewingSecurityCams = true;
        Debug.Log("Vista de cámara de seguridad activada.");
    }

    private void ReturnToPlayerCam()
    {
        if (securityCameras == null || securityCameras.Length == 0) return;
        securityCameras[currentCamIndex].enabled = false;
        playerCamera.enabled = true;
        viewingSecurityCams = false;
        Debug.Log("Volviste a tu cámara.");
    }

    private void NextCamera()
    {
        if (securityCameras == null || securityCameras.Length == 0) return;
        securityCameras[currentCamIndex].enabled = false;
        currentCamIndex = (currentCamIndex + 1) % securityCameras.Length;
        securityCameras[currentCamIndex].enabled = true;
    }

    private void PreviousCamera()
    {
        if (securityCameras == null || securityCameras.Length == 0) return;
        securityCameras[currentCamIndex].enabled = false;
        currentCamIndex = (currentCamIndex - 1 + securityCameras.Length) % securityCameras.Length;
        securityCameras[currentCamIndex].enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            Debug.Log("Presioná E para ver las cámaras de seguridad.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            // Si el jugador salió de la zona mientras veía cámaras de seguridad, volver automáticamente
            if (viewingSecurityCams)
            {
                ReturnToPlayerCam();
            }
        }
    }


    public override void AsignarCreador(Creadores creadores)
    {
        this.CreadoresSimulator = creadores;  // Propiedad definida en Simulator.cs :contentReference[oaicite:1]{index=1}
        Debug.Log($"[CameraZone] Creador asignado: {creadores}");
    }


    public override void Describir()
    {
        Debug.Log($"CameraZone gestiona la vista de cámaras de seguridad y la cámara del jugador. Creado por: {CreadoresSimulator}");
    }
}
