using UnityEngine;

public class PegarPowerUp : MonoBehaviour
{
    public TipoPowerUp tipo = TipoPowerUp.MultiShot;
    public float duracao = 8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;

        Destroy(other.gameObject);

        switch (tipo)
        {
            case TipoPowerUp.MultiShot:
            {
                var shooter = FindObjectOfType<ShooterSimples>();
                if (shooter) shooter.EnableMultiShotFor(duracao);
                break;
            }
            case TipoPowerUp.Shield:
            {
                var shield = FindObjectOfType<ShieldController>();
                if (shield) shield.EnableShieldFor(duracao);
                break;
            }
            case TipoPowerUp.Dash:
            {
                break;
            }
        }

        Destroy(gameObject);
    }
}
