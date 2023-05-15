using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CookingPot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] SceneMana sceneMana;
    [SerializeField] GameObject glowEffect;
    Animator glowAnimator;

    [Header("Anims")]
    [SerializeField] GameObject ManaAnim;
    [SerializeField] GameObject PowerAnim;
    [SerializeField] GameObject HealthAnim;

    [SerializeField] List<SO_Ingredient> ingredients = new List<SO_Ingredient>();
    [SerializeField] int ingredientsLimit = 10;
    [SerializeField] TextMeshProUGUI currentIngredients;

    public int curMana = 0;
    public int curHealth = 0;
    public int curPower = 0;

    public float OverallScore = 0;

    SO_Ingredient lastIngredient;

    [SerializeField] RecipeBoard recipeBoard;
    public SO_Recipe currentRecipe;
    //AFTER DEBUG BACK INTO PRIVATE SET

    Coroutine delayCoroutine, delayBubbleCoroutine;

    GameManager manager;

    public delegate void OnIngredientsChanged();
    public OnIngredientsChanged onIngredientsChangedCallback;

    void Start()
    {
        manager = GameManager.Instance;
        currentRecipe = recipeBoard.recipe;
        glowAnimator = glowEffect.GetComponent<Animator>();

        ingredients = new List<SO_Ingredient>();

        Debug.Log(ingredientsLimit + " this is the current max limit of ingredients");
        if (onIngredientsChangedCallback != null)
        {
            onIngredientsChangedCallback.Invoke();
        }
        manager.ChangeGameState(GameManager.GameState.CookingState);
    }

    public void DebugUpdateRecipe()
    {
        //FOR TESTING

        if (onIngredientsChangedCallback != null)
        {
            onIngredientsChangedCallback.Invoke();
        }
        ingredients = new List<SO_Ingredient>();

        curHealth = 0;
        curMana = 0;
        curPower = 0;

        OverallScore = 0.0f;

        foreach (var item in GameObject.FindGameObjectsWithTag("Ingredient"))
        {
            Destroy(item.gameObject);
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        CookIngredient currentIngredient = eventData.pointerDrag.GetComponent<CookIngredient>();

        //Debug.Log("Dropped: " + currentIngredient.ingredient.ingredientName);

        currentIngredient.IsOnTheke = true;

        //SFX
        if (glowEffect != null)
        {
            glowAnimator.SetTrigger("EndGlow");
            delayCoroutine = StartCoroutine(DelayUntilDeactivation());
        }

        if (ingredients.Count < ingredientsLimit)
        {
            eventData.pointerDrag.gameObject.transform.SetParent(currentIngredient.AfterOnTheke);
            
            if (!currentIngredient.HasBeenOnTheke && currentIngredient.IsOnTheke)
            {
                ingredients.Add(currentIngredient.ingredient);

                UpdateUIText();

                AddToScore(currentIngredient);

                currentIngredient.HasBeenOnTheke = true;
            }
            lastIngredient = currentIngredient.ingredient;
        }
        else
        {
            Destroy(eventData.pointerDrag);
        }
        
    }

    private void AddToScore(CookIngredient currentIngredient)
    {
        curHealth += currentIngredient.ingredient.health;
        curMana += currentIngredient.ingredient.mana;
        curPower += currentIngredient.ingredient.power;

        if (onIngredientsChangedCallback != null)
        {
            onIngredientsChangedCallback.Invoke();
        }

    }

    private void UpdateUIText()
    {
        currentIngredients.SetText((ingredients.Count) + "/" + ingredientsLimit);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CheckForCookIngredientOnPot(eventData))
        {
            if(glowEffect != null)
            {
                if(delayCoroutine != null)
                {
                    StopCoroutine(delayCoroutine);
                    glowAnimator.Play("GlowAnim");
                }
                
                glowEffect.SetActive(true);
            }
            //eventData.pointerDrag.GetComponent<CookIngredient>().SizeUp();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CheckForCookIngredientOnPot(eventData))
        {
            if (glowEffect != null)
            {
                glowAnimator.SetTrigger("EndGlow");
                delayCoroutine = StartCoroutine(DelayUntilDeactivation());
            }
            
            //eventData.pointerDrag.GetComponent<CookIngredient>().SizeDown();
        }
    }

    IEnumerator DelayUntilDeactivation()
    {
        yield return new WaitForSeconds(0.55f);
        glowEffect.SetActive(false);
    }

    bool CheckForCookIngredientOnPot(PointerEventData _eventData)
    {
        if (_eventData.pointerDrag == null)
        { 
            return false; 
        }
        if (_eventData.pointerDrag.GetComponent<CookIngredient>() == null)
        {
            return false;
        }

        return true;
    }

    bool IngredientHasBeenAddedBefore()
    {
        return true;
    }

    public void RemoveItem(SO_Ingredient _ingredient)
    {
        List<SO_Ingredient> _tempList = new List<SO_Ingredient>();
        bool itemDeleted = false;
        foreach(var item in ingredients)
        {
            if (item.CompareIngredient(_ingredient) && !itemDeleted)
            {
                itemDeleted = true;
            }
            else
            {
                _tempList.Add(item);
            }
        }

        curHealth -= _ingredient.health;
        curMana -= _ingredient.mana;
        curPower -= _ingredient.power;

        ingredients = new List<SO_Ingredient>();
        ingredients = _tempList;
        UpdateUIText();

        if (onIngredientsChangedCallback != null)
        {
            onIngredientsChangedCallback.Invoke();
        }
    }
   
    float Scoring()
    {
        int manaDiff = Mathf.Abs(currentRecipe.mana - curMana);
        float finalManaScore = CalculateFinalScore(manaDiff);

        int healthDiff = Mathf.Abs(currentRecipe.health - curHealth);
        float finalHealthScore = CalculateFinalScore(healthDiff);
        
        int powerDiff = Mathf.Abs(currentRecipe.power - curPower);
        float finalPowerScore = CalculateFinalScore(powerDiff);
        

        float maxScore = 300.0f;
        float onepercent = maxScore / 100.0f;

        if (currentRecipe.mana < 0)
        {
            finalManaScore = Mathf.Abs(finalManaScore);
        }
        if (currentRecipe.health < 0)
        {
            finalHealthScore = Mathf.Abs(finalHealthScore);
        }
        if (currentRecipe.power < 0)
        {
            finalPowerScore = Mathf.Abs(finalPowerScore);
        }

        if(finalManaScore < 0) { finalManaScore = 0; }
        if(finalHealthScore < 0) { finalHealthScore = 0; }
        if(finalPowerScore < 0) { finalPowerScore = 0; }

        float playerScore = finalHealthScore + finalManaScore + finalPowerScore;

        playerScore = (maxScore + playerScore)/ onepercent;

        playerScore = Mathf.Abs(100 - playerScore);

        return playerScore;
        //Debug.Log("Player Score Cooking: " + playerScore +
        //    "\nMana: " + curMana + "/" + currentRecipe.mana + "Score: " + finalManaScore +
        //    "\nPower: " + curPower + "/" + currentRecipe.power + "Score: " + finalPowerScore +
        //    "\nHealth: " + curHealth + "/" + currentRecipe.health + "Score: " + finalHealthScore);
    }

    float CalculateFinalScore(float _scoreDifference)
    {
        if (_scoreDifference <= 0)
        {
            return 100;
        }
        float _scorePercentage = (_scoreDifference / (currentRecipe.power / 100.0f));

        if (_scorePercentage > 100)
        {
            return 100 - _scorePercentage;
        }

        if (_scorePercentage < 0)
        {
            return _scorePercentage;
        }
        return 100 - _scorePercentage;
    }

    public void StartCooking()
    {
        if (currentRecipe.ContainsRecipe(ingredients))
        { 
            OverallScore =  Scoring();
            manager.CookingGameScore = OverallScore;
            sceneMana.LoadNextScene("TransitionScene");
        }
    }
}
