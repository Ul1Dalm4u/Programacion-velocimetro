using UnityEngine;
using System;
public class LightController : Simulator
{
    [Header("Configuración de hora")]
    [Range(0f, 24f)]
    public float horaDelDia;         

    [Tooltip("Hora a partir de la cual se considera noche")]
    public float horaInicioNoche = 18f;  

    [Tooltip("Hora hasta la cual se considera noche")]
    public float horaFinNoche = 6f;      

    private Light[] luces;

    [Header("Datos de Creación")]
    [Tooltip("Selecciona quién es el creador de este LightController")]
    public Creadores creador;  // Proviene de Creadores.cs :contentReference[oaicite:0]{index=0}

    void Start()
    {
        // Asignamos el creador al Simulator interno
        AsignarCreador(creador);

        // Buscar todos los objetos con el tag "PosteLuz" que tengan componente Light
        GameObject[] faros = GameObject.FindGameObjectsWithTag("PosteLuz");
        luces = new Light[faros.Length];
        for (int i = 0; i < faros.Length; i++)
        {
            luces[i] = faros[i].GetComponent<Light>();
        }
    }

    void Update()
    {
        bool activar = EsDeNoche();
        foreach (Light luz in luces)
        {
            if (luz != null)
                luz.enabled = activar;
        }
    }

    bool EsDeNoche()
    {
        return horaDelDia >= horaInicioNoche || horaDelDia < horaFinNoche;
    }

    public override void AsignarCreador(Creadores creadores)
    {
        // La propiedad heredada guarda el creador seleccionado
        this.CreadoresSimulator = creadores;
        Debug.Log($"[LightController] Creador asignado: {creadores}");
    }

    public override void Describir()
    {
        Debug.Log($"LightController controla encendido/apagado de luces según la hora. Creado por: {CreadoresSimulator}");
    }
}
