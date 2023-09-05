﻿using System.Collections;
using UnityEngine;

public static class SLS
{
    public static SaveLoadData Data => SaveLoadSystem.Instance.Data;

    public static void Save()
    {
        SaveLoadSystem.Instance.ForceSave();
    }
}

public class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
{
    private const string DataKey = "Data";

    [SerializeField] private float saveDelay = 0;

    private float delay;
    private bool needSave;

    public SaveLoadData Data { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }

    private void LoadData()
    {
        var ppData = PlayerPrefs.GetString(DataKey, "");
        if (string.IsNullOrEmpty(ppData))
            Data = new SaveLoadData();
        else
            Data = JsonUtility.FromJson<SaveLoadData>(ppData);
    }

    public void Save()
    {
        if (delay > 0)
        {
            needSave = true;
            return;
        }

        SaveData();
    }

    public void ForceSave()
    {
        SaveData();
    }

    private void SaveData()
    {
        if (Data == null)
            return;

        var json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(DataKey, json);

        needSave = false;
        StartCoroutine(SaveTimer());
    }

    private IEnumerator SaveTimer()
    {
        delay = saveDelay;

        for (; delay > 0; delay -= Time.deltaTime)
            yield return null;

        delay = 0;

        if (needSave)
            SaveData();
    }
}