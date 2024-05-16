using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class FogMap : MonoBehaviour
{
    Cell[,] fog;
    public GameObject fogPrefab;
    public GameObject player;
    public int size = 100;
    public float scale = .1f;
    public float threshold = 0.001f;
    public int iterationFog;


    void Start()
    {
        fog = new Cell[size, size];
        float[,] noiseMap = new float[size, size];
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }
        GenerateFog(fog);
    }


    void Update()
    {
        Iterations iterations = player.GetComponent<Iterations>();
        int iteration = iterations.iteration;

        if (iterationFog != iteration)
        {
            DestroyPreviousFog();
            threshold += 0.002f;
            GenerateFog(fog);
            iterationFog = iteration;
        }

    }
    void GenerateFog(Cell[,] fog)
    {
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);

                if (Random.value < noiseValue * threshold) 
                {
                    Vector3 position = new Vector3(x, 0, y);
                    Instantiate(fogPrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }

    void DestroyPreviousFog()
    {
        GameObject previousFog = GameObject.Find("FogVolume(Clone)");
        Destroy(previousFog);
    }


}
