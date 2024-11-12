using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public int multiplierAmount = 2;
    private TextMeshProUGUI textUI;
    void Start()
    {
        textUI = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textUI.text = multiplierAmount.ToString() + "X";
    }
}
