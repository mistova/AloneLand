using UnityEngine;

public class BlankMapGenerator : MonoBehaviour
{
    [SerializeField] int fieldSize = 32;
    [SerializeField] Camera cam;
    void Start()
    {
        squares = new GameObject[fieldSize * fieldSize];

        SetCameraPosition();

        GenerateSquares();
    }

    [SerializeField] float marginAmount = 2f;
    void SetCameraPosition()
    {
        float fieldOfView = cam.fieldOfView;

        if (fieldOfView >= 80)
            fieldOfView = 80;

        float z = -(fieldSize / 2 + marginAmount) / Mathf.Sin(Mathf.Deg2Rad * fieldOfView / 2) * Mathf.Sin(Mathf.Deg2Rad * (90 - fieldOfView / 2));

        cam.transform.position = new Vector3(fieldSize / 2 - 0.5f, fieldSize / 2 - 0.5f, z);
    }

    [SerializeField] SquareManager squareManager;
    [SerializeField] GameObject squarePref;
    GameObject[] squares;
    float meshSize = 10;
    void GenerateSquares()
    {
        float unit = 1;
        Vector3 startPoints = new Vector3(0, 0, 0);

        int index;

        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                index = j * fieldSize + i;

                squares[index] = Instantiate(squarePref, transform);

                squares[index].transform.localScale = Vector3.one * unit / meshSize;

                squares[index].transform.localPosition = startPoints + Vector3.forward * i * unit + Vector3.right * j * unit;

                squares[index].GetComponent<Renderer>().material.color = Color.white * ((i + j) % 2 + 1);
            }
        }

        squareManager.SetSquares(squares);
    }
}