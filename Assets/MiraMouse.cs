using UnityEngine;

public class MiraMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        Vector2 dir = (mouse - transform.position).normalized;

        if (dir.sqrMagnitude > 0.0001f)
            transform.up = dir;
    }
}