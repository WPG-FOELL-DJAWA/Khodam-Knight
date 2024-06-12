//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: Crafting.cs
//  Description: Script for handle crafting in world
//
//  History:
//  - November 4, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(VirtualCameraSwitcher))]
public class Crafting : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private VirtualCameraSwitcher _cameraSwitcher;
    [SerializeField] private CraftingMaterialUI _craftingMaterialUI;
    [SerializeField] private GameObject _craftingUI;
    [SerializeField] private GameObject _triggerUI;

    [SerializeField] private Button _craftingUICloseButton;

    private bool onCrafting = false;

    private void Awake()
    {
        _craftingUICloseButton.onClick.AddListener(() =>
            {
                closeCraftingMode();
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _triggerUI.SetActive(true);
            onCrafting = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player") && CharacterInput.instance.actionTrigger && !onCrafting)
        {
            _craftingUI.SetActive(true);
            _triggerUI.SetActive(false);
            _cameraSwitcher.virtualMode();
            CharacterInput.instance.changePlayerState(PlayerState.Crafting);// noted : sementara langsung trigger dari sini
            onCrafting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _triggerUI.SetActive(false);
            onCrafting = false;
        }
    }


    private void closeCraftingMode()  //noted : sementara
    {
        _craftingMaterialUI.closeCrafting();
        _craftingUI.SetActive(false);
        _cameraSwitcher.roamingMode();
        CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
    }


    public void startCraftAnimation(string trigger)
    {
        _animator.SetTrigger(trigger);
    }
}
