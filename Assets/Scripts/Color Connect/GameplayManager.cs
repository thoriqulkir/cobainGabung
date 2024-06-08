using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    [HideInInspector]
    public bool hasGameFinished;

    [SerializeField]
    private GameObject winText;

    private void Awake()
    {
        Instance = this;

        hasGameFinished = false;
        winText.SetActive(false);

        SpawnBoard();
        SpawnNodes();
        
    }

    #region BOARD_SPAWN

    [SerializeField]
    private SpriteRenderer boardPrefab, cellPrefab;

    private void SpawnBoard()
    {
        // Tentukan ukuran papan tetap
        int boardSize = 3;
        float cellSpacing = 2.7f; // Jarak antar sel

        // Buat papan
        var board = Instantiate(boardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        board.size = new Vector2(8.2f, 8.2f); // Sesuaikan dengan ukuran papan yang telah ditentukan

        // Buat sel-sel pada papan
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                float xPos = -2.7f + j * cellSpacing;
                float yPos = 2.7f - i * cellSpacing;
                Instantiate(cellPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            }
        }
    }

    #endregion

    #region NODE_SPAWN

    [SerializeField]
    private Node _nodePrefab;
    private List<Node> _nodes;

    public Dictionary<Vector2Int, Node> _nodeGrid;

    private void SpawnNodes()
    {
        _nodes = new List<Node>();
        _nodeGrid = new Dictionary<Vector2Int, Node>();

        int boardSize = 3;
        float cellSpacing = 2.7f; // Jarak antar sel

        Node spawnedNode;
        Vector3 spawnPos;

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                float xPos = -2.7f + j * cellSpacing;
                float yPos = 2.7f - i * cellSpacing;

                spawnPos = new Vector3(xPos, yPos, 0f);
                spawnedNode = Instantiate(_nodePrefab, spawnPos, Quaternion.identity);
                spawnedNode.Init();

                _nodes.Add(spawnedNode);
                _nodeGrid.Add(new Vector2Int(i, j), spawnedNode);
                spawnedNode.gameObject.name = i.ToString() + j.ToString();
                spawnedNode.Pos2D = new Vector2Int(i, j);
            }
        }

        List<Vector2Int> offsetPos = new List<Vector2Int>()
        {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (var item in _nodeGrid)
        {
            foreach (var offset in offsetPos)
            {
                var checkPos = item.Key + offset;
                if (_nodeGrid.ContainsKey(checkPos))
                {
                    item.Value.SetEdge(offset, _nodeGrid[checkPos]);
                }
            }
        }
    }

    public List<Color> NodeColors;

    public int GetColorId(int i, int j)
    {
        // Implementasi sesuai kebutuhan Anda
        return -1; // Misalnya, Anda dapat mengembalikan -1 jika tidak ada warna yang ditetapkan pada posisi tertentu
    }

    public Color GetHighLightColor(int colorID)
    {
        Color result = NodeColors[colorID % NodeColors.Count];
        result.a = 0.4f;
        return result;
    }

    #endregion

    #region UPDATE_METHODS

    private Node startNode;

    private void Update()
    {
        if (hasGameFinished) return;

        if (Input.GetMouseButtonDown(0))
        {
            startNode = null;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (startNode == null)
            {
                if (hit && hit.collider.gameObject.TryGetComponent(out Node tNode) && tNode.IsClickable)
                {
                    startNode = tNode;
                }

                return;
            }

            if (hit && hit.collider.gameObject.TryGetComponent(out Node tempNode) && startNode != tempNode)
            {
                if (startNode.colorId != tempNode.colorId && tempNode.IsEndNode)
                {
                    return;
                }

                startNode.UpdateInput(tempNode);
                CheckWin();
                startNode = null;
            }

            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            startNode = null;
        }

    }

    #endregion

    #region WIN_CONDITION

    private void CheckWin()
    {
        bool IsWinning = true;

        foreach (var item in _nodes)
        {
            item.SolveHighlight();
        }

        foreach (var item in _nodes)
        {
            IsWinning &= item.IsWin;
            if (!IsWinning)
            {
                return;
            }
        }

        // Unlock level or any other win condition
        // GameManager.Instance.UnlockLevel();

        hasGameFinished = true;
    }

    #endregion

}
