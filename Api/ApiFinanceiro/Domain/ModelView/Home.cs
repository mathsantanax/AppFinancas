using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinanceiro.Domain.ModelView
{
    public struct Home
    {
        public string Mensagem {get => "Bem vindo a api de finanças";}
        public string Doc {get => "/swagger/index.html";}
    }
}