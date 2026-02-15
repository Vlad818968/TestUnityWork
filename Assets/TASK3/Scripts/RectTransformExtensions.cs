using UnityEngine;

public static class RectTransformExtensions
{
    public static void AnchoredPositionSetY(this RectTransform rectTransform, float value)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, value);
    }
}
