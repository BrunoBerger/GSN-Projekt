using UnityEngine;

public class PositionBar : MonoBehaviour
{
    public Transform slidingObject;
    public int yMin;
    public int yMax;

    public void SetValue(float value)
    {
        var position = slidingObject.localPosition;
        position.y = Mathf.Lerp(yMin, yMax, Mathf.Clamp(value, 0f, 1f));
        slidingObject.localPosition = position;
    }
}
