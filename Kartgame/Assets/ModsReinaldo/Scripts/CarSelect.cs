using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour
{
    private GameObject[] carList;
    private int index = 3;

    private void Start()
    {

       

        index = PlayerPrefs.GetInt("Character Selected");

        carList= new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            carList[i]= transform.GetChild(i).gameObject;
        }

        foreach(GameObject car in carList)
        {
            car.SetActive(false);
        }
        if (carList[index])
        {
            carList[index].SetActive(true);
        }
    }

    public void PreviousCar() 
    {
        carList[index].SetActive(false);

        index--;

        if(index < 0)
        {
            index = carList.Length - 1;
        }

        carList[index].SetActive(true);
    }

    public void NextCar()
    {
        carList[index].SetActive(false);

        index++;

        if(index == carList.Length) 
        {
            index = 0;
        }

        carList[index].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Character Selected", index);
        SceneManager.LoadSceneAsync("MainScene");
    }
}
