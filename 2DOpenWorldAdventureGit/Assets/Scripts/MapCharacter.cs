using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (GameStatus.status.currentLevel != "")
        {
            GameObject.Find(GameStatus.status.currentLevel).GetComponent<GoToLevel>().Cleared(true);
            // Jos tullaan tasosta esim. Level1-karttaan, etsitään Map-scenestä currentLevel-arvon mukainen GameObject
            // ja otetaan siitä sen ensimmäinen child-objekti sekä sen sijainti.
            transform.position = GameObject.Find(GameStatus.status.currentLevel).transform.GetChild(0).transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoToLevel"))
        {
            GameStatus.status.currentLevel = collision.gameObject.name;
            SceneManager.LoadScene(collision.GetComponent<GoToLevel>().level);
        }
    }
}
