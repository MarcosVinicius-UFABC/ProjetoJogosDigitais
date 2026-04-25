using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject boss;
    private bool bossExisted = true;

    void Update()
    {
        if(boss == null && bossExisted)
        {
            bossExisted = false;
            gameObject.GetComponent<VictoryScreen>().Pause();
        }
        else
        {
            return;
        }
    }
}
