﻿using API_Boletim.Context;
using API_Boletim.Domains;
using API_Boletim.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Boletim.Repositories
{
    public class AlunoRepository : IAluno
    {
        //Chamamos a classe de conexao do banco
        BoletimContext conexao = new BoletimContext();

        //Chamamos o objeto que poderá receber e executar os comandos do banco
        SqlCommand cmd = new SqlCommand();


        public Aluno Alterar(int id, Aluno a)
        {
            //Inicio da coenxao
            cmd.Connection = conexao.Conectar();

            //colocar os parametros 
            cmd.CommandText = "UPDATE Aluno SET Nome= @nome, RA = @ra, Idade= @idade WHERE IdAluno= @id";

            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            cmd.Parameters.AddWithValue("@id", id);


            cmd.ExecuteNonQuery();
            //encerrar conexao
            conexao.Desconectar();

            return a;
        }

        public Aluno BuscarPorId(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id";

            //Atribuimos as variáveis que vem como arguemento
            cmd.Parameters.AddWithValue("@id", id);

            //Dar play
            SqlDataReader dados = cmd.ExecuteReader();

            Aluno a = new Aluno();

            while (dados.Read())
            {
                a.IdAluno   = Convert.ToInt32(dados.GetValue(0));
                a.Nome      = dados.GetValue(1).ToString();
                a.RA        = dados.GetValue(2).ToString();
                a.Idade     = Convert.ToInt32(dados.GetValue(3));
            }

            conexao.Desconectar();

            return a;
        }

        public Aluno Cadastrar(Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText =
                "INSERT INTO Aluno (Nome, RA, Idade) " +
                "VALUES" +
                "(@nome, @ra, @idade)";
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            // Será este comando o responsável por injetar os dados no banco efetivamente
            cmd.ExecuteNonQuery();

            //DML --> ExecuteNonQuery

            return a;
        }

        public void Excluir(int id)
        {
            //Conectando
            cmd.Connection = conexao.Conectar();

            //Casar os seguintes id
            cmd.CommandText = "DELETE FROM Aluno WHERE IdAluno= @id";
            cmd.Parameters.AddWithValue("@id", id);
            

            // Será este comando o responsável por injetar os dados no banco efetivamente
            cmd.ExecuteNonQuery();

            //Desconectando
            conexao.Desconectar();
        
        }
        

        public List<Aluno> LerTodos()
        {
            //Abrir conexao
            cmd.Connection = conexao.Conectar();

            //Preparar a Query(consulta)
            cmd.CommandText = "SELECT * FROM Aluno";

            // Dar play
            SqlDataReader dados = cmd.ExecuteReader();

            //Criamos a lista para guardar os alunos
            List<Aluno> alunos = new List<Aluno>();

            while (dados.Read())
            {
                alunos.Add(
                        new Aluno()
                        {
                            IdAluno = Convert.ToInt32(dados.GetValue(0)),
                            Nome    = dados.GetValue(1).ToString(),
                            RA      = dados.GetValue(2).ToString(),
                            Idade   = Convert.ToInt32(dados.GetValue(3))
                        }
                    );
            }

            //Fechar conexao
            conexao.Desconectar();

            return alunos;
        }
    }
}
