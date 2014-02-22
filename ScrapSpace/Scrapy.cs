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
    class Scrapy
    {
        Texture2D textura;
        Vector2 posicion;
        Vector2 velocidad;//Velocidad de la nave
        //Viewport pantalla;
        float rotacion = 0.0f;
        int mapaactual;
        
        public Scrapy(Texture2D nave, int _mapaactual/*, Vector2 posicion*/)
        {
            textura = nave;
            posicion = new Vector2(320,240);
            //this.pantalla = pantalla;
            velocidad = Vector2.Zero;
            mapaactual = _mapaactual;

        }
        public void Update(KeyboardState teclado) {
            if (teclado.IsKeyDown(Keys.Up))
            {
                Vector2 direccionNave = Vector2.Zero;

                //calculamos la direccion de hacia la que apunta la avioneta

                direccionNave.X = (float)Math.Sin(rotacion);
                direccionNave.Y = -(float)Math.Cos(rotacion);
                //Velocidad de desplazamiento
                velocidad = velocidad + direccionNave *new Vector2(0.05f,0.05f);
            }
            if (teclado.IsKeyDown(Keys.Left))
            {
                rotacion -= 0.01f;
            }
            if (teclado.IsKeyDown(Keys.Right))
            {
                rotacion += 0.01f;
            }

            posicion += velocidad;
            //velocidad *= 0.90f;
            velocidad *= 0.98f;
        }
        //Método que controla cambio de mapa
        public void cambiamapa(ref int mapaactual)
        {
            if (posicion.X <= 0)
            {
                if (mapaactual == 3) mapaactual = 2;//Oeste
                else if (mapaactual == 2) mapaactual = 4;
                else if (mapaactual == 4) mapaactual = 5;
                else if (mapaactual == 5) mapaactual = 3;
                else if (mapaactual == 6) mapaactual = 2;
                else if (mapaactual == 1) mapaactual = 2;
                posicion.X = 640;
            }
            else if (posicion.Y <= 0)
            {
                if (mapaactual == 3) mapaactual = 6; //Norte
                else if (mapaactual == 6) mapaactual = 4;
                else if (mapaactual == 4) mapaactual = 1;
                else if (mapaactual == 1) mapaactual = 3;
                else if (mapaactual == 5) mapaactual = 6;
                else if (mapaactual == 2) mapaactual = 6;
                posicion.Y = 480;
            }
            else if (posicion.X >= 640)
            {
                if (mapaactual == 3) mapaactual = 5;//Este
                else if (mapaactual == 5) mapaactual = 4;
                else if (mapaactual == 4) mapaactual = 2;
                else if (mapaactual == 2) mapaactual = 3;
                else if (mapaactual == 6) mapaactual = 5;
                else if (mapaactual == 1) mapaactual = 5;
                posicion.X = 0;
            }
            else if (posicion.Y >= 480)
            {
                if (mapaactual == 3) mapaactual = 1; //Sur
                else if (mapaactual == 1) mapaactual = 4;
                else if (mapaactual == 4) mapaactual = 6;
                else if (mapaactual == 6) mapaactual = 3;
                else if (mapaactual == 2) mapaactual = 1;
                else if (mapaactual == 5) mapaactual = 1;
                posicion.Y = 0;
            }
        }
        //Metodo que calcula el perímetro para la colición
        public void calcula_perimetro(ref BoundingBox perimetro_nave)
        {
            perimetro_nave = new BoundingBox(new Vector3(posicion, 0),
                    new Vector3(textura.Width + posicion.X,
                             textura.Height + posicion.Y, 0));
        }
        //Método para dibujar
        public void Draw(SpriteBatch batch) 
        {

            batch.Draw(textura, posicion, null, Color.White,
            rotacion, new Vector2(textura.Width/2,textura.Height/2), 1, SpriteEffects.None, 0);
        }
    }
}
