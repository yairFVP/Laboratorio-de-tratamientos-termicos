
using Cysharp.Threading.Tasks.Triggers;
using System.Resources;
using System.Security.Permissions;
using UdonSharp;

using UnityEngine;
using UnityEngine.Rendering;
using VRC.SDKBase;
using VRC.Udon;

public class temporizador : UdonSharpBehaviour
{

    public Animator temp;//control de animacion del temporizador

    public string inicio = "inicio"; // parametro del inicio de la animacion temporizador
    public AudioSource audiocronometro;

    public bool banderacambio = false; //bandera para activar y desactivar collider de objetos
    public cambio_color cambio; // Referencia a script 

    public Collider colliderInteract; //Referenica a la manija
    public Collider Boton; //Referencia al boton ON/OFF del horno
    public Collider collidertemp; //Referencia al temporizador
    public cerrar_puerta cerrar_Puerta;

    public bool activarupdate = false;
    public string nombreEstado = "Fin"; // nombre de la animacion del temporizador
    public string banderafin = "banderafinanimacion";// bandera de cuando termina la animacion del temporizador

    public override void Interact()
    {

        IniciarAnimacion();

    }
    public void Start()
    {
        collidertemp.enabled = false;
    }

    public void ActivarCollider()
    {
       

        colliderInteract.enabled = true;
        Boton.enabled = true;
        collidertemp.enabled = true;
        
    }

    public void DesactivarCollider()
    {
        colliderInteract.enabled = false;
        Boton.enabled = false;
        collidertemp.enabled = false;
    }

    public void IniciarAnimacion()
    {
        
        audiocronometro.Play();
        temp.SetTrigger(inicio);
        //temp.Play("tiempo", 0, 0f);
        DesactivarCollider();
        activarupdate = true;

    }
    
    void Update()
    {
        if (activarupdate)
        {
            FinAnimacion();
        }
       
      
    }

    public bool estado = false;
    public void FinAnimacion()
    {             
            AnimatorStateInfo state = temp.GetCurrentAnimatorStateInfo(0);

            if (state.IsName(nombreEstado) && state.normalizedTime >=1f )
            {
                estado = true;
                ActivarCollider();
                Debug.Log("Fin animacion");              
            }            
    }
}

