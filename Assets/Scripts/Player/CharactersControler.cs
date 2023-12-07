using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersControler : MonoBehaviour
{
    [SerializeField] Character front;
    [SerializeField] Character right;
    [SerializeField] Character left;
    [SerializeField] Character back;


    public void TakeDamage(float amount)
    {
        float randomchar;

        Character characterSelected = null;
        while (characterSelected == null)
        {
            randomchar = Random.Range(0f, 100f);
            if (randomchar >= 0 && randomchar <= 1)
            {
                characterSelected = back;
            }
            else if (randomchar >= 1 && randomchar < 21)
            {
                characterSelected = right;
            }
            else if (randomchar >= 21 && randomchar < 41)
            {
                characterSelected = left;
            }
            else if (randomchar >= 41 && randomchar <= 100)
            {
                characterSelected = front;
            }
        }
        characterSelected.TakeDamage(amount);
    }
}
