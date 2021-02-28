using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace City.Trafic.Road
{
    public class FolowRoad : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField] float speedModifier = 1;
        [SerializeField] float RotationSpeed = 1;

        [SerializeField] float carCenterPos = 0.2f;
        [SerializeField] float nextPosMove = 0.001f;
        [SerializeField] GameObject Map;

        Bezier_Curve[] bezier_Curves;

        //public FolowRoad FR;

        public int nodeIndex = 0;
        public int currentRoad = 0; //use this for the current road amount


        private Vector3 carTargetPosition = Vector3.zero;

        [SerializeField] bool MovingForward = false;

        float distanceTravelled = 0;
        Vector3 lastPosition;

        void Start()
        {
            bezier_Curves = Map.GetComponentsInChildren<Bezier_Curve>();
            StartCoroutine(GoByTheRoute());

            lastPosition = transform.position;
        }
        void Update()
        {
            distanceTravelled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            //Debug.Log(lastPosition);
        }
        private IEnumerator GoByTheRoute()
        {
            //Vector3 bezier_curve = bezier_Curve[curentPath].positions[positionIndex];
            //var bezier_curve_Length = bezier_Curve[curentPath].positions.Length;

            while (true)
            {
                if (MovingForward)
                {

                    carTargetPosition = bezier_Curves[currentRoad].controlPoints[nodeIndex] + new Vector3(carCenterPos, 0, 0);
                    //Debug.Log(curentPath);
                }
                else
                {
                    carTargetPosition = bezier_Curves[currentRoad].controlPoints[nodeIndex] + new Vector3(-carCenterPos, 0, 0);
                }

                Vector3 DistanceToTarget = carTargetPosition - transform.position;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(DistanceToTarget), Time.time * RotationSpeed);

                //s = d/t
                transform.position = Vector3.MoveTowards(transform.position, carTargetPosition, speedModifier / 500);

                float Distance = Vector3.Distance(transform.position, carTargetPosition);

                if (Distance < nextPosMove)//Checks the distance from the car to the target if not 0 don't move to the next one
                {
                    if (MovingForward)
                    {
                        nodeIndex++;
                    }
                    else
                    {
                        nodeIndex--;
                    }
                    if (nodeIndex >= bezier_Curves[currentRoad].controlPoints.Length || nodeIndex < 0)
                    {
                        //Debug.Log("Node Index: " + nodeIndex);
                        //Debug.Log("2: Bezier Curve node amount: " + bezier_Curve[roadAmount].controlPoints.Length);
                        if (MovingForward)
                        {
                            
                            currentRoad++;
                            Debug.Log("Road Amount: " + currentRoad);
                            //Debug.Log("3: Bezier Curve node amount: " + bezier_Curve[roadAmount].controlPoints.Length);
                            if (currentRoad >= bezier_Curves.Length) //&& positionIndex >= 1) //here
                            {
                                Debug.Log("Roadamount equals 0");
                                currentRoad = 0;
                            }
                            nodeIndex = 0;

                        }
                        else
                        {
                            currentRoad--;
                            if (currentRoad < 0)
                            {
                                currentRoad = bezier_Curves.Length - 1;
                            }
                            nodeIndex = bezier_Curves[currentRoad].controlPoints.Length - 1;
                        }
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}