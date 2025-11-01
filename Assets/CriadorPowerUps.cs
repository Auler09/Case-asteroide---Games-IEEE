using UnityEngine;

public class CriadorPowerUps : MonoBehaviour
{
    [Header("Quais prefabs sortear")]
    public GameObject[] powerUpPrefabs;

    [Header("Tempo")]
    public float atrasoInicial = 3f;
    public float intervalo = 7f;

    [Header("Limite simultâneo (0 = sem limite)")]
    public int maxSimultaneos = 0;

    void Start()
    {
        InvokeRepeating(nameof(Criar), atrasoInicial, intervalo);
    }

    void Criar()
    {
        if (powerUpPrefabs == null || powerUpPrefabs.Length == 0) return;

        if (maxSimultaneos > 0)
        {
            int atuais = FindObjectsOfType<PegarPowerUp>().Length;
            if (atuais >= maxSimultaneos) return;
        }

        Camera cam = Camera.main;
        if (!cam) return;

        float h = cam.orthographicSize * 2f;
        float w = h * cam.aspect;

        int side = Random.Range(0, 4);
        float off = 0.6f;
        Vector3 pos;
        switch (side)
        {
            case 0: pos = new Vector3(-w * 0.5f - off, Random.Range(-h * 0.5f, h * 0.5f), 0f); break;
            case 1: pos = new Vector3( w * 0.5f + off, Random.Range(-h * 0.5f, h * 0.5f), 0f); break;
            case 2: pos = new Vector3(Random.Range(-w * 0.5f, w * 0.5f), -h * 0.5f - off, 0f); break;
            default: pos = new Vector3(Random.Range(-w * 0.5f, w * 0.5f),  h * 0.5f + off, 0f); break;
        }

        var prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Instantiate(prefab, pos, Quaternion.identity);
    }
}