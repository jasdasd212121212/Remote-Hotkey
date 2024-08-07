using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AnchoredPositionResetter : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}