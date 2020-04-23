using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 1f;
    public Camera Camera;

    public GameObject Water;
    public GameObject Grass;
    public GameObject Cliff;
    public GameObject Tree;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed,
            Input.GetAxisRaw("Depth") * Time.deltaTime * Speed,
            Input.GetAxisRaw("Vertical") * Time.deltaTime * Speed,
            Space.World
        );

        if(Input.GetKeyUp(KeyCode.F))
        {
            if(transform.rotation.eulerAngles.x == 90)
            {
                transform.rotation = Quaternion.Euler(55, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out raycastHit, 100f))
            {
                Debug.Log(raycastHit.point);
                Vector3 point = raycastHit.point;
                Vector3 position = new Vector3(Mathf.Floor(point.x), point.y, Mathf.Floor(point.z));
                Debug.Log(point.y.Equals(Mathf.Round(3)));
                if (point.y == 1.0f || point.y == 3.0f || point.y == 5.0f)
                {
                    Debug.Log("All good");
                    Instantiate(Cliff, position, Quaternion.identity);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                Debug.Log(raycastHit.point);
                Destroy(raycastHit.collider.gameObject);
                if (raycastHit.point.y <= 0.5f)
                {
                    Vector3 point = raycastHit.point + new Vector3(0.5f, 0, 0.5f);
                    Vector3 position = new Vector3(Mathf.Floor(point.x), 0, Mathf.Floor(point.z));
                    Instantiate(Water, position, Quaternion.identity);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                Debug.Log(raycastHit.point);
                if (raycastHit.point.y >= 1f)
                {
                    Vector3 point = raycastHit.point;
                    Vector3 position = new Vector3(Mathf.Floor(point.x) + 0.5f, Mathf.Floor(point.y), Mathf.Floor(point.z) + 0.5f);
                    Instantiate(Tree, position, Quaternion.Euler(0, 180, 0));
                }
            }
        }
    }
}
