
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Video.Components.AVPro;
using VRC.SDKBase;
using VRC.Udon;

public class vider_control : UdonSharpBehaviour
{
   


    public GameObject pantalla;

    public override void Interact()
    {
        pantalla.SetActive(true);
    }
}
