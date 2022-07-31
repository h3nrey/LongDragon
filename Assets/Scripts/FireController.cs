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

    private void Start() {
        canShoot = true;
    }

    private void FixedUpdate() {
        foreach (GameObject projectille in projectilles) {
            //print($"projectille name: {projectille.name}");
            //projectille.transform.Translate(Vector2.up * projectilleSpeed);
            //projectille.GetComponent<Rigidbody2D>().AddForce(transform.right * projectilleSpeed * Time.fixedDeltaTime,ForceMode2D.Impulse);
        }
    }

    public void createFire() {
        if(canShoot) {
            GameObject fire = Instantiate(fireProjectille, cannonPoint.position, Quaternion.identity);
            canShoot = false;
            projectilles.Add(fire);

            fire.GetComponent<Rigidbody2D>().AddForce(transform.right * projectilleSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            Invoke("enableShoot", projectilleCooldown);

        }
    }

    private void enableShoot() {
        canShoot = true;
    }

    public void Shoot(InputAction.CallbackContext context) {
        if (context.started) createFire();
    }
}
