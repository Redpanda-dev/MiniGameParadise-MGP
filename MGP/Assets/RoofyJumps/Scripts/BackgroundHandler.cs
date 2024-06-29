using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 0.2f;

    Transform transformer;

    void Start()
    {
        transformer = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        transformer.localPosition += new Vector3(speed*Time.deltaTime, 0, 0);
    }
}
