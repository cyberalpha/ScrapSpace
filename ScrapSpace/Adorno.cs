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
    class Adorno
    {
        //Atributos
        Texture2D imagen_adorno; //Variable para la imagen de basura.
        public Vector2 coordes_adorno; //Variable de la posición en donde se ubicará la imagen.
        SpriteBatch spriteBatch; //Variable para dibujar en la pantalla.
        float rotar;
        public float sentido_de_giro = 1;
        public int mapa_adorno;
        Random r = new Random();

        //Contructor
        public Adorno(GraphicsDeviceManager graficos)
        {
            this.spriteBatch = new SpriteBatch(graficos.GraphicsDevice); //Instancio spritebacth
        }
        //Métodos
        //Método de Cargar
        public void cargarImagen(ContentManager contenido, String nombreImagen)
        {
            this.imagen_adorno = contenido.Load<Texture2D>(nombreImagen);
        }
        //Método de inicialización
        public void inicial(ref int mapa_adorno, ref Vector2 coordes_adorno)
        {
            this.mapa_adorno = mapa_adorno;

        }
        //Método rotar
        public void rotacion()
        {
            sentido_de_giro = r.Next(-1,1);
            rotar += 0.55f * sentido_de_giro;
         }
        //Método de Dibujar
        public void dibujar(ref Vector2 coordes_adorno)
        {       
            spriteBatch.Begin();
            spriteBatch.Draw(imagen_adorno, coordes_adorno, null, Color.White,
    MathHelper.ToRadians(rotar),
    new Vector2(imagen_adorno.Width / 2, imagen_adorno.Height / 2), 1,
        SpriteEffects.None, 0);
            spriteBatch.End();
        }

    }
}
