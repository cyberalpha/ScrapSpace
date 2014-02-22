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
    class Fondo
    {
        //Atributos
        Texture2D imagen_Fondo; //Variable para la imagen de fondo.
        Vector2 coordes_Fondo; //Variable de la posición en donde se ubicará la imagen.
        SpriteBatch spriteBatch; //Variable para dibujar en la pantalla.

        //Contructor
        public Fondo(GraphicsDeviceManager graficos, Vector2 _posicion)
        {
            this.coordes_Fondo = _posicion;
            this.spriteBatch = new SpriteBatch(graficos.GraphicsDevice); //Instancio spritebacth
        }
        //Métodos
        //Método de Cargar
        public void cargarImagen(ContentManager contenido, String nombreImagen)
        {
            this.imagen_Fondo = contenido.Load<Texture2D>(nombreImagen);
        }
        //Método de Dibujar
        public void dibujar()
        {       
            spriteBatch.Begin();
            spriteBatch.Draw(imagen_Fondo, coordes_Fondo, null, Color.White);
            spriteBatch.End();
        }
        //elegir mapa

    }
}
