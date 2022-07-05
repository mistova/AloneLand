using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{
    private void Start()
    {
        regionColors = new List<Color>();

        selectedColors = new List<int>();
    }

    internal void SetSquares(GameObject[] squares)
    {
        squareEdgeLenght = (int) Mathf.Pow(squares.Length, 0.5f);

        this.squares = squares;

        squareHolders = new int[squares.Length];

        materials = new Material[squares.Length];

        for (int i = 0; i < squareHolders.Length; i++)
        {
            squareHolders[i] = -1;

            materials[i] = this.squares[i].GetComponent<MeshRenderer>().sharedMaterial;
        }
    }

    GameObject[] squares;
    Material[] materials;
    int[] squareHolders;

    int squareEdgeLenght;

    [SerializeField] Color[] colors;
    List<Color> regionColors;

    Vector2 currentHoldingIndexes = -Vector2.one, startIndexes = -Vector2.one;
    List<int> selectedColors;
    internal void SetHoldingIndexes(Vector2 holdingIndexes, ClickState clickState)
    {
        if(clickState == ClickState.Clicked)
        {
            currentHoldingIndexes = holdingIndexes;
            startIndexes = holdingIndexes;

            PaintDifference(holdingIndexes, holdingIndexes);
        }
        else if(clickState == ClickState.Dragged)
        {
            PaintDifference(currentHoldingIndexes, holdingIndexes);

            currentHoldingIndexes = holdingIndexes;
        }
        else if(clickState == ClickState.Released)
        {
            SaveColors(startIndexes, holdingIndexes);
        }
        else
        {
            //Do nothing
        }
    }

    void PaintDifference(Vector2 oldIndexes, Vector2 newIndexes)
    {
        for (int i = (int)startIndexes.y; i <= newIndexes.y; i++)
        {
            for (int j = (int)oldIndexes.x + 1; j <= newIndexes.x; j++)
            {
                materials[squareEdgeLenght * i + j].color = colors[regionColors.Count % colors.Length];
            }

            for (int j = (int)newIndexes.x + 1; j <= oldIndexes.x; j++)
            {
                if (squareHolders[squareEdgeLenght * i + j] == -1)
                {
                    materials[squareEdgeLenght * i + j].color = Color.white * ((i + j) % 2 + 1);
                }
                else
                {
                    materials[squareEdgeLenght * i + j].color = regionColors[squareHolders[squareEdgeLenght * i + j]];
                }
            }
        }

        for (int i = (int)startIndexes.x; i <= newIndexes.x; i++)
        {
            for (int j = (int)oldIndexes.y + 1; j <= newIndexes.y; j++)
            {
                materials[squareEdgeLenght * i + j].color = colors[regionColors.Count % colors.Length];
            }

            for (int j = (int)newIndexes.y + 1; j <= oldIndexes.y; j++)
            {
                if (squareHolders[squareEdgeLenght * i + j] == -1)
                {
                    materials[squareEdgeLenght * i + j].color = Color.white * ((i + j) % 2 + 1);
                }
                else
                {
                    materials[squareEdgeLenght * i + j].color = regionColors[squareHolders[squareEdgeLenght * i + j]];
                }
            }
        }
    }

    void SaveColors(Vector2 startIndexes, Vector2 EndIndexes)
    {

    }

    void CheckIntervalIndexes()
    {
        //Check selected color
    }
}