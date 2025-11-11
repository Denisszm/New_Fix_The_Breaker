using UnityEngine;
using System.Collections;

public class StalkerScript : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float appearanceInterval;
    private int appearanceCount = 0;
    private float timer = 0f;
    private bool isBerserk = false;
    public float spawnDistance;

    public GameObject EnemySprite;

    private void Start()
    {
        EnemySprite.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= appearanceInterval)
        {
            appearanceCount++;
            timer = 0f;

            if (appearanceCount < 3)
            {
                StartCoroutine(AppearForSeconds(3f));
                Debug.Log("Appearance #" + appearanceCount);
            }
            else if (appearanceCount == 3)
            {
                isBerserk = true;
                EnemySprite.SetActive(true);
                Debug.Log("Stalker has gone berserk!");
            }
        }

        if (isBerserk)
        {
            ChasePlayer();
        }

    }

    IEnumerator AppearForSeconds(float duration)
    {
        this.transform.position = GetSpawnPositionAroundPlayer();
        EnemySprite.SetActive(true);
        Debug.Log("Enemy visible for " + duration + " seconds");
        yield return new WaitForSeconds(duration);

        if (!isBerserk)
        {
            EnemySprite.SetActive(false);
            Debug.Log("Enemy hidden again");
        }
    }

    private Vector2 GetSpawnPositionAroundPlayer()
    {
        float angle = Random.Range(0f, 360f);
        float radians = angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * spawnDistance;
        return (Vector2)player.position + offset;
    }

    private void ChasePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Fake Player")
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
