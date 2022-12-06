using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cashier : MonoBehaviour
{
    private List<string> productsName;
    private List<float> productsCount;

    public float FontSize;
    public GameObject PlayerDialogueWindow;
    public GameObject CashierDialogueWindow;
    public Answer[] Answers;
    public Text Question;
    public Text FullPriceText;

    private int differentProductsCount;
    private float fullPrice;

    void Start()
    {
        PlayerDialogueWindow.SetActive(false);
        CashierDialogueWindow.SetActive(false);
        differentProductsCount = 0;
        fullPrice = 0;
        FullPriceText.text = "0.00";
        productsName = new List<string>();
        productsCount = new List<float>();
    }
    
    void Update()
    {
        
    }

    public void AddProduct(InteractableObject product)
    {
        if(productsName.Contains(product.objectName))
        {
            productsCount[productsName.IndexOf(product.objectName)] += 1;
            fullPrice += product.objectPrice;
        }
        else
        {
            differentProductsCount++;
            productsName.Add(product.objectName);
            productsCount.Add(1);
            fullPrice += product.objectPrice;
        }

        FullPriceText.text = fullPrice.ToString();
    }

    public void RemoveProduct(InteractableObject product)
    {
        if (productsCount[productsName.IndexOf(product.objectName)] > 1)
        {
            productsCount[productsName.IndexOf(product.objectName)] -= 1;
            fullPrice -= product.objectPrice;
        }
        else
        {
            fullPrice -= product.objectPrice;
            differentProductsCount--;
            for (int i = 0; i < productsName.Capacity - 1; i++)
            {
                productsName[i] = productsName[i + 1];
                productsCount[i] = productsCount[i + 1];
            }
            productsName.Remove(productsName[-1]);
            productsCount.Remove(productsCount[-1]);
        }

        FullPriceText.text = fullPrice.ToString();
    }

    public void StartScenario(int i)
    {
        PlayerDialogueWindow.SetActive(true);
        CashierDialogueWindow.SetActive(true);
        if (i == 0)
        {
            PlayerDialogueWindow.SetActive(false);
            CashierDialogueWindow.SetActive(false);
        }
        else if(i == 1)
        {
            Question.text = "������������, ��� ���� ��� ������?";

            Answers[0].gameObject.SetActive(true);
            Answers[0].AnswerText.text = "� �� �����, ����� �� �������� ������ ���� �������";
            Answers[0].scenarioIndex = 2;
            Answers[1].gameObject.SetActive(true);
            Answers[1].AnswerText.text = "� �� ����� �������� ��� " + fullPrice.ToString() + " P.";
            Answers[1].scenarioIndex = 3;
            Answers[2].gameObject.SetActive(true);
            Answers[2].AnswerText.text = "��������, ����������, ��� �����";
            Answers[2].scenarioIndex = 4;
            Answers[3].gameObject.SetActive(true);
            Answers[3].AnswerText.text = "��������, ��� �����������";
            Answers[3].scenarioIndex = 5;
        }
        else if (i == 2)
        {
            Question.text = "��� ��� ������ ������������ �������. ������� ��������?";

            Answers[0].gameObject.SetActive(true);
            Answers[0].AnswerText.text = "��, �������� � ����� ����������";
            Answers[0].scenarioIndex = 3;
            Answers[1].gameObject.SetActive(true);
            Answers[1].AnswerText.text = "���, ������� ����� �� �����";
            Answers[1].scenarioIndex = 4;
            Answers[2].gameObject.SetActive(true);
            Answers[2].AnswerText.text = "�, �������, ��� �������";
            Answers[2].scenarioIndex = 5;
            Answers[3].gameObject.SetActive(false);
        }
        else
        {
            if (i == 3)
            {
                Question.text = "� ��� " + fullPrice.ToString() + " �. ������� �� �������! ��������� ���!";
                productsCount.Clear();
                productsName.Clear();
                fullPrice = 0;
                FullPriceText.text = "0.00";
            }

            else if (i == 4)
            {
                Question.text = "������, ��� �������.";
                productsCount.Clear();
                productsName.Clear();
                fullPrice = 0;
                FullPriceText.text = "0.00";
            }
            else if (i == 5) Question.text = "�������, ���� ��� ���-�� ����� �����. ����� ��������";

            Answers[0].gameObject.SetActive(true);
            Answers[0].AnswerText.text = "�������, �� ��������";
            Answers[0].scenarioIndex = 0;
            Answers[1].gameObject.SetActive(false);
            Answers[2].gameObject.SetActive(false);
            Answers[3].gameObject.SetActive(false);
        }

        for(int j = 0; j < 4; j++)
        {
            Answers[j].AnswerText.fontSize = (int)(FontSize / Answers[j].AnswerText.text.Length);
        }
        Question.fontSize = (int)(FontSize / Question.text.Length);
    }
}
