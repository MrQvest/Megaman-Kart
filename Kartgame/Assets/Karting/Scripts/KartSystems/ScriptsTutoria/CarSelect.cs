using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour
{
    private GameObject[] carList;
    private int index = 3; // The index of the currently selected car (default value is 3)

    private void Start()
    {
        // Retrieve the index of the previously selected car from player preferences
        index = PlayerPrefs.GetInt("Character Selected");

        // Create an array to store all the cars available in the scene
        carList = new GameObject[transform.childCount];

        // Fill the 'carList' array with all the cars by getting each child object of the current GameObject
        for (int i = 0; i < transform.childCount; i++)
        {
            carList[i] = transform.GetChild(i).gameObject;
        }

        // Deactivate all the cars initially to avoid showing multiple cars at the same time
        foreach (GameObject car in carList)
        {
            car.SetActive(false);
        }

        // Activate the previously selected car (as indicated by the 'index' value)
        if (index < carList.Length)
        {
            carList[index].SetActive(true);
        }
    }

    // Switch to the previous car in the list
    public void PreviousCar()
    {
        // Deactivate the current car
        carList[index].SetActive(false);

        // Decrement the index
        index--;

        // If the index is out of bounds, wrap around to the end of the list
        if (index < 0)
        {
            index = carList.Length - 1;
        }

        // Activate the new car
        carList[index].SetActive(true);
    }

    // Switch to the next car in the list
    public void NextCar()
    {
        // Deactivate the current car
        carList[index].SetActive(false);

        // Increment the index
        index++;

        // If the index is out of bounds, wrap around to the beginning of the list
        if (index == carList.Length)
        {
            index = 0;
        }

        // Activate the new car
        carList[index].SetActive(true);
    }

    // Save the selected car to player preferences and load the main game scene
    public void StartGame()
    {
        PlayerPrefs.SetInt("Character Selected", index);
        SceneManager.LoadSceneAsync("MainScene");
    }
}
