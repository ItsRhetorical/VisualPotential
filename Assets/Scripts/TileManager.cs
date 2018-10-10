using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public Tilemap ForeMap;
    public Tile myTile;

    public Vector3Int Position;
    public Color TileColor;

    public Vector2Int mapSize;
    
    private float[,] dataMap;
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

        UpdateTileMap();
	}

    public void UpdateTileMap()
    {

        if (dataMap == null)
        {
            createMap();
        }

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

    private void insertValue()
    {
        dataMap[2, 1] = 1f;
        print("test");
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

    public void clearMap(bool complete)
    {

        ForeMap.ClearAllTiles();
        if (complete)
        {
            dataMap = null;
        }


    }
}
