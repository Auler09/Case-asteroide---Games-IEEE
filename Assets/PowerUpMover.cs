using UnityEngine;

public class PowerUpMover : MonoBehaviour
{
    [Header("Velocidade (mundo/seg)")]
    public Vector2 speedRange = new Vector2(1.6f, 2.6f);

    [Header("Vida máxima (failsafe)")]
    public float maxLifetime = 12f;

    [Header("Margem fora da tela para destruir")]
    public float killMargin = 0.9f;

    Vector3 _dir;
    float _speed;  
    float _t; 

    void OnEnable()
    {
        _speed = Random.Range(speedRange.x, speedRange.y);

       
        Camera cam = Camera.main;
        if (!cam) { _dir = Vector3.down; return; }

        float h = cam.orthographicSize * 2f;
        float w = h * cam.aspect;

        float inset = 0.2f; 
        Vector3 target = new Vector3(
            Random.Range(-w * 0.5f + inset,  w * 0.5f - inset),
            Random.Range(-h * 0.5f + inset,  h * 0.5f - inset),
            0f
        );

        _dir = (target - transform.position).normalized;
        if (_dir.sqrMagnitude < 0.0001f) _dir = Vector3.down; 
    }

    void Update()
    {
       
        transform.position += _dir * _speed * Time.deltaTime;

     
        _t += Time.deltaTime;
        if (_t >= maxLifetime) { Destroy(gameObject); return; }

       
        if (IsOutsideWithMargin()) Destroy(gameObject);
    }

    bool IsOutsideWithMargin()
    {
        Camera cam = Camera.main;
        if (!cam) return false;

        float h = cam.orthographicSize * 2f;
        float w = h * cam.aspect;

        float halfW = w * 0.5f + killMargin;
        float halfH = h * 0.5f + killMargin;

        Vector3 p = transform.position;
        return (p.x < -halfW || p.x > halfW || p.y < -halfH || p.y > halfH);
    }
}