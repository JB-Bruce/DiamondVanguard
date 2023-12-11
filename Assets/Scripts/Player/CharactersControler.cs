using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersControler : MonoBehaviour
{
    [SerializeField] Character grosBras;
    [SerializeField] Character tireur;
    [SerializeField] Character hacker;
    [SerializeField] Character healer;

    [SerializeField] CharacterUIStatsUpdater frontUI;
    [SerializeField] CharacterUIStatsUpdater rightUI;
    [SerializeField] CharacterUIStatsUpdater leftUI;
    [SerializeField] CharacterUIStatsUpdater backUI;

    private void Start()
    {
        grosBras.controler = this;
        tireur.controler = this;
        hacker.controler = this;
        healer.controler = this;
    }

    private void test() 
    {
        TakeDamage(5);
    }

    public void TakeDamage(float amount)
    {
        if (healer.dead && hacker.dead && tireur.dead && grosBras.dead)
            return;

        float randomchar;

        Character characterSelected = null;
        while (characterSelected == null)
        {
            randomchar = Random.Range(0f, 100f);
            if (backUI.currentCharacter != null && randomchar >= 0 && randomchar <= 1 && !backUI.currentCharacter.dead)
            {
                characterSelected = backUI.currentCharacter;
            }
            else if (rightUI.currentCharacter != null && randomchar >= 1 && randomchar < 21 && !rightUI.currentCharacter.dead)
            {
                characterSelected = rightUI.currentCharacter;
            }
            else if (leftUI.currentCharacter != null && randomchar >= 21 && randomchar < 41 && !leftUI.currentCharacter.dead)
            {
                characterSelected = leftUI.currentCharacter;
            }
            else if (frontUI.currentCharacter != null && randomchar >= 41 && randomchar <= 100f && !frontUI.currentCharacter.dead)
            {
                characterSelected = frontUI.currentCharacter;
            }
        }
        characterSelected.TakeDamage(amount);
    }

    public void Die(Character isdead)
    {
        if (frontUI.currentCharacter != null && isdead == frontUI.currentCharacter)
        {
            ResetFront();
        }

        else if (rightUI.currentCharacter != null && isdead == rightUI.currentCharacter)
        {
            ResetRight();
        }

        else if (leftUI.currentCharacter != null && isdead == leftUI.currentCharacter)
        {
            ResetLeft();
        }

        else if (backUI.currentCharacter != null && isdead == backUI.currentCharacter)
        {
            ResetBack();
        }
    }

    private void ResetFront()
    {
        frontUI.ResetCharacter();
        if(rightUI.currentCharacter != null && !rightUI.currentCharacter.dead)
        {
            Character newChar = ResetRight();
            frontUI.SetNewCharacter(newChar);
        }
        else
        {
            GameOver();
        }
    }

    private Character ResetRight()
    {
        Character newChar = rightUI.ResetCharacter();
        if (leftUI.currentCharacter != null && !leftUI.currentCharacter.dead)
        {
            Character newChar2 = ResetLeft();
            rightUI.SetNewCharacter(newChar2);
        }
        return newChar;
    }

    private Character ResetLeft()
    {
        Character newChar = leftUI.ResetCharacter();
        if (backUI.currentCharacter != null && !backUI.currentCharacter.dead)
        {
            Character newChar2 = ResetBack();
            leftUI.SetNewCharacter(newChar2);
        }
        return newChar;
    }

    private Character ResetBack()
    {
        return backUI.ResetCharacter();
    }

    private void GameOver()
    {
        print("GAMEOVER");
    }
}
