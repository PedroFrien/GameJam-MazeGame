using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Text_SelectedTrap : MonoBehaviour
{
    public TMP_Text sampleText;

    // Update is called once per frame
    public void ChangeText(string text)
    {
        sampleText.text = text;
    }
}
