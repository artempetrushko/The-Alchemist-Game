using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounterView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthCounter;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image healthBarFillingArea;
    [SerializeField]
    private List<HealthBarState> healthBarStates;

    private PlayerState playerState;

    private void OnEnable()
    {
        playerState = FindObjectOfType<PlayerState>();
        playerState.HealthParamsChanged += SetHealthInfo;
    }

    private void OnDisable()
    {
        playerState.HealthParamsChanged -= SetHealthInfo;
    }

    private void SetHealthInfo((int health, int maxHealth) playerHealthParams)
    {
        healthCounter.text = string.Format("{0}/{1}", playerHealthParams.health.ToString(), playerHealthParams.maxHealth.ToString());
        var playerHealthRatio = (float)playerHealthParams.health / playerHealthParams.maxHealth;
        foreach (var state in healthBarStates)
        {
            if (state.TrySetHealthBarState(playerHealthRatio * 100, healthBar, healthBarFillingArea))
            {
                break;
            }
        }
        healthBarFillingArea.fillAmount = playerHealthRatio;
    }
}
