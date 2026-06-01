
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class puertaabrircerrar : UdonSharpBehaviour
{
    public Animator doorAnimator;

    [UdonSynced] private bool isOpen;

    private float lastUse;
    public float cooldown = 1f;

    public override void Interact()
    {


        Networking.SetOwner(Networking.LocalPlayer, gameObject);

        if (Time.time - lastUse < cooldown) return;
        lastUse = Time.time;

        if (doorAnimator == null) return;

        isOpen = !isOpen;

        AplicarAnimacion();

        RequestSerialization();
    }

    public override void OnDeserialization()
    {
        AplicarAnimacion();
    }

    private void AplicarAnimacion()
    {
        doorAnimator.ResetTrigger("abrir");
        doorAnimator.ResetTrigger("cerrar");

        if (isOpen)
        {
            doorAnimator.SetTrigger("abrir");
        }
        else
        {
            doorAnimator.SetTrigger("cerrar");
        }
    }
}
