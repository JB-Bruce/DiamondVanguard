using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] public AudioClip massWiffSFX;
    [SerializeField] public AudioClip massHitSFX;
    [SerializeField] public AudioClip hitSFX;
    [SerializeField] public AudioClip slashWiffSFX;
    [SerializeField] public AudioClip slashhitSFX;
    [SerializeField] public AudioClip makarovSFX;
    [SerializeField] public AudioClip shotgunSFX;
    [SerializeField] public AudioClip sniperSFX;
    [SerializeField] public AudioClip healSFX;
    [SerializeField] public AudioClip dyingSFX;

    public static CharactersControler instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        grosBras.controler = this;
        tireur.controler = this;
        hacker.controler = this;
        healer.controler = this;
        //InvokeRepeating("test", 1, 0.1f);
    }

    private void test() 
    {
        TakeDamage(5);
    }

    public void ConsumeStats(CharacterEnum character, float life = 0f, float energy = 0f)
    {
        Character newChar = GetCharacterFromEnum(character);
        newChar.EnvironmentDamage(life);
        newChar.ConsumeEnergy(energy);
    }

    public bool HasStats(CharacterEnum character, float life = 0f, float energy = 0f)
    {
        Character newChar = GetCharacterFromEnum(character);
        return newChar.pv >= life && newChar.energy >= energy && !newChar.dead;
    }

    private Character GetCharacterFromEnum(CharacterEnum character)
    {
        if (grosBras.charactereType == character)
            return grosBras;
        if (tireur.charactereType == character)
            return tireur;
        if (hacker.charactereType == character)
            return hacker;
        return healer;
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

    public void HealGroup(float amount)
    {
        healer.Heal(amount);
        grosBras.Heal(amount);
        tireur.Heal(amount);
        hacker.Heal(amount);
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
        SceneManager.LoadScene("Test_GameOver");
    }
}

public enum CharacterEnum
{
    GrosBras,
    Tireur,
    Hacker,
    CharcuDoc
}
