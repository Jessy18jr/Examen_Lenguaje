using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenguajeExamen
{
    public abstract class VehiculoElectrico
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public double AutonomiaMaxima { get; set; }
        public double CargaActual { get; set; }

        public VehiculoElectrico(string marca, string modelo, double autonomiaMaxima, double cargaActual)
        {
            Marca = marca;
            Modelo = modelo;
            AutonomiaMaxima = autonomiaMaxima;
            CargaActual = cargaActual;
        }

        public virtual void CargarBateria()
        {
            CargaActual = 100;
        }

        public abstract double ConsumoPorKm();
    }

    // Subclases
    public class AutoElectrico : VehiculoElectrico
    {
        public AutoElectrico(string marca, string modelo, double autonomiaMaxima, double cargaActual)
            : base(marca, modelo, autonomiaMaxima, cargaActual) { }

        public override void CargarBateria()
        {
            CargaActual += 10;
            if (CargaActual > 100) CargaActual = 100;
        }

        public override double ConsumoPorKm()
        {
            return 3.0;
        }
    }

    public class MotocicletaElectrica : VehiculoElectrico
    {
        public MotocicletaElectrica(string marca, string modelo, double autonomiaMaxima, double cargaActual)
            : base(marca, modelo, autonomiaMaxima, cargaActual) { }

        public override void CargarBateria()
        {
            CargaActual += 15;
            if (CargaActual > 100) CargaActual = 100;
        }

        public override double ConsumoPorKm()
        {
            return 2.0;
        }
    }

    public class BicicletaElectrica : VehiculoElectrico
    {
        public BicicletaElectrica(string marca, string modelo, double autonomiaMaxima, double cargaActual)
            : base(marca, modelo, autonomiaMaxima, cargaActual) { }

        public override void CargarBateria()
        {
            CargaActual += 20;
            if (CargaActual > 100) CargaActual = 100;
        }

        public override double ConsumoPorKm()
        {
            return 1.0;
        }
    }

    // Motor de inferencia para vehículos
    class Hecho
    {
        public string Nombre { get; set; }
        public object Valor { get; set; }
    }

    class Regla
    {
        public Func<List<Hecho>, bool> Condicion { get; set; }
        public Action Accion { get; set; }
    }

    class MotorDeInferencia
    {
        public List<Hecho> Hechos = new List<Hecho>();
        public List<Regla> Reglas = new List<Regla>();

        public void Ejecutar()
        {
            foreach (var regla in Reglas)
            {
                if (regla.Condicion(Hechos))
                {
                    regla.Accion();
                }
            }
        }
    }

    
}
