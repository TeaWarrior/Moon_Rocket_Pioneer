using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        
    }

    private void Update()
    {
        // Поворачиваем объект в сторону камеры
        transform.LookAt(mainCamera.transform.position);
    }
}
