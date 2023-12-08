using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    public Item item = null;
    public Image itemImage;

    

    public bool HasItem()
    {
        if (item != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void addItem(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.icon;
    }

    public void imageUpdate()
    {
        if (item != null)
        {
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }
}
