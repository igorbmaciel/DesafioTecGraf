using DesafioTecGraf.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecGraf.Domain.Interface
{
    public interface IMatriculaService
    {
        Dictionary<string, int> LerArquivo(string source);
        Matriculas CalcularDigito(string matricula);
        bool ValidatMatriculas(string matricula);
        void CriarTxt(List<string> texto, string filename);
    }
}
