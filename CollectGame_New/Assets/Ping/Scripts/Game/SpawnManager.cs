using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PfHero
{
    public GameObject[] hero;
}
public class SpawnManager : MonoBehaviour {

    static SpawnManager _instance;
    public static SpawnManager Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    public GameObject pfSpike;
    public GameObject pfDrop;
    public GameObject[] lightEffect;
    public GameObject lightTop;
    public PfHero[] pfHero;
    public GameObject[] pfCreep;
    public float intervalSpike = 3;
    public float intervalDrop = 15;

    public List<Creep> listCreep { get; set; }
    public void Init()
    {
        listCreep = new List<Creep>();
        for (int i = 0; i < GSGamePlay.Instance.lanes.Length; i++)
        {
            StartCoroutine(SpawnCreep(i));
        }
        StartCoroutine(SpawnSpikeRoutine());
        StartCoroutine(SpawnDropRoutine());
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
    IEnumerator SpawnDropRoutine()
    {
        while (true)
        {
            StartCoroutine(SpawnDrop());
            yield return new WaitForSeconds(intervalDrop);
        }
    }
    IEnumerator SpawnCreep(int _lane)
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1f));
        //turnLight
        int _pos = Random.Range(0, 2);
        lightEffect[_lane * 2 + _pos].SetActive(true);
        yield return new WaitForSeconds(1f);
        lightEffect[_lane * 2 + _pos].SetActive(false);
        int _index = Random.Range(0, pfCreep.Length);
        GameObject _obj = ObjectPoolManager.Spawn(pfCreep[_index]);
        Creep _creep = _obj.GetComponent<Creep>();
        listCreep.Add(_creep);
        _creep.Init(_lane, _pos);
    }
    void SpawnSpike()
    {
        int _lane = Random.Range(0, GSGamePlay.Instance.lanes.Length);
        GameObject _obj = ObjectPoolManager.Spawn(pfSpike);
        CreepSpike _creep = _obj.GetComponent<CreepSpike>();
        _creep.Init(_lane);
    }
    IEnumerator SpawnDrop()
    {
        float _posX = Random.Range(Pos.spawnLeft, Pos.spawnRight);
        lightTop.transform.position = new Vector3(_posX, lightTop.transform.position.y, lightTop.transform.position.z);
        lightTop.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        lightTop.SetActive(false);
        GameObject _obj = ObjectPoolManager.Spawn(pfDrop);
        CreepDrop _creep = _obj.GetComponent<CreepDrop>();
        _creep.Init(0);
        _obj.transform.position = new Vector3(_posX, Pos.top);
    }
    public void UnSpawnCreep(Creep _creep)
    {
        ObjectPoolManager.Unspawn(_creep.gameObject);
        int _lane = _creep.lane;
        listCreep.Remove(_creep);
        StartCoroutine(SpawnCreep(_lane));
    }
}
