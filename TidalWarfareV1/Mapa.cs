using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static TidalWarfareV1.Program;

namespace TidalWarfareV1
{
    public partial class Mapa : Form
    {
        private List<ObjetoGrafico> navios = new List<ObjetoGrafico>();
        private List<ObjetoGrafico> RocaLimite = new List<ObjetoGrafico>();
        private List<ObjetoGrafico> BombaLimite = new List<ObjetoGrafico>();// Lista de objetos para verificar colisiones
        Tablero tablero = new Tablero();
        private Navio navio1;
        private Navio navio2;
        private Dictionary<Keys, bool> teclasPresionadas; // Diccionario para el estado de las teclas
        private Timer gameTimer;
        private Timer timerPowerUp;
        private List<PowerUpCuracion> powerUps = new List<PowerUpCuracion>();
        private Label labelVidaNavio1;
        private Label labelVidaNavio2;
        private bool juegoTerminado = false;
        string color1 = InformacionJugadores.colorNavio1;
        string color2 = InformacionJugadores.colorNavio2;
        string jugador1 = InformacionJugadores.jugador1;
        string jugador2 = InformacionJugadores.jugador2;


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

            gameTimer = new Timer();
            gameTimer.Interval = 20; // Actualiza cada 20 ms
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // Configuración para permitir la detección de teclas
            this.KeyPreview = true; // Permite capturar teclas en el formulario
            this.KeyDown += new KeyEventHandler(Mapa_KeyDown); // Asocia el evento KeyDown
            this.KeyUp += new KeyEventHandler(Mapa_KeyUp); // Asegurarse de suscribirse a KeyUp

            InitializeHealthLabels();
        }

        private void InitializeHealthLabels()
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

            this.Controls.Add(labelVidaNavio1);
            this.Controls.Add(labelVidaNavio2);
        }

        private void InitializePowerUpTimer()
        {
            timerPowerUp = new Timer
            {
                Interval = 10000 // Genera power-up cada 10 segundos
            };
            timerPowerUp.Tick += TimerPowerUp_Tick;
            timerPowerUp.Start();
        }

        private void TimerPowerUp_Tick(object sender, EventArgs e)
        {
            // Eliminar power-ups inactivos
            for (int i = powerUps.Count - 1; i >= 0; i--)
            {
                if (!powerUps[i].Activo)
                {
                    this.Controls.Remove(powerUps[i].Imagen);
                    powerUps[i].Imagen.Dispose();
                    powerUps.RemoveAt(i);
                }
            }

            // Generar nuevo power-up si hay menos de 3 en el mapa
            if (powerUps.Count < 3)
            {
                List<ObjetoGrafico> objetosExistentes = new List<ObjetoGrafico>();
                objetosExistentes.AddRange(RocaLimite);
                objetosExistentes.AddRange(BombaLimite);
                objetosExistentes.Add(navio1);
                objetosExistentes.Add(navio2);                
                objetosExistentes.AddRange(powerUps.Where(p => p.Activo));

                Point posicion = PowerUpCuracion.ObtenerPosicionAleatoria(this.Size, objetosExistentes);
                var powerUp = new PowerUpCuracion(posicion.X, posicion.Y);
                powerUps.Add(powerUp);
                this.Controls.Add(powerUp.Imagen);
            }
        }

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

        private void Mina_Explosion(object sender, ExplosionEventArgs e)
        {
            if (navio1.GetBounds().IntersectsWith(e.Area))
            {
                navio1.RecibirDanio(e.Danio);
            }
            if (navio2.GetBounds().IntersectsWith(e.Area))
            {
                navio2.RecibirDanio(e.Danio);
            }
        }

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

            // Inicializa la lista de objetos (puedes añadir obstáculos aquí si lo deseas)
            List<ObjetoGrafico> objetos = new List<ObjetoGrafico>();

            navio1.DanioRecibido += Navio_DanioRecibido;
            navio2.DanioRecibido += Navio_DanioRecibido;
            navio1.CuracionRecibida += Navio_CuracionRecibida;
            navio2.CuracionRecibida += Navio_CuracionRecibida;
            navio1.NavioDestruido += Navio_Destruido;
            navio2.NavioDestruido += Navio_Destruido;

            InitializePowerUpTimer();
            ActualizarLabelsVida();
        }

        private void Navio_CuracionRecibida(object sender, DanioEventArgs e)
        {
            ActualizarLabelsVida();
        }

        private void Navio_DanioRecibido(object sender, DanioEventArgs e)
        {
            ActualizarLabelsVida();
        }
        private void ActualizarLabelsVida()
        {
            labelVidaNavio1.Text = $"{jugador1}: {navio1.VidaActual}/{navio1.VidaMaxima} HP";
            labelVidaNavio2.Text = $"{jugador2}: {navio2.VidaActual}/{navio2.VidaMaxima} HP";
        }

        private void Navio_Destruido(object sender, EventArgs e)
        {
            if (juegoTerminado) return;
            juegoTerminado = true;

            string ganador = sender == navio1 ? jugador2 : jugador1;
            MessageBox.Show($"¡{ganador} ha ganado la partida!", "Fin del juego", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Opcional: reiniciar el juego o cerrar la aplicación
            this.Close();
        }
        //Evento para manejar las teclas de flechas y teclas WASD

        private void Mapa_KeyDown(object sender, KeyEventArgs e)
        {
            Audio audio = new Audio(2);
            if (teclasPresionadas.ContainsKey(e.KeyCode))
                teclasPresionadas[e.KeyCode] = true;

            // Teclas de disparo
            if (e.KeyCode == Keys.Space) // Disparo para el jugador 1
            {
                audio.ReproducirAudio();
                navio1.Disparar(this);
            }
            if (e.KeyCode == Keys.Enter) // Disparo para el jugador 2
            {
                audio.ReproducirAudio();
                navio2.Disparar(this);
            }
        }

        private void Mapa_KeyUp(object sender, KeyEventArgs e)
        {
            if (teclasPresionadas.ContainsKey(e.KeyCode))
                teclasPresionadas[e.KeyCode] = false;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Audio audio = new Audio(3);
            if (juegoTerminado) return;

            if (teclasPresionadas[Keys.W])
                navio1.MoverArriba(RocaLimite);
            if (teclasPresionadas[Keys.S])
                navio1.MoverAbajo(RocaLimite);
            if (teclasPresionadas[Keys.A])
                navio1.MoverIzquierda(RocaLimite);
            if (teclasPresionadas[Keys.D])
                navio1.MoverDerecha(RocaLimite);

            if (teclasPresionadas[Keys.Up])
                navio2.MoverArriba(RocaLimite);
            if (teclasPresionadas[Keys.Down])
                navio2.MoverAbajo(RocaLimite);
            if (teclasPresionadas[Keys.Left])
                navio2.MoverIzquierda(RocaLimite);
            if (teclasPresionadas[Keys.Right])
                navio2.MoverDerecha(RocaLimite);

            foreach (var objeto in BombaLimite.ToList())
            {
                if (objeto is Mina mina && mina.Activa)
                {
                    if (navio1.GetBounds().IntersectsWith(mina.GetBounds()) ||
                        navio2.GetBounds().IntersectsWith(mina.GetBounds()))
                    {
                        audio.ReproducirAudio();
                        mina.Explotar(this); // Pasamos la referencia del formulario
                    }
                }
            }

            // Actualizar balas
            List<ObjetoGrafico> objetosColision = new List<ObjetoGrafico>();
            objetosColision.AddRange(RocaLimite);
            objetosColision.AddRange(BombaLimite.Where(b => (b as Mina)?.Activa ?? false));
            objetosColision.Add(navio1);
            objetosColision.Add(navio2);

            navio1.ActualizarBalas(objetosColision);
            navio2.ActualizarBalas(objetosColision);

            navio1.ActualizarBalas(objetosColision);
            navio2.ActualizarBalas(objetosColision);

            foreach (var powerUp in powerUps.ToList())
            {
                if (!powerUp.Activo) continue;

                if (navio1.GetBounds().IntersectsWith(powerUp.GetBounds()))
                {
                    powerUp.Recoger(navio1);                    
                }
                else if (navio2.GetBounds().IntersectsWith(powerUp.GetBounds()))
                {
                    powerUp.Recoger(navio2);                    
                }
            }
        }

        private void timerAnimacion_Tick(object sender, EventArgs e)
        {                        
            navio1.Animacion();
            navio2.Animacion();
        }
    }
}