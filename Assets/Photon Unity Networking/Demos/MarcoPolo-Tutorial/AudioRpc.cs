using UnityEngine;

public class AudioRpc : Photon.MonoBehaviour
{

    public AudioClip marco;
    public AudioClip polo;

    [RPC]
    void Marco()
    {
        if (!this.enabled)
        {
            return;
        }

        Debug.Log("Marco");

        this.audio.clip = marco;
        this.audio.Play();
    }

    [RPC]
    void Polo()
    {
        if (!this.enabled)
        {
            return;
        }

        Debug.Log("Polo");

        this.audio.clip = polo;
        this.audio.Play();
    }

    void OnApplicationFocus(bool focus)
    {
        this.enabled = focus;
    }
}
