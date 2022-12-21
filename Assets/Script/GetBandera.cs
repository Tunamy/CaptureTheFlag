using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBandera : MonoBehaviour
{
    public bool tienesLaBandera = false;
    

    BoxCollider2D colliderCoche;
    GameManager gameMan;
    // Start is called before the first frame update
    void Start()
    {

        colliderCoche= GetComponent<BoxCollider2D>();
        gameMan= GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // if (GetComponent<PhotonView>().IsMine)
        
            if (collision.CompareTag("Bandera"))
            {
                tienesLaBandera = true;

            
                
                collision.GetComponent<GameObject>();
                Destroy(collision.gameObject);

                gameObject.GetComponent<GameManager>().banderaSpawnCambio();


        }


            if (collision.CompareTag("Meta") && tienesLaBandera)
            {
                Debug.Log("punto");
                tienesLaBandera= false;
                
                collision.GetComponent<GameObject>();
                Destroy(collision.gameObject);

                //llamar a una funcion que instancie los 2 circulos
            }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        
            if (collision.gameObject.CompareTag("Player") && tienesLaBandera == true )
            {
                tienesLaBandera = false;
                Debug.Log("pierdes la bandera");
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled=false;

            }

            if (collision.gameObject.CompareTag("Player") && tienesLaBandera == false )
            {


                tienesLaBandera = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled=false;


            }
        
           
    }




}
