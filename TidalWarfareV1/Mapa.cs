using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static TidalWarfareV1.Program;

namespace TidalWarfareV1
{

    /// <summary>
    /// Representa el formulario principal donde se desarrolla la partida del juego Tidal Warfare.
    /// </summary>
    public partial class Mapa : Form
    {
        // Listas de objetos gráficos que representan los barcos, rocas y minas
        private List<ObjetoGrafico> RocaLimite = new List<ObjetoGrafico>();
        private List<ObjetoGrafico> BombaLimite = new List<ObjetoGrafico>();// Lista de objetos para verificar colisiones
       
        Tablero tablero = new Tablero();
        private Navio navio1;
        private Navio navio2;
        private Dictionary<Keys, bool> teclasPresionadas; // Diccionario para el estado de las teclas
        private Timer gameTimer;
        private Timer timerCuracion; // Temporizador para generar power-ups
        private List<PowerUpCuracion> Curacion = new List<PowerUpCuracion>(); // Lista de power-ups activos en el juego
        private Label labelVidaNavio1;
        private Label labelVidaNavio2;
        private bool juegoTerminado = false;

        // Información de jugadores (colores y nombres)
        string color1 = InformacionJugadores.colorNavio1;
        string color2 = InformacionJugadores.colorNavio2;
        string jugador1 = InformacionJugadores.jugador1;
        string jugador2 = InformacionJugadores.jugador2;

        /// <summary>
        /// Constructor del formulario del mapa. Inicializa componentes y configuraciones iniciales.
        /// </summary>
        public Mapa()
        {
            InitializeComponent();
            teclasPresionadas = new Dictionary<Keys, bool> {
            { Keys.W, false },
            { Keys.S, false },
            { Keys.A, false },
            { Keys.D, false },
            { Keys.Up, false },
            { Keys.Down, false },
            { Keys.Left, false },
            { Keys.Right, false }
        };

            // Configura el temporizador principal del juego
            gameTimer = new Timer();
            gameTimer.Interval = 20; // Actualiza cada 20 ms
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // Configuración para permitir la detección de teclas
            this.KeyPreview = true; // Permite capturar teclas en el formulario
            this.KeyDown += new KeyEventHandler(Mapa_KeyDown); // Asocia el evento KeyDown
            this.KeyUp += new KeyEventHandler(Mapa_KeyUp); // Asegurarse de suscribirse a KeyUp
            this.DoubleBuffered= true;

            InicializarLabelsDeVida();
        }

        /// <summary>
        /// Inicializa y configura las etiquetas que muestran la vida de los barcos en pantalla.
        /// </summary>
        private void InicializarLabelsDeVida()
        {
            labelVidaNavio1 = new Label
            {
                Location = new Point(10, 10),
                Size = new Size(200, 30),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Red,
                BackColor = Color.FromArgb(128, 0, 0, 0)
            };

            labelVidaNavio2 = new Label
            {
                Location = new Point(this.Width - 210, 10),
                Size = new Size(200, 30),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Gray,
                BackColor = Color.FromArgb(128, 0, 0, 0)
            };

            // Agrega las etiquetas al formulario
            this.Controls.Add(labelVidaNavio1);
            this.Controls.Add(labelVidaNavio2);
        }

        /// <summary>
        /// Inicializa y configura el temporizador que genera items de curacion cada cierto tiempo.
        /// </summary>
        private void InicializarTimerCuraciones()
        {
            timerCuracion = new Timer
            {
                Interval = 10000 // Genera power-up cada 10 segundos
            };
            timerCuracion.Tick += TimerCuracion_Tick;
            timerCuracion.Start();
        }

        /// <summary>
        /// Evento para generar nuevos iten de curacion y eliminar los que ya no están activos.
        /// </summary>     
        private void TimerCuracion_Tick(object sender, EventArgs e)
        {
            // Eliminar items inactivos
            for (int i = Curacion.Count - 1; i >= 0; i--)
            {
                if (!Curacion[i].Activo)
                {
                    this.Controls.Remove(Curacion[i].Imagen);
                    Curacion[i].Imagen.Dispose();
                    Curacion.RemoveAt(i);
                }
            }

            // Generar nuevo item si hay menos de 3 en el mapa
            if (Curacion.Count < 3)
            {
                List<ObjetoGrafico> objetosExistentes = new List<ObjetoGrafico>();
                objetosExistentes.AddRange(RocaLimite);
                objetosExistentes.AddRange(BombaLimite);
                objetosExistentes.Add(navio1);
                objetosExistentes.Add(navio2);
                objetosExistentes.AddRange(Curacion.Where(p => p.Activo));

                // Generar una posición aleatoria sin colisiones
                Point posicion = PowerUpCuracion.ObtenerPosicionAleatoria(this.Size, objetosExistentes);
                var CuracionItem = new PowerUpCuracion(posicion.X, posicion.Y);
                Curacion.Add(CuracionItem);
                this.Controls.Add(CuracionItem.Imagen);
            }
        }

        /// <summary>
        /// Crea los límites del mapa utilizando rocas y minas.
        /// </summary>
        private void CrearLimite()
        {
            var CoordRocas = tablero.CoordMuros;
            foreach (var coor in CoordRocas)
            {
                Roca pared = new Roca(coor.X, coor.Y);
                RocaLimite.Add(pared);
                this.Controls.Add(pared.Imagen);
            }

            var CoordMinas = tablero.CoordMinas;
            foreach (var coor in CoordMinas)
            {
                Mina mina = new Mina(coor.X, coor.Y);
                mina.Explosion += Mina_Explosion;
                BombaLimite.Add(mina);
                this.Controls.Add(mina.Imagen);
            }
        }

        /// <summary>
        /// Maneja la explosión de una mina y el daño a los barcos que se encuentren en su área.
        /// </summary>
        private void Mina_Explosion(object sender, ExplosionEventArgs e)
        {
            if (navio1.GetBounds().IntersectsWith(e.Area))
            {
                navio1.RecibirDanio(e.Damage);
            }
            if (navio2.GetBounds().IntersectsWith(e.Area))
            {
                navio2.RecibirDanio(e.Damage);
            }
        }

        /// <summary>
        /// Genera todos los objetos en el mapa
        /// </summary>
        private void Mapa_Load(object sender, EventArgs e)
        {
            Audio audio = new Audio(1);
            audio.ReproducirAudio();

            // Inicializa los navios de ambos jugadores
            navio1 = new Navio(new Point(100, 100), 45, 45, color1, 4, 128);
            navio2 = new Navio(new Point(700, 550), 45, 45, color2, 4, 128);

            // Agrega las imágenes de los navios al formulario
            this.Controls.Add(navio1.Imagen);
            this.Controls.Add(navio2.Imagen);
            CrearLimite();

            // Inicializa la lista de objetos (puedes añadir obstáculos aquí si se desea)
            List<ObjetoGrafico> objetos = new List<ObjetoGrafico>();

            // Se llaman los eventos al recibir daño o curación y cuando se destruye el navío
            navio1.DanioRecibido += Navio_DamageRecibido;
            navio2.DanioRecibido += Navio_DamageRecibido;
            navio1.CuracionRecibida += Navio_CuracionRecibida;
            navio2.CuracionRecibida += Navio_CuracionRecibida;
            navio1.NavioDestruido += Navio_Destruido;
            navio2.NavioDestruido += Navio_Destruido;

            InicializarTimerCuraciones();
            ActualizarLabelsVida();
        }

        // Actualizar labels al recibir curación
        private void Navio_CuracionRecibida(object sender, DamageEventArgs e)
        {
            ActualizarLabelsVida();
        }

        // Actualizar labels al recibir daño
        private void Navio_DamageRecibido(object sender, DamageEventArgs e)
        {
            ActualizarLabelsVida();
        }

        // Labels de marcador
        private void ActualizarLabelsVida()
        {
            labelVidaNavio1.Text = $"{jugador1}: {navio1.VidaActual}/{navio1.VidaMaxima} HP";
            labelVidaNavio2.Text = $"{jugador2}: {navio2.VidaActual}/{navio2.VidaMaxima} HP";
        }

        /// <summary>
        /// Determina el final de la partida al destruir un navío, define un ganador y un perdedor.
        /// Se llama a la BD y ejecuta los métodos definidos según el resultado.
        /// </summary>        
        private void Navio_Destruido(object sender, EventArgs e)
        {
            if (juegoTerminado) return;
            juegoTerminado = true;

            string ganador = "";
            string perdedor = "";

            if (sender == navio1)
            {
                ganador = jugador2;
                perdedor = jugador1;
            }
            else
            {
                ganador = jugador1;
                perdedor = jugador2;
            }

            MessageBox.Show($"¡{ganador} ha ganado la partida!", "Fin del juego", MessageBoxButtons.OK, MessageBoxIcon.Information);

            GestionDB.AgregarVictoria(ganador);
            GestionDB.AgregarDerrota(perdedor);

            Jugar jugar = new Jugar();
            jugar.Show();
            this.Close();
        }

        // Evento para manejar las teclas de flechas y teclas WASD
        private void Mapa_KeyDown(object sender, KeyEventArgs e)
        {
            // Asocia la tecla a true al presionarse
            Audio audio = new Audio(2);
            if (teclasPresionadas.ContainsKey(e.KeyCode))
                teclasPresionadas[e.KeyCode] = true;

            // Teclas de disparo
            if (e.KeyCode == Keys.Space) // Disparo para el jugador 1
            {
                audio.ReproducirAudio();
                navio1.Disparar(this);
            }
            if (e.KeyCode == Keys.L) // Disparo para el jugador 2
            {
                audio.ReproducirAudio();
                navio2.Disparar(this);
            }
        }

        // Asocia la tecla a false al levantarse
        private void Mapa_KeyUp(object sender, KeyEventArgs e)
        {
            if (teclasPresionadas.ContainsKey(e.KeyCode))
                teclasPresionadas[e.KeyCode] = false;
        }

        /// <summary>
        /// Método que maneja las actualizaciones del juego en cada tick del temporizador
        /// </summary>        
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Instanciar audios
            Audio audio = new Audio(3);
            Audio audioItem = new Audio(4);

            // Estado de la partida
            if (juegoTerminado) return;

            // Movimiento del jugador 1
            if (teclasPresionadas[Keys.W]) navio1.MoverArriba(RocaLimite);
            if (teclasPresionadas[Keys.S]) navio1.MoverAbajo(RocaLimite);
            if (teclasPresionadas[Keys.A]) navio1.MoverIzquierda(RocaLimite);
            if (teclasPresionadas[Keys.D]) navio1.MoverDerecha(RocaLimite);
            // Movimiento del jugador 2
            if (teclasPresionadas[Keys.Up]) navio2.MoverArriba(RocaLimite);
            if (teclasPresionadas[Keys.Down]) navio2.MoverAbajo(RocaLimite);
            if (teclasPresionadas[Keys.Left]) navio2.MoverIzquierda(RocaLimite);
            if (teclasPresionadas[Keys.Right]) navio2.MoverDerecha(RocaLimite);

            // Colisiones de los navios con la mina y reproducir audios
            foreach (var objeto in BombaLimite.ToList())
            {
                if (objeto is Mina mina && mina.Activa)
                {
                    if (navio1.GetBounds().IntersectsWith(mina.GetBounds()) ||
                        navio2.GetBounds().IntersectsWith(mina.GetBounds()))
                    {
                        audio.ReproducirAudio();
                        mina.Explotar(this);
                    }
                }
            }

            // Lista de objetos de colisiones y se agregan las rocas y minas activas
            List<ObjetoGrafico> objetosColision = new List<ObjetoGrafico>();
            objetosColision.AddRange(RocaLimite);
            objetosColision.AddRange(BombaLimite.Where(b => (b as Mina)?.Activa ?? false));
            objetosColision.Add(navio1);
            objetosColision.Add(navio2);

            // Actualizar balas
            navio1.ActualizarBalas(objetosColision);
            navio2.ActualizarBalas(objetosColision);            

            // Verificar si se recoge la curación
            foreach (var powerUp in Curacion.ToList())
            {
                if (!powerUp.Activo) continue;

                if (navio1.GetBounds().IntersectsWith(powerUp.GetBounds()))
                {
                    audioItem.ReproducirAudio();
                    powerUp.Recoger(navio1);
                }
                else if (navio2.GetBounds().IntersectsWith(powerUp.GetBounds()))
                {
                    audioItem.ReproducirAudio();
                    powerUp.Recoger(navio2);
                }
            }
        }

        // Timer de animaciones para los navíos
        private void timerAnimacion_Tick(object sender, EventArgs e)
        {
            navio1.Animacion();
            navio2.Animacion();
        }
    }
}
