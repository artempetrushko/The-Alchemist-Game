using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ABC_StateManager;

[System.Serializable]
public class ABC_GlobalPortal : ScriptableObject
{



        ABC_GlobalPortal()
    {

    }


    public GameObject ComponentPreset; 

    public List<GameObject> UI = new List<GameObject>();
    public List<GameObject> tagHolders = new List<GameObject>();


    public CharacterType characterType = CharacterType.Player;
    public CharacterIntegrationType characterIntegrationType = CharacterIntegrationType.ABC;
    public GameType gameType = GameType.Action;

    public bool addComponentPresets = true;

    public bool persistentCrosshairAestheticMode = false; 

    public bool setupGameTypeTargetting = true;

    public bool convertAbilitiesToGameType = false;

    public bool alwaysShowUI = true; 

    public PointClickType clickType = PointClickType.Click;

    public bool setupForWeaponsAndAbilities = true;

    public bool addUI = true; 

    public bool enableAI = true;

    public AIType typeAI = AIType.CloseCombat; 

    public bool addCamera = true;

    public bool invertYAxis = false;

    public List<GameObject> gameCameras = new List<GameObject>();

    public bool addMovement = true;

    public RuntimeAnimatorController AniController; 

    public bool enableLockOnMovement = false;

    public bool enableJumping = false; 

    public bool enableCameraRotation = true; 

    public GameObject weaponHolderAdjustmentObject;

    public GameObject gunHolderAdjustmentObject;

    public bool displayABCTemplates = true;

    public bool displayABCElements = true; 





    public enum CharacterType {
        Player, 
        Enemy, 
        Friendly
    }


    public enum AIType {
        CloseCombat,
        Ranged        
    }


    public enum CharacterIntegrationType {
        ABC, 
        GameCreator2
    }


    public enum GameType {
        Action, 
        FPS, 
        TPS, 
        RPGMMO, 
        MOBA, 
        TopDownAction
    }

    public enum PointClickType {
        Hover, 
        Click
    }


}
