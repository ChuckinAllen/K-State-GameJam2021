using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace City.Trafic.Road
{
    public class Bezier_Curve : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] Transform point0, point1, point2, point3;

        //[SerializeField] float DrawSpeed = 1f;

        [SerializeField] int numPoints = 10;
        public Vector3[] controlPoints = new Vector3[10];
        public List<Vector3> control_Points = new List<Vector3>();

        void Start()
        {
            lineRenderer.positionCount = numPoints;

        }
        private void Update()
        {
            //LinearCurve();
            //QuadraticCurve();
            CubicCurve();
        }

        private void LinearCurve()
        {
            for (int i = 1; i < numPoints + 1; i++)
            {
                float t = i / (float)numPoints;

                //Positions is the posiiton of road
                controlPoints[i - 1] = CalculateLinearBezierPoint(t, point0.position, point1.position);
            }
            //lineRenderer.SetPositions(positions);
        }
        private void QuadraticCurve()
        {
            for (int i = 1; i < numPoints + 1; i++)
            {
                float t = i / (float)numPoints;

                //Positions is the posiiton of road
                controlPoints[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
            }
            //lineRenderer.SetPositions(positions);
        }

        private void CubicCurve()
        {
            for (int i = 1; i < numPoints + 1; i++)
            {
                float t = i / (float)numPoints;

                //Positions is the posiiton of road
                controlPoints[i - 1] = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);

                control_Points.Add(CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position));
                //Debug.Log(controlPoints.Length);
            }
            //lineRenderer.SetPositions(positions);
        }

        private Vector3 CalculateLinearBezierPoint(float t, Vector3 P1, Vector3 P0)
        {
            return P0 + t * (P1 - P0);
        }

        private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 P1, Vector3 P0, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * P0;
            p += 2 * u * t * P1;
            p += tt * p2;
            return p;
        }

        private Vector3 CalculateCubicBezierPoint(float t, Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;
            Vector3 p = uuu * P0;
            p += 3 * uu * t * P1;
            p += 3 * u * tt * P2;
            p += ttt * P3;
            return p;
        }
    }

}
