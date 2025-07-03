using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenguajeExamen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================\n");
            Console.WriteLine("Sistema de Gestión de Vehículos Eléctricos");
            Console.WriteLine("==========================================\n");

            // Crear lista de vehículos
            List<VehiculoElectrico> vehiculos = new List<VehiculoElectrico>
        {
            new AutoElectrico("BMW", "iX3", 400, 15),
            new MotocicletaElectrica("Zero", "DSR", 200, 18),
            new BicicletaElectrica("Specialized", "Turbo", 80, 5),
            new AutoElectrico("Nissan", "Leaf", 300, 85),
            new MotocicletaElectrica("Harley", "LiveWire", 180, 60),
            new BicicletaElectrica("Scott", "Sub Cross", 90, 75)
        };

            // Mostrar vehículos
            Console.WriteLine("LISTA DE VEHÍCULOS:");
            foreach (var vehiculo in vehiculos)
            {
                Console.WriteLine($"{vehiculo.GetType().Name}: {vehiculo.Marca} {vehiculo.Modelo} - Batería: {vehiculo.CargaActual}%");
            }

            Console.WriteLine("\n" + new string('-', 50));

            // PARADIGMA FUNCIONAL
            Console.WriteLine("\n1. VEHÍCULOS CON MENOS DEL 20% DE BATERÍA:");
            var vehiculosBateriaBaja = vehiculos.Where(v => v.CargaActual < 20);
            foreach (var vehiculo in vehiculosBateriaBaja)
            {
                Console.WriteLine($"   {vehiculo.Marca} {vehiculo.Modelo} - {vehiculo.CargaActual}%");
            }

            Console.WriteLine("\n2. MARCAS CON MÁS DEL 50% DE BATERÍA:");
            var marcasAlta = vehiculos.Where(v => v.CargaActual > 50).Select(v => v.Marca).Distinct();
            foreach (var marca in marcasAlta)
            {
                Console.WriteLine($"   {marca}");
            }

            Console.WriteLine("\n3. CONSUMO TOTAL DE LA FLOTA:");
            double consumoTotal = vehiculos.Sum(v => v.ConsumoPorKm());
            Console.WriteLine($"   Consumo total por km: {consumoTotal}%");

            Console.WriteLine("\n" + new string('-', 50));

            // MOTOR DE INFERENCIA
            Console.WriteLine("\nSISTEMA DE INFERENCIA PARA VEHÍCULOS:");
            var motor = new MotorDeInferencia();

            // Agregar hechos basados en los vehículos
            foreach (var vehiculo in vehiculos)
            {
                motor.Hechos.Add(new Hecho { Nombre = $"vehiculo_{vehiculo.Marca}_{vehiculo.Modelo}", Valor = vehiculo });

                if (vehiculo.CargaActual < 20)
                    motor.Hechos.Add(new Hecho { Nombre = "bateria_critica", Valor = vehiculo });

                if (vehiculo.CargaActual > 80)
                    motor.Hechos.Add(new Hecho { Nombre = "bateria_alta", Valor = vehiculo });
            }

            // Definir reglas
            motor.Reglas.Add(new Regla
            {
                Condicion = hechos => hechos.Any(h => h.Nombre == "bateria_critica"),
                Accion = () => Console.WriteLine(" ALERTA: Hay vehículos con batería crítica que necesitan carga urgente.")
            });

            motor.Reglas.Add(new Regla
            {
                Condicion = hechos => hechos.Any(h => h.Nombre == "bateria_alta"),
                Accion = () => Console.WriteLine(" BIEN: Hay vehículos con batería alta listos para uso prolongado.")
            });

            motor.Reglas.Add(new Regla
            {
                Condicion = hechos => hechos.Count(h => h.Nombre == "bateria_critica") >= 2,
                Accion = () => Console.WriteLine(" CRÍTICO: Múltiples vehículos necesitan carga inmediata.")
            });

            // Ejecutar motor de inferencia
            motor.Ejecutar();

            Console.WriteLine("\n" + new string('-', 50));

            // INFERENCIA BÁSICA
            Console.WriteLine("\nINFERENCIA BÁSICA:");

            // Funciones de inferencia simple
            bool PuedeHacerViajeCorto(VehiculoElectrico v) => v.CargaActual > 30;
            bool PuedeHacerViajeLargo(VehiculoElectrico v) => v.CargaActual > 70;
            bool NecesitaCarga(VehiculoElectrico v) => v.CargaActual < 25;

            foreach (var vehiculo in vehiculos)
            {
                Console.WriteLine($"\n{vehiculo.Marca} {vehiculo.Modelo}:");
                Console.WriteLine($"   Viaje corto: {PuedeHacerViajeCorto(vehiculo)}");
                Console.WriteLine($"   Viaje largo: {PuedeHacerViajeLargo(vehiculo)}");
                Console.WriteLine($"   Necesita carga: {NecesitaCarga(vehiculo)}");
            }

            Console.WriteLine("\n" + new string('-', 50));

            // SISTEMA DE RECOMENDACIONES
            Console.WriteLine("\nSISTEMA DE RECOMENDACIONES:");

            foreach (var vehiculo in vehiculos)
            {
                string recomendacion = EvaluarVehiculo(vehiculo);
                Console.WriteLine($"{vehiculo.Marca} {vehiculo.Modelo}: {recomendacion}");
            }

            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static string EvaluarVehiculo(VehiculoElectrico v)
        {
            int puntajeRiesgo = 0;
            List<string> razones = new List<string>();

            if (v.CargaActual < 20)
            {
                puntajeRiesgo += 3;
                razones.Add("Batería crítica");
            }
            else if (v.CargaActual < 40)
            {
                puntajeRiesgo += 1;
                razones.Add("Batería baja");
            }

            if (v.ConsumoPorKm() > 2.5)
            {
                puntajeRiesgo += 1;
                razones.Add("Alto consumo");
            }

            if (v.AutonomiaMaxima < 100)
            {
                puntajeRiesgo += 1;
                razones.Add("Autonomía limitada");
            }

            if (puntajeRiesgo >= 3)
            {
                return $" NO RECOMENDADO - {string.Join(", ", razones)}";
            }
            else if (puntajeRiesgo >= 2)
            {
                return $"  PRECAUCIÓN - {string.Join(", ", razones)}";
            }
            else if (puntajeRiesgo >= 1)
            {
                return $" ACEPTABLE - {string.Join(", ", razones)}";
            }
            else
            {
                return " EXCELENTE - Listo para usar";
            }
        }
    }
}
