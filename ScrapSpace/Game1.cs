using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ScrapSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Variables de coordenadas
        Vector2 coordes_cartel;
        Vector2[] coordes_Asteroide;
        Vector2[] coordes_basura;
        Vector2 coordes_adorno;

        //Objetos
        Fondo[] fondo;
        Scrapy nave;
        Asteroide[] asteroide;
        Basura[] basura;
        Adorno[] adorno;

        //Variables de colisión
        BoundingSphere[] circunferencia_Asteroide;
        BoundingBox[] perimetro_basura;
        BoundingBox perimetro_nave;

        //
        Texture2D texturaNave;

        int mapa;
        int[] mapa_asteroide;
        int[] mapa_basura;
        int[] mapa_adorno;
        int i, j, k;
        int cantidad_de_asteroides=10;
        int cantidad_de_basuras = 6;
        int cantidad_de_adornos = 3;

        //Variables de mensajes de texto
        SpriteFont fuente1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "../../Content";
            this.graphics.PreferredBackBufferWidth = 640; //X
            this.graphics.PreferredBackBufferHeight = 480; //Y
        }


        protected override void Initialize()
        {
            mapa = 3;
            coordes_cartel = new Vector2(0,455);
            //fondos
            fondo = new Fondo[6];
            for(int i=0;i<6;i++) fondo[i] = new Fondo(graphics,Vector2.Zero);
            //Control mapa del asteroide y asteroide
            mapa_asteroide = new int[cantidad_de_asteroides];
            asteroide = new Asteroide[cantidad_de_asteroides];
            coordes_Asteroide = new Vector2[cantidad_de_asteroides];
            circunferencia_Asteroide = new BoundingSphere[cantidad_de_asteroides];

            for (int i = 0; i < cantidad_de_asteroides; i++)
            {
                asteroide[i] = new Asteroide(graphics);
                asteroide[i].inicial(ref mapa_asteroide[i], ref coordes_Asteroide[i]);
            }
            //Basura
            basura = new Basura[cantidad_de_basuras];
            coordes_basura = new Vector2[cantidad_de_basuras];
            perimetro_basura = new BoundingBox[cantidad_de_basuras];
            mapa_basura = new int[cantidad_de_basuras];

            for (int i = 0; i < cantidad_de_basuras; i++)
            {
                basura[i] = new Basura(graphics);
                mapa_basura[i] = i;
                basura[i].inicial(ref mapa_basura[i], ref coordes_basura[i]);
            }
            //Adornos
            adorno = new Adorno[cantidad_de_adornos];
            coordes_adorno = new Vector2(200, 240);
            mapa_adorno = new int[cantidad_de_adornos];

            for (int i = 0; i < cantidad_de_adornos; i++)
            {
                adorno[i] = new Adorno(graphics);
            }
            mapa_adorno[0] = 3;
            mapa_adorno[1] = 4;
            mapa_adorno[2] = 2;
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Fuente
            //fuente1 = Content.Load<SpriteFont>("Fuentes/Fuente1");
            //Fondo o mapa
            fondo[0].cargarImagen(Content, "Graficos/Fondo1");
            fondo[1].cargarImagen(Content, "Graficos/Fondo2");
            fondo[2].cargarImagen(Content, "Graficos/Fondo3");
            fondo[3].cargarImagen(Content, "Graficos/Fondo4");
            fondo[4].cargarImagen(Content, "Graficos/Fondo5");
            fondo[5].cargarImagen(Content, "Graficos/Fondo6");
            //Nave
            texturaNave = Content.Load<Texture2D>("Graficos/Scrapy01");
            nave = new Scrapy(texturaNave, mapa);
            //Asteriodes
            asteroide[0].cargarImagen(Content, "Graficos/Meteoro0");
            asteroide[1].cargarImagen(Content, "Graficos/Meteoro1");
            asteroide[2].cargarImagen(Content, "Graficos/Meteoro2");
            asteroide[3].cargarImagen(Content, "Graficos/Meteoro3");
            asteroide[4].cargarImagen(Content, "Graficos/Meteoro4");
            asteroide[5].cargarImagen(Content, "Graficos/Meteoro5");
            asteroide[6].cargarImagen(Content, "Graficos/Meteoro6");
            asteroide[7].cargarImagen(Content, "Graficos/Meteoro0");
            asteroide[8].cargarImagen(Content, "Graficos/Meteoro1");
            asteroide[9].cargarImagen(Content, "Graficos/Meteoro2");
            //Basura
            basura[0].cargarImagen(Content, "Graficos/Basura1");
            basura[1].cargarImagen(Content, "Graficos/Basura2");
            basura[2].cargarImagen(Content, "Graficos/Basura3");
            basura[3].cargarImagen(Content, "Graficos/Basura4");
            basura[4].cargarImagen(Content, "Graficos/Basura5");
            basura[5].cargarImagen(Content, "Graficos/Basura6");
            //Adornos
            adorno[0].cargarImagen(Content, "Graficos/Adorno1");
            adorno[1].cargarImagen(Content, "Graficos/Adorno2");
            adorno[2].cargarImagen(Content, "Graficos/Adorno3");

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState teclado = Keyboard.GetState();

            //Salida
            if (teclado.IsKeyDown(Keys.Escape)) this.Exit();
            
            //Defino los perímetros para colisión
            nave.calcula_perimetro(ref perimetro_nave);
            for (i = 0; i < cantidad_de_asteroides; i++)
            {
                asteroide[i].calcular_circunferencia(ref circunferencia_Asteroide[i], ref coordes_Asteroide[i]);
            }

            //Verifico colisión
            for (i = 0; i < cantidad_de_asteroides; i++)
            {
                if (perimetro_nave.Intersects(circunferencia_Asteroide[i])) // Explosión
                {

                }
                for (j = 0; j < cantidad_de_asteroides; j++)
                    if (j != i)
                    {
                        if (circunferencia_Asteroide[i].Intersects(circunferencia_Asteroide[j])) //Colición
                        {
                            k = asteroide[i].direccion;
                            asteroide[i].direccion = asteroide[j].direccion;
                            asteroide[j].direccion = k;
                            asteroide[i].sentido_de_giro *= -1;
                            asteroide[j].sentido_de_giro *= -1;
                        }
                        if (circunferencia_Asteroide[i].Contains(circunferencia_Asteroide[j]) == ContainmentType.Contains) //Colición
                        {

                            asteroide[i].inicial(ref mapa_asteroide[i], ref coordes_Asteroide[i]);
                            //asteroide[j].inicial(ref mapa_asteroide[j], ref coordes_Asteroide[j]);

                        }
                    }
            }

            //Movimiento nave
            nave.Update(teclado);
            nave.cambiamapa(ref mapa);
            //Movimiento de los asteroides
            for (i = 0; i < cantidad_de_asteroides; i++)
            {
                asteroide[i].desplazar(ref coordes_Asteroide[i]);
                asteroide[i].cambiamapa(ref mapa_asteroide[i], ref coordes_Asteroide[i]);
            }
            //Movimiento basura
            for (i = 0; i < cantidad_de_basuras; i++)
            {
                basura[i].desplazar(ref coordes_basura[i]);
                basura[i].cambiamapa(ref mapa_basura[i], ref coordes_basura[i]);
            }
            //Movimiento de Adornos
            for (i = 0; i < cantidad_de_adornos; i++)
            {
                adorno[i].rotacion();
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Fondos
            if (mapa == 1) fondo[0].dibujar();
            if (mapa == 2) fondo[1].dibujar();
            if (mapa == 3) fondo[2].dibujar();
            if (mapa == 4) fondo[3].dibujar();
            if (mapa == 5) fondo[4].dibujar();
            if (mapa == 6) fondo[5].dibujar();

            spriteBatch.Begin();
            //Información
            //spriteBatch.DrawString(fuente1, "Mapa: " + mapa, coordes_cartel, Color.GreenYellow);
            //Adornos
            for (i = 0; i < cantidad_de_adornos; i++)
            {
                if (mapa == mapa_adorno[i]) adorno[i].dibujar(ref coordes_adorno);
            }
            //Asteroides
            for (i = 0; i < cantidad_de_asteroides; i++)
            {
                if (mapa == mapa_asteroide[i]) asteroide[i].dibujar(ref coordes_Asteroide[i]);
            }
            //Basura espacial
            for (i = 0; i < cantidad_de_basuras; i++)
            {
                if (mapa == mapa_basura[i]) basura[i].dibujar(ref coordes_basura[i]);
            }
            //Nave
            nave.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
