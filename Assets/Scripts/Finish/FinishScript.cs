using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    List<GameObject> listOfOpponents = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        listOfOpponents.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MartialHero")
        {
            if (listOfOpponents.Count <= 0)
            {
                // They are dead!
                Invoke("CompleteLevel", 1);
            }
            
            //add delay and fade to blaxk
            
        }
    }
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KilledOpponent(GameObject opponent)
    {
        if (listOfOpponents.Contains(opponent))
        {
            listOfOpponents.Remove(opponent);
        }
    }
    
}
