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
        Disable_SanitizeInteraction();
        Enable_DeskInteraction();
    }
    public void Enable_DoorInteraction()
    {
        doorInteraction.layer = interactableLayer;
        Enable_ExitInteraction();
    }
    public void Disable_DeskInteraction()
    {
        deskInteraction.layer = defaultLayer;
        Enable_TvInteraction();
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
    }
    public void Enable_PrinterInteraction()
    {
        printerInteraction.layer = interactableLayer;
    }
    public void Disable_AttendanceInteraction()
    {
        attendanceInteraction.layer = defaultLayer;
        Enable_DoorInteraction();
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
