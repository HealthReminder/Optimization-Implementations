using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The catenary class returns a list of points representing a catenary curve
/// The curve can then be used to draw a rope using the points
/// Could not make use of this in a practical way but leaving here for future reference
/// </summary>
public static class Catenary
{
        public static List<Vector3> CalculateCatenary(int numSegments, float length, float weight, Vector3 start, Vector3 end)
        {
            List<Vector3> points = new List<Vector3>();

            // Calculate the distance between the start and end points
            float distTotal = Vector3.Distance(start, end);

            // Calculate the distance between each segment
            float segmentLength = distTotal / numSegments;

            // Calculate the sag constant (T)
            // The sag constant is used to calculate the vertical
            // Position of each rope segment
            float sagConstant = weight * segmentLength * segmentLength;

            // Calculate the horizontal distance between each segment
            float distX = (end.x - start.x) / numSegments;

            // Calculate the vertical distance between each segment
            // Tecnically a catenary would use the hyperbolic cosine function (cosh(x))
            // But using Log simplifies the operation
            float distY = sagConstant * Mathf.Log((distTotal + sagConstant) / sagConstant);

            // Add the start point to the list of points
            points.Add(start);

            // Loop through each segment and add the next point to the list of points
            for (int i = 1; i <= numSegments; i++)
            {
                float x = start.x + i * distX;
                float y = start.y + i * distY / distTotal * length;
                float z = start.z + i * (end.z - start.z) / numSegments;

                points.Add(new Vector3(x, y, z));
            }

            // Add the end point to the list of points
            points.Add(end);

            return points;
        }
    }