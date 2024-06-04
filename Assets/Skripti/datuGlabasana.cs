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
    public UI ui;

    private string dbName = "URI=file:progress.db";
    void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
        teksts = FindAnyObjectByType<Teksts>();
        ui = FindAnyObjectByType<UI>();
        createDB();
    }
	public void createDB(){
		using (var connection = new SqliteConnection (dbName)) {
			connection.Open();

			using (var command = connection.CreateCommand ()) {
				command.CommandText = "CREATE TABLE IF NOT EXISTS rezultati (ID INT, Laiks TEXT, Vards VARCHAR(65), Valsts TEXT);";
				command.ExecuteNonQuery ();
			}
			using (var command = connection.CreateCommand ()) {
				command.CommandText = "CREATE TABLE IF NOT EXISTS progress (StateNos TEXT primary key, Ipasnieks TEXT, rotuSkaits INT);";
				command.ExecuteNonQuery ();
			}
			connection.Close ();
		}
	}

public void pievienotDatus()
{
    if (objekti.LSPRUzvarejis == true)
    {
        teksts.Valstis = "LSPR";
    }
    else if (objekti.playerUzvarejis == true)
    {
        teksts.Valstis = "Latvija";
    }

    using (var connection = new SqliteConnection(dbName))
    {
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO rezultati (Laiks, Vards, Valsts) VALUES ('" + teksts.timerText + "' , '" + objekti.segVards.text + "', '" + teksts.Valstis + "');";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }
    paraditRezDat();
}

	public void paraditRezDat(){

		teksts.segVardsNos.text = "";
		teksts.laiksNos.text = "";
        teksts.valstsNos.text = "";

		using (var connection = new SqliteConnection (dbName)) {
			connection.Open ();

			using (var command = connection.CreateCommand ()) {
				command.CommandText = "SELECT Laiks, Vards, Valsts FROM rezultati ORDER BY Vards DESC";

				using (IDataReader reader = command.ExecuteReader ()) {

					while (reader.Read ()) {
						teksts.segVardsNos.text += reader ["Vards"] + "\n\n";
						teksts.valstsNos.text += reader ["Valsts"] + "\n\n";
                        teksts.laiksNos.text += reader ["Laiks"] + "\n\n";
					}

					reader.Close ();
				}
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

            using (var transaction = connection.BeginTransaction())
            {
                foreach (var territory in territories) 
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT OR REPLACE INTO progress (StateNos, Ipasnieks, rotuSkaits) VALUES (@valstsNosaukums, @ipasnieks, @rotasSkaits)";

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
                transaction.Commit(); 
            }
            
            connection.Close();
            string vesturiskaAprakstaString = "";
            for (int i = 0; i < objekti.vesturiskaAprakstaSkaits.Length; i++)
            {
                vesturiskaAprakstaString += objekti.vesturiskaAprakstaSkaits[i] ? "1" : "0";
            }
            PlayerPrefs.SetString("vesturiskaAprakstaSkaits", vesturiskaAprakstaString);
            PlayerPrefs.SetInt("rotuSkaitsLSPR", objekti.rotuSkaitsLSPR);
            PlayerPrefs.SetInt("rotuSkaitsPlayer", objekti.rotuSkaitsPlayer);
            PlayerPrefs.SetString("saglabasanasLaiks", saglabasanasLaiks); 
            if(objekti.AIieslegts == true){
            PlayerPrefs.SetString("pogasIzvele", "AI");
            }else{
            PlayerPrefs.SetString("pogasIzvele", "1v1");
            }
            if(objekti.lietotajuKarta == true){
                PlayerPrefs.SetString("lietotajuKarta", "Latvia");
            }else if(objekti.otraSpeletajaKarta == true){
                PlayerPrefs.SetString("lietotajuKarta", "LSPR");
            }
            PlayerPrefs.SetFloat("spelesLaiks", ui.laiks);
            teksts.loadTeksts.text = "Iepriekšējais progress: " + PlayerPrefs.GetString("saglabasanasLaiks") + " Veids: "+PlayerPrefs.GetString("pogasIzvele");
            teksts.saveTeksts.text = "Saglabāts progress: "+ PlayerPrefs.GetString("saglabasanasLaiks") + " Veids: "+PlayerPrefs.GetString("pogasIzvele");
            Debug.Log("Spēle saglabājās veiskmīgi!");
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
                            ValstsNosaukums = reader["StateNos"].ToString(),
                            Ipasnieks = reader["Ipasnieks"].ToString(),
                            RotasSkaits = Convert.ToInt32(reader["rotuSkaits"])
                        };
                        territories.Add(territory);
                    }

                    foreach (var territory in territories)
                    {
                        GameObject stateObject = GameObject.Find(territory.ValstsNosaukums);
                        if (stateObject != null)
                        {
                            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();

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

                            GameObject[] rotasPozicijas = GameObject.FindGameObjectsWithTag("state" + territory.ValstsNosaukums.Split('_')[1] + "Pozicijas");

                            stateController.valsts.speletajs = (Valstis.Speletaji)System.Enum.Parse(typeof(Valstis.Speletaji), territory.Ipasnieks);
                            stateController.rotasSkaitsByPlayer[stateController.valsts.speletajs] = territory.RotasSkaits;
                            for (int i = 0; i < territory.RotasSkaits && i < rotasPozicijas.Length; i++)
                            {
                                GameObject troopPrefab = (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER) ? objekti.rotasPrefs : objekti.rotasPrefsLSPR;
                                Instantiate(troopPrefab, rotasPozicijas[i].transform.position, Quaternion.identity, stateObject.transform); 
                            }

                            stateController.tintesKrasa((stateController.valsts.speletajs == Valstis.Speletaji.PLAYER) ? new Color32(139, 221, 51, 255) : new Color32(243, 43, 43, 235));
                        }
                    }
                }
                string saglabataisString = PlayerPrefs.GetString("vesturiskaAprakstaSkaits");
                for (int i = 0; i < objekti.vesturiskaAprakstaSkaits.Length && i < saglabataisString.Length; i++) 
                {
                    objekti.vesturiskaAprakstaSkaits[i] = saglabataisString[i] == '1';
                }
            objekti.rotuSkaitsLSPR = PlayerPrefs.GetInt("rotuSkaitsLSPR"); 
            objekti.rotuSkaitsPlayer = PlayerPrefs.GetInt("rotuSkaitsPlayer");
            ui.laiks = 0f;
            if (PlayerPrefs.GetString("pogasIzvele") == "AI"){
                objekti.AIieslegts = true;
            }else if (PlayerPrefs.GetString("pogasIzvele") == "1v1"){
                objekti.AIieslegts = false;
            }
            if(PlayerPrefs.GetString("pogasIzvele") == "pogasIzvele"){}
            if(PlayerPrefs.GetString("lietotajuKarta") == "Latvia"){
                objekti.lietotajuKarta = true;
                objekti.otraSpeletajaKarta = false;
                objekti.LatvijasKarogs.gameObject.SetActive(true);
                objekti.LSPRKarogs.gameObject.SetActive(false);
            }else if(PlayerPrefs.GetString("lietotajuKarta") == "LSPR"){
                objekti.otraSpeletajaKarta = true;
                objekti.lietotajuKarta = false;
                objekti.LatvijasKarogs.gameObject.SetActive(false);
                objekti.LSPRKarogs.gameObject.SetActive(true);
            }
            Debug.Log("Spēle ielādējas veiksmīgi.");
            }
        }
    }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e.Message);
        }
    }

}
