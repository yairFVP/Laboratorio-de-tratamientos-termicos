
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleSystemfuego : UdonSharpBehaviour
{
    public ParticleSystem fuego;
    public GameObject[] probetasefectos;
    
    




    public void OnTriggerEnter(Collider other)
    {
        

        for (int i = 0; i < probetasefectos.Length; i++)
        {
            if (other.gameObject == probetasefectos[i])
            {
                fuego.Play();
                SendCustomEventDelayedSeconds(nameof(Apagar), 3f);
                break;
            }
        }
    }

    public void Apagar()
    {
        fuego.Stop();
    }

    
}
