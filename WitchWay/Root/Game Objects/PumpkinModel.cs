﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WitchWay
{
    public class PumpkinModel
    {
        Model m_model;
        Vector3 m_position;
        float m_angle;

        public PumpkinModel(ContentManager content)
        {
            m_model = content.Load<Model>("pumpkinModel");
            m_position = new Vector3(0, 0, -50);
            m_angle = 0;
        }

        public void Update(GameTime gameTime)
        {
            m_angle += 0.01f;
        }

        public void Draw(GraphicsDevice graphics)
        {
            // Important piece of code that enables depth buffer for 3d objects
            graphics.DepthStencilState = new DepthStencilState()
            {
                DepthBufferEnable = true
            };

            DrawModel(graphics, m_model, m_position, m_angle);

            graphics.DepthStencilState = new DepthStencilState()
            {
                DepthBufferEnable = false
            };
        }

        // Creates a helper function that allows us to easily draw models
        private void DrawModel(GraphicsDevice graphics, Model model, Vector3 position, float angle=0)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                   
                    effect.World = Matrix.CreateTranslation(position) * Matrix.CreateScale(0.03f) * Matrix.CreateRotationX(MathHelper.ToRadians(-90)) * Matrix.CreateRotationY(m_angle);

                    // Move the camera 10 units away from the origin:
                    var cameraPosition = new Vector3(0, 0, 10);
                    // Tell the camera to look at the origin:
                    var cameraLookAtVector = Vector3.Zero;
                    // Tell the camera that positive Z is up
                    var cameraUpVector = Vector3.UnitY;

                    effect.View = Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector);

                    // We want the aspect ratio of our display to match
                    // the entire screen's aspect ratio:
                    float aspectRatio = graphics.Viewport.AspectRatio;
                   
                    float fieldOfView = MathHelper.PiOver4;
                    float nearClipPlane = 1;
                    float farClipPlane = 200;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }

                mesh.Draw();
            }
        }
    }
}
