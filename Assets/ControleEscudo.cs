using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{
    [Header("Ref visual (filho)")]
    public GameObject shieldVisual;

    bool _on;
    Coroutine _co;
    public bool IsOn => _on;

    public void EnableShieldFor(float seconds)
    {
        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(ShieldRoutine(seconds));
    }

    IEnumerator ShieldRoutine(float seconds)
    {
        Set(true);
        yield return new WaitForSeconds(seconds);
        Set(false);
        _co = null;
    }

    public void Set(bool on)
    {
        _on = on;
        if (shieldVisual) shieldVisual.SetActive(on);
    }
}