using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light posteLuz;         
    [Range(0f, 24f)]

    public float horaDelDia;       
    public float horaInicioNoche = 18f;  
    public float horaFinNoche = 6f;     

    void Update()
    {

        if (EsDeNoche())
        {
            posteLuz.enabled = true;
        }
        else
        {
            posteLuz.enabled = false;
        }
    }

    bool EsDeNoche()
    {
        return horaDelDia >= horaInicioNoche || horaDelDia < horaFinNoche;
    }
}
