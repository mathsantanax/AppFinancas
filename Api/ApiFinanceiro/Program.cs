using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;
using ApiFinanceiro.Domain.Interfaces;
using ApiFinanceiro.Domain.ModelView;
using ApiFinanceiro.Domain.Servicos;
using ApiFinanceiro.Infraestrutura.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IValoresEntrada, ValoresEntradaService>();
builder.Services.AddScoped<IValoresSaida, ValoresSaidaService>();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlString")
    );
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

#region Validação de DTO's

    ErrosDeValidacao validacaoDTO (ValoresDTO valoresDTO)
    {
        var valida = new ErrosDeValidacao{
            Mensagens = new List<string>()
        };

        if(string.IsNullOrEmpty(valoresDTO.Date.ToString()))
            valida.Mensagens.Add("A data não pode ser vazia !");
        
        if(string.IsNullOrEmpty(valoresDTO.Valor.ToString()))
            valida.Mensagens.Add("O valor não poder ser vazio! ");

        if(string.IsNullOrEmpty(valoresDTO.Descricao))
            valida.Mensagens.Add("Descrição não pode ser vazia!");

        if(string.IsNullOrEmpty(valoresDTO.Tipo.ToString()))
            valida.Mensagens.Add("O Tipo não poder ser vazio! ");

        if(string.IsNullOrEmpty(valoresDTO.Categoria.ToString()))
            valida.Mensagens.Add("O Categoria não poder ser vazio! ");

        return valida;
    }

#endregion


#region Finanças api
app.MapPost("/Financeiro",([FromBody] ValoresDTO valoresDTO, IValoresEntrada valoresEntradaService, IValoresSaida valoresSaidaService) =>
{
    var validacao = validacaoDTO(valoresDTO);

    if(validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    if(valoresDTO.Tipo == Tipo.Entrada)
    {
        var valor = new ValoresEntrada
        {
            Date = valoresDTO.Date.Date,
            Descricao = valoresDTO.Descricao,
            Valor = valoresDTO.Valor,
            Tipo = valoresDTO.Tipo.ToString() ?? Tipo.Saida.ToString(),
            Categoria = valoresDTO.Categoria.ToString() ?? Categoria.Casa.ToString(),
            IdUser = valoresDTO.IdUser
        };

        valoresEntradaService.Incluir(valor);
        return Results.Created($"/Financeiro/{valor.Id}", valor);
    }
    else if(valoresDTO.Tipo == Tipo.Saida)
    {   
        var valor = new ValoresSaida
        {
            Date = valoresDTO.Date.Date,
            Descricao = valoresDTO.Descricao,
            Valor = valoresDTO.Valor,
            Tipo = valoresDTO.Tipo.ToString() ?? Tipo.Saida.ToString(),
            Categoria = valoresDTO.Categoria.ToString() ?? Categoria.Casa.ToString(),
            IdUser = valoresDTO.IdUser
        };
        valoresSaidaService.Incluir(valor);
        return Results.Created($"/Financeiro/{valor.Id}", valor);
    }

    return Results.Ok();

}).WithName("CriarValor")
.WithOpenApi();

// app.MapGet("Financeiro", ([FromQuery] int? pagina, IValores valoresService) => {
//     var valores = valoresService.BuscarTodos(pagina);

//     return Results.Ok(valores);
// }).WithName("Financeiro")
// .WithOpenApi();

// app.MapGet("/Financeiro/Tipo/{tipo}", ([FromRoute] Tipo tipo, IValores valoresService) => {

//     var valores = valoresService.BuscarPorTipo(tipo);
//     if(valores == null)
//         return Results.NotFound();

//     return Results.Ok(valores);
// }).WithName("BuscarPorTipo")
// .WithOpenApi();

// app.MapGet("Financeiro/Categoria/{categoria}", ([FromRoute] Categoria categoria, IValores valoresService) => {
//     var valores = valoresService.BuscarPorCategoria(categoria);
//     if(valores == null) return Results.NotFound();

//     return Results.Ok(valores);
// }).WithName("BuscarPorCategoria")
// .WithOpenApi();

// app.MapGet("Financeiro/Data/{Date}", ([FromRoute] DateTime Date, IValores valoresService) => {
//     var valores = valoresService.BuscarPorData(Date);

//     if(valores == null || !valores.Any()) return Results.NotFound("Nenhum valor encontrado para a data especidifcada! ");

//     return Results.Ok(valores);
// }).WithName("BuscarPorData")
// .WithOpenApi();

// app.MapDelete("Financeiro/Delete/{id}", ([FromRoute] int id, IValores ValoresServicos) => {
//     var valores = ValoresServicos.BuscarPorId(id);
//     if(valores == null) return Results.NotFound("Não foi possível localizar a transação! ");

//     //Apagando a Transação
//     ValoresServicos.Apagar(valores);
//     return Results.Ok(valores);
// });

#endregion

app.Run();