using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public void Collect()
    {
        Debug.Log("Llave recogida.");
        Destroy(gameObject);
    }
}
