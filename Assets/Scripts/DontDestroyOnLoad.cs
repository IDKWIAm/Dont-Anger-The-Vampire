using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] bool allowDuplicates = true;

    void Start()
    {
        if (!allowDuplicates && gameObject.tag != "Default")
        {
            GameObject[] cloneGameObject = GameObject.FindGameObjectsWithTag(gameObject.tag);
            if (cloneGameObject.Length > 1) Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
