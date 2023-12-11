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
        InvokeRepeating("test", 1, 0.3f);
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
            if (randomchar >= 0 && randomchar <= 1 && !healer.dead)
            {
                characterSelected = healer;
            }
            else if (randomchar >= 1 && randomchar < 21 && !tireur.dead)
            {
                characterSelected = tireur;
            }
            else if (randomchar >= 21 && randomchar < 41 && !hacker.dead)
            {
                characterSelected = hacker;
            }
            else if (randomchar >= 41 && randomchar <= 100f && !grosBras.dead)
            {
                characterSelected = grosBras;
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
            frontUI.SetNewCharacter(rightUI.currentCharacter);
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
            leftUI.SetNewCharacter(newChar);
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
