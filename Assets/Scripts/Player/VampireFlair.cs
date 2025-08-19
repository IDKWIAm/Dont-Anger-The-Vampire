using UnityEngine;
using System.Collections.Generic;

public class VampireFlair : MonoBehaviour
{
    private GameObject[] interactablesArray;
    private List<GameObject> interactablesList = new List<GameObject>();

    private void Start()
    {
        interactablesArray = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject gameObject in interactablesArray)
            interactablesList.Add(gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            for (int i = 0; i < interactablesList.Count; i++)
            {
                interactablesList[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (Input.GetButtonUp("Fire3"))
        {
            for (int i = 0; i < interactablesList.Count; i++)
            {
                interactablesList[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void AddObject(GameObject Object)
    {
        Object.tag = "Interactable";
        interactablesList.Add(Object);
    }

    public void DeleteObject(GameObject Object)
    {
        Object.tag = "Untagged";
        interactablesList.Remove(Object);
    }
}