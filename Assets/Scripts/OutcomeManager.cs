using UnityEngine;
using TMPro;

public class OutcomeManager : MonoBehaviour
{
    public static OutcomeManager instance;
    private int defaultLayer;
    private int interactableLayer;
    // Opening Scene Interaction
    public GameObject handshakeInteraction;
    public GameObject waveInteraction;
    public GameObject hugInteraction;
    // Door Interaction
    public GameObject doorInteraction;
    // Sanitize Hands Interaction
    public GameObject sanitizeInteraction;
    // Desk Interaction
    public GameObject deskInteraction;
    // TV Interaction
    public GameObject tvInteraction1;
    public GameObject tvInteraction2;
    // Printer Interaction
    public GameObject printerInteraction;
    // Attendance Interaction
    public GameObject attendanceInteraction;
    // Exit Interaction
    public GameObject exitInteraction;

    public Transform currentObjective;
    public Transform currentObjective2;
    public TextMeshProUGUI objectiveText;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        defaultLayer = 8;
        interactableLayer = 9;
        currentObjective = doorInteraction.transform;
        objectiveText.text = "Finish Work in your Office";
    }

    public void Disable_OpeningSceneInteraction()
    {
        handshakeInteraction.layer = defaultLayer;
        waveInteraction.layer = defaultLayer;
        hugInteraction.layer = defaultLayer;
    }
    public void Disable_SanitizeInteraction()
    {
        if(GameManager.instance.SanitizingEvent == -1){
            GameManager.instance.SanitizingEvent = 0;
        }
        sanitizeInteraction.layer = defaultLayer;
    }
    public void Disable_DoorInteraction()
    {
        doorInteraction.layer = defaultLayer;
        Disable_SanitizeInteraction();
        Enable_DeskInteraction();
        currentObjective = deskInteraction.transform;
    }
    public void Enable_DoorInteraction()
    {
        doorInteraction.layer = interactableLayer;
        Enable_ExitInteraction();
        currentObjective = exitInteraction.transform;
    }
    public void Disable_DeskInteraction()
    {
        deskInteraction.layer = defaultLayer;
        Enable_TvInteraction();
        currentObjective = tvInteraction1.transform;
        currentObjective2 = tvInteraction2.transform;
        objectiveText.text = "Take a break in the Entertainment area";
    }
    public void Enable_DeskInteraction()
    {
        deskInteraction.layer = interactableLayer;
    }
    public void Disable_TvInteraction()
    {
        tvInteraction1.layer = defaultLayer;
        tvInteraction2.layer = defaultLayer;
        Enable_PrinterInteraction();
        currentObjective = printerInteraction.transform;
        currentObjective2 = null;
        objectiveText.text = "Print the work Documents";
    }
    public void Enable_TvInteraction()
    {
        tvInteraction1.layer = interactableLayer;
        tvInteraction2.layer = interactableLayer;
    }
    public void Disable_PrinterInteraction()
    {
        printerInteraction.layer = defaultLayer;
        Enable_AttendanceInteraction();
        currentObjective = attendanceInteraction.transform;
        objectiveText.text = "Take your attendance";
    }
    public void Enable_PrinterInteraction()
    {
        printerInteraction.layer = interactableLayer;
    }
    public void Disable_AttendanceInteraction()
    {
        attendanceInteraction.layer = defaultLayer;
        Enable_DoorInteraction();
        currentObjective = exitInteraction.transform;
        objectiveText.text = "Leave the Building";
    }
    public void Enable_AttendanceInteraction()
    {
        attendanceInteraction.layer = interactableLayer;
    }
    public void Enable_ExitInteraction()
    {
        exitInteraction.layer = interactableLayer;
    }
}
