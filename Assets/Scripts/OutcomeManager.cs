using UnityEngine;

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
    // TV Interaction
    public GameObject tvInteraction1;
    public GameObject tvInteraction2;
    // Printer Interaction
    public GameObject printerInteraction;
    // Attendance Interaction
    public GameObject attendanceInteraction;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        defaultLayer = 8;
        interactableLayer = 9;
    }

    public void Disable_OpeningSceneInteraction()
    {
        handshakeInteraction.layer = defaultLayer;
        waveInteraction.layer = defaultLayer;
        hugInteraction.layer = defaultLayer;
    }
    public void Disable_SanitizeInteraction()
    {
        sanitizeInteraction.layer = defaultLayer;
    }
    public void Disable_DoorInteraction()
    {
        doorInteraction.layer = defaultLayer;
    }
    public void Enable_DoorInteraction()
    {
        doorInteraction.layer = interactableLayer;
    }
    public void Disable_TvInteraction()
    {
        tvInteraction1.layer = defaultLayer;
        tvInteraction2.layer = defaultLayer;
    }
    public void Enable_TvInteraction()
    {
        tvInteraction1.layer = interactableLayer;
        tvInteraction2.layer = interactableLayer;
    }
    public void Disable_PrinterInteraction()
    {
        printerInteraction.layer = defaultLayer;
    }
    public void Enable_PrinterInteraction()
    {
        printerInteraction.layer = interactableLayer;
    }
    public void Disable_AttendanceInteraction()
    {
        attendanceInteraction.layer = defaultLayer;
    }
    public void Enable_AttendanceInteraction()
    {
        attendanceInteraction.layer = interactableLayer;
    }
}
