public class RegistroDAO
{
    private MeuDiarioSENACContext conexao = new MeuDiarioSENACContext();

    public void Inserir(Registro registro)
    {
        conexao.Registros.Add(registro);
        conexao.SaveChanges();
    }

    public List<Registro> ListarPorUsuario(int usuarioId)
    {
        return conexao.Registros
            .Where(r => r.UsuarioID == usuarioId)
            .OrderBy(r => r.DataRegistro)
            .ToList();
    }

    public Registro? BuscarPorId(int id, int usuarioId)
    {
        return conexao.Registros
            .FirstOrDefault(r => r.Id == id && r.UsuarioID == usuarioId);
    }

    public void Editar(Registro registro)
    {
        conexao.Registros.Update(registro);
        conexao.SaveChanges();
    }
}
