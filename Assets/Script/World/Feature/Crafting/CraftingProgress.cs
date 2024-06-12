//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: CraftingProgress.cs
//  Description: Script for handle crafting progress slider
//
//  History:
//  - November 8, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CraftingProgress : MonoBehaviour
{
    [SerializeField] private Crafting _crafting;
    [SerializeField] private CraftingMaterialUI _craftingMaterialUI;

    [Space(10)]
    [SerializeField] private GameObject _rayCastBlock;
    [SerializeField] private Slider _slider;

    public IEnumerator startCraft()
    {
        _rayCastBlock.SetActive(true);
        _slider.value = 0;
        _slider.maxValue = 10;
        _crafting.startCraftAnimation("Craft Progress");
        while (_slider.value < _slider.maxValue)
        {
            _slider.value += Time.deltaTime * 2;

            yield return null;
        }
        _craftingMaterialUI.craftingProgressDone();

    }

    //call in CraftingMaterialUI
    public void closeRayCastBlock()
    {
        _rayCastBlock.SetActive(false);
    }
}
