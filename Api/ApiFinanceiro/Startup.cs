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

    ErrosDeValidacao validacaoValoresDTO (ValoresDTO valoresDTO)
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

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; set; } = default!;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen();
        
        services.AddScoped<IUsers, UsersService>();
        services.AddScoped<IValores, ValoresService>();

        services.AddEndpointsApiExplorer();

        services.AddDbContext<DbContexto>(options =>{
            options.UseSqlServer(Configuration.GetConnectionString("SqlString"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseRouting();

        app.UseCors("AllowAll");

        app.UseEndpoints(endpoints => {
            #region Home 

            // Inicio da api mostando que tem swagger
            endpoints.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
            #endregion

            #region Usuario

            // Login de Usuario
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
            #endregion
            

            #region Financeiro
            // Post de valores 
            endpoints.MapPost("Finance/Post", ([FromBody] ValoresDTO valoresDTO, [FromServices] IValores valoresService) => {

            var validacao = validacaoValoresDTO(valoresDTO);

                if(validacao.Mensagens.Count > 0) return Results.BadRequest(validacao);
                bool valor = valoresService.Incluir(valoresDTO);
                if(valor)
                {
                    return Results.Created($"Finance/Post/{valoresDTO.Id}", valoresDTO);
                }
                else
                {
                    return Results.BadRequest("Erro ao salvar no banco de dados! ");
                }
            })
            .WithTags("Financeiro")
            .WithOpenApi(); 

            // buscando valores passando tipo e id 
            endpoints.MapGet("Finance/GetId/{Tipo}`{id}", ([FromRoute]string Tipo, int id, [FromServices] IValores valoresService) => {
                ValoresDTO valoresDTO = new ValoresDTO{
                    Tipo = Tipo,
                    Id = id,
                };
                var valores = valoresService.BuscarPorId(valoresDTO);
                if(valores == null) return Results.BadRequest("Não encontrado nada !");

                return Results.Ok(valores);
            })
            .WithTags("Financeiro")
            .WithOpenApi();

            // buscando por categoria
            endpoints.MapGet("Finance/GetCategoria/{categoria}", (string categoria, [FromServices] IValores valoresService) => {
                ValoresDTO valoresDTO = new ValoresDTO{
                    Categoria = categoria
                };
                var resultado = valoresService.BuscaCategoria(valoresDTO);
                return Results.Ok(resultado);
            })
            .WithTags("Financeiro")
            .WithOpenApi();

            // buscando por data
            endpoints.MapGet("Finance/GetDate/{date}", (DateTime date, [FromServices] IValores valoresService) => {
                ValoresDTO valoresDTO = new ValoresDTO{
                    Date = date,
                };
                var resultado = valoresService.BuscaPorData(valoresDTO);
                
                return Results.Ok(resultado);
            })
            .WithTags("Financeiro")
            .WithOpenApi();

            //buscando por tipo de procedimento entrada ou saida de valores
            endpoints.MapGet("Finance/GetTipo/{tipo}", (string tipo, [FromServices] IValores valoresService) => {
                ValoresDTO valoresDTO = new ValoresDTO{
                    Tipo = tipo
                };

                var resultado = valoresService.BuscaTipo(valoresDTO);
                return Results.Ok(resultado);
            })
            .WithTags("Financeiro")
            .WithOpenApi();

            // Deletando Valores passando o tipo e id
            endpoints.MapDelete("Finance/del/{Tipo}`{id}", ([FromRoute]string Tipo, int id, [FromServices] IValores valoresService) => {

                ValoresDTO valoresDTO = new ValoresDTO{
                    Tipo = Tipo,
                    Id = id,
                };
                var valores = valoresService.BuscarPorId(valoresDTO);
                if(valores == null) return Results.BadRequest("Não encontrado nada !");

                bool resultado = valoresService.Apagar(valores);
                if(resultado) return Results.Ok("Apagado com Sucesso !");

                return Results.BadRequest();

            })
            .WithTags("Financeiro")
            .WithOpenApi();

            #endregion

        });
    }
}