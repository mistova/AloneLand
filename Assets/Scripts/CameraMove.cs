using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Rigidbody rgb;

    public float speedY;
    public float speedX;
    public float speedR;

    public float cameraDistanceMax;
    public float cameraDistanceMin;
    float cameraDistance = -90f;
    public float scrollSpeed;

    void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        if (cameraDistance > cameraDistanceMin)
            cameraDistance = cameraDistanceMin;
        else if (cameraDistance < cameraDistanceMax)
            cameraDistance = cameraDistanceMax;

        transform.position = new Vector3(transform.position.x, transform.position.y, cameraDistance);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(0, 0, speedR * Time.deltaTime);
        else if (Input.GetKey(KeyCode.Q))
            transform.Rotate(0, 0, (-1) * speedR * Time.deltaTime);
        else
            transform.Rotate(0, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.up * Time.deltaTime * speedY);
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.down * Time.deltaTime * speedY);
        else
            rgb.velocity = new Vector3(rgb.velocity.x, 0f, rgb.velocity.z);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * Time.deltaTime * speedX);
        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * Time.deltaTime * speedX);
        else
            rgb.velocity = new Vector3(0, rgb.velocity.y, rgb.velocity.z);
    }
}