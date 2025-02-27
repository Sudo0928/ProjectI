using UnityEngine;

public class Tear : MonoBehaviour
{
    [SerializeField] private GameObject tear;

    private void OnEnable()
    {
        EventManager.RegisterListener<PlayerAttackEvent>(Attack, 0);
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<PlayerAttackEvent>(Attack);
    }

    private void Attack(PlayerAttackEvent e)
    {
        Player player = e.player;

        Vector2 velocity = player.Rigidbody2D.velocity;
        Vector2 desiredDirection = (player.LookDirection + velocity * 0.2f);

        GameObject gameObject = Instantiate(tear);
        gameObject.transform.position = player.transform.position - new Vector3(0, 0.1f, 0);
        gameObject.GetComponent<BaseTear>().Init(player.gameObject, player.projectileSpeed, player.projectileDistance, player.projectileSize, e.direction, player.IsParbolic);
    }
}