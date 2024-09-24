using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Transform Door;
    public Vector3 openRotation;
    public Vector3 closedRotation;
    public float openSpeed = 2.0f;
    private bool isOpen = false;

    void Start()
    {
        if (Door == null)
        {
            Debug.LogError("Door Transform is not assigned. Please assign it in the Inspector.");
            return;
        }
        Door.localEulerAngles = closedRotation;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        Debug.Log("Toggling door. New state: " + (isOpen ? "Open" : "Closed"));
        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
        Debug.Log("Starting door rotation to: " + targetRotation);
        while (Quaternion.Angle(Door.localRotation, targetQuaternion) > 0.01f)
        {
            Door.localRotation = Quaternion.RotateTowards(Door.localRotation, targetQuaternion, openSpeed * Time.deltaTime * 100);
            yield return null;
        }
        Door.localRotation = targetQuaternion;
        Debug.Log("Door rotation complete.");
    }
}
