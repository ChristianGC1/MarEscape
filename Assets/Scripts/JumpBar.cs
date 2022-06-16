using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxJump(int jump)
    {
        slider.maxValue = jump;

        slider.value = jump;
    }

    public void SetJump(int jump)
    {
        slider.value = jump;
    }
}
