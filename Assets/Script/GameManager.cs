using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class GameManager : MonoBehaviour
{
    public bool banderaEnSpawn = true;
    
    // Start is called before the first frame update
    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("Coche1", new Vector3(0, 0, 0), Quaternion.identity);
        else
            PhotonNetwork.Instantiate("Coche2", new Vector3(0, 1, 0), Quaternion.identity);
        banderaEnSpawn = true;
    }


    public void banderaSpawnCambio()
    {
        banderaEnSpawn = false;
    }
    
    
}
