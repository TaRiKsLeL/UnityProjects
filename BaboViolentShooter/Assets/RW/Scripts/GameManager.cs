using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using Photon.Realtime;


namespace Photon.Pun.Demo.PunBasics
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] Vector2 mapSize = new Vector2(10, 10);


        private GameObject player;
        private float cameraOrtSize;

        // Start is called before the first frame update
        void Start()
        {
            if (!PhotonNetwork.IsConnected) // 1
            {
                SceneManager.LoadScene("Launcher");
                return;
            }

            if (PlayerManager.LocalPlayerInstance == null)
            {
                Debug.Log("Instantiating Player");

                Vector2 playerPos = Generator.getInstance().GenerateRandPosition();

                if (PhotonNetwork.IsMasterClient)
                {
                    player = PhotonNetwork.Instantiate("PlayerF", playerPos, Quaternion.identity, 0);
                    //player.GetComponent<SpriteRenderer>().color = Generator.getInstance().GenerateRandColor();
                }
                else
                {
                    player = PhotonNetwork.Instantiate("PlayerS", playerPos, Quaternion.identity, 0);
                }

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //1
            {
                Application.Quit();
            }
        }

        // Photon Methods
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Launcher");
            }
        }

        // Helper Methods
        public void QuitRoom()
        {
            Application.Quit();
        }
    }
}
