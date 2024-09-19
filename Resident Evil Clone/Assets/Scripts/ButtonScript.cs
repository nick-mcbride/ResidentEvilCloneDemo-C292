using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController doorController;
    public float interactionDistance = 2.0f;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            doorController.ToggleDoor();
        }
    }
}