using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float Speed = 1f;
    public Camera Camera;

    public GameObject Water;
    public GameObject Grass;
    public GameObject Cliff;
    public GameObject Tree;

    public bool RiverBuild;
    public bool CliffBuild;
    public bool TreeBuild;
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

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out raycastHit, 100f))
            {
                Debug.Log(raycastHit.point);
                Vector3 point = raycastHit.point;
                if (CliffBuild)
                {
                    if (point.y == 1.0f || point.y == 3.0f || point.y == 5.0f)
                    {
                        Vector3 position = new Vector3(Mathf.Floor(point.x), point.y, Mathf.Floor(point.z));
                        Instantiate(Cliff, position, Quaternion.identity);
                    }
                    else if (point.y < 0.7f)
                    {
                        Vector3 position = new Vector3(Mathf.Floor(point.x), 0, Mathf.Floor(point.z));
                        Instantiate(Grass, position, Quaternion.identity);
                    }
                    else if (point.z % 1 == 0)
                    {
                        if (point.y < 3)
                        {
                            point.y = 1;
                        }
                        else if (point.y < 5)
                        {
                            point.y = 3;
                        }
                        else
                        {
                            point.y = 5;
                        }
                        Vector3 position = new Vector3(Mathf.Floor(point.x), point.y, Mathf.Floor(point.z - 1));

                        Instantiate(Cliff, position, Quaternion.identity);
                    }
                }
                else if(RiverBuild)
                {
                    if (point.y == 1.0f || point.y == 3.0f || point.y == 5.0f)
                    {
                        Destroy(raycastHit.collider.gameObject);
                        Vector3 position = new Vector3(Mathf.Floor(point.x), point.y - 1.3f, Mathf.Floor(point.z));
                        Instantiate(Water, position, Quaternion.identity);
                    }
                }
                else if(TreeBuild)
                {
                    if (raycastHit.point.y >= 1f)
                    {
                        Vector3 position = new Vector3(Mathf.Floor(point.x) + 0.5f, Mathf.Floor(point.y), Mathf.Floor(point.z) + 0.5f);
                        Instantiate(Tree, position, Quaternion.Euler(0, 180, 0));
                    }
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
    }

    public void SetBuildCliff()
    {
        CliffBuild = true;
        RiverBuild = false;
        TreeBuild = false;
    }

    public void SetBuildRiver()
    {
        CliffBuild = false;
        RiverBuild = true;
        TreeBuild = false;
    }

    public void SetBuildTree()
    {
        CliffBuild = false;
        RiverBuild = false;
        TreeBuild = true;
    }
}
