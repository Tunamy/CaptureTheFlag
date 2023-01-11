using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class CocheControl : MonoBehaviour 
{
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float friccion = 0.5f;
    public float velocidadMaxima = 20f;


    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    //Components
    Rigidbody2D carRigidbody2D;

    

    PhotonView mypv;

    public bool tienesLaBandera;

    public int bandera;

    public static bool noHayBanderas = false;

    
    public GameObject banderaEnCoche;

    public BoxCollider2D boxCollider;

    public GameManager gameManager;
   
    

    private void Awake()
    {
        
            carRigidbody2D = GetComponent<Rigidbody2D>();
            mypv = GetComponent<PhotonView>();
            

            banderaEnCoche.SetActive(false);
            
        
    }


    void FixedUpdate()
    {

        if (GetComponent<PhotonView>().IsMine)
        {
            ApplyEngineForce();
            ApplySteering();
            killorthogonalvelocity();

        }

    }

    void ApplyEngineForce() {

        if (carRigidbody2D.velocity.magnitude > velocidadMaxima)
            return;

        if(accelerationInput == 0) //friccion
        {
            carRigidbody2D.AddForce(-carRigidbody2D.velocity * friccion, ForceMode2D.Force);
        }

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput *accelerationFactor;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }
    void ApplySteering() { //giro

        float velocidadMinimaParaGirar = (carRigidbody2D.velocity.magnitude / 8);
        velocidadMinimaParaGirar = Mathf.Clamp01(velocidadMinimaParaGirar);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * velocidadMinimaParaGirar;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void killorthogonalvelocity() 
    {

        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right* Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

    }

    public void SetInputVector(Vector2 inputVector)  // llega la tecla que pulsamos
    {

        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!mypv.IsMine)
            return;
        

        if (collision.CompareTag("Bandera"))
        {

            bandera = mypv.ViewID;

            tienesLaBandera = (mypv.ViewID == bandera);
           

            mypv.RPC("QuienTieneLaBandera", RpcTarget.All, bandera);


            Debug.Log("tiene la bandera" + bandera);




            



        }


        if (collision.CompareTag("Meta") && tienesLaBandera == true)
        {
            
            bandera = 0;

            tienesLaBandera = (mypv.ViewID == bandera);

           
            mypv.RPC("QuienTieneLaBandera", RpcTarget.All, bandera);

            mypv.RPC("NoHayBanderas", RpcTarget.All);
            
            noHayBanderas = true;
            
            

        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mypv.IsMine)
            return;


        if (collision.gameObject.CompareTag("Player") && tienesLaBandera == true)
        {

            bandera = collision.gameObject.GetComponent<PhotonView>().ViewID;

            PhotonView pvcoll = collision.gameObject.GetComponent<PhotonView>();

            Debug.Log("colisiona");



            pvcoll.RPC("QuienTieneLaBandera", RpcTarget.All, bandera);

            //pvcoll.RPC("CambiarTag", RpcTarget.All);

        }

        if (collision.gameObject.CompareTag("Player") && tienesLaBandera == true)
        {
            bandera = collision.gameObject.GetComponent<PhotonView>().ViewID;
            mypv.RPC("QuienTieneLaBandera", RpcTarget.All, bandera);

            mypv.RPC("CambiarTag", RpcTarget.All);

        }

        
        //gameObject.tag = "Reload";
        //Invoke("EnableTag", 1.2f);


    }

   

    [PunRPC]
    void QuienTieneLaBandera(int bandera)
    {
       

        this.bandera = bandera;
        tienesLaBandera = (mypv.ViewID == bandera);
        
        if (tienesLaBandera)
        {
            banderaEnCoche.SetActive(true);
        }
        else
        {
            banderaEnCoche.SetActive(false);
        }

        

        Debug.Log("Se ha actualizado la bandera a " + bandera + " en el cliente " + mypv.ViewID);

    }

    [PunRPC]
    void NoHayBanderas()
    {
        noHayBanderas = true;
    }

    [PunRPC]
    void CambiarTag()
    {
        gameObject.tag = "Reload";
        Invoke("EnableTag", 1.2f);
    }

   

    public void EnableTag() 
    {
        gameObject.tag = "Player";
        
    }

    



}





