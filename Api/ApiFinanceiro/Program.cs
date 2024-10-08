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
builder.Services.AddScoped<IUsers, UsersService>();

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

#region Usuario

app.MapPost("/Usuario/Incluir", ([FromBody] UserDTO userDto, [FromServices] IUsers usersService) => {
    if(userDto == null) return Results.BadRequest("No content");
    var user = new User{
        Name = userDto.Name,
        Email = userDto.Email,
    };

    usersService.Incluir(user);
    return Results.Created($"/Usuario/{user.Id}", user);
}).WithTags("Usuario")
.WithName("IncluirUsuario")
.WithOpenApi();

app.MapGet("/Usuario/Listar", ([FromServices] IUsers usersService) => {
    
    var user = usersService.ListarUsuarios();
    return Results.Ok(user);
})
.WithName("ListarUsuarios")
.WithTags("Usuario")
.WithOpenApi();

app.MapGet("/Usuario/BuscarPorEmail{email}", ([FromRoute] string email, [FromServices] IUsers usersService) => {

    var user = usersService.BuscarPorEmail(email);

    if(user == null) return Results.NotFound();
    return Results.Ok(user);
})
.WithName("BuscarPorEmail")
.WithTags("Usuario")
.WithOpenApi();

app.MapGet("/Usuario/BuscarPorId/{id}", ([FromRoute] int id, [FromServices] IUsers usersSerivce) => 
{
    var user = usersSerivce.BuscarPorId(id);
    if(user == null) return Results.NotFound();

    return Results.Ok(user);

})
.WithName("BuscarPorId")
.WithTags("Usuario")
.WithOpenApi();

app.MapDelete("/Usuario/Delete/{id}", ([FromRoute] int id, IUsers usersService) => 
{
    var user = usersService.BuscarPorId(id);
    if(user == null) return Results.NotFound();

    usersService.Deletar(user);

    return Results.NoContent();
})
.WithName("BuscarDeletar")
.WithTags("Usuario")
.WithOpenApi();


#endregion


#region Finanças api
app.MapPost("/Financeiro", ([FromBody] ValoresDTO valoresDTO, IValoresEntrada valoresEntradaService, IValoresSaida valoresSaidaService) =>
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
.WithTags("Finance")
.WithOpenApi();

app.MapGet("/GetId/{categoria}", ([FromBody] Categoria categoria, [FromServices] IValoresEntrada valorEntrada, [FromServices] IValoresSaida  valorSaida) => {
    var valoresEntrada = valorEntrada.BuscarPorCategoria(categoria);
    var valoresSaida = valorSaida.BuscarPorCategoria(categoria);

    if (valoresEntrada == null) return Results.NotFound();
    if (valoresSaida == null) return Results.NotFound();

    return Results.Ok($"{valoresEntrada}  {valoresSaida}");
})
.WithName("BuscaId")
.WithTags("Finance")
.WithOpenApi();

#endregion

app.Run();




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
