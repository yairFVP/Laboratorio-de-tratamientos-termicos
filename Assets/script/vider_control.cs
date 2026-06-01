
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Video.Components.AVPro;
using VRC.SDKBase;
using VRC.Udon;

public class vider_control : UdonSharpBehaviour
{
    /*public VRCAVProVideoPlayer videoPlayer;
    public VRCUrl videoURL;  //Aquí en lugar de string

    public override void Interact()
    {
        if (videoPlayer != null && videoURL != null)
        {
            videoPlayer.PlayURL(videoURL);
        }
    }*/


    public GameObject pantalla;

    public override void Interact()
    {
        pantalla.SetActive(true);
    }
}
