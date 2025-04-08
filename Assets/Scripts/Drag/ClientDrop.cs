using UnityEngine;
using UnityEngine.EventSystems;

public class ClientDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
    }
}
