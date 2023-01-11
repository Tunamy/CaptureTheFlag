using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{


    public static int bandera;
    public TextMeshProUGUI CuentaAtras;
    public float segundos;
    public GameObject contador;

    public PhotonView pv;

    

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {


        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Coche1", new Vector3(0, 0, 0), Quaternion.identity);
            Invoke("InstanciarBanderas", 5f);
        }
            
        else
            PhotonNetwork.Instantiate("Coche2", new Vector3(0, 1, 0), Quaternion.identity);

        
        

    }

    public void Update()
    {
        segundos -= Time.deltaTime;
        if(segundos < 0) 
        { 
            contador.SetActive(false);
            segundos= 0;
        }
        CuentaAtras.text = Mathf.Round(segundos).ToString();


        if (CocheControl.noHayBanderas == true)
        {
            segundos = 3;
            contador.SetActive(true);
            Invoke("InstanciarBanderas", 3f);
            CocheControl.noHayBanderas = false;

        }
    }

    public void InstanciarBanderas()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            float numeroaleatorio1 = UnityEngine.Random.Range(-4.1f, 4.1f);
            float numeroaleatorio2 = UnityEngine.Random.Range(-8.1f, 8.1f);
            float numeroaleatorio3 = UnityEngine.Random.Range(-4.1f, 4.1f);
            float numeroaleatorio4 = UnityEngine.Random.Range(-8.1f, 8.1f);

            PhotonNetwork.Instantiate("Bandera", new Vector3(numeroaleatorio2, numeroaleatorio1, 0), Quaternion.identity);
            PhotonNetwork.Instantiate("Meta", new Vector3(numeroaleatorio4, numeroaleatorio3, 0), Quaternion.identity);

            CocheControl.noHayBanderas = false;
        }

    }

    
  



    


}
