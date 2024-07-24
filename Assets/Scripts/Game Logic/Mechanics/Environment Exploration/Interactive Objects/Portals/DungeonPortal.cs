using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortalState
{
    Unstable,
    Stabilizing,
    Stable
}

public class DungeonPortal : InteractiveObject
{
    public event Action StabilizationStarted; // PortalInteractionDisabled + ������� ������������� ������� �� ���������
    public event Action PortalInteractionDisabled; // ��������� ��������� � Interactive Object Info View

    [Space, SerializeField]
    private int nextSceneIndex;
    [SerializeField, Tooltip("��������, ���� ������� ������� ��������� � ������")]
    private bool isLastLevel;
    [Space, SerializeField]
    private GameObject stabilizationEffect;
    [SerializeField]
    private int stabilizationTimeInSeconds;
    [Space, SerializeField]
    private EnemiesSpawner enemiesSpawner;
    [SerializeField]
    private int spawnerAppearanceDelayInSeconds;

    public int NextSceneIndex => nextSceneIndex;
    public bool IsLastLevel => isLastLevel;
    public int StabilizationTimeInSeconds => stabilizationTimeInSeconds;
    public PortalState PortalState { get; private set; }

    public void SetInteractionAvailability(bool isInteractable) => GetComponent<Collider>().enabled = isInteractable;

    public void StartStabilization(bool isStabilizerAvailable)
    {
        if (PortalState == PortalState.Unstable && isStabilizerAvailable)
        {
            PortalState = PortalState.Stabilizing;
            StabilizationStarted?.Invoke();
            StartCoroutine(StabilizePortal_COR());
        }       
    }

    private IEnumerator StabilizePortal_COR()
    {
        stabilizationEffect.SetActive(true);
        yield return StartCoroutine(ActivateEnemiesSpawner_COR());
        stabilizationEffect.SetActive(false);
        PortalState = PortalState.Stable;
        SetInteractionAvailability(true);
    }

    private IEnumerator ActivateEnemiesSpawner_COR()
    {
        yield return new WaitForSeconds(spawnerAppearanceDelayInSeconds);
        enemiesSpawner.gameObject.SetActive(true);
        yield return new WaitForSeconds(stabilizationTimeInSeconds - spawnerAppearanceDelayInSeconds);
        enemiesSpawner.gameObject.SetActive(false);
    }
}