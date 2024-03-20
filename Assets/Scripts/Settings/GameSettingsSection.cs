using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsSection
{
    private string sectionName;
    private GameSetting[] settings;

    public GameSettingsSection(string sectionName, GameSetting[] settings)
    {
        this.sectionName = sectionName;
        this.settings = settings;
    }
}
