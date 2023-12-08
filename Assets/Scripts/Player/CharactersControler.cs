using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersControler : MonoBehaviour
{
    [SerializeField] Character front;
    [SerializeField] Character right;
    [SerializeField] Character left;
    [SerializeField] Character back;

    [SerializeField] CharacterUIStatsUpdater frontUI ;
    [SerializeField] CharacterUIStatsUpdater rightUI;
    [SerializeField] CharacterUIStatsUpdater leftUI;
    [SerializeField] CharacterUIStatsUpdater backUI;

    private void Start()
    {
        front.controler = this;
        right.controler = this;
        left.controler = this;
        back.controler = this;
        InvokeRepeating("test", 1, 0.3f);
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

    public void Die(Character isdead)
    {
        if (isdead == front)
        {
            if (!right.dead)
            {
                frontUI.SetNewCharacter(right);
                if (!left.dead)
                {
                    rightUI.SetNewCharacter(left);
                    if (!back.dead)
                    {
                        leftUI.SetNewCharacter(back);
                        backUI.ResetCharacter();
                    }
                    else
                    {
                        leftUI.ResetCharacter();
                    }
                }
                else
                {
                    rightUI.ResetCharacter();
                }
            }
            else
            {
                frontUI.ResetCharacter();
                GameOver();
            }
        }

        else if (isdead == right)
        {
            if (!left.dead)
            {
                rightUI.SetNewCharacter(left);
                if (!back.dead)
                {
                    leftUI.SetNewCharacter(back);
                    backUI.ResetCharacter();
                }
                else
                {
                    leftUI.ResetCharacter();
                }
            }
            else
            {
                rightUI.ResetCharacter();
            }
        }

        else if (isdead == left)
        {
            if (!back.dead)
            {
                leftUI.SetNewCharacter(back);
                backUI.ResetCharacter();
            }
            else
            {
                leftUI.ResetCharacter();
            }
        }

        else if (isdead == back)
        {
            backUI.ResetCharacter();
        }
    }

    private void GameOver()
    {

    }
}
