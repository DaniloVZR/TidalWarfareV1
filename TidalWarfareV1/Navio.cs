using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TidalWarfareV1;

internal class Navio : Entidad
{
    int velocidad = 4;
    int posX = 0, posY = 0;
    public List<Bala> Balas { get => balas; }
    private int direccionActual = 1; // Por defecto mirando a la derecha

    // Daño
    private int vidaMaxima = 100;
    private int vidaActual;
    private bool estaVivo = true;
    public event EventHandler<DanioEventArgs> DanioRecibido;
    public event EventHandler<DanioEventArgs> CuracionRecibida;
    public event EventHandler NavioDestruido;

    // Limitar balas    
    private List<Bala> balas = new List<Bala>();
    private const int MAXIMO_BALAS = 3;  // Límite de balas
    private Timer timerCooldownDisparo;
    private bool puedeDisparar = true;
    private const int COOLDOWN_DISPARO_MS = 200;  // Medio segundo de cooldown

    // Powerup
    private Timer timerInvulnerabilidad;
    private bool esInvulnerable;
    public int VidaActual => vidaActual;
    public int VidaMaxima => vidaMaxima;
    public bool EstaVivo => estaVivo;
   

    public Navio(Point coor, int w, int h, string nombreRecursoBase, int frames, int Tamframe): base(coor, w, h, nombreRecursoBase, frames, Tamframe)
    {
        vidaActual = vidaMaxima;

        // Inicializar el timer de cooldown
        timerCooldownDisparo = new Timer
        {
            Interval = COOLDOWN_DISPARO_MS
        };
        timerCooldownDisparo.Tick += (s, e) =>
        {
            puedeDisparar = true;
            timerCooldownDisparo.Stop();
        };
    }
    public void RecibirDanio(int cantidad)
    {
        if (!estaVivo) return;

        vidaActual = Math.Max(0, vidaActual - cantidad);
        DanioRecibido?.Invoke(this, new DanioEventArgs(cantidad));  // Quitar vidaActual

        if (vidaActual <= 0)
        {
            estaVivo = false;
            NavioDestruido?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Curar(int cantidad)
    {
        vidaActual = Math.Min(vidaActual + cantidad, vidaMaxima);
        CuracionRecibida?.Invoke(this, new DanioEventArgs(cantidad, vidaActual));  // Añadir vidaActual aquí también
    }
    public void Disparar(Form formulario)
    {
        // Verificar si puede disparar y si no excede el límite de balas
        if (!puedeDisparar || balas.Count >= MAXIMO_BALAS) return;

        int offsetX = Imagen.Width / 2 - 10;
        int offsetY = Imagen.Height / 2 - 10;

        Point posicionBala = new Point(Imagen.Location.X + offsetX, Imagen.Location.Y + offsetY);

        switch (direccionActual)
        {
            case 0: // Izquierda
                posicionBala.X = Imagen.Location.X - 15;
                break;
            case 1: // Derecha
                posicionBala.X = Imagen.Location.X + Imagen.Width + 15;
                break;
            case 2: // Abajo
                posicionBala.Y = Imagen.Location.Y + Imagen.Height + 15;
                break;
            case 3: // Arriba
                posicionBala.Y = Imagen.Location.Y - 15;
                break;
        }

        Bala nuevaBala = new Bala(posicionBala.X, posicionBala.Y, direccionActual);
        balas.Add(nuevaBala);
        formulario.Controls.Add(nuevaBala.Imagen);

        // Activar cooldown
        puedeDisparar = false;
        timerCooldownDisparo.Start();
    }

    public void ActualizarBalas(List<ObjetoGrafico> objetos)
    {
        for (int i = balas.Count - 1; i >= 0; i--)
        {
            if (balas[i].Activa)
            {
                balas[i].Mover(objetos);
            }

            if (!balas[i].Activa)
            {
                Form formulario = balas[i].Imagen.FindForm();
                if (formulario != null)
                {
                    formulario.Controls.Remove(balas[i].Imagen);
                }
                balas[i].Imagen.Dispose();
                balas.RemoveAt(i);
            }
        }
    }

    // Método para mover el barco
    public bool Mover(int dx, int dy, int yFrame, List<ObjetoGrafico> objetos)
    {
        Rectangle nuevaPosicion = this.GetBounds();
        nuevaPosicion.X += dx * velocidad;
        nuevaPosicion.Y += dy * velocidad;

        foreach (ObjetoGrafico objetoGrafico in objetos)
        {
            if (objetoGrafico.GetBounds().IntersectsWith(nuevaPosicion))
            {
                posy = yFrame;
                return false;
            }
        }
        posy = yFrame;
        this.setPos(nuevaPosicion.X, nuevaPosicion.Y);
        return false;
    }

    public bool MoverArriba(List<ObjetoGrafico> objetos)
    {
        direccionActual = 3;
        CambiarDireccion(3);
        return Mover(0, -1, tamFrame * 3, objetos);
    }

    public bool MoverAbajo(List<ObjetoGrafico> objetos)
    {
        direccionActual = 2;
        CambiarDireccion(2);
        return Mover(0, 1, tamFrame * 2, objetos);
    }

    public bool MoverIzquierda(List<ObjetoGrafico> objetos)
    {
        direccionActual = 0;
        CambiarDireccion(0);
        return Mover(-1, 0, 0, objetos);
    }

    public bool MoverDerecha(List<ObjetoGrafico> objetos)
    {
        direccionActual = 1;
        CambiarDireccion(1);
        return Mover(1, 0, tamFrame, objetos);
    }
}

public class DanioEventArgs : EventArgs
{
    public int Cantidad { get; private set; }
    public int VidaActual { get; private set; }

    public DanioEventArgs(int cantidad)
    {
        Cantidad = cantidad;
    }

    public DanioEventArgs(int cantidad, int vidaActual)  // Modificar el constructor
    {
        Cantidad = cantidad;
        VidaActual = vidaActual;
    }
}