using System;
using System.Drawing;

namespace Robot_Navigation_LRTA
{
    /*
     * Class to draw Graphics on a 2d array
     */ 
    class ArrayGraphics
    {
        public Value[,] array;
        public int maxWidth { get; private set; }
        public int maxHeight { get; private set; }
        private static Color blackColor = Color.Black;

        /*
         * Initializes the array and also puts obstacles on  the boundary of the state space
         */ 
        public ArrayGraphics(int width, int height)
        {
            array = new Value[width, height];
            for (int i = 0; i < array.GetLength(0); i++)        //travesing width
            {
                array[i, 0] = Value.Obstacle;
                array[i, array.GetLength(1)-1] = Value.Obstacle;
            }
            for (int i = 0; i < array.GetLength(1); i++)    //traversing height
            {
                array[0, i] = Value.Obstacle;
                array[array.GetLength(0) - 1, i] = Value.Obstacle;
            }
            maxWidth = width;
            maxHeight = height;
        }

        /*
         * Function used to flood fill rectangles in a 2d array at a given 
         * Center point with a given size
         */ 
        public void FillRectangle(int centerX, int centerY, int size, Value a)
        {
            int UBX = ((centerX + size) < array.GetLength(0)) ? (centerX + size) : array.GetLength(0) - 1,
                LBX = ((centerX - size) > 0) ? (centerX - size) : 0,
                UBY = ((centerY + size) < array.GetLength(1)) ? (centerY + size) : array.GetLength(1) - 1,
                LBY = ((centerY - size) > 0) ? (centerY - size) : 0;
            for (int i = LBX+1; i < UBX ; i++)
            {
                for (int j = LBY+1; j < UBY ; j++)
                {
                    if(array[i,j] == Value.Empty || a == Value.Empty)
                        array[i, j] = a; 
                }
            }
            if (a == Value.Obstacle)
            {
                Constants.Corners.Add(new int[] { LBX, LBY });     //Adding the corners to a list
                Constants.Corners.Add(new int[] { UBX, LBY });
                Constants.Corners.Add(new int[] { LBX, UBY });
                Constants.Corners.Add(new int[] { UBX, UBY });
                Constants.fileData += centerX + "," + centerY + "," + size + ",0,\n";
            }
        }

        /*
         * Function used to flood fill Triangle in a 2d array at a given 
         * Center point with a given size
         */
        public void FillTriangle(int centerX, int centerY, int size, Value a)
        {
            /*
                C
              /   \
            A  ---  B
            */
            Point C = new Point(centerX, centerY - size);
            double sixtyDegrees = (Math.PI / 3);
            int xDiff = (int)Math.Round(size * Math.Sin(sixtyDegrees));
            int yDiff = (int)Math.Round(size * Math.Cos(sixtyDegrees));
            Point B = new Point(centerX + xDiff, centerY + yDiff);
            Point A = new Point(centerX - xDiff, centerY + yDiff);

            int LBY = C.Y > 0 ? C.Y : 0,
                UBY = A.Y < array.GetLength(1) ? A.Y : array.GetLength(1) - 1,
                UBX = (B.X < array.GetLength(0)) ? B.X : array.GetLength(0) - 1,
                LBX = (A.X > 0) ? A.X : 0;


            for (int i = LBX + 1; i < UBX; i++)
            {
                for (int j = LBY + 1; j < UBY; j++)
                {
                    if (array[i, j] == Value.Empty || a == Value.Empty)
                    {
                        Point curr = new Point(i, j);
                        if (curr.Y < A.Y && isLeft(C,B,curr) && isLeft(A, C, curr))
                        {
                            array[i, j] = a;
                        }
                    }
                }
            }

            if(A.X >= LBX && A.Y <= UBY)
                Constants.Corners.Add(new int[] { A.X, A.Y});     //Adding all Triangle Corners to a list
            if (B.Y <= UBY && B.X <= UBX)
                Constants.Corners.Add(new int[] { B.X, B.Y });     //Adding all Triangle Corners to a list
            if(C.Y >= LBY)
                Constants.Corners.Add(new int[] { C.X, C.Y });     //Adding all Triangle Corners to a list
            Constants.fileData += centerX + "," + centerY + "," + size + ",1\n";
        }

        //checks if the point C is on the left of vector AB
        private bool isLeft(Point A, Point B, Point C)
        {
            return ((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)) > 0;
        }
    }
}
