using UnityEngine;

public class Units : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform[] playerSpawnPositions;
    public Transform[] enemySpawnPositions;

    private GameObject[] playerUnits;
    private GameObject[] enemyUnits;

    public enum Owner { Player, Enemy }
    public Owner unitOwner;
    void Start()
    {
        InitializeUnits();
    }

    void InitializeUnits()
    {
        playerUnits = new GameObject[playerSpawnPositions.Length * 5]; // Assuming 5 units per state
        enemyUnits = new GameObject[enemySpawnPositions.Length * 2]; // Assuming 2 units per state

        SpawnUnits(playerSpawnPositions, playerUnits, Owner.Player);
        SpawnUnits(enemySpawnPositions, enemyUnits, Owner.Enemy);
    }

    void SpawnUnits(Transform[] spawnPositions, GameObject[] units, Owner owner)
    {
        int index = 0;

        for (int i = 0; i < spawnPositions.Length; i++)
        { 
            GameObject newUnit = Instantiate(unitPrefab, spawnPositions[i].position, Quaternion.identity);
            Units unitComponent = newUnit.GetComponent<Units>();

            if (unitComponent != null)
            {
                unitComponent.unitOwner = owner;
                // Set other attributes as needed
                units[index] = newUnit; // Assign the instantiated unit to the array
                index++;
            }
            else
            {
                Debug.LogError("Units component not found on the instantiated prefab.");
            }

            if (index >= units.Length) // Break the loop if all units are instantiated
            {
                break;
            }
        }
    }
}
