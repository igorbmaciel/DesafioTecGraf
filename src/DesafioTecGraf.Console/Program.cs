using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioTecGraf.Domain.Domain;
using DesafioTecGraf.Domain.Service;

namespace DesafioTecGraf.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var filename = "";
            var path = "";
            var matriculaService = new MatriculaService();
            var solutionpath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            filename = "matriculasSemDV.txt";

            path = $@"{solutionpath}\Arquivos\{filename}";

            System.Console.WriteLine($"Lendo o Arquivo {filename}");

            var matriculasSemDv = matriculaService.LerArquivo(path);

            System.Console.WriteLine($"Calculando as matrículas com DV");

            var matriculasComDv = (from matricula in matriculasSemDv
                                   where !matricula.Key.Equals("")
                                   select matriculaService.CalcularDigito(matricula.Key) 
                                   into matriculas select matriculas.MatriculaDigito)
                                   .ToList();

            filename = "matriculasComDV.txt";

            System.Console.WriteLine($"Criando o Arquivo {filename}");

            matriculaService.CriarTxt(matriculasComDv, filename);

            filename = "matriculasParaVerificar.txt";

            path = $@"{solutionpath}\Arquivos\{filename}";

            System.Console.WriteLine($"Lendo o Arquivo {filename}");

            var matriculasValidar = matriculaService.LerArquivo(path);

            System.Console.WriteLine($"Validando as matrículas com DV");

            var validarMatriculas = (from matricula in matriculasValidar
                                     where !matricula.Key.Equals("")
                                    select matriculaService.ValidatMatriculas
                                    (matricula.Key)
                                    into matriculas
                                    select matriculas.ToString())
                                    .ToList();

            filename = "matriculasVerificadas.txt";

            System.Console.WriteLine($"Criando o Arquivo {filename}");

            matriculaService.CriarTxt(validarMatriculas, filename);

            System.Console.ReadKey();

        }
    }
}
