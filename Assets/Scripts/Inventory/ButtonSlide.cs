using UnityEngine;

public class ButtonSlide : MonoBehaviour
{
    public void UpSlide()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, 424);
    }

    public void DownSlide() 
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 400);
    }
}
