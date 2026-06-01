
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class reaparecer_objeto : UdonSharpBehaviour
{
    public GameObject objetoARespawnear;
    public Transform puntoRespawn;

    public void OnTriggerExit(Collider other)
    {
        if (objetoARespawnear == null || puntoRespawn == null) return;

        if (other.gameObject == objetoARespawnear)
        {
            SendCustomEventDelayedSeconds(nameof(Respawn), 1f);
        }
    }

    public void Respawn()
    {
        objetoARespawnear.transform.position = puntoRespawn.position;
    }
}
