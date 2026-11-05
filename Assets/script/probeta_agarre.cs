
using UdonSharp;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;

public class probeta_agarre : UdonSharpBehaviour
{

    public Collider horno;
    public Collider final;
    public GameObject final2;
    public GameObject horno2;
   

   

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == horno2 )
        {

            horno.enabled = true;

        }
        if (other.gameObject == final2 )
        {

            final.enabled = true;

        }
    }


}
