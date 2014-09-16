﻿using UnityEngine;
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
};

public class NodeJsSocketConnector : MonoBehaviour {

    public string host;
    public int port;

    public int fightId;
    public int playerId;

    int cntFrame = 0;

    GameController gameController;

    SocketThreadManager socketThreadManager = new SocketThreadManager();

    // Size of receive buffer.
    public const int BufferSize = 256;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];

    public int divisionRate = 10;
    int counter = 0;

	// Use this for initialization
	void Start () {
        gameController = GetComponent<GameController>();
        gameController.Pause();
	}
	
	// Update is called once per frame
	void Update () {
        if (counter < divisionRate)
        {
            counter++;
            return;
        }
        counter = 0;

        if (socketThreadManager.communicationState != CommunicationState.NOT_READY)
        {
            processData();    
        }

        Debug.Log("End of this function");
	}

    void processData()
    {
        float posX = gameController.player.transform.position.x;
        float posY = gameController.player.transform.position.y;
        float posZ = gameController.player.transform.position.z;
        float yaw = gameController.player.transform.rotation.eulerAngles.y;

        socketThreadManager.message = "{ "
            + "\"fightId\": " + fightId
            + ", \"playerId\": " + playerId
            + ", \"posX\": " + posX
            + ", \"posY\": " + posY
            + ", \"posZ\": " + posZ
            + ", \"yaw\": " + yaw
            + "}";

        Debug.Log("Message : " + socketThreadManager.message);

        String response = socketThreadManager.response;
        if (response != null)
        {
            Debug.Log("Response : " + response);

            var responseObj = SimpleJSON.JSON.Parse(socketThreadManager.response);

            gameController.enemy.transform.position = new Vector3(
                responseObj["enemy"]["x"].AsFloat,
                responseObj["enemy"]["y"].AsFloat,
                responseObj["enemy"]["z"].AsFloat
            );
            gameController.enemy.transform.eulerAngles = new Vector3(0.0f, responseObj["enemy"]["yaw"].AsFloat, 0.0f);
        }
    }

    void OnGUI()
    {
        if (socketThreadManager.communicationState == CommunicationState.NOT_READY)
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
                Debug.Log("Connecting");

                socketThreadManager.doConnect(host, port);
                gameController.UnPause();

                Debug.Log("Connected");
            }
        }
    }

    

    void onDestroy() {
        socketThreadManager.Destroy();
        
    }
}

public class SocketThreadManager
{
    TcpClient client = null;
    NetworkStream stream = null;

    public CommunicationState communicationState = CommunicationState.NOT_READY;

    String messageString = null; // input message

    String responseString = null;

    Thread thread;

    bool shouldRun = true;

    void Process() {
        while (shouldRun)
        {
            if (message == null)
            {
                continue;
            }
            // Send test data to the remote device.

            Send(message);

            response = Receive();

            Thread.Sleep(500);
        }
    }

    

    public void doConnect(String host, int port)
    {
        TcpClient client = new TcpClient(host, port);
        stream = client.GetStream();

        communicationState = CommunicationState.READY;

        Debug.Log("Starting thread");

        thread = new Thread(Process);
        thread.Start();

        Debug.Log("Thread started");
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

    public void Destroy()
    {
        shouldRun = false;

        stream.Close();
        client.Close();
    }

    public String message
    {
        get
        {
            return messageString;
        }
        set
        {
            messageString = value;
        }
    }

    public String response
    {
        get
        {
            return responseString;
        }
        set
        {
            responseString = value;
        }
    }
}


