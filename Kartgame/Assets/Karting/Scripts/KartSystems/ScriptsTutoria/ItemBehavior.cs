using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class ItemBehavior : MonoBehaviour
{
    #region Horrible variables
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
    #endregion

     public GameObject itemView;

    [SerializeField]
    private List<GameObject> itemModels;

    [SerializeField]
    private List<GameObject> itemObjects;


    [SerializeField] VariablesHolder variablesHolder;

    void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(SpinItem());
        StartCoroutine(SpinAngle());
        spinA = _h_spinA * Mathf.Sin(Random.Range(-20, 20)) + _t_spinA;
        spinB = _h_spinB * Mathf.Sin(Random.Range(-20, 20)) + _t_spinB;
        spinC = _h_spinC * Mathf.Sin(Random.Range(-20, 20)) + _t_spinC;
        _w_spinA = Random.Range(-20f, 20f);
        _w_spinB = Random.Range(-20f, 20f);
        _w_spinC = Random.Range(-20f, 20f);
        transform.localScale = Vector3.zero;

        
    }

    private IEnumerator FadeIn()
    {
        for (float i = 0; i < 1; i += 0.1f)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.025f);
        }
    }

    private IEnumerator SpinItem()
    {
        int modelIndex = Random.Range(0, itemModels.Count);
        for (float i = 0; i < 5; i += 0.25f)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            itemView = Instantiate(itemModels[modelIndex], transform.position, transform.rotation, transform);
            itemView.transform.localScale = transform.localScale;
            itemView.transform.rotation = transform.rotation;
            modelIndex = (modelIndex + 1) % itemModels.Count;
            yield return new WaitForSeconds(0.1f);
        }       
        SelectCorrectItem();
    }

    private void SelectCorrectItem()
    {

        switch (itemView.name)
        {
            case "Peao(Clone)":
                variablesHolder.selectedItemP1 = itemObjects[0];
                    break;
            case "Cone(Clone)":
                variablesHolder.selectedItemP1 = itemObjects[1];
                break;
        }

        variablesHolder.itemUsed = false;

    }

    private IEnumerator SpinAngle()
    {
        while (true)
        {
            transform.Rotate((speed + spinA) * Time.deltaTime, (speed + spinB) * Time.deltaTime, (speed + spinC) * Time.deltaTime);
            if (itemView)
            {
                itemView.transform.rotation = transform.rotation;
            }
            _w_spinA = _w_spinA + _d_w_spinA;
            spinA = _h_spinA * Mathf.Sin(_w_spinA) + _t_spinA;
            _w_spinB = _w_spinB + _d_w_spinB;
            spinB = _h_spinB * Mathf.Sin(_w_spinB) + _t_spinB;
            _w_spinC = _w_spinC + _d_w_spinC;
            spinC = _h_spinC * Mathf.Sin(_w_spinC) + _t_spinC;
            yield return null;
        }
    }


}
