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

    private Tile tmpTile;
    private int[,] myMap;
    int width;
    int height;

	void Start () {
        createMap();
	}

	void Update () {
       SetUpMap();
	}

    public void SetUpMap()
    {
        width = mapSize.x;
        height = mapSize.y;

        if (myMap == null)
        {
            myMap = new int[width, height];
            createMap();
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (myMap[x, y] == 1)
                {
                    ForeMap.SetTile(new Vector3Int(x, y, 0), myTile);
                    ForeMap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                    ForeMap.SetColor(Position, TileColor);
                }
            }
        }
    }

    private void createMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                myMap[x, y] = 1;
            }

        }
    }

    public void clearMap(bool complete)
    {

        ForeMap.ClearAllTiles();
        if (complete)
        {
            myMap= null;
        }


    }
}
