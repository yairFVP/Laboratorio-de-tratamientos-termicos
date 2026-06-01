
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class fix_joint : UdonSharpBehaviour
{
    public FixedJoint joint;
    private Rigidbody objeto;

    public void OnTriggerEnter(Collider other)
    {
        if (joint.connectedBody != null) return;

        Rigidbody rb = other.attachedRigidbody;

        if (rb != null)
        {
            objeto = rb;
            joint.connectedBody = objeto;
        }
    }

    public void Soltar()
    {
        joint.connectedBody = null;
        objeto = null;
    }
    public override void Interact()
    {
        Soltar();
    }
}
