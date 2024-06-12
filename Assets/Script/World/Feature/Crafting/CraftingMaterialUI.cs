//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: CraftingMaterialUI.cs
//  Description: Script for handle crafting material UI
//
//  History:
//  - November 7, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingMaterialUI : MonoBehaviour
{
    [SerializeField] private CraftingProgress _craftingProgress;
    [SerializeField] private Crafting _crafting;

    [Space(10)]
    [SerializeField] private GameObject _groupUI;
    [SerializeField] private Image _itemIcon;

    [Space(5)]
    [SerializeField] private Image _materialOneIcon;
    [SerializeField] private TextMeshProUGUI _materialOneAmount;

    [Space(5)]
    [SerializeField] private Image _materialTwoIcon;
    [SerializeField] private TextMeshProUGUI _materialTwoAmount;

    [Space(5)]
    [SerializeField] private Image _materialThreeIcon;
    [SerializeField] private TextMeshProUGUI _materialThreeAmount;

    [Space(10)]
    [SerializeField] private Button _craftButton;
    private ItemCrafting.Material[] _material;
    private ItemBaseSO _productCrafting;

    [Space(10)]
    [SerializeField] private Button _claimCraftingProduct;

    [Space(10)]
    [SerializeField] private Image _craftingSuccessIcon;

    private bool _craftingDone = false;


    private void Awake()
    {

        _craftButton.onClick.AddListener(() =>
        {
            StartCoroutine(_craftingProgress.startCraft()); // sementara
        });

        _claimCraftingProduct.onClick.AddListener(() =>
        {
            claimCraftingProduct();
        });
    }

    public void ShowMaterialDesc(ItemCrafting.Material[] material, ItemBaseSO productCrafting)
    {
        _groupUI.SetActive(true);
        _itemIcon.sprite = productCrafting.icon;
        _materialOneIcon.sprite = material[0].itemSO.icon;
        _materialOneAmount.text = material[0].itemSO.amountInInventory.ToString() + "/" + material[0].amount;

        _materialTwoIcon.sprite = material[1].itemSO.icon;
        _materialTwoAmount.text = material[1].itemSO.amountInInventory.ToString() + "/" + material[1].amount;

        _materialThreeIcon.sprite = material[2].itemSO.icon;
        _materialThreeAmount.text = material[2].itemSO.amountInInventory.ToString() + "/" + material[2].amount;

        if ((material[0].itemSO.amountInInventory >= material[0].amount) && (material[1].itemSO.amountInInventory >= material[1].amount)
         && (material[2].itemSO.amountInInventory >= material[2].amount))
        {
            _material = material;
            _productCrafting = productCrafting;
            _craftButton.gameObject.SetActive(true);
        }
        else
            _craftButton.gameObject.SetActive(false);
    }

    public void craftingProgressDone()
    {
        _crafting.startCraftAnimation("Craft Success");

        _material[0].itemSO.amountInInventory -= _material[0].amount;
        _material[1].itemSO.amountInInventory -= _material[1].amount;
        _material[2].itemSO.amountInInventory -= _material[2].amount;

        _craftingSuccessIcon.gameObject.SetActive(true);
        _craftingSuccessIcon.sprite = _productCrafting.icon;

        _craftingDone = true;
    }

    public void closeCrafting()
    {
        _groupUI.SetActive(false);
    }

    public void claimCraftingProduct()
    {
        if (!_craftingDone) return;
        _productCrafting.amountInInventory += 1;
        // Inventory.instance.checkItemAmountByName(_productCrafting.itemName);
        ShowMaterialDesc(_material, _productCrafting);

        _craftingSuccessIcon.gameObject.SetActive(false);
        _craftingProgress.closeRayCastBlock();

        _craftingDone = false;
    }
}


