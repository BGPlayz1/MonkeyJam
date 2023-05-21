using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
   public  GameObject player;
    void Awake()
    {
        
        player.GetComponent<Stats>().OnDeath += TriggerRespawnTimer;
    }

    private void TriggerRespawnTimer()
    {
        StartCoroutine(StartRespawnCoroutine());
    }
    public IEnumerator StartRespawnCoroutine()
    {
        Debug.Log("On");
        player.SetActive(false);
        yield return new WaitForSeconds(2);
        Debug.Log("off");
        player.SetActive(true);
    }
}