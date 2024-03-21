using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceDataSave
{
    void LoadData(GameDataSave data);
    void SaveData(ref GameDataSave data);
}
