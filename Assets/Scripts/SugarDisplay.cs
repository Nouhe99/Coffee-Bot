using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SugarDisplay : MonoBehaviour
{
    [SerializeField]
    private Image board;

    [SerializeField]
    private Image brownSugarCube;
    [SerializeField]
    private Image whiteSugarCube;

    [SerializeField]
    private TMP_Text brownSugarText;
    [SerializeField]
    private TMP_Text whiteSugarText;

    public void UpdateDisplay(int white, int brown)
    {
        if (white == 0)
        {
            whiteSugarText.enabled = false;
            whiteSugarCube.enabled = false;
        }
        else
        {
            whiteSugarText.enabled = true;
            whiteSugarCube.enabled = true;

            whiteSugarText.text = white + "x";
        }

        if (brown == 0)
        {
            brownSugarText.enabled = false;
            brownSugarCube.enabled = false;
        }
        else
        {
            brownSugarText.enabled = true;
            brownSugarCube.enabled = true;

            brownSugarText.text = brown + "x";
        }
    }
}
