using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cashier : MonoBehaviour
{
    private List<string> productsName;
    private List<int> productsCount;
    private List<float> productsPrice;

    public float FontSize;
    public GameObject PlayerDialogueWindow;
    public GameObject CashierDialogueWindow;
    public GameObject CheckList;
    public GameObject[] ProductsInCheckView;
    public Answer[] Answers;
    public Text Question;
    public Text FullPriceText;

    private int firstProductInListView;
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
        productsCount = new List<int>();
        productsPrice = new List<float>();
        
        firstProductInListView = 0;
        ChangeProductIconTo(0);
    }
    
    void Update()
    {
        
    }
    
    public void ChangeProductIconTo(int i)
    {
        if (productsName.Count > 4)
        {
            for (int k = 0; k < 4; k++)
            {
                ProductsInCheckView[k].SetActive(true);
                ProductsInCheckView[k].GetComponentInChildren<Text>().text = productsName[firstProductInListView + k] + " х " + productsCount[firstProductInListView + k].ToString();
            }
            if ((i == -1 && firstProductInListView > 0) || (i == 1 && firstProductInListView < productsName.Count - 4))
            {
                firstProductInListView += i;
                for (int j = 0; j < 4; j++)
                {
                    ProductsInCheckView[j].GetComponentInChildren<Text>().text = productsName[firstProductInListView + j] + " х " + productsCount[firstProductInListView + j].ToString();
                }
            }
        }
        else
        {
            if (productsName.Count == 0)
            {
                for (int k = 0; k < 4; k++)
                {
                    ProductsInCheckView[k].SetActive(false);
                }
            }
            else
            {
                for (int j = 0; j < productsName.Count; j++)
                {
                    ProductsInCheckView[j].SetActive(true);
                    ProductsInCheckView[j].GetComponentInChildren<Text>().text = productsName[j] + " х " + productsCount[j].ToString();
                }
                for (int k = productsName.Count; k < 4; k++)
                {
                    ProductsInCheckView[k].SetActive(false);
                }
            }
        }
        
    }

    public void AddProduct(InteractableObject product)
    {
        if (productsName.Contains(product.objectName))
        {
            productsCount[productsName.IndexOf(product.objectName)] += 1;
            productsPrice[productsName.IndexOf(product.objectName)] += product.objectPrice;
            fullPrice += product.objectPrice;
        }
        else
        {
            differentProductsCount++;
            productsName.Add(product.objectName);
            productsCount.Add(1);
            productsPrice.Add(product.objectPrice);
            fullPrice += product.objectPrice;
        }

        FullPriceText.text = ((float)fullPrice).ToString();
        ChangeProductIconTo(firstProductInListView);
    }

    public void RemoveProduct(int productInCheckIndex)
    {
        int productIndex = productInCheckIndex + firstProductInListView;
        if (productsCount[productIndex] > 1)
        {
            productsPrice[productIndex] -= productsPrice[productIndex] / productsCount[productIndex];
            productsCount[productIndex] -= 1;
            fullPrice -= productsPrice[productIndex] / productsCount[productIndex];
            ProductsInCheckView[productInCheckIndex].GetComponentInChildren<Text>().text = productsName[productIndex] + " х " + productsCount[productIndex].ToString();
        }
        else
        {
            fullPrice -= productsPrice[productIndex];
            differentProductsCount--;
            productsName.Remove(productsName[productIndex]);
            productsCount.Remove(productsCount[productIndex]);
            productsPrice.Remove(productsPrice[productIndex]);
            ChangeProductIconTo(Mathf.Max(firstProductInListView - 1, 0));
            //if (productsName.Count > 1)
            //{
            //    for (int i = productIndex; i < productsName.Count; i++)
            //    {
            //        productsName[productIndex] = productsName[productIndex + 1];
            //        productsCount[productIndex] = productsCount[productIndex + 1];
            //        productsPrice[productIndex] = productsPrice[productIndex + 1];
            //    }
            //}
        }

        FullPriceText.text = ((float)fullPrice).ToString();
    }

    public void AddProduct(int productInCheckIndex)
    {
        int productIndex = productInCheckIndex + firstProductInListView;

        productsPrice[productIndex] += productsPrice[productIndex] / productsCount[productIndex];
        productsCount[productIndex] += 1;
        fullPrice += productsPrice[productIndex] / productsCount[productIndex];
        FullPriceText.text = ((float)fullPrice).ToString();

        ProductsInCheckView[productInCheckIndex].GetComponentInChildren<Text>().text = productsName[productIndex] + " х " + productsCount[productIndex].ToString();
    }

    public void StartScenario(int i)
    {
        PlayerDialogueWindow.SetActive(true);
        CashierDialogueWindow.SetActive(true);
        CheckList.SetActive(false);
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
            Answers[1].AnswerText.text = "Я бы хотел оплатить все " + ((float)fullPrice).ToString() + " P.";
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
            CheckList.SetActive(true);
            ChangeProductIconTo(firstProductInListView);

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
                Question.text = "С вас " + ((float)(fullPrice)).ToString() + " Р. Спасибо за покупку! Приходите еще!";
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
