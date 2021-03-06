using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float disableAfterSeconds = 1f;
    [SerializeField] AudioClip impactSound;
    [SerializeField] GameObject destroyParticles;
    bool destroyAfter = false;

    LayerMask whatIsEnemy;
    public Action onHit;
    Vector2 lastPos;
    int damage;

    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    public void SetDestroyAfterwards(bool value)
    {
        destroyAfter = value;
    }

    private void FixedUpdate()
    {
        lastPos = transform.position;
        transform.position += transform.right * Time.fixedDeltaTime * speed;
        Vector2 difference = (Vector2)transform.position - lastPos;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, difference.normalized, difference.magnitude, whatIsEnemy);
        if (hit)
        {
            DamageObject(hit);
        }

    }

    private void DamageObject(RaycastHit2D hitObject)
    {
        IDamageable damageable = hitObject.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (impactSound != null)
                AudioManager.Instance.PlaySFX(impactSound);
            damageable.TakeDamage(damage, transform.position);
            onHit?.Invoke();
        }
        Destroy();
    }

    private void Destroy()
    {
        if (destroyParticles != null)
        {
            Instantiate(destroyParticles, transform.position, Quaternion.identity);
        }
        if (!destroyAfter)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }

    public void Initialize(LayerMask whatIsEnemy, int damage)
    {
        this.whatIsEnemy = whatIsEnemy;
        this.damage = damage;
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(disableAfterSeconds);
        Destroy();
    }
}
