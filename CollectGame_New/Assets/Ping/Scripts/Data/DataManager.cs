using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }
    void Awake(){ _instance = this; }
    public void LoadData()
    {
        loadCustomizeData();
    }
    //Example
    public List<DataCustomize> dataCustomize { get; set; }
    void loadCustomizeData()
    {
        string filepath = "Data/CustomizeDatas";
        dataCustomize = new List<DataCustomize>();
        IEnumerable<DataCustomize> dataListFromCSV = CsvControll.LoadCSVDataFromFile<DataCustomize>(filepath);
        foreach (DataCustomize datafromCSV in dataListFromCSV)
        {
            dataCustomize.Add(datafromCSV);
        }
    }
    public DataCustomize GetDataCustomize(int _id)
    {
        for (int i = 0; i < dataCustomize.Count; i++)
        {
            if (dataCustomize[i].id == _id)
            {
                return dataCustomize[i];
            }
        }
        return null;
    }
}
