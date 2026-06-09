using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool hasKey = false;
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        KeyItem key = other.GetComponent<KeyItem>();
        if (key != null)
        {
            hasKey = true;
            key.Collect();

            if (gameManager != null)
            {
                gameManager.SetMessage("Llave recogida. Ahora abre la puerta.");
                gameManager.SetKeyState(true);
            }

            return;
        }

        DoorController door = other.GetComponent<DoorController>();
        if (door != null)
        {
            if (hasKey)
            {
                door.OpenDoor();

                if (gameManager != null)
                {
                    gameManager.SetMessage("Puerta abierta. Avanza hasta la zona final.");
                    gameManager.SetDoorState(true);
                }
            }
            else
            {
                if (gameManager != null)
                {
                    gameManager.SetMessage("Necesitas la llave para abrir esta puerta.");
                }
            }

            return;
        }

        GoalZone goal = other.GetComponent<GoalZone>();
        if (goal != null)
        {
            if (gameManager != null)
            {
                gameManager.CompleteLevel();
            }
        }
    }
}
