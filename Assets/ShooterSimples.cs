using UnityEngine;

public class ShooterSimples : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float fireRate = 8f;

    //multishot temporário
    bool multiShotOn = false; 
    public int extraBullets = 2;
    public float spreadAngle = 18f;

    float _next;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _next)
        {
            _next = Time.time + 1f / fireRate;

            int count = 1 + (multiShotOn ? extraBullets : 0);
            FireSpread(count);
        }
    }

    public void EnableMultiShotFor(float seconds)
    {
        StopAllCoroutines();
        StartCoroutine(MultiShotRoutine(seconds));
    }

    System.Collections.IEnumerator MultiShotRoutine(float seconds)
    {
        multiShotOn = true;
        yield return new WaitForSeconds(seconds);
        multiShotOn = false;
    }

    void FireSpread(int count)
    {
        if (count <= 1)
        {
            var go = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            go.transform.up = muzzle.up;
            return;
        }

        float step = spreadAngle / (count - 1);
        float start = -spreadAngle * 0.5f;

        for (int i = 0; i < count; i++)
        {
            float angle = start + step * i;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward) * muzzle.rotation;
            var go = Instantiate(bulletPrefab, muzzle.position, rot);
            go.transform.up = rot * Vector3.up;
        }
    }
}
