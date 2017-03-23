using System;
using System.Configuration;
using System.Data.Entity;

namespace Model
{
    public class DataContext:DbContext
    {
        public DataContext():base(ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString)
        {
            Database.SetInitializer<DataContext>(new InitializeDatabase());
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
    }

    public class InitializeDatabase : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.Departamentos.Add(new Departamento()
            {
                Nombre = "Sistemas"
            });
            context.SaveChanges();
            context.Puestos.Add(new Puesto() {Nombre = "Gerente"});
            context.Puestos.Add(new Puesto() { Nombre = "Soporte Técnico"});
            context.SaveChanges();
            context.Empleados.Add(new Empleado()
            {
                Nombres = "Rene Evaristo",
                Apellidos = "Cruz Ballinas",
                Edad = 37,
                PuestoId = 1,
                DepartamentoId = 1,
                FechaContratacion = new DateTime(2013,03,19)
            });

            context.Empleados.Add(new Empleado()
            {
                Nombres = "Rene Emmanuel",
                Apellidos = "Zamorano Flores",
                Edad = 32,
                PuestoId = 2,
                DepartamentoId = 1,
                FechaContratacion = new DateTime(2014, 09, 03)
            });

            context.Empleados.Add(new Empleado()
            {
                Nombres = "Gerardo Raymundo",
                Apellidos = "Reyes Castillo",
                Edad = 32,
                PuestoId = 2,
                DepartamentoId = 1,
                FechaContratacion = new DateTime(2016,08,15)
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}