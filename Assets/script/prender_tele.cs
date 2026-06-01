
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Video.Components.Base;
using VRC.SDKBase;
using VRC.Udon;

public class prender_tele : UdonSharpBehaviour
{

    public GameObject pantalla;
    void Start()
    {
        PrenderPantalla();
        //videoPlayer.Stop();
    }

    /*public BaseVRCVideoPlayer videoPlayer;
    public VRCUrl videoUrl;*/

    public override void Interact()
    {
        /*videoPlayer.LoadURL(videoUrl);
        videoPlayer.Play();*/
        pantalla.SetActive(true);
        SendCustomEventDelayedSeconds(nameof(PrenderPantalla),108f);
    }

    public void PrenderPantalla()
    {
        pantalla.SetActive(false);
    }
      
}
