using UnityEngine;

public class ClickMenuButtons : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit2D rayhit2;
    Transform clickObject;
    public GameObject HideTarget;
    public GameObject ShowTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            rayhit2 = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            clickObject = rayhit2 ? rayhit2.collider.transform : null;

            if (clickObject)
            {
                HideTarget.SetActive(false);
                ShowTarget.SetActive(true);
            }
        }
    }
}
