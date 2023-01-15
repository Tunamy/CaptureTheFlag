using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirMeta : MonoBehaviour
{
    public PhotonView mypv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player") && collision.gameObject.GetComponent<CocheControl>().tienesLaBandera == true)
        {
            Debug.Log("chirivita");

            

            mypv.RPC("DestruirObjeto", RpcTarget.All);

        }
    }


    [PunRPC]
    void DestruirObjeto()
    {


        Destroy(gameObject);
        
    }
}
