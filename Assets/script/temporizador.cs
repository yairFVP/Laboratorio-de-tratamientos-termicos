
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
        collidertemp.enabled = false;//Collider del temporizador inicia desactivado para evitar interactuar con el
    }

    public void ActivarCollider()//
    {
       

        colliderInteract.enabled = true;
        Boton.enabled = true;
        //collidertemp.enabled = true;
        
    }
    public bool tempoactivado = false;//activa el collider del temporizador cuando
                                      //la probeta esta dentro del horno y se cumplen
                                      //las condiciones del script cambio_color

    public void DesactivarCollider()
    {
        colliderInteract.enabled = false;
        Boton.enabled = false;
        /*if (tempoactivado)
        {
            collidertemp.enabled = false;
        }*/
        tempoactivado = true;
      
        
        
    }

    public void IniciarAnimacion()
    {

        
        audiocronometro.Play();
        temp.SetTrigger(inicio);
        DesactivarCollider();
        
        activarupdate = true;

    }
    
    void Update()
    {

        if (activarupdate)
        {
            Debug.Log("inicio update");
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
                tempoactivado = false;
                Debug.Log("Fin animacion");
                activarupdate = false;//modi1
                
            }            
    }
}

