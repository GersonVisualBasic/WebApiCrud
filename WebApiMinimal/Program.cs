using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexto;
using WebApiMinimal.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer(
        "Data Source=localhost;Initial Catalog=Clientes_Catalog;Integrated Security=False;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("Adicionar-EditarCliente", async (Clientes Clientes, Contexto contexto) =>
{
    contexto.Cliente.Add(Clientes);
    await contexto.SaveChangesAsync();

});


app.MapPost("ExcluirCliente/{id}", async (int id, Contexto contexto) =>
{
    var excluirCliente = await contexto.Cliente.FirstOrDefaultAsync(p => p.Id == id);
    if (excluirCliente != null)
    {
        contexto.Cliente.Remove(excluirCliente);
        await contexto.SaveChangesAsync();
    }
});

app.MapPost("ListarClienteAtivo", async (int id, Contexto contexto) =>
{
    return await contexto.Cliente.ToListAsync();

});

app.UseSwaggerUI();

app.Run();
