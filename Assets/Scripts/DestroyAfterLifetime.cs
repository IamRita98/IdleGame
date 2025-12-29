using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{
    [SerializeField] float lifetime;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime) KillSelf();
    }

    void KillSelf()
    {
        Destroy(this.gameObject);
    }
}
