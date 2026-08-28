using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GameObject playerInventoryPanel;
    [SerializeField] private List<Image> inventoryItemsImages;
    [SerializeField] private Image selectedItemImage;
    [SerializeField] private Sprite unselectedItemSprite;
    [Header("PlayerStatus")]
    [SerializeField] private GameObject staminaPanel;
    [SerializeField] private Slider staminaSlider;

    public void InitializeInventoryUI(int inventorySlots)
    {
        playerInventoryPanel.SetActive(true);
        for (int i = 0; i < inventoryItemsImages.Count; i++)
        {
            if (i < inventorySlots)
            {
                inventoryItemsImages[i].gameObject.SetActive(true);
                inventoryItemsImages[i].sprite = unselectedItemSprite;
            }
            else
                inventoryItemsImages[i].gameObject.SetActive(false);
        }
    }

    public void SetInventoryItemIcon(int inventorySlot, Sprite itemIcon)
    {
        if (itemIcon != null)
            inventoryItemsImages[inventorySlot].sprite = itemIcon;
        else
            inventoryItemsImages[inventorySlot].sprite = unselectedItemSprite;
        SwitchSelectedItem(inventorySlot);
    }

    public void SwitchSelectedItem(int newSelectSlot)
    {
        selectedItemImage.transform.position = inventoryItemsImages[newSelectSlot].transform.position;
    }

    public void InitializeStaminaUI(int maxStamina)
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        staminaPanel.SetActive(true);
    }

    public void ChangeStaminaSliderValue(int staminaValue)
    {
        staminaSlider.value = staminaValue;
    }
}
