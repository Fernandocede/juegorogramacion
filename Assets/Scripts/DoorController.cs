using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool opened = false;

    public void OpenDoor()
    {
        if (opened)
            return;

        opened = true;
        Debug.Log("Puerta abierta.");
        gameObject.SetActive(false);
    }
}
