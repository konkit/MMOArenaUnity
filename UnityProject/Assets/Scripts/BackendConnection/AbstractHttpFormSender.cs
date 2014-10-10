using UnityEngine;
using System.Collections;

public class AbstractHttpFormSender : MonoBehaviour {
    protected string absoluteAddress = "";

    public string receivedData = "";

    //delegate on received

    protected delegate void ReceiveHttpDataDelegate(string data);
    protected ReceiveHttpDataDelegate receiveCallback;

    protected WWWForm form;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected IEnumerator SendCoroutine()
    {
        Debug.Log("Request sent to : " + absoluteAddress);

        WWW www = new WWW(absoluteAddress, form);
        yield return www;

        if (www.error != null)
        {
            throw new UnityException("Http request error : " + www.error);
        }

        receivedData = www.text;
        receiveCallback(receivedData);
    }
}

