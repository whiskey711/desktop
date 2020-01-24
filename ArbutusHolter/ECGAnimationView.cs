using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Uvic_Ecg_EcgAnimationView
{
    /// <summary>
    /// this class have 4 main method, pictureBox_paint, updateValues, AddPoints, and timer_Tick.
    /// They represent draw the points, update the data buffer, update the points and insert points
    /// to Points array from data buffer respectively.
    /// </summary>
    public partial class ECGAnimationView : UserControl
    {
        private const int totalsizeX = 18000;
        private const int PEN_WIDTH = 3;
        private const int ResetPictureBoxTimeInSec = 30;
        private Graphics g;
        // 2 pen with different color to draw ecg and background grid
        private Pen mypen = new Pen(Color.Black, 2);
        private Pen bgp = new Pen(Color.DarkRed, 1);
        // set  the properties of the labels
        private Font drawFont = new Font("Microsoft Sans Serif", 10);
        private SolidBrush drawBrush = new SolidBrush(Color.Black);
        private int labelV, unitOfLabel;
        // variable to trace the newst point in the points array
        private int lengthOfPoints;
        // variables to control the scale of the grid
        private int changeValue;
        private int changeValueOfLabel;
        //variable to control the scale of data 
        private int pixelPerPoint;
        // variables of Grid
        private int numOfCellsX, numOfCellsY, cellSizeX, cellSizeY;
        // unit of cells is used to change the moves
        private int unitOfCell;
        //the size of the view
        private int viewsizeX, viewsizeY;
        // the position of the most left top point of the picturebox1
        private int picX, picY;
        // points array that contains the ecg data
        private PointF[] points;
        //points of location of mouse
        private Point newMouseDelta;
        public Point oldMouseDelta;
        //values changed after drag
        private int changedDrag;
        //numOfPoints can be plotted in a view
        private int numOfPoints;
        //count to call RepeatView and CalTimeDiff
        private int drawTimerCnt;
        //Data buffer
        public List<float> values;
        //time difference factor
        private double SpendTime;
        //points buffer for every Tick
        private float[] pointsToLine;
        //number of points to be added for every Tick
        private int numberOfpoints;
        //stopWatch to calTimeDiff
        private Stopwatch stopwatch;
        //extra points to be draw but smaller than one
        private double storedPoints;
        //predicted excution time
        private double diffCalTime;
        //factor to make y Axis easier to look
        private float yAxisFactor;
        //predicted points to be draw per second
        private int pointsPerSec;
        private int numOfLabels;
        //factor to make label dosen't need to redraw so frequently
        private int labelFactor;
        public event MouseEventHandler ViewClick;
        private long lastTime;
        private long thisTime;
        public ECGAnimationView()
        {
            InitializeComponent();
            InitializeParams();
        }
        private void ECGAnimationView_Load(object sender, EventArgs e)
        {
            //vertical lines array
            //Point[] vArray = new Point[0];
            InitializeParams();
        }
        private void InitializeParams()
        {
            // init UI related params
            // Viewsize is to control the size of the grid
            viewsizeX = pictureBox.Width;
            viewsizeY = pictureBox.Height;

            // Indicate the distance between the points
            //change to 250/250....
            pixelPerPoint = 1;
            // initial value of lengthOfPoints since we have only the origin in the beginging
            lengthOfPoints = 1;
            // cellsize is equal to viewsize divede by numOfCells
            unitOfCell = 1;
            //5 cell to be 1 second
            cellSizeX = unitOfCell * 50;
            cellSizeY = unitOfCell * 50;
            numOfCellsX = (totalsizeX - (totalsizeX % cellSizeX)) / cellSizeX;
            numOfCellsY = (viewsizeY - (viewsizeY % cellSizeY)) / cellSizeY;
            picX = pictureBox.Location.X;
            picY = pictureBox.Location.Y;
            unitOfLabel = 200;
            labelV = 0;
            // initial position of the 1st line,because Graphics.drawLines function will 
            // automaticlly draw the line between two point, if there is only one point
            points = new PointF[2];
            points[0] = new Point(0, viewsizeY / 2);
            points[1] = new Point(0, viewsizeY / 2);
            // init others
            drawTimerCnt = 0;
            changeValue = 0;
            changeValueOfLabel = 0;
            values = new List<float>();
            pointsPerSec = 240;
            numberOfpoints = (int)(pointsPerSec * DrawTimer.Interval);
            pointsToLine = new float[numberOfpoints];
            diffCalTime = 5000;
            yAxisFactor = 30;
            labelFactor = unitOfLabel / cellSizeX;
        }
        private void PictureBox_paint(object sender, PaintEventArgs e)
        {
            // Draw the ECG.
            g = e.Graphics;
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }
            // draw the labels
            labelFactor = unitOfLabel / cellSizeX;
            //draw more labels at the right end of the view so we can just move it to the left instead of reset it
            numOfLabels = ((numOfCellsX - numOfCellsX % labelFactor) / labelFactor) * 5;
            for (int l = 0; l < numOfLabels; l++)
            {
                int pos = (unitOfLabel * l) + changeValueOfLabel;
                if (pos >= 0 && pos <= viewsizeX)
                {
                    g.DrawString((labelV).ToString(), drawFont, drawBrush, pos, viewsizeY - 20);
                }
                labelV = labelV + unitOfLabel;
            }
            labelV -= numOfLabels * unitOfLabel;    
            // draw horizontal lines of gird
            for (int y = 0; y <= numOfCellsY; ++y)
            {
                g.DrawLine(bgp, 0, y * cellSizeY, viewsizeX, y * cellSizeY);
            }
            // draw vertical lines of gri， +6 because here we are drawing 6 more lines on the right of the grid for further moving.
            for (int x = 1; x <= numOfCellsX * 2; ++x)
            {
                int position = (x * cellSizeX) + changeValue;
                if (position >= 0 && position <= viewsizeX)
                {
                    g.DrawLine(bgp, position, 0, position, viewsizeY);
                }
            }
            //drawPoints
            g.DrawLines(mypen, points);
            //draw the most top line
            //g.DrawLine(bgp, viewsizeX - 1, 0, viewsizeX - 1, viewsizeY);
            // draw the most right line
            g.DrawLine(bgp, viewsizeX - 1, 0, viewsizeX - 1, viewsizeY);
            //draw the most left line
            g.DrawLine(bgp, 0, 0, 0, viewsizeY);
            // draw the bottom line, the reason that draw those line outside the for loop, is because the location.X of
            // the most left point of the picturebox is actually 599, therefore,it cannot display the 600 index point.
            g.DrawLine(bgp, 0, viewsizeY - 1, viewsizeX, viewsizeY - 1);
        }
        /// <summary>
        /// add points from data buffer to the array to be drawn
        /// </summary>
        /// <param name="y"></param>
        public void AddY(float[] y)
        {
            int targetIndex = 0;
            for (int j = 0; j < y.Length; j++)
            {
                if (y[j]!=-1)
                {
                    y[targetIndex++] = y[j];
                }
            }
            float[] actualArray = new float[targetIndex];
            Array.Copy(y, 0, actualArray, 0, targetIndex);
            int numberOfpoints = actualArray.Length;
            // to see if there are enough ecg data(points) to fill  the screen
            // if the screen cannot fit the points, then we move all the points
            // to the left by (changeValue) units, which depends on the scale of the grid.
            if (points[points.Length - 1].X >= viewsizeX)
            {
                // changeValue is to change the position of the grid and the label
                if (changeValue <= (-numOfCellsX * cellSizeX))
                {
                    changeValue += numOfCellsX * cellSizeX;
                }
                else
                {
                    changeValue -= numberOfpoints * pixelPerPoint;
                }
                numOfLabels = ((numOfCellsX - numOfCellsX % 4) / 4);
                if (changeValueOfLabel <= (-numOfLabels * unitOfLabel))
                {
                    labelV += numOfLabels * unitOfLabel;
                    changeValueOfLabel += numOfLabels * unitOfLabel;
                }
                else
                {
                    changeValueOfLabel -= numberOfpoints * pixelPerPoint;
                }
                // when the screen cannot fit all the points, move all the point to the left by numberOfpoints in input * pixelPerPoint unit.
                for (int count = 0; count < points.Length - numberOfpoints; count++)
                {
                    //points[count].X = points[count + numberOfpoints].X - pixelPerPoint;
                    points[count].Y = points[count + numberOfpoints].Y;
                }
                // and then add the array of points to the new dataValue.
                for (int i = 0; i < numberOfpoints; i++)
                {
                    int indexOfnewPoint = points.Length - numberOfpoints + i;
                    points[indexOfnewPoint] = new PointF(pixelPerPoint * indexOfnewPoint, actualArray[i]);
                }
            }
            else
            {
                // if there is still space to add new point, simply just apending it to the array
                Array.Resize(ref points, lengthOfPoints + numberOfpoints);
                for (int i = 0; i < numberOfpoints; i++)
                {
                    points[lengthOfPoints] = new PointF(pixelPerPoint * lengthOfPoints, actualArray[i]);
                    // increase the number of points
                    lengthOfPoints++;
                }
            }
        }
        /// <summary>
        /// a public function that can be called in the form.
        /// </summary>
        public void DrawTimer_Stop()
        {
            DrawTimer.Stop();
        }
        public void CleanTheData()
        {
            values.Clear();
        }
        public void DrawTimer_Start()
        {
            DrawTimer.Start();
        }
        /// <summary>
        /// add the newest data to the end of data buffer
        /// </summary>
        /// <param name="newValues"></param>
        public void UpdateValue(float[] newValues)
        {
            if (newValues != null)
            {
                foreach (float dataIndouble in newValues)
                {
                    if (dataIndouble == 0)
                    {
                        //in front of every data, there will be some zeros
                        values.Add(viewsizeY / 2);
                    }
                    else
                    {
                        //data is around 11.469945 when no input, for more user friendly, we set it to zero
                        double minusToZero = 11.469945;
                        values.Add((dataIndouble - (float)minusToZero) * yAxisFactor + (viewsizeY / 2));
                    }
                }
            }
        }
        /// <summary>
        /// Clean the view and stop the timers
        /// </summary>
        public void CleanView()
        {
            InitializeParams();
            StopTick();
        }
        /// <summary>
        /// Calculate the difference for every 5 second
        /// </summary>
        public void CalDiff()
        {
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            this.SpendTime = stopwatch.ElapsedMilliseconds / diffCalTime;
        }
        /// <summary>
        /// clean the whole view but keep the values in the data buffer that is not drawn.
        /// </summary>
        public void RepeatView()
        {
            var temp = values;
            InitializeParams();
            this.values = temp;
        }
        /// <summary>
        /// update timer is used to redrawing the picture per time interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (drawTimerCnt == 0)
            {
                //creates and start the instance of Stopwatch your sample code
                stopwatch = Stopwatch.StartNew();
                lastTime = stopwatch.ElapsedMilliseconds;
            }
            //record the actual running time since begining and divide by the expect running time to get the timeDifferenceFactor
            thisTime = stopwatch.ElapsedMilliseconds;
            this.SpendTime = thisTime - lastTime;
            lastTime = thisTime;
            //reset the view every 30 sec.
            drawTimerCnt++;
            if (drawTimerCnt >= ResetPictureBoxTimeInSec * 1000 / DrawTimer.Interval)
            {
                RepeatView();
            }

            //reset the number of points to be added every tick
            //here we assusme the result is Integer, if not should do floor and calculating
            numberOfpoints = (int)(pointsPerSec * DrawTimer.Interval * 0.001);
            //do points make up if there is time difference
            if (this.SpendTime != 0)
            {
                storedPoints += (pointsPerSec * SpendTime * 0.001) - Math.Floor(pointsPerSec * SpendTime * 0.001);
                numberOfpoints = (int)Math.Floor(pointsPerSec * SpendTime * 0.001);
            }
            if (storedPoints >= 1)
            {
                numberOfpoints++;
                storedPoints--;
            }
            pointsToLine = new float[numberOfpoints];
            //initial to be -1 for no data detection
            for(int j = 0; j < numberOfpoints; j++)
            {
                pointsToLine[j] = -1;
            }
            if (values == null || values.Count == 0)
            {
                for (int i = 0; i < numberOfpoints; i++)
                {
                    //if no data, draw the line in the mid of the view
                    pointsToLine[i] = viewsizeY / 2;
                }
                this.AddY(pointsToLine);
            }
            else
            {
                if (values.Count >= numberOfpoints)
                {
                    //draw the points in the values and delelte it.
                    for (int i = 0; i < numberOfpoints; i++)
                    {
                        pointsToLine[i] = viewsizeY - values[i];
                    }
                    this.AddY(pointsToLine);
                    values.RemoveRange(0, numberOfpoints);
                    if (values.Count <= pointsPerSec)
                    {
                        var test = values;
                    }
                }
                else
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        pointsToLine[i] = viewsizeY - values[i];
                    }
                    this.AddY(pointsToLine);
                    values.RemoveRange(0, values.Count);
                }
            }

            //Refresh the picturebox
            this.pictureBox.Invalidate();
        }
        public void StartTick()
        {
            DrawTimer.Start();
        }
        public void StopTick()
        {
            DrawTimer.Stop();
        }
        
        //Methods below is for future features, not compelted yet
        /// <summary>
        /// get the new location after press mouse_left and move, and record the changeValue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ECGAnimationView_MouseMove(object sender, MouseEventArgs e)
        {
            newMouseDelta = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                //Moves to left if positive, moves to right if negative
                changedDrag = oldMouseDelta.X - newMouseDelta.X;
            }
        }
        /// <summary>
        /// get the mouse location after press mouse_left button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ECGAnimationView_MouseDown(object sender, MouseEventArgs e)
        {
            if (ViewClick != null)
            {
                this.ViewClick(this, e);
            }
        }
        /// <summary>
        /// update the UI values when the size of pictureBox1 changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            // update the viewsizeXY
            viewsizeX = pictureBox.Width;
            viewsizeY = pictureBox.Height;
            // Update numOfCells
            numOfCellsX = (viewsizeX + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);
            numOfCellsY = (viewsizeY + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);
        }
        /// <summary>
        /// plot all the point at once
        /// </summary>
        public void PlotAll()
        {
            // update the viewsizeXY
            viewsizeX = pictureBox.Width;
            viewsizeY = pictureBox.Height;
            // Update numOfCells
            numOfCellsX = (viewsizeX + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);
            numOfCellsY = (viewsizeY + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);
            numOfPoints = viewsizeX / pixelPerPoint;
            for (int i = 0; i < numOfPoints; i++)
            {
                // this.AddY(rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3));
            }
        }
        /// <summary>
        /// update the pic after dragging
        /// </summary>
        private void UpdateDrag()
        {
            int pointsAdded = changedDrag / pixelPerPoint;
            for (int i = 0; i < pointsAdded; i++)
            {
                //this.AddY(rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3));
            }
        }
        /// <summary>
        /// public zoom in function
        /// </summary>
        public void ZoomIn(object sender, EventArgs e)
        {
            if (unitOfCell < 3)
            {
                return;
            }
            else
            {
                unitOfCell += 2;
            }
        }
        /// <summary>
        /// moves view to left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MoveLeft()
        {
        }
        /// <summary>
        /// moves view to right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MoveRight()
        {
        }
    }
}
