using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject itemCollectible;

    [SerializeField]
    private GameObject itemPlayerHolder;

    private float respawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0 && respawnTimer <= 0)
        {
            respawnTimer = 4;
            //Debug.Log("!!!reset timer!!!");
        }

        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
        }

        if (respawnTimer < 1 && transform.childCount <= 0)
        {
            //Debug.Log("attempt to spawn item");
            Instantiate(itemCollectible, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, gameObject.transform);
        }
        //Debug.Log(respawnTimer);
        //Debug.Log("child: " + transform.childCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.childCount > 0)
            {
                if (!getChildGameObject(other.gameObject, "ItemPlayerHolder(Clone)"))
                {
                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }
                    var itemHolder = Instantiate(itemPlayerHolder, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, other.transform);
                    itemHolder.transform.localPosition = new Vector3(0, 3, -2);
                }
            }
        }
    }

    public static GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
