using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bogus;

namespace ProdutoCartesiano
{
    class Program
    {
        static void Main(string[] args)
        {
            const int quantidadeOperacao = 11;
            const int id = 1;

            var context = new ApplicationContext();

            #region Popular banco de dados
            //context.Database.EnsureDeleted();

            //context.Database.EnsureCreated();

            //var empresa = new Empresa
            //{
            //    Nome = new Faker("pt_BR").Company.CompanyName(),
            //    Funcionarios = new List<Funcionario>(),
            //    Departamentos = new List<Departamento>(),
            //    Contatos = new List<Contato>()
            //};

            //for (int i = 0; i < 1000; i++)
            //{
            //    empresa.Funcionarios.Add(new Funcionario { Nome = new Faker("pt_BR").Person.FullName });
            //    empresa.Departamentos.Add(new Departamento { Nome = new Faker("pt_BR").Lorem.Word() });
            //}

            //empresa.Contatos.Add(new Contato { Valor = new Faker("pt_BR").Phone.PhoneNumber() });
            //empresa.Contatos.Add(new Contato { Valor = new Faker("pt_BR").Phone.PhoneNumber() });

            //context.Empresas.Add(empresa);
            //context.SaveChanges();
            #endregion

            #region Consulta 1
            //for (var i = 0; i < quantidadeOperacao; i++)
            //{
            //    var sw = Stopwatch.StartNew();

            //    var empresa = context.Empresas
            //        .Include(e => e.Funcionarios)
            //        .Include(e => e.Departamentos)
            //        .Include(e => e.Contatos)
            //        .FirstOrDefault(e => e.Id == id);

            //    sw.Stop();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
            //    context.ChangeTracker.Clear();
            //}
            #endregion

            #region Consulta 2
            //for (var i = 0; i < quantidadeOperacao; i++)
            //{
            //    var sw = Stopwatch.StartNew();

            //    var empresa = context.Empresas
            //        .Include(d => d.Funcionarios)
            //        .Include(d => d.Departamentos)
            //        .Include(d => d.Contatos)
            //        .AsSplitQuery()
            //        .FirstOrDefault(e => e.Id == id);

            //    sw.Stop();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
            //    context.ChangeTracker.Clear();
            //}
            #endregion

            #region Consulta 3
            //for (var i = 0; i < quantidadeOperacao; i++)
            //{
            //    var sw = Stopwatch.StartNew();

            //    var empresa = context.Empresas
            //        .FirstOrDefault(e => e.Id == id);

            //    context.Entry(empresa).Collection(e => e.Departamentos).Load();
            //    context.Entry(empresa).Collection(e => e.Funcionarios).Load();
            //    context.Entry(empresa).Collection(e => e.Contatos).Load();

            //    sw.Stop();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
            //    context.ChangeTracker.Clear();
            //}
            #endregion

            #region Consulta 4
            //const string query = @"
            //    SELECT *
            //    FROM Empresas e
            //    INNER JOIN Funcionarios f ON f.EmpresaId = e.Id
            //    INNER JOIN Departamentos d ON d.EmpresaId = e.Id
            //    INNER JOIN Contatos c ON c.EmpresaId = e.Id
            //    WHERE e.Id = @id;";

            //for (var i = 0; i < quantidadeOperacao; i++)
            //{
            //    var sw = Stopwatch.StartNew();
            //    var empresas = new Dictionary<int, Empresa>();
            //    var departamentos = new HashSet<int>();
            //    var funcionarios = new HashSet<int>();
            //    var contatos = new HashSet<int>();
            //    using var connection = new SqlConnection(Conexao.StringConexao);
                
            //    var result = connection.Query<Empresa, Funcionario, Departamento, Contato, Empresa>(query, (e, f, d, c) =>
            //    {
            //        if (!empresas.TryGetValue(e.Id, out var empresaOut))
            //        {
            //            e.Funcionarios = new List<Funcionario>();
            //            e.Departamentos = new List<Departamento>();
            //            e.Contatos = new List<Contato>();
            //            empresas.Add(e.Id, empresaOut = e);
            //        }

            //        if (!funcionarios.Contains(f.Id))
            //        {
            //            funcionarios.Add(f.Id);
            //            empresaOut.Funcionarios.Add(f);
            //        }

            //        if (!departamentos.Contains(d.Id))
            //        {
            //            departamentos.Add(d.Id);
            //            empresaOut.Departamentos.Add(d);
            //        }

            //        if (!contatos.Contains(c.Id))
            //        {
            //            contatos.Add(c.Id);
            //            empresaOut.Contatos.Add(c);
            //        }

            //        return empresaOut;
            //    }, new { id });

            //    var empresa = result.FirstOrDefault();

            //    sw.Stop();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
            //}
            #endregion

            #region Consulta 5
            //var query = @"
            //    SELECT *
            //    FROM Empresas
            //    WHERE Id = @id;

            //    SELECT * FROM Funcionarios WHERE EmpresaId = @id;

            //    SELECT * FROM Departamentos WHERE EmpresaId = @id;

            //    SELECT * FROM Contatos WHERE EmpresaId = @id";

            //for (var i = 0; i < quantidadeOperacao; i++)
            //{
            //    var sw = Stopwatch.StartNew();

            //    using var connection = new SqlConnection(Conexao.StringConexao);

            //    using var reader = connection.QueryMultiple(query, new { id });

            //    var empresa = reader.Read<Empresa>().FirstOrDefault();
            //    empresa.Funcionarios = reader.Read<Funcionario>().ToList();
            //    empresa.Departamentos = reader.Read<Departamento>().ToList();
            //    empresa.Contatos = reader.Read<Contato>().ToList();

            //    sw.Stop();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
            //}
            #endregion
        }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
                .UseSqlServer(Conexao.StringConexao)
                .LogTo(Console.WriteLine, LogLevel.Information)
        ;
    }

    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
        public ICollection<Contato> Contatos { get; set; }
    }

    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Empresa Empresa { get; set; }
    }

    public class Contato
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public Empresa Empresa { get; set; }
    }

    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Empresa Empresa { get; set; }
    }

    public static class Conexao
    {
        public static string StringConexao =
            "sua-string-de-conexao";
    }
}
