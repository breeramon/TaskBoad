using TaskBoad.Api.Autenticacao;
using TaskBoad.Api.Erros;
using TaskBoad.Application;
using TaskBoad.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Erros de regra de negócio viram respostas 400/409 padronizadas.
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<TratadorDeExcecoes>();

// Camadas da aplicação.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("TaskBoad")); // fica nos Segredos do Usuário
builder.Services.AddAutenticacaoSupabase(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // lê e valida o token
app.UseAuthorization();  // aplica o [Authorize]

app.MapControllers();

app.Run();
