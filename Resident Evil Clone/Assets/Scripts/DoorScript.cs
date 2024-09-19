using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform doorTransform;
    public Vector3 openPosition;
    public Vector3 closedPosition;
    public float openSpeed = 2.0f;
    private bool isOpen = false;

    void Start()
    {
        doorTransform.position = closedPosition;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(MoveDoor(isOpen ? openPosition : closedPosition));
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(doorTransform.position, targetPosition) > 0.01f)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, targetPosition, openSpeed * Time.deltaTime);
            yield return null;
        }
        doorTransform.position = targetPosition;
    }
}