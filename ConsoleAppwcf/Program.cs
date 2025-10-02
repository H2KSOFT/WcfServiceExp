using ConsoleAppwcf.ServiceReferencewcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppwcf
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await ProgramAsync.MainAsync(args);

                #region WCF
                //Console.WriteLine("Conectando al servicio WCF...");

                //using (var client = new WcfOrdenServiceClient())
                //{
                //    var orden = new OrdenContrato
                //    {
                //        Cliente = "Cliente de Prueba",
                //        Fecha = DateTime.Now,
                //        Total = 150.75m,
                //        //Detalles = new DetalleOrdenContrato[]
                //        //{
                //        //    new DetalleOrdenContrato
                //        //    {
                //        //        Producto = "Producto A",
                //        //        Cantidad = 2,
                //        //        PrecioUnitario = 50.25m
                //        //    },
                //        //    new DetalleOrdenContrato
                //        //    {
                //        //        Producto = "Producto B",
                //        //        Cantidad = 1,
                //        //        PrecioUnitario = 50.25m
                //        //    }
                //        //}
                //    };

                //    Console.WriteLine("Enviando orden al servicio WCF...");

                //    var respuesta = client.RegistrarOrden(orden);

                //    if (respuesta.Exito)
                //    {
                //        Console.WriteLine($"Orden registrada exitosamente!");
                //        Console.WriteLine($"ID de orden: {respuesta.OrdenId}");
                //        Console.WriteLine($"Mensaje: {respuesta.Mensaje}");
                //    }
                //    else
                //    {
                //        Console.WriteLine($"Error: {respuesta.Mensaje}");
                //    }
                //}
                #endregion
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                Console.WriteLine($"Error de comunicación: {ex.Message}");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"Timeout: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();

        }

        
    }

    public class ProgramAsync
    {
        private static Timer _timer;
        private static readonly OrderProcessorAsync _orderProcessor = new OrderProcessorAsync();
        private static bool _isRunning = true;

        public static async Task MainAsync(string[] args)
        {
            Console.WriteLine("INICIANDO SIMULACIÓN");
            Console.WriteLine("====================================\n");

            _timer = new Timer(ExecuteProcessing, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            Console.WriteLine("Servicio iniciado. Procesando cada 10 segundos...");
            Console.WriteLine("Presiona [Q] para salir\n");

            while (_isRunning)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    _isRunning = false;
                }
                await Task.Delay(100);
            }

            _timer.Dispose();
            Console.WriteLine("\n Servicio detenido");
        }

        private static async void ExecuteProcessing(object state)
        {
            try
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Iniciando procesamiento...");
                await _orderProcessor.ProcessPendingOrdersAsync();
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Procesamiento completado\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Error: {ex.Message}\n");
            }
        }

        public class OrderProcessorAsync
        {
            private int _processedCount = 0;

            public async Task ProcessPendingOrdersAsync()
            {
                await Task.Delay(800);

                var random = new Random();
                var ordersToProcess = random.Next(1, 5);

                for (int i = 0; i < ordersToProcess; i++)
                {
                    await Task.Delay(200);
                    _processedCount++;
                    Console.WriteLine($"Orden #{_processedCount} marcada como procesada");
                }

                Console.WriteLine($"Total órdenes procesadas: {_processedCount}");
            }
        }
    }


}
