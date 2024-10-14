using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;
using ApiFinanceiro.Domain.Interfaces;
using ApiFinanceiro.Domain.ModelView;
using ApiFinanceiro.Domain.Servicos;
using ApiFinanceiro.Infraestrutura.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

        var key = Encoding.ASCII.GetBytes(Configuration["Jwt:key"]);
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters{
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                //ValidIssuer = Configuration["Jwt:Issuer"],
                //ValidAudience = Configuration["Jwt:Audience"],
            };
        });

        services.AddAuthorization();

        services.AddScoped<IUsers, UsersService>();
        services.AddScoped<IValores, ValoresService>();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Fiananças", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT no formato: Bearer {token}"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });


        services.AddDbContext<DbContexto>(options =>{
            options.UseSqlServer(
                Configuration.GetConnectionString("SqlString"));
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

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("AllowAll");

        app.UseEndpoints(endpoints => {
            #region Home 

            // Inicio da api mostando que tem swagger
            endpoints.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
            #endregion

            #region Usuario

            // Login de Usuario
            endpoints.MapPost("/Usuario", ([FromBody] UserDTO userDTO, [FromServices] IUsers usersService) => {
                var user = usersService.Logar(userDTO);

                if(user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(Configuration["Jwt:key"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userDTO.Email)
                        }),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Results.Ok(new {token = tokenString});
                }
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
            .RequireAuthorization()
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
            .RequireAuthorization()
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
            .RequireAuthorization()
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
            .RequireAuthorization()
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
            .RequireAuthorization()
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
            .RequireAuthorization()
            .WithTags("Financeiro")
            .WithOpenApi();

            #endregion

        });
    }
}