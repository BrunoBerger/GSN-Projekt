using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StreetGenerator : MonoBehaviour
{
    public float StreetLenght = 10;
    public float StreetWidth = 5; 
    public float ActiveStreetTiles = 5;
    public float ScrollSpeed = 1f;
    [Range (0f, 1f)]
    public float DecorationSpawnRate = 0.5f;
    public float DecorationSpawnAreaWidth = 20f;

    public GameObject StreetPrefab;
    public GameObject ObstaclePrefab;
    public GameObject DecorationPrefab;

    private Queue<GameObject> _streetQueue = new Queue<GameObject>();
    private GameObject _lastAdded;

    private Queue<GameObject> _decorationQueue = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < ActiveStreetTiles; i++)
        {
            var s = Instantiate(StreetPrefab, new Vector3(0, 0, (i) * StreetLenght), Quaternion.identity,transform);
            _streetQueue.Enqueue(s);
            _lastAdded = s;

            SpawnDecoration(i * StreetLenght);
        }
        InvokeRepeating("SpawnStreetTile",  StreetLenght/ ScrollSpeed , StreetLenght / ScrollSpeed);
        InvokeRepeating("SpawnDecoration", StreetLenght / ScrollSpeed, StreetLenght / ScrollSpeed);


    }

    void Update()
    {
        foreach (var item in _streetQueue)
        {
            var p = item.transform.position;
            item.transform.position = new Vector3(p.x,p.y,p.z- ScrollSpeed * Time.deltaTime);
        }

        foreach (var item in _decorationQueue)
        {
            var p = item.transform.position;
            item.transform.position = new Vector3(p.x, p.y, p.z - ScrollSpeed * Time.deltaTime);
        }
        if(_decorationQueue.Count>0 && _decorationQueue.Peek().transform.position.z < -2)
        {
            var d = _decorationQueue.Dequeue();
            Destroy(d);
        }

    }

    private void SpawnStreetTile()
    {
        var s = Instantiate(StreetPrefab, _lastAdded.transform.position + new Vector3(0, 0, StreetLenght), Quaternion.identity, transform);
        _streetQueue.Enqueue(s);
        _lastAdded = s;
        var old = _streetQueue.Dequeue();
        Destroy(old);
    }

    private void SpawnDecoration()
    {
        SpawnDecoration(StreetLenght * ActiveStreetTiles);
    }

    private void SpawnDecoration(float zPos)
    {
        var random = Random.value;
        if (random > DecorationSpawnRate)
            return;

        var x = (Random.value * DecorationSpawnAreaWidth) + (StreetWidth/2);
        x = random < DecorationSpawnRate / 2 ? -x : x;

        var d = Instantiate(DecorationPrefab, new Vector3(x, 0, zPos), Quaternion.identity, transform);
        _decorationQueue.Enqueue(d);
    }

}
