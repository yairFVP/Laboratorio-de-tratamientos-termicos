
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class info_activar_video : UdonSharpBehaviour
{

    public GameObject[] imagenes;

    private int indiceActual = 0;

    void Start()
    {
        
        MostrarImagen(indiceActual);
        
        

    }

    public override void Interact()
    {
        // Avanza al siguiente índice
        indiceActual++;

        // Si llega al final, vuelve al inicio
        if (indiceActual >= imagenes.Length)
        {
            indiceActual = 0;
        }

        MostrarImagen(indiceActual);
    }

    public void MostrarImagen(int indice)
    {
        // Apaga todas
        for (int i = 0; i < imagenes.Length; i++)
        {
            imagenes[i].SetActive(false);
        }

        // Enciende solo la actual
        imagenes[indice].SetActive(true);
    }
}
