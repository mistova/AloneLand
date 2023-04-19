using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{
    private void Start()
    {
        regionColors = new List<Color>();

        selectedColorsIndexes = new List<int>();
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
    int colorCount = 0;

    Vector2 currentHoldingIndexes = -Vector2.one, startIndexes = -Vector2.one;
    List<int> selectedColorsIndexes;
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
        Vector2 sign = Vector2.one;

        if (startIndexes.x > newIndexes.x)
            sign.x = -1;
        if (startIndexes.y > newIndexes.y)
            sign.y = -1;

        for (int i = (int)startIndexes.x; (sign.x > 0 && i <= newIndexes.x) || (sign.x > 0 && i <= oldIndexes.x) || (sign.x < 0 && i >= newIndexes.x) || (sign.x < 0 && i >= oldIndexes.x); i += (int) sign.x)
        {
            for (int j = (int)startIndexes.y; (sign.y > 0 && j <= newIndexes.y) || (sign.y > 0 && j <= oldIndexes.y) || (sign.y < 0 && j >= newIndexes.y) || (sign.y < 0 && j >= oldIndexes.y); j += (int) sign.y)
            {
                if(((sign.y > 0 && j <= newIndexes.y) || (sign.y < 0 && j >= newIndexes.y)) && ((sign.x > 0 && i <= newIndexes.x) || (sign.x < 0 && i >= newIndexes.x)))
                {
                    materials[squareEdgeLenght * i + j].color = colors[colorCount % colors.Length];
                }
                else
                {
                    if (squareHolders[squareEdgeLenght * i + j] == -1)
                    {
                        materials[squareEdgeLenght * i + j].color = Color.white * ((i + j + 1) % 2);
                    }
                    else
                    {
                        materials[squareEdgeLenght * i + j].color = regionColors[squareHolders[squareEdgeLenght * i + j]];
                    }
                }
            }
        }
    }

    void SaveColors(Vector2 startIndexes, Vector2 endIndexes)
    {
        if(startIndexes.x > endIndexes.x)
        {
            float index = startIndexes.x;
            startIndexes.x = endIndexes.x;
            endIndexes.x = index;
        }

        if(startIndexes.y > endIndexes.y)
        {
            float index = startIndexes.y;
            startIndexes.y = endIndexes.y;
            endIndexes.y = index;
        }

        for (int i = (int)startIndexes.x; i < endIndexes.x; i++)
        {
            for (int j = (int)startIndexes.y; j < endIndexes.y; j++)
            {
                int index = squareEdgeLenght * i + j;

                if (squareHolders[index] != -1 && !selectedColorsIndexes.Contains(squareHolders[index]))
                    selectedColorsIndexes.Add(squareHolders[index]);

                squareHolders[index] = regionColors.Count;
            }
        }

        for (int i = 0; i < squareEdgeLenght; i++)
        {
            for (int j = 0; j < squareEdgeLenght; j++)
            {
                int index = squareEdgeLenght * i + j;

                for(int k = 0; k < selectedColorsIndexes.Count; k++)
                {
                    if(selectedColorsIndexes[k] == squareHolders[index])
                    {
                        selectedColorsIndexes.RemoveAt(k);

                        break;
                    }
                }
            }
        }

        for (int k = 0; k < selectedColorsIndexes.Count; k++)
        {
            for(int i = selectedColorsIndexes[k] + 1; i < regionColors.Count; i++)
            {
                regionColors[i - 1] = regionColors[i];
            }

            for (int a = 0; a < squareEdgeLenght; a++)
            {
                for (int b = 0; b < squareEdgeLenght; b++)
                {
                    int index = squareEdgeLenght * a + b;

                    if (squareHolders[index] > selectedColorsIndexes[k])
                    {
                        squareHolders[index]--;
                    }

                }
            }

            regionColors.RemoveAt(regionColors.Count - 1);
        }

        regionColors.Add(colors[colorCount % colors.Length]);

        colorCount++;
    }

    void CheckIntervalIndexes()
    {
        //Check selected color
    }
}