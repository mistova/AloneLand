using UnityEngine;

public class CreateSquares : MonoBehaviour
{

    public GameObject lenght;
    public GameObject height;

    public Transform lenghtTransform;
    public Transform heightTransform;

    void Start()
    {
        Initial();
    }

    private void Initial()
    {
        for (float i = lenghtTransform.position.y; i <= lenghtTransform.position.y * (-1); i++)
            Instantiate(lenght, new Vector3(lenghtTransform.position.x, i, 0), lenghtTransform.rotation);
        for (float i = heightTransform.position.x; i <= heightTransform.position.x * (-1); i++)
            Instantiate(height, new Vector3(i, heightTransform.position.y, 0), lenghtTransform.rotation);
    }
}