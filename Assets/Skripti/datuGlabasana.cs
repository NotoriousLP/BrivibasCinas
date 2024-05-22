using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;
using UnityEngine;
using System;

public class datuGlabasana : MonoBehaviour
{
    public Objekti objekti;
    public SpelesKontrole kontrole;
    public Valstis.Speletaji speletaji;
    public Teksts  teksts;

    private string dbName = "URI=file:progress.db";
    void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
        teksts = FindAnyObjectByType<Teksts>();
        createDB();
    }
	public void createDB(){
		using (var connection = new SqliteConnection (dbName)) {
			connection.Open();

			using (var command = connection.CreateCommand ()) {
				command.CommandText = "CREATE TABLE IF NOT EXISTS progress (StateName TEXT primary key, Owner TEXT, TroopCount INT);";
				command.ExecuteNonQuery ();
			}

			connection.Close ();
		}
	}

        public class Teritorija{
            public string ValstsNosaukums { get; set; }
            public string Ipasnieks { get; set; }
            public int RotasSkaits { get; set; }
        }

    public void saglabatSpeli()
    {
        string saglabasanasLaiks = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            // Dzēst esošos datus
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM progress";
                command.ExecuteNonQuery();
            }

            // Save the current game state
            List<Teritorija> territories = new List<Teritorija>();
            foreach (GameObject stateObject in GameObject.FindGameObjectsWithTag("Valsts"))
            {
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                territories.Add(new Teritorija {
                    ValstsNosaukums = stateObject.name, 
                    Ipasnieks = stateController.valsts.speletajs.ToString(), 
                    RotasSkaits = stateController.rotasSkaitsByPlayer[stateController.valsts.speletajs]
                });
            }

            // Ieliek jaunus datus
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var territory in territories) // Move the foreach loop inside the transaction block
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT OR REPLACE INTO progress (StateName, Owner, TroopCount) VALUES (@valstsNosaukums, @ipasnieks, @rotasSkaits)";

                        // Add parameters using command.Parameters.Add
                        var stateNameParam = command.CreateParameter();
                        stateNameParam.ParameterName = "@valstsNosaukums";
                        stateNameParam.Value = territory.ValstsNosaukums;
                        command.Parameters.Add(stateNameParam);


                        var ownerParam = command.CreateParameter();
                        ownerParam.ParameterName = "@ipasnieks";
                        ownerParam.Value = territory.Ipasnieks;
                        command.Parameters.Add(ownerParam);


                        var troopCountParam = command.CreateParameter();
                        troopCountParam.ParameterName = "@rotasSkaits";
                        troopCountParam.Value = territory.RotasSkaits;
                        command.Parameters.Add(troopCountParam);

                        command.ExecuteNonQuery();
                    }

                }
                transaction.Commit(); // Commit the changes to the database
            }
            
            connection.Close();
            PlayerPrefs.SetString("saglabasanasLaiks", saglabasanasLaiks); 
            teksts.loadTeksts.text = "Iepriekšējais progress: " + PlayerPrefs.GetString("saglabasanasLaiks");
            teksts.saveTeksts.text = "Saglabāts progress: "+ PlayerPrefs.GetString("saglabasanasLaiks");
            Debug.Log("Game saved successfully.");
        }
    }

    public void ieprieksejaisProgress()
{
    try
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM progress";

                using (IDataReader reader = command.ExecuteReader())
                {
                    List<Teritorija> territories = new List<Teritorija>();
                    while (reader.Read())
                    {
                        Teritorija territory = new Teritorija()
                        {
                            ValstsNosaukums = reader["StateName"].ToString(),
                            Ipasnieks = reader["Owner"].ToString(),
                            RotasSkaits = Convert.ToInt32(reader["TroopCount"])
                        };
                        territories.Add(territory);
                    }

                    // Update game state from loaded territories
                    foreach (var territory in territories)
                    {
                        GameObject stateObject = GameObject.Find(territory.ValstsNosaukums);
                        if (stateObject != null)
                        {
                            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();

                            // Clear existing troops and reset counts
                            foreach (Transform child in stateObject.transform)
                            {
                                if (child.CompareTag("PLAYERTroop") || child.CompareTag("LSPRTroop"))
                                {
                                    Destroy(child.gameObject);
                                }
                            }

                            foreach (Valstis.Speletaji player in Enum.GetValues(typeof(Valstis.Speletaji)))
                            {
                                stateController.rotasSkaitsByPlayer[player] = 0;
                            }

                            // Find predefined troop positions
                            GameObject[] troopPositions = GameObject.FindGameObjectsWithTag("state" + territory.ValstsNosaukums.Split('_')[1] + "Pozicijas");

                            // Set ownership and instantiate new troops
                            stateController.valsts.speletajs = (Valstis.Speletaji)System.Enum.Parse(typeof(Valstis.Speletaji), territory.Ipasnieks);
                            stateController.rotasSkaitsByPlayer[stateController.valsts.speletajs] = territory.RotasSkaits;
                            for (int i = 0; i < territory.RotasSkaits && i < troopPositions.Length; i++) // Loop through positions and available troops
                            {
                                GameObject troopPrefab = (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER) ? objekti.rotasPrefs : objekti.rotasPrefsLSPR;
                                Instantiate(troopPrefab, troopPositions[i].transform.position, Quaternion.identity, stateObject.transform); 
                            }

                            // Update visuals
                            stateController.tintesKrasa((stateController.valsts.speletajs == Valstis.Speletaji.PLAYER) ? new Color32(139, 221, 51, 255) : new Color32(243, 43, 43, 235));
                        }
                    }
                }
            }
            Debug.Log("Game loaded successfully.");
        }
    }
        catch (Exception e)
        {
            Debug.LogError("Error loading game: " + e.Message);
        }
    }

}
