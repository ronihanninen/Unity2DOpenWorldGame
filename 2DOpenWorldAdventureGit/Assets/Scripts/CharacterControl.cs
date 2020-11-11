using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D playerRB;
    public Animator animator;

    public float counter;
    public float maxCounter;
    public Image filler;

    public GameObject bonfire;
    public GameObject axe;
        
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameStatus.status.previousHealth = GameStatus.status.health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            // Kotitehtävä: Katso YT:stä "Unity Better Jumping with 4 Lines of Code" jo toteuta se tähän.
            // Estä pelaajaa hyppäämästä ilmassa eli tee grounded boolean -muuttuja

            playerRB.velocity = new Vector2(0, jumpForce); // Kolme vaikuttavaa asiaa: jumpForce, Mass, Gravity Scale
            animator.SetTrigger("Jump");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject axeInstance = Instantiate(axe, transform.position + new Vector3(3 * transform.localScale.x, 0, 0), Quaternion.identity);
            axeInstance.GetComponent<Axe>().rotateSpeed *= transform.localScale.x;
            axeInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(20 * transform.localScale.x, 10), ForceMode2D.Impulse);
        }

        // Counter, joka lähtee nollasta ja menee maxCounteriin ja kun pääsee sinne, aloittaa jälleen nollasta.
        if (counter < maxCounter)
        {
            GameStatus.status.previousHealth = GameStatus.status.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(GameStatus.status.previousHealth / GameStatus.status.maxHealth, GameStatus.status.health / GameStatus.status.maxHealth, counter / maxCounter);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(bonfire, transform.position + new Vector3(3*transform.localScale.x,0,0), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPickup"))
        {
            AddHealth(20);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("MaxHealthPickup"))
        {
            AddMaxHealth(10);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonfire"))
        {
            AddHealth(Time.deltaTime);
        }
    }

    private void AddHealth(float amount)
    {
        counter = 0;
        GameStatus.status.previousHealth = GameStatus.status.health;
        GameStatus.status.health += amount;
        if (GameStatus.status.health > GameStatus.status.maxHealth)
        {
            GameStatus.status.health = GameStatus.status.maxHealth;
        }
    }

    private void AddMaxHealth(float amount)
    {
        GameStatus.status.maxHealth += amount;
    }

    private void TakeDamage(float dmg)
    {
        GameStatus.status.previousHealth = filler.fillAmount * GameStatus.status.maxHealth;
        counter = 0;
        GameStatus.status.health -= dmg;
    }
}

/* K0titehtävä
    JOs haluaa, muuta game of balls peli käyttämään 2D fysiikkamoottoria. Muuta koodit, colliderit, rigidbodyt yms.
    jotta toimii 2D versiona.

    Tasohyppelypeliin:
    Tee esine, jonka keräämällä health arvo nousee 20
    Tee esine, jonka keräämällä maxhealth arvo nousee 10
    Tee ominaisuus, että kun painetaan f kirjainta, pelaajan eteen instansioituu nuotio ja jos pelaaja on lähellä
    nuotiota, health arvo kasvaa pikkuhiljaa
    Tee ominiasuus, että kun painetaan hiiren nappulaa, pelaaja heittää kirveen. Kirves pyörii ilmassa ja jos 
    osuu kärjestään Tag: Obstacle kappaleeseen, pyöriminen loppuu ja kirven jää kiinni. Pelaaja voi hypätä kirveen pääle
    kirveen päälle ja käyttää sitä apuna. 
    Tee Map scene, jossa on 3 empty gameobjectia ja pelaaja (Map Character)
    Map Character liikkuu w,a,s,d ja jos osuu mepty gameobjectiin aukeaa jokin tasohyppelyscene.
    Tee tasohyppelyscenen loppuun partikkeliefekti, kun pelaaja osuu partikkeliin, scene map avautuu. 



*/
