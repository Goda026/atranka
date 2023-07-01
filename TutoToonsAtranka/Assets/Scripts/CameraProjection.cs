using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProjection : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bg;
    
    void Start()
    {      
        float worldScreenHeight = Camera.main.orthographicSize * 2;

        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
            worldScreenWidth / bg.sprite.bounds.size.x,
            worldScreenHeight / bg.sprite.bounds.size.y, 1);
    }

    
}
