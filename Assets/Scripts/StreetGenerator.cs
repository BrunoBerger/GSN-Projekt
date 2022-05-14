using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StreetGenerator : MonoBehaviour
{
    [SerializeField] GameState gameState;

    public  float StreetLenght = 10;
    public  float StreetWidth = 5; 
    public  float ActiveStreetTiles = 5;
    private float ScrollSpeed;
    [Range(1.1f,100f)]
    public float ScrollSpeedFactor = 7f; // If gameState.speed should be taken directly as ScrollSpeed, set to 1
    public float Carspeed = 5;
    [Range(0f, 1f)]
    public float ObstacleSpawnRate = 0.5f;
    [Range (0f, 1f)]
    public float DecorationSpawnRate = 0.5f;
    public float DecorationSpawnAreaWidth = 20f;

    public GameObject StreetPrefab;
    public GameObject[] ObstaclePrefabs;
    public Material[] ObstacleMaterials;
    public GameObject DecorationPrefab;

    private Queue<GameObject> _streetQueue = new Queue<GameObject>();
    private GameObject _lastAdded;

    GameObject[] lanes;
    private List<GameObject> _obstaclesList = new List<GameObject> ();

    public Queue<GameObject> _decorationQueue = new Queue<GameObject>();


    void Start()
    {
        ScrollSpeed = gameState.speed * ScrollSpeedFactor;

        // Find & order all of the lanes in the scene
        lanes = GameObject.FindGameObjectsWithTag("lane").OrderBy(i => i.transform.position.x).ToArray();

        for (int i = 0; i < ActiveStreetTiles; i++)
        {
            var s = Instantiate(StreetPrefab, new Vector3(transform.position.x, transform.position.y, (i) * StreetLenght), Quaternion.identity,transform);
            _streetQueue.Enqueue(s);
            _lastAdded = s;

            SpawnDecoration(i * StreetLenght);
        }

        StartCoroutine(SpawnStuff());
    }

    IEnumerator SpawnStuff()
    {
        while (true)
        {
            yield return new WaitForSeconds(StreetLenght / ScrollSpeed);
            SpawnDecoration();
            SpawnObstacle();
        }
    }

    void Update()
    {

        ScrollSpeed = gameState.speed * ScrollSpeedFactor;

        //Street loop
        TranslateEverythingInQueue(_streetQueue);
        if(_streetQueue.Count > 0 && _streetQueue.Peek().transform.position.z < (-3 - StreetLenght/2))
        {
            var s = _streetQueue.Dequeue();
            s.transform.position = _lastAdded.transform.position + new Vector3(0, 0, StreetLenght);
            _streetQueue.Enqueue(s);
            _lastAdded = s;
        }

        //obstacle loop
        GameObject outOfView = null;
        foreach (var item in _obstaclesList)
        {
            var p = item.transform.position;
            if(p.x > 0)//on left side
            {
                item.transform.position = new Vector3(p.x, p.y, p.z - (ScrollSpeed - Carspeed)  * Time.deltaTime);
            }
            else
            {
                item.transform.position = new Vector3(p.x, p.y, p.z - (ScrollSpeed + Carspeed) * Time.deltaTime);
            }

            if(p.z < -4)
            {
                outOfView = item;
            }

        }
        if(outOfView != null)
        {
            _obstaclesList.Remove(outOfView);
            Destroy(outOfView.gameObject);
        }



        //decoration loop
        TranslateEverythingInQueue(_decorationQueue);
        DequeueOutOfView(_decorationQueue);

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

    private void SpawnObstacle()
    {
        SpawnObstacle(StreetLenght * ActiveStreetTiles);
    }

    private void SpawnObstacle(float zPos)
    {
        if (ObstaclePrefabs.Length <= 0)
            return;

        var random = Random.value;
        if (random > ObstacleSpawnRate)
            return;

        int lane = (int)(Random.value * lanes.Length);

        var type = (int)(Random.value * ObstaclePrefabs.Length);

        var o = Instantiate(ObstaclePrefabs[type], new Vector3(lanes[lane].transform.position.x,0, zPos), Quaternion.identity, transform);
        if(lanes[lane].transform.position.x < 0)
        {
            o.transform.Rotate(Vector3.up, 180);
        }
        var randomMaterialIndex= (int)(Random.value * ObstacleMaterials.Length);
        var meshRenderer = o.GetComponentsInChildren<MeshRenderer>();
        foreach (var r in meshRenderer)
        {
            if(r.gameObject.tag == "Obstacle")
            {
                r.material = ObstacleMaterials[randomMaterialIndex];
            }
        }
        
        _obstaclesList.Add(o);


    }

    private void DequeueOutOfView(Queue<GameObject> queue)
    {
        if (queue.Count > 0 && queue.Peek().transform.position.z < -2)
        {
            var go = queue.Dequeue();
            Destroy(go);
        }
    }

    private void TranslateEverythingInQueue(Queue<GameObject> queue)
    {
        foreach (var item in queue)
        {
            var p = item.transform.position;
            item.transform.position = new Vector3(p.x, p.y, p.z - (ScrollSpeed * Time.deltaTime));
        }
    }

}
