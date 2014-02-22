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
    class Basura
    {
        //Atributos
        Texture2D imagen_basura; //Variable para la imagen de basura.
        public Vector2 coordes_basura; //Variable de la posición en donde se ubicará la imagen.
        SpriteBatch spriteBatch; //Variable para dibujar en la pantalla.
        float rotar;
        public float sentido_de_giro = 1;
        int rnd;
        public int direccion;
        public int mapa_basura;
        float desplazamiento = 0.35f;
        //BoundingSphere circunferencia_asteroide;
        Random r = new Random();

        //Contructor
        public Basura(GraphicsDeviceManager graficos)
        {
            this.spriteBatch = new SpriteBatch(graficos.GraphicsDevice); //Instancio spritebacth
        }
        //Métodos
        //Método de Cargar
        public void cargarImagen(ContentManager contenido, String nombreImagen)
        {
            this.imagen_basura = contenido.Load<Texture2D>(nombreImagen);
        }
        //Método de inicialización
        public void inicial(ref int mapa_basura, ref Vector2 coordes_basura)
        {
            this.mapa_basura = mapa_basura;
            rnd = r.Next(1,4);
            switch (rnd)
            {
                case 1://Derecha
                    {
                        coordes_basura.X = 1;
                        coordes_basura.Y = r.Next(1, 479);
                        direccion = 1;
                        //mapa_basura = r.Next(1, 6);
                        break;
                    }
                case 2://Izquieda
                    {
                        coordes_basura.X = 559;
                        coordes_basura.Y = r.Next(1, 479);
                        direccion = 2;
                        //mapa_basura = r.Next(1, 6);
                        break;
                    }
                case 3://Arriba
                    {
                        coordes_basura.X = r.Next(1, 639);
                        coordes_basura.Y = 524;
                        direccion = 3;
                        //mapa_basura = r.Next(1, 6);
                        break;
                    }
                case 4://Abajo
                    {
                        coordes_basura.X = r.Next(1, 639);
                        coordes_basura.Y = 1;
                        direccion = 4;
                        //mapa_basura = r.Next(1, 6);
                        break;
                    }
            }
        }
        //Método de desplazar
        public void desplazar(ref Vector2 coordes_basura)
        {
            rotar += 0.4f * sentido_de_giro;
            switch (direccion)
            {
                case 1: //derecha
                    {
                        coordes_basura.X +=desplazamiento;
                        break;
                    }
                case 2: //izquierda
                    {
                        coordes_basura.X -=desplazamiento;
                        break;
                    }
                case 3: // arriba
                    {
                        coordes_basura.Y -=desplazamiento;
                        break;
                    }
                case 4: // abajo
                    {
                        coordes_basura.Y +=desplazamiento;
                        break;
                    }

            }
        }
        //Método para cambiar de mapa tipo dado
        public void cambiamapa(ref int mapa_basura, ref Vector2 coordes_basura)
        {

            if (coordes_basura.X <= 0)
            {
                if (mapa_basura == 3) mapa_basura = 2;//Oeste
                else if (mapa_basura == 2) mapa_basura = 4;
                else if (mapa_basura == 4) mapa_basura = 5;
                else if (mapa_basura == 5) mapa_basura = 3;
                else if (mapa_basura == 6) mapa_basura = 2;
                else if (mapa_basura == 1) mapa_basura = 2;
                coordes_basura.X = 639;
            }
            else if (coordes_basura.Y <= 0)
            {
                if (mapa_basura == 3) mapa_basura = 6; //Norte
                else if (mapa_basura == 6) mapa_basura = 4;
                else if (mapa_basura == 4) mapa_basura = 1;
                else if (mapa_basura == 1) mapa_basura = 3;
                else if (mapa_basura == 5) mapa_basura = 6;
                else if (mapa_basura == 2) mapa_basura = 6;
                coordes_basura.Y = 479;
            }
            else if (coordes_basura.X >= 640)
            {
                if (mapa_basura == 3) mapa_basura = 5;//Este
                else if (mapa_basura == 5) mapa_basura = 4;
                else if (mapa_basura == 4) mapa_basura = 2;
                else if (mapa_basura == 2) mapa_basura = 3;
                else if (mapa_basura == 6) mapa_basura = 5;
                else if (mapa_basura == 1) mapa_basura = 5;
                coordes_basura.X = 1;
            }
            else if (coordes_basura.Y >= 480)
            {
                if (mapa_basura == 3) mapa_basura = 1; //Sur
                else if (mapa_basura == 1) mapa_basura = 4;
                else if (mapa_basura == 4) mapa_basura = 6;
                else if (mapa_basura == 6) mapa_basura = 3;
                else if (mapa_basura == 2) mapa_basura = 1;
                else if (mapa_basura == 5) mapa_basura = 1;
                coordes_basura.Y = 1;
            }
        }
        //Método calcular circunferencia para colición
        public void calcular_circunferencia (ref BoundingSphere circunferencia_basura, ref Vector2 coordes_basura)
        {
            circunferencia_basura = new BoundingSphere(new Vector3(new Vector2(imagen_basura.Width / 2, imagen_basura.Height / 2) + coordes_basura, 0), imagen_basura.Width / 2);
        }
        //Método de Dibujar
        public void dibujar(ref Vector2 coordes_basura)
        {       
            spriteBatch.Begin();
            spriteBatch.Draw(imagen_basura, coordes_basura, null, Color.White,
    MathHelper.ToRadians(rotar),
    new Vector2(imagen_basura.Width / 2, imagen_basura.Height / 2), 1,
        SpriteEffects.None, 0);
            spriteBatch.End();
        }

    }
}
