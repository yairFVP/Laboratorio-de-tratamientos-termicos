
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AyCpinzas : UdonSharpBehaviour
{
    /*[Header("Animator principal de las pinzas")]
    public Animator animator;

    [Header("Collider de las puntas (referencia desde el Inspector)")]
    public Collider colliderPuntas;

    [Header("Nombre de los Triggers en el Animator")]
    public string triggerCerrar = "Cerrar";
    public string triggerAtrapado = "Atrapado";

    private bool objetoAtrapado = false;

    // 🔹 Se ejecuta cuando se presiona el botón “Use” (por defecto el trigger del mando)
    public override void OnPickupUseDown()
    {
        // Reproduce la animación de cerrar
        if (animator != null)
        {
            animator.SetTrigger(triggerCerrar);
        }
    }

    // 🔹 Detecta cuando algo entra en el collider de las puntas
    private void OnTriggerEnter(Collider other)
    {
        // Solo se ejecuta si no hay ya un objeto atrapado
        if (!objetoAtrapado && other.gameObject != gameObject)
        {
            objetoAtrapado = true;

            // Reproduce animación de atrapar
            if (animator != null)
            {
                animator.SetTrigger(triggerAtrapado);
            }
        }
    }

    // 🔹 Cuando el objeto deja de tocar las puntas, se libera
    private void OnTriggerExit(Collider other)
    {
        if (objetoAtrapado)
        {
            objetoAtrapado = false;
        }
    }
    /* [Header("Componentes principales")]
     public Animator anim;                    // Animator de las pinzas
     public string triggerCerrar = "Cerrar";  // Nombre del Trigger al presionar el trigger del control
     public string triggerAtrapado = "Atrapado"; // Trigger cuando se detecta un objeto entre las puntas

     [Header("Movimiento físico opcional")]
     public Transform ladoPinza;              // Lado móvil de la pinza
     public Vector3 posicionCerrada;          // Posición local del lado cuando se cierra
     public float velocidad = 5f;             // Velocidad del movimiento

     private Vector3 posicionInicial;         // Posición original
     private bool apretando = false;          // Si se está presionando el trigger
     private bool objetoAtrapado = false;     // Si hay algo entre las puntas

     void Start()
     {
         if (ladoPinza != null)
             posicionInicial = ladoPinza.localPosition;
     }

     // Se ejecuta cuando el jugador presiona el trigger mientras sostiene las pinzas
     public override void OnPickupUseDown()
     {
         apretando = true;
         anim.SetTrigger(triggerCerrar); // Activa animación de cierre
     }

     // Cuando el jugador suelta el trigger
     public override void OnPickupUseUp()
     {
         apretando = false;
         objetoAtrapado = false; // Liberamos el estado de atrapado
     }

     void Update()
     {
         // Movimiento visual del lado de las pinzas
         if (ladoPinza != null)
         {
             Vector3 destino = apretando ? posicionCerrada : posicionInicial;
             ladoPinza.localPosition = Vector3.Lerp(ladoPinza.localPosition, destino, Time.deltaTime * velocidad);
         }
     }

     // ESTE SCRIPT DEBE IR EN EL OBJETO PRINCIPAL DE LAS PINZAS
     // Y UN COLLIDER HIJO (en las puntas) TENDRÁ ESTE MISMO SCRIPT REFERENCIADO
     private void OnTriggerStay(Collider other)
     {
         // Si las pinzas están cerrando y detectan algo entre sus puntas
         if (apretando && !objetoAtrapado)
         {
             // Ignora colisiones con el propio jugador


             objetoAtrapado = true;
             anim.SetTrigger(triggerAtrapado); // Activa animación de “atrapado”
             Debug.Log("Objeto atrapado: " + other.name);
         }
     }*/
    public Animator animador;
    public string triggerAbrir = "abrirpinzas";
    public string triggerCerrar = "cerrarpinzas";
    


    private bool abierta = true;


    public override void Interact()
    {
        if (abierta)
        {
            animador.SetTrigger(triggerCerrar);
            abierta = false;

        }
        else
        {
            animador.SetTrigger(triggerAbrir);
            abierta = true;
        }



    }
    /*public Animator animPinzas;
    public Transform puntoAtrapado;

    private GameObject objetoAtrapado;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto tiene el componente "Atrapable"
        probeta_agarre esAtrapable = other.GetComponent<probeta_agarre>();

        if (esAtrapable != null)
        {
            animPinzas.SetTrigger("Cerrar");

            objetoAtrapado = other.gameObject;
            objetoAtrapado.transform.SetParent(puntoAtrapado);
            objetoAtrapado.transform.localPosition = Vector3.zero;
            objetoAtrapado.transform.localRotation = Quaternion.identity;

            Rigidbody rb = objetoAtrapado.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objetoAtrapado != null && other.gameObject == objetoAtrapado)
        {
            objetoAtrapado.transform.SetParent(null);

            Rigidbody rb = objetoAtrapado.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            objetoAtrapado = null;

            animPinzas.Play("abrirPinzas");
        }
    }*/


}
