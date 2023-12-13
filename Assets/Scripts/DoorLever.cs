using UnityEngine;
using UnityEngine.UI;

public class DoorLever : MonoBehaviour
{
    public bool opened {  get; private set; }

    bool interacted;

    [SerializeField] Door linkedDoor;
    [SerializeField] GameObject ui;

    CharactersControler cc;

    [SerializeField] float closeTime;

    [Header("Gros Bras")]
    public float GBLifeCost;
    public int GBEnergyCost;
    [SerializeField] GameObject GBImpossibleImage;
    [SerializeField] Button GBButton;

    [Header("Hacker")]
    public float HEnergyCost;
    [SerializeField] GameObject HImpossibleImage;
    [SerializeField] Button HButton;


    private void Start()
    {
        cc = CharactersControler.instance;
        ui.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (opened || interacted)
            return;

        interacted = true;

        ui.GetComponent<Animator>().Play("ShowScreen");
        Invoke("Close", closeTime);

        if (!cc.HasStats(CharacterEnum.GrosBras, energy:GBEnergyCost))
        {
            GBImpossibleImage.SetActive(true);
            GBButton.interactable = false;
        }
        else
        {
            GBImpossibleImage.SetActive(false);
            GBButton.interactable = true;
        }

        if (!cc.HasStats(CharacterEnum.Hacker, energy: GBEnergyCost))
        {
            HImpossibleImage.SetActive(true);
            HButton.interactable = false;
        }
        else
        {
            HImpossibleImage.SetActive(false);
            HButton.interactable = true;
        }
    }

    private void Close()
    {
        interacted = false;
        ui.GetComponent<Animator>().Play("CloseScreen");
    }

    public void OpenWithGB()
    {
        cc.ConsumeStats(CharacterEnum.GrosBras, GBLifeCost, GBEnergyCost);
        Open();
    }

    public void OpenWithH()
    {
        cc.ConsumeStats(CharacterEnum.Hacker, energy:HEnergyCost);
        Open();
    }

    public void OpenWithItem()
    {
        Open();
    }

    private void Open()
    {
        opened = true;
        linkedDoor.Open();
    }
}
