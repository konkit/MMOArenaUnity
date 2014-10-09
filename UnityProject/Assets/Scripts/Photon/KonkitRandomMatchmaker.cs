using UnityEngine;
using System.Collections;

public class KonkitRandomMatchmaker : MonoBehaviour {

    public GameObject playerPrefab;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;

        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom("asdf");
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        GameObject monster = PhotonNetwork.Instantiate("MAX_Photon", Vector3.zero, Quaternion.identity, 0);
        monster.GetComponent<CharacterControlInterface>().enabled = true;
        monster.GetComponent<PlayerMovementController>().enabled = true;
        monster.GetComponent<MouseController>().enabled = true;
        monster.GetComponent<HumanPlayerController>().enabled = true;
    }
}
