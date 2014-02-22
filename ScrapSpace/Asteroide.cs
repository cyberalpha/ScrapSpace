using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ScrapSpace
{
    class Asteroide
    {
        //Atributos
        Texture2D imagen_Asteroide; //Variable para la imagen de fondo.
        public Vector2 coordes_Asteroide; //Variable de la posición en donde se ubicará la imagen.
        SpriteBatch spriteBatch; //Variable para dibujar en la pantalla.
        float rotar;
        public float sentido_de_giro = 1;
        int rnd;
        public int direccion;
        //int mapa_Asteroide;
        float desplazamiento = 0.40f;
        //BoundingSphere circunferencia_asteroide;
        Random r = new Random();

        //Contructor
        public Asteroide(GraphicsDeviceManager graficos)
        {
            this.spriteBatch = new SpriteBatch(graficos.GraphicsDevice); //Instancio spritebacth
        }
        //Métodos
        //Método de Cargar
        public void cargarImagen(ContentManager contenido, String nombreImagen)
        {
            this.imagen_Asteroide = contenido.Load<Texture2D>(nombreImagen);
        }
        //Método de inicialización
        public void inicial(ref int mapa_Asteroide, ref Vector2 coordes_Asteroide)
        {
            rnd = r.Next(1,4);
            switch (rnd)
            {
                case 1://Derecha
                    {
                        coordes_Asteroide.X = 1;
                        coordes_Asteroide.Y = r.Next(1, 479);
                        direccion = 1;
                        mapa_Asteroide = r.Next(1, 6);
                        break;
                    }
                case 2://Izquieda
                    {
                        coordes_Asteroide.X = 559;
                        coordes_Asteroide.Y = r.Next(1, 479);
                        direccion = 2;
                        mapa_Asteroide = r.Next(1, 6);
                        break;
                    }
                case 3://Arriba
                    {
                        coordes_Asteroide.X = r.Next(1, 639);
                        coordes_Asteroide.Y = 524;
                        direccion = 3;
                        mapa_Asteroide = r.Next(1, 6);
                        break;
                    }
                case 4://Abajo
                    {
                        coordes_Asteroide.X = r.Next(1, 639);
                        coordes_Asteroide.Y = 1;
                        direccion = 4;
                        mapa_Asteroide = r.Next(1, 6);
                        break;
                    }
            }
        }
        //Método de desplazar
        public void desplazar(ref Vector2 coordes_Asteroide)
        {
            rotar += 0.5f * sentido_de_giro;
            switch (direccion)
            {
                case 1: //derecha
                    {
                        coordes_Asteroide.X +=desplazamiento;
                        break;
                    }
                case 2: //izquierda
                    {
                        coordes_Asteroide.X -=desplazamiento;
                        break;
                    }
                case 3: // arriba
                    {
                        coordes_Asteroide.Y -=desplazamiento;
                        break;
                    }
                case 4: // abajo
                    {
                        coordes_Asteroide.Y +=desplazamiento;
                        break;
                    }

            }
        }
        //Método para cambiar de mapa tipo dado
        public void cambiamapa(ref int mapa_Asteroide, ref Vector2 coordes_Asteroide)
        {

            if (coordes_Asteroide.X <= 0)
            {
                if (mapa_Asteroide == 3) mapa_Asteroide = 2;//Oeste
                else if (mapa_Asteroide == 2) mapa_Asteroide = 4;
                else if (mapa_Asteroide == 4) mapa_Asteroide = 5;
                else if (mapa_Asteroide == 5) mapa_Asteroide = 3;
                else if (mapa_Asteroide == 6) mapa_Asteroide = 2;
                else if (mapa_Asteroide == 1) mapa_Asteroide = 2;
                coordes_Asteroide.X = 639;
            }
            else if (coordes_Asteroide.Y <= 0)
            {
                if (mapa_Asteroide == 3) mapa_Asteroide = 6; //Norte
                else if (mapa_Asteroide == 6) mapa_Asteroide = 4;
                else if (mapa_Asteroide == 4) mapa_Asteroide = 1;
                else if (mapa_Asteroide == 1) mapa_Asteroide = 3;
                else if (mapa_Asteroide == 5) mapa_Asteroide = 6;
                else if (mapa_Asteroide == 2) mapa_Asteroide = 6;
                coordes_Asteroide.Y = 479;
            }
            else if (coordes_Asteroide.X >= 640)
            {
                if (mapa_Asteroide == 3) mapa_Asteroide = 5;//Este
                else if (mapa_Asteroide == 5) mapa_Asteroide = 4;
                else if (mapa_Asteroide == 4) mapa_Asteroide = 2;
                else if (mapa_Asteroide == 2) mapa_Asteroide = 3;
                else if (mapa_Asteroide == 6) mapa_Asteroide = 5;
                else if (mapa_Asteroide == 1) mapa_Asteroide = 5;
                coordes_Asteroide.X = 1;
            }
            else if (coordes_Asteroide.Y >= 480)
            {
                if (mapa_Asteroide == 3) mapa_Asteroide = 1; //Sur
                else if (mapa_Asteroide == 1) mapa_Asteroide = 4;
                else if (mapa_Asteroide == 4) mapa_Asteroide = 6;
                else if (mapa_Asteroide == 6) mapa_Asteroide = 3;
                else if (mapa_Asteroide == 2) mapa_Asteroide = 1;
                else if (mapa_Asteroide == 5) mapa_Asteroide = 1;
                coordes_Asteroide.Y = 1;
            }
        }
        //Método calcular circunferencia para colición
        public void calcular_circunferencia (ref BoundingSphere circunferencia_asteroide, ref Vector2 coordes_Asteroide)
        {
            circunferencia_asteroide = new BoundingSphere(new Vector3(new Vector2(imagen_Asteroide.Width / 2, imagen_Asteroide.Height / 2) + coordes_Asteroide, 0), imagen_Asteroide.Width / 2);
        }
        //Método de Dibujar
        public void dibujar(ref Vector2 coordes_Asteroide)
        {       
            spriteBatch.Begin();
            spriteBatch.Draw(imagen_Asteroide, coordes_Asteroide, null, Color.White,
    MathHelper.ToRadians(rotar),
    new Vector2(imagen_Asteroide.Width / 2, imagen_Asteroide.Height / 2), 1,
        SpriteEffects.None, 0);
            spriteBatch.End();
        }

    }
}