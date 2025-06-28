using UnityEngine;

public class PickupItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Người chơi đã nhặt vật phẩm: {gameObject.name}");
            Destroy(gameObject);
        }
    }
}
