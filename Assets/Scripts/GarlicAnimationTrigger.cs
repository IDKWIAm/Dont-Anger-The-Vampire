using UnityEngine;

public class GarlicAnimationTrigger : MonoBehaviour
{
    private BossAttack garlic;
    void Start()
    {
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).childCount > 0) garlic = transform.GetChild(0).GetChild(0).GetComponent<BossAttack>();
            else garlic = transform.GetChild(0).GetComponent<BossAttack>();
        }
    }

    public void Throw()
    {
        garlic.Throw();
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
