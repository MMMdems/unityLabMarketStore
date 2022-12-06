using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public Cashier Cashier;
    public Text AnswerText;
    public int scenarioIndex;

    public void SetScenario()
    {
        Cashier.StartScenario(scenarioIndex);
    }
}
