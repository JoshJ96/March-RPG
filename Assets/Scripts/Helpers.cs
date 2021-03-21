using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/*----------------------------------------------------------
    This is a static class handling common operations
                    in the game engine
------------------------------------------------------------*/

public static class Helpers
{
    /*----------------------------------------------------------
                       GetNavPathDistance
    Takes two navmesh points and gets the distance of the path
    ------------------------------------------------------------*/
    public static float GetNavPathDistance(Vector3 source, Vector3 destination)
    {
        //New path
        NavMeshPath path = new NavMeshPath();

        //Calculate the path between the source and dest and appends data to path variable
        NavMesh.CalculatePath(source, destination, NavMesh.AllAreas, path);

        //Paths are made up of a list of Vector3, or "corners." this piece adds up all the distances of these points
        float length = 0;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.green);
            length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        //Return the length
        return length;
    }

    /*-------------------------------------------------------------------
                            GetClosestPoint
    Takes list of a source and navmesh points and returns the closest one
    ---------------------------------------------------------------------*/
    public static Vector3 GetClosestPoint(Vector3 source, List<Vector3> pointList)
    {
        //Keeps track of the point and the distance from the source
        Dictionary<Vector3, float> distances = new Dictionary<Vector3, float>();

        foreach (Vector3 point in pointList)
        {
            float distance = GetNavPathDistance(source, point);

            //Get rid of values that are 0
            //Todo: optimize interactable's 8 points to check if on navmesh
            if (distance == 0)
            {
                continue;
            }

            distances.Add(point, distance);
        }

        if (distances.Count == 0)
        {
            return source;
        }

        //Find the shortest distance
        Vector3 closestPoint = distances.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;

        return closestPoint;
    }
}
