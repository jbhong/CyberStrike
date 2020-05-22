using UnityEngine;

public class PauseAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(BGSoundScript.Instance != null)
            BGSoundScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
