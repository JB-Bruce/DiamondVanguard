using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIStatsUpdater : MonoBehaviour
{
    public Slider slider;

    public void gaugeUi(int amount)
    {
        slider.value = amount;
    }
}
