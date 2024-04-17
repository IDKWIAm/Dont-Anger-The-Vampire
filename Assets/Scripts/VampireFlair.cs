using UnityEngine;

public class VampireFlair : MonoBehaviour
{
    private GameObject[] interactables;

    private void Start()
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            for (int i = 0; i < interactables.Length; i++)
            {
                interactables[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(2))
        {
            for (int i = 0; i < interactables.Length; i++)
            {
                interactables[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}