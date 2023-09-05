using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    public float delayInSeconds = 1;

    private void OnEnable()
    {
        Destroy(gameObject, delayInSeconds);
    }
}