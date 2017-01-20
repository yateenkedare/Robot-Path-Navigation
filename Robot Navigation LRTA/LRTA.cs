using System;
using System.Collections.Generic;

namespace Robot_Navigation_LRTA
{
    class LRTA
    {
        public List<int[]> getPercept(Value[,] world, int CurrentX, int CurrentY)
        {
            /*
             Gets the percept of the current state 
             */
            List<int[]> percept = new List<int[]>();
            for (int i = 0; i < Constants.Corners.Count; i++)
            {
                int[] corner = Constants.Corners[i];
                if (ifVisible(world, CurrentX, CurrentY, corner))//chcks if a corner is visible or not
                {
                    percept.Add(corner);
                }
            }
            return percept;
        }

        private bool ifVisible(Value[,] world, int CurrentX, int CurrentY, int[] corner)
        {
            /*
             Checks if the corner is visible or not by checking the if the line intersects between two points
             */
            float dx = corner[0] - CurrentX;
            float dy = corner[1] - CurrentY;
            float slope;
            if (0 == dx)
            {
                //line such as x = 1
                int yMax = CurrentY < corner[1] ? corner[1] : CurrentY;
                for (int y = CurrentY < corner[1] ? CurrentY : corner[1]; y < yMax; y++)
                {
                    if (world[CurrentX, y] == Value.Obstacle)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (0 == dy)
            {
                //line such as y == 1
                int xMax = CurrentX < corner[0] ? corner[0] : CurrentX;
                for (int x = CurrentX < corner[0] ? CurrentX : corner[0]; x < xMax; x++)
                {
                    if (world[x, CurrentY] == Value.Obstacle)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                //line with random slope
                slope = dy / dx;
                int xMax = CurrentX < corner[0] ? corner[0] : CurrentX;
                for (int x = CurrentX < corner[0] ? CurrentX : corner[0]; x < xMax; x++)
                {
                    int y = (int)Math.Round(corner[1] - slope * (corner[0] - x));
                    if (world[x, y] == Value.Obstacle)
                    {
                        return false;
                    }
                }
                return true;
            }



            
        }

        public int calculateHeuristicValue(int x, int y)
        {
            //calcilates Heuristic value based on Eculedian Distance
            return (int)Math.Round(Math.Sqrt((Constants.finish.X - x) * (Constants.finish.X - x) + (Constants.finish.Y - y) * (Constants.finish.Y - y)));
        }
        
    }
}
