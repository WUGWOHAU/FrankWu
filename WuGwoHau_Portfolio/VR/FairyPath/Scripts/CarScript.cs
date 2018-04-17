using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

    [SerializeField]
    float CarRotationSpeed = 1.0f;
    [SerializeField]
    float CarSpeed = 0;

    [SerializeField]
    MagicRockManager MagicRock;

    void Start() {
    }

    void Update() {

        CarRotation();

        if (MagicRock.MoveFlag == true) {
            CarMove();
        }

    }

    void CarRotation() {

        //キーボードモード
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(new Vector3(0f, -CarRotationSpeed, 0f));
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(new Vector3(0f, CarRotationSpeed, 0f));
        }

    }

    void CarMove() {
        transform.position += transform.forward * CarSpeed * Time.deltaTime;
    }
}
