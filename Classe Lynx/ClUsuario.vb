
Imports MySql.Data.MySqlClient

Public Class clUsuario

    Public idUsuario, NomeCompleto, idSetor, Setor, TipoUsuario, Login, Senha, email, Descricao, Foto, txtCorte,
                           txtDobra, txtSolda, txtPintura, txtMontagem, txtAlmoxarifado, CriadoPor, DataCadastro,
                           D_E_L_E_T_E, MapaProducao, Romaneio, OrdemServico, SolidWorks, Sigla As String

    Public Function RetornaDadosUsuario(ByVal LoginEntrada As String, ByVal SenhaEntrada As String) As Boolean

        ' Verifica se o banco de dados não está aberto


        Try

            Using da As New MySqlCommand("SELECT * FROM usuario WHERE Login = @Login AND Senha = @Senha", myconect)
                ' Adicionar parâmetros para evitar SQL Injection
                da.Parameters.AddWithValue("@Login", LoginEntrada)
                da.Parameters.AddWithValue("@Senha", SenhaEntrada)

                ' Executar o comando e abrir o DataReader
                Using dr As MySqlDataReader = da.ExecuteReader()
                    ' Verificar se há linhas retornadas
                    If dr.HasRows Then
                        ' Ler a primeira linha retornada
                        dr.Read()

                        ' Preencher as variáveis com os valores retornados
                        idUsuario = dr("idUsuario").ToString()
                        NomeCompleto = dr("NomeCompleto").ToString()
                        idSetor = dr("idSetor").ToString()
                        Setor = dr("Setor").ToString()
                        TipoUsuario = dr("TipoUsuario").ToString()
                        Login = dr("Login").ToString()
                        Senha = dr("Senha").ToString()
                        email = dr("email").ToString()
                        Descricao = dr("Descricao").ToString()
                        txtCorte = dr("txtCorte").ToString()
                        txtDobra = dr("txtDobra").ToString()
                        txtSolda = dr("txtSolda").ToString()
                        txtPintura = dr("txtPintura").ToString()
                        txtMontagem = dr("txtMontagem").ToString()
                        txtAlmoxarifado = dr("txtAlmoxarifado").ToString()
                        CriadoPor = dr("CriadoPor").ToString()
                        DataCadastro = dr("DataCadastro").ToString()
                        D_E_L_E_T_E = dr("D_E_L_E_T_E").ToString()
                        MapaProducao = dr("MapaProducao").ToString()
                        Romaneio = dr("Romaneio").ToString()
                        OrdemServico = dr("OrdemServico").ToString()
                        SolidWorks = dr("SolidWorks").ToString()
                        Sigla = dr("Sigla").ToString()

                        RetornaDadosUsuario = True
                    Else
                        RetornaDadosUsuario = False
                    End If
                End Using
            End Using

        Catch ex As Exception

        Finally

        End Try



    End Function

End Class