using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI suggestField;
    [SerializeField] private TextMeshProUGUI characterParams;
    [SerializeField] private PawnManager pawnManager;

    void Start()
    {
        
    }

    void Update()
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

    public void ShowHealth(string health)
    {
        if (health != "DEAD")
        {
            characterParams.text = "Health : " + health;
        }
        else
        {
            characterParams.text = health;
        }
        
    }
}
