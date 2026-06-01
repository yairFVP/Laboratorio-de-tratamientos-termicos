
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Display_Temp : UdonSharpBehaviour
{
    public GameObject[] objetos;
    public float tiempoPaso = 3f;

    private int indice = 0;

    public void IniciarSecuencia()
    {
        indice = 0;
        ActivarActual();
        
    }

    public void ActivarActual()
    {
        for (int i = 0; i < objetos.Length; i++)
        {
            objetos[i].SetActive(i == indice);
        }

        indice++;

        if (indice < objetos.Length)
        {
            SendCustomEventDelayedSeconds(nameof(ActivarActual), tiempoPaso);

        }
        
            

    }


    public void ApagandoHorno()//120326
    {
        for (int i = 0; i < objetos.Length; i++)
        {
            objetos[i].SetActive(i == indice);
        }

        indice--;

        if (indice >= 0)
        {
            SendCustomEventDelayedSeconds(nameof(ApagandoHorno), tiempoPaso);
        }

    }
    public void IniciarSecuenciaReversa()
    {
        indice = objetos.Length - 1; // empezar desde el último
        ApagandoHorno();
    }
    void Start()
    {
        ApagarTodos();
    }

    public void ApagarTodos()
    {
        for (int i = 1; i < objetos.Length; i++)
        {
            objetos[i].SetActive(false);
        }
    }

    /*public void FinSecuencia()
    {
        if(indice == objetos.Length)
        {
            ApagarTodos();
            
        }
    }*/

}
