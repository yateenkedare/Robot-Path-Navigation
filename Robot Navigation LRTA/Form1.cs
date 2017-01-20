using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Priority_Queue;
using System.IO;

namespace Robot_Navigation_LRTA
{
    public partial class Form1 : Form
    {
        static ArrayGraphics world; //represents the entire state space
        Value hover = Value.Empty;
        Rectangle temp;
        Queue<int[]> solutionPath = new Queue<int[]>(); //stores the path from start to goal
        public Form1()
        {
            //Initialization of all the variables
            new Constants();
            InitializeComponent();
            Constants.drawTriangle = false;
            Constants.drawSquare = false;
            timer.Tick += UpdateTemp;
        }
        
        private void pbCamvas_Paint(object sender, PaintEventArgs e)
        {
            /*
             Function updates the GUI and prints all the obstacles including
             the start state, goal state and the path from start to goal state
             */
            Graphics canvas = e.Graphics;
            for(int i = 0; i < pbCamvas.Size.Width; i++)
                for(int j = 0; j < pbCamvas.Size.Height; j++)
                    if(world.array[i,j] == Value.Obstacle )
                        canvas.FillRectangle(Brushes.Gray, i, j, 1, 1);
                    else if(world.array[i, j] == Value.Start)
                        canvas.FillRectangle(Brushes.Red, i, j, 1, 1);
                    else if (world.array[i, j] == Value.Finish)
                        canvas.FillRectangle(Brushes.Green, i, j, 1, 1);
            if (solutionPath.Count != 0)
            {
                while (solutionPath.Count > 1)
                {
                    //printing the path
                    Point A = new Point(solutionPath.Peek()[0], solutionPath.Peek()[1]);
                    solutionPath.Dequeue();
                    canvas.DrawLine(new Pen(Color.Blue, 1), A, new Point(solutionPath.Peek()[0], solutionPath.Peek()[1]));
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            //called when the form is resized so that the variables don't run out of bounds
            world = new ArrayGraphics(pbCamvas.Size.Width, pbCamvas.Size.Height);
            world.FillRectangle(Constants.start.X, Constants.start.Y, Constants.start.Width, Value.Start);
            world.FillRectangle(Constants.finish.X, Constants.finish.Y, Constants.finish.Width, Value.Finish);
        }

        private bool IsInside(int x, int y, Rectangle box)
        {
            //checks if the point is inside the box 
            if (box.X <= x && box.X + box.Width >= x)
                return true;
            else return false;
        }

        private void tbSize_TextChanged(object sender, EventArgs e)
        {
            //sets the size with which  you want to draw the state space
            if (System.Text.RegularExpressions.Regex.IsMatch(tbSize.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter numbers only.","Size Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                tbSize.Text = tbSize.Text.Remove(tbSize.Text.Length - 1);
            }
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            //invoked on button click
            Constants.drawTriangle = true;
            Constants.drawSquare = false;
        }
        private void btnSquare_Click(object sender, EventArgs e)
        {
            //invoked on button click
            Constants.drawTriangle = false;
            Constants.drawSquare = true;
        }
        private void pbCamvas_MouseUp(object sender, MouseEventArgs e)
        {
            //invoked when mouse button is up after it was pressed
            hover = Value.Empty;
            timer.Stop();
            pbCamvas.Invalidate();
        }

        private void pbCamvas_MouseDown(object sender, MouseEventArgs e)
        {
            //invoked when mouse button is pressed
            //this function draws obstacles on the GUI on mouse press
            if (IsInside(e.X, e.Y, Constants.start))
            {
                timer.Start();
                hover = Value.Start;
            }
            else if (IsInside(e.X, e.Y, Constants.finish))
            {
                timer.Start();
                hover = Value.Finish;
            }
            else if (hover == Value.Empty)
            {
                if (Constants.drawTriangle)
                    world.FillTriangle(e.X, e.Y, Convert.ToInt32(tbSize.Text), Value.Obstacle);
                else if (Constants.drawSquare)
                    world.FillRectangle(e.X, e.Y, Convert.ToInt32(tbSize.Text), Value.Obstacle);
            }
        }

        private void UpdateTemp(object sender, EventArgs e)
        {
            //Used to give a moving GUI for start and end position
            temp.X = pbCamvas.PointToClient(MousePosition).X;
            temp.Y = pbCamvas.PointToClient(MousePosition).Y;
            temp.Size = Constants.start.Size;
            switch (hover)
            {
                case Value.Start:
                    {
                        world.FillRectangle(Constants.start.X, Constants.start.Y, Constants.start.Width, Value.Empty);
                        world.FillRectangle(temp.X, temp.Y, temp.Width, Value.Start);
                        Constants.start = temp;
                    }
                    break;
                case Value.Finish:
                    {
                        world.FillRectangle(Constants.finish.X, Constants.finish.Y, Constants.finish.Width, Value.Empty);
                        world.FillRectangle(temp.X, temp.Y, temp.Width, Value.Finish);
                        Constants.finish = temp;

                    }
                    break;
            }
            pbCamvas.Invalidate();
        }

        private void LRTAFindPath()
        {
            //defining the variables 
            Constants.newHValues();
            solutionPath = new Queue<int[]>();
            solutionPath.Enqueue(new int[] { Constants.start.X, Constants.start.Y });   //adding start to solution path
            Constants.Corners.Add(new int[] { Constants.finish.X, Constants.finish.Y });    //adding finish state as one of the corners
            LRTA lrta = new LRTA();
            int HVal = 0;
            int[] currentPos = new int[] { Constants.start.X, Constants.start.Y };
            bool solutionFound = false;

            //navigating to first corner
            List<int[]> percept = lrta.getPercept(world.array, currentPos[0], currentPos[1]);
            SimplePriorityQueue<int[]> priorityQueue = new SimplePriorityQueue<int[]>();
            //finding the Heuristic value for all the corners
            foreach (int[] x in percept)
            {
                HVal = lrta.calculateHeuristicValue(x[0], x[1]);
                try
                {
                    Constants.Hvalues.Add(x, HVal);
                }
                catch { }
                //Goal Test
                if (HVal == 0)
                {
                    solutionPath.Enqueue(x);
                    solutionFound = true;
                    break;
                }
                priorityQueue.Enqueue(x, Constants.Hvalues[x]);
            }
            int[] lastPos = new int[] { Constants.start.X, Constants.start.Y };
            Constants.Hvalues.Add(lastPos, lrta.calculateHeuristicValue(lastPos[0], lastPos[1]));
            //Appending the best node to the priority Queue
            if (!solutionFound)
            {
                currentPos = priorityQueue.Dequeue();
                solutionPath.Enqueue(currentPos);
            }
            //now we have last and current so find next and check if it is in a local minima
            while (!solutionFound)
            {
                //Getting the percept for current pos
                percept = lrta.getPercept(world.array, currentPos[0], currentPos[1]);
                priorityQueue = new SimplePriorityQueue<int[]>();
                foreach (int[] x in percept)
                {
                    //finding Heuristic values for the current percept
                    if (x != currentPos)
                    {
                        HVal = lrta.calculateHeuristicValue(x[0], x[1]);
                        try
                        {
                            Constants.Hvalues.Add(x, HVal);
                        }
                        catch { }
                        //Goal Test
                        if (HVal == 0)
                        {
                            solutionPath.Enqueue(x);
                            solutionFound = true;
                            break;
                        }
                        priorityQueue.Enqueue(x, Constants.Hvalues[x]);
                    }
                }
                if (!solutionFound)
                {
                    int[] nextPos = priorityQueue.Dequeue();
                    if (Constants.Hvalues[currentPos] <= Constants.Hvalues[nextPos] && Constants.Hvalues[currentPos] <= Constants.Hvalues[lastPos])
                    {
                        //stuck in local minima hence update the herustic value of that position
                        Constants.Hvalues[currentPos] = (Constants.Hvalues[nextPos] < Constants.Hvalues[lastPos] ?
                            Constants.Hvalues[nextPos] : Constants.Hvalues[lastPos]) + 1;
                        int[] tempPos = lastPos;
                        lastPos = currentPos;
                        //moving it to the next best value
                        currentPos = Constants.Hvalues[nextPos] < Constants.Hvalues[lastPos] ? nextPos : tempPos;   
                    }
                    else
                    {
                        //best position found which is not a local minima 
                        lastPos = currentPos;
                        currentPos = nextPos;
                    }
                    //adding the path to the solution
                    solutionPath.Enqueue(nextPos);
                }

            }
        }

        private void bt_q1_Click(object sender, EventArgs e)
        {
            //invoked for Q1
            LRTAFindPath();
            lbl_perf.Text = "Performance : " + (1001 - solutionPath.Count);
            pbCamvas.Invalidate();  //Update the GUI

        }

        private void bt_q2_Click(object sender, EventArgs e)
        {
            //Invoked for Q2
            int netPerformance = 0;
            //printing the data to a file as required by the question
            using (var w = new StreamWriter("Myfile.csv"))
            {
                w.WriteLine("start, goal,performance, path");
                for (int episodes = 0; episodes < 100; episodes++)
                {
                    spwanStartAtNewPosition();
                    LRTAFindPath(); //finding the solution
                    netPerformance += (1001 - solutionPath.Count);  

                    string s = Constants.start.X + ":" + Constants.start.Y+"," +Constants.finish.X + ":" + Constants.finish.Y + ",";
                    s+=1001-solutionPath.Count+",";
                    while (solutionPath.Count != 0)
                    {
                        int[] a = solutionPath.Dequeue();
                        s += "=>"+a[0]+":"+a[1];    //writing stuff to file
                    }
                    
                    w.WriteLine(s);
                    w.Flush();
                }
                w.Close();
            }
            lbl_perf.Text = "NP:" + netPerformance / 100;
            pbCamvas.Invalidate();      //updating the GUI
        }

        public void spwanStartAtNewPosition()
        {
            //This function is used to spwan the start state at a new function
            Random random = new Random();
            bool success = false;
            int x = 100, y = 100;
            //checks if the new position is not inside an obstacle and is empty
            while (!success)
            {
                x = random.Next(world.array.GetLength(0) - 25);
                y = random.Next(world.array.GetLength(1) - 25);
                if(!(x <169) && !(y <226))
                    success = checkOccupancy(x, y);
            }
            world.FillRectangle(Constants.start.X, Constants.start.Y, Constants.start.Height, Value.Empty);

            Constants.start.X = x;
            Constants.start.Y = y;
            world.FillRectangle(Constants.start.X, Constants.start.Y, Constants.start.Height, Value.Start);
            pbCamvas.Invalidate();  //updates the GUI
        }

        private bool checkOccupancy(int x, int y)
        {//Checks if the current position is already occupied by some other object or goal state
            for (int i = x - Constants.start.Height; i < x + Constants.start.Height; i++)
            {
                for (int j = y - Constants.start.Height; j < y + Constants.start.Height; j++)
                {
                    if (world.array[i, j] == Value.Obstacle || world.array[i, j] == Value.Finish)
                        return false;
                }
            }
            return true;
        }

        private void bt_q3_Click(object sender, EventArgs e)
        {
            //Invoked for Q3
            Constants.newHValues();
            solutionPath = new Queue<int[]>();
            solutionPath.Enqueue(new int[] { Constants.start.X, Constants.start.Y });   //adding the start state to the solutionPath
            Constants.Corners.Add(new int[] { Constants.finish.X, Constants.finish.Y });
            LRTA lrta = new LRTA();
            int HVal = 0;
            int[] currentPos = new int[] { Constants.start.X, Constants.start.Y };  //setting the start state as the current positiion
            bool solutionFound = false;

            //navigating to first corner
            List<int[]> percept = lrta.getPercept(world.array, currentPos[0], currentPos[1]);   //getting the percept
            SimplePriorityQueue<int[]> priorityQueue = new SimplePriorityQueue<int[]>();
            //getting the heuristic values for the percept
            foreach (int[] x in percept)
            {
                HVal = lrta.calculateHeuristicValue(x[0], x[1]);
                try
                {
                    Constants.Hvalues.Add(x, HVal);
                }
                catch { }
                //Goal Test
                if (HVal == 0)
                {
                    solutionPath.Enqueue(x);
                    solutionFound = true;
                    break;
                }
                priorityQueue.Enqueue(x, Constants.Hvalues[x]);
            }
            int[] lastPos = new int[] { Constants.start.X, Constants.start.Y };
            Constants.Hvalues.Add(lastPos, lrta.calculateHeuristicValue(lastPos[0], lastPos[1]));

            if (!solutionFound)
            {
                currentPos = priorityQueue.Dequeue();
                solutionPath.Enqueue(currentPos);
            }
            //now we have last and current so find next and check if it is in a local minima
            while (!solutionFound)
            {
                //finding the percept for the current state
                percept = lrta.getPercept(world.array, currentPos[0], currentPos[1]);
                priorityQueue = new SimplePriorityQueue<int[]>();
                //finding all the heuristic values
                foreach (int[] x in percept)
                {
                    if (x != currentPos)
                    {
                        HVal = lrta.calculateHeuristicValue(x[0], x[1]);
                        try
                        {
                            Constants.Hvalues.Add(x, HVal);
                        }
                        catch { }
                        //Goal Test
                        if (HVal == 0)
                        {
                            solutionPath.Enqueue(x);
                            solutionFound = true;
                            break;
                        }
                        priorityQueue.Enqueue(x, Constants.Hvalues[x]);
                    }
                }
                //Implementing the 30-70% probability
                if (!solutionFound)
                {
                    Random rand = new Random();
                    int probability = rand.Next(10);    //random number for probability
                    if (probability > 2)
                    {
                        //Take the correct path and implement it as in Q1
                        int[] nextPos = priorityQueue.Dequeue();
                        if (Constants.Hvalues[currentPos] <= Constants.Hvalues[nextPos] && Constants.Hvalues[currentPos] <= Constants.Hvalues[lastPos])
                        {
                            //stuck in local minima
                            Constants.Hvalues[currentPos] = (Constants.Hvalues[nextPos] < Constants.Hvalues[lastPos] ? Constants.Hvalues[nextPos] : Constants.Hvalues[lastPos]) + 1;
                            int[] tempPos = lastPos;
                            lastPos = currentPos;
                            currentPos = Constants.Hvalues[nextPos] < Constants.Hvalues[lastPos] ? nextPos : tempPos;   //moving it to the next best value
                        }
                        else
                        {
                            lastPos = currentPos;
                            currentPos = nextPos;
                        }
                        solutionPath.Enqueue(nextPos);
                    }
                    else
                    {
                        //Robot went to a wrong position
                        //Implementing a Stack to backtrack to the original position
                        Stack<int[]> returnToOriginalPlan = new Stack<int[]>();
                        int[] actualNextPos = currentPos;
                        int[] randomNextPos = new int[] { 0, 0 };
                        returnToOriginalPlan.Push(currentPos);  //Pushing the current position to the stack
                        do
                        {
                            int times = rand.Next(priorityQueue.Count);
                            for (int i = 0; i < times; i++)     //selecting a random position to go
                            {
                                randomNextPos = priorityQueue.Dequeue();
                            }
                            
                            percept = lrta.getPercept(world.array, randomNextPos[0], randomNextPos[1]); //getting percept of that position to try to get back
                            priorityQueue.Clear();
                            //getting heuristic values 
                            foreach (int[] x in percept)
                            {
                                if (x != randomNextPos)
                                {
                                    int Hval = calcHeurToGetBackToOriginalPlan(x, actualNextPos);
                                    priorityQueue.Enqueue(x, Hval);

                                }
                            }

                            probability = rand.Next(10);    //again checking the probability for going back
                            if (probability > 2)
                            {
                                //correct path pop from stack and update the percept
                                int[] a = returnToOriginalPlan.Pop();
                                solutionPath.Enqueue(a);
                                //priority queue update
                                percept = lrta.getPercept(world.array, a[0], a[1]); //getting percept of that position to try to get back
                                priorityQueue.Clear();
                                foreach (int[] x in percept)
                                {
                                    if (x != a)
                                    {
                                        int Hval = calcHeurToGetBackToOriginalPlan(x, actualNextPos);
                                        priorityQueue.Enqueue(x, Hval);

                                    }
                                }
                            }
                            else
                            {
                                //wrong path go to the new wrong positino and pust the current position onto the stack
                                solutionPath.Enqueue(randomNextPos);        //going to that position
                                returnToOriginalPlan.Push(randomNextPos);
                                actualNextPos = randomNextPos;
                            }
                        } while (returnToOriginalPlan.Count != 0);
                    }
                }

            }
            lbl_perf.Text = "Performance : " + (1001 - solutionPath.Count);
            pbCamvas.Invalidate();

        }

        private int calcHeurToGetBackToOriginalPlan(int[] a, int[] b)
        {
            //calculates the heuristics in order to implement Q3 - Eucleadian Distance
            return (int)Math.Round(Math.Sqrt((b[0] - a[0]) * (b[0] - a[0]) + (b[1] - a[1]) * (b[1] - a[1])));
        }

        private void bt_q4_2_Click(object sender, EventArgs e)
        {
            //Implements Q4
            Constants.newHValues();
            solutionPath = new Queue<int[]>();
            solutionPath.Enqueue(new int[] { Constants.start.X, Constants.start.Y });
            Constants.Corners.Add(new int[] { Constants.finish.X, Constants.finish.Y });
            LRTA lrta = new LRTA();
            int HVal = 0;
            int[] currentPos = new int[] { Constants.start.X, Constants.start.Y };
            bool solutionFound = false;

            //navigating to first corner
            List<int[]> percept = lrta.getPercept(world.array, currentPos[0], currentPos[1]);
            //getting the percept and calculating the heuristic values for all tha points in the percept
            SimplePriorityQueue<int[]> priorityQueue = new SimplePriorityQueue<int[]>();
            foreach (int[] x in percept)
            {
                HVal = lrta.calculateHeuristicValue(x[0], x[1]);
                try
                {
                    Constants.Hvalues.Add(x, HVal);
                }
                catch { }
                //goal test
                if (HVal == 0)
                {
                    solutionPath.Enqueue(x);
                    solutionFound = true;
                    break;
                }
                priorityQueue.Enqueue(x, Constants.Hvalues[x]); //pushing the heuristic values in the queue
            }
            int[] lastPos = new int[] { Constants.start.X, Constants.start.Y };
            Constants.Hvalues.Add(lastPos, lrta.calculateHeuristicValue(lastPos[0], lastPos[1]));

            if (!solutionFound)
            {
                //adding the current position in the output
                currentPos = priorityQueue.Dequeue();
                solutionPath.Enqueue(currentPos);
            }
            //now we have last and current so find next and check if it is in a ditch
            while (!solutionFound)
            {
                //getting the percept
                percept = lrta.getPercept(world.array, currentPos[0], currentPos[1]);
                priorityQueue.Clear();
                //calculating heuristic values
                foreach (int[] x in percept)
                {
                    if (x != currentPos)
                    {
                        HVal = lrta.calculateHeuristicValue(x[0], x[1]);
                        try
                        {
                            Constants.Hvalues.Add(x, HVal);
                        }
                        catch { }
                        //goal test
                        if (HVal == 0)
                        {
                            solutionPath.Enqueue(x);
                            solutionFound = true;
                            break;
                        }
                        priorityQueue.Enqueue(x, Constants.Hvalues[x]);
                    }
                }
                if (!solutionFound)
                {
                    //implementing the probability 30-70%
                    Random rand = new Random();
                    int probability = rand.Next(10);
                    if (probability > 2)
                    {
                        //Take the correct path as in Q1
                        int[] nextPos = priorityQueue.Dequeue();
                        if (Constants.Hvalues[currentPos] <= Constants.Hvalues[nextPos] && Constants.Hvalues[currentPos] <= Constants.Hvalues[lastPos])
                        {
                            //stuck in local minima
                            Constants.Hvalues[currentPos] = (Constants.Hvalues[nextPos] < Constants.Hvalues[lastPos] ? Constants.Hvalues[nextPos] : Constants.Hvalues[lastPos]) + 1;
                            int[] tempPos = lastPos;
                            lastPos = currentPos;
                            currentPos = Constants.Hvalues[nextPos] < Constants.Hvalues[lastPos] ? nextPos : tempPos;   //moving it to the next best value
                        }
                        else
                        {
                            //not in local minima
                            lastPos = currentPos;
                            currentPos = nextPos;
                        }
                        solutionPath.Enqueue(nextPos);
                    }
                    else
                    {
                        //wrong path- find a new path to the goal from this new position
                        lastPos = currentPos;
                        int i = rand.Next(priorityQueue.Count);
                        for (int k = 0; k < i; k++) priorityQueue.Dequeue();
                        currentPos = priorityQueue.Dequeue();
                        solutionPath.Enqueue(currentPos);
                    }
                }

            }
            lbl_perf.Text = "Performance : " + (1001 - solutionPath.Count);
            pbCamvas.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             Function implements the save functinoality
             Saves all the obstacles and the goal and start state to a fine
             */
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            saveFileDialog1.Filter = filter;
            StreamWriter writer = null;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filter = saveFileDialog1.FileName;
                writer = new StreamWriter(filter);

                writer.WriteLine(Constants.start.X+","+ Constants.start.Y + ","+Constants.start.Height);
                writer.WriteLine(Constants.finish.X + "," + Constants.finish.Y + "," + Constants.finish.Height);
                writer.WriteLine(Constants.fileData);
                writer.Flush();
                writer.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            /*
             * Function Loads data form a file and Updates the GUI
             * Called when the load button is pressed
             */
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "~";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        world = new ArrayGraphics(pbCamvas.Size.Width, pbCamvas.Size.Height);
                        string[] line = sr.ReadLine().Split(',');
                        world.FillRectangle(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]), Convert.ToInt32(line[2]), Value.Start);
                        Constants.start.X = Convert.ToInt32(line[0]);
                        Constants.start.Y = Convert.ToInt32(line[1]);
                        Constants.start.Width = Convert.ToInt32(line[2]);
                        Constants.start.Height = Convert.ToInt32(line[2]);
                        line = sr.ReadLine().Split(',');
                        world.FillRectangle(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]), Convert.ToInt32(line[2]), Value.Finish);
                        Constants.finish.X = Convert.ToInt32(line[0]);
                        Constants.finish.Y = Convert.ToInt32(line[1]);
                        Constants.finish.Width = Convert.ToInt32(line[2]);
                        Constants.finish.Height = Convert.ToInt32(line[2]);
                        while (!sr.EndOfStream)
                        {
                            string[] ln = sr.ReadLine().Split(',');
                            if (Convert.ToInt32(ln[3]) == 0)
                            {
                                world.FillRectangle(Convert.ToInt32(ln[0]), Convert.ToInt32(ln[1]), Convert.ToInt32(ln[2]), Value.Obstacle);
                                //adding the obstacles to state space
                                
                            }
                            else
                            {
                                world.FillTriangle(Convert.ToInt32(ln[0]), Convert.ToInt32(ln[1]), Convert.ToInt32(ln[2]), Value.Obstacle);
                                //adding the obstacles to state space
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File Loaded ");
                }
                pbCamvas.Invalidate();
            }
        }
    }
}
