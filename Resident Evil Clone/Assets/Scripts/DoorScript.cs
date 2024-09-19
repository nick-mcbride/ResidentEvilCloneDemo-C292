using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Transform doorTransform;
    public Vector3 openRotation;
    public Vector3 closedRotation;
    public float openSpeed = 2.0f;
    private bool isOpen = false;

    void Start()
    {
        doorTransform.localEulerAngles = closedRotation;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
        while (Quaternion.Angle(doorTransform.localRotation, targetQuaternion) > 0.01f)
        {
            doorTransform.localRotation = Quaternion.RotateTowards(doorTransform.localRotation, targetQuaternion, openSpeed * Time.deltaTime * 100);
            yield return null;
        }
        doorTransform.localRotation = targetQuaternion;
    }
}

