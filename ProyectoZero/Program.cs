using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoZero
{
    //Proyecto para consola basado en el juego "Hundir la flota"
    class Program
    {
        class Barco {
            public enum Orientacion { HORIZONTAL, VERTICAL };

            public int fila;
            public int columna;
            public Orientacion orientacion;
            public int tamaño;
            public int impactos;
        }

        class Jugador {
            public enum Disparo { DESCONOCIDO, AGUA, TOCADO, HUNDIDO };

            public Barco[] listaBarcos;
            public int[,] tableroBarcos;
            public Disparo[,] tableroDisparos;

            public int numeroFilas;
            public int numeroColumnas;
            public int barcosRestantes;
            public int barcosCreados;
        }

        class Partida {
            public Jugador jugador;

            public int filas;
            public int columnas;
            public int numeroBarcos;
            public int[] numeroBarcosTamaño = new int[6];
        }

        //Crear nueva partida
        //Devuelve la partida creada
        static Partida CrearPartida(int filas, int columnas, int numTam2, int numTam3, int numTam4, int numTam5) {

            Partida partida = new Partida();

            partida.filas = filas;
            partida.columnas = columnas;
            partida.numeroBarcos = numTam2 + numTam3 + numTam4 + numTam5;
            partida.numeroBarcosTamaño[0] = numTam2;
            partida.numeroBarcosTamaño[1] = numTam3;
            partida.numeroBarcosTamaño[2] = numTam4;
            partida.numeroBarcosTamaño[3] = numTam5;
            partida.jugador = CrearJugador(filas, columnas, partida.numeroBarcos);

            return partida;
        }

        //Crear nuevo jugador
        //Devuelve el jugador creado
        static Jugador CrearJugador(int numeroFilas, int numeroColumnas, int numeroBarcos) {

            Jugador jugador = new Jugador();

            jugador.numeroFilas = numeroFilas;
            jugador.numeroColumnas = numeroColumnas;
            jugador.barcosRestantes = numeroBarcos;
            jugador.barcosCreados = 0;
            jugador.tableroBarcos = new int[numeroFilas, numeroColumnas];
            int i, j;
            for (i = 0; i < numeroFilas; i++)
                for (j = 0; j < numeroColumnas; j++) {
                    jugador.tableroBarcos[i, j] = -1;  //Coloca un -1 en todo el tablero, ya que no hay ningún barco
                }

            return jugador;
        }

        //Crear nuevo barco
        //Devuelve el barco creado
        static Barco CrearBarco(int tamaño, int fila, int columna, Barco.Orientacion orientacion) {
            Barco barco = new Barco();

            barco.tamaño = tamaño;
            barco.fila = fila;
            barco.columna = columna;
            barco.impactos = 0;
            barco.orientacion = orientacion;

            return barco;
        }

        //Muestra por pantalla el tablero de barcos
        static void MostrarTableroBarcos(Jugador jugador) {
            int i, j;

            Console.Write(" ");

            for (j = 0; j < jugador.numeroColumnas; j++) {
                Console.Write("   " + j);
            }

            Console.WriteLine("");
            Console.WriteLine("");

            for (i = 0; i < jugador.numeroFilas; i++) {

                Console.Write(i + "  ");

                for (j = 0; j < jugador.numeroColumnas; j++) {
                    if (jugador.tableroBarcos[i, j] == -1)
                        Console.Write(jugador.tableroBarcos[i, j] + "  ");
                    else
                        Console.Write(" " + jugador.tableroBarcos[i, j] + "  ");
                }

                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }

            Console.ReadKey();
        }

        //Muestra por pantalla el tablero de disparos
        static void MostrarTableroDisparos(Jugador jugador) {
            int i, j;

            Console.Write(" ");

            for (j = 0; j < jugador.numeroColumnas; j++)
            {
                Console.Write("   " + j);
            }

            Console.WriteLine("");
            Console.WriteLine("");

            for (i = 0; i < jugador.numeroFilas; i++)
            {

                Console.Write(i + "  ");

                for (j = 0; j < jugador.numeroColumnas; j++)
                {
                    Console.Write(" ?" + "  ");
                }

                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }

            Console.ReadKey();
        }

        //Pregunta al usuario la posicion y el tamaño del barco que quiere colocar
        static void PreguntarBarco(Partida partida) {
            int tamaño2, tamaño3, tamaño4, tamaño5;
            tamaño2 = partida.numeroBarcosTamaño[0];
            tamaño3 = partida.numeroBarcosTamaño[1];
            tamaño4 = partida.numeroBarcosTamaño[2];
            tamaño5 = partida.numeroBarcosTamaño[3];

            if (partida.numeroBarcos == 1)
                Console.WriteLine("Dispones de " + partida.numeroBarcos + " barco para colocar:");
            else
                Console.WriteLine("Dispones de " + partida.numeroBarcos + " barcos para colocar:");

            Console.WriteLine("");

            if (tamaño2 == 1)
                Console.WriteLine(tamaño2 + " barco de tamaño 2");
            else
                Console.WriteLine(tamaño2 + " barcos de tamaño 2");

            if (tamaño3 == 1)
                Console.WriteLine(tamaño3 + " barco de tamaño 3");
            else
                Console.WriteLine(tamaño3 + " barcos de tamaño 3");

            if (tamaño4 == 1)
                Console.WriteLine(tamaño4 + " barco de tamaño 4");
            else
                Console.WriteLine(tamaño4 + " barcos de tamaño 4");

            if (tamaño5 == 1)
                Console.WriteLine(tamaño5 + " barco de tamaño 5");
            else
                Console.WriteLine(tamaño5 + " barcos de tamaño 5");

            BarcoTamaño2(partida, partida.numeroBarcosTamaño[0], 2, 1);
            }

        static void BarcoTamaño2 (Partida partida, int tamaño, int n2, int cont) {
            int x, y;
            int orientacion;
            if (tamaño == 1)
            {
                Console.WriteLine("Introduce la primera coordenada donde quieres colocar el barco de tamaño 2:");
                x = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Introduce la segunda coordenada:");
                y = Convert.ToInt32(Console.ReadLine());

                if (x > partida.filas || y > partida.columnas)
                {
                    Console.WriteLine("Error: ha introducido una posición fuera de los límites del tablero, vuelva a intentarlo");
                    Console.ReadLine();
                    BarcoTamaño2(partida, tamaño, n2, cont);
                }
                else
                {
                    Console.WriteLine("Introduce 1 si quieres colocar el barco en horizontal o 2 en vertical");
                    orientacion = Convert.ToInt32(Console.ReadLine());
                    bool comprobacion = ComprobarBarco(partida, n2, orientacion, x, y); //Funcion para ver si se sale del tablero o toca con otro barco
                    if (comprobacion == false)
                    {
                        Console.WriteLine("Error: el barco se sale de los límites del tablero o toca otro barco");
                        Console.ReadLine();
                        BarcoTamaño2(partida, tamaño, n2, cont);
                    }
                    else
                    {
                        int resultado = ColocarBarco(partida, n2, x, y, orientacion); //Crea barco, marca casillas...
                        if (resultado == 0)
                            Console.WriteLine("Se ha colocado el barco correctamente");
                        else
                            Console.WriteLine("No se ha podido colocar el barco");
                        Console.ReadLine();
                    }
                }
            }
            else {
                while (cont <= tamaño) {
                    Console.WriteLine("Introduce la primera coordenada del barco número " + cont + " de tamaño 2:");
                    x = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Introduce la segunda coordenada:");
                    y = Convert.ToInt32(Console.ReadLine());

                    if (x > partida.filas || y > partida.columnas)
                    {
                        Console.WriteLine("Error: ha introducido una posición fuera de los límites del tablero");
                        Console.ReadLine();
                        BarcoTamaño2(partida, tamaño, n2, cont);
                    }
                    else
                    {
                        Console.WriteLine("Introduce 1 si quieres colocar el barco en horizontal o 2 en vertical");
                        orientacion = Convert.ToInt32(Console.ReadLine());
                        bool comprobacion = ComprobarBarco(partida, n2, orientacion, x, y); //Funcion para ver si se sale del tablero o toca con otro barco
                        if (comprobacion == false)
                        {
                            Console.WriteLine("Error: el barco se sale de los límites del tablero o toca otro barco");
                            Console.ReadLine();
                            BarcoTamaño2(partida, tamaño, n2, cont);
                        }
                        else 
                        {
                            cont++;
                            int resultado = ColocarBarco(partida, n2, x, y, orientacion); //Crea barco, marca casillas...
                            if (resultado == 0)
                                Console.WriteLine("Se ha colocado el barco correctamente");
                            else
                                Console.WriteLine("No se ha podido colocar el barco");
                            Console.ReadLine();
                        }
                    }
                }
            }
        }

        static bool ComprobarBarco(Partida partida, int tamaño, int orientacion, int fila, int columna) {
            int x, y;
            //Comprobar si se sale de los límites del tablero
            if (orientacion == 1)
            { //Horizontal
                if ((columna + 1) > partida.columnas)
                    return false;
                else {
                    //Comprobar que no toca otro barco
                    for (x = fila - 1; x <= fila + 1; x++) {
                        for (y = columna - 1; y <= columna + 2; y++) {
                            try
                            {
                                if (partida.jugador.tableroBarcos[x, y] == 0 || partida.jugador.tableroBarcos[x, y] == null
                                    || partida.jugador.tableroBarcos[x, y] == tamaño || partida.jugador.tableroBarcos[x, y] == -1)
                                {
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            catch (Exception e) { 
                            }
                        }
                    }
                        return true;
                }
            }
            else
            //Comprobar si se sale de los límites del tablero
            {  //Vertical
                if ((fila + 1) > partida.filas)
                    return false;
                else
                {
                    int contador = 0;
                    //Comprobar que no toca otro barco
                    for (x = fila - 1; x <= fila + 2; x++)
                    {
                        for (y = columna - 1; y <= columna + 1; y++)
                        {
                            try
                            {
                                if (partida.jugador.tableroBarcos[x, y] == 0 || partida.jugador.tableroBarcos[x, y] == tamaño
                                    || partida.jugador.tableroBarcos[x, y] == -1)
                                    if (partida.jugador.tableroBarcos[x, y] == tamaño)
                                    {
                                        if (contador <= tamaño)
                                            contador++;
                                        else
                                            return false;
                                    }

                                    else
                                    {
                                        return false;
                                    }
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }

                    return true;
                }
            }
        }

        static int ColocarBarco(Partida partida, int n, int fila, int columna, int orientacionInt) {
            try
            {
                //Crear barco
                Barco.Orientacion orientacion = (Barco.Orientacion)orientacionInt;
                Barco barco = CrearBarco(n, fila, columna, orientacion);

                //Añadir barco a la lista
                partida.jugador.listaBarcos = new Barco[partida.numeroBarcos];
                partida.jugador.listaBarcos[partida.jugador.barcosCreados] = barco;
                partida.jugador.barcosCreados = partida.jugador.barcosCreados + 1;

                //Marcar casillas del tablero "Mostrar Barcos"
                partida.jugador.tableroBarcos[fila, columna] = n;
                if (orientacionInt == 1)
                { //HORIZONTAL
                    if (n == 2)
                        partida.jugador.tableroBarcos[fila, columna + 1] = n;
                }
                if (orientacionInt == 2)
                { //VERTICAL
                    if (n == 2)
                        partida.jugador.tableroBarcos[fila + 1, columna] = n;
                }
                return 0;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        static void Main(string[] args)
        {
            int numFilas;
            int numColumnas;
            int numTamaño2, numTamaño3, numTamaño4, numTamaño5;
            Partida partida;

            //1‐ Preguntar al usuario los parámetros de configuración: número de filas, columnas y barcos de cada tipo
            Console.WriteLine("Introduce el número de filas del tablero");
            numFilas = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Introduce el número de columnas del tablero");
            numColumnas = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Introduce el número de barcos de tamaño 2");
            numTamaño2 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Introduce el número de barcos de tamaño 3");
            numTamaño3 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Introduce el número de barcos de tamaño 4");
            numTamaño4 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Introduce el número de barcos de tamaño 5");
            numTamaño5 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            
            partida = CrearPartida(numFilas, numColumnas, numTamaño2, numTamaño3, numTamaño4, numTamaño5);

            //3‐ Mostrar el tablero de barcos
            MostrarTableroBarcos(partida.jugador);

            Console.WriteLine("");
            Console.WriteLine("");

            //4‐ Mostrar el tablero de disparos
            MostrarTableroDisparos(partida.jugador);

            //2‐ Pedirle que coloque los barcos, comprobando para cada uno que la posición es correcta
            Console.WriteLine("");
            PreguntarBarco(partida);

            Console.WriteLine("");
            Console.WriteLine("");
            MostrarTableroBarcos(partida.jugador);

        }
    }
}
