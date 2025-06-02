using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public Camera[] securityCameras;         // Cámaras del mapa
    public Camera playerCamera;              // La cámara del jugador/vehículo
    public KeyCode activationKey = KeyCode.E;
    public KeyCode returnKey = KeyCode.Q;

    private bool playerInZone = false;
    private int currentCamIndex = 0;
    private bool viewingSecurityCams = false;

    void Update()
    {
        if (playerInZone)
        {
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
    }

    private void SwitchToSecurityCam()
    {
        playerCamera.enabled = false;
        securityCameras[currentCamIndex].enabled = true;
        viewingSecurityCams = true;
        Debug.Log("Vista de cámara de seguridad activada.");
    }

    private void ReturnToPlayerCam()
    {
        securityCameras[currentCamIndex].enabled = false;
        playerCamera.enabled = true;
        viewingSecurityCams = false;
        Debug.Log("Volviste a tu cámara.");
    }

    private void NextCamera()
    {
        securityCameras[currentCamIndex].enabled = false;
        currentCamIndex = (currentCamIndex + 1) % securityCameras.Length;
        securityCameras[currentCamIndex].enabled = true;
    }

    private void PreviousCamera()
    {
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
            if (viewingSecurityCams)
            {
                ReturnToPlayerCam();
            }
        }
    }
}
