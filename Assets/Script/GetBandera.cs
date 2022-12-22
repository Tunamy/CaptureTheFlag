using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBandera : MonoBehaviour
{
    public bool tienesLaBandera = false;
    //public bool banderaEnSpawn = true;
    //actornumber

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
                gameObject.tag = "tienebandera";
            
                GameManager.banderaEnSpawn = false;
                Debug.Log(GameManager.banderaEnSpawn);
                
               
                collision.GetComponent<GameObject>();
                Destroy(collision.gameObject);

                


        }


            if (collision.CompareTag("Meta") && tienesLaBandera)
            {
                Debug.Log("punto");
                tienesLaBandera= false;
            
                GameManager.banderaEnSpawn = true;
                gameObject.tag = "Player";

                collision.GetComponent<GameObject>();
                Destroy(collision.gameObject);

                //llamar a una funcion que instancie los 2 circulos
            }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        
        
            if (collision.gameObject.CompareTag("tienebandera"))
            {

            if (this.gameObject.tag != "Player")
            {
                Debug.Log("choca" + collision.gameObject);
                Debug.Log("choca" + gameObject);
            }
                //tienesLaBandera = false;
                //gameObject.tag = "tienebandera";
                //collision.gameObject.tag = "Player";

                //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                //collision.gameObject.GetComponent<BoxCollider2D>().enabled=false;

            }

       

            //if (collision.gameObject.CompareTag("Player") && tienesLaBandera == false )
            //{


            //    tienesLaBandera = true;
            // gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //    collision.gameObject.GetComponent<BoxCollider2D>().enabled=false;


            //}


    }




}
