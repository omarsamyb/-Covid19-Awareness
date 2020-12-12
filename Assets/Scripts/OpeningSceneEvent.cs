using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningSceneEvent : MonoBehaviour
{
    public Transform npc;
    public Transform player;
    private Vector3 npcInitialPosition;
    private PlayerMotor playerMotor;
    private NPCMotor npcMotor;
    public Transform playerInteractionPoint;
    public Transform npcInteractionPoint;
    public Interactable handshakeOption;
    public Interactable hugOption;
    public Interactable waveOption;
    private Interactable currentHovered;
    private Interactable hovered;
    private Interactable selected;
    private bool isSelecting;
    private Camera cam;
    private RaycastHit hit;
    private Color hoverColor;
    private float rayCastRange;
    public Material hoverMaterial;
    public Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        playerMotor = player.GetComponent<PlayerMotor>();
        npcMotor = npc.GetComponent<NPCMotor>();
        isSelecting = false;
        cam = Camera.main;
        hoverColor = new Color(255f, 140f, 0f);
        npcInitialPosition = npc.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelecting)
        {
            rayCastRange = Vector3.Distance(player.position, npc.position) + 6f;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayCastRange, GameManager.instance.interactableMask))
            {
                currentHovered = hit.collider.GetComponent<Interactable>();
                if (hovered != currentHovered)
                {
                    if (hovered != null)
                    {
                        hovered.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                        hovered.transform.GetComponentInChildren<TextMeshPro>().fontSharedMaterial = defaultMaterial;
                    }
                    if (currentHovered != null)
                    {
                        currentHovered.transform.GetComponentInChildren<TextMeshPro>().color = hoverColor;
                        currentHovered.transform.GetComponentInChildren<TextMeshPro>().fontSharedMaterial = hoverMaterial;
                        hovered = currentHovered;
                    }
                }
                if (Input.GetKeyDown(KeyCode.E) && hovered != null)
                {
                    selected = hovered;
                    isSelecting = false;
                }
            }
            else
            {
                if (hovered != null)
                {
                    hovered.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                    hovered.transform.GetComponentInChildren<TextMeshPro>().fontSharedMaterial = defaultMaterial;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.instance.OpeningSceneEvent)
        {
            GameManager.instance.OpeningSceneEvent = true;
            GameManager.instance.controlsEnabled = false;
            playerMotor.MoveToPoint(playerInteractionPoint.position);
            npcMotor.MoveToPoint(npcInteractionPoint.position);
            StartCoroutine(FaceEachOther());
            Time.timeScale = 0.3f;
            handshakeOption.transform.GetChild(0).gameObject.SetActive(true);
            waveOption.transform.GetChild(0).gameObject.SetActive(true);
            hugOption.transform.GetChild(0).gameObject.SetActive(true);
            isSelecting = true;
        }
    }

    IEnumerator FaceEachOther()
    {
        while (!playerMotor.arrived || !npcMotor.arrived)
        {
            GameManager.instance.controlsEnabled = false;
            if (!isSelecting)
            {
                Time.timeScale = 1f;
                handshakeOption.transform.GetChild(0).gameObject.SetActive(false);
                waveOption.transform.GetChild(0).gameObject.SetActive(false);
                hugOption.transform.GetChild(0).gameObject.SetActive(false);
            }
            yield return null;
        }
        GameManager.instance.controlsEnabled = false;
        Vector3 npcPlayerDirection = (player.position - npc.position).normalized;
        Vector3 playerNpcDirection = (npc.position - player.position).normalized;
        Quaternion npcLookRotation = Quaternion.LookRotation(new Vector3(npcPlayerDirection.x, 0f, npcPlayerDirection.z));
        Quaternion playerLookRotation = Quaternion.LookRotation(new Vector3(playerNpcDirection.x, 0f, playerNpcDirection.z));
        player.rotation = playerLookRotation;
        npc.rotation = npcLookRotation;
        if (isSelecting)
        {
            Time.timeScale = 1f;
            // need to make this random
            selected = handshakeOption;
            selected.OnFocused(player);
            handshakeOption.transform.GetChild(0).gameObject.SetActive(false);
            waveOption.transform.GetChild(0).gameObject.SetActive(false);
            hugOption.transform.GetChild(0).gameObject.SetActive(false);
            isSelecting = false;
        }
        else
        {
            selected.OnFocused(player);
        }
        yield return WaitForInteraction(selected);

        npcMotor.MoveToPoint(npcInitialPosition);
        handshakeOption.gameObject.SetActive(false);
        waveOption.gameObject.SetActive(false);
        hugOption.gameObject.SetActive(false);
        GameManager.instance.controlsEnabled = true;
    }

    IEnumerator WaitForInteraction(Interactable interaction)
    {
        while (!interaction.finished)
        {
            yield return null;
        }
    }
}
