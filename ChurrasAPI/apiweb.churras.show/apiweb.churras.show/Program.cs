using apiweb.churras.show;
using apiweb.churras.show.Context;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;
using apiweb.churras.show.Service;
using apiweb.churras.show.Utils.Mail;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao contêiner.
builder.Services.AddControllers();

// Registra os repositórios.
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IStatusEventoRepository, StatusEventoRepository>();
builder.Services.AddScoped<ITiposUsuarioRepository, TiposUsuarioRepository>();
builder.Services.AddScoped<IPacoteRepository, PacotesRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();

// Registra os serviços.
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<EmailSendingService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));

// Configura o CORS.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Configura a conexão com o banco de dados.
string connectionString = builder.Configuration["ConnectionStrings:Default"];

builder.Services.AddDbContext<ChurrasShowContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Configura o Swagger para documentação da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API ChurrasShow",
        Description = "API Para Gerenciamento de Eventos e Orçamento - ChurrasShow WEB.API",
        Contact = new OpenApiContact
        {
            Name = "Alunos - Senai Informática",
            Url = new Uri("https://github.com/BrizidoAndre/ChurrasShow")
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta maneira: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// Configuração do serviço de moderação de conteúdo - Azure.
builder.Services.AddSingleton(provider => new ContentModeratorClient(
    new ApiKeyServiceClientCredentials("e1ad89af0c8649c6a48cb4c1606c7d97"))
{
    Endpoint = "https://churrasshowcontentmoderator.cognitiveservices.azure.com/"
}
);

// Configura a autenticação JWT.
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("churrasshow-webapi-chave-symmetricsecuritykey")),
        ClockSkew = TimeSpan.FromMinutes(10),
        ValidIssuer = "apiweb.churras.show",
        ValidAudience = "apiweb.churras.show"
    };
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usa a política de CORS definida anteriormente.
app.UseCors("CorsPolicy");

// Usa a autenticação e autorização.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
