
using System.Security.Permissions;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
public class controles_animacion : UdonSharpBehaviour
{
    public Animator control;
    public string inicio = "Boton";

    public GameObject pantallaTutorial;

    public Material color;
    public Material colorinicial;

    public Renderer botonpulsado;

    private bool activarupdateboton= false;

    public override void Interact()
    {
        control.SetTrigger(inicio);
        cambiocolorboton();
        if (pantallaTutorial != null)
        {
            activarupdateboton = true;
            pantallaTutorial.SetActive(true);
            SendCustomEventDelayedSeconds(nameof(Pantalla), 10f);

        }
       


    }
    public void Pantalla()
    {
        pantallaTutorial.SetActive(false);
        activarupdateboton = false;
        botonpulsado.sharedMaterial = colorinicial;
    }
    private void Update()
    {

        if (activarupdateboton)
        {
            Debug.Log("cambio de color realizado2");
            AnimatorStateInfo state = control.GetCurrentAnimatorStateInfo(0);

            if (state.IsName(inicio) && state.normalizedTime >= 1f)
            {

                cambiocolorboton();

                Debug.Log("cambio de color realizado");


            }
        }
        
    }

    public void cambiocolorboton()
    {
        
            botonpulsado.sharedMaterial = color;
        
        
        
    }

}
