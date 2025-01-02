Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient


Public Class CLOrdemServico

    Public IDOrdemServico As Integer
    Public IDOrdemServicoITEM As Integer
    Public PROJETO As String
    Public TAG As String
    Public IDPROJETO As String
    Public IDTAG As String
    Public DESCRICAO As String
    Public DESCEMPRESA As String
    Public ENDERECOOrdemServico As String
    Public CRIADOPOR As String
    Public DATACRIACAO As Date
    Public ESTATUS As String
    Public IdMaterial As String
    Public DescResumo As String
    Public DescDetal As String
    Public Autor As String
    Public Palavrachave As String
    Public Notas As String
    Public Espessura As String
    Public AreaPintura As String
    Public AreaPinturaUnitario As String
    Public NumeroDobras As String
    Public Peso As String
    Public PesoUnitario As String
    Public Unidade As String
    Public UnidadeSW As String
    Public ValorSW As String
    Public Altura As String
    Public Largura As String
    Public CodMatFabricante As String
    Public DtCad As String
    Public UsuarioCriacao As String
    Public UsuarioAlteracao As String
    Public DtAlteracao As String
    Public EnderecoArquivo As String
    Public MaterialSw As String
    Public QtdeTotal As String
    Public Qtde As String
    Public txtSoldagem As String
    Public txtTipoDesenho As String
    Public txtCorte As String
    Public txtDobra As String
    Public txtSolda As String
    Public txtPintura As String
    Public txtMontagem As String
    Public txtAcabamento As String
    Public tttxtCorte As String
    Public tttxtDobra As String
    Public tttxtSolda As String
    Public tttxtPintura As String
    Public tttxtMontagem As String
    Public DataPrevisao As String

    Public LIBERADO_ENGENHARIA As String
    Public DATA_LIBERACAO_ENGENHARIA As String
    Public idOSReferencia As String

    Public Comprimentocaixadelimitadora As String
    Public Larguracaixadelimitadora As String
    Public Espessuracaixadelimitadora As String
    Public txtItemEstoque As String

    Public RNC As String


    Public Function CriarOsCompleta(ByVal DgvGrid As DataGridView, ByVal timerDgvOS As Timer, ByVal timerDgvOSiTEM As Timer)

        If My.Settings.EnderecoPastaRaizOS.ToString = "" And System.IO.Directory.Exists(My.Settings.EnderecoPastaRaizOS) = False Then

            MsgBox("O endereço onde será criado a pasta da Ordem de Serviço  não foi informado!")
            Exit Function
        Else

            Try

                If TAG = "" Or PROJETO = "" Then

                    MsgBox("O Projeto e Tag devem ser informados", vbInformation, "Atenção")
                Else

                    ''''''' OrdemServico.IDPROJETO = Nothing
                    ''''''OrdemServico.PROJETO = PROJETO
                    '''''''   OrdemServico.IDTAG = Nothing
                    ''''''OrdemServico.TAG = TAG.ToUpper
                    ''''''OrdemServico.DESCRICAO = DESCRICAO.ToUpper
                    OrdemServico.CRIADOPOR = Usuario.NomeCompleto
                    OrdemServico.DATACRIACAO = Date.Now.Date
                    OrdemServico.ESTATUS = "A".ToUpper
                    ''''''OrdemServico.IDPROJETO = IDPROJETO
                    ''''''OrdemServico.IDTAG = IDTAG
                    ''''''OrdemServico.DESCEMPRESA = DESCEMPRESA
                    ''''''OrdemServico.LIBERADO_ENGENHARIA = ""
                    ''''''OrdemServico.DATA_LIBERACAO_ENGENHARIA = DESCEMPRESA
                    ''''''OrdemServico.idOSReferencia = ""

                    If OrdemServico.IDOrdemServico = Nothing Or
                        OrdemServico.IDOrdemServico.ToString = "" Or
                        OrdemServico.IDOrdemServico = 0 Then

                        Dim idosRetono As String

                        Try

                            idosRetono = cl_BancoDados.RetornaCampoDaPesquisa("SELECT IDOrdemServico from ordemservico where IDOrdemServico = '" & OrdemServico.IDOrdemServico & "'", "IDOrdemServico")
                        Catch ex As Exception
                            idosRetono = 0
                        Finally
                        End Try

                        If idosRetono = 0 Then

                            Try
                                Dim NovoIdOrdemServicoDB As Integer = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT max(IDOrdemServico)  as NovoIdOrdemServico FROM ordemservico", "NovoIdOrdemServico")) + 1

                                NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
                            Catch ex As Exception

                                ' Em caso de erro, atribuir "00001" como valor inicial
                                NovoIdOrdemServico = "00001"

                            End Try

                            OrdemServico.ENDERECOOrdemServico = (My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico).ToString.ToUpper

                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico)
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DXF")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PDF")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DFT")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PUNC")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LASER")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PROJETO")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PEÇAS DE ESTOQUE")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LXDS")

                            OrdemServico.LIBERADO_ENGENHARIA = ""
                            OrdemServico.DATA_LIBERACAO_ENGENHARIA = ""
                            OrdemServico.idOSReferencia = OrdemServico.IDOrdemServico

                            Try

                                OrdemServico.DataPrevisao = cl_BancoDados.RetornaCampoDaPesquisa("Select DataPrevisao from tags where idtag = '" & OrdemServico.IDTAG & "'", "DataPrevisao").ToString

                            Catch ex As Exception
                                OrdemServico.DataPrevisao = ""
                            End Try


                            SalvarOrdeMServicoBanco()

                            '                            cl_BancoDados.Salvar("insert into ordemservico (idProjeto,
                            'PROJETO,
                            'idTag,
                            'TAG,
                            'DESCRICAO,
                            'ENDERECOOrdemServico,
                            'CRIADOPOR,
                            'DATACRIACAO,
                            'ESTATUS,
                            'D_E_L_E_T_E,
                            'LIBERADO_ENGENHARIA,
                            'DATA_LIBERACAO_ENGENHARIA,
                            'IDOSReferencia,
                            'DESCEMPRESA,
                            'DataPrevisao)
                            'values
                            '('" & OrdemServico.IDPROJETO & "','" _
                            '            & OrdemServico.PROJETO & "','" _
                            '            & OrdemServico.IDTAG & "','" _
                            '            & OrdemServico.TAG & "','" _
                            '            & OrdemServico.DESCRICAO & "','" _
                            '            & OrdemServico.ENDERECOOrdemServico & "','" _
                            '            & OrdemServico.CRIADOPOR.ToString().ToUpper() & "','" _
                            '            & OrdemServico.DATACRIACAO & "','" _
                            '            & OrdemServico.ESTATUS & "','','" _
                            '            & OrdemServico.LIBERADO_ENGENHARIA & "','" _
                            '            & OrdemServico.DATA_LIBERACAO_ENGENHARIA & "','" _
                            '            & OrdemServico.idOSReferencia & "','" _
                            '            & OrdemServico.DESCEMPRESA & "','" _
                            '            & OrdemServico.DataPrevisao & "');")

                            timerDgvOS.Enabled = True

                            MsgBox("Ordem de Serviço Criada com sucesso!")

                        End If
                    Else

                        'Altera dos dados da Ordem de serviço
                        cl_BancoDados.Salvar("update ordemservico set DESCRICAO = '" & OrdemServico.DESCRICAO & "',
Projeto = '" & OrdemServico.PROJETO & "',
TAG = '" & OrdemServico.TAG & "',
IdProjeto = '" & OrdemServico.IDPROJETO & "',
IdTag = '" & OrdemServico.IDTAG & "',
descempresa = '" & OrdemServico.DESCEMPRESA & "'
where IDOrdemServico = '" & OrdemServico.IDOrdemServico & "'")

                        'Altera dos dados dos itens da Ordem de serviço
                        cl_BancoDados.Salvar("update ordemservicoitem set Projeto = '" & OrdemServico.PROJETO & "',
TAG = '" & OrdemServico.TAG & "',
IdProjeto = '" & OrdemServico.IDPROJETO & "',
IdTag = '" & OrdemServico.IDTAG & "'
where IDOrdemServico = '" & OrdemServico.IDOrdemServico & "' and idProjeto = '" & DgvGrid.CurrentRow.Cells("IdProjeto").Value & "' and IdTag = '" & DgvGrid.CurrentRow.Cells("IdTag").Value & "'")

                        ', , DESCRICAO, ESTATUS, , , ,  FROM ordemservico o;

                        DgvGrid.CurrentRow.Cells("Projeto").Value = OrdemServico.PROJETO

                        DgvGrid.CurrentRow.Cells("TAG").Value = OrdemServico.TAG

                        DgvGrid.CurrentRow.Cells("IdProjeto").Value = OrdemServico.IDPROJETO

                        DgvGrid.CurrentRow.Cells("IdTag").Value = OrdemServico.IDTAG

                        DgvGrid.CurrentRow.Cells("DESCRICAO").Value = OrdemServico.DESCRICAO

                        DgvGrid.CurrentRow.Cells("descempresa").Value = OrdemServico.DESCEMPRESA

                        DgvGrid.CurrentRow.Cells("DataPrevisao").Value = OrdemServico.DataPrevisao

                        MsgBox("Ordem de serviço alterada com sucesso!")

                    End If

                End If

                timerDgvOSiTEM.Enabled = True
            Catch ex As Exception

                MsgBox("Erro ao criar a Ordem de Serviço" & ex.Message)
            Finally
            End Try

        End If

    End Function


    Public Function SalvarOrdeMServicoBanco()

        Try



            Dim cmd As New MySqlCommand("insert into ordemservico 
    (idProjeto, PROJETO, idTag, TAG, DESCRICAO, ENDERECOOrdemServico, 
    CRIADOPOR, DATACRIACAO, ESTATUS, D_E_L_E_T_E, LIBERADO_ENGENHARIA, 
    DATA_LIBERACAO_ENGENHARIA, IDOSReferencia, DESCEMPRESA, DataPrevisao) 
    values 
    (@idProjeto, @PROJETO, @idTag, @TAG, @DESCRICAO, @ENDERECOOrdemServico, 
    @CRIADOPOR, @DATACRIACAO, @ESTATUS, @D_E_L_E_T_E, @LIBERADO_ENGENHARIA, 
    @DATA_LIBERACAO_ENGENHARIA, @IDOSReferencia, @DESCEMPRESA, @DataPrevisao)", myconect)

            cmd.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
            cmd.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
            cmd.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
            cmd.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
            cmd.Parameters.AddWithValue("@DESCRICAO", OrdemServico.DESCRICAO)
            cmd.Parameters.AddWithValue("@ENDERECOOrdemServico", OrdemServico.ENDERECOOrdemServico)
            cmd.Parameters.AddWithValue("@CRIADOPOR", If(String.IsNullOrEmpty(OrdemServico.CRIADOPOR), DBNull.Value, OrdemServico.CRIADOPOR.ToUpper()))
            cmd.Parameters.AddWithValue("@DATACRIACAO", OrdemServico.DATACRIACAO)
            cmd.Parameters.AddWithValue("@ESTATUS", OrdemServico.ESTATUS)
            cmd.Parameters.AddWithValue("@D_E_L_E_T_E", "")  ' ou tratar conforme necessário
            cmd.Parameters.AddWithValue("@LIBERADO_ENGENHARIA", OrdemServico.LIBERADO_ENGENHARIA)
            cmd.Parameters.AddWithValue("@DATA_LIBERACAO_ENGENHARIA", OrdemServico.DATA_LIBERACAO_ENGENHARIA)
            cmd.Parameters.AddWithValue("@IDOSReferencia", OrdemServico.idOSReferencia)
            cmd.Parameters.AddWithValue("@DESCEMPRESA", OrdemServico.DESCEMPRESA)
            cmd.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)

            cmd.ExecuteNonQuery()


        Catch ex As Exception

            MsgBox(ex.Message)
        Finally

        End Try


    End Function

End Class

Public Class CLOrdemServicoItem

    Public IDOrdemServicoITEM As Integer
    Public IDOrdemServico As Integer
    Public PROJETO As String
    Public TAG As String
    Public ESTATUS_OrdemServico As String
    Public IdMaterial As Integer
    Public QtdeTotal As Double
    Public CRIADOPOR As String
    Public DATACRIACAO As String
    Public ESTATUS As String
    Public ACABAMENTO As String
    Public PrevDataEntrega As String

End Class

Public Class CLOrdemServicoItemPendencia

    Public idordemservicoitempendencia As Integer
    Public IDOrdemServicoITEM As Integer
    Public IDOrdemServico As Integer
    Public IdMaterial As Integer
    Public DescricaoPendencia As String
    Public DescricaoFinalizacao As String
    Public Usuario As String
    Public DataCriacao As String
    Public D_E_L_E_T_E As String
    Public UsuarioProjeto As String
    Public DataAcertoProjet As String
    Public estatu As String

End Class

Public Class clProjeto

    Public IdProjeto As String
    Public Projeto As String
    Public DescProjeto As String
    Public Responsavel As String
    Public DescEmpresa As String
    Public DataEntrada As String
    Public DataPrevisao As String
    Public DataTermino As String
    Public TotalProjeto As String
    Public StatusProj As String
    Public D_E_L_E_T_E As String
    Public DescStatus As String
    Public IdEmpresa As String
    Public liberado As String
    Public UsuarioD_E_L_E_T_E As String
    Public DataD_E_L_E_T_E As String

    Public Sub SalvarDadosNoBanco(
    ByVal idProjeto As String, ByVal projeto As String, ByVal descProjeto As String,
    ByVal responsavel As String, ByVal descEmpresa As String, ByVal dataEntrada As String,
    ByVal dataPrevisao As String, ByVal dataTermino As String, ByVal totalProjeto As String,
    ByVal statusProj As String, ByVal d_e_l_e_t_e As String, ByVal descStatus As String,
    ByVal idEmpresa As String, ByVal liberado As String,
    ByVal usuarioD_e_l_e_t_e As String, ByVal dataD_e_l_e_t_e As String
)
        Using conn As New MySqlConnection(conexao)

            Try
                '   conn.Open()

                ' SQL de inserção
                Dim query As String = "INSERT INTO projetos " &
                                  "(Projeto, DescProjeto, Responsavel, DescEmpresa, DataEntrada, DataPrevisao, DataTermino, TotalProjeto, StatusProj, D_E_L_E_T_E, DescStatus, IdEmpresa, Liberado, UsuarioD_E_L_E_T_E, DataD_E_L_E_T_E) " &
                                  "VALUES (@IdProjeto, @Projeto, @DescProjeto, @Responsavel, @DescEmpresa, @DataEntrada, @DataPrevisao, @DataTermino, @TotalProjeto, @StatusProj, @D_E_L_E_T_E, @DescStatus, @IdEmpresa, @Liberado, @UsuarioD_E_L_E_T_E, @DataD_E_L_E_T_E)"

                ' Comando e parâmetros
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdProjeto", idProjeto)
                    cmd.Parameters.AddWithValue("@Projeto", projeto)
                    cmd.Parameters.AddWithValue("@DescProjeto", descProjeto)
                    cmd.Parameters.AddWithValue("@Responsavel", responsavel)
                    cmd.Parameters.AddWithValue("@DescEmpresa", descEmpresa)
                    cmd.Parameters.AddWithValue("@DataEntrada", dataEntrada)
                    cmd.Parameters.AddWithValue("@DataPrevisao", dataPrevisao)
                    cmd.Parameters.AddWithValue("@DataTermino", dataTermino)
                    cmd.Parameters.AddWithValue("@TotalProjeto", totalProjeto)
                    cmd.Parameters.AddWithValue("@StatusProj", statusProj)
                    cmd.Parameters.AddWithValue("@D_E_L_E_T_E", d_e_l_e_t_e)
                    cmd.Parameters.AddWithValue("@DescStatus", descStatus)
                    cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa)
                    cmd.Parameters.AddWithValue("@Liberado", liberado)
                    cmd.Parameters.AddWithValue("@UsuarioD_E_L_E_T_E", usuarioD_e_l_e_t_e)
                    cmd.Parameters.AddWithValue("@DataD_E_L_E_T_E", dataD_e_l_e_t_e)

                    ' Executar o comando
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As MySqlException
                '  MsgBox("Erro ao salvar os dados: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Public Sub AtualizarDadosNoBanco(
    ByVal idProjeto As String, ByVal projeto As String, ByVal descProjeto As String,
    ByVal responsavel As String, ByVal descEmpresa As String, ByVal dataEntrada As String,
    ByVal dataPrevisao As String, ByVal dataTermino As String, ByVal totalProjeto As String,
    ByVal statusProj As String, ByVal d_e_l_e_t_e As String, ByVal descStatus As String,
    ByVal idEmpresa As String, ByVal liberado As String,
    ByVal usuarioD_e_l_e_t_e As String, ByVal dataD_e_l_e_t_e As String
)
        Using conn As New MySqlConnection(conexao)

            Try
                '  conn.Open()

                ' SQL de atualização
                Dim query As String = "UPDATE projetos SET " &
                                  "Projeto = @Projeto, DescProjeto = @DescProjeto, Responsavel = @Responsavel, " &
                                  "DescEmpresa = @DescEmpresa, DataEntrada = @DataEntrada, DataPrevisao = @DataPrevisao, " &
                                  "DataTermino = @DataTermino, TotalProjeto = @TotalProjeto, StatusProj = @StatusProj, " &
                                  "D_E_L_E_T_E = @D_E_L_E_T_E, DescStatus = @DescStatus, IdEmpresa = @IdEmpresa, " &
                                  "Liberado = @Liberado, UsuarioD_E_L_E_T_E = @UsuarioD_E_L_E_T_E, DataD_E_L_E_T_E = @DataD_E_L_E_T_E " &
                                  "WHERE IdProjeto = @IdProjeto"

                ' Comando e parâmetros
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdProjeto", idProjeto)
                    cmd.Parameters.AddWithValue("@Projeto", projeto)
                    cmd.Parameters.AddWithValue("@DescProjeto", descProjeto)
                    cmd.Parameters.AddWithValue("@Responsavel", responsavel)
                    cmd.Parameters.AddWithValue("@DescEmpresa", descEmpresa)
                    cmd.Parameters.AddWithValue("@DataEntrada", dataEntrada)
                    cmd.Parameters.AddWithValue("@DataPrevisao", dataPrevisao)
                    cmd.Parameters.AddWithValue("@DataTermino", dataTermino)
                    cmd.Parameters.AddWithValue("@TotalProjeto", totalProjeto)
                    cmd.Parameters.AddWithValue("@StatusProj", statusProj)
                    cmd.Parameters.AddWithValue("@D_E_L_E_T_E", d_e_l_e_t_e)
                    cmd.Parameters.AddWithValue("@DescStatus", descStatus)
                    cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa)
                    cmd.Parameters.AddWithValue("@Liberado", liberado)
                    cmd.Parameters.AddWithValue("@UsuarioD_E_L_E_T_E", usuarioD_e_l_e_t_e)
                    cmd.Parameters.AddWithValue("@DataD_E_L_E_T_E", dataD_e_l_e_t_e)

                    ' Executar o comando
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As MySqlException
                '  MsgBox("Erro ao atualizar os dados: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Public Sub MarcarComoDeletado(ByVal idProjeto As String)

        Using conn As New MySqlConnection(conexao)

            Try
                '  conn.Open()

                ' SQL de atualização
                Dim query As String = "UPDATE projetos SET " &
                                  "D_E_L_E_T_E = '*', " &
                                  "UsuarioD_E_L_E_T_E = @Usuario, " &
                                  "DataD_E_L_E_T_E = @DataAtual " &
                                  "WHERE IdProjeto = @IdProjeto"

                ' Comando e parâmetros
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdProjeto", idProjeto)
                    cmd.Parameters.AddWithValue("@Usuario", My.User.Name)
                    cmd.Parameters.AddWithValue("@DataAtual", Date.Now.Date)

                    ' Executar o comando
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As MySqlException
                ' MsgBox("Erro ao atualizar os dados: " & ex.Message)
            Finally
                conn.Close()
            End Try

        End Using

    End Sub

    Public Function ValidarDadosEntrada(
     ByVal projeto As String, ByVal descProjeto As String,
    ByVal responsavel As String, ByVal descEmpresa As String, ByVal dataEntrada As String,
    ByVal dataPrevisao As String, ByVal dataTermino As String, ByVal totalProjeto As String,
    ByVal statusProj As String
) As String

        ' Verificar campos obrigatórios
        If String.IsNullOrWhiteSpace(projeto) Then
            Return "O campo 'Projeto' é obrigatório."
        End If

        If String.IsNullOrWhiteSpace(descProjeto) Then
            Return "O campo 'Descrição do Projeto' é obrigatório."
        End If

        If String.IsNullOrWhiteSpace(responsavel) Then
            Return "O campo 'Responsável' é obrigatório."
        End If

        If String.IsNullOrWhiteSpace(dataEntrada) Then
            Return "O campo 'Data de Entrada' é obrigatório."
        End If

        ' Validar formato de data
        Dim dataValida As DateTime
        If Not DateTime.TryParse(dataEntrada, dataValida) Then
            Return "O campo 'Data de Entrada' deve conter uma data válida."
        End If

        If Not String.IsNullOrWhiteSpace(dataPrevisao) AndAlso Not DateTime.TryParse(dataPrevisao, dataValida) Then
            Return "O campo 'Data de Previsão' deve conter uma data válida."
        End If

        If Not String.IsNullOrWhiteSpace(dataTermino) AndAlso Not DateTime.TryParse(dataTermino, dataValida) Then
            Return "O campo 'Data de Término' deve conter uma data válida."
        End If

        ' Validar valores numéricos
        Dim total As Decimal
        If Not String.IsNullOrWhiteSpace(totalProjeto) AndAlso Not Decimal.TryParse(totalProjeto, total) Then
            Return "O campo 'Total do Projeto' deve conter um valor numérico válido."
        End If

        ' Validar valores específicos (opcional)
        Dim statusValidos As String() = {"Em Andamento", "Concluído", "Pendente"}
        If Not String.IsNullOrWhiteSpace(statusProj) AndAlso Not statusValidos.Contains(statusProj) Then
            Return "O campo 'Status do Projeto' contém um valor inválido."
        End If

        ' Se todas as validações passarem, retornar string vazia
        Return String.Empty
    End Function



End Class

