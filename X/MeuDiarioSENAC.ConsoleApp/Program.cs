using System;
using System.Collections.Generic;

class Program
{
static void Main()
{
UsuarioDAO usuarioDAO = new UsuarioDAO();
RegistroDAO registroDAO = new RegistroDAO();

    Console.WriteLine("=== MEU DIÁRIO SENAC ===");
    Console.WriteLine();

    Console.Write("Digite seu nome: ");
    string nome = Console.ReadLine() ?? "";

    Usuario? usuario = usuarioDAO.BuscarPorNome(nome);

    if (usuario == null)
    {
        Console.WriteLine("Usuário não encontrado.");

        Console.WriteLine();
        Console.Write("Deseja criar um novo usuário? (s/n): ");
        string resposta = Console.ReadLine() ?? "";

        if (resposta.ToLower() == "s")
        {
            usuario = new Usuario
            {
                Nome = nome
            };

            usuarioDAO.Inserir(usuario);

            Console.WriteLine("Usuário criado com sucesso!");
            Console.WriteLine($"Seu ID é: {usuario.Id}");
        }
        else
        {
            Console.WriteLine("Encerrando...");
            return;
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Olá, {usuario.Nome}!");

    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("=== MENU ===");
        Console.WriteLine("1 - Anotar");
        Console.WriteLine("2 - Registros");
        Console.WriteLine("3 - Editar");
        Console.WriteLine("4 - Sair");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine() ?? "";

        if (opcao == "1")
        {
            Anotar(usuario, registroDAO);
        }
        else if (opcao == "2")
        {
            ListarRegistros(usuario, registroDAO);
        }
        else if (opcao == "3")
        {
            EditarRegistro(usuario, registroDAO);
        }
        else if (opcao == "4")
        {
            Console.WriteLine("Saindo...");
            break;
        }
        else
        {
            Console.WriteLine("Opção inválida!");
        }
    }
}

static void Anotar(Usuario usuario, RegistroDAO registroDAO)
{
    Console.WriteLine();
    Console.WriteLine("=== NOVO REGISTRO ===");

    Console.Write("Digite a data (dd/MM/yyyy): ");
    string dataInput = Console.ReadLine() ?? "";

    if (!DateTime.TryParse(dataInput, out DateTime data))
    {
        Console.WriteLine("Data inválida!");
        return;
    }

    Console.Write("Digite o título: ");
    string titulo = Console.ReadLine() ?? "";

    Console.Write("Faça sua anotação: ");
    string conteudo = Console.ReadLine() ?? "";

    Registro registro = new Registro
    {
        Titulo = titulo,
        DataRegistro = data,
        Conteudo = conteudo,
        UsuarioID = usuario.Id
    };

    registroDAO.Inserir(registro);

    Console.WriteLine();
    Console.WriteLine("Registro salvo com sucesso!");
}

static void ListarRegistros(Usuario usuario, RegistroDAO registroDAO)
{
    Console.WriteLine();
    Console.WriteLine("=== REGISTROS ===");

    List<Registro> registros = registroDAO.ListarPorUsuario(usuario.Id);

    if (registros.Count == 0)
    {
        Console.WriteLine("Nenhum registro encontrado.");
        return;
    }

    foreach (Registro registro in registros)
    {
        Console.WriteLine();
        Console.WriteLine($"ID: {registro.Id}");
        Console.WriteLine($"Data: {registro.DataRegistro:dd/MM/yyyy}");
        Console.WriteLine($"Título: {registro.Titulo}");
        Console.WriteLine($"Conteúdo: {registro.Conteudo}");
        Console.WriteLine("----------------------------");
    }
}

static void EditarRegistro(Usuario usuario, RegistroDAO registroDAO)
{
    Console.WriteLine();
    Console.WriteLine("=== EDITAR REGISTRO ===");

    List<Registro> registros = registroDAO.ListarPorUsuario(usuario.Id);

    if (registros.Count == 0)
    {
        Console.WriteLine("Nenhum registro para editar.");
        return;
    }

    foreach (Registro registro in registros)
    {
        Console.WriteLine(
            $"{registro.Id} - {registro.DataRegistro:dd/MM/yyyy} - {registro.Titulo}"
        );
    }

    Console.WriteLine();
    Console.Write("Digite o ID do registro que deseja editar: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID inválido!");
        return;
    }

    Registro? registroSelecionado =
        registroDAO.BuscarPorId(id, usuario.Id);

    if (registroSelecionado == null)
    {
        Console.WriteLine("Registro não encontrado.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"Título atual: {registroSelecionado.Titulo}");
    Console.Write("Digite o novo título: ");

    string novoTitulo = Console.ReadLine() ?? "";

    Console.WriteLine();
    Console.WriteLine($"Conteúdo atual: {registroSelecionado.Conteudo}");
    Console.Write("Digite o novo conteúdo: ");

    string novoConteudo = Console.ReadLine() ?? "";

    registroSelecionado.Titulo = novoTitulo;
    registroSelecionado.Conteudo = novoConteudo;

    registroDAO.Editar(registroSelecionado);

    Console.WriteLine();
    Console.WriteLine("Registro editado com sucesso!");
}


}