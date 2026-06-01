
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class tomar_probeta : UdonSharpBehaviour
{
    /*public Animator animador;
    public string triggerAtrapar = "atraparobjeto";
    public string triggerSoltar = "soltarobjeto";
    public string triggerAbrir = "abrirpinzas";
    public string triggerCerrar = "cerrarpinzas";
    private bool abierta = true;


    // Zona donde se colocará el objeto al ser agarrado (por ejemplo, una mano o punto de agarre)
    public Transform zonaAgarre;

    // Referencia al objeto actualmente atrapado (si no hay, será null)
    private GameObject objetoAtrapado;

    // Este método se llama automáticamente cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {

        
        // Si ya hay un objeto atrapado, no hacemos nada (evita agarrar dos objetos al mismo tiempo)
        if (objetoAtrapado != null) return;

        // Intentamos obtener el componente "agarrable" del objeto que entró
        // (solo los objetos con este componente pueden ser agarrados)
        probeta_agarre agarre = other.GetComponent<probeta_agarre>();

        // Si el objeto tiene el componente "agarrable", procedemos a atraparlo
        if (agarre != null && abierta==true)//CAMBIO 120326
        {
            animador.SetTrigger(triggerAtrapar);
            // Guardamos una referencia al objeto que vamos a agarrar
            objetoAtrapado = other.gameObject;

            // Lo hacemos hijo de la zona de agarre (así seguirá su posición y rotación)
            // El segundo parámetro 'false' hace que adopte la posición local de la zona de agarre
            objetoAtrapado.transform.SetParent(zonaAgarre, true);
            objetoAtrapado.transform.localPosition = Vector3.zero   ;
            //objetoAtrapado.transform.localRotation = Quaternion.identity;
            //objetoAtrapado.transform.localScale = Vector3.one;

            // Buscamos si el objeto tiene un Rigidbody (para manipular su física)
            Rigidbody rb = objetoAtrapado.GetComponent<Rigidbody>();



            // Si tiene Rigidbody, lo volvemos cinemático y reseteamos sus velocidades
            if (rb != null)
            {
                // Lo volvemos cinemático para que la física no lo mueva
                rb.isKinematic = true;

                // Cancelamos su movimiento actual (por si venía volando o cayendo)
                rb.velocity = Vector3.zero;

                // Cancelamos su rotación actual
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    // Método público para soltar el objeto atrapado
    public void SoltarObjeto()
    {
        // Solo soltamos si efectivamente tenemos un objeto atrapado
        if (objetoAtrapado != null)
        {
            animador.SetTrigger(triggerSoltar);
            // Lo separamos de la zona de agarre (ya no será hijo de ella)
            objetoAtrapado.transform.SetParent(null);

            // Buscamos su Rigidbody para reactivar la física
            Rigidbody rb = objetoAtrapado.GetComponent<Rigidbody>();

            // Si tiene Rigidbody, lo hacemos no cinemático (vuelve a comportarse físicamente)
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // Borramos la referencia al objeto (ya no hay nada agarrado)
            objetoAtrapado = null;
        }
    }

    // Este método sobrescribe (override) el método "Interact" de una clase base
    // y se ejecuta cuando el jugador u otro sistema interactúa con este objeto
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

        
        // Al interactuar, soltamos el objeto si hay alguno agarrado
        SoltarObjeto();
    }*/
    public Animator animador;

    public string triggerAtrapar = "atraparobjeto";
    public string triggerSoltar = "soltarobjeto";
    public string triggerAbrir = "abrirpinzas";
    public string triggerCerrar = "cerrarpinzas";

    public Transform zonaAgarre;

    private GameObject objetoAtrapado;

    [UdonSynced] private bool abierta = true;
    [UdonSynced] private bool tieneObjeto = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        if (objetoAtrapado != null) return;

        probeta_agarre agarre = other.GetComponent<probeta_agarre>();

        if (agarre != null && abierta == true)
        {
            Networking.SetOwner(Networking.LocalPlayer, other.gameObject);

            objetoAtrapado = other.gameObject;
            tieneObjeto = true;

            AtraparObjetoLocal();

            RequestSerialization();
        }
    }

    public void SoltarObjeto()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        if (objetoAtrapado != null)
        {
            tieneObjeto = false;

            SoltarObjetoLocal();

            RequestSerialization();
        }
    }

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);

        if (abierta)
        {
            abierta = false;
            animador.SetTrigger(triggerCerrar);
        }
        else
        {
            abierta = true;
            animador.SetTrigger(triggerAbrir);
        }

        SoltarObjeto();

        RequestSerialization();
    }

    public override void OnDeserialization()
    {
        if (abierta)
        {
            animador.SetTrigger(triggerAbrir);
        }
        else
        {
            animador.SetTrigger(triggerCerrar);
        }

        if (tieneObjeto)
        {
            AtraparObjetoLocal();
        }
        else
        {
            SoltarObjetoLocal();
        }
    }

    private void AtraparObjetoLocal()
    {
        if (objetoAtrapado == null) return;

        animador.SetTrigger(triggerAtrapar);

        objetoAtrapado.transform.SetParent(zonaAgarre, true);
        objetoAtrapado.transform.localPosition = Vector3.zero;

        Rigidbody rb = objetoAtrapado.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void SoltarObjetoLocal()
    {
        if (objetoAtrapado == null) return;

        animador.SetTrigger(triggerSoltar);

        objetoAtrapado.transform.SetParent(null);

        Rigidbody rb = objetoAtrapado.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        objetoAtrapado = null;
    }

}
