
using UdonSharp;
using UnityEngine;
using UnityEngine.Rendering;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Exceptions;

public class cambio_color : UdonSharpBehaviour
{

    public Animator transicionColor;//INTERMEDIO
    public string estadorojovivo = "primer";

    public Renderer objetoObjetivoRenderer;

    public Material TexturaHorno;
    public Material TexturaFinal;

    public bool retrasoActivo = false;

    public temporizador cambiocolor;
    public giro_probeta transicion;
    public cerrar_puerta cerrar_Puerta;

    public bool marcador = false;
    public bool activamicroestructura=false;
    public bool activartapa=false; 
   



    public prender_horno hornoprendido;
    public bool objeto_dentro_horno = false;

 
    public MeshRenderer microestructura; 
    public GameObject tapa_desactivar;

    public GameObject probetaAgua;
    public GameObject probetaAceite;
    public GameObject probetaSalmuera;
    
    
    public GameObject probeta_activar;
    public GameObject probetaotra;
    public GameObject portal;
  
    public VRC_Pickup pickup;


    void Start()
    {
      
        probeta_activar.SetActive(false);
        probetaotra.SetActive(false);
        microestructura.enabled = false;
        pickup.pickupable = false;
        portal.SetActive(false);
        
    }
    

    void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.name == "basehorno")
        {
           
            transicionColor.enabled = false;
            
            cambiocolor.temp.SetBool(cambiocolor.banderafin, false);
            if (hornoprendido.encendidas == true && cerrar_Puerta.abierta == false)
            {

                cambiocolor.collidertemp.enabled = true;
                if (cambiocolor.estado == true /*&& cerrar_Puerta.abierta==false*/)
                {
                    //cambiocolor.FinAnimacion();
                    CambiarColor();
                    Debug.Log("cambio de color");


                }
                Debug.Log("OBJETO DENTRO");
                
            }

        }
        
       
        else if (other.gameObject.name == "Fluido")
        {
            
            transicionColor.enabled = true;
            transicionColor.SetTrigger("AnimtransicionInicio");
            if ( transicion.terminado==true)
            {
                
                transicionColor.enabled = false;
                objetoObjetivoRenderer.sharedMaterial = TexturaFinal;
                microestructura.enabled=true;

            }

        }
       
    }

    public void CambiarColor()
    {
       
        if (objetoObjetivoRenderer != null )
        {
            objetoObjetivoRenderer.sharedMaterial = TexturaHorno;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "basehorno")
        {
          
            Debug.Log("OBJETO FUERA");
            
            cambiocolor.temp.SetBool(cambiocolor.banderafin, true);
            cambiocolor.collidertemp.enabled = false;
            cambiocolor.estado = false;
            cambiocolor.activarupdate = false;

            tapa_desactivar.SetActive(false);

        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "basefinal")
        {
            if (objetoObjetivoRenderer.sharedMaterial == TexturaFinal)
            {
                tapa_desactivar.SetActive(true);

                SendCustomEventDelayedSeconds(nameof(RetardoProbeta), 3f);

                microestructura.enabled = true;
               pickup.pickupable     = true;
                portal.gameObject.SetActive(true);


            }
        }
    }

    public void RetardoProbeta()
    {
        probeta_activar.SetActive(true);
    }
}
