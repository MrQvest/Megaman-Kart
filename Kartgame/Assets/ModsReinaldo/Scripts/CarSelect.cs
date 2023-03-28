using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour
{
    private GameObject[] carList;
    private int index = 3;

    private void Start()
    {

        
        // Store player preference of previously selected car
        index = PlayerPrefs.GetInt("Character Selected");

        // Gets a list of the number of cars available
        carList = new GameObject[transform.childCount];

        
        for (int i = 0; i < transform.childCount; i++)
        {

            // Make the cars visible 
            carList[i] = transform.GetChild(i).gameObject;
        }

        // Each and every car gets Deactivated
        foreach (GameObject car in carList)
        {
            car.SetActive(false);
        }

        // If car "x" was previously selected...
        if (carList[index])
        {

            // Activate car "x" (Show it on screen next time)
            carList[index].SetActive(true);
        }
    }

    public void PreviousCar()
    {
        carList[index].SetActive(false);

        index--;

        if (index < 0)
        {
            index = carList.Length - 1;
        }

        carList[index].SetActive(true);
    }

    public void NextCar()
    {
        carList[index].SetActive(false);

        index++;

        if (index == carList.Length)
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
