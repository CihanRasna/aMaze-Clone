using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //For instantiate----------------
    [SerializeField] public int xLine;
    [SerializeField] public int zLine;

    private int row = 0;
    private int column = 0;
    private int rnd;
    [SerializeField] GameObject Cubes;
    [SerializeField] private GameObject Floor;

    //-------------------------------

    //For mapArray-------------------

    [SerializeField] public bool doMapArray;

    [SerializeField] private Transform Walls;

    private MapModel map;

    //-------------------------------

    public GameObject Player { get; private set; }
    private GameObject Hole;

    private Rigidbody rbPlayer;

    private BoxCollider colPlayer;

    public static float moveSpeed;

    [SerializeField] private float distance;

    public TextMeshProUGUI levelText;

    public static int level = 1;

    private void Awake()
    {
        if (GameObject.Find("LevelText") != null)
        {
            levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        }

        moveSpeed = 7.5f;
        Player = GameObject.Find("Player");
        Hole = GameObject.Find("Hole");
        Walls = GameObject.Find("Walls").transform;
        colPlayer = GameObject.Find("Player").GetComponent<BoxCollider>();
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody>();
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        levelText.text = "Level  " + level;
        map = GetComponent<MapModel>();
        if (doMapArray)
        {
            CreateMap(map.Blocks);
        }
        else
        {
            SpawnCubes();
        }

        //Debug.Log(level + ". level");
    }

    private void Update()
    {
        DistanceTracker();
    }

    private void CreateMap(int[,,] mapArray)
    {
        int rndLevel = Random.Range(0, 10);
        int childCount = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (mapArray[rndLevel, i, j] == 1)
                {
                    Walls.GetChild(childCount).gameObject.SetActive(true);
                }
                else
                {
                    Walls.GetChild(childCount).gameObject.SetActive(false);
                }

                childCount++;
                Instantiate(Floor, new Vector3(transform.position.x + row, -1, transform.position.z + column + 1),
                    Quaternion.identity, transform);
                row++;
            }

            row = 0;
            column--;
        }

        Player.transform.localPosition =
            new Vector3(map.playerPos[rndLevel, 0], map.playerPos[rndLevel, 1], map.playerPos[rndLevel, 2]);
        Hole.transform.localPosition =
            new Vector3(map.holePos[rndLevel, 0], map.holePos[rndLevel, 1], map.holePos[rndLevel, 2]);
    }

    void SpawnCubes()
    {
        
        // Player.transform.position =
        //     new Vector3(Random.Range(-3, zLine - 6), 0, Random.Range(2, 5 - xLine));
        // Hole.transform.position =
        //     new Vector3(Random.Range(-3, zLine - 6), 0, Random.Range(2, 5 - xLine));

        
        for (int i = 0; i < xLine; i++)
        {
            for (int j = 0; j < zLine; j++)
            {
                Instantiate(Cubes, new Vector3(transform.position.x + row, 0, transform.position.z + column),
                    Quaternion.identity, transform);
                Instantiate(Floor, new Vector3(transform.position.x + row, -1, transform.position.z + column),
                    Quaternion.identity, transform);

                rnd = Random.Range(0, 2);
                bool rndActive = Convert.ToBoolean(rnd);
                Cubes.SetActive(rndActive);

                if (i == 0 || i == xLine - 1)
                {
                    Cubes.SetActive(true);
                    //Debug.Log("Ust ve Alt Duvar");
                }

                if (j == zLine - 2 || j == zLine - 1)
                {
                    Cubes.SetActive(true);
                    //Debug.Log("Sol ve Sag Duvar");
                }


                // if (colPlayer.bounds.Intersects(Cubes.GetComponent<Collider>().bounds))
                // {
                //     Cubes.SetActive(false);//     
                // }

                row++;
            }

            column--;
            row = 0;
        }
    }


    private void DistanceTracker()
    {
        distance = Vector3.Distance(Player.transform.position, Hole.transform.position);

        if (distance < 1f)
        {
            colPlayer.size = Vector3.one;
            colPlayer.isTrigger = true;
            rbPlayer.useGravity = true;

            if (distance <= 0.2f)
            {
                Hole.SetActive(false);
                moveSpeed = 0.2f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            level++;
        }
    }
}