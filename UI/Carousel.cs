using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    #region Variables

    private Vector3 positionVector;
    [SerializeField] private float moveSpeed = 3f;

    #endregion

    #region Cached

    private RectTransform rectTransform;

    #endregion

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        positionVector = rectTransform.position;
    }

    void Update()
    {
        if (rectTransform.position.y <= positionVector.y - rectTransform.rect.height)
            rectTransform.position = positionVector;

        rectTransform.position = new Vector3(rectTransform.position.x, 
            rectTransform.position.y - moveSpeed * Time.deltaTime, rectTransform.position.z);
        
    }
}
