using UnityEngine;

public class LightController : MonoBehaviour
{
    [Range(0f, 24f)]
    public float horaDelDia;         

    public float horaInicioNoche = 18f;  
    public float horaFinNoche = 6f;      

    private Light[] luces;

    void Start()
    {
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
}
