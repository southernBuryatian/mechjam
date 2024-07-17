using UnityEngine;
using UnityEngine.Tilemaps;

public class LettersGameScript : MonoBehaviour
{
    public TileBase tileA;
    public Vector2Int size;

    void Start()
    {
        Vector3Int[] positions = new Vector3Int[size.x * size.y];
        TileBase[] tileArray = new TileBase[positions.Length];

        for (int index = 0; index < positions.Length; index++)
        {
            positions[index] = new Vector3Int(index % size.x, index / size.y, 0);
            tileArray[index] = tileA;
        }

        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTiles(positions, tileArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
