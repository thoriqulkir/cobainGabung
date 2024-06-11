using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    [SerializeField]
    private Transform emptySpace = null;

    private Camera kamera;

    [SerializeField]
    private Tiles[] tiles;

    private int emptySpaceIndex = 8;

    private bool finished;

    [SerializeField]
    private GameObject endPanel;

    // Start is called before the first frame update
    void Start()
    {
        kamera = Camera.main;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = kamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 5.3)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    Tiles thisTile = hit.transform.GetComponent<Tiles>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;

                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        
        if (!finished)
        {
            int correctTiles = 0;
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.inRightPlace)
                        correctTiles++;
                }
            }

            if (correctTiles == tiles.Length - 1)
            {
                finished = true;
                endPanel.SetActive(true);
            }
        }
        
    }

    public void Shuffle()
    {
        if (emptySpaceIndex != 8)
        {
            var tileOn8LastPos = tiles[8].targetPosition;
            tiles[8].targetPosition = emptySpace.position;
            emptySpace.position = tileOn8LastPos;
            tiles[emptySpaceIndex] = tiles[8];
            tiles[8] = null;
            emptySpaceIndex = 8;
        }
        
        int inversion;
        do
        {
            for (int i = 0; i <= 7; i++)
            {
            var lastPosition = tiles[i].targetPosition;
            int randomIndex = Random.Range(0, 7);

            tiles[i].targetPosition = tiles[randomIndex].targetPosition;
            tiles[randomIndex].targetPosition = lastPosition;

            var tile = tiles[i];
            tiles[i] = tiles[randomIndex];
            tiles[randomIndex] = tile;
            }
            inversion = getInversions();
            Debug.Log("Tiles Shuffled!");
            }
        while (inversion % 2 != 0);
        
    }

    public int findIndex(Tiles ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int getInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInversion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInversion++;
                    }
                }
            }
            inversionsSum += thisTileInversion;
        }
        return inversionsSum;
    }

    public void Close()
    {
        Application.Quit();
    }
}
