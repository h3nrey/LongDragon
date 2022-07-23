using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehaviour : MonoBehaviour
{
    private void FixedUpdate() {
        Flying();
    }

    private void Flying() {
        if (PlayerBehaviour.instance.pressingFlybutton) {
            if(PlayerBehaviour.instance.flySpeed <= PlayerBehaviour.instance.maxFlySpeed) {
                PlayerBehaviour.instance.rb.gravityScale = 0;
                PlayerBehaviour.instance.rb.velocity = new Vector2(PlayerBehaviour.instance.velX, PlayerBehaviour.instance.flySpeed * Time.fixedDeltaTime);

                PlayerBehaviour.instance.flySpeed += PlayerBehaviour.instance.flySpeedIncrementer;
            }
            
        } else {
            PlayerBehaviour.instance.rb.gravityScale = PlayerBehaviour.instance.gravityScale;
            PlayerBehaviour.instance.flySpeed = PlayerBehaviour.instance.baseFlySpeed;
        }
    }
}
