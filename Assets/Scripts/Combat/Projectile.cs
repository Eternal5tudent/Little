using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float disableAfterSeconds = 1f;

    LayerMask whatIsEnemy;
    public Action onHit;
    Vector2 lastPos;
    int damage;

    private void OnEnable()
    {
        StartCoroutine(Disable());
    }
    private void FixedUpdate()
    {
        lastPos = transform.position;
        transform.position += transform.right * Time.fixedDeltaTime * speed;
        Vector2 difference = (Vector2)transform.position - lastPos;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, difference.normalized, difference.magnitude, whatIsEnemy);
        if (hit != null)
        {
            foreach (RaycastHit2D hitObject in hit)
            {
                IDamageable damageable = hitObject.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                    onHit?.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }
       
    }

    public void Initialize(LayerMask whatIsEnemy, int damage)
    {
        this.whatIsEnemy = whatIsEnemy;
        this.damage = damage;
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(disableAfterSeconds);
        gameObject.SetActive(false);
    }
}
