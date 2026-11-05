
//using System.Drawing.Text;
using UdonSharp;
using UnityEngine;
using UnityEngine.Video;
using VRC.SDKBase;
using VRC.Udon;

public class prender_horno : UdonSharpBehaviour
{
    [Header("Luces o grupos de luces a controlar")]
    public GameObject[] luces;

    [Header("Estado inicial de las luces")]
    public bool encendidas = false;

    [Header("Tiempo que tarda en prender o apagar (segundos)")]
    public float duracionTransicion = 2f;

    [Header("Intensidad máxima cuando están encendidas")]
    public float intensidadMaxima = 2f;

    [Header("Parte visual del botón (opcional)")]
    public Transform botonVisual;
    public Vector3 desplazamientoPresionado = new Vector3(0, -0.0002f, 0);

    // Variables internas
    private bool enTransicion = false;
    private float progreso = 0f;
    private bool objetivoEncender = false;

    private bool estado = false;
    public Display_Temp secuencia;

    public AudioSource PrenderHorno;

    void Start()
    {
        // Configurar luces según su estado inicial
        foreach (GameObject luzObj in luces)
        {
            if (luzObj == null) continue;

            Light lightComp = luzObj.GetComponent<Light>();
            if (lightComp != null)
                lightComp.intensity = encendidas ? intensidadMaxima : 0f;

            luzObj.SetActive(encendidas);
        }
    }

    public override void Interact()
    {
        PrenderHorno.Play();
        PrenderApagarHorno();
      
        

        if (enTransicion) return;

        objetivoEncender = !encendidas;
        enTransicion = true;
        progreso = 0f;

        // Activar las luces antes de iniciar el encendido
        if (objetivoEncender)
        {
            foreach (GameObject luzObj in luces)
            {
                if (luzObj != null)
                    luzObj.SetActive(true);
            }
        }

        // Movimiento visual del botón (opcional)
        if (botonVisual != null)
            botonVisual.localPosition = objetivoEncender ? desplazamientoPresionado : Vector3.zero;


       
        

    }

    void Update()
    {
        if (!enTransicion) return;

        progreso += Time.deltaTime / duracionTransicion;
        if (progreso > 1f) progreso = 1f;

        float intensidadActual;
        if (objetivoEncender)
        {
            intensidadActual = Mathf.Lerp(0f, intensidadMaxima, progreso);
            
        }
           
            
        else

            intensidadActual = Mathf.Lerp(intensidadMaxima, 0f, progreso);
           

        foreach (GameObject luzObj in luces)
        {
            if (luzObj == null) continue;

            Light lightComp = luzObj.GetComponent<Light>();
            if (lightComp != null)
                lightComp.intensity = intensidadActual;

            Renderer rend = luzObj.GetComponent<Renderer>();
            if (rend != null && rend.material.HasProperty("_EmissionColor"))
            {
                Color baseColor = Color.white;
                Color emisionColor = baseColor * (intensidadActual / intensidadMaxima);
                rend.material.SetColor("_EmissionColor", emisionColor);
                if (intensidadActual > 0.01f)
                    rend.material.EnableKeyword("_EMISSION");
                else
                    rend.material.DisableKeyword("_EMISSION");
            }
        }

        // Final de la transición
        if (progreso >= 1f)
        {
            enTransicion = false;
            encendidas = objetivoEncender;
            Pruebadefuncionluces();
           
            

            // Si se apagaron, las desactivamos completamente
            if (!encendidas)
            {
               
                foreach (GameObject luzObj in luces)
                {
                    if (luzObj != null)
                        luzObj.SetActive(false);
                        
                }
                
            }
        }
    }
    private bool prendido =false;
    public void PrenderApagarHorno()
    { 
        if(prendido == true && encendidas == true)
        {
            secuencia.IniciarSecuenciaReversa();
            prendido = false;
        }
        if(prendido == false && encendidas == false)
        {
            secuencia.IniciarSecuencia();
            prendido = true;
        }
    }

    public void Pruebadefuncionluces()
    {
        if (encendidas == true)
            Debug.Log("luces prendidas");
    }

        
    

    

    



}
