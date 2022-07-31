using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour {
    private List<Transform> segments {
        get => PlayerBehaviour.instance.segments;
        set => PlayerBehaviour.instance.segments = value;
    }
    [SerializeField] Transform PlayerBody;
    
    [SerializeField] Sprite[] bodySprites;
    [SerializeField] float partOffset = 1f;
    [SerializeField] bool canEat = true;

    [SerializeField] GameObject partPrefab;
    [SerializeField] private Transform target;

    [SerializeField] Transform bodyPartSpawner; // Debug
      
    [SerializeField] float followSpeed;
    Vector2 lastPos = new Vector2(-1,0);
    [SerializeField] Vector2 Offset = new Vector2(0.2f, 0.2f);
    void Start() {
        target = this.transform;
        //AddPart(bodySprites[0]);
        segments.Add(this.transform);
    }

    private void Update() {
        bodyPartSpawner.position = GenerateSpawnerPoint(segments[segments.Count - 1].position);
    }

    private void FixedUpdate() {
        for (int i = segments.Count - 1; i > 0; i--) {
            //segments[i].position = Vector2.MoveTowards(segments[i].position, GenerateSpawnerPoint(segments[i - 1].position), Time.fixedDeltaTime * followSpeed);
            segments[i].position = GenerateSpawnerPoint(segments[i - 1].position);
            segments[i].rotation = segments[i - 1].rotation;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject obj = other.gameObject;
        string objTag = obj.tag;

        switch (objTag) {
            case "food":
                Destroy(obj);
                AddPart(bodySprites[1]);
                break;
        }
    }

    void AddPart(Sprite partSprite) {
        if (canEat) {
            float pos = transform.position.x;
            float partPos = pos - (partOffset * segments.Count * transform.right.x);

            if (segments.Count > 0) target = AddTargetPart();

            GameObject part = Instantiate(partPrefab, bodyPartSpawner.position, transform.rotation);
            canEat = false;
            Invoke("EnableEat", 0.2f);

            part.transform.SetParent(PlayerBody);

            part.GetComponent<SpriteRenderer>().sprite = partSprite;

            part.GetComponent<PartBehaviour>().target = target;

            part.GetComponent<PartBehaviour>().rotatePoint = this.transform;

            part.GetComponent<PartBehaviour>().partPosition = segments.Count;

            segments.Add(part.transform);
        }
    }

    Transform AddTargetPart() {
        GameObject targetObject = Instantiate(new GameObject(), GenerateSpawnerPoint(transform.position), transform.rotation);
        targetObject.name = "target";
        targetObject.transform.SetParent(this.transform);
        return targetObject.transform;
    }

    Vector2 GenerateSpawnerPoint(Vector2 basePos) {
        Vector2 pos = PlayerBehaviour.instance.input;
        if (pos.magnitude != 0) lastPos = pos - (Offset * transform.right);
        Vector2 target = new Vector2(0,0);

        target = basePos - lastPos;

        return target;
    }


    private void EnableEat() {
        canEat = true;
    }
}