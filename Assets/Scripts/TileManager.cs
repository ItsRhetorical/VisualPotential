using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public Tilemap ForeMap;
    public Tile myTile;
    public Camera myCamera;

    public Vector3Int Position;
    public Color TileColor;

    public Vector2Int mapSize;

    [Range(0, 20)]
    public int iterations;
    
    private float[,] dataMap;
    private List<Vector2Int> sources = new List<Vector2Int>();
    int width;
    int height;

	void Start () {
        clearMap(true);
    }

	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            insertValue();
        }

        if (dataMap == null)
        {
            createMap();
            UpdateTileMap();
            setCamera();
        }

        RunSim(iterations);

        UpdateTileMap();
	}
       
    public void UpdateTileMap()
    {
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                ForeMap.SetTile(new Vector3Int(x, y, 0), myTile);
                ForeMap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                ForeMap.SetColor(new Vector3Int(x, y, 0), Color.Lerp(Color.red,Color.green,dataMap[x,y]));
                
            }
        }
    }

    private void RunSim(int iter)
    {
        for (int i = 0; i < iter; i++)
        {
            for (int y = 0; y <= height - 1; y++)
            {
                for (int x = 0; x <= width - 1; x++)
                {
                    foreach (Vector2Int source in sources)
                    {
                        if (x == source.x && y == source.y)
                        {
                            dataMap[x, y] = 1f;
                        }
                    }
                    //if (x == Position.x && y == Position.y)
                    //{
                    //    dataMap[x, y] = 1f;
                    //}
                    dataMap[x, y] = averageOfNeighbors(x, y);
                    foreach (Vector2Int source in sources)
                    {
                        if (x == source.x && y == source.y)
                        {
                            dataMap[x, y] = 1f;
                        }
                    }
                    //if (x == Position.x && y == Position.y)
                    //{
                    //    dataMap[x, y] = 1f;
                    //}
                    //foreach (Vector2Int source in sources)
                    //{
                    //    if (x == source.x && y == source.y)
                    //    {
                    //        dataMap[x, y] = 1f;
                    //    }
                    //}
                }
            }
        }
    }

    private float averageOfNeighbors(int x, int y)
    {
        float left = 0;
        float right = 0;
        float down = 0;
        float up = 0;


        if (x != 0)
            left = dataMap[x - 1, y];
        if (x != width - 1)
            right = dataMap[x + 1, y];
        if (y != 0)
            down = dataMap[x, y - 1];
        if (y != height - 1)
            up = dataMap[x, y + 1];

        return (left+right+up+down)/4.0f;
    }

    private void insertValue()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mapPos = ForeMap.WorldToCell(mouseWorldPos);
        print("Mouse World Position: " + mouseWorldPos);
        print("Mouse Workd Position: " + mouseWorldPos);
        sources.Add(new Vector2Int(mapPos.x, mapPos.y));
    }

    private void createMap()
    {
        width = mapSize.x;
        height = mapSize.y;

        dataMap = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                dataMap[x, y] = 0f;
            }

        }

    }


    public void setCamera()
    {
        print(ForeMap.localBounds);

        myCamera.GetComponent<Transform>().position =
            new Vector3(ForeMap.localBounds.center.x, ForeMap.localBounds.center.y, -10f);
        myCamera.GetComponent<Camera>().orthographicSize = ForeMap.localBounds.extents.y;

    }

    public void clearMap(bool complete)
    {

        ForeMap.ClearAllTiles();
        if (complete)
        {
            dataMap = null;
        }


    }

}
