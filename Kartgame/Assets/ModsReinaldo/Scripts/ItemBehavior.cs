using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    float speed = 30f;
    float _h_spinA = (20 - (-20)) / 2;
    float _t_spinA = (20 + (-20)) / 2;
    float spinA;
    float _w_spinA;
    float _d_w_spinA = 1.5f;
    float _h_spinB = (20 - (-20)) / 2;
    float _t_spinB = (20 + (-20)) / 2;
    float spinB;
    float _w_spinB;
    float _d_w_spinB = 1.5f;
    float _h_spinC = (20 - (-20)) / 2;
    float _t_spinC = (20 + (-20)) / 2;
    float spinC;
    float _w_spinC;
    float _d_w_spinC = 1.5f;

    GameObject itemView;

    private float spinTimer = 0;

    [SerializeField]
    private List<GameObject> itemModels;

    void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(SpinItem());
        spinA = _h_spinA * Mathf.Sin(Random.Range(-20, 20)) + _t_spinA;
        spinB = _h_spinB * Mathf.Sin(Random.Range(-20, 20)) + _t_spinB;
        spinC = _h_spinC * Mathf.Sin(Random.Range(-20, 20)) + _t_spinC;
        _w_spinA = Random.Range(-20f, 20f);
        _w_spinB = Random.Range(-20f, 20f);
        _w_spinC = Random.Range(-20f, 20f);
    }

    private IEnumerator FadeIn()
    {
        for (float i = 0; transform.localScale.x < 0.75f; i += 0.25f * Time.deltaTime)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return null;
        }
    }

    private IEnumerator SpinItem()
    {
        
        int modelIndex = Random.Range(0, itemModels.Count);
        for (float i = 0; i < 5; i += 0.25f * Time.deltaTime)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            itemView = Instantiate(itemModels[modelIndex], transform.position, transform.rotation, transform);
            itemView.AddComponent<ItemSpin>();
            itemView.transform.localScale = transform.localScale;
            itemView.transform.rotation = transform.rotation;
            modelIndex = (modelIndex + 1) % itemModels.Count;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SpinAngle()
    {
        while (true)
        {
            if (itemView)
            {
                transform.Rotate((speed + spinA) * Time.deltaTime, (speed + spinB) * Time.deltaTime, (speed + spinC) * Time.deltaTime);
                itemView.transform.rotation = transform.rotation;
                _w_spinA = _w_spinA + _d_w_spinA;
                spinA = _h_spinA * Mathf.Sin(_w_spinA) + _t_spinA;
                _w_spinB = _w_spinB + _d_w_spinB;
                spinB = _h_spinB * Mathf.Sin(_w_spinB) + _t_spinB;
                _w_spinC = _w_spinC + _d_w_spinC;
                spinC = _h_spinC * Mathf.Sin(_w_spinC) + _t_spinC;
            }
            yield return null;
        }
    }
}
