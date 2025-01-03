Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient


Public Class CLOrdemServico

    Public IdOrdemServico As Integer
    Public IDOrdemServicoItem As Integer
    Public Projeto As String
    Public Tag As String
    Public idProjeto As String
    Public idTag As String
    Public Descricao As String
    Public DescEmpresa As String
    Public EnderecoOrdemServico As String
    Public CriadoPor As String
    Public DataCriacao As Date
    Public Estatus As String
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
    Public MaterialSW As String
    Public QtdeTotal As String
    Public qtde As String
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

    Public Liberado_Engenharia As String
    Public Data_Liberacao_Engenharia As String
    Public IdOSReferencia As String

    Public Comprimentocaixadelimitadora As String
    Public Larguracaixadelimitadora As String
    Public Espessuracaixadelimitadora As String
    Public txtItemEstoque As String

    Public RNC As String

    Public ProdutoPadrao As String
    Public CodDesenhoProduto As String
    Public CodOmie As String
    Public DescricaoProduto As String
    Public EnderecoFichaTecnica As String
    Public EnderecoIsometrico As String
    Public ProdutoCriadoPor As String
    Public DataCriacaoProduto As String
    Public Function CriarOsCompleta(ByVal DgvGrid As DataGridView, ByVal timerDgvOS As Timer, ByVal timerDgvOSiTEM As Timer)

        If My.Settings.EnderecoPastaRaizOS.ToString = "" And System.IO.Directory.Exists(My.Settings.EnderecoPastaRaizOS) = False Then

            MsgBox("O endereço onde será criado a pasta da Ordem de Serviço  não foi informado!")
            Exit Function
        Else

            Try

                If Tag = "" Or Projeto = "" Then

                    MsgBox("O Projeto e Tag devem ser informados", vbInformation, "Atenção")
                Else

                    ''''''' OrdemServico.idProjeto = Nothing
                    ''''''OrdemServico.Projeto = Projeto
                    '''''''   OrdemServico.idTag = Nothing
                    ''''''OrdemServico.Tag = Tag.ToUpper
                    ''''''OrdemServico.Descricao = Descricao.ToUpper
                    OrdemServico.CriadoPor = Usuario.NomeCompleto
                    OrdemServico.DataCriacao = Date.Now.Date
                    OrdemServico.Estatus = "A".ToUpper
                    ''''''OrdemServico.idProjeto = idProjeto
                    ''''''OrdemServico.idTag = idTag
                    ''''''OrdemServico.DescEmpresa = DescEmpresa
                    ''''''OrdemServico.Liberado_Engenharia = ""
                    ''''''OrdemServico.Data_Liberacao_Engenharia = DescEmpresa
                    ''''''OrdemServico.IdOSReferencia = ""

                    If OrdemServico.IdOrdemServico = Nothing Or
                        OrdemServico.IdOrdemServico.ToString = "" Or
                        OrdemServico.IdOrdemServico = 0 Then

                        Dim idosRetono As String

                        Try

                            idosRetono = cl_BancoDados.RetornaCampoDaPesquisa("SELECT IdOrdemServico from  " & ComplementoTipoBanco & "ordemservico where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'", "IdOrdemServico")
                        Catch ex As Exception
                            idosRetono = 0
                        Finally
                        End Try

                        If idosRetono = 0 Then

                            Try
                                Dim NovoIdOrdemServicoDB As Integer = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT max(IdOrdemServico)  as NovoIdOrdemServico FROM  " & ComplementoTipoBanco & "ordemservico", "NovoIdOrdemServico")) + 1

                                NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
                            Catch ex As Exception

                                ' Em caso de erro, atribuir "00001" como valor inicial
                                NovoIdOrdemServico = "00001"

                            End Try

                            OrdemServico.EnderecoOrdemServico = (My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico).ToString.ToUpper

                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico)
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DXF")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PDF")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DFT")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PUNC")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LASER")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\Projeto")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PEÇAS DE ESTOQUE")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LXDS")

                            OrdemServico.Liberado_Engenharia = ""
                            OrdemServico.Data_Liberacao_Engenharia = ""
                            OrdemServico.IdOSReferencia = OrdemServico.IdOrdemServico

                            Try

                                OrdemServico.DataPrevisao = cl_BancoDados.RetornaCampoDaPesquisa("Select DataPrevisao from  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "DataPrevisao").ToString

                            Catch ex As Exception
                                OrdemServico.DataPrevisao = ""
                            End Try


                            SalvarOrdeMServicoBanco()

                            '                            cl_BancoDados.Salvar("insert into ordemservico (idProjeto,
                            'Projeto,
                            'idTag,
                            'Tag,
                            'Descricao,
                            'EnderecoOrdemServico,
                            'CriadoPor,
                            'DataCriacao,
                            'Estatus,
                            'D_E_L_E_T_E,
                            'Liberado_Engenharia,
                            'Data_Liberacao_Engenharia,
                            'IdOSReferencia,
                            'DescEmpresa,
                            'DataPrevisao)
                            'values
                            '('" & OrdemServico.idProjeto & "','" _
                            '            & OrdemServico.Projeto & "','" _
                            '            & OrdemServico.idTag & "','" _
                            '            & OrdemServico.Tag & "','" _
                            '            & OrdemServico.Descricao & "','" _
                            '            & OrdemServico.EnderecoOrdemServico & "','" _
                            '            & OrdemServico.CriadoPor.ToString().ToUpper() & "','" _
                            '            & OrdemServico.DataCriacao & "','" _
                            '            & OrdemServico.Estatus & "','','" _
                            '            & OrdemServico.Liberado_Engenharia & "','" _
                            '            & OrdemServico.Data_Liberacao_Engenharia & "','" _
                            '            & OrdemServico.IdOSReferencia & "','" _
                            '            & OrdemServico.DescEmpresa & "','" _
                            '            & OrdemServico.DataPrevisao & "');")

                            timerDgvOS.Enabled = True

                            MsgBox("Ordem de Serviço Criada com sucesso!")

                        End If
                    Else

                        'Altera dos dados da Ordem de serviço
                        cl_BancoDados.Salvar("update ordemservico set Descricao = '" & OrdemServico.Descricao & "',
Projeto = '" & OrdemServico.Projeto & "',
Tag = '" & OrdemServico.Tag & "',
idProjeto = '" & OrdemServico.idProjeto & "',
idTag = '" & OrdemServico.idTag & "',
DescEmpresa = '" & OrdemServico.DescEmpresa & "'
where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")

                        'Altera dos dados dos itens da Ordem de serviço
                        cl_BancoDados.Salvar("update  " & ComplementoTipoBanco & "ordemservicoitem set Projeto = '" & OrdemServico.Projeto & "',
Tag = '" & OrdemServico.Tag & "',
idProjeto = '" & OrdemServico.idProjeto & "',
idTag = '" & OrdemServico.idTag & "'
where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'") ' and idProjeto = '" & OrdemServico.idProjeto & "' and idTag = '" & OrdemServico.idTag & "'")

                        ', , Descricao, Estatus, , , ,  FROM ordemservico o;

                        DgvGrid.CurrentRow.Cells("Projeto").Value = OrdemServico.Projeto

                        DgvGrid.CurrentRow.Cells("Tag").Value = OrdemServico.Tag

                        DgvGrid.CurrentRow.Cells("idProjeto").Value = OrdemServico.idProjeto

                        DgvGrid.CurrentRow.Cells("idTag").Value = OrdemServico.idTag

                        DgvGrid.CurrentRow.Cells("Descricao").Value = OrdemServico.Descricao

                        DgvGrid.CurrentRow.Cells("DescEmpresa").Value = OrdemServico.DescEmpresa

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

        If TipoBanco = "MYSQL" Then


            Try



                Dim cmd As New MySqlCommand("insert into  " & ComplementoTipoBanco & "ordemservico 
    (idProjeto, Projeto, idTag, Tag, Descricao, EnderecoOrdemServico, 
    CriadoPor, DataCriacao, Estatus, D_E_L_E_T_E, Liberado_Engenharia, 
    Data_Liberacao_Engenharia, IdOSReferencia, DescEmpresa, DataPrevisao) 
    values 
    (@idProjeto, @Projeto, @idTag, @Tag, @Descricao, @EnderecoOrdemServico, 
    @CriadoPor, @DataCriacao, @Estatus, @D_E_L_E_T_E, @Liberado_Engenharia, 
    @Data_Liberacao_Engenharia, @IdOSReferencia, @DescEmpresa, @DataPrevisao)", myconect)

                cmd.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                cmd.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                cmd.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                cmd.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                cmd.Parameters.AddWithValue("@Descricao", OrdemServico.Descricao)
                cmd.Parameters.AddWithValue("@EnderecoOrdemServico", OrdemServico.EnderecoOrdemServico)
                cmd.Parameters.AddWithValue("@CriadoPor", If(String.IsNullOrEmpty(OrdemServico.CriadoPor), DBNull.Value, OrdemServico.CriadoPor.ToUpper()))
                cmd.Parameters.AddWithValue("@DataCriacao", OrdemServico.DataCriacao)
                cmd.Parameters.AddWithValue("@Estatus", OrdemServico.Estatus)
                cmd.Parameters.AddWithValue("@D_E_L_E_T_E", "")  ' ou tratar conforme necessário
                cmd.Parameters.AddWithValue("@Liberado_Engenharia", OrdemServico.Liberado_Engenharia)
                cmd.Parameters.AddWithValue("@Data_Liberacao_Engenharia", OrdemServico.Data_Liberacao_Engenharia)
                cmd.Parameters.AddWithValue("@IdOSReferencia", OrdemServico.IdOSReferencia)
                cmd.Parameters.AddWithValue("@DescEmpresa", OrdemServico.DescEmpresa)
                cmd.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)

                cmd.ExecuteNonQuery()


            Catch ex As Exception

                MsgBox(ex.Message)
            Finally

            End Try

        ElseIf TipoBanco = "SQL" Then


            Try



                Dim cmd As New SqlCommand("insert into " & ComplementoTipoBanco & " ordemservico 
    (idProjeto, Projeto, idTag, Tag, Descricao, EnderecoOrdemServico, 
    CriadoPor, DataCriacao, Estatus, D_E_L_E_T_E, Liberado_Engenharia, 
    Data_Liberacao_Engenharia, IdOSReferencia, DescEmpresa, DataPrevisao) 
    values 
    (@idProjeto, @Projeto, @idTag, @Tag, @Descricao, @EnderecoOrdemServico, 
    @CriadoPor, @DataCriacao, @Estatus, @D_E_L_E_T_E, @Liberado_Engenharia, 
    @Data_Liberacao_Engenharia, @IdOSReferencia, @DescEmpresa, @DataPrevisao)", myconectSQL)

                cmd.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                cmd.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                cmd.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                cmd.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                cmd.Parameters.AddWithValue("@Descricao", OrdemServico.Descricao)
                cmd.Parameters.AddWithValue("@EnderecoOrdemServico", OrdemServico.EnderecoOrdemServico)
                cmd.Parameters.AddWithValue("@CriadoPor", If(String.IsNullOrEmpty(OrdemServico.CriadoPor), DBNull.Value, OrdemServico.CriadoPor.ToUpper()))
                cmd.Parameters.AddWithValue("@DataCriacao", OrdemServico.DataCriacao)
                cmd.Parameters.AddWithValue("@Estatus", OrdemServico.Estatus)
                cmd.Parameters.AddWithValue("@D_E_L_E_T_E", "")  ' ou tratar conforme necessário
                cmd.Parameters.AddWithValue("@Liberado_Engenharia", OrdemServico.Liberado_Engenharia)
                cmd.Parameters.AddWithValue("@Data_Liberacao_Engenharia", OrdemServico.Data_Liberacao_Engenharia)
                cmd.Parameters.AddWithValue("@IdOSReferencia", OrdemServico.IdOSReferencia)
                cmd.Parameters.AddWithValue("@DescEmpresa", OrdemServico.DescEmpresa)
                cmd.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)

                cmd.ExecuteNonQuery()


            Catch ex As Exception

                MsgBox(ex.Message)
            Finally

            End Try


        End If


    End Function

End Class

Public Class CLOrdemServicoItem

    Public IDOrdemServicoItem As Integer
    Public IdOrdemServico As Integer
    Public Projeto As String
    Public Tag As String
    Public ESTATUS_OrdemServico As String
    Public IdMaterial As Integer
    Public QtdeTotal As Double
    Public CriadoPor As String
    Public DataCriacao As String
    Public Estatus As String
    Public Acabamento As String
    Public PrevDataEntrega As String

End Class

Public Class CLOrdemServicoItemPendencia

    Public idordemservicoitempendencia As Integer
    Public IDOrdemServicoItem As Integer
    Public IdOrdemServico As Integer
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

    Public idProjeto As String
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
    ByVal idProjeto As String, ByVal Projeto As String, ByVal descProjeto As String,
    ByVal responsavel As String, ByVal DescEmpresa As String, ByVal dataEntrada As String,
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
                                  "VALUES (@idProjeto, @Projeto, @DescProjeto, @Responsavel, @DescEmpresa, @DataEntrada, @DataPrevisao, @DataTermino, @TotalProjeto, @StatusProj, @D_E_L_E_T_E, @DescStatus, @IdEmpresa, @Liberado, @UsuarioD_E_L_E_T_E, @DataD_E_L_E_T_E)"

                ' Comando e parâmetros
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idProjeto", idProjeto)
                    cmd.Parameters.AddWithValue("@Projeto", Projeto)
                    cmd.Parameters.AddWithValue("@DescProjeto", descProjeto)
                    cmd.Parameters.AddWithValue("@Responsavel", responsavel)
                    cmd.Parameters.AddWithValue("@DescEmpresa", DescEmpresa)
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
    ByVal idProjeto As String, ByVal Projeto As String, ByVal descProjeto As String,
    ByVal responsavel As String, ByVal DescEmpresa As String, ByVal dataEntrada As String,
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
                                  "WHERE idProjeto = @idProjeto"

                ' Comando e parâmetros
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idProjeto", idProjeto)
                    cmd.Parameters.AddWithValue("@Projeto", Projeto)
                    cmd.Parameters.AddWithValue("@DescProjeto", descProjeto)
                    cmd.Parameters.AddWithValue("@Responsavel", responsavel)
                    cmd.Parameters.AddWithValue("@DescEmpresa", DescEmpresa)
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
                                  "WHERE idProjeto = @idProjeto"

                ' Comando e parâmetros
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idProjeto", idProjeto)
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
     ByVal Projeto As String, ByVal descProjeto As String,
    ByVal responsavel As String, ByVal DescEmpresa As String, ByVal dataEntrada As String,
    ByVal dataPrevisao As String, ByVal dataTermino As String, ByVal totalProjeto As String,
    ByVal statusProj As String
) As String

        ' Verificar campos obrigatórios
        If String.IsNullOrWhiteSpace(Projeto) Then
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

