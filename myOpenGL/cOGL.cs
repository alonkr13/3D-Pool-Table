using System;
using System.Drawing;//this is importnt for the Bitmap class
using System.Windows.Forms;

namespace OpenGL
{
    class cOGL
    {
        Control p;
        int Width;
        int Height;

        GLUquadric obj;

        public cOGL(Control pb)
        {

            p = pb;
            Width = p.Width;
            Height = p.Height;
            obj = GLU.gluNewQuadric(); //!!!
            InitializeGL();

            ground[0, 0] = -15;
            ground[0, 1] = 0;
            ground[0, 2] = 0;

            ground[1, 0] = 15;
            ground[1, 1] = 0;
            ground[1, 2] = 0;

            ground[2, 0] = 15;
            ground[2, 1] = 0;
            ground[2, 2] = 15;
        }

        ~cOGL()
        {
            GLU.gluDeleteQuadric(obj); //!!!
            WGL.wglDeleteContext(m_uint_RC);
        }

		uint m_uint_HWND = 0;
        public uint HWND
		{
			get{ return m_uint_HWND; }
		}
		
        uint m_uint_DC   = 0;

        public uint DC
		{
			get{ return m_uint_DC;}
		}
		uint m_uint_RC   = 0;

        public uint RC
		{
			get{ return m_uint_RC; }
		}

        public uint tableList;
        public Ball ball1 = new Ball();


        protected void CreateTableList()
        {
            //Table list
            GL.glNewList(tableList, GL.GL_COMPILE); //---------------------------------------------------------------- List Start

            //Color control
            float woodR = 0.5f, woodG = 0.35f, woodB = 0.2f;
            float floorR = 0.2f, floorG = 0.2f, floorB = 0.2f;
            float clothR = 0.1f, clothG = 0.3f, clothB = 0.1f;
            float pocketR = 0.4f, pocketG = 0.45f * 0.8f, pocketB = 0.4f;


            for (int i = 0; i < 4; i++)
            {
                GL.glPushMatrix();

                if (i % 2 != 0)
                    GL.glScaled(-1, 1, 1);
                if (i > 1)
                    GL.glScaled(1, 1, -1);

                //Table surface

                GL.glColor3f(1.0f, 1.0f, 1.0f);
                GL.glEnable(GL.GL_TEXTURE_2D);
                GL.glBindTexture(GL.GL_TEXTURE_2D, textures[0]);
                GL.glBegin(GL.GL_QUADS);
                //GL.glDisable(GL.GL_LIGHTING);
                GL.glNormal3f(0.0f, 1.0f, 0.0f);
                    GL.glTexCoord2f(1.0f, 3.0f);
                    GL.glVertex3f(0.0f, 0.0f, 0.0f);
                
                    GL.glTexCoord2f(-1.0f, 3.0f);
                    GL.glVertex3f(0.0f, 0.0f, -4.0f);
                
                    GL.glTexCoord2f(-1.0f, 0.0f);
                    GL.glVertex3f(9.0f, 0.0f, -4.0f);
                
                    GL.glTexCoord2f(1.0f, 0.0f);
                    GL.glVertex3f(9.0f, 0.0f, 0.0f);
                GL.glEnd();
                GL.glDisable(GL.GL_TEXTURE_2D);// we need to disable the texture to give color to the other parts
                //createBox((float)0, (float)9, (float)0.0, (float)-0.01, (float)0.0, (float)-4, clothR, clothG, clothB);

                //Table longside
                createBox((float)0.74, (float)8.5, (float)0.5, (float)-0.5, (float)-4, (float)-5, clothR, clothG, clothB);
                //Table shortside
                createBox((float)9, (float)10, (float)0.5, (float)-0.5, (float)0, (float)-3.5, clothR, clothG, clothB);
                //Table longside 2
                createBox((float)0, (float)10, (float)0.5, (float)-0.5, (float)-5, (float)-5.5, woodR, woodG, woodB);
                //Table shortside 2 
                createBox((float)10, (float)10.5, (float)0.5, (float)-0.5, (float)0, (float)-5, woodR, woodG, woodB);
                
                
                //Table Leg
                GL.glDisable(GL.GL_LIGHTING);
                createBox((float)8.5, (float)9.5, (float)-0.5, (float)-4, (float)-3.5, (float)-4.5, woodR*0.15f, woodG * 0.15f, woodB * 0.15f);
                GL.glEnable(GL.GL_LIGHTING);

                //Table Corner Cylinder
                GL.glColor3f(woodR, woodG, woodB);
                createCylinder(10f, -0.5f, -5f, 0.5f, 1, 64, 0.25f, 270, true,false,true);



                //Table Corner Pocket Cylinder
                createCylinder(9.5f, -0.5f, -4.5f, 0.5f, 1, 64, 0.75f, 180, false, true);
                //Table Side Pocket Cylinder
                createCylinder(0, -0.5f, -4.5f, 0.5f, 1, 64, 0.25f, 270, false, true);



                GL.glColor3f(clothR, clothG, clothB);

                //Table Corner Pocket wall 1
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(9f, 0.5f, -4.5f);
                GL.glVertex3f(9f, -0.5f, -4.5f);
                GL.glVertex3f(8.5f, -0.5f, -4f);
                GL.glVertex3f(8.5f, 0.5f, -4f);
                GL.glEnd();
                //wall top fill 1
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(9f, 0.5f, -4.5f);
                GL.glVertex3f(9f, 0.5f, -5f);
                GL.glVertex3f(8.5f, 0.5f, -5f);
                GL.glVertex3f(8.5f, 0.5f, -4f);

                //Table Corner Pocket wall 2
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(9.5f, 0.5f, -4f);
                GL.glVertex3f(9.5f, -0.5f, -4f);
                GL.glVertex3f(9f, -0.5f, -3.5f);
                GL.glVertex3f(9f, 0.5f, -3.5f);
                GL.glEnd();
                //wall top fill 2
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(9.5f, 0.5f, -4f);
                GL.glVertex3f(10f, 0.5f, -4f);
                GL.glVertex3f(10f, 0.5f, -3.5f);
                GL.glVertex3f(9f, 0.5f, -3.5f);
                GL.glEnd();

                //Table Side Pocket walls
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(0.5f, 0.5f, -4.5f);
                GL.glVertex3f(0.5f, -0.5f, -4.5f);
                GL.glVertex3f(0.75f, -0.5f, -4f);
                GL.glVertex3f(0.75f, 0.5f, -4f);
                GL.glEnd();
                
                //Table Side Pocket walls fill
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(0.5f, 0.5f, -4.5f);
                GL.glVertex3f(0.75f, 0.5f, -4.5f);
                GL.glVertex3f(0.75f, 0.5f, -4f);
                GL.glVertex3f(0.75f, 0.5f, -4f);
                GL.glEnd();
                
                //Table Side Pocket walls fill
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(0.5f, 0.5f, -4.5f);
                GL.glVertex3f(0.75f, 0.5f, -4.5f);
                GL.glVertex3f(0.75f, 0.5f, -5f);
                GL.glVertex3f(0.5f, 0.5f, -5f);
                GL.glEnd();

                GL.glPopMatrix();
            }
            GL.glEndList();                         //---------------------------------------------------------------- Table List End



        }
        protected void DrawQue(bool forShading = false)
        {
            if (!queAppearance)
                return;

            if (forShading == true)
            {
                GL.glDisable(GL.GL_LIGHTING);
                GL.glColor3d(0.1f * 0.3f, 0.3f * 0.3f, 0.1f * 0.3f);
            }
            else
                GL.glColor3f(0.8f, 0.8f, 0.8f);

            GL.glPushMatrix();
            GL.glTranslatef(ball1._locationX, LightMove[1] * 0.2f, ball1._locationY);

            GL.glRotatef(queRotation, 0, 1, 0);
            GL.glRotatef(-10, 1, 0, 0);
            GL.glTranslatef(0, 0f,0.4f + quePower);
            GLUT.gluCylinder(obj, 0.03, 0.05, 4, 16, 16);

            GL.glPopMatrix();


        }

        protected void DrawArm(bool forShading = false)
        {
            float colorR = 0.8f, colorG = 0.8f, colorB = 0.8f;
            float armSize = 4.6f;



            if (armShadowAppearance == false && forShading == true)
                return;

                if (forShading == true )
            {
                GL.glDisable(GL.GL_LIGHTING);
                GL.glColor3d(0.3f, 0.3f, 0.3f);
                
                colorR = 0.1f * 0.3f;
                colorG = 0.3f * 0.3f;
                colorB = 0.1f * 0.3f;

            }
            else
                GL.glColor3f(0.8f, 0.8f, 0.8f);

            GL.glPushMatrix();

                GL.glTranslatef(0,0,-6.5f);//move back
                GL.glTranslatef(0f,0f,0f);//move right-left
                GL.glTranslatef(0f, armHeight, 0f);//move up-down
                if (forShading == false)
                    createBox((float)-0.3, (float)0.3, (float)0.5, (float)-4, (float)0.5, (float)-0.5, colorR, colorG, colorB); // first block

                GL.glTranslatef(-0.35f, 0.5f, 0f);
                GL.glRotatef(armRotation1, 1, 0, 0);// first arm angle
                GL.glRotatef(90, 0, 1, 0);
                if (forShading == false)
                    GLUT.gluCylinder(obj, 0.7, 0.7, 0.7, 16, 16);
                if (forShading == false)
                    GLUT.gluDisk(obj, 0, 0.7, 16, 16);
                GL.glTranslatef(0f, 0f, 0.7f);
                if (forShading == false)
                    GLUT.gluDisk(obj, 0, 0.7, 16, 16);
                GL.glTranslatef(0f, 0f, -0.35f);
                GL.glRotatef(-90, 0, 1, 0);
                if (forShading == false)
                    createBox((float)-0.3, (float)0.3, (float)armSize, (float)0.5, (float)0.5, (float)-0.5, colorR, colorG, colorB);

            
                GL.glTranslatef(-0.35f, armSize, 0f);//move origin to the end of the arm
                GL.glRotatef(armRotation2, 1, 0, 0);// second arm angle
                GL.glRotatef(90, 0, 1, 0);
                if (forShading == false)
                    GLUT.gluCylinder(obj, 0.7, 0.7, 0.7, 16, 16);
                if (forShading == false)
                    GLUT.gluDisk(obj, 0, 0.7, 16, 16);
                GL.glTranslatef(0f, 0f, 0.7f);
                if (forShading == false)
                    GLUT.gluDisk(obj, 0, 0.7, 16, 16);
                GL.glTranslatef(0f, 0f, -0.35f);
                GL.glRotatef(-90, 0, 1, 0);
                createBox((float)-0.3, (float)0.3, (float)armSize-0.2f, (float)0.5, (float)0.5, (float)-0.5, colorR, colorG, colorB);

                GL.glTranslatef(0f, armSize - 0.2f, 0f);//move origin to the end of the arm
                createBox((float)-0.1, (float)0.1, (float)0.1f, (float)0, (float)-0.1, (float)0.1, colorR, colorG, colorB);
            
                GL.glTranslatef(0f, 0.55f, 0f);//move origin to the end of the arm
                if(forShading == false)
                    GL.glColor3f(0.8f, 0.8f, 0.8f);


            if (armBallAppearance)
            {
                GL.glPushAttrib(GL.GL_ALL_ATTRIB_BITS); // שמירת הסטטוס של OpenGL
                GL.glPushMatrix(); // שמירת מצב המטריצה

                GLUT.glutSolidSphere(0.25, 32, 16);

                GL.glPopMatrix(); // שחזור מצב המטריצה
                GL.glPopAttrib(); // שחזור כל הסטטוסים
            }




            GL.glTranslatef(0f, -0.55f, 0f);//move origin to the end of the arm
                //the claw

                for (int i = 0; i < 2; i++)
                {
                    GL.glPushMatrix();

                    if (i == 1)
                        GL.glScalef(-1, 1, 1);

                    GL.glRotatef(clawOpening, 0, 0, 1);
                    GL.glBegin(GL.GL_QUADS);
                    // פאה קדמית (כבר קיימת)
                    GL.glVertex3d(0.1, 0, 0);
                    GL.glVertex3d(0.3, 0.25, 0);
                    GL.glVertex3d(0.4, 0.25, 0);
                    GL.glVertex3d(0.25, 0, 0);
                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.3, 0.25, 0);
                    GL.glVertex3d(0.4, 0.25, 0);
                    GL.glVertex3d(0.25, 0.6, 0);
                    GL.glVertex3d(0.25, 0.45, 0);
                    GL.glEnd();

                    // פאה אחורית (עם עומק בציר Z)
                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.1, 0, -0.2);
                    GL.glVertex3d(0.3, 0.25, -0.2);
                    GL.glVertex3d(0.4, 0.25, -0.2);
                    GL.glVertex3d(0.25, 0, -0.2);
                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.3, 0.25, -0.2);
                    GL.glVertex3d(0.4, 0.25, -0.2);
                    GL.glVertex3d(0.25, 0.6, -0.2);
                    GL.glVertex3d(0.25, 0.45, -0.2);
                    GL.glEnd();

                    // חיבור הפאות - דפנות
                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.1, 0, 0);
                    GL.glVertex3d(0.25, 0, 0);
                    GL.glVertex3d(0.25, 0, -0.2);
                    GL.glVertex3d(0.1, 0, -0.2);
                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.3, 0.25, 0);
                    GL.glVertex3d(0.3, 0.25, -0.2);
                    GL.glVertex3d(0.4, 0.25, -0.2);
                    GL.glVertex3d(0.4, 0.25, 0);
                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.25, 0.6, 0);
                    GL.glVertex3d(0.25, 0.6, -0.2);
                    GL.glVertex3d(0.25, 0.45, -0.2);
                    GL.glVertex3d(0.25, 0.45, 0);
                    GL.glEnd();

                    // סגירת הקצוות (כיסויים)
                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.1, 0, 0);
                    GL.glVertex3d(0.3, 0.25, 0);
                    GL.glVertex3d(0.3, 0.25, -0.2);
                    GL.glVertex3d(0.1, 0, -0.2);
                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3d(0.25, 0, 0);
                    GL.glVertex3d(0.4, 0.25, 0);
                    GL.glVertex3d(0.4, 0.25, -0.2);
                    GL.glVertex3d(0.25, 0, -0.2);
                    GL.glEnd();
                    GL.glPopMatrix();

                }

                GL.glPopMatrix();

        }


        protected void DynamicBallDraw(bool forShading = false)// this used to be a list, but it cant be 'static', the ball location values need to change...
        {
            if (forShading == true)
            {
                GL.glDisable(GL.GL_LIGHTING);
                GL.glColor3d(0.1f * 0.3f, 0.3f * 0.3f, 0.1f * 0.3f);
            }
            else
                GL.glColor3f(0.8f, 0.8f, 0.8f);

            GL.glPushMatrix();
                GL.glTranslatef(ball1._locationX, LightMove[1]*0.2f, ball1._locationY);
                if (!armBallAppearance)
                    GLUT.glutSolidSphere(0.25, 32, 16);
            GL.glPopMatrix();

        }
        protected void DrawFloor()
        {
            //Room Floor
            GL.glPushMatrix();


            GL.glColor3f(1.0f, 1.0f, 1.0f);
            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, textures[1]);
            GL.glBegin(GL.GL_QUADS);
            GL.glNormal3f(0.0f, 1.0f, 0.0f);
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3d(-16, -4, 15);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3d(-16, -4, -15);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3d(16, -4, -15);

            GL.glTexCoord2f(1.0f,0.0f);
            GL.glVertex3d(16, -4, 15);
            GL.glEnd();
            GL.glDisable(GL.GL_TEXTURE_2D);// we need to disable the texture to give color to the other parts


            GL.glPopMatrix();


            //createBox((float)-20, (float)20, (float)10, (float)-4.01, (float)10, (float)-10, 0.3f, 0.3f, 0.3f,true);
        }
        protected void DrawLights()
        {
            //enable lighting and set the light position
            GL.glEnable(GL.GL_LIGHTING);
            GL.glEnable(GL.GL_COLOR_MATERIAL);

            GL.glEnable(GL.GL_LIGHT0);
            float[] lightPosition0 = { 0f, 3f, 0f, 1f };
            float[] lightDiffuse0 = { 1f, 1f, 1f, 1f };  // White diffuse light
            float[] lightSpecular0 = { 1f, 1f, 1f, 1f };  // Specular highlights
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, lightDiffuse0);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, lightSpecular0);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, lightPosition0);
            pos[0] = lightPosition0[0];
            pos[1] = lightPosition0[1]+5;
            pos[2] = lightPosition0[2];

            //make a sphere that indicates the light0 position
            GL.glPushMatrix();
                GL.glDisable(GL.GL_LIGHTING);
                GL.glColor3f(1.0f, 1.0f, 0.0f);
                    GL.glTranslatef(lightPosition0[0], lightPosition0[1], lightPosition0[2]);
                    GLUT.glutSolidSphere(0.1, 8, 8);
                GL.glColor3f(1.0f, 1.0f, 1.0f);
                GL.glEnable(GL.GL_LIGHTING);
            GL.glPopMatrix();

        }
        protected void DrawTestObjects()
        {
            // grid
            GL.glPushMatrix();
            GL.glScalef(1, 0, 0);
            GLUT.glutWireCube(10);
            GL.glPopMatrix();

            GL.glPushMatrix();
            GL.glScalef(0, 0, 1);
            GLUT.glutWireCube(10);
            GL.glPopMatrix();

            GL.glPushMatrix();
            GL.glScalef(0, 1, 0);
            GLUT.glutWireCube(10);
            GL.glPopMatrix();

            for (int i = 0; i < 10; i++)
            {
                GL.glPushMatrix();
                //GL.glTranslatef(0, -1-i, 0);
                GL.glScalef(1 + i, 0.0f, 1 + i);
                GLUT.glutWireCube(2);
                GL.glPopMatrix();
            }

            // cubes
            GL.glColor3f(1.0f, 1.0f, 1.0f);
            createBox((float)-0.5, (float)0.5, (float)0.5, (float)-0.5, (float)-0.5, (float)0.5, 0.7f, 0.5f, 0.1f);
            createBox((float)-0.5 + 3, (float)0.5 + 3, (float)0.5, (float)-0.5, (float)-0.5, (float)0.5, 0.7f, 0.5f, 0.1f);

        }
        protected void DrawLightsAndCallLists()// sets light, and spheres
        {
            DrawLights();
            //DrawTestObjects();
            GL.glCallList(tableList);
            DrawFloor();
        }
        void DrawMirror()
        {
            GL.glEnable(GL.GL_LIGHTING);
            GL.glPushMatrix();

            GL.glRotatef(-90, 0, 1, 0);
            GL.glTranslatef(0f, 0.0f, -16f);
            GL.glBegin(GL.GL_QUADS);
            //!!! for blended REFLECTION 
            GL.glColor4d(1, 1, 1, 0.3);
            GL.glVertex3d(-3, -3, 0);
            GL.glVertex3d(-3, 3, 0);
            GL.glVertex3d(3, 3, 0);
            GL.glVertex3d(3, -3, 0);

            GL.glEnd();
            GL.glPopMatrix();

        }
        void DrawFigures()
        {
            GL.glPushMatrix();

            //draw figures start
            DrawLightsAndCallLists();
            GL.glTranslatef(0f, 0.25f, 0f);
            DynamicBallDraw();
            DrawQue();
            DrawArm();
            DrawShadows();
            //draw figures end

            GL.glPopMatrix();
        }

        //important camera stuff Start ------------------------------------------------------------------------------------------------------------------------------------------------------------
        public float cameraAngleY = 0.0f;
        public float cameraAngleX = 0.0f;
        public float cameraTranslateX = 0.0f;
        public float cameraTranslateY = 0.0f;
        //important camera stuff END ------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Draw()//camera control, and then calls DrawAll
        {

            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);
            GL.glLoadIdentity();


            GL.glPushMatrix();
            GL.glTranslatef(0f, 0.0f, -9.0f);
            GL.glRotatef(30, 1.0f, 0.0f, 0.0f);


            //important camera stuff Start ------------------------------------------------------------------------------------------------------------------------------------------------------------

            //camera orbit using the mouse Z
            /*            GL.glTranslatef(-(cameraTranslateX / 30), 0, -(cameraTranslateY / 30));
                        GL.glRotatef(cameraAngleY, 0.0f, 1.0f, 0.0f);
                        GL.glTranslatef((cameraTranslateX / 30), 0, (cameraTranslateY / 30));
            */

            float cosAngle = (float)Math.Cos(cameraAngleY * Math.PI / 180);
            float sinAngle = (float)Math.Sin(cameraAngleY * Math.PI / 180);

            //camera orbit using the mouse X and Z
            GL.glRotatef(cameraAngleY, 0.0f, 1.0f, 0.0f);
            GL.glRotatef(cameraAngleX/2, (float)(Math.Cos(cameraAngleY * Math.PI / 180)), 0.0f, (float)(Math.Sin(cameraAngleY * Math.PI / 180)));
            if (orbitBall)
                GL.glTranslatef((cameraTranslateX / 30), 0, (cameraTranslateY / 30));
            else
                GL.glTranslatef(-ball1._locationX, 0,-ball1._locationY);


            //camera translate using the middle mouse 
            //GL.glTranslatef( (float)Math.Cos(cameraAngleY * Math.PI / 180) * (cameraTranslateX / 30) - (float)Math.Sin(cameraAngleY * Math.PI / 180) * (cameraTranslateY / 30), 0f, (float)Math.Cos(cameraAngleY * Math.PI / 180) * (cameraTranslateY / 30) + (float)Math.Sin(cameraAngleY * Math.PI / 180) * (cameraTranslateX / 30));




            /*            GL.glTranslatef(
                            (float)Math.Cos(cameraAngleY * Math.PI / 180) * (cameraTranslateX / 30) - (float)Math.Sin(cameraAngleY * Math.PI / 180) * (cameraTranslateY / 30),
                            0f,
                            (float)Math.Cos(cameraAngleY * Math.PI / 180) * (cameraTranslateY / 30) + (float)Math.Sin(cameraAngleY * Math.PI / 180) * (cameraTranslateX / 30));*/
            /*
                        GL.glTranslatef(-1,0, 2);
                        GL.glRotatef(90, 0.0f, 1.0f, 0.0f);
                        GL.glTranslatef(2,0,1) ;
            */
            /*
                        GL.glTranslatef(-1,0, 2);
                        GL.glRotatef(180, 0.0f, 1.0f, 0.0f);
                        GL.glTranslatef(-1,0,2) ;
            */
            /*
                        GL.glTranslatef(-1,0, 2);
                        GL.glRotatef(270, 0.0f, 1.0f, 0.0f);
                        GL.glTranslatef(-2,0,-1);
            */

            /*            float testX = 1, testY = 3;

                                    cameraTranslateX = testX;
                                    cameraTranslateY = testY;

                                    GL.glTranslatef((cameraTranslateX / 30), 0, (cameraTranslateY / 30));
                                    GL.glRotatef(cameraAngleY, 0.0f, 1.0f, 0.0f);
                                    GL.glTranslatef(
                                        (cameraTranslateX / 30) * ((float)Math.Cos(cameraAngleY)) - (cameraTranslateY / 30) * ((float)Math.Sin(cameraAngleY))
                                        , 0,
                                        (cameraTranslateY / 30) * ((float)Math.Cos(cameraAngleY)) + (cameraTranslateX / 30) * ((float)Math.Sin(cameraAngleY))
                                        );
            */





            //important camera stuff END ------------------------------------------------------------------------------------------------------------------------------------------------------------


            //Shadows 
            pos[3] = 1.0f;
            ground[0, 1] = ground[1, 1] = ground[2, 1] = -0.2499f;
            //Shadows


            //DrawFloor();

            //REFLECTION b
            GL.glEnable(GL.GL_BLEND);
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);


            //only floor, draw only to STENCIL buffer
            GL.glEnable(GL.GL_STENCIL_TEST);
            GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE);
            GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF); // draw floor always
            GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
            GL.glDisable(GL.GL_DEPTH_TEST);

            DrawMirror();

            // restore regular settings
            GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
            GL.glEnable(GL.GL_DEPTH_TEST);

            // reflection is drawn only where STENCIL buffer value equal to 1
            GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
            GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);

            GL.glEnable(GL.GL_STENCIL_TEST);


            // draw reflected scene
            GL.glPushMatrix();
            GL.glTranslatef(32.0f, 0.0f, 0);
            GL.glScalef(-1, 1, 1); //swap on Z axis
            GL.glEnable(GL.GL_CULL_FACE);
            GL.glCullFace(GL.GL_BACK);
            DrawFigures();
            GL.glCullFace(GL.GL_FRONT);
            DrawFigures();
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();


            // really draw floor 
            //( half-transparent ( see its color's alpha byte)))
            // in order to see reflected objects 
            GL.glDepthMask((byte)GL.GL_FALSE);
            //DrawMirror();
            GL.glDepthMask((byte)GL.GL_TRUE);
            // Disable GL.GL_STENCIL_TEST to show All, else it will be cut on GL.GL_STENCIL
            GL.glDisable(GL.GL_STENCIL_TEST);
            DrawFigures();
            //REFLECTION e

            GL.glPopMatrix();
            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);

        }

		protected virtual void InitializeGL()
		{
			m_uint_HWND = (uint)p.Handle.ToInt32();
			m_uint_DC   = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
			// result in a failure to subsequently create the RC.
			WGL.wglSwapBuffers(m_uint_DC);

			WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
			WGL.ZeroPixelDescriptor(ref pfd);
			pfd.nVersion        = 1; 
			pfd.dwFlags         = (WGL.PFD_DRAW_TO_WINDOW |  WGL.PFD_SUPPORT_OPENGL |  WGL.PFD_DOUBLEBUFFER); 
			pfd.iPixelType      = (byte)(WGL.PFD_TYPE_RGBA);
			pfd.cColorBits      = 32;
			pfd.cDepthBits      = 32;
			pfd.iLayerType      = (byte)(WGL.PFD_MAIN_PLANE);

            //for Stencil support 

            pfd.cStencilBits = 32;


            int pixelFormatIndex = 0;
			pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
			if(pixelFormatIndex == 0)
			{
				MessageBox.Show("Unable to retrieve pixel format");
				return;
			}

			if(WGL.SetPixelFormat(m_uint_DC,pixelFormatIndex,ref pfd) == 0)
			{
				MessageBox.Show("Unable to set pixel format");
				return;
			}
			//Create rendering context
			m_uint_RC = WGL.wglCreateContext(m_uint_DC);
			if(m_uint_RC == 0)
			{
				MessageBox.Show("Unable to get rendering context");
				return;
			}
			if(WGL.wglMakeCurrent(m_uint_DC,m_uint_RC) == 0)
			{
				MessageBox.Show("Unable to make rendering context current");
				return;
			}


            initRenderingGL();
        }

        public void OnResize()
        {
            Width = p.Width;
            Height = p.Height;

            //!!!!!!!
            GL.glViewport(0, 0, Width, Height);
            //!!!!!!!
            
            initRenderingGL();
            Draw();
        }

        protected virtual void initRenderingGL()
		{
			if(m_uint_DC == 0 || m_uint_RC == 0)
				return;
			if(this.Width == 0 || this.Height == 0)
				return;

            //old that works:
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glClearColor(0, 0, 0, 0);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();
            GLU.gluPerspective(45.0, ((double)Width) / Height, 1.0, 1000.0);
            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();

            //InitTexture("floorTexture.bmp");
            InitTexture("fabrics_0075_color_1k.bmp");
            textures[0] = texture[0];
            InitTexture("floorTexture.bmp");
            textures[1] = texture[0];
            tableList = GL.glGenLists(1);
            CreateTableList();
        }

        /// <summary>
        /// Creates a box from the given 6 coordinates of xLeft,xRight,yTop,yBottom,zFront,zBack and RGB
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="color"></param>
        public void createBox(float xL, float xR, float yT, float yB, float zF, float zB, float R, float G, float B,bool flipNormals = false)
        {
            int normal = 1;
            if (flipNormals)
                normal *= -1;

            GL.glPushMatrix();

            GL.glColor3f(R, G, B);

            GL.glBegin(GL.GL_QUADS);
            {//purely aesthetic braces - LEFT
                GL.glNormal3d(-normal, 0, 0);
                GL.glVertex3f(xL, yB, zB);
                GL.glVertex3f(xL, yB, zF);
                GL.glVertex3f(xL, yT, zF);
                GL.glVertex3f(xL, yT, zB);
            }
            GL.glEnd();

            GL.glBegin(GL.GL_QUADS);
            {//purely aesthetic braces - TOP
                GL.glNormal3d(0, normal, 0);
                GL.glVertex3f(xL, yT, zB);
                GL.glVertex3f(xR, yT, zB);
                GL.glVertex3f(xR, yT, zF);
                GL.glVertex3f(xL, yT, zF);
            }
            GL.glEnd();

            GL.glBegin(GL.GL_QUADS);
            {//purely aesthetic braces - RIGHT
                GL.glNormal3d(normal, 0, 0);
                GL.glVertex3f(xR, yT, zB);
                GL.glVertex3f(xR, yT, zF);
                GL.glVertex3f(xR, yB, zF);
                GL.glVertex3f(xR, yB, zB);
            }
            GL.glEnd();

            GL.glBegin(GL.GL_QUADS);
            {//purely aesthetic braces - BACK
                GL.glNormal3d(0, 0, -normal);
                GL.glVertex3f(xL, yT, zB);
                GL.glVertex3f(xL, yB, zB);
                GL.glVertex3f(xR, yB, zB);
                GL.glVertex3f(xR, yT, zB);
            }
            GL.glEnd();

            GL.glBegin(GL.GL_QUADS);
            {//purely aesthetic braces - FRONT
                GL.glNormal3d(0, 0, normal);
                GL.glVertex3f(xL, yT, zF);
                GL.glVertex3f(xL, yB, zF);
                GL.glVertex3f(xR, yB, zF);
                GL.glVertex3f(xR, yT, zF);
            }
            GL.glEnd();
            
            GL.glBegin(GL.GL_QUADS);
            {//purely aesthetic braces - BOTTOM
                GL.glNormal3d(0, -normal, 0);
                GL.glVertex3f(xL, yB, zF);
                GL.glVertex3f(xR, yB, zF);
                GL.glVertex3f(xR, yB, zB);
                GL.glVertex3f(xL, yB, zB);
            }
            GL.glEnd();


            GL.glPopMatrix();
        }


        /// <summary>
        /// Creates a Cylinder from the given 3 coordinates of X,Y,Z and radius, sides and slice
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="radius"></param>
        /// <param name="sides"></param>
        /// <param name="howFull"></param>
        public void createCylinder(float x, float y, float z, float radius, float height = 1 ,uint sides = 64, float howFull = 1, float rotation = 0, bool cap = false, bool invertedCap = false, bool flipNormals = false)
        {
            int normals = -1;
            if (flipNormals == true)
                normals = 1;

            rotation = rotation * (float)Math.PI / 180;

            for (int i = 0; i < sides*howFull; i++)
            {
                GL.glBegin(GL.GL_QUADS);
                GL.glNormal3d(normals*(float)Math.Cos(((float)i / sides) * Math.PI * 2 + rotation), 0, normals*(float)Math.Sin(((float)i / sides) * Math.PI * 2 + rotation));
                GL.glVertex3f((float)Math.Cos(((float)i /sides)* Math.PI*2 + rotation) * radius+x, height +y, (float)Math.Sin(((float)i / sides) * Math.PI*2 + rotation) * radius + z);
                GL.glVertex3f((float)Math.Cos(((float)i /sides)* Math.PI*2 + rotation) * radius+x, 0f+y, (float)Math.Sin(((float)i / sides) * Math.PI*2 + rotation) * radius + z);
                GL.glVertex3f((float)Math.Cos(((float)(i+1)/sides) * Math.PI*2 + rotation) * radius+x, 0f+y, (float)Math.Sin(((float)(i + 1) / sides) * Math.PI*2 + rotation) * radius + z);
                GL.glVertex3f((float)Math.Cos(((float)(i+1)/sides) * Math.PI*2 + rotation) * radius+x, height + y, (float)Math.Sin(((float)(i + 1) / sides) * Math.PI*2 + rotation) * radius+z);
                GL.glEnd();

            }
            GL.glNormal3d(0,1,0);

            if (cap)
            {
                for (int i = 0; i < sides * howFull; i++)
                {
                    GL.glBegin(GL.GL_QUADS);
                    GL.glNormal3d(0, 1, 0);
                    GL.glVertex3f((float)Math.Cos(((float)i / sides) * Math.PI * 2 + rotation) * radius + x, height + y, (float)Math.Sin(((float)i / sides) * Math.PI * 2 + rotation) * radius + z);
                    GL.glVertex3f((float)Math.Cos(((float)(i + 1) / sides) * Math.PI * 2 + rotation) * radius + x, height + y, (float)Math.Sin(((float)(i + 1) / sides) * Math.PI * 2 + rotation) * radius + z);
                    GL.glVertex3f(x, height + y, z);
                    GL.glVertex3f(x, height + y, z);
                    GL.glEnd();

                }
            }


            float x1, x2, z1, z2;
            float dotX, dotZ;
            
/*
            if (false)// this is the privious inverter cap
                for (int i = 0; i < sides * howFull; i++)
                {
                    x1 = (float)Math.Cos(((float)i / sides) * Math.PI * 2 + rotation) * radius;
                    x2 = (float)Math.Cos(((float)(i + 1) / sides) * Math.PI * 2 + rotation) * radius;
                    z1 = (float)Math.Sin(((float)i / sides) * Math.PI * 2 + rotation) * radius;
                    z2 = (float)Math.Sin(((float)(i + 1) / sides) * Math.PI * 2 + rotation) * radius;

                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3f(x1 + x, height + y, z1 + z);
                    GL.glVertex3f(x2 + x, height + y, z2 + z);

                    if (i < sides * 0.25)
                    {
                        GL.glVertex3f(x + radius, height + y, z2 + z);
                        GL.glVertex3f(x + radius, height + y, z1 + z);
                    }
                    if (i >= sides * 0.25 && i < sides * 0.5)
                    {
                        GL.glVertex3f(x - radius, height + y, z2 + z);
                        GL.glVertex3f(x - radius, height + y, z1 + z);
                    }
                    if (i >= sides * 0.5 && i <= sides * 0.75)
                    {
                        GL.glVertex3f(x - radius, height + y, z2 + z);
                        GL.glVertex3f(x - radius, height + y, z1 + z);
                    }
                    if (i > sides * 0.75)
                    {
                        GL.glVertex3f(x + radius, height + y, z2 + z);
                        GL.glVertex3f(x + radius, height + y, z1 + z);
                    }

                    GL.glEnd();

                }
*/
            if (invertedCap)
            {
                for (int i = 0; i < sides * howFull; i++)
                {
                    x1 = (float)Math.Cos(((float)i / sides) * Math.PI * 2 + rotation) * radius;
                    x2 = (float)Math.Cos(((float)(i + 1) / sides) * Math.PI * 2 + rotation) * radius;
                    z1 = (float)Math.Sin(((float)i / sides) * Math.PI * 2 + rotation) * radius;
                    z2 = (float)Math.Sin(((float)(i + 1) / sides) * Math.PI * 2 + rotation) * radius;

                    dotZ = (float)Math.Sin( ((float)Math.PI / 2) * (Math.Floor((float)i / (sides/4))) + (Math.PI / 4) + rotation) * radius * 1.414213f;
                    dotX = (float)Math.Cos( ((float)Math.PI / 2) * (Math.Floor((float)i / (sides/4))) + (Math.PI / 4) + rotation) * radius * 1.414213f;


                    GL.glBegin(GL.GL_QUADS);
                    GL.glVertex3f(x1 + x, height + y, z1 + z);
                    GL.glVertex3f(x2 + x, height + y, z2 + z);
                    GL.glVertex3f(x + dotX, height + y, z + dotZ);
                    GL.glVertex3f(x + dotX, height + y, z + dotZ);
                    GL.glEnd();
                }

            }

        }


        // This is the Shadow Realm

        public float[] pos = new float[4];
        float[] planeCoeff = { 1, 1, 1, 1 };
        float[,] ground = new float[3, 3];//{ { -15, 3, 0 }, { 15, 3, 0 }, { 15, 3, 15 } };
        public float PlaneMoveValue = 0.0f;
        public float queRotation = 0.0f;
        public float quePower = 0;
        public bool queAppearance, armShadowAppearance = false, armBallAppearance = true;
        public float armRotation1 = 0, armRotation2 = 0, armHeight = -11.5f, clawOpening = 0;
        //public float armRotation1 = 45, armRotation2 = 94, armHeight = 0,clawOpening=0;
        public bool orbitBall = true;
        public float[] LightMove = new float[3];



        // Reduces a normal vector specified as a set of three coordinates,
        // to a unit normal vector of length one.
        void ReduceToUnit(float[] vector)
        {
            float length;

            // Calculate the length of the vector		
            length = (float)Math.Sqrt((vector[0] * vector[0]) +
                                (vector[1] * vector[1]) +
                                (vector[2] * vector[2]));

            // Keep the program from blowing up by providing an exceptable
            // value for vectors that may calculated too close to zero.
            if (length == 0.0f)
                length = 1.0f;

            // Dividing each element by the length will result in a
            // unit normal vector.
            vector[0] /= length;
            vector[1] /= length;
            vector[2] /= length;

            // Alon Change!! to flip the vector
            vector[0] *= -1;
            vector[1] *= -1;
            vector[2] *= -1;

        }

        const int x = 0;
        const int y = 1;
        const int z = 2;

        // Points p1, p2, & p3 specified in counter clock-wise order
        void calcNormal(float[,] v, float[] outp)
        {
            float[] v1 = new float[3];
            float[] v2 = new float[3];

            // Calculate two vectors from the three points
            v1[x] = v[0, x] - v[1, x];
            v1[y] = v[0, y] - v[1, y];
            v1[z] = v[0, z] - v[1, z];

            v2[x] = v[1, x] - v[2, x];
            v2[y] = v[1, y] - v[2, y];
            v2[z] = v[1, z] - v[2, z];

            // Take the cross product of the two vectors to get
            // the normal vector which will be stored in out
            outp[x] = v1[y] * v2[z] - v1[z] * v2[y];
            outp[y] = v1[z] * v2[x] - v1[x] * v2[z];
            outp[z] = v1[x] * v2[y] - v1[y] * v2[x];

            // Normalize the vector (shorten length to one)
            ReduceToUnit(outp);
        }

        float[] cubeXform = new float[16];

        // Creates a shadow projection matrix out of the plane equation
        // coefficients and the position of the light. The return value is stored
        // in cubeXform[,]
        void MakeShadowMatrix(float[,] points)
        {
            float[] planeCoeff = new float[4];
            float dot;

            // Find the plane equation coefficients
            // Find the first three coefficients the same way we
            // find a normal.
            calcNormal(points, planeCoeff);

            // Find the last coefficient by back substitutions
            planeCoeff[3] = -(
                (planeCoeff[0] * points[2, 0]) + (planeCoeff[1] * points[2, 1]) +
                (planeCoeff[2] * points[2, 2]));


            // Dot product of plane and light position
            dot = planeCoeff[0] * pos[0] +
                    planeCoeff[1] * pos[1] +
                    planeCoeff[2] * pos[2] +
                    planeCoeff[3];

            // Now do the projection
            // First column
            cubeXform[0] = dot - pos[0] * planeCoeff[0];
            cubeXform[4] = 0.0f - pos[0] * planeCoeff[1];
            cubeXform[8] = 0.0f - pos[0] * planeCoeff[2];
            cubeXform[12] = 0.0f - pos[0] * planeCoeff[3];

            // Second column
            cubeXform[1] = 0.0f - pos[1] * planeCoeff[0];
            cubeXform[5] = dot - pos[1] * planeCoeff[1];
            cubeXform[9] = 0.0f - pos[1] * planeCoeff[2];
            cubeXform[13] = 0.0f - pos[1] * planeCoeff[3];

            // Third Column
            cubeXform[2] = 0.0f - pos[2] * planeCoeff[0];
            cubeXform[6] = 0.0f - pos[2] * planeCoeff[1];
            cubeXform[10] = dot - pos[2] * planeCoeff[2];
            cubeXform[14] = 0.0f - pos[2] * planeCoeff[3];

            // Fourth Column
            cubeXform[3] = 0.0f - pos[3] * planeCoeff[0];
            cubeXform[7] = 0.0f - pos[3] * planeCoeff[1];
            cubeXform[11] = 0.0f - pos[3] * planeCoeff[2];
            cubeXform[15] = dot - pos[3] * planeCoeff[3];
        }
        //Shadows

        void DrawShadows()//this function is basically above all (shadowwise), it draws the planes, the objects and the shadows
        {

            //SHADING begin
            //we'll define cubeXform matrix in MakeShadowMatrix Sub
            // Disable lighting, we'll just draw the shadow
            //else instead of shadow we'll see stange projection of the same objects

            // wall shadow
            //!!!!!!!!!!!!!
            GL.glPushMatrix();
            //!!!!!!!!!!!!       
            MakeShadowMatrix(ground);
            GL.glMultMatrixf(cubeXform);
            //ObjectColor(true, 1);
            DynamicBallDraw(true);
            DrawQue(true);
            DrawArm(true);
            //!!!!!!!!!!!!!
            GL.glPopMatrix();
            //!!!!!!!!!!!!!
        }



        //---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//TEXTURE

        public uint[] texture;
        uint[] textures = new uint[2]; 
        void InitTexture(string imageBMPfile)
        {
            GL.glEnable(GL.GL_TEXTURE_2D);

            texture = new uint[1];// storage for texture

            Bitmap image = new Bitmap(imageBMPfile);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            GL.glGenTextures(1, texture);
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[0]);
            //  VN-in order to use System.Drawing.Imaging.BitmapData Scan0 I've added overloaded version to
            //  OpenGL.cs
            //  [DllImport(GL_DLL, EntryPoint = "glTexImage2D")]
            //  public static extern void glTexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering

            image.UnlockBits(bitmapdata);
            image.Dispose();
        }


    }
}
