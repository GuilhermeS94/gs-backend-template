using GS.Backend.Dominios.Middlewares;
using GS.Backend.Dominios.Notificacoes;
using GS.Backend.Infra;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdiomas();
builder.Services.AddMvc(options => options.Filters.Add<NotificacoesFiltro>());

builder.Services.AddControllers();

builder.Services.AddComandos();
builder.Services.AddFiltros();
builder.Services.AddServicosExternos();
builder.Services.AddSwaggerCustomizado();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt => {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "GS Backend Modelo");
    });
}

app.UseHttpsRedirection();

var idiomas = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(idiomas.Value);

app.UseAuthorization();

app.UseMiddleware<TratamentoExcecao>();

app.MapControllers();

app.Run();

