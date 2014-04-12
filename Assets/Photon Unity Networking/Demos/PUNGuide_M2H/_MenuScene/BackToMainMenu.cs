using UnityEngine;
using System.Collections;

public class BackToMainMenu : Photon.MonoBehaviour
{

    public static BackToMainMenu SP;

    void Awake()
    {
        if (SP != null && SP != this)
        {
            Destroy(this);
            return;
        }
        SP = this;
        DontDestroyOnLoad(this);
    }

    void OnGUI()
    {
        if (Application.loadedLevel >= 1)
        {
            if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 20, 150, 20), "Back to Main Menu"))
            {
                QuitToMainMenu();
            }
        }
    }

    void QuitToMainMenu()
    {
        if (PhotonNetwork.connected){
            PhotonNetwork.Disconnect();
        }
        Application.LoadLevel(0);
    }
}