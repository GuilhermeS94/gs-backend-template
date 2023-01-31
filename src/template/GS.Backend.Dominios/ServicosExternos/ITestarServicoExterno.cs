using System;
using GS.Backend.Dominios.Modelos.Entradas;
using GS.Backend.Dominios.ServicosExternos.Saidas;

namespace GS.Backend.Dominios.ServicosExternos
{
    public interface ITestarServicoExterno
    {
        Task<TestarSaida> Processar(TestarEntrada entrada);
    }
}

