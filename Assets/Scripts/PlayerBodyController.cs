using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    [SerializeField] Transform PlayerBody;
    [SerializeField] List<Transform> bodyParts;
    [SerializeField] Sprite[] bodySprites;
    [SerializeField] float partOffset = 1f;
    [SerializeField] bool canEat = true;

    [SerializeField] GameObject partPrefab;
    [SerializeField] private Transform target;

    [SerializeField] Transform bodyPartSpawner; // Debug
    void Start() {
        target = this.transform;
        AddPart(bodySprites[0]);
    }

    private void Update() {
        float pos = transform.position.x;
        float targetPos = pos - (partOffset * bodyParts.Count * transform.right.x);
        bodyPartSpawner.position = new Vector3(targetPos, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject obj = other.gameObject;
        string objTag = obj.tag;

        switch(objTag) {
            case "food":
                Destroy(obj);
                AddPart(bodySprites[1]);
                break;
        }
    }

    void AddPart(Sprite partSprite) {
        if(canEat) {
            float pos = transform.position.x;
            float partPos = pos - (partOffset * bodyParts.Count * transform.right.x);

            if (bodyParts.Count > 0) target = AddTargetPart();

            GameObject part = Instantiate(partPrefab,bodyPartSpawner.position, transform.rotation);
            canEat = false;
            Invoke("EnableEat", 0.2f);

            part.transform.SetParent(PlayerBody);

            part.GetComponent<SpriteRenderer>().sprite = partSprite;

            part.GetComponent<PartBehaviour>().target = target;

            part.GetComponent<PartBehaviour>().rotatePoint = this.transform;

            bodyParts.Add(part.transform);
        }
    }

    Transform AddTargetPart() {
        GameObject targetObject = Instantiate(new GameObject(), GenerateSpawnerPoint(), transform.rotation);
        targetObject.name = "target";
        targetObject.transform.SetParent(this.transform);
        return targetObject.transform;
    }

    Vector2 GenerateSpawnerPoint() {
        float pos = transform.position.x;
        float targetPos = pos - (partOffset * bodyParts.Count * transform.right.x);
        return new Vector2(targetPos, transform.position.y);
    }


    private void EnableEat() {
        canEat = true;
    }
}
