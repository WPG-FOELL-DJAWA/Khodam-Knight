using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    [SerializeField] private SoundName soundName;
    [SerializeField] private GameObject effect;

    private void EnableWeapon(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioManager.instance.playSFX1(soundName);
            weaponLogic.SetActive(true);
        }
    }

    private void DisableWeapon(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            weaponLogic.SetActive(false);
        }
    }

    private void EnableParticle(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            effect.SetActive(true);
        }
    }

    private void DisableParticle(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            effect.SetActive(false);
        }
    }
}
