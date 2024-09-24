using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController doorController;
    public float interactionDistance = 2.0f;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not found. Make sure the player GameObject is tagged as 'Player'.");
        }
    }

    void Update()
    {
        if (doorController == null)
        {
            Debug.LogError("DoorController reference is not assigned. Please assign it in the Inspector.");
            return;
        }

        float distance = Vector3.Distance(playerTransform.position, transform.position);


        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player is within interaction distance and pressed 'E'. Toggling door.");
            doorController.ToggleDoor();
        }
    }
}
