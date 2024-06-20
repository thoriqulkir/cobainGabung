using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JigsawScript : MonoBehaviour
{
    [SerializeField]
    private RectTransform emptySpace = null;

    private Camera kamera;

    [SerializeField]
    private Tiles[] tiles;

    private int emptySpaceIndex = 8;

    private bool finished;

    [SerializeField]
    private GameObject puzzleCanvas; // Tambahkan referensi ke Canvas

    public delegate void PuzzleCompleted();
    public event PuzzleCompleted OnPuzzleCompleted;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    void Start()
    {
        kamera = Camera.main;
        graphicRaycaster = puzzleCanvas.GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        Shuffle();
    }

    public void InitializePuzzle()
    {
        finished = false;
        Shuffle(); // Panggil Shuffle setiap kali puzzle diinisialisasi
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            if (results.Count > 0)
            {
                RectTransform hitTransform = results[0].gameObject.GetComponent<RectTransform>();
                if (hitTransform != null)
                {
                    if (Vector2.Distance(emptySpace.anchoredPosition, hitTransform.anchoredPosition) < 267.2) // Sesuaikan threshold jarak
                    {
                        Vector2 lastEmptySpacePosition = emptySpace.anchoredPosition;
                        Tiles thisTile = hitTransform.GetComponent<Tiles>();
                        emptySpace.anchoredPosition = thisTile.targetPosition;
                        thisTile.targetPosition = lastEmptySpacePosition;

                        int tileIndex = findIndex(thisTile);
                        tiles[emptySpaceIndex] = tiles[tileIndex];
                        tiles[tileIndex] = null;
                        emptySpaceIndex = tileIndex;
                    }
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
                Debug.Log("Completed");
                //StopInteract();
                OnPuzzleCompleted?.Invoke(); // Memanggil event setelah puzzle selesai
            }
        }
    }

    public void StopInteract()
    {
        puzzleCanvas.SetActive(false);
        Time.timeScale = 1f; // Melanjutkan waktu saat puzzle tidak aktif
    }

    public void Shuffle()
    {
        if (emptySpaceIndex != 8)
        {
            var tileOn8LastPos = tiles[8].targetPosition;
            tiles[8].targetPosition = emptySpace.anchoredPosition;
            emptySpace.anchoredPosition = tileOn8LastPos;
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
        while (inversion % 2 != 0 || inversion == 0);
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
}