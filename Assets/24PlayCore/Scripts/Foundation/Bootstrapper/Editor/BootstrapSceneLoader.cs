using UnityEditor.SceneManagement;
using UnityEngine;

public static class BootstrapSceneLoader
{
    public static bool StartFromBoot { get; set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadDefaultScene()
    {
        if (DontStartFromBoot())
            return;

        DisableCurrentOpenedSceneMonoBehaviours();
        EditorSceneManager.LoadScene(0);
    }

    private static void DisableCurrentOpenedSceneMonoBehaviours()
    {
        MonoBehaviour[] monoBehaviours = Object.FindObjectsOfType<MonoBehaviour>();
        foreach (var mono in monoBehaviours)
            mono.SetInactive();
    }

    private static bool DontStartFromBoot() =>
        !StartFromBoot || EditorSceneManager.GetActiveScene().buildIndex == 0;
}