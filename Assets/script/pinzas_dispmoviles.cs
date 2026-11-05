
using JetBrains.Annotations;
//using System.Drawing.Text;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class pinzas_dispmoviles : UdonSharpBehaviour
{
    

    public Animator animador;
    public string triggerAtrapar = "atraparobjeto";
    public string triggerSoltar = "soltarobjeto";
    public string triggerAbrir = "abrirpinzas";
    public string triggerCerrar = "cerrarpinzas";
    

     
    

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
        if (agarre != null)
        {
            
            animador.SetTrigger(triggerAtrapar);
            // Guardamos una referencia al objeto que vamos a agarrar
            objetoAtrapado = other.gameObject;

            // Lo hacemos hijo de la zona de agarre (así seguirá su posición y rotación)
            // El segundo parámetro 'false' hace que adopte la posición local de la zona de agarre
            objetoAtrapado.transform.SetParent(zonaAgarre, false);
            objetoAtrapado.transform.localPosition = Vector3.zero;
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


    
    private void Start()
    {
        animador.SetTrigger(triggerAbrir);
    }
}
