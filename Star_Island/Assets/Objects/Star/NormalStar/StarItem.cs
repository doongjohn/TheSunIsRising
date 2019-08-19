using System.Collections;
using UnityEngine;

public class StarItem : MonoBehaviour
{
    [Header("Consume")]
    [SerializeField]
    private float consumeSpeed;
    [SerializeField]
    private int starPower = 1;

    [Header("Effects")]
    [SerializeField]
    private GameObject collectedEffect;
    [SerializeField]
    private GameObject consumedEffect;
    [SerializeField]
    private GameObject trailEffect;

    private float speed;

    private IEnumerator MoveToStar(Transform target)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            Vector3 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * consumeSpeed * Time.deltaTime, Space.World);

            if (Vector2.Distance(target.position, transform.position) <= 0.2f)
            {
                Instantiate(consumedEffect).GetComponent<ParticleOptions>().Spawn(transform);
                Destroy(gameObject);
                yield break;
            }
        }
    }
    public int Consume(Transform target)
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(MoveToStar(target));

        // Effect
        Instantiate(consumedEffect).GetComponent<ParticleOptions>().Spawn(transform, autoDestroy:true);
        Instantiate(trailEffect).GetComponent<ParticleOptions>().Spawn(transform, true);
        return starPower;
    }
}
