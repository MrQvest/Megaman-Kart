using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    private float speed = 30f;
    void Start()
    {
        StartCoroutine(SpinAnimation());
    }

    private IEnumerator SpinAnimation()
    {
        var _h_spinA = (20 - (-20)) / 2;
        var _t_spinA = (20 + (-20)) / 2;
        var spinA = _h_spinA * Mathf.Sin(Random.Range(-20, 20)) + _t_spinA;
        var _w_spinA = Random.Range(-20f, 20f);
        var _d_w_spinA = 1.5f;
        var _h_spinB = (20 - (-20)) / 2;
        var _t_spinB = (20 + (-20)) / 2;
        var spinB = _h_spinB * Mathf.Sin(Random.Range(-20, 20)) + _t_spinB;
        var _w_spinB = Random.Range(-20f, 20f);
        var _d_w_spinB = 1.5f;
        var _h_spinC = (20 - (-20)) / 2;
        var _t_spinC = (20 + (-20)) / 2;
        var spinC = _h_spinC * Mathf.Sin(Random.Range(-20, 20)) + _t_spinC;
        var _w_spinC = Random.Range(-20f, 20f);
        var _d_w_spinC = 1.5f;
        for (int i = 0; i < 99999; i++)
        {
            transform.Rotate((speed + spinA) * Time.deltaTime, (speed + spinB) * Time.deltaTime, (speed + spinC) * Time.deltaTime);

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
