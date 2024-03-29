using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModScripts
{
    public class ShieldScript : MonoBehaviour, IUseItem
    {
        private GameObject nearestObject;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(FadeAnim());
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
            float nearestDistance = Mathf.Infinity;

            // Para cada GameObject chamado obj com a Tag Player
            foreach (GameObject obj in objectsWithTag)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                // Se a dist�ncia atual for menor que Infinito
                if (distance < nearestDistance)
                {
                    nearestObject = obj;

                    // Infinito ser�Edist�ncia atual
                    nearestDistance = distance;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = nearestObject.transform.position;
        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position, rotation);
        }

        private IEnumerator FadeAnim()
        {
            for (float i = 0; i < 1; i += 0.1f)
            {
                transform.localScale = new Vector3(i * 5, i * 5, i * 5);
                yield return new WaitForSeconds(0.025f);
            }
            yield return new WaitForSeconds(3f);
            for (float i = 1; i > 0; i -= 0.1f)
            {
                transform.localScale = new Vector3(i * 5, i * 5, i * 5);
                yield return new WaitForSeconds(0.025f);
            }
            Destroy(gameObject);
        }
    }
}
