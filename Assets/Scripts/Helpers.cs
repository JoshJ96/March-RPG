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
}
