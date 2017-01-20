using System;
using System.Collections.Generic;
using System.Drawing;

namespace Robot_Navigation_LRTA
{
    /*
     * Enum for representating what is present on a particular point 
     */ 
    public enum Value
    {
        Empty,
        Start,
        Finish,
        Obstacle,
        Visited
    };
    class Constants
    {
        /*
         * Stores Static constants that are used in the program
         */ 
        public static Rectangle start;
        public static Rectangle finish;
        public static Boolean drawTriangle;
        public static Boolean drawSquare;
        public static List<int[]> Corners;
        public static Dictionary<int[], int> Hvalues;
        public static string fileData;
        public Constants()  //initializes all the static constants
        {
            fileData = "";
            Hvalues = new Dictionary<int[], int>();
            Corners = new List<int[]>();
            drawTriangle = false;
            drawSquare = false;
            start = new Rectangle(200,400,10,10);
            finish = new Rectangle(800, 400, 10, 10);
        }
        public static void newHValues()
        {
            //reinitializes Dictionary
            Hvalues = new Dictionary<int[], int>();
        }
        
    }
}
