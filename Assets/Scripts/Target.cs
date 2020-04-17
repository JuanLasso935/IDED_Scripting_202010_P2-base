using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : Subject
{

    private const float TIME_TO_DESTROY = 10F;

    [SerializeField]
    private int maxHP = 1;

    private int currentHP;

    [SerializeField]
    private int scoreAdd = 10;

    private void Start()
    {
        currentHP = maxHP;
        Destroy(gameObject, TIME_TO_DESTROY);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int collidedObjectLayer = collision.gameObject.layer;

        if (collidedObjectLayer.Equals(Utils.BulletLayer))
        {
            Notify(true);
            Destroy(collision.gameObject);

            currentHP -= 1;

            if (currentHP <= 0)
            {
                Notify(true);
                Player player = FindObjectOfType<Player>();

                if (player != null)
                {
                    Notify(true);
                    player.Score += scoreAdd;
                }

                Destroy(gameObject);
            }
        }
        else if (collidedObjectLayer.Equals(Utils.PlayerLayer) ||
            collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {
            Player player = FindObjectOfType<Player>();

            if (player != null)
            {
                Notify(true);
                player.Lives -= 1;

                if (player.Lives <= 0 && player.OnPlayerDied != null)
                {
                    player.OnPlayerDied();
                }
            }

            Destroy(gameObject);


        }
    }

}