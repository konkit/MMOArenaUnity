using UnityEngine;
using System.Collections;

public class GuiDisplayer : MonoBehaviour {

    GameController gameController;    

	// Use this for initialization
	void Start () {
        gameController = GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI()
    {
        if (gameController.errorMsg.Length > 0)
        {
            GUILayout.BeginArea(new Rect((Screen.width / 2) - 250, (Screen.height / 2), 500, 100));
            GUILayout.BeginVertical();

            GUILayout.Box("Error occured : " + gameController.errorMsg);

            if (GUILayout.Button("Go back"))
            {
                Debug.Log("Go back external call");
                Application.ExternalCall("redirectAfterFight");
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        else if (gameController.isGameStarted == false)
        {
            GUILayout.BeginArea(new Rect((Screen.width / 2) - 250, (Screen.height / 2), 500, 100));
            GUILayout.BeginVertical();

            if (gameController.loadingMsg.Length > 0)
            {
                GUILayout.Box(gameController.loadingMsg);
            }
            else
            {
                GUILayout.Box("Connecting...");
            }
            

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        /*
        if( fightResultSender.fightAwards != null ) {
            GUILayout.BeginArea(new Rect((Screen.width / 2) - 50, (Screen.height / 2), 100, 100));
            GUILayout.BeginVertical();

            if (fightResultSender.fightAwards.hasWon == true)
            {
                GUILayout.Label("You won!");
            }
            else
            {
                GUILayout.Label("You lost!");
            }

            GUILayout.Label("Exp earned : " + fightResultSender.fightAwards.expEarned);

            if (GUILayout.Button("Go back"))
            {
                Debug.Log("Go back external call");
                Application.ExternalCall("redirectAfterFight");
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        */
        
    }
}
