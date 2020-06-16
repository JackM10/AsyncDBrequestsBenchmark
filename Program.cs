using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

namespace AsyncDBrequestsBenchmark
{
    class Program
    {
        static async Task Main()
        {
            var summary =
                BenchmarkRunner
                    .Run<MyBench>(); //to instal Benchmark run in Nuget Console : 'Install-Package BenchmarkDotNet'
            Console.WriteLine(summary);
            Console.ReadLine();
        }
    }

    public class MyBench
    {
        [Benchmark]
        public async Task<int> SynchronioslyEachCall()
        {
            using var db = new PersonContext();
            var persons = db.Persons.ToList();
            var entities = db.Entities.ToList();
            var pcs = db.Pcs.ToList();

            return persons.Count + entities.Count + pcs.Count;
        }

        [Benchmark]
        public async Task<int> AsyncEachCall()
        {
            using var db = new PersonContext();
            var persons = await db.Persons.ToListAsync();
            var entities = await db.Entities.ToListAsync();
            var pcs = await db.Pcs.ToListAsync();

            return persons.Count + entities.Count + pcs.Count;
        }

        [Benchmark]
        public async Task<int> AsyncIndependantCallsWaitAll()
        {
            using var dbPersons = new PersonContext();
            using var dbEntities = new PersonContext();
            using var dbPcs = new PersonContext();
            var persons = dbPersons.Persons.ToListAsync();
            var entities = dbEntities.Entities.ToListAsync();
            var pcs = dbPcs.Pcs.ToListAsync();

            Task.WaitAll(persons, entities, pcs);

            return persons.Result.Count + entities.Result.Count + pcs.Result.Count;
        }

        [Benchmark]
        public async Task<int> AsyncIndependantCallsWithAwaiters()
        {
            using var dbPersons = new PersonContext();
            using var dbEntities = new PersonContext();
            using var dbPcs = new PersonContext();
            var persons = dbPersons.Persons.ToListAsync();
            var entities = dbEntities.Entities.ToListAsync();
            var pcs = dbPcs.Pcs.ToListAsync();

            return persons.Result.Count + entities.Result.Count + pcs.Result.Count;
        }
    }

    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Entitie> Entities { get; set; }
        public DbSet<Pc> Pcs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=test;Integrated Security=True;persist security info=True;MultipleActiveResultSets=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }

    public class Person
    {
        [Required] public int Id { get; set; }

        [Required] [MaxLength(50)] public string FirstName { get; set; }

        public string LstName { get; set; }

        public string Addresses { get; set; }
    }


    public class Entitie
    {
        [Required] public int Id { get; set; }

        [Required] [MaxLength(50)] public string FirstName { get; set; }

        public string LstName { get; set; }

        public string Addresses { get; set; }
    }

    public class Pc
    {
        [Required] public int Id { get; set; }

        [Required] [MaxLength(50)] public string FirstName { get; set; }

        public string LstName { get; set; }

        public string Addresses { get; set; }
    }
}
