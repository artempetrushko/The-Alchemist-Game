using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveObjectInfoView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text interactiveObjectTitle;
    [SerializeField]
    private ControlsTipsSectionView controlsTipsSectionView;
    [Space, SerializeField]
    private float viewOffsetMultiplier = 3f;

    private Vector3 viewPosition;

    public ControlsTipsSectionView ControlsTipsSectionView => controlsTipsSectionView;
   
    public void SetInfo(string objectName, Transform objectTransform)
    {
        interactiveObjectTitle.text = objectName;

        var currentObjectMesh = objectTransform.GetComponent<MeshRenderer>();
        var currentObjectHeight = currentObjectMesh != null ? currentObjectMesh.bounds.size.y : 0;
        viewPosition = objectTransform.position + new Vector3(0, currentObjectHeight * viewOffsetMultiplier, 0);
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        transform.position = (Vector2)Camera.main.WorldToScreenPoint(viewPosition);
    }
}
