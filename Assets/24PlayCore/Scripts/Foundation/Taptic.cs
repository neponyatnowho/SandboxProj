using UnityEngine;
using Lofelt.NiceVibrations;

public class Taptic
{
    public static bool TapticOn
    {
        get
        {
            return HapticController.hapticsEnabled;
        }
        set
        {
            HapticController.hapticsEnabled = value;
        }
    }

    public static void Warning()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
    }

    public static void Failure()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
    }

    public static void Success()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
    }

    public static void Selection()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
    }

    public static void Light()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }

    public static void Medium()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
    }

    public static void Heavy()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
    }

    public static void SoftImpact()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
    }

    public static void RigidImpact()
    {
        if (!TapticOn || Application.isEditor)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
    }

    public static void Default()
    {
        if (!TapticOn || Application.isEditor)
            return;

#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }
}
