
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Video.Components.AVPro;
using VRC.SDK3.Video.Components.Base;
using VRC.SDKBase;
using VRC.Udon;

public class prender_tele : UdonSharpBehaviour
{

    public GameObject canvasvideo;

    public GameObject pantalla;

    public VRCAVProVideoPlayer reproductor;
    public bool reproducir = false;
        

    void Start()
    {
        //PararVideo();
        //canvasvideo.SetActive(false);
        
    }

    

    public override void Interact()
    {
       

        canvasvideo.SetActive(false);
        if (reproducir == true)
        {

            PararVideo();
        }
        else
        {
            Reproducirvideo();
            
        }
    }

    
    

    public void Reproducirvideo()
    {
        reproducir = true;
        
        reproductor.Play();
        
    }
    public void PararVideo()
    {
        reproducir = false;
        reproductor.Pause();
    }
      
}
