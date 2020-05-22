using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    private void Start()
    {
        //create player UI
        playerUIInstance = Instantiate(playerUIPrefab);
        playerUIInstance.name = playerUIPrefab.name;

        FindObjectOfType<GameManager>().SetPlayerUIInstance(playerUIInstance);

        //configure ui
        PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
        if(ui == null)
        {
            Debug.LogError("Error: No player UI component on PlayerUI prefab.");
        }
        ui.SetPlayer(GetComponent<Player>());

    }
}