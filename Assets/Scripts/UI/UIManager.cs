using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI suggestField;

    void Start()
    {
        
    }

    public void ShowSuggestion(string suggestion)
    {
        suggestField.text = suggestion;
    }

    public void CloseSuggestion()
    {
        suggestField.text = "";
    }
}
