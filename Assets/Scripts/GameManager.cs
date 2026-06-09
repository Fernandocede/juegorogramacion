using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text objectiveText;
    public Text statusText;

    private bool keyCollected = false;
    private bool doorOpened = false;
    private bool levelCompleted = false;

    void Start()
    {
        UpdateUI();
        SetMessage("Objetivo: recoge la llave, abre la puerta y llega a la meta.");
    }

    public void SetKeyState(bool value)
    {
        keyCollected = value;
        UpdateUI();
    }

    public void SetDoorState(bool value)
    {
        doorOpened = value;
        UpdateUI();
    }

    public void SetMessage(string message)
    {
        if (objectiveText != null)
        {
            objectiveText.text = message;
        }
    }

    public void CompleteLevel()
    {
        if (levelCompleted)
            return;

        levelCompleted = true;
        SetMessage("¡Nivel completado! Prototipo funcional terminado.");
        UpdateUI();
        Debug.Log("Nivel completado.");
    }

    void UpdateUI()
    {
        if (statusText == null)
            return;

        string keyStatus = keyCollected ? "Sí" : "No";
        string doorStatus = doorOpened ? "Abierta" : "Cerrada";
        string completeStatus = levelCompleted ? "Completado" : "En proceso";

        statusText.text =
            "Estado del prototipo\n" +
            "Llave recogida: " + keyStatus + "\n" +
            "Puerta: " + doorStatus + "\n" +
            "Nivel: " + completeStatus;
    }
}
