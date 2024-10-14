using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.ModelView
{
    public record UserModelView
    {
        public string Email { get; set; } = default!;
        public string Password { get; set;} = default!;
    }
}