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
        TakeDamage(5);
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
        if (frontUI.currentCharacter != null && isdead == frontUI.currentCharacter)
        {
            print("dead front");
            ResetFront();
        }

        else if (rightUI.currentCharacter != null && isdead == rightUI.currentCharacter)
        {
            print("dead right");
            ResetRight();
        }

        else if (leftUI.currentCharacter != null && isdead == leftUI.currentCharacter)
        {
            print("dead left");
            ResetLeft();
        }

        else if (backUI.currentCharacter != null && isdead == backUI.currentCharacter)
        {
            print("dead back");
            ResetBack();
        }
    }

    private void ResetFront()
    {
        frontUI.ResetCharacter();
        if(rightUI.currentCharacter != null && !rightUI.currentCharacter.dead)
        {
            ResetRight();
            frontUI.SetNewCharacter(right);
        }
        else
        {
            GameOver();
        }
    }

    private void ResetRight()
    {
        rightUI.ResetCharacter();
        if (leftUI.currentCharacter != null && !leftUI.currentCharacter.dead)
        {
            ResetLeft();
            rightUI.SetNewCharacter(left);
        }
    }

    private void ResetLeft()
    {
        leftUI.ResetCharacter();
        if (backUI.currentCharacter != null && !backUI.currentCharacter.dead)
        {
            ResetBack();
            leftUI.SetNewCharacter(back);
        }
    }

    private void ResetBack()
    {
        backUI.ResetCharacter();
    }

    private void GameOver()
    {
        print("GAMEOVER");
    }
}
