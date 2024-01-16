using UnityEngine;

public class Units : MonoBehaviour
{
    public GameObject playerTroopPrefab; // Reference to the player troop prefab
    public GameObject enemyTroopPrefab; // Reference to the enemy troop prefab
    public Transform[] playerTroopSpawnPoints; // Blank GameObjects for player troop spawns
    public Transform[] enemyTroopSpawnPoints; // Blank GameObjects for enemy troop spawns

    void Start()
    {
        // Spawn player troops
        SpawnTroops(playerTroopPrefab, playerTroopSpawnPoints);

        // Spawn enemy troops
        SpawnTroops(enemyTroopPrefab, enemyTroopSpawnPoints);
    }

    void SpawnTroops(GameObject troopPrefab, Transform[] spawnPoints)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(troopPrefab, spawnPoint.position, Quaternion.identity);
            // You might want to store these instantiated troops in an array or list for further manipulation
        }
    }
}
