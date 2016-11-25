using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

    static SpawnManager _instance;
    public static SpawnManager Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    public GameObject pfSpike;
    public GameObject[] pfCreep;
    public float intervalSpike = 3;

    public List<Creep> listCreep { get; set; }
    void Start()
    {
        listCreep = new List<Creep>();
        for (int i = 0; i < GSGamePlay.Instance.lanes.Length; i++)
        {
            StartCoroutine(SpawnCreep(i));
        }
        StartCoroutine(SpawnSpikeRoutine());
    }
    public void StartGame ()
    {
    }
    public void GameOver()
    {
    }
	
    IEnumerator SpawnSpikeRoutine()
    {
        while (true)
        {
            SpawnSpike();
            yield return new WaitForSeconds(intervalSpike);
        }
    }
    IEnumerator SpawnCreep(int _lane)
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1f));
        int _index = Random.Range(0, pfCreep.Length);
        GameObject _obj = ObjectPoolManager.Spawn(pfCreep[_index]);
        Creep _creep = _obj.GetComponent<Creep>();
        listCreep.Add(_creep);
        _creep.Init(_lane);
    }
    void SpawnSpike()
    {
        int _lane = Random.Range(0, GSGamePlay.Instance.lanes.Length);
        GameObject _obj = ObjectPoolManager.Spawn(pfSpike);
        CreepSpike _creep = _obj.GetComponent<CreepSpike>();
        _creep.Init(_lane);
    }
    public void UnSpawnCreep(Creep _creep)
    {
        ObjectPoolManager.Unspawn(_creep.gameObject);
        int _lane = _creep.lane;
        listCreep.Remove(_creep);
        StartCoroutine(SpawnCreep(_lane));
    }
}
