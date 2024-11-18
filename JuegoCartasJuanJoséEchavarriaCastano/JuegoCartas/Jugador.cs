using System.Text;

namespace JuegoCartas
{
    public class Jugador
    {
        private const int TOTAL_CARTAS = 10;
        private Carta[] cartas;
        private Random r = new Random();


        public void Repartir()
        {
            cartas = new Carta[TOTAL_CARTAS];
            for (int i = 0; i < TOTAL_CARTAS; i++)
            {
                cartas[i] = new Carta(r);
            }
        }

        public void Mostrar(ListView lv)
        {
            lv.Items.Clear();
            foreach (Carta carta in cartas)
            {
                carta.mostrar(lv);
            }
        }

        public String ObtenerGrupos()
        {
            StringBuilder mensaje = new StringBuilder();
            int[] contadores = new int[Enum.GetValues(typeof(NombreCarta)).Length];
            Dictionary<NombreCarta, int> cartasRestantes = new Dictionary<NombreCarta, int>();

            foreach (Carta carta in cartas)
            {
                contadores[(int)carta.ObtenerNombre()]++;
                NombreCarta nombre = carta.ObtenerNombre();
                if (cartasRestantes.ContainsKey(nombre))
                {
                    cartasRestantes[nombre]++;
                }
                else
                {
                    cartasRestantes[nombre] = 1;
                }
            }

            // Verificar escaleras completas
            for (int i = 0; i < contadores.Length - 2; i++)
            {
                if (contadores[i] > 0)
                {
                    int longitudEscalera = 1;
                    for (int j = i + 1; j < contadores.Length && contadores[j] > 0; j++)
                    {
                        longitudEscalera++;
                    }

                    if (longitudEscalera >= 3)
                    {
                        mensaje.Append("Escalera encontrada: ");
                        for (int k = i; k < i + longitudEscalera; k++)
                        {
                            mensaje.Append($"{((NombreCarta)k)}, ");
                            // Remover solo una carta de cada valor en la escalera
                            cartasRestantes[(NombreCarta)k]--;
                            if (cartasRestantes[(NombreCarta)k] == 0)
                            {
                                cartasRestantes.Remove((NombreCarta)k);
                            }
                        }
                        mensaje.Length -= 2; // Eliminar la última coma y espacio
                        mensaje.AppendLine();
                        i += longitudEscalera - 1; // Saltar la longitud de la escalera encontrada
                    }
                }
            }

            // Calcular puntaje de cartas restantes
            int puntaje = 0;
            foreach (var cartaRestante in cartasRestantes)
            {
                puntaje += ((int)cartaRestante.Key + 1) * cartaRestante.Value; // +1 porque el enum empieza en 0
            }

            mensaje.AppendLine($"Puntaje de las cartas restantes: {puntaje}");
            return mensaje.ToString();
        }
    }
}
