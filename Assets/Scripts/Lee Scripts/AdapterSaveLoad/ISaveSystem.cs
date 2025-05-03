// Created by Lee Fischer
// 5/3/25

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public interface ISaveSystem
{
    void SavePlayerData(PlayerData playerData);

    PlayerData LoadPlayerData();
}
