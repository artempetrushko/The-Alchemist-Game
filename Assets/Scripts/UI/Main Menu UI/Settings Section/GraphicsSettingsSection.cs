using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphicsSettingsSection : MonoBehaviour
{
    [SerializeField]
    private SettingView settingViewPrefab;
    [SerializeField]
    private GameObject settingViewsContainer;
    
    public SettingView CreateSettingView()
    {
        var settingView = Instantiate(settingViewPrefab, settingViewsContainer.transform);
        return settingView;
    }
}
