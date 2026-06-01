
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class activarDona : UdonSharpBehaviour
{
    public GameObject objetoADesaparecer;
    public GameObject objetoPermitido; // ej. probeta

    public float tiempoNecesario = 3f;

    public MeshRenderer rendererObjeto;
    public Material materialNormal;
    public Material materialActivo;

    private float contador = 0f;
    private bool objetoDentro = false;

    public Animator giro;
    public GameObject flecha; 

    void Start()
    {
        rendererObjeto.material = materialNormal;
        //giro.Play();

    }

    void Update()
    {
        if (objetoDentro)
        {
            contador += Time.deltaTime;

            if (contador >= tiempoNecesario)
            {
                objetoADesaparecer.SetActive(false);
                objetoDentro = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objetoPermitido)
        {
            objetoDentro = true;
            contador = 0f;
            rendererObjeto.material = materialActivo;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objetoPermitido)
        {
            objetoDentro = false;
            contador = 0f;
            rendererObjeto.material = materialNormal;
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        
          
    }*/
}
