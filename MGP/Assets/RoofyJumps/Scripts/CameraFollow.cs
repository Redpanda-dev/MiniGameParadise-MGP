using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject character;

    [SerializeField] GameObject leftLimit;
    [SerializeField] GameObject rightLimit;

    void Update()
    { 
        if(character != null) {
            checkPosition();
        }
    }

    void LateUpdate()
    {
        if(character != null) {
            if(character.transform.position.y > transform.position.y){
                Vector3 newPosition = new Vector3(transform.position.x, character.transform.position.y, transform.position.z);
                transform.position = newPosition;
            }
        }
    }

    void checkPosition()
    {
        float newY = character.transform.position.y;

        if(character.transform.position.x < leftLimit.transform.position.x) {

            float newX = rightLimit.transform.position.x;
            Vector2 newPos = new Vector2(newX, newY);

            character.transform.position = newPos;
        }

        if(character.transform.position.x > rightLimit.transform.position.x) {
            float newX = leftLimit.transform.position.x;
            Vector2 newPos = new Vector2(newX, newY);

            character.transform.position = newPos;
        }
    }
}
