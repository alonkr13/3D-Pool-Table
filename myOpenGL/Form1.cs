using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenGL;
using System.Runtime.InteropServices; 

namespace myOpenGL
{
    public partial class Form1 : Form
    {
        cOGL cGL;
        double t = 0;
        double t1 = 0;
        Point temp;
        List<Ball> BallList = new List<Ball>();
        

        public Form1()
        {

            InitializeComponent();
            timerRUN.Enabled = true;
            cGL = new cOGL(panel1);
            this.DoubleBuffered = true;

            addNewBall(0);
        }


        private void timerRepaint_Tick(object sender, EventArgs e)
        {
             cGL.Draw();
            timerRepaint.Enabled = false;
        }


        private void timerRUN_Tick(object sender, EventArgs e)
        {
            cGL.Draw(); 
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            cGL.Draw();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            cGL.OnResize();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //important camera stuff Start ------------------------------------------------------------------------------------------------------------------------------------------------------------

        bool rightClickMovmentFlag = false;
        bool middleClickMovmentFlag = false;
        bool leftClickMovmentFlag = false;
        float oldMovmentDistanceX=0;
        float oldMovmentDistanceY=0;
        float oldDistanceX=0;
        float oldDistanceY=0;
        float initialMousePositionX;
        float currentMousePositionX;
        float distanceFromInitialToCurrentPositionX;
        float initialMousePositionY;
        float currentMousePositionY;
        float distanceFromInitialToCurrentPositionY;

        private void MouseDownPanelFunc(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)//right click
            {
            
                if (rightClickMovmentFlag == false)
                {
                    rightClickMovmentFlag = true;
                    initialMousePositionX = Cursor.Position.X;
                    initialMousePositionY = Cursor.Position.Y;
                }

            }

            if (e.Button == MouseButtons.Middle)//middle click
            {
            
                if (middleClickMovmentFlag == false)
                {
                    middleClickMovmentFlag = true;
                    initialMousePositionX = Cursor.Position.X;
                    initialMousePositionY = Cursor.Position.Y;
                }

            }

            if (e.Button == MouseButtons.Left)//Left click
            {
                if (leftClickMovmentFlag == false)
                    leftClickMovmentFlag = true;

                timer1.Stop();
                //endOfMovment(ball1);
                //this function is used when i want to be able to stop the ball and redirect it.
                t = 0;

                int initialCursorXpos = Cursor.Position.X;
                int initialCursorYpos = Cursor.Position.Y;

                temp.X = initialCursorXpos;
                temp.Y = initialCursorYpos;
            }

        }

        private void MouseUpPanelFunc(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)//right click
            {
                label2.Text = Convert.ToString(cGL.cameraAngleX);
                
                if (rightClickMovmentFlag == true)
                {
                    oldDistanceX = cGL.cameraAngleY;
                    oldDistanceY = cGL.cameraAngleX;
                    rightClickMovmentFlag = false;
                }
            }
            
            if (e.Button == MouseButtons.Middle)//middle click
            {
                label2.Text = Convert.ToString(cGL.cameraTranslateX);
                
                if (middleClickMovmentFlag == true)
                {
                    oldMovmentDistanceX = cGL.cameraTranslateX;
                    oldMovmentDistanceY = cGL.cameraTranslateY;
                    middleClickMovmentFlag = false;
                }
            }
                        
            if (e.Button == MouseButtons.Left)//left click
            {
                if (leftClickMovmentFlag == true)
                    leftClickMovmentFlag = false;

                //this is the new way, where the mouse only controls the force, and the rotation/direction is controled with a slider


                double force = Math.Sqrt(Math.Pow((temp.X - Cursor.Position.X), 2) + Math.Pow((temp.Y - Cursor.Position.Y), 2));
                if (force >= 300)
                    force = 300;
                Console.WriteLine(force);
                cGL.ball1._x.b = force * Math.Cos((cGL.queRotation + 90) * (Math.PI / 180f));
                cGL.ball1._y.b = force * Math.Sin((cGL.queRotation - 90) * (Math.PI / 180f));

                double newLine = Math.Sqrt(Math.Pow(cGL.ball1._x.b, 2) + Math.Pow(cGL.ball1._y.b, 2));

                cGL.ball1._x.c = -(cGL.ball1._x.b / newLine) * 300;
                cGL.ball1._y.c = -(cGL.ball1._y.b / newLine) * 300;

                Console.WriteLine("this is the b: (" + cGL.ball1._x.b + "," + cGL.ball1._y.b + ")");
                Console.WriteLine("this is the c: (" + cGL.ball1._x.c + "," + cGL.ball1._y.c + ")");

                timer1.Start();
                cGL.queAppearance = false;
                cGL.quePower = 0;
            }

        }

        private void MouseMovePanelFunc(object sender, MouseEventArgs e)
        {
            if (rightClickMovmentFlag == true)
            {
                currentMousePositionX = Cursor.Position.X;
                currentMousePositionY = Cursor.Position.Y;
                distanceFromInitialToCurrentPositionX = currentMousePositionX - initialMousePositionX;
                distanceFromInitialToCurrentPositionY = currentMousePositionY - initialMousePositionY;
                cGL.cameraAngleY = oldDistanceX + distanceFromInitialToCurrentPositionX / 2;
                cGL.cameraAngleX = oldDistanceY + distanceFromInitialToCurrentPositionY;
                label2.Text = Convert.ToString(cGL.cameraAngleY);
            }

            if (middleClickMovmentFlag == true)
            {
                currentMousePositionX = Cursor.Position.X;
                currentMousePositionY = Cursor.Position.Y;
                distanceFromInitialToCurrentPositionX = currentMousePositionX - initialMousePositionX;
                distanceFromInitialToCurrentPositionY = currentMousePositionY - initialMousePositionY;
                cGL.cameraTranslateX = oldMovmentDistanceX + distanceFromInitialToCurrentPositionX / 2;
                cGL.cameraTranslateY = oldMovmentDistanceY + distanceFromInitialToCurrentPositionY / 2;
                label2.Text = Convert.ToString(cGL.cameraTranslateY);
            }
            
            if (leftClickMovmentFlag == true)
            {
                currentMousePositionX = Cursor.Position.X;
                currentMousePositionY = Cursor.Position.Y;
                distanceFromInitialToCurrentPositionX = currentMousePositionX - initialMousePositionX;
                distanceFromInitialToCurrentPositionY = currentMousePositionY - initialMousePositionY;
                cGL.quePower = (float)Math.Sqrt(Math.Pow((temp.X - Cursor.Position.X), 2) + Math.Pow((temp.Y - Cursor.Position.Y), 2))/100;
                if (cGL.quePower >= 3)
                    cGL.quePower = 3;
            }

        }

        private void hScrollBar1_changed(object sender, ScrollEventArgs e)
        {
            cGL.PlaneMoveValue = (float)hScrollBar1.Value;
            Console.WriteLine(cGL.PlaneMoveValue.ToString());
        }


        private void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            cGL.queRotation = hScrollBarRotation.Value;
            Console.WriteLine(cGL.queRotation.ToString());

        }


        //ball physics:
        private void addNewBall(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Ball newTempBall = new Ball(i);
                BallList.Add(newTempBall);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += 0.001;

            cGL.ball1._locationX= (float)ballXmovment(cGL.ball1);
            cGL.ball1._locationY = (float)ballYmovment(cGL.ball1);


            if (cGL.ball1._locationX <= -9 + cGL.ball1._ballRadius)
                reverseSpeed(cGL.ball1, 1);
            if (cGL.ball1._locationX >= 9- cGL.ball1._ballRadius)
                reverseSpeed(cGL.ball1, 2);
            if (cGL.ball1._locationY <= -4 + cGL.ball1._ballRadius)
                reverseSpeed(cGL.ball1, 3);
            if (cGL.ball1._locationY >= 4 - cGL.ball1._ballRadius)
                reverseSpeed(cGL.ball1, 4);

            if ((int)(cGL.ball1._y.b + 2 * cGL.ball1._y.c * t) == 0 && (int)(cGL.ball1._x.b + 2 * cGL.ball1._x.c * t) == 0)
            {
                endOfMovment(cGL.ball1);
            }

            //this is a reapting code, should correct it and put the cueball in the ball list...
            foreach (Ball currentBall in BallList)
            {
                currentBall._locationX = (float)ballXmovment(currentBall);
                currentBall._locationY = (float)ballYmovment(currentBall);


                if (cGL.ball1._locationX <= -9 + cGL.ball1._ballRadius)
                    reverseSpeed(currentBall, 1);
                if (cGL.ball1._locationX >= 9 - cGL.ball1._ballRadius)
                    reverseSpeed(currentBall, 2);
                if (cGL.ball1._locationY <= -4 + cGL.ball1._ballRadius)
                    reverseSpeed(currentBall, 3);
                if (cGL.ball1._locationY >= 4- cGL.ball1._ballRadius)
                    reverseSpeed(currentBall, 4);

                if ((float)(currentBall._y.b + 2 * currentBall._y.c * t) == 0 && (float)(currentBall._x.b + 2 * currentBall._x.c * t) == 0)
                {
                    endOfMovment(currentBall);
                }
            }
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            checkForBallCollisions();
        }

        private void checkForBallCollisions()
        {
            foreach (Ball tempBall in BallList)
            {
                if (ballIntersect(tempBall, cGL.ball1))
                    ballsCollide(tempBall, cGL.ball1);
            }
        }

        private void ballsCollide(Ball ball1, Ball ball2)
        {

            ball1._x.a = ballXmovment(ball1);
            ball1._y.a = ballYmovment(ball1);
            ball2._x.a = ballXmovment(ball2);
            ball2._y.a = ballYmovment(ball2);

            ball1._x.b += (2 * ball1._x.c * t);
            ball1._y.b += (2 * ball1._y.c * t);
            ball2._x.b += (2 * ball2._x.c * t);
            ball2._y.b += (2 * ball2._y.c * t);

            ball1.angle = Math.Sqrt(Math.Pow(ball1._x.b, 2) + Math.Pow(ball1._y.b, 2));
            ball2.angle = Math.Sqrt(Math.Pow(ball2._x.b, 2) + Math.Pow(ball2._y.b, 2));

            Vector tempX = ball1._x;
            Vector tempY = ball1._y;

            ball1._x.b += ball2._x.b;
            ball1._y.b += ball2._y.b;
            ball2._x.b += tempX.b;
            ball2._y.b += tempY.b;

        }

        private void reverseSpeed(Ball inBall, int num)
        {
            inBall._x.a = ballXmovment(inBall);
            inBall._y.a = ballYmovment(inBall);
            inBall._x.b += (2 * inBall._x.c * t);
            inBall._y.b += (2 * inBall._y.c * t);

            t = 0.001;

            Console.WriteLine("reverse speed thing");
            switch (num)
            {
                case 1:
                case 2:
                    inBall._x.b *= -1;
                    inBall._x.c *= -1;
                    break;
                case 3:
                case 4:
                    inBall._y.b *= -1;
                    inBall._y.c *= -1;
                    break;
            }
        }

        private void endOfMovment(Ball inBall)
        {
            inBall._x.a = ballXmovment(inBall);
            inBall._y.a = ballYmovment(inBall);
            inBall._x.b = 0;
            inBall._y.b = 0;
            inBall._x.c = 0;
            inBall._y.c = 0;
            cGL.queAppearance = true;
        }

        private double ballXmovment(Ball inball)
        {
            return (float)(inball._x.a + inball._x.b * t + inball._x.c * t * t);
        }
        
        private double ballYmovment(Ball inball)
        {
            return (float)(inball._y.a + inball._y.b * t + inball._y.c * t * t);
        }
        
        private bool ballIntersect(Ball inball_1, Ball inball_2)
        {
            double x, y, distance;

            x = inball_1._locationX - inball_2._locationX;
            y = inball_1._locationY - inball_2._locationY;

            distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            if (distance <= inball_1._ballRadius*2)
                return true;
            return false;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cGL.orbitBall = !cGL.orbitBall;
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            animation_timer.Start();
        }

        private void animation_timer_Tick(object sender, EventArgs e)
        {
            t1 += 1;

            //shadow apearance
            if (t1 > 100 && t1 < 400)
                cGL.armShadowAppearance = true;
            if (t1 > 320)
                cGL.armShadowAppearance = false;


            if (cGL.armHeight<0 && t1 < 200)
            {
                cGL.armHeight += 0.07f;
            }

            if (t1 > 40 && t1 < 200)
            {
                if (cGL.armRotation2 < 94)
                    cGL.armRotation2 += 0.6f;
                if (cGL.armRotation2 > 94)
                    cGL.armRotation2 = 94;
            }

            if (t1 > 100 && t1 < 200)
            {
                if (cGL.armRotation1 < 45)
                    cGL.armRotation1 += 0.7f;
                if (cGL.armRotation1 > 45)
                    cGL.armRotation1 = 45;
            }

            if (t1 == 198)
                cGL.armBallAppearance = false;

            if(t1>=198)
            {
                if(cGL.clawOpening > -20)
                cGL.clawOpening -= 0.5f;
            }

            if (t1 >= 250 && t1 < 550)
            {
                if (cGL.armRotation2 > 0)
                    cGL.armRotation2 -= 0.9f;
                if (cGL.armRotation1 > 0)
                    cGL.armRotation1 -= 0.7f;
                cGL.armHeight -= 0.045f;

            }

        }
    }
}