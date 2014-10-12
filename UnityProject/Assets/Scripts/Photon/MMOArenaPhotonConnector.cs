using UnityEngine;
using System.Collections;

public class MMOArenaPhotonConnector : MonoBehaviour {

    public bool isOffline = false;

    RoomNameFetcher roomNameFetcher;

    bool isConnected = false;

    protected string roomName = "";

	// Use this for initialization
	void Start () {
        roomNameFetcher = GetComponent<RoomNameFetcher>();
	}
	
	// Update is called once per frame
	void Update () {
        if ( !isConnected && roomNameFetcher != null && roomNameFetcher.roomName != "" )
        {
            isConnected = true;

            if (isOffline)
            {
                Debug.Log("Setting offline mode");
                PhotonNetwork.offlineMode = true;
                PhotonNetwork.CreateRoom("offlinemoderoom");
            }
            else
            {
                Connect(roomNameFetcher.roomName);
            }            
        }
	}

    public void Connect(string roomName)
    {
        this.roomName = roomName;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
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
        character.GetComponent<CharacterControlInterface>().enabled = true;
        character.GetComponent<PlayerMovementController>().enabled = true;
        character.GetComponent<MouseController>().enabled = true;
        character.GetComponent<HumanPlayerController>().enabled = true;

        PlayerDataFetcher playerDataFetcher = GetComponent<PlayerDataFetcher>();
        character.GetComponent<CharacterStats>().LoadFromData(playerDataFetcher.playerData);

        character.GetComponent<PhotonCharacterSpellcasting>().LoadSpells(playerDataFetcher.playerData);

        //FightResultSender fightResultSender = GetComponent<FightResultSender>();
        //fightResultSender.player = character.GetComponent<CharacterStats>();

        //gameController.gameState = GameState.ONGOING;
    }
}
