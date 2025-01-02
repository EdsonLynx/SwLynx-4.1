Imports System.Collections.Generic
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Threading.Thread
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports System.Configuration


Imports System.Runtime.CompilerServices
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime

Imports System.IO.Compression
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks


Public Class ClBancoDados

    '''''''Public Function AbrirBanco() As Boolean

    '''''''    ' String de conexão ajustada
    '''''''    conexao = "Server=lynxlocal.mysql.uhserver.com;database=lynxlocal;uid=lynxlocal;pwd=jHAzhFG848@yN@U;" &
    '''''''     "Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
    '''''''    My.Settings.BancoDadosAtivo = "lynxlocal"

    '''''''    'conexao = "Server=alfatec2.mysql.uhserver.com;database=alfatec2;uid=alfateccozinhas;pwd=jHAzhFG848@yN@U;
    '''''''    'Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
    '''''''    'My.Settings.BancoDadosAtivo = "alfatec2"

    '''''''conexao = "Server=mettapaineis.mysql.uhserver.com;database=mettapaineis;uid=rubensmetta;pwd=jHAzhFG848@yN@U;
    '''''''Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
    '''''''My.Settings.BancoDadosAtivo = "mettapaineis"

    '''''''    '    'conexao = "Server=marp.mysql.uhserver.com;database=marp;uid=mrogerio;pwd=RZ*5rDs7FPsGTT9;
    '''''''    '    Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
    '''''''    '    'My.Settings.BancoDadosAtivo = "marp"

    '''''''    '    'conexao = "Server=tecnorio.mysql.uhserver.com;database=tecnorio;uid=tecnoriousuario;pwd=jHAzhFG848@yN@U;
    '''''''    '    Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
    '''''''    '    'My.Settings.BancoDadosAtivo = "tecnorio"

    '''''''    ' Salvar configurações
    '''''''    My.Settings.Save()

    '''''''    Try
    '''''''        ' Criar uma nova conexão MySQL
    '''''''        myconect = New MySqlConnection(conexao)

    '''''''        ' Abrir a conexão
    '''''''        myconect.Open()

    '''''''        ' Definir o status da abertura do banco como verdadeiro
    '''''''        AbrirBanco = True

    '''''''        ' Manter a conexão ativa com um "ping" periódico (opcional)
    '''''''        StartKeepAlive()

    '''''''    Catch ex As MySqlException

    '''''''        ' Se houve um erro na conexão, tente novamente
    '''''''        myconect = New MySqlConnection(conexao)
    '''''''        myconect.Open()

    '''''''        AbrirBanco = True

    '''''''    Catch ex As Exception

    '''''''        ' Trate outros erros inesperados
    '''''''        AbrirBanco = False
    '''''''        MessageBox.Show("Erro inesperado: " & ex.Message)

    '''''''        ' Finalizar o Add-in em caso de erro crítico
    '''''''        swapp.UnloadAddIn("SwLynx_4._1")

    '''''''    Finally
    '''''''    End Try

    '''''''    Return AbrirBanco
    '''''''End Function

    '''''''' Função para enviar um "ping" para manter a conexão ativa
    '''''''' Função para enviar um "ping" para manter a conexão ativa
    '''''''Private Sub StartKeepAlive()
    '''''''    ' Criar um Timer e configurar o intervalo (ex: 5 minutos - 300000ms)
    '''''''    Dim keepAliveTimer As New System.Windows.Forms.Timer()

    '''''''    ' Definir o intervalo de tempo para 5 minutos (300000 milissegundos)
    '''''''    keepAliveTimer.Interval = 300000  ' 300000ms = 5 minutos

    '''''''    ' Adicionar o manipulador de eventos para o evento Tick do Timer
    '''''''    AddHandler keepAliveTimer.Tick, AddressOf PingDatabase

    '''''''    ' Iniciar o timer
    '''''''    keepAliveTimer.Start()
    '''''''End Sub

    '''''''' Função de "ping" no banco de dados
    '''''''Private Sub PingDatabase(sender As Object, e As EventArgs)
    '''''''    Try
    '''''''        ' Verifica se a conexão ainda está aberta
    '''''''        If myconect.State = ConnectionState.Open Then
    '''''''            ' Envia um comando "ping" para manter a conexão viva
    '''''''            Dim command As New MySqlCommand("SELECT 1", myconect)
    '''''''            command.ExecuteScalar()
    '''''''        End If
    '''''''    Catch ex As MySqlException
    '''''''        ' Caso a conexão tenha sido fechada, tente reabrir
    '''''''        If myconect.State = ConnectionState.Closed OrElse myconect.State = ConnectionState.Broken Then
    '''''''            AbrirBanco() ' Tenta abrir a conexão novamente
    '''''''        End If
    '''''''    End Try
    '''''''End Sub


    Public Function AbrirBanco() As Boolean
        ' Verifica se as configurações estão preenchidas

        '' String de conexão ajustada
        'conexao = "Server=lynxlocal.mysql.uhserver.com;database=lynxlocal;uid=lynxlocal;pwd=jHAzhFG848@yN@U;" &
        ' "Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
        'My.Settings.BancoDadosAtivo = "lynxlocal"

        conexao = "Server=alfatec2.mysql.uhserver.com;database=alfatec2;uid=alfateccozinhas;pwd=jHAzhFG848@yN@U;
        Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
        My.Settings.BancoDadosAtivo = "alfatec2"

        'conexao = "Server=mettapaineis.mysql.uhserver.com;database=mettapaineis;uid=rubensmetta;pwd=jHAzhFG848@yN@U;
        'Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
        'My.Settings.BancoDadosAtivo = "mettapaineis"

        '    '    'conexao = "Server=marp.mysql.uhserver.com;database=marp;uid=mrogerio;pwd=RZ*5rDs7FPsGTT9;
        '    '    Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
        '    '    'My.Settings.BancoDadosAtivo = "marp"

        '    '    'conexao = "Server=tecnorio.mysql.uhserver.com;database=tecnorio;uid=tecnoriousuario;pwd=jHAzhFG848@yN@U;
        '    '    Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
        '    '    'My.Settings.BancoDadosAtivo = "tecnorio"

        Try
            ' Inicializa a conexão
            myconect = New MySqlConnection(conexao)
            myconect.Open()

            AbrirBanco = True

            ' Inicia o mecanismo de keep-alive
            StartKeepAlive()

            Return True
        Catch ex As MySqlException
            myconect = New MySqlConnection(conexao)
            myconect.Open()

            AbrirBanco = True

            'MessageBox.Show("Erro ao conectar ao banco: " & ex.Message)
            ' AbrirBanco = False
            Return False
        Catch ex As Exception
            myconect = New MySqlConnection(conexao)
            myconect.Open()

            AbrirBanco = True

            ' MessageBox.Show("Erro inesperado: " & ex.Message)
            'AbrirBanco = False
            Return False
        End Try
    End Function



    Private Sub StartKeepAlive()
        ' Criar um Timer e configurar o intervalo (ex: 5 minutos - 300000ms)
        Dim keepAliveTimer As New System.Windows.Forms.Timer()

        ' Definir o intervalo de tempo para 5 minutos (300000 milissegundos)
        keepAliveTimer.Interval = 300000  ' 300000ms = 5 minutos

        ' Adicionar o manipulador de eventos para o evento Tick do Timer
        AddHandler keepAliveTimer.Tick, AddressOf PingDatabase

        ' Iniciar o timer
        keepAliveTimer.Start()
    End Sub

    Private Sub PingDatabase(sender As Object, e As EventArgs)
        Try
            ' Verifica se a conexão ainda está aberta
            If myconect IsNot Nothing AndAlso myconect.State = ConnectionState.Open Then
                ' Envia um comando "ping" para manter a conexão viva
                Dim command As New MySqlCommand("SELECT 1", myconect)
                command.ExecuteScalar()
            Else
                ' Caso a conexão tenha sido fechada ou perdida, tenta reabri-la
                If myconect IsNot Nothing Then
                    myconect.Close()
                End If
                AbrirBanco() ' Tenta abrir a conexão novamente
            End If
        Catch ex As MySqlException
            ' Caso a conexão tenha sido fechada ou com erro de rede, tente reabrir
            If myconect IsNot Nothing AndAlso (myconect.State = ConnectionState.Closed OrElse myconect.State = ConnectionState.Broken) Then
                AbrirBanco() ' Tenta abrir a conexão novamente
            End If
        Catch ex As Exception
            ' Lidar com outros tipos de exceção, como problemas de rede ou servidor
            MessageBox.Show("Erro ao manter a conexão com o banco: " & ex.Message)
        End Try
    End Sub

    Public Function AbrirBancoAccess() As Boolean
        ' Ajuste o caminho para o arquivo do banco de dados Access (.accdb ou .mdb) conforme necessário
        Dim conexao As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\caminho_do_banco\seu_banco.accdb;Persist Security Info=False;"
        ' Se estiver usando um banco .mdb antigo, use o provider Jet:
        ' Dim conexao As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\caminho_do_banco\seu_banco.mdb;Persist Security Info=False;"

        My.Settings.BancoDadosAtivo = "AccessDatabase"
        My.Settings.Save()

        Try
            myconectAccess = New OleDbConnection(conexao)
            myconectAccess.Open()

            AbrirBancoAccess = True
            ' MessageBox.Show("Conexão aberta com sucesso.")
        Catch ex As OleDbException
            ' Trate erros específicos do Access aqui
            AbrirBancoAccess = False
            ' MessageBox.Show("Erro ao abrir a conexão com o banco de dados: " & ex.Message)
        Catch ex As Exception
            ' Trate outros erros aqui
            AbrirBancoAccess = False
            ' Descarrega o complemento, se necessário
            swapp.UnloadAddIn("SwLynx_4._1")
            ' MessageBox.Show("Erro inesperado: " & ex.Message)
        Finally
            ' Fechar a conexão caso necessário
        End Try

        Return AbrirBancoAccess

    End Function

    Public Function FechaBanco() As Boolean
        If myconect.State = ConnectionState.Open Then
            Try
                myconect.Close()
                myconect.Dispose()
            Catch ex As Exception
                ' Trate erros ao fechar a conexão aqui
                ' MessageBox.Show("Erro ao fechar a conexão: " & ex.Message)
            Finally
            End Try
        End If
    End Function

    Public Function FechaBancoAccess() As Boolean
        Try
            If myconectAccess IsNot Nothing AndAlso myconectAccess.State = ConnectionState.Open Then
                myconectAccess.Close()
                myconectAccess.Dispose()
                MessageBox.Show("Conexão com o banco de dados Access fechada com sucesso.")
                Return True
            End If
        Catch ex As Exception
            ' Trate erros ao fechar a conexão aqui
            MessageBox.Show("Erro ao fechar a conexão com o banco de dados Access: " & ex.Message)
            Return False
        End Try

        Return False
    End Function

    ' Função para carregar dados em um DataTable
    Public Function CarregarDados(ByVal query As String) As DataTable

        Dim dtTabela As New System.Data.DataTable()

        Try


            ' Cria um adaptador de dados
            Using adaptador As New MySqlDataAdapter(query, myconect)
                ' Preenche o DataTable com os dados da consulta
                adaptador.Fill(dtTabela)

            End Using
            'End Using
        Catch ex As MySqlException
            ' Trate erros específicos do MySQL
            ' MessageBox.Show("Erro ao carregar dados: " & ex.Message)
        Catch ex As Exception
            ' Trate outros erros
            ' MessageBox.Show("Erro inesperado: " & ex.Message)
        End Try

        Return dtTabela

        'FechaBanco(myconect)

    End Function


    Public Function CarregarDadosNova(ByVal query As String, Optional ByVal parametros As Dictionary(Of String, Object) = Nothing) As DataTable


        Dim dtTabela As New System.Data.DataTable()

        ' Usar uma nova conexão para cada chamada
        Using myconect As New MySqlConnection(conexao)
            Try
                ' Certifique-se de que a conexão está aberta
                If myconect.State = ConnectionState.Closed Then
                    myconect.Open()
                End If

                ' Criar comando SQL
                Using comando As New MySqlCommand(query, myconect)
                    ' Adicionar parâmetros, se existirem
                    If parametros IsNot Nothing Then
                        For Each param In parametros
                            comando.Parameters.AddWithValue(param.Key, param.Value)
                        Next
                    End If

                    ' Criar adaptador e preencher o DataTable
                    Using adaptador As New MySqlDataAdapter(comando)
                        adaptador.Fill(dtTabela)
                    End Using
                End Using

            Catch ex As MySqlException
                ' Logar erros específicos do MySQL
                MessageBox.Show($"Erro MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                ' Logar outros erros
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' A conexão será automaticamente fechada pelo 'Using'
            End Try
        End Using

        Return dtTabela
    End Function

    Public Async Function CarregarDadosNovaAsync(ByVal query As String, Optional ByVal parametros As Dictionary(Of String, Object) = Nothing) As Task(Of DataTable)
        Dim dtTabela As New DataTable()

        ' Usar uma nova conexão para cada chamada
        Using myconect As New MySqlConnection(conexao)
            Try
                ' Certifique-se de que a conexão está aberta
                If myconect.State = ConnectionState.Closed Then
                    Await myconect.OpenAsync()
                End If

                ' Criar comando SQL
                Using comando As New MySqlCommand(query, myconect)
                    ' Adicionar parâmetros, se existirem
                    If parametros IsNot Nothing Then
                        For Each param In parametros
                            comando.Parameters.AddWithValue(param.Key, param.Value)
                        Next
                    End If

                    ' Criar adaptador e preencher o DataTable de forma assíncrona
                    Using adaptador As New MySqlDataAdapter(comando)
                        Await Task.Run(Sub() adaptador.Fill(dtTabela))
                    End Using
                End Using

            Catch ex As MySqlException
                ' Logar erros específicos do MySQL
                MessageBox.Show($"Erro MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                ' Logar outros erros
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' A conexão será automaticamente fechada pelo 'Using'
            End Try
        End Using

        Return dtTabela
    End Function
    Public Function CarregarDadosAccess(ByVal query As String) As DataTable
        Dim dtTabela As New System.Data.DataTable()

        Try
            ' Cria um adaptador de dados para Access
            Using adaptador As New OleDbDataAdapter(query, myconectAccess)
                ' Preenche o DataTable com os dados da consulta
                adaptador.Fill(dtTabela)
            End Using
        Catch ex As OleDbException
            ' Trate erros específicos do Access
            ' MessageBox.Show("Erro ao carregar dados: " & ex.Message)
        Catch ex As Exception
            ' Trate outros erros
            ' MessageBox.Show("Erro inesperado: " & ex.Message)
        End Try

        Return dtTabela
    End Function

    Public Function ContemSubstring(conjuntoStrings As IEnumerable(Of String), substring As String) As Boolean
        ' Certifica-se de que o substring é fornecido em minúsculas para comparação
        Dim substringLower As String = substring.ToLower()

        ' Percorre cada string no conjunto
        For Each str As String In conjuntoStrings
            ' Compara a string atual com o substring usando uma comparação que ignora maiúsculas e minúsculas
            If str.IndexOf(substringLower, StringComparison.OrdinalIgnoreCase) >= 0 Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Sub Salvar(ByVal funcaosql As String)
        Try
            ' Verifica se a conexão está fechada antes de abrir
            'If myconect.State = ConnectionState.Closed Then
            '    cl_BancoDados.AbrirBanco() ' Abre a conexão, se necessário
            'End If

            ' Usar "Using" para garantir o fechamento correto do comando e da conexão
            Using mycomand As New MySqlCommand(funcaosql, myconect)
                ' Executa o comando SQL
                Dim rowsAffected As Integer = mycomand.ExecuteNonQuery()
                ' Opcional: Pode usar rowsAffected para verificar quantas linhas foram afetadas
            End Using
        Catch ex As MySqlException
            ' Tratar erros específicos do MySQL, como problemas de conexão ou sintaxe SQL
            'MessageBox.Show("Erro ao executar a operação no MySQL: " & ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' Tratar erros gerais
            'MessageBox.Show("Erro inesperado: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Verifica se o banco de dados não está aberto
            'If cl_BancoDados.AbrirBanco Then

            '    ' Tenta abrir o banco de dados
            '    cl_BancoDados.FechaBanco(myconect)

            'End If
        End Try
    End Sub

    Public Sub SalvarAccess(ByVal funcaosql As String)
        Try
            ' Verifica se a conexão está fechada antes de abrir
            If myconectAccess.State = ConnectionState.Closed Then
                myconectAccess.Open() ' Abre a conexão, se necessário
            End If

            ' Usar "Using" para garantir o fechamento correto do comando
            Using mycomand As New OleDbCommand(funcaosql, myconectAccess)
                ' Executa o comando SQL
                Dim rowsAffected As Integer = mycomand.ExecuteNonQuery()
                ' Opcional: Pode usar rowsAffected para verificar quantas linhas foram afetadas
            End Using
        Catch ex As OleDbException
            ' Tratar erros específicos do Access, como problemas de conexão ou sintaxe SQL
            ' MessageBox.Show("Erro ao executar a operação no Access: " & ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' Tratar erros gerais
            ' MessageBox.Show("Erro inesperado: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Fecha a conexão, se estiver aberta
            If myconectAccess IsNot Nothing AndAlso myconectAccess.State = ConnectionState.Open Then
                myconectAccess.Close()
            End If
        End Try
    End Sub

    Function ComboBoxDataSet(ByVal Tabela As String, ByVal Campo_Id As String, ByVal CampoDescricao As String, ByVal ObjComboBox As ComboBox, ByVal consulta As String) As Boolean
        Try
            ' Construção da consulta SQL com parâmetros
            Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {Tabela} {consulta} ORDER BY {CampoDescricao}"

            'If cl_BancoDados.AbrirBanco = False Then

            '    ' Tenta abrir o banco de dados
            '    cl_BancoDados.AbrirBanco()

            'End If

            ' Criação do adaptador e do dataset
            Using da As New MySqlDataAdapter(sqlStr, myconect)
                Dim ds As New DataSet()
                da.Fill(ds, Tabela)

                ' Configuração do ComboBox
                ObjComboBox.DataSource = ds.Tables(Tabela)
                ObjComboBox.DisplayMember = CampoDescricao.ToUpper()
                ObjComboBox.ValueMember = Campo_Id

                Return True
            End Using
        Catch ex As Exception

            Return False
        End Try


    End Function

    ' Função refatorada para ser assíncrona
    Async Function ComboBoxDataSetAsync(ByVal Tabela As String, ByVal Campo_Id As String, ByVal CampoDescricao As String, ByVal ObjComboBox As ComboBox, ByVal consulta As String) As Task(Of Boolean)
        Try
            ' Construção da consulta SQL com parâmetros
            Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {Tabela} {consulta} ORDER BY {CampoDescricao}"

            '' Certifique-se de que o banco de dados esteja aberto
            'If cl_BancoDados.AbrirBanco = False Then
            '    ' Tenta abrir o banco de dados
            '    cl_BancoDados.AbrirBanco()
            'End If

            ' Criação do comando SQL e adaptador
            Using comando As New MySqlCommand(sqlStr, myconect)
                ' Abre a conexão do banco de dados de forma assíncrona
                Using reader As MySqlDataReader = Await comando.ExecuteReaderAsync()

                    ' Criação de um DataTable para armazenar os dados
                    Dim dt As New DataTable()
                    dt.Load(reader)

                    ' Configuração do ComboBox
                    ObjComboBox.DataSource = dt
                    ObjComboBox.DisplayMember = CampoDescricao.ToUpper()
                    ObjComboBox.ValueMember = Campo_Id

                    Return True
                End Using
            End Using
        Catch ex As Exception
            ' Em caso de erro, retornar False
            Return False
        End Try
    End Function



    Function ChekListBoxDataSet(ByVal Tabela As String, ByVal Campo_Id As String, ByVal CampoDescricao As String, ByRef ObjCheckedListBox As CheckedListBox, ByVal consulta As String) As Boolean
        Try
            ' Construção da consulta SQL com parâmetros
            Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {Tabela} {consulta} ORDER BY {CampoDescricao}"

            ' Verificar se o banco está aberto, caso contrário, abrir
            'If cl_BancoDados.AbrirBanco = False Then
            '    cl_BancoDados.AbrirBanco()
            'End If

            ' Criação do adaptador e do dataset
            Using da As New MySqlDataAdapter(sqlStr, myconect)
                Dim ds As New DataSet()
                da.Fill(ds, Tabela)

                ' Limpa o CheckedListBox antes de adicionar os itens
                ObjCheckedListBox.Items.Clear()

                ' Percorre os dados e adiciona ao CheckedListBox
                For Each row As DataRow In ds.Tables(Tabela).Rows
                    ' Adiciona cada item ao CheckedListBox com o texto sendo o CampoDescricao
                    ObjCheckedListBox.Items.Add(row(CampoDescricao).ToString(), False) ' Inicialmente desmarcado (False)
                Next

                Return True
            End Using
        Catch ex As Exception
            ' Se ocorrer algum erro, exibe uma mensagem de erro
            ' MessageBox.Show("Erro: " & ex.Message)
            Return False
        End Try

        ' Chama a função para carregar os dados no CheckedListBox
        '  Dim sucesso As Boolean = ChekListBoxDataSet("sua_tabela", "id", "nome", CheckedListBox1, "WHERE ativo = 1")



    End Function

    Function ComboBoxDataSetAccess(ByVal Tabela As String, ByVal Campo_Id As String, ByVal CampoDescricao As String, ByRef ObjComboBox As ComboBox, ByVal consulta As String) As Boolean
        Try
            ' Construção da consulta SQL com parâmetros
            Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {Tabela} {consulta} ORDER BY {CampoDescricao}"

            '' Verifica se a conexão está fechada e abre se necessário
            'If cl_BancoDados.AbrirBancoAccess() = False Then
            '    cl_BancoDados.AbrirBancoAccess()
            'End If

            ' Criação do adaptador e do dataset
            Using da As New OleDbDataAdapter(sqlStr, myconectAccess)
                Dim ds As New DataSet()
                da.Fill(ds, Tabela)

                ' Configuração do ComboBox
                ObjComboBox.DataSource = ds.Tables(Tabela)
                ObjComboBox.DisplayMember = CampoDescricao.ToUpper()
                ObjComboBox.ValueMember = Campo_Id

                Return True
            End Using
        Catch ex As Exception
            ' Log do erro e retorno de False em caso de falha
            ' MessageBox.Show("Erro ao carregar dados no ComboBox: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function RetornaCampoDaPesquisa(ByVal Valor_Para_Pesquisa As String, ByVal Campo_A_Retornar As String) As String
        Try

            ' Criação do comando SQL
            Dim daMysql As New MySqlCommand(Valor_Para_Pesquisa, myconect)
            ' Verifica se o banco de dados não está aberto
            'If Not cl_BancoDados.AbrirBanco Then

            'End If

            ' Execução da consulta e leitura dos dados
            Using drMysql As MySqlDataReader = daMysql.ExecuteReader()
                If drMysql.HasRows Then
                    drMysql.Read()
                    Return drMysql(Campo_A_Retornar).ToString()
                Else
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            ' Log do erro e retorno de Nothing em caso de falha
            ' MessageBox.Show("Erro ao executar a pesquisa: " & ex.Message)
            Return Nothing
        End Try
        ' Verifica se o banco de dados não está aberto
        'If cl_BancoDados.AbrirBanco Then

        '    ' Tenta abrir o banco de dados
        '    cl_BancoDados.FechaBanco(myconect)

        'End If

    End Function

    Public Function RetornaCampoDaPesquisaAccess(ByVal Valor_Para_Pesquisa As String, ByVal Campo_A_Retornar As String) As String
        Try
            ' Criação do comando SQL para Access
            Dim daAccess As New OleDbCommand(Valor_Para_Pesquisa, myconectAccess)

            ' Verifica se a conexão está fechada e abre se necessário
            If myconectAccess.State = ConnectionState.Closed Then
                myconectAccess.Open()
            End If

            ' Execução da consulta e leitura dos dados
            Using drAccess As OleDbDataReader = daAccess.ExecuteReader()
                If drAccess.HasRows Then
                    drAccess.Read()
                    Return drAccess(Campo_A_Retornar).ToString()
                Else
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            ' Log do erro e retorno de Nothing em caso de falha
            ' MessageBox.Show("Erro ao executar a pesquisa: " & ex.Message)
            Return Nothing
        Finally
            ' Fecha a conexão, se estiver aberta
            If myconectAccess.State = ConnectionState.Open Then
                myconectAccess.Close()
            End If
        End Try
    End Function

    Public Function AlteracaoEspecifica(tabela As String, campoTabela As String, NovoValor As String, campo_id As String, Valor_id As String) As Boolean

        Try
            AlteracaoEspecifica = False

            ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
            Dim sql As String = $"UPDATE {tabela} SET {campoTabela} = @NovoValor WHERE {campo_id} = @Valor_id"

            '' Verifica se o banco de dados não está aberto
            'If Not cl_BancoDados.AbrirBanco Then

            '    ' Tenta abrir o banco de dados
            '    cl_BancoDados.AbrirBanco()

            'End If

            ' Criação do comando SQL com o uso de parâmetros
            Using mycomand As New MySqlCommand(sql.ToLower(), myconect)
                mycomand.Parameters.AddWithValue("@NovoValor", NovoValor)
                mycomand.Parameters.AddWithValue("@Valor_id", Valor_id)

                ' Execução do comando SQL
                mycomand.ExecuteNonQuery()
            End Using

            AlteracaoEspecifica = True
        Catch ex As Exception
            '   MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
        Finally
        End Try

        Return AlteracaoEspecifica
        ' Verifica se o banco de dados não está aberto
        'If cl_BancoDados.AbrirBanco Then

        '    ' Tenta abrir o banco de dados
        '    cl_BancoDados.FechaBanco(myconect)

        'End If

    End Function

    Public Function AlteracaoEspecificaAccess(tabela As String, campoTabela As String, NovoValor As String, campo_id As String, Valor_id As String) As Boolean
        Try
            AlteracaoEspecificaAccess = False

            ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
            Dim sql As String = $"UPDATE {tabela} SET {campoTabela} = @NovoValor WHERE {campo_id} = @Valor_id"

            ' Verifica se a conexão está fechada e abre se necessário
            If myconectAccess.State = ConnectionState.Closed Then
                myconectAccess.Open()
            End If

            ' Criação do comando SQL com o uso de parâmetros para Access
            Using mycomand As New OleDbCommand(sql, myconectAccess)
                mycomand.Parameters.AddWithValue("@NovoValor", NovoValor)
                mycomand.Parameters.AddWithValue("@Valor_id", Valor_id)

                ' Execução do comando SQL
                mycomand.ExecuteNonQuery()
            End Using

            AlteracaoEspecificaAccess = True
        Catch ex As Exception
            ' MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
        Finally
            ' Fecha a conexão, se estiver aberta
            If myconectAccess.State = ConnectionState.Open Then
                myconectAccess.Close()
            End If
        End Try

        Return AlteracaoEspecificaAccess
    End Function

    Function FormatarPara5Caracteres(numero As String) As String
        Return numero.PadLeft(5, "0")
    End Function

    Public Function FecharArquivoMemoria() As Boolean
        Try
            ' Verifica se o objeto swapp foi inicializado
            If swapp IsNot Nothing Then
                ' Verifica se há um documento aberto
                If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then
                    ' Fecha o documento aberto
                    swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                End If

                ' Libera o objeto COM do SolidWorks
                System.Runtime.InteropServices.Marshal.ReleaseComObject(swapp)
                swapp = Nothing
            End If

            ' Força a coleta de lixo e aguarda a finalização dos objetos
            GC.Collect()
            GC.WaitForPendingFinalizers()

            Return True ' Indica que o arquivo foi fechado com sucesso
        Catch ex As Exception
            ' Log de erro opcional ou tratamento de exceção
            ' MsgBox("Erro ao fechar o arquivo: " & ex.Message)
            Return False ' Indica que houve um erro ao fechar o arquivo
        End Try
    End Function

    Public Sub ProcessarArquivosDGV(ByVal ObjDgvGrdi As DataGridView)
        ' Iterar sobre as linhas do DataGridView
        For Each row As DataGridViewRow In ObjDgvGrdi.Rows
            ' Pular linhas vazias ou não válidas
            If row.IsNewRow Then Continue For

            Dim enderecoArquivo As String = Convert.ToString(row.Cells("EnderecoArquivo").Value)
            If String.IsNullOrEmpty(enderecoArquivo) Then Continue For

            ' Cache das células para evitar múltiplas buscas
            Dim celulaDXF = row.Cells("DGVDXF")
            Dim celulaPDF = row.Cells("DGVPDF")

            ' Verificar extensões
            Dim extensao As String = Path.GetExtension(enderecoArquivo)?.ToUpperInvariant()
            If extensao = ".SLDPRT" OrElse extensao = ".SLDASM" Then
                ' Processar arquivo DXF
                Dim caminhoDXF = Path.ChangeExtension(enderecoArquivo, ".dxf")
                celulaDXF.Value = If(File.Exists(caminhoDXF), My.Resources.arquivo_dxf, My.Resources.Sem_Incone)

                ' Processar arquivo PDF
                Dim caminhoPDF = Path.ChangeExtension(enderecoArquivo, ".pdf")
                celulaPDF.Value = If(File.Exists(caminhoPDF), My.Resources.ficheiro_pdf, My.Resources.Sem_Incone)
            Else
                ' Resetar células se a extensão não for válida
                celulaDXF.Value = My.Resources.Sem_Incone
                celulaPDF.Value = My.Resources.Sem_Incone
            End If
        Next
    End Sub


End Class




Public Class BancoDadosClasse
    Private Shared _instance As BancoDadosClasse
    Private Shared myconect As MySqlConnection
    Private Shared ReadOnly LockObj As New Object()

    ' Construtor privado
    Private Sub New()
        Dim conexaoString As String = "Server=lynxlocal.mysql.uhserver.com;database=lynxlocal;uid=lynxlocal;pwd=jHAzhFG848@yN@U;Max Pool Size=50;Connection Timeout=60;Connection Lifetime=30;CharSet=utf8;"
        myconect = New MySqlConnection(conexaoString)
        My.Settings.BancoDadosAtivo = "lynxlocal"
    End Sub

    ' Singleton para obter a instância
    Public Shared Function GetInstance() As BancoDadosClasse
        If _instance Is Nothing Then
            SyncLock LockObj
                If _instance Is Nothing Then
                    _instance = New BancoDadosClasse()
                End If
            End SyncLock
        End If
        Return _instance
    End Function

    ' Método para abrir o banco
    Public Function AbrirBanco() As Boolean
        Dim tentativas As Integer = 0
        Dim maxTentativas As Integer = 3

        While tentativas < maxTentativas
            Try
                If myconect.State = ConnectionState.Closed OrElse myconect.State = ConnectionState.Broken Then
                    myconect.Open()
                End If

                AbrirBanco = True
                Exit While
            Catch ex As MySqlException
                tentativas += 1
                If tentativas >= maxTentativas Then
                    MessageBox.Show("Erro ao conectar ao banco de dados após várias tentativas: " & ex.Message)
                    AbrirBanco = False
                    Exit While
                End If
            End Try
        End While

        Return AbrirBanco
    End Function

    ' Obter conexão
    Public Function ObterConexao() As MySqlConnection
        If myconect.State = ConnectionState.Closed OrElse myconect.State = ConnectionState.Broken Then
            myconect.Open()

        End If
        Return myconect
    End Function
End Class

