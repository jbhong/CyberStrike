using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public GameObject player;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void LateUpdate()
    {
        player = gameManager.GetPlayerInstance();

        Vector3 newPosition = player.transform.position;
        newPosition.y = transform.position.y;

        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.transform.eulerAngles.y, 0f);
    }
}
