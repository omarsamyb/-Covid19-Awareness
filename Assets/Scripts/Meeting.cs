using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Meeting : MonoBehaviour
{
    public GameObject videoFX;
    private VideoPlayer vp;
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
        vp = videoFX.GetComponent<VideoPlayer>();
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (vp.isPlaying && !started)
        {
            started = true;
        }
        if(!vp.isPlaying && started)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
