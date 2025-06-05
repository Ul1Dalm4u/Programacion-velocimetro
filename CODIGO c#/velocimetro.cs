using UnityEngine;
using TMPro;

public class Velocimetro : Simulator
{
    public ControladorVehiculo vehiculo;

    public Transform agujaVelocimetro;
    public float rotacionMin = 0f;
    public float rotacionMax = -270f;
    public float velocidadMaxima = 200f;

    [SerializeField]
    private Creadores creadores = Creadores.Dalmau_Ulises;

    private void Awake()
    {
        creadores = Creadores.Dalmau_Ulises;
    }

    private void Start()
    {
        AsignarCreador(creadores);
        Describir();
    }

    void Update()
    {
        if (vehiculo != null)
        {
            float v = vehiculo.velocidadActual;
            string marcha = vehiculo.marchaActual;

            Debug.Log($"Velocidad: {v:F1} km/h");

            if (agujaVelocimetro != null)
            {
                float velocidadNormalizada = Mathf.Clamp01(v / velocidadMaxima);
                float anguloZ = Mathf.Lerp(rotacionMin, rotacionMax, velocidadNormalizada);
                agujaVelocimetro.localEulerAngles = new Vector3(0, 0, anguloZ);
            }
        }
    }

    public override void Describir()
    {
        Debug.Log($"Soy el velocímetro, creado por: {CreadoresSimulator}");
    }

    public override void AsignarCreador(Creadores creador)
    {
        CreadoresSimulator = creador;
        Debug.Log($"Creador del velocímetro asignado: {creador}");
    }
}
