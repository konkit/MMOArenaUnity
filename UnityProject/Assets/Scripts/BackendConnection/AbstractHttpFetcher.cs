using UnityEngine;
using System.Collections;

public class AbstractHttpFetcher : MonoBehaviour {

    protected string absoluteAddress = "";

    public string receivedData = "";
    //delegate on received
    protected delegate void ReceiveHttpDataDelegate(string data);
    protected ReceiveHttpDataDelegate receiveCallback;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Fetch()
    {
        StartCoroutine(FetchCoroutine());
    }

    protected IEnumerator FetchCoroutine()
    {
        Debug.Log("Request sent to : " + absoluteAddress);

        WWW www = new WWW(absoluteAddress);
        yield return www;

        if (www.error != null)
        {
            throw new UnityException("Http request error : " + www.error);
        }

        receivedData = www.text;
        receiveCallback(receivedData);
    }
}
