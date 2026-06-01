
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class abrir_horno_dipmoviles : UdonSharpBehaviour
{

    [Header("Animator de la puerta")]
    public Animator anim;
    public string triggerAbrir = "ABRIR";
    public string triggerCerrar = "CERRAR";

    [Header("Movimiento Manual (Opcional)")]
    public Transform puerta;
    public Vector3 rotacionAbierta = new Vector3(0f, 110f, 0f);
    public float velocidad = 2f;

    [Header("Audio")]
    public AudioSource fuenteAudio;

    private Quaternion rotacionInicial;
    private Quaternion rotacionFinal;

    private bool abierta = false;
    private bool girando = false;

    void Start()
    {
        if (puerta == null) puerta = this.transform;

        rotacionInicial = puerta.localRotation;
        rotacionFinal = Quaternion.Euler(rotacionAbierta) * rotacionInicial;
    }

    public override void Interact()
    {
        if (girando) return;

        girando = true;
        abierta = !abierta;

        // Sonido
        if (fuenteAudio != null)
            fuenteAudio.Play();

        // Animación
        if (anim != null)
        {
            if (abierta)
                anim.SetTrigger(triggerAbrir);
            else
                anim.SetTrigger(triggerCerrar);
        }

        // Movimiento suave (compatible Quest/Mobile)
        SendCustomEventDelayedFrames(nameof(MoverPuerta), 5);
    }

    public void MoverPuerta()
    {
        Quaternion objetivo = abierta ? rotacionFinal : rotacionInicial;

        puerta.localRotation = Quaternion.Lerp(
            puerta.localRotation,
            objetivo,
            velocidad * 0.1f     // Ajuste estable para Mobile
        );

        // Si todavía no llega a la rotación objetivo, seguir moviendo
        if (Quaternion.Angle(puerta.localRotation, objetivo) > 0.5f)
        {
            SendCustomEventDelayedFrames(nameof(MoverPuerta), 1);
        }
        else
        {
            puerta.localRotation = objetivo;
            girando = false;
        }
    }
}
