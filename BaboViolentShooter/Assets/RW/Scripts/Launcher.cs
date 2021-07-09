using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject controlPanel;

        [SerializeField]
        private Text feedbackText;

        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        bool isConnecting;

        string gameVersion = "1";

        [Space(10)]
        [Header("Custom Variables")]
        public InputField playerNameField;
        public InputField roomNameField;

        [Space(5)]
        public Text playerStatus;
        public Text connectionStatus;

        [Space(5)]
        public GameObject roomJoinUI;
        public GameObject buttonLoadArena;
        public GameObject buttonJoinRoom;


        // Start Method
        void Start()
        {
            //1
            PlayerPrefs.DeleteAll();

            Debug.Log("Connecting to Photon Network");

            //2
            roomJoinUI.SetActive(false);
            buttonLoadArena.SetActive(false);

            //3
            ConnectToPhoton();
        }

        void Awake()
        {
            //4 
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Tutorial Methods
        void ConnectToPhoton()
        {
            connectionStatus.text = "З'єднання...";
            PhotonNetwork.GameVersion = gameVersion; //1
            PhotonNetwork.ConnectUsingSettings(); //2
        }

        public void JoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                if(string.IsNullOrEmpty(playerNameField.text) || string.IsNullOrEmpty(roomNameField.text))
                {
                    return;
                }

                PhotonNetwork.LocalPlayer.NickName = playerNameField.text; //1
                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + roomNameField.text);
                RoomOptions roomOptions = new RoomOptions(); //2
                TypedLobby typedLobby = new TypedLobby(roomNameField.text, LobbyType.Default); //3
                PhotonNetwork.JoinOrCreateRoom(roomNameField.text, roomOptions, typedLobby); //4
            }
        }

        public void LoadArena()
        {
            // 5
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                PhotonNetwork.LoadLevel("MainArena");
            }
            else
            {
                playerStatus.text = "Minimum 2 Players required to Load Arena!";
            }
        }

        // Photon Methods
        public override void OnConnected()
        {
            // 1
            base.OnConnected();
            // 2
            connectionStatus.text = "Під'єднано до Photon!";
            connectionStatus.color = Color.green;
            roomJoinUI.SetActive(true);
            buttonLoadArena.SetActive(false);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            // 3
            isConnecting = false;
            controlPanel.SetActive(true);
            Debug.LogError("Disconnected. Please check your Internet connection.");
        }

        public override void OnJoinedRoom()
        {
            // 4
            if (PhotonNetwork.IsMasterClient)
            {
                buttonLoadArena.SetActive(true);
                buttonJoinRoom.SetActive(false);
                playerStatus.text = "Ви головний в Лоббі";
            }
            else
            {
                playerStatus.text = "Під'єднано до Лоббі";
            }
        }
    }
}