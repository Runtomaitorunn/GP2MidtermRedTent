using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iterations : MonoBehaviour
{
    public int iteration = 0;
    public GameObject gridPrefab;
    public GameObject star;



    private void Update()
    {
        if (iteration >= 3)
        {
            ChangeFogColor();
        }
        if (iteration == 8)
        {
            LoadEndScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tent"))
        {

            DestroyPreviousGrid();
            DestroyPreviousTent();
            GameObject newGridObject = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("generated!");
            iteration++;

        }
    }
    public void DestroyPreviousGrid()
    {
        GameObject previousGrid = GameObject.Find("Grid");
        if (previousGrid != null)
        {
            Destroy(previousGrid);
        }
        else
        {
            GameObject previousGridClone = GameObject.Find("Grid(Clone)");
            if (previousGridClone != null)
            {
                Destroy(previousGridClone);
            }
        }

    }
    public void DestroyPreviousTent()
    {
        GameObject previousTent = GameObject.Find("Tent(Clone)");
        if (previousTent != null)
        {
            Destroy(previousTent);
        }
    }

    public void ChangeFogColor()
    {
        StarColor starcolor = star.GetComponent<StarColor>();
        starcolor.StarsColor();

    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndGame");
    }
}
