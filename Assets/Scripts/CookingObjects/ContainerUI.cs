using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    public Canvas canvas;
    public Image[] containedIngredientsImages;
    [SerializeField] private Sprite noIngredientSprite;

    private void Awake()
    {
        RotateToCamera(Camera.main.transform);
    }

    public void Initialize(int maxIngredients)
    {
        for (int i = 0; i < containedIngredientsImages.Length; i++)
        {
            if (i >= maxIngredients)
                containedIngredientsImages[i].enabled = false;
        }
    }

    public void RotateToCamera(Transform camera)
    {
        canvas.transform.LookAt(camera);
    }

    public void UpdateIngredients(List<IngredientScript> ingredients)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i] != null)
                containedIngredientsImages[i].sprite = ingredients[i].data.icon;
            else
                containedIngredientsImages[i].sprite = noIngredientSprite;
        }
    }
}
