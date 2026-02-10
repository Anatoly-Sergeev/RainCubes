using UnityEngine;

public class Painter : MonoBehaviour
{
    public Color GetRandomColor()
    {
        return Random.ColorHSV();
    }
}
