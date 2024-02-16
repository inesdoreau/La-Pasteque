using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class JellyMesh : MonoBehaviour
{
    private const float SplineOffset = 0.5f;
    [SerializeField] public SpriteShapeController spriteShape;
    [SerializeField] public Transform[] points;

    private void Awake()
    {
        UpdateVertices();
    }

    private void Update()
    {
        UpdateVertices();
    }

    private void UpdateVertices()
    {
        for (int i = 0; i < points.Length -1 ; i++)
        {
            Vector2 _vertex = points[i].localPosition;
            Vector2 _towardCenter = (Vector2.zero - _vertex).normalized;

            float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                spriteShape.spline.SetPosition(i, (_vertex - _towardCenter * _colliderRadius));
            }
            catch
            {
                spriteShape.spline.SetPosition(i, (_vertex - _towardCenter * (_colliderRadius + SplineOffset)));
            }

            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);
            Vector2 _newRt = Vector2.Perpendicular(_towardCenter) * _lt.magnitude;
            Vector2 _newLt = -_newRt;

            spriteShape.spline.SetLeftTangent(i, _newLt);
            spriteShape.spline.SetRightTangent(i, _newRt);
        }
    }
}
