using Microsoft.EntityFrameworkCore;
using TaskBoad.Application.Abstracoes;
using TaskBoad.Domain.Comum;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Perfil> Perfis => Set<Perfil>();
    public DbSet<AreaDeTrabalho> AreasDeTrabalho => Set<AreaDeTrabalho>();
    public DbSet<MembroAreaDeTrabalho> MembrosAreaDeTrabalho => Set<MembroAreaDeTrabalho>();
    public DbSet<Quadro> Quadros => Set<Quadro>();
    public DbSet<Lista> Listas => Set<Lista>();
    public DbSet<Cartao> Cartoes => Set<Cartao>();
    public DbSet<Etiqueta> Etiquetas => Set<Etiqueta>();
    public DbSet<CartaoEtiqueta> CartaoEtiquetas => Set<CartaoEtiqueta>();
    public DbSet<CartaoResponsavel> CartaoResponsaveis => Set<CartaoResponsavel>();
    public DbSet<Checklist> Checklists => Set<Checklist>();
    public DbSet<ItemChecklist> ItensChecklist => Set<ItemChecklist>();
    public DbSet<Comentario> Comentarios => Set<Comentario>();
    public DbSet<Atividade> Atividades => Set<Atividade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Carrega todas as classes IEntityTypeConfiguration<T> da pasta Configuracoes.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Os Ids são gerados no C# (Guid v7), nunca pelo banco. Sem isso, o EF Core
        // confundiria um item novo adicionado numa coleção com um item já existente.
        foreach (var entidade in modelBuilder.Model.GetEntityTypes()
                     .Where(e => typeof(EntidadeBase).IsAssignableFrom(e.ClrType)))
        {
            modelBuilder.Entity(entidade.ClrType).Property(nameof(EntidadeBase.Id)).ValueGeneratedNever();
        }

        // Tabelas, colunas, chaves e índices em snake_case (padrão do Postgres).
        ConvencaoSnakeCase.Aplicar(modelBuilder);
    }
}
