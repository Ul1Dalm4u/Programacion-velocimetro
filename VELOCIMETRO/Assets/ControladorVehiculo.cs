using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControladorVehiculo : MonoBehaviour
{
    private Rigidbody rb;
    public float fuerzaAceleracion = 10f;
    public float velocidadMaxima = 120f; // en km/h
    public float friccion = 0.98f;
    public float fuerzaGiro = 50f; // fuerza de giro modificable

    [HideInInspector]
    public float velocidadActual;
    [HideInInspector]
    public string marchaActual;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ActualizarVelocidad();

        // Aplicar aceleración y freno
        if (Input.GetKey(KeyCode.W))
        {
            if (velocidadActual < velocidadMaxima)
            {
                rb.AddForce(transform.forward * fuerzaAceleracion);
            }
            marchaActual = "D";
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * fuerzaAceleracion);
            marchaActual = "R";
        }
        else
        {
            // Aplicar fricción
            rb.linearVelocity *= friccion;
            marchaActual = "N";
        }

        // Movimiento lateral (giro)
        if (Input.GetKey(KeyCode.A))
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, -fuerzaGiro * Time.deltaTime, 0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, fuerzaGiro * Time.deltaTime, 0f));
        }
    }

    private void ActualizarVelocidad()
    {
        float velocidadEnMS = rb.linearVelocity.magnitude;

        if (velocidadEnMS < 0.1f)
        {
            velocidadActual = 0f;
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            velocidadActual = velocidadEnMS * 3.6f;
        }
    }
}
