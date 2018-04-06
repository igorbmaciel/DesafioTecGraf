using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DesafioTecGraf.Domain.Domain;
using DesafioTecGraf.Domain.Interface;

namespace DesafioTecGraf.Domain.Service
{
    public class MatriculaService : IMatriculaService
    {
        public Dictionary<string, int> LerArquivo(string source)
        {
            var text = "";

            var uri = new Uri(source);

           if (uri.IsFile)
            {
                using (var reader = new StreamReader(uri.LocalPath))
                {
                    text = reader.ReadToEnd();
                    reader.Close();
                }
            }

            var words = text.Split(' ', ',', ';', '.', '!', '?', '\r', '\n', '\t', ':', '/', '(', ')', '{', '}', '[', ']', '*', '#');

            var result = words
                .GroupBy(w => w)
                .Select(intermediate => new
                {
                    Word = intermediate.Key,
                    Frequency = intermediate.Sum(w => 1)
                })
                .ToDictionary(x => x.Word, x => x.Frequency);

            return result;
        }

        public Matriculas CalcularDigito(string matricula)
        {
            var somamatricula = matricula.ToCharArray(0, 4)
                .Select(dados => Convert.ToInt16(dados.ToString()
                .Substring(0, 1)) * (5 - matricula.ToString().IndexOf(dados, 0)))
                .ToList();

            var matriculaDigito = somamatricula.Sum() % 16;

            var matriculaDigitoHexaDecimal = matriculaDigito.ToString("X");

            var matriculaComDigito = $"{matricula}-{matriculaDigitoHexaDecimal}";

            var matriculas = new Matriculas()
            {
                Matricula = matricula,
                MatriculaDigito = matriculaComDigito
            };

            return matriculas;
        }

        public bool ValidatMatriculas(string matricula)
        {
            var matriculas = matricula.Substring(0, 4);

            var matriculaDigito = CalcularDigito(matriculas);

            var validarMatricula = matriculaDigito.MatriculaDigito.Equals(matricula);

            return validarMatricula;

        }

        public void CriarTxt(List<string> texto, string filename)
        {

            var solutionpath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            var path = $@"{solutionpath}\Arquivos\{filename}";

            if (!File.Exists(path))
            {
                File.Create(path).Close();

                using (var tw = new StreamWriter(path))
                {
                    foreach (var dados in texto)
                        tw.WriteLine(dados);

                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {

                File.WriteAllText(path, String.Empty);

                using (var tw = new StreamWriter(path, true))
                {
                    foreach (var dados in texto)
                        tw.WriteLine(dados);

                    tw.Close();
                }
            }
        }
    }
}
