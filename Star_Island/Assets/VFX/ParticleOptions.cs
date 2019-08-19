using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOptions : MonoBehaviour
{
    ParticleSystem ptcSystem;

    private void Awake()
    {
        ptcSystem = GetComponent<ParticleSystem>();
    }

    private IEnumerator Follow(Transform target)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (target != null)
                transform.position = target.position;
            else
            {
                Destroy(gameObject, ptcSystem.main.duration);
                yield break;
            }
        }
    }
    public void Spawn(Transform target, bool follow = false, bool autoDestroy = false)
    {
        transform.position = target.position;

        if (follow)
            StartCoroutine(Follow(target));

        if (autoDestroy)
            Destroy(gameObject, ptcSystem.main.duration);
    }
}
