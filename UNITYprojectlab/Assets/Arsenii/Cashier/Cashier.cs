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
            Question.text = "Здравствуйте, чем могу вам помочь?";

            Answers[0].gameObject.SetActive(true);
            Answers[0].AnswerText.text = "Я бы хотел, чтобы вы озвучили список моих покупок";
            Answers[0].scenarioIndex = 2;
            Answers[1].gameObject.SetActive(true);
            Answers[1].AnswerText.text = "Я бы хотел оплатить все " + fullPrice.ToString() + " P.";
            Answers[1].scenarioIndex = 3;
            Answers[2].gameObject.SetActive(true);
            Answers[2].AnswerText.text = "Отмените, пожалуйста, мой заказ";
            Answers[2].scenarioIndex = 4;
            Answers[3].gameObject.SetActive(true);
            Answers[3].AnswerText.text = "Простите, что побеспокоил";
            Answers[3].scenarioIndex = 5;
        }
        else if (i == 2)
        {
            Question.text = "Вот ваш список неоплаченных товаров. Желаете оплатить?";

            Answers[0].gameObject.SetActive(true);
            Answers[0].AnswerText.text = "Да, положите в пакет пожалуйста";
            Answers[0].scenarioIndex = 3;
            Answers[1].gameObject.SetActive(true);
            Answers[1].AnswerText.text = "Нет, верните товар на места";
            Answers[1].scenarioIndex = 4;
            Answers[2].gameObject.SetActive(true);
            Answers[2].AnswerText.text = "Я, пожалуй, еще подумаю";
            Answers[2].scenarioIndex = 5;
            Answers[3].gameObject.SetActive(false);
        }
        else
        {
            if (i == 3)
            {
                Question.text = "С вас " + fullPrice.ToString() + " Р. Спасибо за покупку! Приходите еще!";
                productsCount.Clear();
                productsName.Clear();
                fullPrice = 0;
                FullPriceText.text = "0.00";
            }

            else if (i == 4)
            {
                Question.text = "Хорошо, сию секунду.";
                productsCount.Clear();
                productsName.Clear();
                fullPrice = 0;
                FullPriceText.text = "0.00";
            }
            else if (i == 5) Question.text = "Скажите, если вам что-то будет нужно. Всего хорошего";

            Answers[0].gameObject.SetActive(true);
            Answers[0].AnswerText.text = "Спасибо, до свидания";
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
