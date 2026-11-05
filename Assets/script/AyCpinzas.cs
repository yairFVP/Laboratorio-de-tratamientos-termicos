/*Este script controla las animaciones de abrir y cerrar las pinzas*/
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AyCpinzas : UdonSharpBehaviour
{
    public Animator animador; //Animacion de abrir y cerrar pinzas
    public string triggerAbrir = "abrirpinzas"; //referencia al evento a reproducir
    public string triggerCerrar = "cerrarpinzas";
    
    private bool abierta = true; //Bandera que señala cuando la pinza esta abierta o cerrada

    public override void Interact()
    {
        if (abierta)
        {
            animador.SetTrigger(triggerCerrar);
            abierta = false;
        }
        else
        {
            animador.SetTrigger(triggerAbrir);
            abierta = true;
        }
    }
}
