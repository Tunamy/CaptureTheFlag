using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System;
using System.Reflection;
using Photon.Pun.UtilityScripts;

public class GameManager : MonoBehaviour
{


    public static int bandera;
    public TextMeshProUGUI CuentaAtras;
    public float segundos;
    public GameObject contador;

    public PhotonView pv;

    public TextMeshProUGUI[] score;
    
    public Transform[] spawns;

    public string[] coches;
    public string[] escenarios;

    Player[] allPlayers;

    int minumerospawn;
    



    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {

        allPlayers = PhotonNetwork.PlayerList;

        for(int i = 0; i < allPlayers.Length; i++)
        {
            if (allPlayers[i] == PhotonNetwork.LocalPlayer)
            {
                PhotonNetwork.LocalPlayer.SetScore(1);
                int cochealeatorio = UnityEngine.Random.Range(0, coches.Length);
                PhotonNetwork.Instantiate(coches[cochealeatorio], spawns[i].position, spawns[i].rotation);
            }
        }

        ActualizarPuntuacion();
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    if (player != PhotonNetwork.LocalPlayer)
        //    {
        //        minumerospawn++;
        //    }

        //    if (player.IsLocal)
        //    {
        //        int cochealeatorio = UnityEngine.Random.Range(0, coches.Length);
        //        PhotonNetwork.Instantiate(coches[cochealeatorio], spawns[minumerospawn].position, spawns[minumerospawn].rotation);
        //    }

        //}



        //foreach (Player player in PhotonNetwork.PlayerList)
        //{

        //    if (player.IsLocal) 
        //    {
        //        int cochealeatorio = UnityEngine.Random.Range(0, coches.Length);
        //        PhotonNetwork.Instantiate(coches[cochealeatorio], spawns[0].position, spawns[0].rotation);
        //    }

        //}

        //int index = 0;
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{


        //    if (index <= PhotonNetwork.PlayerList.Length)
        //    {


        //        int cochealeatorio = UnityEngine.Random.Range(0, coches.Length);

        //        PhotonNetwork.Instantiate(coches[cochealeatorio], spawns[index].position, spawns[index].rotation);


        //    }
        //    index++;
        //}


        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //{

        //    Photon.Realtime.Player player = PhotonNetwork.PlayerList[i];

        //    int cochealeatorio = UnityEngine.Random.Range(0, coches.Length);

        //    PhotonNetwork.Instantiate(coches[cochealeatorio], spawns[i].position, spawns[i].rotation);

        //}






        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("InstanciarBanderas", 5f);
            int numeroaleatorio = UnityEngine.Random.Range(0, escenarios.Length);
            PhotonNetwork.Instantiate(escenarios[numeroaleatorio], new Vector3(0,0,0), Quaternion.identity);
        }
            
       
           

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

            Invoke("ActualizarPuntuacion", 0.3f);
            

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

    public void ActualizarPuntuacion()
    {
        
            int index = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                // Update the score text for each player

               

                    score[index].text = player.NickName + ": " + player.GetScore();


                
                index++;
            }
        
    }

    

    
  



    


}
