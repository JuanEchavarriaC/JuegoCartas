namespace JuegoCartas
{
    public partial class Juego : Form
    {
        Jugador jugador1, jugador2;
        public Juego()
        {
            InitializeComponent();
            jugador1 = new Jugador();
            jugador2 = new Jugador();

        }

        private void btnRepartir_Click(object sender, EventArgs e)
        {
            jugador1.Repartir();
            jugador1.Mostrar(lvJugador1);

            jugador2.Repartir();
            jugador2.Mostrar(lvJugador2);
        }
        private void btnVerificar_Click(object sender, EventArgs e)
        {
            string resultadoJugador1 = jugador1.ObtenerGrupos();
            string resultadoJugador2 = jugador2.ObtenerGrupos();

            MessageBox.Show($"Juan:\n{resultadoJugador1}\n\nFray:\n{resultadoJugador2}", "Resultados");
        }

    }
}
