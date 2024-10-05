using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.ModelView
{
    public record UserModelView
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}