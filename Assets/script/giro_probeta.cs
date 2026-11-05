
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class giro_probeta : UdonSharpBehaviour
{


    public GameObject pinza;
    public GameObject seguidor;
    public GameObject[] probetas;

    public GameObject marcador;
    public GameObject dona;

    public int vueltasTotales = 3;

    private int vueltasActuales = 0;
    public bool terminado = false;

    private bool pinzaDentro = false;
    private bool seguidorDentro = false;
    private bool probetaDentro = false;

    
    public void OnTriggerEnter(Collider other)
    {
        

        // Detectar pinza
        if (other.gameObject == pinza)
        {
            pinzaDentro = true;
        }

        // Detectar seguidor
        if (other.gameObject == seguidor)
        {
            seguidorDentro = true;
        }

        // Detectar probeta (cualquiera del arreglo)
        for (int i = 0; i < probetas.Length; i++)
        {
            if (other.gameObject == probetas[i])
            {
                probetaDentro = true;
                break;
            }
        }

        // Si los 3 entraron → cuenta vuelta
        if (seguidorDentro )
        {
            vueltasActuales++;

            Debug.Log("Vuelta: " + vueltasActuales);

            // Reiniciar detección
            pinzaDentro = false;
            seguidorDentro = false;
            probetaDentro = false;

            // Si llegó al límite
            if (vueltasActuales >= vueltasTotales)
            {

                terminado = true;
                DesaparecerObjetos();
                
            }
        }
    }

    public void DesaparecerObjetos()
    {
       


        seguidor.SetActive(false);
        marcador.SetActive(false);
        dona.SetActive(false);

        Debug.Log("Proceso terminado");


    }
    

    
}

