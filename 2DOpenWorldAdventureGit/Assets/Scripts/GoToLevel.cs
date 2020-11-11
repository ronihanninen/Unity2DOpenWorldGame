using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevel : MonoBehaviour
{
    public string level;
    public bool cleared;

    // Start is called before the first frame update
    void Start()
    {
        // Kun karttascene avataan, jokainen level trigger käy tarkastamassa GameStatuksesta, onko se päästy läpi.
        // Jos on, laittaa se oman cleared-muuttajan arvoksi true.
        if (GameStatus.status.GetType().GetField(level).GetValue(GameStatus.status).ToString() == "True")
        {
            Cleared(true);
        }
    }

    public void Cleared(bool isClear)
    {
        if (isClear == true)
        {
            cleared = true;
            // Casting
            GameStatus.status.GetType().GetField(level).SetValue(GameStatus.status, true);
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
