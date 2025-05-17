using UnityEngine;

public class SmallBigDoc : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}
