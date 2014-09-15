using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;

public enum CommunicationState
{
    NOT_READY,
    READY,
    SENDING,
    SENT,
    RECEIVING,
    RECEIVED
}

public class NodeJsSocketConnector : MonoBehaviour {

    public string host;
    public int port;

    public int fightId;
    public int playerId;

    GameController gameController;
    public CommunicationState communicationState = CommunicationState.NOT_READY;

    TcpClient client = null;
    NetworkStream stream = null;

    // Client socket.
    public Socket socket = null;
    // Size of receive buffer.
    public const int BufferSize = 256;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();

    public static String response = String.Empty;

	// Use this for initialization
	void Start () {
        communicationState = CommunicationState.NOT_READY;

        gameController = GetComponent<GameController>();
        gameController.Pause();
	}
	
	// Update is called once per frame
	void Update () {
        if (communicationState != CommunicationState.NOT_READY)
        {
            // send data to node.js

            float posX = gameController.player.transform.position.x;
            float posY = gameController.player.transform.position.y;
            float posZ = gameController.player.transform.position.z;

            String message = "{ "
                + "\"fightId\": " + fightId 
                + ", \"playerId\": " + playerId 
                + ", \"posX\": " + posX 
                + ", \"posY\": " + posY 
                + ", \"posZ\": " + posZ 
                +"}";

            communicationState = CommunicationState.SENDING;

            // Send test data to the remote device.
            Send(message);

            communicationState = CommunicationState.SENT;

            communicationState = CommunicationState.RECEIVING;

            // Receive the response from the remote device.
            response = Receive();

            communicationState = CommunicationState.RECEIVED;

            var responseObj = SimpleJSON.JSON.Parse(response);

            gameController.enemy.transform.position = new Vector3(
                responseObj["enemy"]["x"].AsFloat,
                responseObj["enemy"]["y"].AsFloat,
                responseObj["enemy"]["z"].AsFloat
            );

            communicationState = CommunicationState.READY;
        }
	}

    void OnGUI()
    {
        if (communicationState == CommunicationState.NOT_READY)
        {
            string tmp = "";

            GUI.Label(new Rect(30, 30, 100, 20), "Host : ");
            host = GUI.TextField(new Rect(150, 30, 100, 20), host);

            GUI.Label(new Rect(30, 60, 100, 20), "Port : ");
            tmp = GUI.TextField(new Rect(150, 60, 100, 20), port.ToString());
            port = int.Parse(tmp);

            GUI.Label(new Rect(30, 90, 100, 20), "FightId : ");
            tmp = GUI.TextField(new Rect(150, 90, 100, 20), fightId.ToString());
            fightId = int.Parse(tmp);

            GUI.Label(new Rect(30, 120, 100, 20), "PlayerId : ");
            tmp = GUI.TextField(new Rect(150, 120, 100, 20), playerId.ToString());
            playerId = int.Parse(tmp);

            if (GUI.Button(new Rect(100, 150, 100, 30), "Connect"))
            {
                doConnect();
            }
        }
    }

    void doConnect()
    {
        TcpClient client = new TcpClient(host, port);
        stream = client.GetStream();

        communicationState = CommunicationState.READY;
        gameController.UnPause();
    }

    void Send(String message)
    {
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);   
 
        stream.Write(data, 0, data.Length);
    }

    String Receive()
    {
        // Buffer to store the response bytes.
        Byte[] data = new Byte[1024];

        // String to store the response ASCII representation.
        String responseData = String.Empty;

        // Read the first batch of the TcpServer response bytes.
        Int32 bytes = stream.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

        return responseData;
    }




    void onDestroy() {


        // Release the socket.
        //socket.Shutdown(SocketShutdown.Both);
        //socket.Close();
    }
}

public class StateObject
{
    // Client socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 256;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();
}
