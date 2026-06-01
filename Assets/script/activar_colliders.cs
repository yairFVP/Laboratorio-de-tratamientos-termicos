
using UdonSharp;
using Unity.Loading;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class activar_colliders : UdonSharpBehaviour
{

    public pinzas_dispmoviles soltar;
    public Collider horno;
    public Collider final;

    public GameObject probeta; // Asigna el objeto real aquí

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject == probeta)
        {
            horno.enabled = false;
            final.enabled = false;

            //soltar.SoltarObjeto();

        }
        



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == probeta)
        {
            horno.enabled = true;
            final.enabled = true;
        }
    }

}

    

    



