using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class ECGAnimationView : UserControl
    {
        private const int PEN_WIDTH = 3;

        private Bitmap DrawArea;
        private Graphics g;

        // 2 pen with different color to draw ecg and background grid
        private Pen mypen = new Pen(Color.Red, 3);
        private Pen bgp = new Pen(Color.DarkRed, 1);
        // set  the properties of the labels
        private Font drawFont = new Font("Microsoft Sans Serif", 10);
        private SolidBrush drawBrush = new SolidBrush(Color.Black);

        private int labelV, unitOfLabel;
        // variable to trace the newst point in the points array
        private int i;
        // variables to count how many points have been added
        private int moves;
        // variables to control the scale of the grid after zoom in and zoom out 
        private int changeValue;
        //variable to control the scale of data after zoom in and zoom out
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
        private Point[] points = new Point[2];

        //points of location of mouse
        private Point newMouseDelta;
        public Point oldMouseDelta;
        //values changed after drag
        private int changedDrag;
        //numOfPoints can be plotted in a view
        private int numOfPoints;
        //bool to disable addY in drawTimer
        private bool disableadd;
        private bool enableDragEvent;

        public bool historyFlag;

        public event MouseEventHandler ViewClick;


        private Random rnd;

        public ECGAnimationView()
        {
            InitializeComponent();
            //CenterToScreen();
            InitializeParams();
            if(historyFlag == true)
            {
                plotAll();
            }
        }

        private void ECGAnimationView_Load(object sender, EventArgs e)
        {

        }

        private void InitializeParams()
        {

            // init UI related params
            // Viewsize is to control the size of the grid
            viewsizeX = pictureBox1.Width;
            viewsizeY = pictureBox1.Height;

            // Indicate the distance between the points
            pixelPerPoint = 50;
            // initial value of i since we have only the origin in the beginging
            i = 1;
            // cellsize is equal to viewsize divede by numOfCells
            unitOfCell = 1;
            cellSizeX = unitOfCell * pixelPerPoint;
            cellSizeY = unitOfCell * pixelPerPoint;
            

            numOfCellsX = (viewsizeX + cellSizeX) / cellSizeX;
            numOfCellsY = (viewsizeY + cellSizeY) / cellSizeY;

            picX = pictureBox1.Location.X;
            picY = pictureBox1.Location.Y;

            unitOfLabel = 200;
            labelV = 0;

            // initial position of the 1st line,because Graphics.drawLines function will 
            // automaticlly draw the line between two point, if there is only one point
            points[0] = new Point(0, viewsizeY / 2);
            points[1] = new Point(0, viewsizeY / 2);

            // init others
            rnd = new Random();
            disableadd = false;
            enableDragEvent = false;
        }

        private void pictureBox1_paint(object sender, PaintEventArgs e)
        {
            // Draw the ECG.
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

            using (g = Graphics.FromImage(DrawArea))
            {
                g.DrawLines(mypen, points);

                // draw the labels
                for (int l = 0; l < numOfCellsX; l++)
                {
                    g.DrawString((labelV).ToString(), drawFont, drawBrush, (l * cellSizeX) + changeValue, viewsizeY / 2);
                    labelV = labelV + unitOfLabel;
                }

                labelV -= numOfCellsX * unitOfLabel;

                // draw horizontal lines of gird
                for (int y = 0; y <= numOfCellsY; ++y)
                {
                    g.DrawLine(bgp, 0, y * cellSizeY, viewsizeX, y * cellSizeY);
                }

                // draw vertical lines of grid
                for (int x = 1; x <= numOfCellsX; ++x)
                {
                    g.DrawLine(bgp, (x * cellSizeX) + changeValue, 0, (x * cellSizeX) + changeValue, numOfCellsY * cellSizeY);
                }

                //draw the most top line
                //g.DrawLine(bgp, viewsizeX - 1, 0, viewsizeX - 1, viewsizeY);
                // draw the most right line
                g.DrawLine(bgp, viewsizeX - 1, 0, viewsizeX - 1, viewsizeY);
                //draw the most left line
                g.DrawLine(bgp, 0, 0, 0, viewsizeY);
                // draw the bottom line, the reason that draw those line outside the for loop, is because the location.X of
                // the most left point of the picturebox is actually 599, therefore,it cannot display the 600 index point.
                g.DrawLine(bgp, 0, viewsizeY - 1, viewsizeX, viewsizeY - 1);

                // TODO SET VIEWSIZEX TO 601;
                pictureBox1.Image = DrawArea;
            }
        }

        /// <summary>
        /// add the point at the left end
        /// </summary>
        /// <param name="y"></param>
        public void addYatLeft(int y)
        {
            // to see if there are enough ecg data(points) to fill  the screen
            // if the screen cannot fit the points, then we move all the points
            // to the left by (changeValue) units, which depends on the scale of the grid.
            if (points[0].X <= 0)
            {

                // moves indicate the number of point have been added
                moves++;

                // changeValue is to change the position of the grid and the label
                if (changeValue > cellSizeX)
                {
                    changeValue += pixelPerPoint;
                    if (changeValue == cellSizeX)
                    {
                        changeValue = 0;
                    }
                }

                if (moves % unitOfCell == 0)
                {
                    // increase labelV when the label moves with the grid lines
                    // because 3 moves(points) has length of 150 units which is a cellSizeX
                    // then the vertical lines of grid will back to its original position as the changeValue 
                    // set to 0. So we should add all labelValue by one unitOfLabel
                    labelV -= unitOfLabel;
                }

                // when the screen cannot fit all the points, move all the point to the left by one pixelPerPoint unit.
                for (int count = points.Length - 1; count >0; count--)
                {
                    points[count].X = points[count - 1].X + pixelPerPoint;
                    points[count].Y = points[count - 1].Y;
                }
                // and then set the most right point to the new dataValue.
                points[0] = new Point(0, y);
            }
            else
            {
                // if there is still space to add new point, simply just apending it to the array
                Array.Resize(ref points, i + 1);
                points[i] = new Point(pixelPerPoint * i, y);
                // increase the number of points
                i++;
                MessageBox.Show("error");
            }
            if(points[points.Length-1] == null)
            {
                MessageBox.Show("reach the end");
            }

            Invalidate();
        }

        public void addY(int y)
        {
            // to see if there are enough ecg data(points) to fill  the screen
            // if the screen cannot fit the points, then we move all the points
            // to the left by (changeValue) units, which depends on the scale of the grid.
            if (points[points.Length - 1].X >= viewsizeX)
            {

                // moves indicate the number of point have been added
                moves++;

                // changeValue is to change the position of the grid and the label
                if (changeValue > -cellSizeX)
                {
                    changeValue -= pixelPerPoint;
                    if (changeValue == -cellSizeX)
                    {
                        changeValue = 0;
                    }
                }

                if (moves % unitOfCell == 0)
                {
                    // increase labelV when the label moves with the grid lines
                    // because 3 moves(points) has length of 150 units which is a cellSizeX
                    // then the vertical lines of grid will back to its original position as the changeValue 
                    // set to 0. So we should add all labelValue by one unitOfLabel
                    labelV += unitOfLabel;
                }

                // when the screen cannot fit all the points, move all the point to the left by one pixelPerPoint unit.
                for (int count = 0; count < points.Length - 1; count++)
                {
                    points[count].X = points[count + 1].X - pixelPerPoint;
                    points[count].Y = points[count + 1].Y;
                }
                // and then set the most right point to the new dataValue.
                points[points.Length - 1] = new Point(pixelPerPoint * (i - 1), y);
            }
            else
            {
                // if there is still space to add new point, simply just apending it to the array
                Array.Resize(ref points, i + 1);
                points[i] = new Point(pixelPerPoint * i, y);
                // increase the number of points
                i++;
            }

            Invalidate();
        }

        /// <summary>
        /// a public function that can be called in the form.
        /// </summary>
        public void DrawTimer_Stop()
        {
            DrawTimer.Stop();
        }

        public void DrawTimer_Start()
        {
            DrawTimer.Start();
        }

        public void CleanView()
        {
            //Array.Clear(points, 0, points.Length);
            InitializeParams();
            DrawTimer.Stop();
        }

        public void disableaddY()
        {
            disableadd = true;
        }

        public void enableDrag()
        {
            enableDragEvent = true;
        }

        /// <summary>
        /// public zoom in function
        /// </summary>
        public void zoomIn(object sender, EventArgs e)
        {
            if (unitOfCell< 3)
            {
                return;
            }
            else
            {
                unitOfCell += 2;
            }
            //Invalidate();
        }

        /// <summary>
        /// update timer is used to redrawing the picture and update params per time interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if(disableadd == false)
            {
                // generate a random value to be  the ecg data
                this.addY(rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3));
            }

            if(enableDragEvent == true)
            {
                this.updateDrag();
            }

            //textbox1_TextChanged(sender, e);
        }

        /// <summary>
        /// Generate a different number for channel2
        /// </summary>
        public void Generate_Diff_RamNum()
        {
            int a = rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3);
        }

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
            if(ViewClick != null)
            {
                this.ViewClick(this, e);
            }

        }

        /// <summary>
        /// update the UI values when the size of pictureBox1 changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            // update the viewsizeXY
            viewsizeX = pictureBox1.Width;
            viewsizeY = pictureBox1.Height;
            // Update numOfCells
            numOfCellsX = (viewsizeX + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);
            numOfCellsY = (viewsizeY + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);
        }

        /// <summary>
        /// plot all the point at once
        /// </summary>
        public void plotAll()
        {
            // update the viewsizeXY
            viewsizeX = pictureBox1.Width;
            viewsizeY = pictureBox1.Height;
            // Update numOfCells
            numOfCellsX = (viewsizeX + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);
            numOfCellsY = (viewsizeY + unitOfCell * pixelPerPoint) / (unitOfCell * pixelPerPoint);

            numOfPoints = viewsizeX / pixelPerPoint;
            for(int i = 0; i < numOfPoints; i++)
            {
                this.addY(rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3));
            }

        }

        /// <summary>
        /// update the pic after dragging
        /// </summary>
        private void updateDrag()
        {
            int pointsAdded = changedDrag / pixelPerPoint;
            for(int i = 0; i < pointsAdded; i++)
            {
                this.addY(rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3));
            }
             
        }

        /// <summary>
        /// moves view to left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void moveLeft()
        {

            this.addY(rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3));
        }

        /// <summary>
        /// moves view to right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void moveRight()
        {
            this.addYatLeft(rnd.Next(viewsizeY / 3, 2 * viewsizeY / 3));
        }
    }

}
