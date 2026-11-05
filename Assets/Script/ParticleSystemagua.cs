
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleSystemagua : UdonSharpBehaviour
{
   public ParticleSystem agua;
   public GameObject[] probetasefectos;
   private bool activado = false;
   public AudioSource fluidosonido;



    public void OnTriggerEnter(Collider other)
    {
        if (activado)
        {
            return;
        }

        for (int i = 0; i < probetasefectos.Length; i++)
        {
            if (other.gameObject == probetasefectos[i])
            {
                activado = true;
                fluidosonido.Play();
                agua.Play();
                SendCustomEventDelayedSeconds(nameof(Apagar), 3f);
                break;
            }
        }
    }

    public void Apagar()
    {
        agua.Stop();
    }
    public void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < probetasefectos.Length; i++)
        {
            if (other.gameObject == probetasefectos[i])
            {
               
                fluidosonido.Stop();
                agua.Stop();
                
                
                break;
            }
        }
    }

}