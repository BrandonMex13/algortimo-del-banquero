using System;

namespace banquero
{
    class Program
    {
        static Random numaleatorio = new Random();
        static int numeroProcesos = 4,
                   numeroRecursos = 3;
        static int vuelta = 0;
        static int temp = 0;
        static int Paso = 0;
        static int contadorinseguro = 0;
        static int[,] necesarios = new int[4,3]{ {3,2,2 },  // MATRIZ A
                                                 {6,1,3 },
                                                 {3,1,4 },
                                                 {4,2,2 } };

        static int[,] asignados = new int[4, 3] { {1,0,0 },     //MATRIZ B
                                                  {6,1,2 },
                                                  {2,1,1 },
                                                  {0,0,2 } };

        static int[,] recursos = new int[1, 3] { { 9, 3, 6 } };

        static int[,] disponibles = new int[1, 3] { {0,1,1 } };

        static int[,] arrayutilizar = new int[4,3]; // MATRIZ A-B

        static void LlenarTerceraMatriz()
        {
            for (int a = 0; a < numeroProcesos; a++)
            {
                for (int b = 0; b < numeroRecursos; b++)
                {
                    arrayutilizar[a, b] = (necesarios[a, b] - asignados[a, b]);

                    if (arrayutilizar[a, b] < 0)
                    {
                        arrayutilizar[a, b] = arrayutilizar[a, b] * -1;
                    }
                }
            }
        }

        static void ImprimirTerceraMatriz()
        {
            for (int a = 0; a < numeroProcesos; a++)
            {
                for (int b = 0; b < numeroRecursos; b++)
                {
                    Console.Write(arrayutilizar[a, b]+ "     ");
                }
                Console.WriteLine();
            }
        }

        static int ContarDisponibles()
        {
            int totalrecursos = 0;
            for (int a = 0; a < numeroRecursos; a++)
            {
                totalrecursos += disponibles[0, a];
            }

            return totalrecursos;
        }

        static int ContarTerceraMatriz()
        {
            int totalterceramatriz = 0;
            for (int a = 0; a < numeroProcesos; a++)
            {
                for ( int b = 0; b < numeroRecursos; b++)
                {
                    totalterceramatriz += arrayutilizar[a, b];
                }
            }

            return totalterceramatriz;
        }

        static void LlenarMatrizA()
        {
            for (int a = 0; a < numeroProcesos; a++)
            {
                for (int b = 0; b < numeroRecursos; b++)
                {
                    int temporal = numaleatorio.Next(1,6);
                    necesarios[a, b] = temporal;
                }
            }
        }

        static void LlenarMatrizB()
        {
            for (int a = 0; a < numeroProcesos; a++)
            {
                for (int b = 0; b < numeroRecursos; b++)
                {
                    int temporal = numaleatorio.Next(1, 4);
                    asignados[a, b] = temporal;
                }
            }
        }

        static void Main(string[] args)
        {
            do {
                LlenarMatrizA();
                LlenarMatrizB();
                LlenarTerceraMatriz();

                //ImprimirTerceraMatriz();

                while (true)
                {
                    Console.WriteLine("Paso: {0}", Paso);

                    for (int a = 0; a < numeroRecursos; a++)
                    {
                        temp += arrayutilizar[vuelta, a];
                    }

                    //Console.WriteLine(temp);
                    if (temp <= ContarDisponibles())
                    {
                        //Console.WriteLine("Entro");
                        // suma lo de asignado(matriz B) a disponible
                        for (int a = 0; a < numeroRecursos; a++)
                        {
                            disponibles[0, a] += asignados[vuelta, a];
                            //asignados[vuelta, a] = 0;
                        }

                        // deja en 0 a los valores de la 3ra matriz ya gastados
                        for (int a = 0; a < numeroRecursos; a++)
                        {
                            arrayutilizar[vuelta, a] = 0;
                        }

                        //  deja en 0 a los valores de asignados(matriz B) ya gastados
                        for (int a = 0; a < numeroRecursos; a++)
                        {
                            asignados[vuelta, a] = 0;
                        }


                        ImprimirTerceraMatriz();
                    }
                    else
                    {
                        Console.WriteLine("Es inseguro...");
                        contadorinseguro++;

                        if (contadorinseguro > 4)
                        {
                            LlenarMatrizA();
                            LlenarMatrizB();
                            contadorinseguro = 0;
                        }
                    }

                    vuelta++;

                    if (vuelta >= 4)
                    {
                        vuelta = 0;
                    }

                    if (ContarTerceraMatriz() == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Todo termino correctamente...");
                        ImprimirTerceraMatriz();
                        break;
                    }

                    temp = 0;
                    Paso++;
                    Console.WriteLine();

                    System.Threading.Thread.Sleep(1000);
                }

                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                //Console.WriteLine();
                Paso = 0;
            } while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));
            

        }
    }
}
