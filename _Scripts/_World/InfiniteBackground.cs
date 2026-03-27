using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [Header("Configurações")]
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float tileWorldSize = 5f; // Tamanho de cada tile em unidades da Unity

    private Transform cameraTransform;
    private Vector2 tileSize;
    private GameObject[,] tiles;
    private SpriteRenderer referenceSpriteRenderer;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        referenceSpriteRenderer = GetComponent<SpriteRenderer>();
        tileSize = new Vector2(tileWorldSize, tileWorldSize);

        // Ajusta o scale do sprite para cobrir o tamanho definido
        AdjustSpriteScale();
        BuildGrid();
    }

    private void AdjustSpriteScale()
    {
        Vector2 spriteSize = referenceSpriteRenderer.bounds.size;
        if (spriteSize.x == 0 || spriteSize.y == 0) return;

        float scaleX = tileWorldSize / spriteSize.x;
        float scaleY = tileWorldSize / spriteSize.y;
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    private void BuildGrid()
    {
        tiles = new GameObject[gridWidth, gridHeight];

        int halfW = gridWidth / 2;
        int halfH = gridHeight / 2;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(
                    (x - halfW) * tileSize.x,
                    (y - halfH) * tileSize.y,
                    0
                );

                if (x == 0 && y == 0)
                {
                    transform.position = position;
                    tiles[x, y] = gameObject;
                }
                else
                {
                    GameObject tile = new GameObject($"Tile_{x}_{y}");
                    tile.transform.position = position;
                    tile.transform.parent = transform.parent;

                    SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
                    sr.sprite = referenceSpriteRenderer.sprite;
                    sr.sortingOrder = referenceSpriteRenderer.sortingOrder;
                    sr.sortingLayerName = referenceSpriteRenderer.sortingLayerName;
                    sr.color = referenceSpriteRenderer.color;

                    // Mesmo scale do tile original
                    tile.transform.localScale = transform.localScale;

                    tiles[x, y] = tile;
                }
            }
        }
    }

    private void LateUpdate()
    {
        RepositionTiles();
    }

    private void RepositionTiles()
    {
        Vector2 camPos = cameraTransform.position;

        foreach (GameObject tile in tiles)
        {
            if (tile == null) continue;

            Vector2 tilePos = tile.transform.position;

            float offsetX = camPos.x - tilePos.x;
            float offsetY = camPos.y - tilePos.y;

            if (Mathf.Abs(offsetX) > tileSize.x * (gridWidth / 2f))
            {
                float signX = Mathf.Sign(offsetX);
                tile.transform.position = new Vector3(
                    tilePos.x + signX * tileSize.x * gridWidth,
                    tilePos.y,
                    0
                );
            }

            if (Mathf.Abs(offsetY) > tileSize.y * (gridHeight / 2f))
            {
                float signY = Mathf.Sign(offsetY);
                tile.transform.position = new Vector3(
                    tile.transform.position.x,
                    tilePos.y + signY * tileSize.y * gridHeight,
                    0
                );
            }
        }
    }
}