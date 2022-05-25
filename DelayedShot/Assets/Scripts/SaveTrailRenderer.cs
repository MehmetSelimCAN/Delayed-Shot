using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrailRenderer : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private LineRenderer lineRenderer;
    private Vector3[] TrailRecorded = new Vector3[10000];
    private int numberOfPositions;

    private void Awake() {
        trailRenderer = GetComponent<TrailRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void CreatePastTrail() {
        SaveTrail();
        DestroyCurrentTrail();
        DestroyLineRenderer();
        CreateLineRenderer();
    }

    private void SaveTrail() {
        numberOfPositions = trailRenderer.GetPositions(TrailRecorded);
    }

    public void DestroyCurrentTrail() {
        trailRenderer.Clear();
    }

    private void CreateLineRenderer() {
        lineRenderer.positionCount = numberOfPositions;
        lineRenderer.SetPositions(TrailRecorded);
    }

    private void DestroyLineRenderer() {
        lineRenderer.positionCount = 0;
    }
}
