using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;

    RectTransform transformer;

    void Start()
    {
        transformer = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        transformer.localPosition += new Vector3(speed*Time.deltaTime, 0, 0);
    }
}
