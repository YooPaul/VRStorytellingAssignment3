using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class BezierCurve3PointRenderer : MonoBehaviour {

    public Transform point1;
    public Transform point2;
    public Transform point3;

    public Transform controller;

    public LineRenderer lineRenderer;
    public int vertexCount = 12;
    public Vector3 endPoint;

    public Gradient stopGradient;
    public Gradient selectGradient;
    public bool canTeleport;
    public GameObject destination;

	// Use this for initialization
	void Start () {
        canTeleport = false;
	}
	
	// Update is called once per frame
	void Update () {

        canTeleport = false;
       

        Vector3 xzPlane = new Vector3(controller.forward.x, 0, controller.forward.z);
        float cosTheta = Vector3.Dot(xzPlane, controller.forward) / (xzPlane.magnitude * controller.forward.magnitude);
        float radian = Mathf.Acos(cosTheta);
        float degree = Mathf.Rad2Deg * radian;
        //Debug.Log("Radian : " + radian + " Degree : " + Mathf.Rad2Deg * radian);

        Vector3 dN = controller.forward.normalized; // controller's forward direction normalized 
        float degreeNormalized = degree / 90f; // 0 ~ 1
        point3.position = point1.position + new Vector3(dN.x * (3 - degreeNormalized) , -2.5f, dN.z * (3 - degreeNormalized));
        //Debug.Log(point3.position);
        
        RaycastHit hit;
        /*
        if (Physics.Raycast(controller.position, controller.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider != null)
            {
                this.GetComponent<LineRenderer>().colorGradient = selectGradient;
                canTeleport = true;
            }
                
            point3.position = hit.point;
        }else
        {
            
            this.GetComponent<LineRenderer>().colorGradient = stopGradient;
            canTeleport = false;
        }*/
        //float y = Vector3.Distance(point1.position, point3.position);
        //float maxY = point3.position.y + 1f;
        //if (y > maxY) y = maxY;

        float height = degree / 70;
        if (controller.forward.y <= 0) height = 0;

        point2.position = new Vector3((point1.position.x + point3.position.x) / 2.0f, point1.position.y + height, (point1.position.z + point3.position.z) / 2.0f);


        var pointList = new List<Vector3>();

        //point2.position = controller.transform.forward;
        
        Debug.DrawRay(controller.transform.position, controller.transform.forward);
        //point3.position = new Vector3(point2.position.x*2, 0, point2.position.z*2);

        //   Original Bezier curve method
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(point1.position, point2.position, ratio);
            var tangentLineVertex2 = Vector3.Lerp(point2.position, point3.position, ratio);
            var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            if(pointList.Count > 0)
            {
                Vector3 originPoint = pointList[pointList.Count - 1];
                Vector3 direction = bezierPoint - originPoint;
                if(Physics.Raycast(originPoint, direction, out hit, direction.magnitude))
                {
                    if(hit.collider != null)
                    {
                        pointList.Add(hit.point);
                        this.GetComponent<LineRenderer>().colorGradient = selectGradient;
                        canTeleport = true;
                        break;
                    }
                }
                
            }
            pointList.Add(bezierPoint);
        }
        if(!canTeleport)
        {
            this.GetComponent<LineRenderer>().colorGradient = stopGradient;
            canTeleport = false;
        }
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
        endPoint = pointList[pointList.Count - 1];
        //destination.transform.position = endPoint;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(point1.position, point2.position);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(point2.position, point3.position);

        Gizmos.color = Color.red;

        for (float ratio = 0.5f / vertexCount; ratio < 1; ratio += 1.0f / vertexCount)
        {
            Gizmos.DrawLine(Vector3.Lerp(point1.position, point2.position, ratio),
                Vector3.Lerp(point2.position, point3.position, ratio));
        }

    }
    
}
