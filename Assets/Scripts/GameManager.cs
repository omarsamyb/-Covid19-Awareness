using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlayerInsideOffice;
    public bool controlsEnabled;
    public bool sceneInProgress;
    // Interaction Events:
    // Opening Scene Event {-1: No Interaction , 0: Wave , 1: Hug , 2: Shake Hands}
    public int OpeningSceneEvent;
    // Sanitizing Event {-1: No Interaction , 0: Didn't Sanitize , 1: Did Sanitize}
    public int SanitizingEvent;
    // Entertainment Event {-1: No Interaction , 0: Full Sofa , 1: Empty Sofa}
    public int EntertainmentEvent;
    // Counting the people that he had contact with as a negative behavior
    public int SocialDistanceCounter;
    // Prinitng Event {False: Didn't Print , True: Did Print}
    public bool PrintingEvent;
    // Leaving Office Event {False: Didn't Checkout , True: Did Checkout} 
    // Cannot exit the scene without checking out
    public bool CheckOutEvent;
    // Laptop Event {False: Didn't Use Laptop , True: Did Use Laptop}
    public bool LaptopTask;
    // End Interaction Events
    public float raycastRange;
    public int interactableMask;
    public int multiChoiceOutcome0;
    public Transform crosshairHover;
    public bool isPaused;
    public GameObject pauseMenu;
    private string menuSceneName;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        isPlayerInsideOffice = false;
        controlsEnabled = true;
        OpeningSceneEvent = -1;
        SanitizingEvent = -1;
        EntertainmentEvent = -1;
        SocialDistanceCounter = 0;
        PrintingEvent = false;
        CheckOutEvent = false;
        LaptopTask = false;
        raycastRange = 8f;
        interactableMask = 1 << 9;
        multiChoiceOutcome0 = -1;
        AudioManager.instance.Play("BackgroundSFX");
        sceneInProgress = false;
        isPaused = false;
        menuSceneName = "MainMenuScene";
    }

    public void Pause()
    {
        AudioManager.instance.Pause("BackgroundSFX");
        AudioManager.instance.Play("MenuMusic");
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Resume()
    {
        AudioManager.instance.Stop("MenuMusic");
        AudioManager.instance.UnPause("BackgroundSFX");
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }
}
