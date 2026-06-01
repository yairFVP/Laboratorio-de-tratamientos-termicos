
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class fanrotation : UdonSharpBehaviour
{
   public Transform[] aspas;
   public float rotacion=200f;
    public AudioSource audioventilador;


    void Start()
    {
        audioventilador.Play();
      
           
    }
    void Update()
    {
        foreach (Transform aspa in aspas)
        {
            aspa.Rotate(0f, 0f, rotacion * Time.deltaTime, Space.Self);

        }
    }

}
