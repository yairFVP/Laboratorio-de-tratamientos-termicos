
using UdonSharp;
using UnityEngine;
using UnityEngine.Rendering;
using VRC.SDKBase;
using VRC.Udon;

public class cerrar_puerta : UdonSharpBehaviour
{

    public Animator abrir;
    public string triggerAbrir = "ABRIR";
    public string triggerCerrar = "CERRAR";

    public Transform puerta;          // El objeto de la puerta
    public Vector3 rotacionAbierta = new Vector3(0, 180, 0); // Grados para abrir
    public float velocidad = 2f;      // Velocidad de rotación

    private Quaternion rotacionInicial;
    private Quaternion rotacionFinal;
    public bool abierta = false;
    private bool girando = false;

    public AudioSource AudioAbrir;
    public AudioSource AudioCerrar;
   
    public temporizador temporizador;

    void Start()
    {
        if (puerta == null) puerta = this.transform;//NO ES NECESARIO
        rotacionInicial = puerta.localRotation;
        rotacionFinal = Quaternion.Euler(rotacionAbierta) * rotacionInicial;
        AudioAbrir.Stop();
        AudioCerrar.Stop();

    }

    public override void Interact()
    {
        
        if (abierta==false)
        {
            AudioAbrir.Play();
            abrir.SetTrigger(triggerAbrir);
            



        }
        else
        {
            SendCustomEventDelayedSeconds(nameof(GirarPuerta), 0);
           
        }






        if (!girando)
        {
            abierta = !abierta;
            SendCustomEventDelayedFrames(nameof(GirarPuerta), 150);//RETARDO PARA SONIDO
            abrir.SetTrigger(triggerCerrar);
            AudioCerrar.Play();
            

        }
    }

    public void GirarPuerta()
    {
        girando = true;

        Quaternion objetivo = abierta ? rotacionFinal : rotacionInicial;
        puerta.localRotation = Quaternion.Lerp(puerta.localRotation, objetivo, Time.deltaTime * velocidad);

        if (Quaternion.Angle(puerta.localRotation, objetivo) > 0.1f)
        {
            SendCustomEventDelayedFrames(nameof(GirarPuerta), 1);
        }
        else
        {
            puerta.localRotation = objetivo;
            girando = false;
        }
    }

    
}
