using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DrawLine : MonoBehaviour{

    public static bool CanDraw = true;

    public delegate void StartEvent();
    public static event StartEvent OnStart;

    public float startWidth = 1.0f;
    public float endWidth = 1.0f;
    public const float threshold = 0.1f;

    public Material lineMaterial;
    public Color lineColor = new Color(0.2f, 0.8f, 0.3f);

    private static List<GameObject> lineGameObject = new List<GameObject>();
    private static List<LineRenderer> lineRenderer = new List<LineRenderer>();
    private static List<EdgeCollider2D> lineEdgeColl2D = new List<EdgeCollider2D>();

    private List<Vector2> edgeCollPoints = new List<Vector2>();

    private EdgeCollider2D edgeCollider;

    private Camera thisCamera;
    private int lineCount = 0;

    private Vector3 lastPos = Vector3.one * float.MaxValue;
    private bool drewLine = false;
    private int currentLine = 0;

    private void Awake()
    {
        thisCamera = Camera.main;
    }

    private void Update()
    {
        if (CanDraw)
        {
            Vector3 cam_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(cam_pos, Vector3.forward);

            if (hit.collider == null || hit.collider.tag == "DrawnLine" )
            {
                if (!drewLine)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        lineGameObject.Add(new GameObject("Line: " + currentLine));
                        lineGameObject[currentLine].tag = "DrawnLine";
                        Vector3 mousePos = Input.mousePosition;

                        lineRenderer.Add(lineGameObject[currentLine].AddComponent<LineRenderer>());
                        lineEdgeColl2D.Add(lineGameObject[currentLine].AddComponent<EdgeCollider2D>());

                        lineEdgeColl2D[currentLine].edgeRadius = 0.06f;

                        //lineRenderer[currentLine].useWorldSpace = true;
                        lineRenderer[currentLine].positionCount = 0;
                        lineRenderer[currentLine].startWidth = startWidth;
                        lineRenderer[currentLine].endWidth = endWidth;
                        lineRenderer[currentLine].material = lineMaterial;
                        lineRenderer[currentLine].startColor = lineColor;
                        lineRenderer[currentLine].endColor = lineColor;

                        drewLine = true;
                    }
                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        Vector3 mousePos = Input.mousePosition;
                        mousePos.z = thisCamera.nearClipPlane + 0.01f;
                        Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);

                        float dist = Vector3.Distance(lastPos, mouseWorld);
                        if (dist <= threshold)
                            return;

                        lastPos = mouseWorld;
                        lineRenderer[currentLine].positionCount++;
                        lineRenderer[currentLine].SetPosition(lineCount++, mouseWorld);

                        edgeCollPoints.Add(mouseWorld);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        ReleaseLine();
                    }
                }
            }
            else if (drewLine)
            {
                ReleaseLine();
            }
        }
    }

    private void ReleaseLine()
    {
        drewLine = false;

        if (edgeCollPoints.Count > 0)
        {
            lineEdgeColl2D[currentLine].points = edgeCollPoints.ToArray();

            ActorLogic.rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            if(OnStart != null)
            {
                OnStart();
            }

            currentLine++;
            lineCount = 0;
            edgeCollPoints.Clear();
        }
    }

    private void OnDestroy()
    {
        lineGameObject.Clear();
        lineRenderer.Clear();
        lineEdgeColl2D.Clear();
        CanDraw = true;
    }
}

