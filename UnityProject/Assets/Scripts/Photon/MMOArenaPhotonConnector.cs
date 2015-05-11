using UnityEngine;
using System.Collections;

public class MMOArenaPhotonConnector : MonoBehaviour {

    public bool isOffline = false;


    bool isConnected = false;

    protected string roomName = "";

    public int playerId = -1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Connect(string roomName)
    {
        this.roomName = roomName;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        GUILayout.Label(PhotonNetwork.player.ID.ToString());
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joining room " + roomName);
        PhotonNetwork.JoinRoom(roomName);
    }

    void OnPhotonJoinRoomFailed()
    {
        Debug.Log("No such room with name : " + roomName + ", creating!");
        RoomOptions roomOptions = new RoomOptions();
            roomOptions.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        GameObject character = PhotonNetwork.Instantiate("MAX_Photon", Vector3.zero, Quaternion.identity, 0);

        //character.GetComponent<CharacterControlInterface>().enabled = true;
        character.GetComponent<PlayerMovementController>().enabled = true;
        character.GetComponent<MouseController>().enabled = true;
        character.GetComponent<HumanPlayerController>().enabled = true;
    }

//    void OnPhotonPlayerDisconnected(PhotonPlayer player)
//    {
//        GetComponent<GameController>().isGameFinished = true;
//    }
}
