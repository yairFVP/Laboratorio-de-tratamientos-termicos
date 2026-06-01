
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GIROVENTILADORRES : UdonSharpBehaviour
{
    public Transform[] aspas;
    public float rotacion = 200f;
    void Start()
    {
        foreach (Transform aspa in aspas)
        {
            aspa.Rotate(0f, 0f, rotacion * Time.deltaTime, Space.Self);
        }
    }
}
