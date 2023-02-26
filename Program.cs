class Program {

        public static void Main (string[]args) {

        /* Cajero Automatico - Proyecto para la semana 7 - Grupo #1 */

        //Variables Globales
        int opcion, external_id, index;
        char resp = 'n';
        string hora = DateTime.Now.ToString ("hh:mm tt"); //Obtener la hora actual
        string fecha = DateTime.Now.ToString ("dd-MM-yyyy"); //Obtener la fecha actual

        //Datos de ingreso
        string[] tarjetas = {"1234 1234 1234 1234", "1111 2222 3333 4444", "5555 6666 7777 8888", "0000 0000 0000 0000"}; // No. de Tarjetas Modelos
        string[] nombres = {"Ashley Classe", "Arisneudy Santana", "Jorge Rosendo", "Orison Soto"}; // Titulares de Tarjetas
        double[] balances = {9530, 12000, 1300, 140000}; // Balance de Tarjetas

        //Datos de cajero
        double balanceCaja = 100000;
        string passwordCaja = "admin";
        int intentosCaja = 3;
        TimeSpan bloqueoCaja = TimeSpan.FromSeconds(25);
        
        do{
            
            Console.Clear ();
            Thread.Sleep (1000);
            Console.WriteLine("Bienvenido al cajero automático ITLA \n");
            Console.Write ("Ingrese el No. de su tarjeta: ");
            string numTarjeta = Console.ReadLine()!;


            //Comprobacion de No. de Tarjeta
            index = Array.IndexOf (tarjetas, numTarjeta);
            if (index >= 0) {

                // Tiempo de espera en pantalla
                Console.Clear ();
                Console.WriteLine ("Ingresando al sistema.....");
                Thread.Sleep (2000);
            }

            else {

                Console.Clear ();
                Console.WriteLine("Tarjeta no existente en el sistema....");

                break;
            }


            do {

                // Menu Principal - Usuarios
                Console.Clear();
                Console.WriteLine ($"Sr/a {nombres[index]} \n");
                Console.WriteLine ("[1] Depósito");
                Console.WriteLine ("[2] Retiro");
                Console.WriteLine ("[3] Transferencia");
                Console.WriteLine ("[4] Consultar Balance");
                Console.WriteLine ("[5] Salir de su Cuenta \n");
                Console.WriteLine ("[0] Modo Administrador \n");
                Console.Write ("¿Qué desea realizar? ");
                opcion = int.Parse (Console.ReadLine ()!);
                
                //Opciones del Menu
                switch (opcion) {
                    
                    // Deposito
                    case 1: {

                        Console.Clear();
                        Console.WriteLine ("------DEPÓSITO DE EFECTIVO------");
                        Console.Write ("\n¿Cual es el monto a depositar a su cuenta? ");
                        double deposito = double.Parse(Console.ReadLine()!);

                        balances[index] += deposito;
                        balanceCaja += deposito;

                        Thread.Sleep (2500);
                        Console.WriteLine ("--------------------");
                        Console.WriteLine ("--Depósito exitoso--");
                        Console.WriteLine ("--------------------");

                        Thread.Sleep (2000);
                        Console.WriteLine ("\nPulse una tecla para continuar....");
                        Console.ReadKey ();
                        Console.Clear ();
                        resp = 's';

                        break;
                    }

                    // Retiro
                    case 2: {

                        Console.Clear();
                        Console.WriteLine ("------RETIRO DE EFECTIVO------");
                        Console.Write ("\n¿Cuanto quiere retirar? ");
                        double retiro = double.Parse(Console.ReadLine()!);

                        if ((balances[index] > retiro) && (balanceCaja > retiro)){

                            balances[index] -= retiro;
                            balanceCaja -= retiro;

                            Thread.Sleep (2500);
                            Console.WriteLine ("--------------------");
                            Console.WriteLine ("---Retiro exitoso---");
                            Console.WriteLine ("--------------------");

                            //Voucher
                            Thread.Sleep (2500);
                            Console.WriteLine ("\nPulse una tecla para continuar....");
                            Console.ReadKey ();
                            Console.Clear ();
                            Console.Write ("\n¿Desea obtener su voucher? (s/n): ");
                            string respuesta = Console.ReadLine ()!;

                            if ((respuesta == "s") || (respuesta == "S")) {

                                Console.Clear ();
                                Console.WriteLine ("Procesando solicitud......");
                                Thread.Sleep (2500);

                                Console.Clear ();
                                Console.WriteLine ("-----------------VOUCHER-----------------");
                                Console.WriteLine ($"Usuario: {nombres[index]}");
                                Console.WriteLine ($"Cuenta Usuario: {tarjetas[index]}");
                                Console.WriteLine ($"Monto Retirado: RD$ {retiro}");
                                Console.WriteLine ($"Hora: {hora}");
                                Console.WriteLine ($"Fecha: {fecha}");
                                Console.WriteLine ("-----------------------------------------");
                                
                                Thread.Sleep (2000);
                                Console.WriteLine ("Pulse una tecla para continuar....");
                                Console.ReadKey ();
                                Console.Clear ();
                                resp = 's';

                            }

                            else {

                                Console.Clear ();
                                Thread.Sleep (2000);
                                Console.WriteLine ("Pulse una tecla para continuar....");
                                Console.ReadKey ();
                                Console.Clear ();
                                resp = 's';

                            }

                            break;
                        }

                        else {

                            Console.Clear ();
                            Console.WriteLine ("------------------------");
                            Console.WriteLine ("--Fondos Insuficientes--");
                            Console.WriteLine ("------------------------");

                            Thread.Sleep (2000);
                            Console.WriteLine ("\nPulse una tecla para continuar....");
                            Console.ReadKey ();
                            Console.Clear ();
                            resp = 's';

                        }

                        break;
                    }

                    // Transferencia
                    case 3: {

                        Console.Clear();
                        Console.WriteLine ("------TRANSFERENCIA------");
                        Console.Write ("\nIngrese No. de tarjeta del Beneficiario: ");
                        string n_tarjeta = Console.ReadLine ()!;
                        
                        //Comprobacion de tarjeta
                        external_id = Array.IndexOf (tarjetas, n_tarjeta);
                        if (external_id == -1) {
                            
                            Console.Clear ();
                            Console.WriteLine ("No. de tarjeta inexistente en el sistema\n"); 

                            Thread.Sleep (2000);
                            Console.WriteLine ("Pulse una tecla para continuar....");
                            Console.ReadKey ();
                            Console.Clear ();
                            resp = 's';

                            break;
                        }

                        Console.Clear ();
                        Console.WriteLine ("Beneficiario: {0}", nombres[external_id]);
                        Console.Write ("Ingrese el monto a transferir: ");
                        double transferencia = double.Parse (Console.ReadLine ()!);

                        if (balances[index] > transferencia) {

                            balances[external_id] += transferencia;
                            balances[index] -= transferencia;

                            Thread.Sleep (3000);
                            Console.WriteLine ("----------------------------------");
                            Console.WriteLine ("--La transacción ha sido exitosa--");
                            Console.WriteLine ("----------------------------------");

                            //Voucher
                            Thread.Sleep (2000);
                            Console.WriteLine ("\nPulse una tecla para continuar....");
                            Console.ReadKey ();
                            Console.Clear ();
                            Console.Write ("\n¿Desea obtener su voucher? (s/n): ");
                            string respuesta = Console.ReadLine ()!;

                            if ((respuesta == "s") || (respuesta == "S")) {

                                Console.Clear ();
                                Console.WriteLine ("Procesando solicitud......");
                                Thread.Sleep (2500);

                                Console.Clear ();
                                Console.WriteLine ("-----------------VOUCHER-----------------");
                                Console.WriteLine ($"Beneficiario: {nombres[external_id]}");
                                Console.WriteLine ($"Cuenta Beneficiario: {n_tarjeta}");
                                Console.WriteLine ($"Monto Transferido: RD$ {transferencia}");
                                Console.WriteLine ($"Hora: {hora}");
                                Console.WriteLine ($"Fecha: {fecha}");
                                Console.WriteLine ("-----------------------------------------");
                                
                                Thread.Sleep (2000);
                                Console.WriteLine ("Pulse una tecla para continuar....");
                                Console.ReadKey ();
                                Console.Clear ();
                                resp = 's';

                            }

                            else {

                                Console.Clear ();
                                Thread.Sleep (2000);
                                Console.WriteLine ("Pulse una tecla para continuar....");
                                Console.ReadKey ();
                                Console.Clear ();
                                resp = 's';

                            }
                        }

                        else {
                            
                            Console.Clear ();
                            Console.WriteLine ("No posee el balance suficiente para realizar esta transacción\n");

                            Thread.Sleep (2000);
                            Console.WriteLine ("Pulse una tecla para continuar....");
                            Console.ReadKey ();
                            Console.Clear ();
                            resp = 's';

                        }
                        
                        break;
                    }

                    // Consultar Balance
                    case 4: {

                        Console.Clear();
                        Console.WriteLine ("------CONSULTA DE BALANCE------");
                        Console.WriteLine ("\nBalance Actual: ");
                        Console.WriteLine ($"RD$ {balances[index]}");

                        Thread.Sleep (2000);
                        Console.WriteLine ("\nPulse una tecla para continuar....");
                        Console.ReadKey ();
                        Console.Clear ();
                        resp = 's';

                        break;
                    }

                    // Salir de su Cuenta
                    case 5: {

                        Console.Clear ();
                        Console.WriteLine ("Cambiando de cuenta.....");
                        Thread.Sleep (2000);
                        Console.Clear();
                        resp = 'n';

                        break;
                    }   

                    // Modo Administrador
                    case 0: {

                        Console.Clear ();
                        Thread.Sleep (2000);
                        Console.WriteLine ("Acceso Restringido para Administrador de Cajero \n");
                        Console.Write ("Favor ingresar contraseña: ");
                        passwordCaja = Console.ReadLine()!;

                        if (passwordCaja == "admin") {
                            
                            do {

                                // Menu Administrador
                                Console.Clear ();
                                Console.WriteLine ("-------Modo Administrador-------\n");
                                Console.WriteLine ("[1] Consultar Saldo de Cajero");
                                Console.WriteLine ("[2] Reposición de Efectivo");
                                Console.WriteLine ("[3] Salir del Modo Admin\n");
                                Console.WriteLine ("[0] Apagar Cajero");
                                Console.Write ("\n¿Qué desea realizar? ");
                                opcion = int.Parse (Console.ReadLine ()!);

                                switch (opcion) {

                                    // Ver Saldo - Modo Administrador
                                    case 1: {

                                        Console.Clear ();
                                        Console.WriteLine ("Saldo Actual: ");
                                        Console.WriteLine ($"\nRD$ {balanceCaja}");

                                        Thread.Sleep (2000);
                                        Console.WriteLine ("\nPulse una tecla para continuar....");
                                        Console.ReadKey ();
                                        Console.Clear ();
                                        resp = 'o';

                                        break;
                                    }

                                    // Reponer Efectivo - Modo Administrador
                                    case 2: {
                                        
                                        Console.Clear ();
                                        Console.WriteLine ("------REPOSICIÓN DE EFECTIVO------");
                                        Console.Write ("\n¿Cual es el monto a reponer al cajero automático? ");
                                        double reposicion = double.Parse(Console.ReadLine()!);

                                        balanceCaja += reposicion;

                                        Thread.Sleep (2500);
                                        Console.WriteLine ("----------------------");
                                        Console.WriteLine ("--Reposición exitosa--");
                                        Console.WriteLine ("----------------------");

                                        Thread.Sleep (2000);
                                        Console.WriteLine ("\nPulse una tecla para continuar....");
                                        Console.ReadKey ();
                                        Console.Clear ();
                                        resp = 'o';

                                        break;
                                    }

                                    // Salir del modo - Modo Administrador
                                    case 3: {

                                        Console.Clear ();
                                        Console.WriteLine ("Saliendo del Modo Administrador.....");
                                        Thread.Sleep (3000);
                                        resp = 's';

                                        break;
                                    }

                                    // Apagar Cajero - Modo Administrador
                                    case 0: {

                                        Console.Clear ();
                                        Console.WriteLine ("Apagando Cajero Automático.....");
                                        Thread.Sleep (3000);
                                        Console.Clear ();

                                        return;

                                    }

                                    // Error! - Modo Administrador
                                    default: {

                                        Console.Clear ();
                                        Console.WriteLine ("La opción ingresada no existe. ¡Intente nuevamente! \n");

                                        Thread.Sleep (2000);
                                        Console.WriteLine ("Pulse una tecla para continuar....");
                                        Console.ReadKey ();
                                        Console.Clear ();
                                        resp = 'o';

                                        break;
                                    }
                                }

                            } while (resp == 'o');
                        }

                        // Contraseña Incorrecta - Modo Administrador
                        else {

                            Console.Clear ();
                            intentosCaja -= 1;
                            Console.WriteLine ("\nContraseña Incorrecta\n");
                            Console.WriteLine("Intentos Restantes: {0}", intentosCaja);
                            Thread.Sleep (1000);

                            //Cuenta regresiva del bloqueo - Modo Administrador                        
                            if (intentosCaja <= 0) {

                                for (int cuentaRegresiva = 25; cuentaRegresiva >= 0; cuentaRegresiva--){

                                    Console.Clear();
                                    Console.WriteLine ("|------CAJERO BLOQUEADO------|\n");
                                    Console.WriteLine ("|----------------------------|");
                                    Console.WriteLine ("|----------({0})-----------|",bloqueoCaja.ToString(@"mm\:ss"));
                                    Console.WriteLine ("|----------------------------|");
                                    bloqueoCaja = bloqueoCaja.Subtract(TimeSpan.FromSeconds(1));
                                    Thread.Sleep (1000);
                                    Console.Clear();
                                }

                                return;
                            }

                            Thread.Sleep (2000);
                            Console.WriteLine ("\nPulse una tecla para continuar....");
                            Console.ReadKey ();
                            Console.Clear ();
                            resp = 's';

                        }

                        break;
                    }             

                    //Error!
                    default: {
                        
                        Console.Clear ();
                        Console.WriteLine ("La opción ingresada no existe. ¡Intente nuevamente! \n");

                        Thread.Sleep (2000);
                        Console.WriteLine ("Pulse una tecla para continuar....");
                        Console.ReadKey ();
                        Console.Clear ();
                        resp = 's';

                        break;
                    }
                }

            } while ((resp == 's') || (resp == 'S')); 
        
        } while((resp == 'n') || (resp == 'N') );
    }
}