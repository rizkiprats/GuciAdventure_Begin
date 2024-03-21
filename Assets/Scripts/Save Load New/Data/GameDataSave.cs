using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataSave
{
    [Header("Timer Saved")]
    public float timerSaved;

    [Header("Player Position Saved")]
    public Vector3 playerPositionSaved;

    [Header("Scene Number Saved in Index")]
    public int sceneNumberSaved;

    [Header("Pressure Plate Active Saved")]
    public bool isOn_PressurePlateSaved;

    [Header("Door Active Saved")]
    public bool doorOpenSaved;

    [Header("Following Key Active Saved")]
    public bool isFollowing_KeySaved;

    [Header("Box Pressure Plate Active And Position Saved")]
    public Vector3 boxPressurePlate_PosSaved;
    public bool boxPressurePlate_PressedSaved;

    [Header("Plate Position Saved")]
    public Vector3 plate_PosSaved;

    [Header("Jurnal Items in Index Active Saved")]
    public bool[] JurnalItem_EnabledSaved;

    [Header("Level Progress Saved")]
    public LevelProgress levelProgressSaved;

    [Header("Number of Obtained Items in Level Saved")]
    public int Obtained_Object_Count_Saved;

    [Header("Camera Saved Position")]
    public Vector3 cameraPos;

    public GameDataSave()
    {
        this.isOn_PressurePlateSaved = false;
        
        this.sceneNumberSaved = 0;
        
        this.timerSaved = 60;

        this.playerPositionSaved = Vector3.zero;
        
        this.doorOpenSaved = false;
        this.isFollowing_KeySaved = false;
        
        this.boxPressurePlate_PosSaved = Vector3.zero;
        
        this.boxPressurePlate_PressedSaved = false;
        
        this.plate_PosSaved = Vector3.zero;
        
        this.JurnalItem_EnabledSaved = new bool[0];

        this.levelProgressSaved = new LevelProgress();

        this.Obtained_Object_Count_Saved = 0;

        this.cameraPos = Vector3.zero;
    }
}