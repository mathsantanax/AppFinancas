using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;
using ApiFinanceiro.Domain.Interfaces;
using ApiFinanceiro.Domain.ModelView;
using ApiFinanceiro.Domain.Servicos;
using ApiFinanceiro.Infraestrutura.Db;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; set; } = default!;

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddScoped<IUsers, UsersService>();
        services.AddScoped<IValores, ValoresService>();

        services.AddDbContext<DbContexto>(options =>{
            options.UseSqlServer(Configuration.GetConnectionString("SqlString"));
        });

    }
    public void Configure(IApplicationBuilder app){
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseEndpoints(endpoints => {
            #region Home 
            endpoints.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("HOme");
            #endregion

            endpoints.MapPost("/Usuario", ([FromBody] UserDTO userDTO, [FromServices] IUsers usersService) => {
                var logar = usersService.Logar(userDTO);

                if(logar != null)
                {
                    return Results.Ok(new UserModelView{
                        Email = userDTO.Email,
                        Password = userDTO.Password,
                        Token = "0"
                    });
                }
                else 
                    return Results.Unauthorized();
                })
                .WithTags("Usuario")
                .WithDescription("Logar Usuario")
                .WithOpenApi();
                
        });
    }
}

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<IUsers, UsersService>();
// builder.Services.AddScoped<IValores, ValoresService>();

// builder.Services.AddDbContext<DbContexto>(options => {
//     options.UseSqlServer(
//         builder.Configuration.GetConnectionString("SqlString")
//     );
// });

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAllOrigins",
//         builder => builder.AllowAnyOrigin()
//                           .AllowAnyMethod()
//                           .AllowAnyHeader());
// });

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }


// app.UseHttpsRedirection();
// app.UseCors("AllowAllOrigins");
// #region Validação de DTO's

//     ErrosDeValidacao validacaoDTO (ValoresDTO valoresDTO)
//     {
//         var valida = new ErrosDeValidacao{
//             Mensagens = new List<string>()
//         };

//         if(string.IsNullOrEmpty(valoresDTO.Date.ToString()))
//             valida.Mensagens.Add("A data não pode ser vazia !");
        
//         if(string.IsNullOrEmpty(valoresDTO.Valor.ToString()))
//             valida.Mensagens.Add("O valor não poder ser vazio! ");

//         if(string.IsNullOrEmpty(valoresDTO.Descricao))
//             valida.Mensagens.Add("Descrição não pode ser vazia!");

//         if(string.IsNullOrEmpty(valoresDTO.Tipo.ToString()))
//             valida.Mensagens.Add("O Tipo não poder ser vazio! ");

//         if(string.IsNullOrEmpty(valoresDTO.Categoria.ToString()))
//             valida.Mensagens.Add("O Categoria não poder ser vazio! ");

//         return valida;
//     }

// #endregion

// #region Usuario

// app.MapPost("/Usuario", ([FromBody] UserDTO userDTO, [FromServices] IUsers usersService) => {
//     var logar = usersService.Logar(userDTO);

//     if(logar != null)
//     {
//         return Results.Ok(new UserModelView{
//             Email = userDTO.Email,
//             Password = userDTO.Password,
//             Token = "0"
//         });
//     }
//     else 
//         return Results.Unauthorized();
// })
// .WithTags("Usuario")
// .WithDescription("Logar Usuario")
// .WithOpenApi();

// #endregion // fim região user


// #region Finance

// app.MapPost("Finance/Post", ([FromBody] ValoresDTO valoresDTO, [FromServices] IValores valoresService) => {

//     var validacao = validacaoDTO(valoresDTO);

//     if(validacao.Mensagens.Count > 0) return Results.BadRequest(validacao);
//     bool valor = valoresService.Incluir(valoresDTO);
//     if(valor)
//     {
//         return Results.Created($"Finance/Post/{valoresDTO.Id}", valoresDTO);
//     }
//     else
//     {
//         return Results.BadRequest("Erro ao salvar no banco de dados! ");
//     }
// })
// .WithTags("Financeiro")
// .WithOpenApi();

// app.MapGet("Finance/GetId/{Tipo}`{id}", ([FromRoute]string Tipo, int id, [FromServices] IValores valoresService) => {
//     ValoresDTO valoresDTO = new ValoresDTO{
//         Tipo = Tipo,
//         Id = id,
//     };
//     var valores = valoresService.BuscarPorId(valoresDTO);
//     if(valores == null) return Results.BadRequest("Não encontrado nada !");

//     return Results.Ok(valores);
// })
// .WithTags("Financeiro")
// .WithOpenApi();

// app.MapGet("Finance/GetCategoria/{categoria}", (string categoria, [FromServices] IValores valoresService) => {
//     ValoresDTO valoresDTO = new ValoresDTO{
//         Categoria = categoria
//     };
//     var resultado = valoresService.BuscaCategoria(valoresDTO);
//     return Results.Ok(resultado);
// })
// .WithTags("Financeiro")
// .WithOpenApi();

// app.MapGet("Finance/GetDate/{date}", (DateTime date, [FromServices] IValores valoresService) => {
//     ValoresDTO valoresDTO = new ValoresDTO{
//         Date = date,
//     };
//     var resultado = valoresService.BuscaPorData(valoresDTO);
    
//     return Results.Ok(resultado);
// })
// .WithTags("Financeiro")
// .WithOpenApi();

// app.MapGet("Finance/GetTipo/{tipo}", (string tipo, [FromServices] IValores valoresService) => {
//     ValoresDTO valoresDTO = new ValoresDTO{
//         Tipo = tipo
//     };

//     var resultado = valoresService.BuscaTipo(valoresDTO);
//     return Results.Ok(resultado);
// })
// .WithTags("Financeiro")
// .WithOpenApi();

// app.MapDelete("Finance/del/{Tipo}`{id}", ([FromRoute]string Tipo, int id, [FromServices] IValores valoresService) => {

//     ValoresDTO valoresDTO = new ValoresDTO{
//         Tipo = Tipo,
//         Id = id,
//     };
//     var valores = valoresService.BuscarPorId(valoresDTO);
//     if(valores == null) return Results.BadRequest("Não encontrado nada !");

//     bool resultado = valoresService.Apagar(valores);
//     if(resultado) return Results.Ok("Apagado com Sucesso !");

//     return Results.BadRequest();

// })
// .WithTags("Financeiro")
// .WithOpenApi();

// #endregion // fim região finance

// app.Run();




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
