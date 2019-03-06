using System.Collections;
using System;
using UnityEngine;
using UnityEditor;

public class DynamicCreatTerrain : MonoBehaviour
{
    public static TerrainData terrainData;
    public Terrain terrain;
    public GameObject cube;
    public float[,] setArray;
    public float[,] array;
    private float[,] heightsBackups;

    void Start()
    {
        //var terrain = CreateTerrain();
        terrainData=terrain.terrainData;

        int width = terrainData.heightmapWidth;
        int height = terrainData.heightmapHeight;
        // 备份高度图
        setArray= new float[width, height];
        array = new float[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                setArray[i, j] =0.01f;
            }
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                array[i, j] = 0.01f;
            }
        heightsBackups = terrainData.GetHeights(0, 0, width, height);
        // 设置高度图
        terrainData.SetHeights(0, 0, setArray);
        print("width:" + width + " height:" + height);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ModifyTerrainDataHeight();
        if (Input.GetKeyDown(KeyCode.F4))
            StartCoroutine(Disable());
    }



    // 动态改变地形
    public void ModifyTerrainDataHeight()
    {
         float c = cube.transform.position.x;
         float d = cube.transform.position.z;
        int a, b;
        a = (int)c;
        b = (int)d;
        
   

        for (int i =a-30; i < 30+a; i++)
            for (int j = b-30; j < b+30; j++)
            {
                array[i, j] = heightsBackups[i, j];
            }

        // 设置高度图
        terrainData.SetHeights(0, 0, array);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Recove Terrain.");
        terrainData.SetHeights(0, 0, heightsBackups);
    }
}
