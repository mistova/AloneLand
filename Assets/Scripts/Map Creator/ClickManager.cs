using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] SquareManager squareManager;
    [SerializeField] LayerMask layerMask;

    Vector2 clickedSquarePos;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 100f, layerMask))
            {
                clickedSquarePos = new Vector2(raycastHit.transform.localPosition.x, raycastHit.transform.localPosition.y);

                squareManager.SetHoldingIndexes(clickedSquarePos, ClickState.Clicked);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            RaycastHit raycastHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 100f, layerMask))
            {
                Vector2 vec2 = new Vector2(raycastHit.transform.localPosition.x, raycastHit.transform.localPosition.y);

                if(vec2.x != clickedSquarePos.x && vec2.y != clickedSquarePos.y)
                    squareManager.SetHoldingIndexes(vec2, ClickState.Dragged);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            squareManager.SetHoldingIndexes(clickedSquarePos, ClickState.Released);
        }
    }
}