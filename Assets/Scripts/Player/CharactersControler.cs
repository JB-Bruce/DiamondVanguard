using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersControler : MonoBehaviour
{
    [SerializeField] Character front;
    [SerializeField] Character right;
    [SerializeField] Character left;
    [SerializeField] Character back;
    public static CharactersControler instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //InvokeRepeating("test", 1, 0.3f);
    }

    private void test() 
    {
        TakeDamage(Random.Range(5, 10));
    }

    public void TakeDamage(float amount)
    {
        if (back.dead && left.dead && right.dead && front.dead)
            return;

        float randomchar;

        Character characterSelected = null;
        while (characterSelected == null)
        {
            randomchar = Random.Range(0f, 100f);
            if (randomchar >= 0 && randomchar <= 1 && !back.dead)
            {
                characterSelected = back;
            }
            else if (randomchar >= 1 && randomchar < 21 && !right.dead)
            {
                characterSelected = right;
            }
            else if (randomchar >= 21 && randomchar < 41 && !left.dead)
            {
                characterSelected = left;
            }
            else if (randomchar >= 41 && randomchar <= 100f && !front.dead)
            {
                characterSelected = front;
            }
        }
        characterSelected.TakeDamage(amount);
    }
}
