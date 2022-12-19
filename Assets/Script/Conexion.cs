using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Conexion : MonoBehaviourPunCallbacks
{
    [Header("Paneles")]

    public GameObject panelPrincipal;
    public GameObject panelCrearRoom;
    public GameObject panelJugadoresRoom;
    public GameObject panelRooms;

    public TextMeshProUGUI textEstado;

    public TMP_InputField inputNickname;
    public TMP_InputField inputRoomName;
    public TMP_InputField inputPrivateRoom;

    public GameObject contenedorRooms;
    public GameObject elemRoom;
    public TextMeshProUGUI textNombreSala;

    public GameObject contenedorJugadores;
    public GameObject elemJugador;




    // Lista Jugadores y salas

    Dictionary<string, RoomInfo> listaSalas;
    ExitGames.Client.Photon.Hashtable propiedadesJugador;



    void Start()
    {
        CambiarPanel(panelPrincipal);

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;

        //Lista de Jugadores y Salas
        propiedadesJugador = new ExitGames.Client.Photon.Hashtable();
        listaSalas = new Dictionary<string, RoomInfo>();
    }

   



    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Estado("Conected to Server");
    }



    public override void OnJoinedRoom()
    {
        Estado("Conected to Room: " + PhotonNetwork.CurrentRoom.Name);

        PhotonNetwork.LocalPlayer.SetCustomProperties(propiedadesJugador);

        ActualizarPanelJugadores();

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Estado("Room no Avalible: " + message);
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ActualizarPanelJugadores();
    }

    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ActualizarPanelJugadores();
    }

    public void AbandonarSala()
    {
        PhotonNetwork.LeaveRoom();
        Estado("Conected to Server");
    }








    public void CambiarPanel(GameObject nombre)
    {
        panelCrearRoom.SetActive(false);
        panelPrincipal.SetActive(false);
        panelJugadoresRoom.SetActive(false);
        panelRooms.SetActive(false);

        nombre.SetActive(true);
    }




    public void ActualizarPanelJugadores()
    {
        PhotonNetwork.NickName = inputNickname.text;

        textNombreSala.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name + "       " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
        

        // Eliminamos los jugadores de la lista
        while (contenedorJugadores.transform.childCount > 0)
        {
            DestroyImmediate(contenedorJugadores.transform.GetChild(0).gameObject);
            
        }

        foreach (Photon.Realtime.Player jugador in PhotonNetwork.PlayerList)
        {
            //instanciamos un boton  y lo colocamos en el contenedor    

            GameObject nuevoElemento = Instantiate(elemJugador);
            nuevoElemento.transform.SetParent(contenedorJugadores.transform, false); // que sea el padre el contenedor

            //Localizamos y actualizamos etiquetsa

            nuevoElemento.transform.Find("txtNickname").GetComponent<TextMeshProUGUI>().text = jugador.NickName;
            
        }

    }

    public void AlCrearSala()
    {
        byte maxJugadores;

        maxJugadores = 8;

        if (!String.IsNullOrEmpty(inputRoomName.text))
        {
            RoomOptions opcionesDeSala = new RoomOptions();

            opcionesDeSala.MaxPlayers = maxJugadores;


            PhotonNetwork.CreateRoom(inputRoomName.text, opcionesDeSala, TypedLobby.Default); //crea sala

            CambiarPanel(panelJugadoresRoom);
        }
        else
        {
            Estado("Introduce the Room Name");
        }

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomlist)
    {
        // Comprobar los elemenos uno por uno de lista roomlist
        // Si es un elemento borrado, lo buscamos en nuestra lista y lo borramos.
        // Si es un elemento nuevo, lo añadimos.
        // Si es un elemento modificado, machacamos su información.

        foreach (RoomInfo r in roomlist)
        {
            Debug.Log("Actualiza la lista de rooms");
            if (r.RemovedFromList || !r.IsOpen || r.IsVisible)
            {
                listaSalas.Remove(r.Name);
            }

            if (listaSalas.ContainsKey(r.Name))
            {
                if (r.PlayerCount > 0)
                    listaSalas[r.Name] = r;
                else  // Lo podemos comentar si no queremos borrar salas vacías.
                    listaSalas.Remove(r.Name);
            }
            else
            {
                listaSalas.Add(r.Name, r);
            }

        }

        ActualizarPanelDeSalas();
        
    }

    public void ActualizarPanelDeSalas()
    {
        while (contenedorRooms.transform.childCount > 0)
        {
            DestroyImmediate(contenedorRooms.transform.GetChild(0).gameObject);

        }

        foreach (RoomInfo r in listaSalas.Values)
        {
            // Creamos un nuevo botón y lo añadimos al contenedor
            GameObject nuevoElemento = Instantiate(elemRoom);
            nuevoElemento.transform.SetParent(contenedorRooms.transform, false);
            // Localizamos las etiquetas y las actualizamos
            nuevoElemento.transform.Find("TxtNombreSala").GetComponent<TextMeshProUGUI>().text = r.Name;
            nuevoElemento.transform.Find("TxtCapcidadSala").GetComponent<TextMeshProUGUI>().text = r.PlayerCount + " / " + r.MaxPlayers;

            //Añadimos la acción de selección en la lista.
            nuevoElemento.GetComponent<Button>().onClick.AddListener(() => { AlPulsarSala(r.Name); });
        }


    }

    public void AlPulsarCrearNuevaPartida()
    {
        if (!String.IsNullOrEmpty(inputNickname.text))
        {
            CambiarPanel(panelCrearRoom);
            Estado("Creating new Room...");

        }
        else
        {
            Estado("Introduce Your Nickname");
        }

    }

    public void AlPulsarFindRoom()
    {
        if (!String.IsNullOrEmpty(inputNickname.text))
        {
            CambiarPanel(panelRooms);
            
            Estado("Exploring Rooms");

        }
        else
        {
            Estado("Introduce Your Nickname");
        }

    }

    public void AlPulsarVolver()
    {
        CambiarPanel(panelPrincipal);
        Estado("Conected to Server");

    }

    public void AlPulsarIniciarPartida()
    {
        PhotonNetwork.LoadLevel(1);
        Destroy(this);
    }

    public void AlPulsarSala(string nombreSala)
    {
        PhotonNetwork.JoinRoom(nombreSala);
        CambiarPanel(panelJugadoresRoom);
    }

    public void AlPulsarBtnJoin()
    {

        if (!String.IsNullOrEmpty(inputPrivateRoom.text))
        {

            PhotonNetwork.JoinRoom(inputPrivateRoom.text);
            
            CambiarPanel(panelJugadoresRoom);
            
        }
        

    }





    public void Estado(string texto) 
    {
        textEstado.text = texto;
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
