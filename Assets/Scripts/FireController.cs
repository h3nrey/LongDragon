using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireController : MonoBehaviour {

    //Read & Write
    private bool  canShoot{
        get => PlayerBehaviour.instance.canShoot;
        set => PlayerBehaviour.instance.canShoot = value;
    }

    //Readonly
    private List<GameObject> projectilles => PlayerBehaviour.instance.projectilles;
    private GameObject fireProjectille => PlayerBehaviour.instance.fireProjectille;
    private Transform cannonPoint => PlayerBehaviour.instance.canonPoint;
    private float projectilleSpeed => PlayerBehaviour.instance.projecctilleSpeed;
    private float projectilleCooldown => PlayerBehaviour.instance.projectilleCooldown;

    private List<Transform> segments => PlayerBehaviour.instance.segments;
    private SpriteRenderer graphics => PlayerBehaviour.instance._graphics;

    private void Start() {
        canShoot = true;
    }
    public void createFire() {
        if(canShoot) {
            GameObject fire = Instantiate(fireProjectille, cannonPoint.position, Quaternion.identity);
            canShoot = false;
            projectilles.Add(fire);

            fire.GetComponent<Rigidbody2D>().AddForce(transform.right * projectilleSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

            StartCoroutine(ChangeSpriteColor(0.5f));
            
            Invoke("enableShoot", projectilleCooldown);

        }
    }

    private void enableShoot() {
        canShoot = true;
        StartCoroutine(ChangeSpriteColor(1));
    }

    private IEnumerator ChangeSpriteColor(float colorValue) {
        yield return new WaitForSeconds(0);
        graphics.color = new Color(1,1,1,colorValue);

        for (int i = 0; i < segments.Count; i++) {
            if(i > 0) {
                yield return new WaitForSeconds(0.1f * i);
                segments[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, colorValue);
            }
        }
    }

    public void Shoot(InputAction.CallbackContext context) {
        if (context.started) createFire();
    }
}
