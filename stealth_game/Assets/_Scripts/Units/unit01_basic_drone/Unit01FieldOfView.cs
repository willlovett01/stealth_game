using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit01FieldOfView : MonoBehaviour {

    public float viewRadius;

    public LayerMask playerMask;
    public LayerMask visionBlocker;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    [Range(0,360)]
    public float viewAngle;
    public float viewHeight;

    public float meshResolution;
    public int edgeResolveInterations;

    public MeshFilter viewMeshFilter;
    MeshRenderer viewRenderer;
    Mesh viewMesh;

    public Color yellow;
    public Color red;

    void Start () {

        viewMesh = new Mesh();
        viewMesh.name = "visionConeMesh";
        viewMeshFilter.mesh = viewMesh;
        viewRenderer = viewMeshFilter.gameObject.GetComponent<MeshRenderer>();

        SetViewMeshColor(yellow);

        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    void LateUpdate() {
        DrawFieldOfView();
    }

    public struct ViewCastInfo {
        public bool hit;
        public Vector3 point;
        public float dis;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dis, float _angle) {
            hit = _hit;
            point = _point;
            dis = _dis;
            angle = _angle;
        }
    }

    public struct EdgeInfo {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
            pointA = _pointA;
            pointB = _pointB;
        }
    }

    // routine to keep looking for targets
    IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    // used to check for targets within vision radius and angle
    void FindVisibleTargets() {
        visibleTargets.Clear();

        SetViewMeshColor(yellow);

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform  target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 ) {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                // check for vision blockers between us and target 
                if ( (!Physics.Raycast(transform.position, dirToTarget, distToTarget, visionBlocker))) {
                    visibleTargets.Add(target);
                    SetViewMeshColor(red);
                }
            }
        }
    }

    // draw vision mesh
    void DrawFieldOfView() {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        //list of all hit points to create mesh
        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo previousViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++) {
            float angle = transform.eulerAngles.y - viewAngle / 2 + (stepAngleSize * i);
            ViewCastInfo newViewCast = ViewCast(angle);
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius);

            if (i > 0) {
                if (previousViewCast.hit != newViewCast.hit) {
                    EdgeInfo Edge = FindEdge(previousViewCast, newViewCast);
                    if (Edge.pointA != Vector3.zero) {
                        viewPoints.Add(Edge.pointA);
                    }
                    if (Edge.pointB != Vector3.zero) {
                        viewPoints.Add(Edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point); // - new Vector3(0,transform.position.y + viewHeight, 0));
            previousViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount - 1; i++) {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            // set verts per triangle
            if (i < vertexCount - 2) {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    // used for refining edges on cone collisions
    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveInterations; i++) {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (newViewCast.hit == minViewCast.hit) {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);

    }

    // method for return hit info for a specific line
    ViewCastInfo ViewCast(float globalAngle) {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, visionBlocker)) {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    // used to visualise cone angle
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if(!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), viewHeight, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    //set vision cone color
    void SetViewMeshColor(Color color) {
        viewRenderer.material.color = color;
        viewRenderer.material.SetColor("_EmissionColor", color);
    }
}









