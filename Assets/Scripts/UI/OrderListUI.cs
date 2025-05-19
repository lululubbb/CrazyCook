using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{

    [SerializeField] private Transform recipeParent;
    [SerializeField] private RecipeUI recipeUITemplate;

    private void Start()
    {
        recipeUITemplate.gameObject.SetActive(false);
        OrderMananger.Instance.OnRecipeSpawned += OrderManager_OnRecipeSpawned;
        OrderMananger.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
    }

    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void OrderManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform child in recipeParent)
        {
            if (child != recipeUITemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }

        List<RecipeSO> recipeSOList = OrderMananger.Instance.GetOrderList();
        foreach (RecipeSO recipeSO in recipeSOList)
        {
            RecipeUI recipeUI = GameObject.Instantiate(recipeUITemplate);
            recipeUI.transform.SetParent(recipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateUI(recipeSO);
        }
    }

}
