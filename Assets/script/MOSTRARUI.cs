
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MOSTRARUI : UdonSharpBehaviour
{
    public Animator animador;
    public string nombreAnimacion = "Mostrar";

    public override void Interact()
    {
        if (animador != null)
        {
            animador.SetTrigger(nombreAnimacion);
        }
    }
}
