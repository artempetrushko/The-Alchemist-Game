using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HealthBarState
{
    [SerializeField]
    private float minHealthPercentage;
    [SerializeField] 
    private float maxHealthPercentage;
    [Space, SerializeField]
    private Sprite healthBarSprite;
    [SerializeField]
    private Sprite healthBarFillingAreaSprite;

    public bool TrySetHealthBarState(float healthPercentage, Image healthbar, Image healthBarFillingArea)
    {
        if (healthPercentage >= minHealthPercentage && healthPercentage <= maxHealthPercentage)
        {
            healthbar.sprite = healthBarSprite;
            healthBarFillingArea.sprite = healthBarFillingAreaSprite;
            return true;
        }
        return false;
    }
}
