Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel
Imports MySql.Data.MySqlClient
Imports Mysqlx.Crud
Imports Mysqlx.Notice
Imports Org.BouncyCastle.Asn1.X509
Imports Org.BouncyCastle.Bcpg
Imports Org.BouncyCastle.Crypto
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports SolidWorks.Interop.swdocumentmgr
Imports SolidWorks.Interop.swpublished
Imports SolidWorksTools
Imports SolidWorksTools.File
Imports SwLynx_4._1.My




Imports System.Runtime.CompilerServices.Unsafe




Imports Google.Protobuf.WellKnownTypes


Imports System.Net.Mail
Imports System.Net.Mime
Imports System.IO.Compression

Imports netDxf
Imports netDxf.Entities
Imports netDxf.Tables


Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas
Imports iText.Kernel.Font
Imports iText.IO.Font.Constants
Imports System.ComponentModel
Imports System.Threading.Tasks
Imports Rectangle = System.Drawing.Rectangle
Imports SolidWorks.Interop.dsgnchk
Imports Assembly = System.Reflection.Assembly
Imports iText.Commons.Bouncycastle
Imports System.Data.SqlClient







<ComVisible(True)>
<ProgId("SwLynx_4._1")>
Public Class Painel_Leitura_Dados

    Friend Sub getSwApp(ByRef swAddin As SolidWorks.Interop.sldworks.SldWorks)

        swapp = swAddin

    End Sub

    Dim dt As New System.Data.DataTable
    Dim dtConsolidado As New System.Data.DataTable()

    Public iconeTipoArquivo As System.Drawing.Image
    Public iconeAtencao As System.Drawing.Image
    Public iconePDF As System.Drawing.Image
    Public iconeDXF As System.Drawing.Image


    Public Function AtualizaTela(ByVal swModel As ModelDoc2) As Boolean

        ' Conectar ao SolidWorks
        IntanciaSolidWorks.ConectarSolidWorks()


        ' Obter o documento ativo
        ' swModel = swapp.ActiveDoc
        DadosArquivoCorrente.EnderecoArquivo = swModel.GetPathName().ToUpper()
        DadosArquivoCorrente.NomeArquivoComExtensao = Path.GetFileName(DadosArquivoCorrente.EnderecoArquivo).ToUpper()
        DadosArquivoCorrente.NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(DadosArquivoCorrente.EnderecoArquivo)

        ' Verifique se o modelo foi aberto com sucesso
        If Not swModel Is Nothing Then

            ' Usa Select Case para diferenciar o tipo do documento
            If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                '****************************************************************
                'BUSCA DADOS DA LEITURA DO DESENHO

                Me.cboTitulo.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle)
                Me.txtAssuntoSubiTitulo.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject).ToUpper()
                Me.txtComentarios.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoComment).ToUpper()
                Me.txtAuthor.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoAuthor).ToUpper()
                Me.txtPalavraChave.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords).ToUpper()


                'Lista de Corte
                Me.lblEspessura.Text = DadosArquivoCorrente.Espessura
                Me.lblLargura.Text = DadosArquivoCorrente.LarguraBlank
                Me.lblComprimento.Text = DadosArquivoCorrente.ComprimentoBlank

                Me.lblNumeroDobra.Text = DadosArquivoCorrente.NumeroDobras
                Me.lblPeso.Text = DadosArquivoCorrente.Massa.ToString
                Me.lblMaterial.Text = DadosArquivoCorrente.Material.ToString
                Me.lblAreaPintura.Text = DadosArquivoCorrente.AreaPintura

                Me.lblAlturaTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Alturacaixadelimitadora
                Me.lblLarguraTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Larguracaixadelimitadora
                Me.lblProfundidadeTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Profundidadeaixadelimitadora

                optProcessoSoldagemSim.Checked = (DadosArquivoCorrente.soldagem = "SIM")
                If optProcessoSoldagemSim.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSoldagem", "NÃO", "NÃO")
                End If


                '  optProcessoSoldagemNao.Checked = Not optProcessoSoldagemSim.Checked




                OPTEstoqueSim.Checked = (DadosArquivoCorrente.ItemEstoque = "SIM")
                If OPTEstoqueSim.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtItemEstoque", "NÃO", "NÃO")
                End If


                '   OPTEstoqueNao.Checked = Not OPTEstoqueSim.Checked

                Me.lblPeso.Text = DadosArquivoCorrente.Massa

                Me.lblMaterial.Text = DadosArquivoCorrente.Material.ToString


                Dim extensoes As String() = {".pdf", ".dxf", ".dft", ".lxds"}
                Dim checkboxes As System.Windows.Forms.CheckBox() = {chkVerificarPDF, chkVerificarDXF, chkVerificarDFT, chkVerificarLXDS}

                For i As Integer = 0 To extensoes.Length - 1
                    Dim caminhoAlterado As String = System.IO.Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, extensoes(i))
                    Select Case extensoes(i)
                        Case ".pdf"
                            DadosArquivoCorrente.ArquivoPdf = caminhoAlterado
                        Case ".dxf"
                            DadosArquivoCorrente.ArquivoDxf = caminhoAlterado
                        Case ".dft"
                            DadosArquivoCorrente.ArquivoDft = caminhoAlterado
                        Case ".lxds"
                            DadosArquivoCorrente.ArquivoLXDS = caminhoAlterado
                    End Select
                    checkboxes(i).Checked = File.Exists(caminhoAlterado)
                Next

                Try
                    ' Recuperar Sobra_Fabrica
                    DadosArquivoCorrente.Sobra_Fabrica = cl_BancoDados.RetornaCampoDaPesquisa(
        $"SELECT Sobra_Fabrica FROM material WHERE codmatFabricante = '{DadosArquivoCorrente.NomeArquivoSemExtensao}'",
        "Sobra_Fabrica"
    )
                    lblQtdeEstoque.Text = DadosArquivoCorrente.Sobra_Fabrica

                Catch ex As Exception
                    ' Definir valor padrão em caso de erro
                    DadosArquivoCorrente.Sobra_Fabrica = "00"
                    lblQtdeEstoque.Text = DadosArquivoCorrente.Sobra_Fabrica
                End Try

                Try
                    ' Recuperar RNC
                    DadosArquivoCorrente.rnc = cl_BancoDados.RetornaCampoDaPesquisa(
        $"SELECT RNC FROM material WHERE codmatFabricante = '{DadosArquivoCorrente.NomeArquivoSemExtensao}'",
        "RNC"
    )

                    If Not String.IsNullOrEmpty(DadosArquivoCorrente.rnc) Then
                        '                ' Recuperar DescricaoPendencia se RNC não estiver vazio
                        '                DadosArquivoCorrente.DescricaoPendencia = cl_BancoDados.RetornaCampoDaPesquisa(
                        '    $"SELECT DescricaoPendencia FROM ordemservicoitempendencia 
                        '      WHERE codmatfabricante = '{DadosArquivoCorrente.NomeArquivoSemExtensao}' 
                        '      AND ESTATUS = 'PENDENCIA' 
                        '      AND d_e_l_e_t_e IS NULL",
                        '    "DescricaoPendencia"
                        ')
                        'txtDescricaoPendencia.Text = DadosArquivoCorrente.DescricaoPendencia
                        btnPendencias.Image = My.Resources.atencao
                        btnPendencias.Enabled = True

                    Else

                        ' RNC vazio
                        LimparPendencias()

                    End If
                Catch ex As Exception
                    ' Tratar falha na recuperação de RNC ou DescricaoPendencia
                    DadosArquivoCorrente.rnc = ""
                    LimparPendencias()
                End Try


                For i As Integer = 0 To chkBoxAcabamento.Items.Count - 1
                    If DadosArquivoCorrente.acabamento.ToString = chkBoxAcabamento.Items(i).ToString Then
                        chkBoxAcabamento.Text = DadosArquivoCorrente.acabamento
                        chkBoxAcabamento.SetItemChecked(i, True) ' Marca o item correspondente
                    Else
                        chkBoxAcabamento.SetItemChecked(i, False) ' Desmarca outros itens
                    End If
                Next

                For i As Integer = 0 To chkBoxTipoDesenho.Items.Count - 1
                    If DadosArquivoCorrente.TipoDesenho.ToString = chkBoxTipoDesenho.Items(i).ToString Then
                        chkBoxTipoDesenho.Text = DadosArquivoCorrente.TipoDesenho
                        chkBoxTipoDesenho.SetItemChecked(i, True) ' Marca o item correspondente
                    Else
                        chkBoxTipoDesenho.SetItemChecked(i, False) ' Desmarca outros itens
                    End If
                Next

                Me.lblEspessura.Text = DadosArquivoCorrente.Espessura

                If Not String.IsNullOrEmpty(DadosArquivoCorrente.NumeroDobras) AndAlso DadosArquivoCorrente.NumeroDobras <> "0" Then
                    DadosArquivoCorrente.Dobra = "1"
                    chkDobra.Checked = True
                End If

                chkCorte.Checked = (DadosArquivoCorrente.Corte.ToString = "1")
                If chkCorte.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtCorte", "", "")
                End If


                chkDobra.Checked = (DadosArquivoCorrente.Dobra.ToString = "1")
                If chkDobra.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtDobra", "", "")
                End If

                chkSolda.Checked = (DadosArquivoCorrente.Solda.ToString = "1")
                If chkSolda.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSolda", "", "")
                End If

                chkPintura.Checked = (DadosArquivoCorrente.Pintura.ToString = "1")
                If chkPintura.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtPintura", "", "")
                End If

                chkMontagem.Checked = (DadosArquivoCorrente.Montagem.ToString = "1")
                If chkMontagem.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtMontagem", "", "")
                End If

            End If

        End If


    End Function

    ' Método auxiliar para limpar pendências
    Private Sub LimparPendencias()
        DadosArquivoCorrente.DescricaoPendencia = ""
        'txtDescricaoPendencia.Clear()
        btnPendencias.Image = My.Resources.verificado
        btnPendencias.Enabled = False
    End Sub


    ' Função para verificar se um valor é Nothing, vazio ou igual a "0"
    Private Function IsNullOrZero(ByVal valor As String) As Boolean
        Return valor Is Nothing OrElse String.IsNullOrEmpty(valor.Trim()) OrElse valor.Trim() = "0"
    End Function

    Public Sub GetSheetMetalProperties(ByVal SwModel As ModelDoc2)

        IntanciaSolidWorks.ConectarSolidWorks()
        ' swApparq = CreateObject("SldWorks.Application")

        SwModel = swapp.ActiveDoc

        Dim sheetThickness As Double = 0.0
        Dim blankLength As Double = 0.0
        Dim blankWidth As Double = 0.0
        Dim swFlatPattern As Feature = Nothing

        If SwModel IsNot Nothing Then
            ' Verifica se o documento ativo é uma peça
            If SwModel.GetType = swDocumentTypes_e.swDocPART Then
                Dim swPart As PartDoc = CType(SwModel, PartDoc)


                ' Itera através das features para obter a espessura da chapa
                Dim swFeature As Feature = swPart.FirstFeature()
                While swFeature IsNot Nothing

                    If swFeature.GetTypeName2() = "SheetMetal" Then
                        ' Obtém os dados da feature Sheet Metal
                        Dim swSheetMetalData As ISheetMetalFeatureData = CType(swFeature.GetDefinition(), ISheetMetalFeatureData)
                        sheetThickness = (swSheetMetalData.Thickness * 1000)

                    End If

                End While



                If DadosArquivoCorrente.Espessura = "" Then

                    DadosArquivoCorrente.Espessura = sheetThickness.ToString("F2")
                    Me.lblEspessura.Text = DadosArquivoCorrente.Espessura

                End If

            End If

        End If


    End Sub

    Private Sub btnLerDados_Click(sender As Object, e As EventArgs)

        Try


            DadosArquivoCorrente.ArquivoCorrente(swModel)

            DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

            'dados da caixa delimitadora
            DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

            AtualizaTela(swModel)


        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub btnSalvar_Click_1(sender As Object, e As EventArgs)

        Try




            DadosArquivoCorrente.AtualizaDesenho(swModel)

            'If swModel Is Nothing Then

            '    Exit Sub

            'End If

            swModel.GraphicsRedraw2()

            ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
            swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)


            'Verifica se o desenhop atualizado esta carregado na BOM se sim, atualiza os dados do desenho'
            If dgvDataGridBOM.Rows.Count > 0 Then


                For i As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                    If DadosArquivoCorrente.NomeArquivoSemExtensao.ToString.Trim = dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Value.ToString.Trim Then

                        ' Adicione os parâmetros ao comando
                        dgvDataGridBOM.Rows(i).Cells("DescResumo").Value = Me.cboTitulo.Text ' UCase(DadosArquivoCorrente.Titulo)
                        dgvDataGridBOM.Rows(i).Cells("DescDetal").Value = Me.txtAssuntoSubiTitulo.Text ' UCase(DadosArquivoCorrente.AssuntoSubiTitulo)
                        dgvDataGridBOM.Rows(i).Cells("Autor").Value = Me.txtAuthor.Text ' UCase(DadosArquivoCorrente.Author)
                        dgvDataGridBOM.Rows(i).Cells("Palavrachave").Value = Me.txtPalavraChave.Text ' UCase(DadosArquivoCorrente.PalavraChave)
                        dgvDataGridBOM.Rows(i).Cells("Notas").Value = Me.txtComentarios.Text ' UCase(DadosArquivoCorrente.Comentarios)
                        dgvDataGridBOM.Rows(i).Cells("Espessura").Value = Me.lblEspessura.Text '  UCase(DadosArquivoCorrente.Espessura)
                        dgvDataGridBOM.Rows(i).Cells("AreaPintura").Value = Me.lblAreaPintura.Text ' UCase(DadosArquivoCorrente.AreaPintura)
                        dgvDataGridBOM.Rows(i).Cells("NumeroDobras").Value = Me.lblNumeroDobra.Text ' UCase(DadosArquivoCorrente.NumeroDobras)
                        dgvDataGridBOM.Rows(i).Cells("Peso").Value = Me.lblPeso.Text  ' UCase(DadosArquivoCorrente.Massa)
                        'dgvDataGridBOM.Rows(i).Cells("Unidade").Value = "PC"
                        dgvDataGridBOM.Rows(i).Cells("Altura").Value = Me.lblComprimento.Text ' UCase(DadosArquivoCorrente.ComprimentoBlank)
                        dgvDataGridBOM.Rows(i).Cells("Largura").Value = Me.lblLargura.Text ' UCase(DadosArquivoCorrente.LarguraBlank)
                        'dgvDataGridBOM.Rows(i).Cells("Profundidade").Value = ""
                        dgvDataGridBOM.Rows(i).Cells("Material").Value = Me.lblMaterial.Text ' UCase(DadosArquivoCorrente.Material)
                        dgvDataGridBOM.Rows(i).Cells("Acabamento").Value = UCase(DadosArquivoCorrente.acabamento)
                        dgvDataGridBOM.Rows(i).Cells("txtSoldagem").Value = UCase(DadosArquivoCorrente.soldagem)
                        dgvDataGridBOM.Rows(i).Cells("txtTipoDesenho").Value = UCase(DadosArquivoCorrente.TipoDesenho)
                        dgvDataGridBOM.Rows(i).Cells("txtCorte").Value = UCase(DadosArquivoCorrente.Corte)
                        dgvDataGridBOM.Rows(i).Cells("txtDobra").Value = UCase(DadosArquivoCorrente.Dobra)
                        dgvDataGridBOM.Rows(i).Cells("txtSolda").Value = UCase(DadosArquivoCorrente.Solda)
                        dgvDataGridBOM.Rows(i).Cells("txtPintura").Value = UCase(DadosArquivoCorrente.Pintura)
                        dgvDataGridBOM.Rows(i).Cells("txtMontagem").Value = UCase(DadosArquivoCorrente.Montagem)
                        dgvDataGridBOM.Rows(i).Cells("Comprimentocaixadelimitadora").Value = Me.lblAlturaTotalCaixaDelimitadora.Text '  DadosArquivoCorrente.Alturacaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("Larguracaixadelimitadora").Value = Me.lblProfundidadeTotalCaixaDelimitadora.Text ' DadosArquivoCorrente.Larguracaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("Espessuracaixadelimitadora").Value = Me.lblProfundidadeTotalCaixaDelimitadora.Text ' DadosArquivoCorrente.Profundidadeaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("txtItemEstoque").Value = DadosArquivoCorrente.ItemEstoque

                        dgvDataGridBOM.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen


                        Exit Sub


                    End If




                Next

            End If



        Catch ex As Exception
        Finally
        End Try


    End Sub

    Public Function AtualisarItensOrdemServico()

        Dim ordemservicoitem As String

        ordemservicoitem = "UPDATE ordemservicoitem Set DescResumo = @DescResumo, DescDetal = @DescDetal, " &
                "Autor = @Autor, Palavrachave = @Palavrachave, Notas = @Notas, Espessura = @Espessura, AreaPintura = @AreaPintura, " &
                "NumeroDobras = @NumeroDobras, Peso = @Peso, Unidade = @Unidade, Altura = @Altura, Largura = @Largura, " &
                "MaterialSW = @MaterialSW, EnderecoArquivo = @EnderecoArquivo, Acabamento = @Acabamento, " &
                "txtSoldagem = @txtSoldagem, txtTipoDesenho = @txtTipoDesenho, txtCorte = @txtCorte, txtDobra = @txtDobra, " &
                "txtSolda = @txtSolda, txtPintura = @txtPintura, txtMontagem = @txtMontagem, Comprimentocaixadelimitadora = @Compcx, " &
                "Larguracaixadelimitadora = @Largcx, Espessuracaixadelimitadora = @Espcx, txtItemEstoque = @txtItemEstoque " &
                " WHERE Idordemservicoitem = @Idordemservicoitem"

        '  DGVTimerFiltroPecaAtivaOS.Refresh()

        DadosArquivoCorrente.ArquivoCorrente(swModel)

        DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

        'dados da caixa delimitadora
        DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)


        For i As Integer = 0 To DGVTimerFiltroPecaAtivaOS.Rows.Count - 1

            Using cmdidordemservicoitem As New MySqlCommand(ordemservicoitem, myconect)

                If Convert.ToBoolean(DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value) = True Then

                    Dim idordemservicoitem As String = DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("IdOrdemServicoItem").Value.ToString


                    ' Adicione os parâmetros ao comando
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Autor", UCase(DadosArquivoCorrente.Author))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Peso", UCase(DadosArquivoCorrente.Massa))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Unidade", "PC")
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Profundidade", String.Empty)
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@MaterialSW", UCase(DadosArquivoCorrente.Material))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Acabamento", UCase(DadosArquivoCorrente.acabamento))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
                    DadosArquivoCorrente.AddTextParameter(cmdidordemservicoitem, "@Idordemservicoitem", idordemservicoitem)

                    Try

                        ' Antes de executar o comando, exibir a SQL para verificação
                        Dim sqlComando As String = cmdidordemservicoitem.CommandText

                        ' Loop pelos parâmetros para exibir seus valores
                        For Each param As MySqlParameter In cmdidordemservicoitem.Parameters
                            sqlComando = sqlComando.Replace(param.ParameterName, param.Value.ToString())
                        Next

                        ' Exibir o comando SQL montado (usando Console ou MessageBox)
                        ' Console.WriteLine(sqlComando) ' Se for aplicação console
                        ' InputBox("", "", sqlComando) ' Se for aplicação WinForms

                        ' Executar o comando após depurar/verificar a SQL
                        Try
                            cmdidordemservicoitem.ExecuteNonQuery()
                        Catch ex As MySqlException
                            '     InputBox("", "", "Erro ao executar SQL: " & ex.Message)
                        End Try

                        cmdidordemservicoitem.ExecuteNonQuery()
                    Catch ex As MySqlException

                        '    InputBox("", "", ex.Message)
                        ' Exibe a mensagem de erro
                        MessageBox.Show("Erro ao executar o comando SQL: " & ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        ' Exibe outros erros gerais
                        MessageBox.Show("Erro inesperado: " & ex.Message, "Erro Geral", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value = False

                End If

            End Using
        Next

        DGVTimerFiltroPecaAtivaOS.Enabled = True

    End Function

    Public Function SalvarArquivoCorrente(ByVal swModel As ModelDoc2)

        Try


            DadosArquivoCorrente.ArquivoCorrente(swModel)
        Catch ex As Exception
        Finally
        End Try


        Try


            DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)
        Catch ex As Exception
        Finally
        End Try


        Try


            'dados da caixa delimitadora
            DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)
        Catch ex As Exception
        Finally
        End Try

        Try


            AtualizaTela(swModel)

        Catch ex As Exception
        Finally
        End Try

        Try



            DadosArquivoCorrente.AtualizaDesenho(swModel)

        Catch ex As Exception
        Finally
        End Try

    End Function

    Private Sub btndxf_Click(sender As Object, e As EventArgs)

        Try

            'Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                'Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                    IntanciaSolidWorks.ConectarSolidWorks()

                    swModel = swapp.ActiveDoc
                    'swModel = swApparq.ActiveDoc

                    swModel.Visible = True

                    swModelDocExt = swModel.Extension

                    Try

                        DadosArquivoCorrente.ExportDXF(swModel, True, True)

                        ' DadosArquivoCorrente.ExportSheetMetalBlankToDXF(swModel.GetPathName, Path.ChangeExtension(swModel.GetPathName, ".dxf"))

                        chkVerificarDXF.Checked = True

                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em dxf")

                    Finally

                    End Try
                End If
            End If
        Catch ex As Exception
        Finally
        End Try

        ''''''''''''''''' InserirTextoDXF(swModel, "", "")
        '''
    End Sub

    Sub InserirTextoDXF(swModel As Object, ByVal EnderecoIncial As String, ByVal EnderecoSaida As String)

        Try
            ' Caminho do arquivo DXF existente
            Dim inputFilePath As String = swModel.GetPathName
            If String.IsNullOrEmpty(inputFilePath) OrElse Not File.Exists(inputFilePath) Then
                Console.WriteLine("O caminho do arquivo DXF é inválido ou o arquivo não existe.")
                Exit Sub
            End If

            ' Caminho para salvar o arquivo atualizado
            Dim outputFilePath As String = Path.ChangeExtension(inputFilePath, ".dxf")

            ' Carregar o arquivo DXF
            Dim dxf As DxfDocument = DxfDocument.Load(outputFilePath)

            ' Criar ou buscar o layer "MeuTexto"
            Dim layerName As String = "MeuTexto"
            Dim meuLayer As netDxf.Tables.Layer
            If Not dxf.Layers.Contains(layerName) Then
                meuLayer = New netDxf.Tables.Layer(layerName)
                dxf.Layers.Add(meuLayer)
            Else
                meuLayer = dxf.Layers(layerName)
            End If

            ' Criar um novo texto
            Dim texto As New netDxf.Entities.Text(Path.GetFileNameWithoutExtension(inputFilePath),
                              New netDxf.Vector2(10, 1), ' Coordenadas X e Y
            5) ' Altura do texto
            texto.Layer = meuLayer ' Adicionar o texto ao layer específico
            texto.Color = AciColor.Blue  ' Opcional: Cor do texto

            ' Adicionar o texto ao desenho
            dxf.Entities.Add(texto)

            ' Salvar o arquivo modificado
            dxf.Save(outputFilePath)

            MsgBox("Texto inserido com sucesso no arquivo:  " & outputFilePath)
        Catch ex As Exception
            MsgBox("Erro: " & ex.Message)
        End Try
    End Sub

    Private Sub btnListaMaterial_Click(sender As Object, e As EventArgs) Handles btnListaMaterial.Click

        Try

            'If cl_BancoDados.AbrirBanco = False Then

            '    cl_BancoDados.AbrirBanco()

            'End If


            TabelaViewMontaPeca = cl_BancoDados.CarregarDados("SELECT * FROM viewmontapeca where D_E_L_E_T_E <> '' OR D_E_L_E_T_E IS NULL")

            ' Conectar ao SolidWorks
            IntanciaSolidWorks.ConectarSolidWorks()

            ' Obter o documento ativo
            swModel = swapp.ActiveDoc


            ' Verificar se o documento foi aberto
            If swModel Is Nothing Then
                MsgBox("Nenhum documento ativo no SolidWorks.", vbCritical, "Erro")
                Exit Sub
            End If

            ' Definir modelo como visível
            'swModel.Visible = True
            swModelDocExt = swModel.Extension

            ' Verifica o tipo de documento (Desenho)
            If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                ' Obter o caminho completo do arquivo
                DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(swModel.GetPathName().ToUpper())

                ' Se a opção de converter para PDF estiver marcada
                If chkConverterPDF.Checked Then
                    ' Verificar se o arquivo existe antes de tentar converter
                    If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                        DadosArquivoCorrente.ExportToPDF(swModel, DadosArquivoCorrente.EnderecoArquivo, True)
                        iconePDF = My.Resources.ficheiro_pdf
                    Else
                        MsgBox("Arquivo não encontrado: " & DadosArquivoCorrente.EnderecoArquivo, vbCritical, "Erro")
                        Exit Sub
                    End If
                End If

                ' Alterar a extensão para SLDASM
                DadosArquivoCorrente.EnderecoArquivo = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, "SLDASM")

                ' Verificar se o arquivo SLDASM existe
                If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                    OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)
                Else
                    MsgBox("O desenho de detalhamento não pertence a um desenho de conjunto!", vbCritical, "Atenção")
                    Exit Sub
                End If

            End If

            ' Processar PART ou ASSEMBLY
            If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then



                ''retirado 03/12/2024 edson  DadosArquivoCorrente.AtualizaDesenho(swModel)


                FormatarColunaIconeDGVListaBom()

                ' Preencher o DataGridView com os dados da peça
                dgvDataGridBOM.Rows.Add(iconeDXF,
                                        iconePDF,
                                        iconeTipoArquivo,
                                    iconeAtencao,
                                    DadosArquivoCorrente.IdMaterial,
                                    DadosArquivoCorrente.NomeArquivoSemExtensao,
                                    DadosArquivoCorrente.Titulo,
                                    DadosArquivoCorrente.AssuntoSubiTitulo,
                                    DadosArquivoCorrente.Author,
                                    DadosArquivoCorrente.PalavraChave,
                                    DadosArquivoCorrente.Comentarios,
                                    DadosArquivoCorrente.Espessura,
                                    DadosArquivoCorrente.ComprimentoBlank,
                                    DadosArquivoCorrente.LarguraBlank,
                                    DadosArquivoCorrente.Material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.acabamento,
                                    DadosArquivoCorrente.soldagem,
                                    DadosArquivoCorrente.TipoDesenho,
                                    DadosArquivoCorrente.Corte,
                                    DadosArquivoCorrente.Dobra,
                                    DadosArquivoCorrente.Solda,
                                    DadosArquivoCorrente.Pintura,
                                    DadosArquivoCorrente.Montagem,
                                    DadosArquivoCorrente.rnc,
                                    DadosArquivoCorrente.Alturacaixadelimitadora,
                                    DadosArquivoCorrente.Larguracaixadelimitadora,
                                    DadosArquivoCorrente.Profundidadeaixadelimitadora,
                                    DadosArquivoCorrente.ItemEstoque,
                                    1)

                Try


                    If DadosArquivoCorrente.rnc.ToString <> "" Then

                        dgvDataGridBOM.Rows(dgvDataGridBOM.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightPink

                    End If

                Catch ex As Exception
                Finally
                End Try

                ' Ler dados da view de montagem
                LerDadosViewMontaPeca()

                Try


                    ' Fechar o documento
                    swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                    cl_BancoDados.FecharArquivoMemoria()
                    IntanciaSolidWorks.LiberarRecurso(swModel)

                Catch ex As Exception

                Finally

                End Try

                ' Processar a lista de material
                ProcessarListaMaterial(swModel)

            End If
        Catch ex As Exception
            '  MsgBox("Erro: " & ex.Message, vbCritical, "Erro")
        Finally
        End Try

    End Sub

    ' Sub para processar a lista de material
    Private Sub ProcessarListaMaterial(swModel As Object)
        Try
            Dim swFeat As Object = swModel.FirstFeature

            ' Iterar sobre as características do modelo
            Do While Not swFeat Is Nothing

                Try


                    If "BomFeat" = swFeat.GetTypeName Then
                        Dim resultado As DialogResult = MessageBox.Show(
                    "Gostaria de Exportar os dados da lista de material? " & swFeat.Name, "Lista de peças",
                    MessageBoxButtons.YesNo)

                        If resultado = DialogResult.Yes Then
                            Dim swBomFeat As Object = swFeat.GetSpecificFeature2
                            ProcessBomFeatureComformTabela(swModel, swBomFeat)
                        End If
                    End If
                    swFeat = swFeat.GetNextFeature()
                Catch ex As Exception
                    ' MsgBox("Erro ao ler a lista de dados da lista de material", MsgBoxStyle.Critical, "Atenção")
                    Continue Do

                End Try

            Loop
        Catch ex As Exception


        Finally
            ' MsgBox("Erro ao processar a lista de material: " & ex.Message)
        End Try
    End Sub


    Public Function LerDadosViewMontaPeca()

        Try

            For i As Integer = 0 To TabelaViewMontaPeca.Rows.Count - 1

                If Trim(TabelaViewMontaPeca.Rows(i)("CodMatFabricante").ToString) = DadosArquivoCorrente.NomeArquivoSemExtensao Then

                    iconeAtencao = My.Resources.verificado1  ' Substitua pelo seu ícone

                    iconeTipoArquivo = My.Resources.material_escolar_32

                    dgvDataGridBOM.Rows.Add(iconeTipoArquivo, iconeAtencao,
                                            Trim(TabelaViewMontaPeca.Rows(i).Item("IdMaterial").ToString.ToUpper), 'IdMaterial,
                                                Trim(TabelaViewMontaPeca.Rows(i).Item("CodMatFabricante").ToString.ToUpper), 'CodMatFabricante,
                                                "",'DescResumo
                                                Trim(TabelaViewMontaPeca.Rows(i).Item("DescDetal").ToString.ToUpper),'DescDetal,
                                    "",'Autor,
                                    "",'Palavrachave,
                                    "",'Notas,
                                    "",'Espessura,
                                    "",'ComprimentoBlank,
                                    "",'LarguraBlank,
                                    "",'Material,
                                    "",'AreaPintura,
                                    "",'NumeroDobras,
                                    Trim(TabelaViewMontaPeca.Rows(i).Item("Peso").ToString) * qtdePecaLm,'Peso,
                                    "",'EnderecoArquivo,
                                    "",'acabamento,
                                    "",'soldagem,
                                    "MATERIAL",'TipoDesenho,
                                    "",'Corte,
                                    "",'Dobra,
                                    "",'Solda,
                                    "",'Pintura,
                                    "",'Montagem,
                                    "",'rnc,
                                    "",'Alturacaixadelimitadora,
                                    "",'Larguracaixadelimitadora,
                                    "",'Profundidadeaixadelimitadora,
                                    "",'ItemEstoque,
                                    Trim(TabelaViewMontaPeca.Rows(i).Item("PecaQtde").ToString * qtdePecaLm)) 'Qtde,

                End If

            Next
        Catch ex As Exception
        Finally
        End Try

    End Function

    Private Sub FormatarColunaIconeDGVListaBom()

        ' Configuração dos ícones padrão para maior clareza
        iconeTipoArquivo = My.Resources.Sem_Incone
        iconeAtencao = My.Resources.verificado1
        iconePDF = My.Resources.Sem_Incone
        iconeDXF = My.Resources.Sem_Incone

        ' Validar EndereçoArquivoCorrente
        Dim enderecoArquivo As String = DadosArquivoCorrente?.EnderecoArquivo
        If String.IsNullOrEmpty(enderecoArquivo) Then
            Exit Sub ' Sai se o endereço for inválido
        End If




        ' Verificar "rnc" e definir o ícone correspondente
        If DadosArquivoCorrente?.rnc?.ToUpperInvariant() = "S" Then
            iconeAtencao = My.Resources.atencao
        End If

        Dim VerificarArquivo As Func(Of String, String, Boolean) =
    Function(caminho, novaExtensao)
        If String.IsNullOrEmpty(caminho) Then
            '  MsgBox ("O caminho do arquivo está vazio ou é nulo.")
            Return False
        End If

        Dim novoCaminho = Path.ChangeExtension(caminho, novaExtensao)
        ' MsgBox($"Verificando arquivo: {novoCaminho}")

        Return Not String.IsNullOrEmpty(novoCaminho) AndAlso File.Exists(novoCaminho)
    End Function

        ' Verificar existência de arquivos PDF e DXF
        If VerificarArquivo(enderecoArquivo, ".pdf") Then
            iconePDF = My.Resources.ficheiro_pdf
        End If
        If VerificarArquivo(enderecoArquivo, ".dxf") Then
            iconeDXF = My.Resources.arquivo_dxf
        End If


        ' Definir o tipo de ícone com base na extensão
        Dim extensao As String = Path.GetExtension(enderecoArquivo)?.ToUpperInvariant()
        Select Case extensao
            Case ".SLDASM"
                iconeTipoArquivo = My.Resources.IcopneMontagemSW
            Case ".SLDPRT"
                iconeTipoArquivo = My.Resources.IcopneMontagemPRT
        End Select

    End Sub



    Sub ProcessBomFeatureComformTabela(ByVal swModel As ModelDoc2, ByVal swBomFeat As BomFeature)
        Try

            Dim swFeat As Feature
            Dim vTableArr As Object
            Dim vTable As Object
            Dim vConfigArray As Object
            Dim vConfig As Object
            Dim ConfigName As String
            Dim swTable As TableAnnotation

            swFeat = swBomFeat.GetFeature
            vTableArr = swBomFeat.GetTableAnnotations

            For Each vTable In vTableArr
                swTable = vTable
                vConfigArray = swBomFeat.GetConfigurations(True, True)
                For Each vConfig In vConfigArray

                    Try


                        ConfigName = vConfig

                        ProcessTableAnnComformTabela(swModel, swTable, ConfigName)

                    Catch ex As Exception

                        MsgBox("Erro ao Ler item da Lista BOM", MsgBoxStyle.Critical, "Atenção")
                        Continue For

                    End Try

                Next vConfig

            Next vTable
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Sub ReadBOMFields(ByVal swModel As ModelDoc2, ByVal swTableAnn As TableAnnotation)
        Dim swBOMTable As BomTableAnnotation
        swBOMTable = swTableAnn

        If swBOMTable Is Nothing Then
            MsgBox("A tabela BOM não foi encontrada.")
            Exit Sub
        End If

        Dim nNumRows As Long
        nNumRows = swBOMTable.RowCount

        Dim nNumCols As Long
        nNumCols = swBOMTable.ColumnCount

        Dim columnNames As New List(Of String)

        ' Loop para obter os nomes das colunas
        For col As Integer = 0 To nNumCols - 1
            Dim colName As String
            colName = swBOMTable.GetColumnTitle(col)
            columnNames.Add(colName)
        Next

        ' Exibir os nomes das colunas
        MsgBox("Nomes das colunas na tabela BOM:" & vbCrLf & String.Join(vbCrLf, columnNames))

        ' Loop para obter os dados de cada célula
        For row As Integer = 1 To nNumRows - 1 ' Começa em 1 para ignorar o cabeçalho
            For col As Integer = 0 To nNumCols - 1
                Dim cellValue As String
                cellValue = swBOMTable.GetCellValue(row, col)
                ' Aqui você pode fazer o que quiser com cellValue, como armazenar ou exibir
            Next
        Next

        MsgBox("Leitura concluída!")
    End Sub

    Sub ProcessTableAnnComformTabela(ByVal swModel As ModelDoc2, ByVal swTableAnn As TableAnnotation, ByVal ConfigName As String)

        Dim nNumRow As Long
        Dim J As Long

        Dim ItemNumber As String = Nothing
        Dim PartNumber As String = Nothing
        Dim vPtArr As Object
        Dim swComp As Object
        Dim pt As Object
        nNumRow = swTableAnn.RowCount

        Dim swBOMTableAnn As BomTableAnnotation
        swBOMTableAnn = swTableAnn

        Dim processedCount As Integer

        ' Criar uma nova tabela temporária
        Dim tempTable As New System.Data.DataTable

        tempTable.Columns.Add("qtdePeca", GetType(Double))
        tempTable.Columns.Add("EnderecoArquivo", GetType(String))

        Dim Encontrado As Boolean = False

        ProgressBarListaSW.Maximum = nNumRow - 1

        For J = 1 To nNumRow - 1

            swBOMTableAnn.GetComponentsCount2(J, ConfigName, ItemNumber, PartNumber)
            vPtArr = swBOMTableAnn.GetComponents2(J, ConfigName)

            If (Not vPtArr Is Nothing) Then

                For i = 0 To UBound(vPtArr)

                    pt = vPtArr(i)

                    swComp = pt

                    If Not swComp Is Nothing Then

                        DadosArquivoCorrente.EnderecoArquivo = swComp.GetPathName.ToString.ToUpper


                    End If

                Next
                qtdePecaLm = swBOMTableAnn.GetComponentsCount2(J, ConfigName, ItemNumber, PartNumber)

            End If

            For i As Integer = 0 To tempTable.Rows.Count - 1

                If tempTable.Rows(i)("EnderecoArquivo").ToString() = DadosArquivoCorrente.EnderecoArquivo.ToUpper.Trim Then

                    tempTable.Rows(i)("qtdePeca") = tempTable.Rows(i)("qtdePeca") + qtdePecaLm

                    Encontrado = True
                    Exit For

                End If

            Next
            If Encontrado = False Then

                tempTable.Rows.Add(qtdePecaLm, DadosArquivoCorrente.EnderecoArquivo)

            End If

            Encontrado = False

            ProgressBarListaSW.Value = J


        Next J

        ProgressBarListaSW.Value = 0
        ProgressBarListaSW.Maximum = tempTable.Rows.Count

        For a As Integer = 0 To tempTable.Rows.Count - 1


            Try


                If tempTable.Rows(a)("EnderecoArquivo").ToString().ToLower().EndsWith(".sldprt") OrElse
               tempTable.Rows(a)("EnderecoArquivo").ToString().ToLower().EndsWith(".sldasm") Then


                    DadosArquivoCorrente.EnderecoArquivo = tempTable.Rows(a)("EnderecoArquivo").ToString()
                    OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, True, swModel)


                    If chkConverterDXF.Checked = True Then

                        '  OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, True, swModel)

                        DadosArquivoCorrente.ExportDXF(swModel, True, True)

                        iconeDXF = My.Resources.arquivo_dxf

                        DadosArquivoCorrente.EnderecoArquivo = tempTable.Rows(a)("EnderecoArquivo").ToString

                    End If

                    System.Threading.Thread.Sleep(200) ' Delay de 100 milissegundos

                    Dim pdf As String


                    If chkConverterPDF.Checked = True Then

                        pdf = DadosArquivoCorrente.EnderecoArquivo.ToUpper.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")

                        If File.Exists(pdf) Then

                            OpenDocumentAndWait(pdf, True, swModel)

                            DadosArquivoCorrente.ExportToPDF(swModel, pdf, False)

                            System.Threading.Thread.Sleep(100) ' Delay de 100 milissegundos

                            iconePDF = My.Resources.ficheiro_pdf

                        End If

                    End If



                    FormatarColunaIconeDGVListaBom()


                    dgvDataGridBOM.Rows.Add(iconeDXF,
                                        iconePDF,
                                        iconeTipoArquivo,
                                                iconeAtencao,
                                                "",'DadosArquivoCorrente.IdMaterial,
                                                DadosArquivoCorrente.NomeArquivoSemExtensao,
                                                DadosArquivoCorrente.Titulo,
                                                DadosArquivoCorrente.AssuntoSubiTitulo,
                                                DadosArquivoCorrente.Author,
                                                DadosArquivoCorrente.PalavraChave,
                                                DadosArquivoCorrente.Comentarios,
                                                DadosArquivoCorrente.Espessura,
                                                DadosArquivoCorrente.ComprimentoBlank,
                                                DadosArquivoCorrente.LarguraBlank,
                                                DadosArquivoCorrente.Material,
                                                DadosArquivoCorrente.AreaPintura,
                                                DadosArquivoCorrente.NumeroDobras,
                                                DadosArquivoCorrente.Massa,
                                                DadosArquivoCorrente.EnderecoArquivo,
                                                DadosArquivoCorrente.acabamento,
                                                DadosArquivoCorrente.soldagem,
                                                DadosArquivoCorrente.TipoDesenho,
                                                DadosArquivoCorrente.Corte,
                                                DadosArquivoCorrente.Dobra,
                                                DadosArquivoCorrente.Solda,
                                                DadosArquivoCorrente.Pintura,
                                                DadosArquivoCorrente.Montagem,
                                                DadosArquivoCorrente.rnc,
                                                DadosArquivoCorrente.Alturacaixadelimitadora,
                                                DadosArquivoCorrente.Larguracaixadelimitadora,
                                                DadosArquivoCorrente.Profundidadeaixadelimitadora,
                                                DadosArquivoCorrente.ItemEstoque,
                                                tempTable.Rows(a)("qtdePeca"))
                    DadosArquivoCorrente.Qtde = tempTable.Rows(a)("qtdePeca")


                    ' Condição 1: RNC é "S"
                    If DadosArquivoCorrente.rnc.ToString = "S" Then
                        AlterarEstiloDataGrid(a, dgvDataGridBOM, rowColor:=Color.LightPink)
                    End If

                    ' Condição 2: Arquivo é uma peça, TipoDesenho está vazio, e Liberação é "NÃO"
                    If swModel.GetType() = swDocumentTypes_e.swDocPART AndAlso
                   String.IsNullOrEmpty(DadosArquivoCorrente.TipoDesenho.ToString) AndAlso
                   My.Settings.LiberaOSsemMaterial = "NÃO" Then

                        dgvDataGridBOM.Rows(a).Cells("RNC").Value = "S"
                        AlterarEstiloDataGrid(a, dgvDataGridBOM, "txtTipoDesenho", Color.Red, Color.LightPink)
                    End If

                    ' Condição 3: Arquivo é uma peça, Material está vazio, Número de Dobras não está vazio, e Liberação é "NÃO"
                    If swModel.GetType() = swDocumentTypes_e.swDocPART AndAlso
                   String.IsNullOrEmpty(DadosArquivoCorrente.Material.ToString) AndAlso
                   Not String.IsNullOrEmpty(DadosArquivoCorrente.NumeroDobras.ToString) AndAlso
                   My.Settings.LiberaOSsemMaterial = "NÃO" Then

                        dgvDataGridBOM.Rows(a).Cells("RNC").Value = "S"
                        AlterarEstiloDataGrid(a, dgvDataGridBOM, "materialsw", Color.Red, Color.LightPink)
                    End If

                    ' Condição 4: Arquivo é uma peça, Espessura não está vazia, Material está vazio,
                    ' Tipo de Desenho está vazio e Liberação é "NÃO".
                    If swModel.GetType() = swDocumentTypes_e.swDocPART AndAlso
                   Not String.IsNullOrEmpty(DadosArquivoCorrente.Espessura) AndAlso
                   My.Settings.LiberaOSsemMaterial = "NÃO" AndAlso
                   String.IsNullOrEmpty(DadosArquivoCorrente.Material) AndAlso
                   String.IsNullOrEmpty(DadosArquivoCorrente.TipoDesenho) Then

                        ' Define o valor da célula "RNC" como "S"
                        dgvDataGridBOM.Rows(a).Cells("RNC").Value = "S"

                        ' Altera o estilo da célula e da linha no DataGridView
                        AlterarEstiloDataGrid(a, dgvDataGridBOM, "txtTipoDesenho", Color.Red, Color.LightPink)
                    End If



                    LerDadosViewMontaPeca()

                    ' Fecha o documento se necessário
                    'If ManterAberto = False Then
                    swapp.CloseDoc(swModel.GetPathName)
                    '  End If


                End If

                swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                cl_BancoDados.FecharArquivoMemoria()
                IntanciaSolidWorks.LiberarRecurso(swModel)
                IntanciaSolidWorks.LiberarRecurso(swPart)


                ProgressBarListaSW.Value = a

                processedCount = processedCount + 1

                ' Verifica se chegou ao tamanho do lote
                If processedCount >= 10 Then
                    ' Reiniciar o SolidWorks
                    RestartSolidWorks()
                    processedCount = 0 ' Reiniciar contador

                End If

                ' Adicionar um pequeno delay para liberar recursos
                System.Threading.Thread.Sleep(300) ' Delay de 100 milissegundos

            Catch ex As Exception

                ' Fechar o documento
                swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                cl_BancoDados.FecharArquivoMemoria()
                IntanciaSolidWorks.LiberarRecurso(swModel)

                '  MsgBox(ex.Message & "Erro na listura de Material")

                Continue For

            End Try

        Next


        '   Using transaction = myconect.BeginTransaction()

        ' Try
        For aa As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

            Try


                'Cria as Colunas para ler a lista de material
                ' DadosArquivoCorrente.IdMaterial = dgvDataGridBOM.Rows(aa).Cells("IdMaterial").Value
                DadosArquivoCorrente.NomeArquivoSemExtensao = dgvDataGridBOM.Rows(aa).Cells("CodMatFabricante").Value
                DadosArquivoCorrente.TipoDesenho = dgvDataGridBOM.Rows(aa).Cells("DescResumo").Value
                DadosArquivoCorrente.AssuntoSubiTitulo = dgvDataGridBOM.Rows(aa).Cells("DescDetal").Value
                DadosArquivoCorrente.Author = dgvDataGridBOM.Rows(aa).Cells("Autor").Value
                DadosArquivoCorrente.PalavraChave = dgvDataGridBOM.Rows(aa).Cells("PalavraChave").Value
                DadosArquivoCorrente.Comentarios = dgvDataGridBOM.Rows(aa).Cells("Notas").Value
                DadosArquivoCorrente.Espessura = dgvDataGridBOM.Rows(aa).Cells("Espessura").Value
                DadosArquivoCorrente.ComprimentoBlank = dgvDataGridBOM.Rows(aa).Cells("Altura").Value
                DadosArquivoCorrente.LarguraBlank = dgvDataGridBOM.Rows(aa).Cells("Largura").Value
                DadosArquivoCorrente.Material = dgvDataGridBOM.Rows(aa).Cells("Material").Value
                DadosArquivoCorrente.AreaPintura = dgvDataGridBOM.Rows(aa).Cells("AreaPintura").Value
                DadosArquivoCorrente.NumeroDobras = dgvDataGridBOM.Rows(aa).Cells("NumeroDobras").Value
                DadosArquivoCorrente.Massa = dgvDataGridBOM.Rows(aa).Cells("Peso").Value
                DadosArquivoCorrente.EnderecoArquivo = dgvDataGridBOM.Rows(aa).Cells("EnderecoArquivo").Value
                DadosArquivoCorrente.acabamento = dgvDataGridBOM.Rows(aa).Cells("acabamento").Value
                DadosArquivoCorrente.soldagem = dgvDataGridBOM.Rows(aa).Cells("txtSoldagem").Value
                DadosArquivoCorrente.TipoDesenho = dgvDataGridBOM.Rows(aa).Cells("txtTipoDesenho").Value
                DadosArquivoCorrente.Corte = dgvDataGridBOM.Rows(aa).Cells("txtCorte").Value
                DadosArquivoCorrente.Dobra = dgvDataGridBOM.Rows(aa).Cells("txtDobra").Value
                DadosArquivoCorrente.Solda = dgvDataGridBOM.Rows(aa).Cells("txtSolda").Value
                DadosArquivoCorrente.Pintura = dgvDataGridBOM.Rows(aa).Cells("txtPintura").Value
                DadosArquivoCorrente.Montagem = dgvDataGridBOM.Rows(aa).Cells("txtMontagem").Value
                DadosArquivoCorrente.rnc = dgvDataGridBOM.Rows(aa).Cells("rnc").Value
                DadosArquivoCorrente.Alturacaixadelimitadora = dgvDataGridBOM.Rows(aa).Cells("Comprimentocaixadelimitadora").Value
                DadosArquivoCorrente.Larguracaixadelimitadora = dgvDataGridBOM.Rows(aa).Cells("Larguracaixadelimitadora").Value
                DadosArquivoCorrente.Profundidadeaixadelimitadora = dgvDataGridBOM.Rows(aa).Cells("Espessuracaixadelimitadora").Value
                DadosArquivoCorrente.ItemEstoque = dgvDataGridBOM.Rows(aa).Cells("txtItemEstoque").Value
                DadosArquivoCorrente.Qtde = dgvDataGridBOM.Rows(aa).Cells("Qtde").Value



                ' Verifica se o registro já existe
                Dim checkCommand As New MySqlCommand("SELECT COUNT(CodMatFabricante) as CodMatFabricante FROM material WHERE PecaManuFat = 'S' AND CodMatFabricante = @valor1", myconect)
                checkCommand.Parameters.AddWithValue("@valor1", DadosArquivoCorrente.NomeArquivoSemExtensao)

                Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())

                If count > 0 Then
                    ' Se existir, faz um UPDATE
                    Dim updateCommand As New MySqlCommand("UPDATE material SET DescResumo = @DescResumo, DescDetal = @DescDetal, PecaManufat = @PecaManufat, " &
            "Autor = @Autor, Palavrachave = @Palavrachave, Notas = @Notas, Espessura = @Espessura, AreaPintura = @AreaPintura, " &
            "NumeroDobras = @NumeroDobras, Peso = @Peso, Unidade = @Unidade, Altura = @Altura, Largura = @Largura, " &
            "Profundidade = @Profundidade, UsuarioAlteracao = @UsuarioAlteracao, DtAlteracao = @DtCad, CodigoJuridicoMat = @CodigoJuridicoMat, " &
            "StatusMat = @StatusMat, MaterialSW = @MaterialSW, EnderecoArquivo = @EnderecoArquivo, Acabamento = @Acabamento, " &
            "txtSoldagem = @txtSoldagem, txtTipoDesenho = @txtTipoDesenho, txtCorte = @txtCorte, txtDobra = @txtDobra, " &
            "txtSolda = @txtSolda, txtPintura = @txtPintura, txtMontagem = @txtMontagem, Comprimentocaixadelimitadora = @Compcx, " &
            "Larguracaixadelimitadora = @Largcx, Espessuracaixadelimitadora = @Espcx, txtItemEstoque = @txtItemEstoque " &
            "WHERE CodMatFabricante = @CodMatFabricante", myconect)

                    '    na linha 1387 ,1388 ,1391 tem um parentese so por exemplo       edinho

                    ' Adicione os parâmetros ao comando
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@PecaManufat", "S")
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Autor", UCase(DadosArquivoCorrente.Author))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Peso", UCase(DadosArquivoCorrente.Massa))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Unidade", "PC")
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Profundidade", String.Empty)
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@DtCad", DateTime.Now.Date) ' Formato ISO
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@CodigoJuridicoMat", String.Empty)
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@StatusMat", "A")
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@MaterialSW", UCase(DadosArquivoCorrente.Material))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Acabamento", UCase(DadosArquivoCorrente.acabamento))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(updateCommand, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)

                    'If cl_BancoDados.AbrirBanco = False Then

                    '    cl_BancoDados.AbrirBanco()

                    'End If
                    updateCommand.ExecuteNonQuery()

                Else
                    ' Se não existir, faz um INSERT
                    Dim insertCommand As New MySqlCommand("INSERT INTO material (DescResumo, DescDetal, PecaManufat, Autor, Palavrachave, Notas, Espessura, AreaPintura, NumeroDobras, Peso, " &
            "Unidade, Altura, Largura, Profundidade, CodMatFabricante, DtCad, UsuarioCriacao, UsuarioAlteracao, DtAlteracao, CodigoJuridicoMat, " &
            "StatusMat, MaterialSW, EnderecoArquivo, Acabamento, txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, txtPintura, " &
            "txtMontagem, Comprimentocaixadelimitadora, Larguracaixadelimitadora, Espessuracaixadelimitadora, txtItemEstoque) " &
            "VALUES (@DescResumo, @DescDetal, @PecaManufat, @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, @NumeroDobras, @Peso, @Unidade, " &
            "@Altura, @Largura, @Profundidade, @CodMatFabricante, @DtCad, @UsuarioCriacao, @UsuarioAlteracao, @DtAlteracao, @CodigoJuridicoMat, " &
            "@StatusMat, @MaterialSW, @EnderecoArquivo, @Acabamento, @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, @txtPintura, " &
            "@txtMontagem, @Compcx, @Largcx, @Espcx, @txtItemEstoque)", myconect)
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@PecaManufat", "S")
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Autor", UCase(DadosArquivoCorrente.Author))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Peso", UCase(DadosArquivoCorrente.Massa))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Unidade", "PC")
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Profundidade", String.Empty)
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@DtCad", DateTime.Now.Date) ' Formato ISO
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@CodigoJuridicoMat", String.Empty)
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@StatusMat", "A")
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@MaterialSW", UCase(DadosArquivoCorrente.Material))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Acabamento", UCase(DadosArquivoCorrente.acabamento))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                    DadosArquivoCorrente.AddTextParameter(insertCommand, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)

                    'If cl_BancoDados.AbrirBanco = False Then

                    '    cl_BancoDados.AbrirBanco()

                    'End If

                    insertCommand.ExecuteNonQuery()

                End If

                ''''''''''  Carregar dados do banco de dados
                '''''''''dt = cl_BancoDados.CarregarDados("SELECT IdMaterial, CodMatFabricante, RNC FROM material where  pecamanufat  = 'S' AND d_e_l_e_t_e is null or d_e_l_e_t_e = '' and CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao.Trim & "'")
                '''''''''' Verificar se o item existe
                '''''''''For iIdMaterial As Integer = 0 To dt.Rows.Count - 1

                '''''''''    If dt.Rows(iIdMaterial).Item("CodMatFabricante").ToString().Trim = DadosArquivoCorrente.NomeArquivoSemExtensao.ToString().Trim Then

                '''''''''        DadosArquivoCorrente.IdMaterial = dt.Rows(iIdMaterial).Item("IdMaterial").ToString.Trim
                '''''''''        DadosArquivoCorrente.rnc = dt.Rows(iIdMaterial).Item("RNC").ToString.ToUpper

                '''''''''        dgvDataGridBOM.Rows(aa).Cells("IdMaterial").Value = DadosArquivoCorrente.IdMaterial
                '''''''''        Exit For

                '''''''''    End If

                '''''''''Next

            Catch ex As Exception

                '  MsgBox(ex.Message & " ERRO NO ARQUIVO: " & UCase(DadosArquivoCorrente.EnderecoArquivo))
                Continue For
            End Try

        Next

        MsgBox("Lista de Material Processada com Sucesso!", vbInformation, "Atenção")
        ProgressBarListaSW.Value = 0
        'DisconnectSolidWorks()


        If dgvDataGridBOM.Rows.Count > 0 Then


            For a As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                Try

                    If a <> 1 Then 'Não fechao o primeiro arquivo do datagrid view da BOM

                        If dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").ToString().ToLower().EndsWith(".sldprt") OrElse
                  dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").ToString().ToLower().EndsWith(".sldasm") Then

                            swapp.CloseDoc(dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").ToString())


                        End If

                    End If


                Catch ex As Exception

                    Continue For

                End Try


            Next

        End If

        chkConverterPDF.Checked = False
        chkConverterDXF.Checked = False

        tempTable.Dispose()
        tempTable = Nothing

    End Sub

    Private Sub AlterarEstiloDataGrid(rowIndex As Integer, dgv As DataGridView, Optional cellName As String = "", Optional cellColor As Color = Nothing, Optional rowColor As Color = Nothing)
        ' Verifica se foi definido um valor para rowColor e aplica à linha inteira
        If rowColor <> Nothing Then
            dgv.Rows(rowIndex).DefaultCellStyle.BackColor = rowColor
        End If

        ' Verifica se foi definido um valor para cellColor e aplica à célula específica, se cellName for fornecido
        If Not String.IsNullOrEmpty(cellName) AndAlso cellColor <> Nothing Then
            dgv.Rows(rowIndex).Cells(cellName).Style.BackColor = cellColor
        End If
    End Sub



    Private Sub RestartSolidWorks()

        System.Threading.Thread.Sleep(100) ' Delay de 100 milissegundos

        Try
            swapp.ExitApp()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(swapp)
            swapp = Nothing

            ' Criar uma nova instância do SolidWorks
            swapp = New SldWorks()
        Catch ex As Exception
            ' Console.WriteLine($"Erro ao reiniciar o SolidWorks: {ex.Message}")
        Finally

        End Try
    End Sub

    Private Sub DisconnectSolidWorks()
        If swapp IsNot Nothing Then
            swapp.ExitApp()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(swapp)
            swapp = Nothing
        End If
        GC.Collect()
    End Sub


    'Sub OpenDocumentAndWait(filePath As String, ByVal visualizarDesenho As Boolean, ByRef swModel As ModelDoc2)

    '    Try
    '        ' Conecta ao SolidWorks
    '        IntanciaSolidWorks.ConectarSolidWorks()

    '        ' Obtém o tipo de documento baseado no arquivo
    '        Dim docType As swDocumentTypes_e = GetDocumentType(filePath)

    '        ' Define opções para abrir o documento
    '        Dim openOptions As Integer = swOpenDocOptions_e.swOpenDocOptions_Silent Or
    '                                 swOpenDocOptions_e.swOpenDocOptions_ReadOnly Or
    '                                 swOpenDocOptions_e.swOpenDocOptions_LoadModel

    '        ' Variáveis para erros e avisos
    '        Dim errors As Integer = 0
    '        Dim warnings As Integer = 0

    '        ' Abre o documento
    '        swModel = swapp.OpenDoc6(filePath, docType, openOptions, "", errors, warnings)

    '        ' Verifica se houve falha ao abrir
    '        If swModel Is Nothing OrElse errors <> 0 Then
    '            Throw New Exception($"Falha ao abrir o documento. Código de erro: {errors}")
    '        End If

    '        ' Ativa o documento
    '        swModel = swapp.ActivateDoc(filePath)
    '        If swModel Is Nothing Then
    '            Throw New Exception("Falha ao ativar o documento.")
    '        End If

    '        ' Verifica se o documento foi carregado completamente
    '        If Not WaitForDocumentToLoad(swModel, docType) Then
    '            Throw New Exception("O documento não foi carregado completamente após várias tentativas.")
    '        End If

    '        ' Ajusta a visualização caso seja um desenho
    '        If docType = swDocumentTypes_e.swDocDRAWING AndAlso visualizarDesenho Then
    '            swModel.ViewZoomtofit2()
    '        End If

    '    Catch ex As Exception
    '        ' Log ou mensagem de erro
    '        MsgBox($"Erro ao abrir o documento: {ex.Message}")
    '    End Try

    'End Sub

    Sub OpenDocumentAndWait(filePath As String, ByVal visualizarDesenho As Boolean, ByRef swModel As ModelDoc2)
        Try
            ' Verifica se o SolidWorks está rodando antes de abrir o documento
            Dim solidWorksProcess As Process = GetSolidWorksProcess()

            If solidWorksProcess Is Nothing Then
                Throw New Exception("O SolidWorks não está em execução.")
            End If

            ' Conecta ao SolidWorks
            IntanciaSolidWorks.ConectarSolidWorks()

            ' Obtém o tipo de documento baseado no arquivo
            Dim docType As swDocumentTypes_e = GetDocumentType(filePath)

            ' Define opções para abrir o documento em modo exclusivo
            Dim openOptions As Integer = swOpenDocOptions_e.swOpenDocOptions_Silent Or
                              swOpenDocOptions_e.swOpenDocOptions_LoadModel

            ' Variáveis para erros e avisos
            Dim errors As Integer = 0
            Dim warnings As Integer = 0

            ' Abre o documento
            swModel = swapp.OpenDoc6(filePath, docType, openOptions, "", errors, warnings)

            ' Verifica se houve falha ao abrir
            If swModel Is Nothing OrElse errors <> 0 Then
                Throw New Exception($"Falha ao abrir o documento. Código de erro: {errors}")
            End If

            ' Ativa o documento
            swModel = swapp.ActivateDoc(filePath)
            If swModel Is Nothing Then
                Throw New Exception("Falha ao ativar o documento.")
            End If

            ' Verifica se o documento foi carregado completamente
            If Not WaitForDocumentToLoad(swModel, docType) Then
                Throw New Exception("O documento não foi carregado completamente após várias tentativas.")
            End If

            ' Ajusta a visualização caso seja um desenho
            If docType = swDocumentTypes_e.swDocDRAWING AndAlso visualizarDesenho Then
                swModel.ViewZoomtofit2()
            End If

        Catch ex As Exception
            ' Log ou mensagem de erro
            MsgBox($"Erro ao abrir o documento: {ex.Message}")
        Finally
            ' Verifica se o SolidWorks ainda está ativo
            Dim solidWorksProcess As Process = GetSolidWorksProcess()
            If solidWorksProcess Is Nothing OrElse solidWorksProcess.HasExited Then
                MsgBox("O SolidWorks foi fechado inesperadamente.", MsgBoxStyle.Critical)
            End If
        End Try
    End Sub

    ' Função para obter o processo do SolidWorks
    Private Function GetSolidWorksProcess() As Process
        Return Process.GetProcessesByName("SLDWORKS").FirstOrDefault()
    End Function


    ''' <summary>
    ''' Aguarda até que o documento esteja completamente carregado.
    ''' </summary>
    ''' <param name="swModel">O modelo ativo do SolidWorks.</param>
    ''' <param name="docType">O tipo do documento.</param>
    ''' <returns>True se o documento foi carregado com sucesso; caso contrário, False.</returns>
    Private Function WaitForDocumentToLoad(ByVal swModel As ModelDoc2, ByVal docType As swDocumentTypes_e) As Boolean
        Dim maxRetries As Integer = 10
        Dim retryInterval As Integer = 100 ' Tempo em milissegundos

        For retryCount As Integer = 1 To maxRetries
            ' Verifica se o documento está carregado baseado no tipo
            Select Case docType
                Case swDocumentTypes_e.swDocPART
                    If TypeOf swModel Is PartDoc Then Return True
                Case swDocumentTypes_e.swDocASSEMBLY
                    If TypeOf swModel Is AssemblyDoc Then Return True
                Case swDocumentTypes_e.swDocDRAWING
                    If TypeOf swModel Is DrawingDoc Then Return True
            End Select

            ' Aguarda antes de tentar novamente
            Threading.Thread.Sleep(retryInterval)
        Next

        ' Retorna falso se não foi possível confirmar o carregamento
        Return False
    End Function



    ' Função para obter o tipo de documento com base na extensão do arquivo
    Private Function GetDocumentType(ByVal filePath As String) As swDocumentTypes_e
        ExtensaoArquivoCorrente = IO.Path.GetExtension(filePath).ToLower()
        Select Case ExtensaoArquivoCorrente
            Case ".sldprt"
                Return swDocumentTypes_e.swDocPART
            Case ".sldasm"
                Return swDocumentTypes_e.swDocASSEMBLY
            Case ".slddrw"
                Return swDocumentTypes_e.swDocDRAWING
            Case Else
                Throw New ArgumentException("Tipo de documento desconhecido.")
        End Select
    End Function

    Public Sub CriarColunasDataGridView(ByVal dgv As DataGridView, ByVal colunas As String())
        ' Limpa todas as colunas existentes no DataGridView
        ' dgv.Columns.Clear()

        ' Cria a primeira coluna como do tipo imagem
        Dim colunaImagem As New DataGridViewImageColumn()
        colunaImagem.HeaderText = "tipo"
        colunaImagem.Name = "dgvListaBomTipoArquivo"
        colunaImagem.ImageLayout = DataGridViewImageCellLayout.Zoom
        colunaImagem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        colunaImagem.Frozen = True

        ' Adiciona a coluna de imagem ao DataGridView
        dgv.Columns.Add(colunaImagem)

        ' Cria a primeira coluna como do tipo imagem
        Dim colunaImagemAtencao As New DataGridViewImageColumn()
        colunaImagemAtencao.HeaderText = "RNC"
        colunaImagemAtencao.Name = "dgvRNC"
        colunaImagemAtencao.ImageLayout = DataGridViewImageCellLayout.Zoom
        colunaImagemAtencao.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader

        ' Adiciona a coluna de imagem ao DataGridView
        dgv.Columns.Add(colunaImagemAtencao)

        ' Adiciona as colunas de texto restantes ao DataGridView
        For Each coluna As String In colunas
            ' Cria uma nova coluna
            Dim novaColuna As New DataGridViewTextBoxColumn()

            ' Define o nome da coluna
            novaColuna.HeaderText = coluna
            novaColuna.Name = coluna
            novaColuna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader

            ' Adiciona a coluna ao DataGridView
            dgv.Columns.Add(novaColuna)
        Next

        dgv.Columns("CodMatFabricante").Frozen = True

        dgv.Columns("IdMaterial").Visible = False



    End Sub

    Private Sub Painel_Leitura_Dados_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Cria as Colunas para ler a lista de material
        colunas = {"IdMaterial",'
                   "CodMatFabricante",'
               "DescResumo",'
               "DescDetal",'
               "Autor",'
               "Palavrachave",'
               "Notas",'
               "Espessura",'
               "Altura",'
               "Largura",'
               "Material",'
               "AreaPintura",'
               "NumeroDobras",'
               "Peso",'
               "EnderecoArquivo",'
               "Acabamento",
               "txtSoldagem",
               "txtTipoDesenho",
               "txtCorte",
               "txtDobra",
               "txtSolda",
               "txtPintura",
               "txtMontagem",
               "RNC",
"Comprimentocaixadelimitadora",
"Larguracaixadelimitadora",
"Espessuracaixadelimitadora",
"txtItemEstoque",
"Qtde"}



        ' Chama a função para criar as colunas
        CriarColunasDataGridView(dgvDataGridBOM, colunas)

        TimerdgvDesenhos.Enabled = True



        cl_BancoDados.ComboBoxDataSet("projetos", "IdProjeto", "Projeto", cboProjeto, " WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')  AND (Liberado = 'S')")
        cl_BancoDados.ComboBoxDataSet("acabamento", "idAcabamento", "DescAcabamento", cboOpcoesAcabamento, "WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')")

        cl_BancoDados.ComboBoxDataSet("projetos", "IdProjeto", "Projeto", cboProjetoPCP, " WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')  AND (Liberado = 'S')")




        ' cl_BancoDados.ComboBoxDataSet("tipoproduto", "idtipoproduto", "tipoproduto", cboTitulo, "WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY tipoproduto")


        ' Chama a função para carregar os dados no CheckedListBox
        PreencherCheckedListBox("Select Descfamilia from familia WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY Descfamilia", chkBoxTipoDesenho)

        ' Chama a função para carregar os dados no CheckedListBox
        PreencherCheckedListBox("Select DescAcabamento from acabamento WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescAcabamento ", chkBoxAcabamento)

        Timerdgvos.Enabled = True

        TimerDGVListaMaterialSW.Enabled = True

        Dim version As Version = Assembly.GetExecutingAssembly().GetName().Version

        tslVersaoSistema.Text = My.Settings.BancoDadosAtivo.ToString & ": " & version.ToString()

        '   TimerpcpAgrupamentoProjeto.Enabled = True






        ' CarregarDadosAgrupados()

    End Sub



    Private viewDadosmaterial As DataView

    Public Sub LimparTelaVariaveis()

        ' Limpar campos do formulário
        LimparCamposFormulario()

        ' Limpar campos relacionados à lista de corte
        LimparCamposListaCorte()

        ' Limpar campos relacionados ao processo
        LimparCamposProcesso()

        ' Limpar campos da caixa delimitadora
        LimparCamposCaixaDelimitadora()

        ' Limpar propriedades do objeto DadosArquivoCorrente
        LimparDadosArquivoCorrente()

    End Sub

    ''' <summary>
    ''' Limpa os campos do formulário.
    ''' </summary>
    Private Sub LimparCamposFormulario()
        Me.cboTitulo.Text = ""
        Me.txtAssuntoSubiTitulo.Clear()
        Me.txtComentarios.Clear()
        Me.txtAuthor.Clear()
        Me.txtPalavraChave.Clear()

        Me.btnPendencias.Image = Nothing
        'Me.txtDescricaoPendencia.Clear()

        optProcessoSoldagemSim.Checked = False
        optProcessoSoldagemNao.Checked = False

        OPTEstoqueSim.Checked = False
        OPTEstoqueNao.Checked = False

        chkCorte.Checked = False
        chkDobra.Checked = False
        chkSolda.Checked = False
        chkPintura.Checked = False
        chkMontagem.Checked = False

        'cboTipoDesenho.Text = ""
        chkVerificarPDF.Checked = False
        chkVerificarDXF.Checked = False
        chkVerificarDFT.Checked = False
        chkVerificarLXDS.Checked = False

        ' cboAcabamento.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa os campos relacionados à lista de corte.
    ''' </summary>
    Private Sub LimparCamposListaCorte()
        Me.lblEspessura.Text = ""
        Me.lblLargura.Text = ""
        Me.lblComprimento.Text = ""
        Me.lblNumeroDobra.Text = ""
        Me.lblPeso.Text = ""
        Me.lblMaterial.Text = ""
        Me.lblAreaPintura.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa os campos relacionados à caixa delimitadora.
    ''' </summary>
    Private Sub LimparCamposCaixaDelimitadora()
        Me.lblAlturaTotalCaixaDelimitadora.Text = ""
        Me.lblProfundidadeTotalCaixaDelimitadora.Text = ""
        Me.lblProfundidadeTotalCaixaDelimitadora.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa os campos relacionados ao processo.
    ''' </summary>
    Private Sub LimparCamposProcesso()
        Me.lblPeso.Text = ""
        Me.lblMaterial.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa as propriedades do objeto DadosArquivoCorrente.
    ''' </summary>
    Private Sub LimparDadosArquivoCorrente()
        With DadosArquivoCorrente
            ' Informações gerais
            .IdMaterial = Nothing
            .NomeArquivoComExtensao = Nothing
            .NomeArquivoSemExtensao = Nothing
            .EnderecoArquivo = Nothing
            .EnderecoArquivoAterior = Nothing
            .Extencao = Nothing
            .DataCriacaDesenho = Nothing
            .DataUltimoSalvamento = Nothing
            .SalvoUltimaVezPor = Nothing
            .Titulo = Nothing
            .AssuntoSubiTitulo = Nothing
            .Comentarios = Nothing
            .Author = Nothing
            .PalavraChave = Nothing

            ' Processo
            .soldagem = Nothing
            .acabamento = Nothing
            .TipoDesenho = Nothing
            .Corte = Nothing
            .Dobra = Nothing
            .Solda = Nothing
            .Pintura = Nothing
            .Montagem = Nothing
            .ItemEstoque = Nothing
            .rnc = Nothing
            .Qtde = Nothing

            ' Arquivos associados
            .ArquivoPdf = Nothing
            .ArquivoDxf = Nothing
            .ArquivoDft = Nothing
            .ArquivoLXDS = Nothing

            ' Caixa delimitadora
            .Profundidadeaixadelimitadora = Nothing
            .Larguracaixadelimitadora = Nothing
            .Alturacaixadelimitadora = Nothing

            ' Lista de corte
            .ComprimentoBlank = Nothing
            .LarguraBlank = Nothing
            .Espessura = Nothing
            .PerimetroCorteExterno = Nothing
            .PerimetroCorteInterno = Nothing
            .NumeroDobras = Nothing
            .Massa = Nothing
            .Material = Nothing
            .AreaPintura = Nothing
        End With
    End Sub


    Public Sub PreencherCheckedListBox(ByVal query As String, ByVal clb As CheckedListBox)
        ' Limpa o CheckedListBox antes de preencher
        clb.Items.Clear()

        ' Chama a função CarregarDados para obter os dados
        Dim dt As System.Data.DataTable = cl_BancoDados.CarregarDados(query)

        ' Verifica se há dados retornados
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ' Itera sobre as linhas do DataTable e adiciona ao CheckedListBox
            For Each row As DataRow In dt.Rows
                ' Supondo que a primeira coluna do DataTable seja o que você quer exibir
                clb.Items.Add(row(0).ToString())
            Next
        Else
            MessageBox.Show("Nenhum dado encontrado.")
        End If
    End Sub

    Private Sub TimerdgvDesenhos_Tick(sender As Object, e As EventArgs) Handles TimerdgvDesenhos.Tick

        dgvDesenhos.DataSource = cl_BancoDados.CarregarDados("Select IdMaterial, Sobra_Fabrica,
               CodMatFabricante,
               DescResumo,
               DescDetal,
               Autor,
               Palavrachave,
               Notas,
               Espessura,
               Altura,
               Largura,
               AreaPintura,
               NumeroDobras,
               Peso,
               EnderecoArquivo,
               Acabamento,
               txtSoldagem,
               txtTipoDesenho,
               txtCorte,
               txtDobra,
               txtSolda,
               txtPintura,
               txtMontagem,
               RNC,
Comprimentocaixadelimitadora,
Larguracaixadelimitadora,
Espessuracaixadelimitadora,
txtItemEstoque
from material where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')
AND (DESCRESUMO LIKE '%" & Me.TxtPesgTitulo.Text & "%')
and (CodMatFabricante LIKE '%" & Me.TxtPesgNomeDesenho.Text & "%')
and (DESCDETAL LIKE '%" & Me.TxtPesqSubtitulo.Text & "%')
and (DESCDETAL LIKE '%" & Me.TxtPesqSubtitulo2.Text & "%')
and (DESCDETAL LIKE '%" & Me.TxtPesqSubtitulo3.Text & "%')
and   (EnderecoArquivo <> '' AND statusMat = 'A')
       ORDER BY 
    CASE 
        WHEN RNC IS NOT NULL AND RNC <> '' THEN 0 
        ELSE 1 
    END,
    CodMatFabricante, 
    DescResumo
LIMIT 200;")

        dgvDesenhos.Columns("IdMaterial").Visible = False
        dgvDesenhos.Columns("CodMatFabricante").Frozen = True
        dgvDesenhos.Columns("EnderecoArquivo").Visible = False
        dgvDesenhos.Columns("Palavrachave").Visible = False
        dgvDesenhos.Columns("Notas").Visible = False
        dgvDesenhos.Columns("RNC").Visible = False

        For Each col As DataGridViewColumn In dgvDesenhos.Columns
            If col.Width > 350 Then
                col.Width = 351
            End If
        Next


        '  cl_BancoDados.ProcessarArquivosDGV(dgvDesenhos)


        TimerdgvDesenhos.Enabled = False






    End Sub

    Private Sub TxtPesgNomeDesenho_TextChanged(sender As Object, e As EventArgs) Handles TxtPesgNomeDesenho.TextChanged
        TimerdgvDesenhos.Enabled = True
    End Sub

    Private Sub TxtPesgTitulo_TextChanged(sender As Object, e As EventArgs) Handles TxtPesgTitulo.TextChanged
        TimerdgvDesenhos.Enabled = True
    End Sub

    Private Sub TxtPesqSubtitulo_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqSubtitulo.TextChanged
        TimerdgvDesenhos.Enabled = True
    End Sub

    Private Sub TxtPesqSubtitulo2_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqSubtitulo2.TextChanged
        TimerdgvDesenhos.Enabled = True
    End Sub

    Private Sub TxtPesqSubtitulo3_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqSubtitulo3.TextChanged
        TimerdgvDesenhos.Enabled = True
    End Sub

    Private Sub dgvDesenhos_DoubleClick(sender As Object, e As EventArgs) Handles dgvDesenhos.DoubleClick
        OpenDocumentAndWait(dgvDesenhos.CurrentRow.Cells("EnderecoArquivo").Value.ToString, True, swModel)
    End Sub

    Private Sub AbrirPDFDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            Dim ArquivoPdf As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

            ' Obtém o caminho completo
            ArquivoPdf = Path.GetFullPath(ArquivoPdf)

            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoPdf) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoPdf)

                    p.Start()
                    p.WaitForExit()

                    DGVListaMaterialSW.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub AbrirDXFDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Click
        Try

            Dim ArquivoDXF As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".DXF")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    p.WaitForExit()

                    DGVListaMaterialSW.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub btnLimparBom_Click(sender As Object, e As EventArgs) Handles btnLimparBom.Click

        dgvDataGridBOM.DataSource = Nothing
        dgvDataGridBOM.Rows.Clear()
        dgvDataGridBOM.Refresh()


    End Sub

    Private Sub dgvDataGrid_DoubleClick(sender As Object, e As EventArgs) Handles dgvDataGridBOM.DoubleClick

        Dim ArquivoListaBom As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString

        ' Obtém o caminho completo
        ArquivoListaBom = Path.GetFullPath(ArquivoListaBom)

        ' Verifica se o arquivo existe e o abre
        If File.Exists(ArquivoListaBom) Then
            Process.Start(ArquivoListaBom)
        End If

    End Sub

    Private Sub txtAuthor_LostFocus(sender As Object, e As EventArgs) Handles txtAuthor.LostFocus
        ' Verifique se o swModel foi aberto com sucesso

        Try


            If Not swModel Is Nothing Then

                swModel.SummaryInfo(swSummInfoField_e.swSumInfoAuthor) = Me.txtAuthor.Text
                swModel.SaveSilent()

            End If

        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub txtPalavraChave_LostFocus(sender As Object, e As EventArgs) Handles txtPalavraChave.LostFocus

        Try


            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then


                swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords) = Me.txtPalavraChave.Text

                swModel.SaveSilent()

            End If

        Catch ex As Exception
        Finally
        End Try


    End Sub

    Private Sub txtComentarios_LostFocus(sender As Object, e As EventArgs) Handles txtComentarios.LostFocus

        Try


            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then


                swModel.SummaryInfo(swSummInfoField_e.swSumInfoComment) = Me.txtComentarios.Text

                swModel.SaveSilent()

            End If

        Catch ex As Exception
        Finally
        End Try


    End Sub

    Private Sub txtAssuntoSubiTitulo_LostFocus(sender As Object, e As EventArgs) Handles txtAssuntoSubiTitulo.LostFocus

        Try


            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject) = Me.txtAssuntoSubiTitulo.Text

                swModel.SaveSilent()
            End If

        Catch ex As Exception
        Finally

        End Try


    End Sub

    Private Sub btnpdf_Click(sender As Object, e As EventArgs)

        Try

            ' Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                ' Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                    Try

                        IntanciaSolidWorks.ConectarSolidWorks()
                        'swApparq = CreateObject("SldWorks.Application")

                        swModel = swapp.ActiveDoc
                        'swModel = swApparq.ActiveDoc

                        swModel.Visible = True

                        swModelDocExt = swModel.Extension

                        DadosArquivoCorrente.ExportToPDF(swModel, swModel.GetPathName.ToString, True)

                        chkVerificarPDF.Checked = True


                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em PDF")
                    Finally

                    End Try
                End If
            End If
        Catch ex As Exception
        Finally
        End Try

        ' Dim pdfFilePath As String = Path.ChangeExtension(swModel.GetPathName.ToString, ".pdf")


        ' pdfsinco.EscreverPdf(pdfFilePath, "C:\bin", "testo no pdf")


    End Sub

    Private Sub btnAtualizaDados_Click(sender As Object, e As EventArgs)

        If dgvDataGridBOM.Rows.Count > 0 Then

            For i As Integer = 0 To dgvDataGridBOM.Rows.Count

                If File.Exists(dgvDataGridBOM.Rows(i).Cells("EnderecoArquivo").Value) Then

                    dgvDataGridBOM.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                End If

            Next

        End If

    End Sub

    Private Sub Abrir3DDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Dim ArquivoSW As String = dgvDesenhos.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

        ' Obtém o caminho completo
        ArquivoSW = Path.GetFullPath(ArquivoSW)

        ' Verifica se o arquivo existe e o abre
        If File.Exists(ArquivoSW) Then
            Using p As New Diagnostics.Process
                p.StartInfo = New ProcessStartInfo(ArquivoSW)

                p.Start()
                p.WaitForExit()

                dgvDesenhos.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
            End Using
        End If

    End Sub

    Private Sub dgvDesenhos_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvDesenhos.DataError
        Try
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Sub ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Click

        If OrdemServico.LIBERADO_ENGENHARIA <> "" Then

            MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")
            Exit Sub
        Else

            If MsgBox("Tem certeza que deseja desabilitar o Desenho Selecionado?", MsgBoxStyle.YesNo, "Confirmar desabilitação!") = MsgBoxResult.No Then

                Exit Sub
            Else

                ' Atualiza o status no banco de dados
                cl_BancoDados.Salvar("UPDATE ordemservicoitem SET D_E_L_E_T_E = '*' WHERE CodMatFabricante = '" & DGVListaMaterialSW.CurrentRow.Cells("CodMatFabricante").Value.ToString() & "'")

                ' Remove a linha corrente do DataGridView
                If DGVListaMaterialSW.CurrentRow IsNot Nothing Then
                    DGVListaMaterialSW.Rows.Remove(DGVListaMaterialSW.CurrentRow)
                End If
                ' MsgBox("Desenho desabilitado e removido da lista.")
            End If

        End If

    End Sub

    Private Sub TimerMontaPeca_Tick(sender As Object, e As EventArgs) Handles TimerMontaPeca.Tick

        DGVMontaPeca.DataSource = cl_BancoDados.CarregarDados("Select IdMontaPeca, idmaterialpeca,NomeArquivoSemExtensao, descdetal,
DescDetal,
DescFamilia, CodMatFabricante, CodigoJuridicoMat,
d_e_l_e_t_e, Valor, pecaqtde, Peso
From viewmontapeca1
Where NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'
And (d_e_l_e_t_e IS NOT NULL or d_e_l_e_t_e is null)
order by descdetal")

        ' Verifique se o controle DGVMontaPeca foi criado antes de acessar suas colunas
        If DGVMontaPeca.Columns.Count > 0 Then

            DGVMontaPeca.Columns("DescDetal").HeaderText = "Descricao"
            DGVMontaPeca.Columns("CodMatFabricante").HeaderText = "Cod.Fabr."
            DGVMontaPeca.Columns("CodigoJuridicoMat").HeaderText = "Fabricante"

            DGVMontaPeca.Columns("IdMontaPeca").Visible = False
            DGVMontaPeca.Columns("idmaterialpeca").Visible = False
            'DGVMontaPeca.Columns("IdMaterial").Visible = False
            DGVMontaPeca.Columns("DescDetal").Visible = False
            DGVMontaPeca.Columns("d_e_l_e_t_e").Visible = False

        End If

        ' DGVMontaPeca.Refresh()

        For Each col As DataGridViewColumn In DGVMontaPeca.Columns
            If col.Width > 400 Then
                col.Width = 401
            End If
        Next

        TimerMontaPeca.Enabled = False

    End Sub

    Private Sub cboProjeto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjeto.SelectedIndexChanged


        Try

            'OrdemServico.PROJETO = cboProjeto.Text
            'OrdemServico.IDPROJETO = Convert.ToInt32(cboProjeto.SelectedValue)

            ' Verificar se o combo box contém algum valor selecionado
            If cboProjeto.SelectedItem Is Nothing Then
                Throw New Exception("Nenhum projeto selecionado. Por favor, selecione um projeto válido.")
            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboProjeto.Text) Then
                Throw New Exception("O nome do projeto não pode estar vazio. Selecione um projeto válido.")
            End If

            ' Atribuir valores ao objeto OrdemServico
            OrdemServico.PROJETO = cboProjeto.Text

            ' Tentar converter o valor selecionado para inteiro
            ' Dim idProjeto As Integer
            If Integer.TryParse(cboProjeto.SelectedValue?.ToString(), OrdemServico.IDPROJETO) Then
                ' OrdemServico.IDPROJETO = idProjeto
                'Else
                '    Throw New Exception("O ID do projeto selecionado não é válido. Por favor, verifique.")
            End If

            ' Preenchendo o ComboBox com os dados do banco
            cl_BancoDados.ComboBoxDataSet("tags", "IdTag", "Tag", cboTag, " where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') AND IdProjeto = '" & OrdemServico.IDPROJETO & "'")

            Try

                ' Tentativa de retornar o nome da empresa
                txtCliente.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT descempresa FROM projetos where idprojeto  = " & OrdemServico.IDPROJETO, "descempresa")

                OrdemServico.TAG = cboTag.Text

                OrdemServico.DESCEMPRESA = txtCliente.Text

            Catch ex As Exception
                Me.txtCliente.Clear()
                OrdemServico.PROJETO = Nothing
                OrdemServico.TAG = Nothing
                OrdemServico.DESCEMPRESA = Nothing

            End Try
        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
        End Try

    End Sub

    Private Sub cboTag_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTag.SelectedIndexChanged



        Try

            'OrdemServico.TAG = cboTag.Text
            'OrdemServico.IDTAG = cboTag.SelectedValue

            ' Verificar se o combo box contém algum valor selecionado
            If cboTag.SelectedItem Is Nothing Then
                Throw New Exception("Nenhuma TAG selecionada. Por favor, selecione uma TAG válida.")
            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboTag.Text) Then
                Throw New Exception("O nome da TAG não pode estar vazio. Selecione uma TAG válida.")
            End If

            ' Atribuir o texto da TAG ao objeto OrdemServico
            OrdemServico.TAG = cboTag.Text

            ' Tentar converter o valor selecionado para inteiro
            ' Dim idTag As Integer
            If Integer.TryParse(cboTag.SelectedValue?.ToString(), OrdemServico.IDTAG) Then
                '    OrdemServico.IDTAG = idTag
                ' Else
                '   Throw New Exception("O ID da TAG selecionada não é válido. Por favor, verifique.")
            End If

            ' Tenta retornar a descrição da Tag
            txtDescricaoTag.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DescTag FROM tags where idtag = '" & OrdemServico.IDTAG & "'", "DescTag")



            Try
                OrdemServico.DataPrevisao = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DataPrevisao FROM tags where idtag = '" & OrdemServico.IDTAG & "'", "DataPrevisao")

            Catch ex As Exception

                OrdemServico.DataPrevisao = ""

            End Try



        Catch ex As Exception
            ' Em caso de erro, limpa o campo de descrição
            Me.txtDescricaoTag.Clear()

            OrdemServico.TAG = Nothing
            OrdemServico.IDTAG = Nothing

            ' Opcional: Registrar o erro em um log
            ' LogError(ex) ' Função fictícia para logar erros
            ' MessageBox.Show("Erro ao carregar a descrição da Tag.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Timerdgvos_Tick(sender As Object, e As EventArgs) Handles Timerdgvos.Tick

        'CarregarDadosAgrupados()

        Dim Filtro As String

        ' Verifica se a checkbox está marcada e ajusta o filtro
        If chkMostraLiberadasPelaEngenharia.Checked = False Then
            Filtro = " AND (LIBERADO_ENGENHARIA = '' OR LIBERADO_ENGENHARIA IS NULL )"
        End If

        dgvos.DataSource = cl_BancoDados.CarregarDados("SELECT IDOrdemServico,
                                                IDPROJETO,
                                                PROJETO,
                                                TAG,
                                                IDTAG,
                                                DESCRICAO,
                                                DESCEMPRESA,
                                                replace(ENDERECOOrdemServico,'##','\\') as ENDERECO,
                                                CRIADOPOR as USUARIO,
                                                DATACRIACAO as DATA,
                                                LIBERADO_ENGENHARIA,
                                                DATA_LIBERACAO_ENGENHARIA,
                                                ESTATUS,
                                                DataPrevisao
                                                FROM ordemservico WHERE (D_E_L_E_T_E <> '*' or D_E_L_E_T_E is null)" & Filtro &
                                                " AND CRIADOPOR LIKE '%" & Me.txtPesqCriadoPor.Text & "%'")

        ' Configura a visibilidade das colunas
        With dgvos.Columns
            .Item("USUARIO").Visible = True
            .Item("DATA").Visible = False
            .Item("ENDERECO").Visible = True
            .Item("IDTAG").Visible = False
            .Item("IDPROJETO").Visible = False
            .Item("ESTATUS").Visible = False
        End With

        Timerdgvos.Enabled = False

    End Sub

    Private Sub chkMostraLiberadasPelaEngenharia_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostraLiberadasPelaEngenharia.CheckedChanged
        Timerdgvos.Enabled = True
    End Sub

    Private Sub dgvos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvos.CellFormatting

        If dgvos.Columns(e.ColumnIndex).Name = "LIBERADO_ENGENHARIA" AndAlso e.Value IsNot Nothing Then
            If e.Value.ToString() = "S" Then
                dgvos.Rows(e.RowIndex).Cells("dgvStatus").Value = My.Resources.verificado1
            Else
                dgvos.Rows(e.RowIndex).Cells("dgvStatus").Value = My.Resources.atencao
            End If
        End If
    End Sub

    Private Sub dgvos_Click(sender As Object, e As EventArgs) Handles dgvos.Click


        Try


            ' Verifica se há uma linha selecionada e se o valor da célula não é nulo
            If dgvos.CurrentRow IsNot Nothing AndAlso dgvos.CurrentRow.Cells("IDOrdemServico").Value IsNot Nothing Then

                OrdemServico.IDOrdemServico = dgvos.CurrentRow.Cells("IDOrdemServico").Value.ToString
                OrdemServico.IDPROJETO = dgvos.CurrentRow.Cells("IDPROJETO").Value.ToString
                OrdemServico.IDTAG = dgvos.CurrentRow.Cells("IDTAG").Value.ToString

                OrdemServico.PROJETO = dgvos.CurrentRow.Cells("PROJETO").Value.ToString
                OrdemServico.TAG = dgvos.CurrentRow.Cells("TAG").Value.ToString

                OrdemServico.LIBERADO_ENGENHARIA = dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value.ToString

                OrdemServico.ESTATUS = dgvos.CurrentRow.Cells("ESTATUS").Value.ToString

                OrdemServico.ENDERECOOrdemServico = dgvos.CurrentRow.Cells("ENDERECO").Value.ToString

                OrdemServico.DataPrevisao = dgvos.CurrentRow.Cells("DataPrevisao").Value.ToString

                Me.lblOrdemServicoAtiva.Text = "Projeto: " & OrdemServico.PROJETO & " - Tag: " & OrdemServico.TAG & " - OS: " & OrdemServico.IDOrdemServico

                Me.cboProjeto.Text = OrdemServico.PROJETO
                Me.txtCliente.Text = dgvos.CurrentRow.Cells("DESCEMPRESA").Value.ToString
                Me.cboTag.Text = OrdemServico.TAG
                Me.txtDescricaoTag.Text = OrdemServico.TAG
                Me.txtDescricao.Text = dgvos.CurrentRow.Cells("DESCRICAO").Value.ToString

            End If
        Catch ex As Exception

            OrdemServico.IDOrdemServico = Nothing
            ' Exibe uma mensagem de erro caso ocorra alguma exceção
            'MessageBox.Show("Erro ao selecionar Ordem de Serviço: " & ex.Message)
        Finally

        End Try

        If OrdemServico.IDOrdemServico.ToString <> "" And OrdemServico.LIBERADO_ENGENHARIA.ToString = "S" Then

            TSBSalvarOrdemServico.Enabled = False

        Else

            TSBSalvarOrdemServico.Enabled = True

        End If

        'DGVListaMaterialSWMaterial.Enabled = True
        TimerDGVListaMaterialSW.Enabled = True

    End Sub

    Private Sub TimerDGVListaMaterialSW_Tick(sender As Object, e As EventArgs) Handles TimerDGVListaMaterialSW.Tick

        Try
            'If cl_BancoDados.AbrirBanco = False Then

            '    cl_BancoDados.AbrirBanco()

            'End If



            DGVListaMaterialSW.DataSource = cl_BancoDados.CarregarDados("SELECT IDOrdemServicoITEM,
    QtdeTotal,
    CodMatFabricante,
    DescResumo,
    DescDetal,
    Materialsw,
    Espessura,
    Altura,
    Largura,
    Unidade,
    txtItemEstoque,
    ACABAMENTO,
    txtTipoDesenho,
    RTRIM(UPPER(replace(EnderecoArquivo,'##','\\'))) as  EnderecoArquivo,
    ProdutoPrincipal,
    Qtde, areapintura, Peso
                FROM
                ordemservicoitem
                WHERE
                (D_E_L_E_T_E <> '*')
                AND (IDOrdemServico = " & OrdemServico.IDOrdemServico & ")
                AND (CodMatFabricante LIKE '%" & Me.txtPesqNumeroDesenho.Text & "%')
                AND (acabamento LIKE '%" & Me.txtPesqAcabamentoDesenho.Text & "%')
                ORDER BY
                    IDOrdemServicoITEM")


            DGVListaMaterialSW.Columns("EnderecoArquivo").Frozen = False
            DGVListaMaterialSW.Columns("ProdutoPrincipal").Frozen = False
            DGVListaMaterialSW.Columns("Qtde").Frozen = False
            DGVListaMaterialSW.Columns("areapintura").Frozen = False
            DGVListaMaterialSW.Columns("Peso").Frozen = False
            ' DGVListaMaterialSW.Columns("IDOrdemServico").Visible = False



        Catch ex As Exception
        Finally

        End Try

        'CarregarDadosDGV()

        TimerDGVListaMaterialSW.Enabled = False

    End Sub

    Private Sub CarregarDadosDGV()
        Try
            ' Certifique-se de que o banco está aberto
            ' If Not cl_BancoDados.AbrirBanco Then Exit Sub

            ' Consultas SQL otimizadas
            Dim sqlListaMaterial As String =
            "SELECT 
        IDOrdemServicoITEM,
          QtdeTotal,
         UPPER(RTrim(CodMatFabricante)) As CodMatFabricante,
         UPPER(RTrim(DescResumo)) As DescResumo,
                    UPPER(RTrim(DescDetal)) As DescDetal,
                    UPPER(RTrim(Autor)) As Autor,
                    UPPER(RTrim(Palavrachave)) As Palavrachave,
                    UPPER(RTrim(Notas)) As Notas,
                    Espessura,
                    Altura,
                    Largura,
                    Replace(areapintura, ',', '.') AS AreaPintura,
                    AreaPinturaUnitario,
                    NumeroDobras,
                    Peso,
                    PesoUnitario,
                    UPPER(RTrim(Unidade)) As Unidade,
                    IDOrdemServico,
                    UPPER(RTrim(PROJETO)) As PROJETO,
                    UPPER(RTrim(Tag)) As TAG,
                    UPPER(RTrim(ESTATUS_OrdemServico)) As ESTATUS_OrdemServico,
                    IdMaterial,
                    QtdeProduzida,
                    QtdeFaltante,
                    UPPER(RTrim(CRIADOPOR)) As CRIADOPOR,
                    DATACRIACAO,
                    UPPER(RTrim(ESTATUS)) As ESTATUS,
                    D_E_L_E_T_E,
                    UPPER(RTrim(ORDEMSERVICOITEMFINALIZADO)) As ORDEMSERVICOITEMFINALIZADO,
                    IdEmpresa,
                    idProjeto,
                    IdTag,
                    UPPER(RTrim(UnidadeSW)) As UnidadeSW,
                    UPPER(RTrim(ValorSW)) As ValorSW,
                    DtCad,
                    UPPER(RTrim(UsuarioCriacao)) As UsuarioCriacao,
                    UsuarioAlteracao,
                    UPPER(RTrim(DtAlteracao)) As DtAlteracao,
                    UPPER(RTrim(Replace(EnderecoArquivo, '##', '\\'))) AS EnderecoArquivo,
                    UPPER(RTrim(MaterialSW)) As MaterialSW,
                    Fator,
                    qtde,
                    UPPER(RTrim(acabamento)) As acabamento,
                    UPPER(RTrim(txtTipoDesenho)) As TipoDesenho,
                    UPPER(RTrim(MaterialSW)) As Material,
                    ProdutoPrincipal,
                    txtItemEstoque,
                    AreaPinturaUnitario,
                    PesoUnitario,
                     Acabamento,
                       txtSoldagem,
                       txtTipoDesenho,
                       txtCorte,
                       txtDobra,
                       txtSolda,
                       txtPintura,
                       txtMontagem,
                       RNC,
        Comprimentocaixadelimitadora,
        Larguracaixadelimitadora,
        Espessuracaixadelimitadora
             FROM ordemservicoitem
             WHERE D_E_L_E_T_E <> '*'
               AND txtTipoDesenho <> 'MATERIAL'
               AND IDOrdemServico = @IDOrdemServico
               AND CodMatFabricante LIKE @NumeroDesenho
               AND Acabamento LIKE @Acabamento
             ORDER BY IDOrdemServicoITEM"

            Dim parametrosListaMaterial = New Dictionary(Of String, Object) From {
            {"@IDOrdemServico", OrdemServico.IDOrdemServico},
            {"@NumeroDesenho", $"%{Me.txtPesqNumeroDesenho.Text.Trim()}%"},
            {"@Acabamento", $"%{Me.txtPesqAcabamentoDesenho.Text.Trim()}%"}
        }

            Dim sqlListaMaterialSW As String =
            "SELECT IDOrdemServico,
                    CodMatFabricante,
                    DescResumo,
                    DescDetal,
                    SUM(REPLACE(QtdeTotal, ',', '.')) AS QtdeTotal,
                    txtTipoDesenho
             FROM ordemservicoitem
             WHERE D_E_L_E_T_E <> '*'
               AND IDOrdemServico = @IDOrdemServico
               AND txtTipoDesenho = 'MATERIAL'
             GROUP BY IDOrdemServico,
                      CodMatFabricante,
                      DescResumo,
                      DescDetal,
                      txtTipoDesenho
             ORDER BY IDOrdemServico"

            Dim parametrosListaMaterialSW = New Dictionary(Of String, Object) From {
            {"@IDOrdemServico", OrdemServico.IDOrdemServico}
        }

            ' Carregar dados de forma assíncrona
            Dim listaMaterialTask = Task.Run(Function() cl_BancoDados.CarregarDadosNovaAsync(sqlListaMaterial, parametrosListaMaterial))
            Dim listaMaterialSWTask = Task.Run(Function() cl_BancoDados.CarregarDadosNovaAsync(sqlListaMaterialSW, parametrosListaMaterialSW))

            Task.WaitAll(listaMaterialTask, listaMaterialSWTask)

            ' Configurar DataGridView principal
            DGVListaMaterialSW.SuspendLayout()
            DGVListaMaterialSW.DataSource = listaMaterialTask.Result

            ' Configuração de colunas
            'ConfigurarColunasDGV(DGVListaMaterialSW, {"QtdeTotal", "CodMatFabricante", "DescResumo", "DescDetal", "Espessura", "Altura", "Largura", "AreaPintura", "Peso", "Acabamento", "TipoDesenho"})


            Dim colunasVisiveis As String() = {"dgvSelecao", "dgvIconeItemOS", "dgvDXF", "dgvPDF",
        "QtdeTotal", "DescResumo", "DescDetal", "Espessura", "Altura", "Largura",
        "AreaPintura", "Peso", "CodMatFabricante", "Acabamento", "TipoDesenho"}



            ' Lista de colunas invisíveis
            Dim colunasInvisiveis As String() = {
        "IDOrdemServico", "PROJETO", "TAG", "ESTATUS_OrdemServico", "IdMaterial",
        "QtdeProduzida", "QtdeFaltante", "CRIADOPOR", "DATACRIACAO", "ESTATUS",
        "D_E_L_E_T_E", "ORDEMSERVICOITEMFINALIZADO", "IdEmpresa", "idProjeto",
        "IdTag", "Autor", "Palavrachave", "Notas", "AreaPinturaUnitario", "NumeroDobras",
        "PesoUnitario", "Unidade", "UnidadeSW", "ValorSW", "DtCad", "UsuarioCriacao",
        "UsuarioAlteracao", "DtAlteracao", "EnderecoArquivo", "MaterialSW", "Fator",
        "qtde", "Material", "ProdutoPrincipal", "txtItemEstoque", "txtSoldagem",
        "txtTipoDesenho", "txtCorte", "txtDobra", "txtSolda", "txtPintura", "txtMontagem",
        "RNC", "Comprimentocaixadelimitadora", "Larguracaixadelimitadora",
        "Espessuracaixadelimitadora"}

            'Chamar a função para configurar as colunas
            ConfigurarColunasDGV(DGVListaMaterialSW, colunasVisiveis, colunasInvisiveis)


            ' Configurar DataGridView secundário
            DGVListaMaterialSWMaterial.SuspendLayout()
            DGVListaMaterialSWMaterial.DataSource = listaMaterialSWTask.Result
            ' ConfigurarColunasDGV(DGVListaMaterialSWMaterial, {"CodMatFabricante", "DescResumo", "DescDetal", "QtdeTotal", "TipoDesenho"})

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar dados: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LogarErro(ex)
        Finally
            ' Certifique-se de que os layouts foram retomados
            DGVListaMaterialSW.ResumeLayout()
            DGVListaMaterialSWMaterial.ResumeLayout()
        End Try
    End Sub

    Private Sub ConfigurarColunasDGV(dgv As DataGridView, colunasVisiveis As String(), colunasInvisiveis As String())

        '' Definindo as colunas que serão visíveis
        'Dim colunasVisiveis As String() = {"CodMatFabricante", "DescResumo", "QtdeTotal", "AreaPintura"}

        '' Definindo as colunas que serão invisíveis
        'Dim colunasInvisiveis As String() = {"IdMaterial", "IdOrdemServico", "UsuarioAlteracao"}

        '' Chamar a função para configurar as colunas
        'ConfigurarColunasDGV(DGVListaMaterialSW, colunasVisiveis, colunasInvisiveis)
        '
        'Tornar todas as colunas invisíveis inicialmente
        For Each coluna As DataGridViewColumn In dgv.Columns
            coluna.Visible = False
        Next

        ' Configurar as colunas que serão visíveis
        For Each nomeColuna As String In colunasVisiveis
            ' Verifica se a coluna existe no DataGridView
            If dgv.Columns.Contains(nomeColuna) Then
                ' Torna a coluna visível
                dgv.Columns(nomeColuna).Visible = True

                ' Se a coluna for "CodMatFabricante", a congura para ficar "congelada" (fixa)
                If nomeColuna = "CodMatFabricante" Then
                    dgv.Columns(nomeColuna).Frozen = True
                End If
            End If
        Next

        ' Configurar as colunas que serão invisíveis (caso não estejam na lista de visíveis)
        For Each nomeColuna As String In colunasInvisiveis
            ' Verifica se a coluna existe no DataGridView
            If dgv.Columns.Contains(nomeColuna) Then
                ' Torna a coluna invisível
                dgv.Columns(nomeColuna).Visible = False
            End If
        Next

        ' Ajustar a largura das colunas
        For Each coluna As DataGridViewColumn In dgv.Columns
            ' Ajusta a largura da coluna para 351, caso seja maior que 350
            If coluna.Width > 350 Then
                coluna.Width = 351
            End If
        Next
    End Sub


    Private Sub LogarErro(ex As Exception)
        Try
            Dim logPath As String = "C:\Caminho\Para\Seu\Log\log.txt"
            File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}{System.Environment.NewLine}")
        Catch logEx As Exception
            ' Evitar interrupção por erro de log
        End Try
    End Sub


    Private Sub FormatarColunaIconeDGVListaMaterialSW()


        Dim dxf, pdf As String

        For Each row As DataGridViewRow In DGVListaMaterialSW.Rows
            Dim valorEnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()
            Dim valorProdutoPrincipal As String = If(row.Cells("ProdutoPrincipal").Value, "").ToString()

            ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
            If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
           valorProdutoPrincipal.IndexOf("SIM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                row.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define outra imagem se for .SLDPRT
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemPRT
            Else
                row.Cells("dgvIconeItemOS").Value = My.Resources.material_escolar_32
            End If

            ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
            If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                dxf = Path.ChangeExtension(valorEnderecoArquivo, ".dxf")

                ' Verifica se o arquivo DXF existe
                If File.Exists(dxf) Then
                    row.Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                Else
                    row.Cells("DGVDXF").Value = My.Resources.Sem_Incone
                End If
            End If

            ' Altera para .pdf
            If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                pdf = Path.ChangeExtension(valorEnderecoArquivo, ".pdf")

                ' Verifica se o arquivo PDF existe
                If File.Exists(pdf) Then
                    row.Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                Else
                    row.Cells("DGVPDF").Value = My.Resources.Sem_Incone
                End If
            End If


        Next
    End Sub

    Private Sub dgvos_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvos.DataError
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub DGVListaMaterialSW_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DGVListaMaterialSW.DataError
        Try
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub btnNovoOS_Click(sender As Object, e As EventArgs)

        OrdemServico.IDOrdemServico = Nothing
        OrdemServico.PROJETO = Nothing
        OrdemServico.TAG = Nothing
        OrdemServico.DESCRICAO = Nothing
        OrdemServico.ESTATUS = Nothing
        OrdemServico.IDTAG = Nothing
        OrdemServico.IDPROJETO = Nothing
        OrdemServico.DESCEMPRESA = Nothing

        Me.lblOrdemServicoAtiva.Text = ""

        Me.cboProjeto.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboTag.DropDownStyle = ComboBoxStyle.DropDownList

        Me.cboProjeto.Text = ""
        Me.cboTag.Text = ""
        Me.txtCliente.Clear()
        Me.txtDescricaoTag.Clear()
        Me.txtDescricao.Clear()
        Me.cboProjeto.Enabled = True
        Me.cboTag.Enabled = True

        Me.cboProjeto.Focus()

        TimerDGVListaMaterialSW.Enabled = True

    End Sub

    Private Sub btnSalvarOs_Click(sender As Object, e As EventArgs)

        Try
            OrdemServico.DESCRICAO = Me.txtDescricao.Text
            OrdemServico.IDTAG = cboTag.SelectedValue
            OrdemServico.IDPROJETO = cboProjeto.SelectedValue

            OrdemServico.CriarOsCompleta(dgvos, Timerdgvos, TimerDGVListaMaterialSW)

            ' Seleciona a última linha do DataGridView
            If dgvos.Rows.Count > 0 Then
                ' Obtém o índice da última linha
                Dim ultimaLinha As Integer = dgvos.Rows.Count - 1

                ' Define a última linha como selecionada
                dgvos.ClearSelection() ' Limpa seleções anteriores
                dgvos.Rows(ultimaLinha).Selected = True

                ' Faz a rolagem para garantir que a linha esteja visível
                dgvos.FirstDisplayedScrollingRowIndex = ultimaLinha

                dgvos.Rows(ultimaLinha).DefaultCellStyle.BackColor = Color.LightCyan

                TimerDGVListaMaterialSW.Enabled = True

            End If

        Catch ex As Exception
            MsgBox("Erro ao criar OS")
        Finally
        End Try


    End Sub
    ' Função para obter valores da célula e tratar erros
    Private Function ObterValorCelula(row As DataGridViewRow, coluna As String, Optional valorPadrao As Object = "") As Object
        Try
            Dim valor = row.Cells(coluna).Value
            If valor Is Nothing Then
                Return valorPadrao
            Else
                Return valor.ToString().ToUpper()
            End If
        Catch ex As Exception
            Return valorPadrao
        End Try
    End Function

    ' Função para converter de ponto (.) para vírgula (,) e vice-versa
    Private Function ConverterSeparadorDecimal(valor As String, paraVirgula As Boolean) As String
        If String.IsNullOrEmpty(valor) Then Return valor
        If paraVirgula Then
            ' Trocar ponto por vírgula
            Return valor.Replace(".", ",")
        Else
            ' Trocar vírgula por ponto
            Return valor.Replace(",", ".")
        End If
    End Function

    Private Sub btnInserirItensOrdemServico_Click(sender As Object, e As EventArgs) Handles btnInserirItensOrdemServico.Click

        Dim verificaRnc As Boolean = False

        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir os itens da lista BOM na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Itens da OS", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Dim rnc As String

            Dim Fator As Double
            '  Try

            ProgressBarListaSW.Minimum = 0
            ProgressBarListaSW.Maximum = dgvDataGridBOM.Rows.Count

            If dgvDataGridBOM.Rows.Count > 0 Then

                For b As Integer = 0 To dgvDataGridBOM.Rows.Count - 1
                    Try



                        Try

                            rnc = dgvDataGridBOM.Rows(b).Cells("RNC").Value.ToString

                        Catch ex As Exception
                            rnc = ""
                        End Try

                        If rnc = "S" Then

                            MsgBox("Na lista, há peças com RNC pendente; para prosseguir com o processo de liberação, é necessário remover a peça da lista ou resolver a RNC.", vbCritical, "Atenção")

                            rnc = dgvDataGridBOM.Rows(b).DefaultCellStyle.BackColor = Color.LightSalmon

                            verificaRnc = True

                            Exit Sub

                        Else

                            verificaRnc = False


                        End If

                    Catch ex As Exception
                        Continue For
                    End Try

                Next

            End If

            If verificaRnc = False Then

                If dgvDataGridBOM.Rows.Count > 0 Then

                    If OrdemServico.IDOrdemServico = Nothing Or OrdemServico.IDOrdemServico = 0 Then

                        MsgBox("A Ordem de Serviço deve ser selecionada", vbCritical, "Atenção")

                        Exit Sub
                    Else

                        Try
                            Fator = InputBox("Informe o Valor Multiplicado de fabricação", "Fator de Multiplicação", 1)

                            ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                            If Fator = "" Then

                                MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                                Exit Sub ' Sai do procedimento
                            End If

                            ' Verifica se o valor inserido é numérico e maior que 0
                            If Not IsNumeric(Fator) OrElse Convert.ToDouble(Fator) <= 0 Then

                                MsgBox("A operação foi cancelada. O valor informado não é um número válido!", vbInformation, "Atenação")

                                Exit Sub
                            End If
                        Catch ex As Exception
                            ' Fator = "1"
                        Finally
                            ' Código adicional, se necessário
                        End Try


                        For A As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                            Try

                                If dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> "" Or dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> Nothing Then


                                    Try
                                        OrdemServico.EnderecoArquivo = dgvDataGridBOM.Rows(A).Cells("EnderecoArquivo").Value.ToString.ToUpper
                                        'OrdemServico.EnderecoArquivo = Replace(OrdemServico.EnderecoArquivo, "\", "##")
                                        ' OrdemServico.EnderecoArquivo = Replace(OrdemServico.EnderecoArquivo, "/", "#")
                                    Catch ex As Exception

                                        OrdemServico.EnderecoArquivo = ""

                                    End Try


                                    Try
                                        OrdemServico.CodMatFabricante = dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString.ToUpper
                                    Catch ex As Exception
                                        OrdemServico.CodMatFabricante = ""
                                    End Try

                                    Try
                                        OrdemServico.DescResumo = dgvDataGridBOM.Rows(A).Cells("DescResumo").Value.ToString.ToUpper
                                    Catch ex As Exception
                                        OrdemServico.DescResumo = ""
                                    End Try

                                    Try
                                        OrdemServico.DescDetal = dgvDataGridBOM.Rows(A).Cells("DescDetal").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.DescDetal = ""
                                    End Try

                                    Try
                                        OrdemServico.Autor = dgvDataGridBOM.Rows(A).Cells("Autor").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Autor = ""
                                    End Try

                                    Try
                                        OrdemServico.Palavrachave = dgvDataGridBOM.Rows(A).Cells("Palavrachave").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Palavrachave = ""
                                    End Try

                                    Try
                                        OrdemServico.Notas = dgvDataGridBOM.Rows(A).Cells("Notas").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Notas = ""
                                    End Try

                                    Try
                                        OrdemServico.Espessura = dgvDataGridBOM.Rows(A).Cells("Espessura").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.Espessura = ""
                                    End Try

                                    Try
                                        OrdemServico.NumeroDobras = dgvDataGridBOM.Rows(A).Cells("NumeroDobras").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.NumeroDobras = ""
                                    End Try

                                    If dgvDesenhos.Rows(A).Cells("EnderecoArquivo").Value.ToString().IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                                        OrdemServico.Unidade = "CONJ"
                                        OrdemServico.UnidadeSW = "CONJ"
                                    Else

                                        OrdemServico.Unidade = "PC"
                                        OrdemServico.UnidadeSW = "PC"

                                    End If

                                    OrdemServico.ValorSW = ""

                                    Try
                                        OrdemServico.Altura = Replace(dgvDataGridBOM.Rows(A).Cells("Altura").Value.ToString, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Altura = ""
                                    End Try

                                    Try
                                        OrdemServico.Largura = Replace(dgvDataGridBOM.Rows(A).Cells("Largura").Value.ToString, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Largura = ""

                                    End Try

                                    OrdemServico.DtCad = ""
                                    OrdemServico.UsuarioCriacao = ""
                                    OrdemServico.UsuarioAlteracao = ""
                                    OrdemServico.DtAlteracao = ""

                                    Try
                                        OrdemServico.MaterialSw = dgvDataGridBOM.Rows(A).Cells("Material").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.MaterialSw = ""

                                    End Try

                                    Try
                                        OrdemServico.Qtde = dgvDataGridBOM.Rows(A).Cells("Qtde").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.Qtde = 0

                                    End Try

                                    Try
                                        OrdemServico.AreaPintura = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                        OrdemServico.AreaPintura = OrdemServico.AreaPintura * OrdemServico.Qtde * Fator
                                        OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ",", ".")
                                    Catch ex As Exception

                                        OrdemServico.AreaPintura = ""

                                    End Try

                                    Try
                                        OrdemServico.AreaPinturaUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                        OrdemServico.AreaPinturaUnitario = Replace(OrdemServico.AreaPinturaUnitario, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.AreaPinturaUnitario = ""

                                    End Try

                                    Try
                                        OrdemServico.Peso = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")

                                        OrdemServico.Peso = OrdemServico.Peso * OrdemServico.Qtde * Fator
                                        OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.Peso = 0
                                    End Try

                                    Try
                                        OrdemServico.PesoUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")
                                        OrdemServico.PesoUnitario = Replace(OrdemServico.PesoUnitario, ",", ".")
                                    Catch ex As Exception

                                        OrdemServico.PesoUnitario = 0
                                    End Try

                                    Try
                                        OrdemServico.txtSoldagem = dgvDataGridBOM.Rows(A).Cells("txtSoldagem").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtSoldagem = ""

                                    End Try

                                    Try
                                        OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ".", ",")
                                        OrdemServico.QtdeTotal = OrdemServico.Qtde * Fator
                                        OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.QtdeTotal = 0

                                    End Try

                                    Try
                                        OrdemServico.txtTipoDesenho = dgvDataGridBOM.Rows(A).Cells("txtTipoDesenho").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtTipoDesenho = ""

                                    End Try



                                    Try
                                        OrdemServico.txtCorte = dgvDataGridBOM.Rows(A).Cells("txtCorte").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtCorte = ""

                                    End Try

                                    Try
                                        OrdemServico.txtDobra = dgvDataGridBOM.Rows(A).Cells("txtDobra").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtDobra = ""

                                    End Try

                                    Try
                                        OrdemServico.txtSolda = dgvDataGridBOM.Rows(A).Cells("txtSolda").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtSolda = ""

                                    End Try

                                    Try
                                        OrdemServico.txtPintura = dgvDataGridBOM.Rows(A).Cells("txtPintura").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtPintura = ""

                                    End Try

                                    Try
                                        OrdemServico.txtMontagem = dgvDataGridBOM.Rows(A).Cells("txtMontagem").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtMontagem = ""

                                    End Try

                                    Try
                                        OrdemServico.Comprimentocaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Comprimentocaixadelimitadora").Value.ToString
                                        OrdemServico.Comprimentocaixadelimitadora = Replace(OrdemServico.Comprimentocaixadelimitadora, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Comprimentocaixadelimitadora = ""

                                    End Try

                                    Try
                                        OrdemServico.Larguracaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Larguracaixadelimitadora").Value.ToString
                                        OrdemServico.Larguracaixadelimitadora = Replace(OrdemServico.Larguracaixadelimitadora, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Larguracaixadelimitadora = ""

                                    End Try

                                    Try
                                        OrdemServico.Espessuracaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Espessuracaixadelimitadora ").Value.ToString
                                        OrdemServico.Espessuracaixadelimitadora = Replace(OrdemServico.Espessuracaixadelimitadora, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Espessuracaixadelimitadora = ""

                                    End Try

                                    Try
                                        OrdemServico.txtItemEstoque = dgvDataGridBOM.Rows(A).Cells("txtItemEstoque").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.txtItemEstoque = ""

                                    End Try


                                    Try
                                        OrdemServico.txtAcabamento = dgvDataGridBOM.Rows(A).Cells("Acabamento").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.txtAcabamento = ""

                                    End Try


                                    OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ",", "")




                                    ProgressBarListaSW.Value = A

                                    Dim query As String = "INSERT INTO ordemservicoitem (
                            IDOrdemServico, idProjeto, PROJETO, idTag, TAG, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CRIADOPOR, 
                            DATACRIACAO, ESTATUS, ACABAMENTO, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IDOrdemServico, @idProjeto, @PROJETO, @idTag, @TAG, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CRIADOPOR, 
                            @DATACRIACAO, @ESTATUS, @ACABAMENTO, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                                    Using command As New MySqlCommand(query, myconect)
                                        ' Adicionando os parâmetros
                                        command.Parameters.AddWithValue("@IDOrdemServico", OrdemServico.IDOrdemServico)
                                        command.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
                                        command.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
                                        command.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
                                        command.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
                                        command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.ESTATUS)
                                        command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                        command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                        command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                        command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                        command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                                        command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                        command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                        command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                                        command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                        command.Parameters.AddWithValue("@Peso", OrdemServico.Peso)
                                        command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                        command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                        command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                                        command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                        command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                        command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                        command.Parameters.AddWithValue("@DtCad", "")
                                        command.Parameters.AddWithValue("@UsuarioCriacao", "")
                                        command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                        command.Parameters.AddWithValue("@DtAlteracao", "")
                                        command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                        command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSw)
                                        command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                        command.Parameters.AddWithValue("@QtdeProduzida", "")
                                        command.Parameters.AddWithValue("@QtdeFaltante", "")
                                        command.Parameters.AddWithValue("@CRIADOPOR", Usuario.NomeCompleto.ToString)
                                        command.Parameters.AddWithValue("@DATACRIACAO", Date.Now)
                                        command.Parameters.AddWithValue("@ESTATUS", "A")
                                        command.Parameters.AddWithValue("@ACABAMENTO", OrdemServico.txtAcabamento)
                                        command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                        command.Parameters.AddWithValue("@fator", Fator)
                                        command.Parameters.AddWithValue("@qtde", OrdemServico.Qtde)
                                        command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                        command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                        command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                        command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                        command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                        command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                        command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                        command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                                        command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                                        command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                                        command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                                        command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                                        command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                        command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                                        command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                        command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                                        command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                                        command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                                        ' Abrir conexão e executar comando

                                        'If cl_BancoDados.AbrirBanco = False Then
                                        '    cl_BancoDados.AbrirBanco()

                                        'End If
                                        'myconect.Open()
                                        command.ExecuteNonQuery()
                                    End Using

                                End If

                                ProgressBarListaSW.Value = A
                            Catch ex As Exception

                                MsgBox(ex.Message & " ERRO ao ler o arquivo: " & OrdemServico.EnderecoArquivo, MsgBoxStyle.Critical, "Atenção")

                                Continue For

                            End Try

                        Next A

                        ''''''''''''''cl_BancoDados.Salvar(SQL)

                        ''''''''''''''SQL = Nothing

                    End If


                End If

                MessageBox.Show("Operação finalizada com sucesso, os itens foram inseridos na OS!")

                ProgressBarListaSW.Value = 0
                TimerDGVListaMaterialSW.Enabled = True
            Else

                MsgBox("Operação cancelada, os itens não serão inseridos na OS! Existem RNC em aberto, verificar as linhas marcadas", vbCritical, "Atenção")

            End If

        End If

        'Else

        '    MsgBox("Não será possivel criar OS existe peças com RNC em aberto!", MsgBoxStyle.Critical, "Atenção")

        'End If

    End Sub

    Private Sub AbrirPastaDaOrdemDeServiçoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Click
        Try
            ' Verifica se a célula "Endereco" não está vazia ou nula
            If IsNothing(dgvos.CurrentRow.Cells("Endereco").Value) OrElse IsDBNull(dgvos.CurrentRow.Cells("Endereco").Value) Then
                MsgBox("O endereço não foi informado!", vbExclamation, "Atenção")
                Exit Sub
            End If

            ' Obtém o endereço da célula e tenta abrir o Explorer
            OrdemServico.ENDERECOOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString()

            If Not String.IsNullOrWhiteSpace(OrdemServico.ENDERECOOrdemServico) Then
                Process.Start("Explorer", OrdemServico.ENDERECOOrdemServico)
            Else
                MsgBox("O endereço está vazio ou não foi informado corretamente!", vbExclamation, "Atenção")
            End If
        Catch ex As Exception
            ' Exibe uma mensagem de erro detalhada
            MsgBox($"Ocorreu um erro ao tentar abrir o endereço: {ex.Message}", vbCritical, "Erro")
        Finally
            ' Código opcional que pode ser executado mesmo após uma exceção
            ' (e.g., limpar variáveis, liberar recursos)
        End Try

    End Sub

    Private Sub LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Click

        Try
            ' Verifica se há uma linha selecionada no DataGridView
            If dgvos.CurrentRow IsNot Nothing Then
                ' Verifica e obtém o valor da célula "ESTATUS"
                If dgvos.CurrentRow.Cells("ESTATUS") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("ESTATUS").Value IsNot DBNull.Value Then
                    OrdemServico.ESTATUS = dgvos.CurrentRow.Cells("ESTATUS").Value.ToString()
                Else
                    OrdemServico.ESTATUS = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "ENDERECO"
                If dgvos.CurrentRow.Cells("ENDERECO") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("ENDERECO").Value IsNot DBNull.Value Then
                    OrdemServico.ENDERECOOrdemServico = dgvos.CurrentRow.Cells("ENDERECO").Value.ToString()
                Else
                    OrdemServico.ENDERECOOrdemServico = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "LIBERADO_ENGENHARIA"
                If dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value IsNot DBNull.Value Then
                    OrdemServico.LIBERADO_ENGENHARIA = dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value.ToString()
                Else
                    OrdemServico.LIBERADO_ENGENHARIA = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "DESCRICAO"
                If dgvos.CurrentRow.Cells("DESCRICAO") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("DESCRICAO").Value IsNot DBNull.Value Then
                    OrdemServico.DESCRICAO = dgvos.CurrentRow.Cells("DESCRICAO").Value.ToString()
                Else
                    OrdemServico.DESCRICAO = String.Empty ' Valor padrão em caso de ausência
                End If
            Else
                ' Tratar caso não exista uma linha selecionada
                Throw New Exception("Nenhuma linha selecionada no DataGridView.")
            End If


            If OrdemServico.LIBERADO_ENGENHARIA = "S" Then

                MsgBox("OS Já liberada, não e possivel liberar novamente!", vbInformation, "Atenção")

                Exit Sub

            Else

                Dim diretorio As String = OrdemServico.ENDERECOOrdemServico

                LimparDiretorio(diretorio & "\PDF")
                LimparDiretorio(diretorio & "\DXF")
                LimparDiretorio(diretorio & "\DFT")
                LimparDiretorio(diretorio & "\LXDS")

                ImportarLXDSParaOS(DGVListaMaterialSW, "DXF", ProgressBarProcessoLiberacaoOrdemServico)
                ImportarLXDSParaOS(DGVListaMaterialSW, "PDF", ProgressBarProcessoLiberacaoOrdemServico)
                ImportarLXDSParaOS(DGVListaMaterialSW, "DFT", ProgressBarProcessoLiberacaoOrdemServico)
                ImportarLXDSParaOS(DGVListaMaterialSW, "LXDS", ProgressBarProcessoLiberacaoOrdemServico)

                ' Try

                cl_BancoDados.Salvar("Update ordemservico set LIBERADO_ENGENHARIA = 'S', 
                        DATA_LIBERACAO_ENGENHARIA = '" & Date.Now & "' 
                        where IDOrdemServico = '" & OrdemServico.IDOrdemServico & "'")

                cl_BancoDados.Salvar("Update ordemservicoitem set LIBERADO_ENGENHARIA = 'S', 
                        DATA_LIBERACAO_ENGENHARIA = '" & Date.Now & "' 
                        where IDOrdemServico = '" & OrdemServico.IDOrdemServico & "'")
                dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value = "S"
                dgvos.CurrentRow.Cells("DATA_LIBERACAO_ENGENHARIA").Value = Date.Now
                dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.verificado1
                dgvos.Refresh()
                If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                    PadraoMetta.ExportarOrdemServicoPadraoMettaAntigo(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.ENDERECOOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, True, DGVListaMaterialSWMaterial)

                Else

                    TemplatesExcel.ExportarOrdemServicoPadrao(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.ENDERECOOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, DGVListaMaterialSWMaterial)

                End If

                MsgBox("Ordem de Serviço Liberada!", vbInformation, "Atenção")

                'Catch ex As Exception

                '    ' dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.atencao

                'Finally
                'End Try

            End If

        Catch ex As Exception


        Finally

        End Try

        ''''  Email.EnviarEmailComOs01(OrdemServico.ENDERECOOrdemServico, OrdemServico.DESCRICAO, Date.Now)


    End Sub

    Private Sub MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Click

        Try
            OrdemServico.IDOrdemServicoITEM = DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoITEM").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "SIM", "IDOrdemServicoITEM", OrdemServico.IDOrdemServicoITEM)

            DGVListaMaterialSW.CurrentRow.Cells("ProdutoPrincipal").Value = "SIM".ToUpper

            DGVListaMaterialSW.CurrentRow.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal
        Catch ex As Exception

            OrdemServico.IDOrdemServicoITEM = Nothing

            MsgBox("Item da Ordem de Serviço não Valido", vbCritical, "Atenção")

        End Try

    End Sub

    Private Sub DesmarcarComoConjuntoPrincipalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesmarcarComoConjuntoPrincipalToolStripMenuItem.Click

        Try
            OrdemServico.IDOrdemServicoITEM = DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoITEM").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "", "IDOrdemServicoITEM", OrdemServico.IDOrdemServicoITEM)

            DGVListaMaterialSW.CurrentRow.Cells("ProdutoPrincipal").Value = "SIM".ToUpper

            DGVListaMaterialSW.CurrentRow.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW
        Catch ex As Exception

            OrdemServico.IDOrdemServicoITEM = Nothing

            MsgBox("Item da Ordem de Serviço não Valido", vbCritical, "Atenção")

        End Try

    End Sub

    Private Sub DGVListaMaterialSW_DoubleClick(sender As Object, e As EventArgs) Handles DGVListaMaterialSW.DoubleClick

        Try
            ' Abre o documento SolidWorks e aguarda até que ele esteja totalmente carregado
            Dim filePath As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()
            Dim CodMatFabricante As String = DGVListaMaterialSW.CurrentRow.Cells("CodMatFabricante").Value.ToString()

            If File.Exists(filePath) Then

                ' Método que abre o documento no SolidWorks
                OpenDocumentAndWait(filePath, True, swModel)

            End If
        Catch ex As FileNotFoundException
            ' Arquivo não encontrado
            MessageBox.Show("Arquivo não encontrado: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As InvalidOperationException
            ' Operação inválida no SolidWorks
            MessageBox.Show("Operação inválida: " & ex.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            ' Captura de qualquer outro tipo de erro
            MessageBox.Show("Erro inesperado ao abrir o arquivo 3D: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        TimerFiltroPecaAtivaOS.Enabled = True

    End Sub

    Private Function ImportarDXFParaOS(ByVal ObjetoDgv As DataGridView) As Boolean

        ImportarDXFParaOS = False

        Dim Origem, Destino, Prefixo, MaterialSW, QtdeTotal, Espessura As String
        Dim caminhoArquivoDestino As String

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
        Destino = Path.Combine(Destino, "DXF") ' Usar Path.Combine para melhor manipulação de caminhos

        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1 ' Ajustado para evitar erro de índice

            Try

                ' Acessando os valores diretamente da linha da tabela temporária
                Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString()

                ' Substituindo extensões de arquivos .SLDPRT e .SLDASM para .DXF, sem comparação de maiúsculas/minúsculas
                Origem = Replace(Origem, ".SLDPRT", ".DXF")
                Origem = Replace(Origem, ".SLDASM", ".DXF")

                ' Tentar obter MaterialSW, QtdeTotal e Espessura, com tratamento de exceção
                Try
                    MaterialSW = ObjetoDgv.Rows(i).Cells("Material").Value.ToString
                Catch ex As Exception
                    MaterialSW = "Sem Material"
                End Try

                Try
                    QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").Value.ToString
                Catch ex As Exception
                    QtdeTotal = "Sem Quantidade"
                End Try

                Try
                    Espessura = ObjetoDgv.Rows(i).Cells("Espessura").Value.ToString
                Catch ex As Exception
                    Espessura = "Sem Espessura"
                End Try

                Prefixo = Espessura & " - " & MaterialSW & " - " & QtdeTotal & " - "

                ' Verifica se o arquivo de origem existe
                If File.Exists(Origem) Then
                    Try
                        ' Obtém o nome do arquivo sem a extensão
                        Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)
                        ' Obtém a extensão do arquivo
                        Dim extensaoArquivo As String = Path.GetExtension(Origem)

                        Dim novoNomeArquivo As String

                        ' Verifica qual formato de exportação foi selecionado
                        If My.Settings.ParametroExportarDXF = "1" Then
                            novoNomeArquivo = $"{Espessura} - {MaterialSW} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                        ElseIf My.Settings.ParametroExportarDXF = "2" Then
                            novoNomeArquivo = $"{QtdeTotal} - {nomeArquivoSemExtensao} - {MaterialSW} - {Espessura}{extensaoArquivo}"
                        Else
                            MessageBox.Show("Nenhuma opção de exportação de DXF selecionada. Vá nas configurações e selecione a opção desejada!")
                            Exit For ' Saída antecipada se não houver configuração válida
                        End If


                        ' Verificar se o item é de estoque
                        If ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "NÃO" OrElse
                            ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "" OrElse
                             ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = Nothing Then
                            ' Caminho para não itens de estoque
                            caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)
                        ElseIf ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "SIM" Then
                            ' Caminho para itens de estoque
                            caminhoArquivoDestino = Replace(Destino, "\DXF", "\PEÇAS DE ESTOQUE")
                            caminhoArquivoDestino = Path.Combine(caminhoArquivoDestino, novoNomeArquivo)
                        End If

                        ' Copia o arquivo para a pasta de destino com o novo nome
                        File.Copy(Origem, caminhoArquivoDestino, True)
                    Catch ex As Exception
                    Finally
                        ' MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ' Else
                    '    MessageBox.Show($"Arquivo de origem não encontrado: {Origem}", "Arquivo Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ImportarDXFParaOS = True

                End If
            Catch ex As Exception
                Continue For

            End Try

        Next

    End Function

    Private Function ImportarPDFParaOS(ByVal ObjetoDgv As DataGridView) As Boolean

        ImportarPDFParaOS = False

        Dim Origem, Destino, Prefixo, QtdeTotal, NumeroOS As String
        Dim caminhoArquivoDestino, PastaDestino As String

        'Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
        'Destino = Path.Combine(Destino, "PDF") ' Usar Path.Combine para melhor manipulação de caminhos

        ' Obtém o caminho do arquivo
        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString().ToUpper().Trim()

        ' Altera a extensão do arquivo para .pdf
        Destino = Path.ChangeExtension(Destino, "\PDF")

        caminhoArquivoDestino = dgvos.CurrentRow.Cells("Endereco").Value & "\PDF"

        NumeroOS = dgvos.CurrentRow.Cells("IDOrdemServico").Value.ToString().ToUpper().Trim()

        ' PastaDestino = dgvos.CurrentRow.Cells("Endereco").Value & "\PDF"


        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1 ' Ajustado para evitar erro de índice

            Try

                ' Acessando os valores diretamente da linha
                Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString().ToUpper().Trim()


                Origem = Replace(Origem, ".SLDPRT", ".PDF", , , CompareMethod.Text)
                Origem = Replace(Origem, ".SLDASM", ".PDF", , , CompareMethod.Text)

                ' Tentar obter QtdeTotal com tratamento de exceção
                Try
                    QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").Value.ToString()

                Catch ex As Exception

                    QtdeTotal = "Sem Quantidade"

                End Try

                Prefixo = "OS - " & NumeroOS & " - " & QtdeTotal & " - "

                ' Verifica se o arquivo de origem existe
                If File.Exists(Origem) Then
                    Try
                        ' Obtém o nome do arquivo sem a extensão
                        Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                        ' Obtém a extensão do arquivo
                        Dim extensaoArquivo As String = Path.GetExtension(Origem)

                        ' Constrói o novo nome do arquivo com o sufixo
                        Dim novoNomeArquivo As String = $"{Prefixo}{nomeArquivoSemExtensao}{extensaoArquivo}"

                        ' Verificar se o item é de estoque
                        If ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "NÃO" OrElse
                            ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "" OrElse
                             ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = Nothing Then
                            ' Caminho para não itens de estoque
                            caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)
                        ElseIf ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "SIM" Then
                            ' Caminho para itens de estoque
                            caminhoArquivoDestino = Replace(Destino, "\PDF", "\PEÇAS DE ESTOQUE")
                            caminhoArquivoDestino = Path.Combine(caminhoArquivoDestino, novoNomeArquivo)
                        End If

                        ' Copia o arquivo para a pasta de destino com o novo nome
                        File.Copy(Origem, caminhoArquivoDestino, True)

                    Catch ex As Exception
                    Finally

                        ' MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End If
            Catch ex As Exception

                Continue For

            End Try

        Next

    End Function

    Private Function ImportarPDFParaOSIndividual(ByVal Endereco As String, ByVal Qtde As String) As Boolean

        ImportarPDFParaOSIndividual = False

        Dim Origem, Destino, Prefixo, QtdeTotal, OS, TextoPdf As String
        Dim caminhoArquivoDestino As String

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
        Destino = Path.Combine(Destino, "PDF") ' Usar Path.Combine para melhor manipulação de caminhos

        OS = dgvos.CurrentRow.Cells("IDOrdemServico").Value.ToString()

        Try

            ' Acessando os valores diretamente da linha
            Origem = Endereco

            Origem = Replace(Origem, ".SLDPRT", ".PDF", , , CompareMethod.Text)
            Origem = Replace(Origem, ".SLDASM", ".PDF", , , CompareMethod.Text)

            ' Tentar obter QtdeTotal com tratamento de exceção
            Try
                QtdeTotal = Qtde
            Catch ex As Exception
                QtdeTotal = "Sem Quantidade"
            End Try

            Prefixo = $"{QtdeTotal} - "
            TextoPdf = "OS: " & OS & " Qtde: " & Qtde


            '  pdfsinco.EscreverPdf(Origem, Destino, TextoPdf)

            ' Verifica se o arquivo de origem existe
            If File.Exists(Origem) Then
                Try
                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                    ' Obtém a extensão do arquivo
                    Dim extensaoArquivo As String = Path.GetExtension(Origem)

                    ' Constrói o novo nome do arquivo com o sufixo
                    Dim novoNomeArquivo As String = $"{Prefixo}{nomeArquivoSemExtensao}{extensaoArquivo}"

                    caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)

                    ' Copia o arquivo para a pasta de destino com o novo nome
                    File.Copy(Origem, caminhoArquivoDestino, True)
                Catch ex As Exception
                Finally

                    ' MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                'Else
                '    MessageBox.Show($"O arquivo de origem não existe: {Origem}", "Arquivo Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End If
        Catch ex As Exception

        Finally


        End Try



    End Function

    Private Function ImportarDFTParaOS(ByVal ObjetoDgv As DataGridView) As Boolean

        Dim Origem, Destino, Prefixo, MaterialSW, QtdeTotal, Espessura As String
        Dim caminhoArquivoDestino As String

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString
        Destino = Path.Combine(Destino, "DFT")

        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1

            ' Acessando os valores diretamente da linha
            Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").ToString
            Origem = Replace(Origem, ".SLDPRT", ".DFT", , , CompareMethod.Text)
            Origem = Replace(Origem, ".SLDASM", ".DFT", , , CompareMethod.Text)

            Try
                MaterialSW = ObjetoDgv.Rows(i).Cells("Material").ToString
            Catch ex As Exception
                MaterialSW = "Sem Material"
            End Try

            Try
                QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").ToString
            Catch ex As Exception
                QtdeTotal = "Sem Quantidade"
            End Try

            Try
                Espessura = ObjetoDgv.Rows(i).Cells("Espessura").ToString
            Catch ex As Exception
                Espessura = "Sem Espessura"
            End Try

            Prefixo = Espessura & " - " & MaterialSW & " - " & QtdeTotal & " - "

            ' Verifica se o arquivo de origem existe
            If File.Exists(Origem) Then
                Try
                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                    ' Obtém a extensão do arquivo
                    Dim extensaoArquivo As String = Path.GetExtension(Origem)

                    Dim novoNomeArquivo As String

                    ' Verifica qual formato de exportação foi selecionado
                    If My.Settings.ParametroExportarDXF = "1" Then
                        novoNomeArquivo = $"{Espessura} - {MaterialSW} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                    ElseIf My.Settings.ParametroExportarDXF = "2" Then
                        novoNomeArquivo = $"{QtdeTotal} - {nomeArquivoSemExtensao} - {MaterialSW} - {Espessura}{extensaoArquivo}"
                    Else
                        MessageBox.Show("Nenhuma opção de exportação de DFT selecionada. Vá nas configurações e selecione a opção desejada!")
                        Exit For ' Saída antecipada se não houver configuração válida
                    End If

                    ' Verificar se o item é de estoque
                    If ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = "NÃO" OrElse
                            ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = "" OrElse
                             ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = Nothing Then
                        ' Caminho para não itens de estoque
                        caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)
                    ElseIf ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = "SIM" Then
                        ' Caminho para itens de estoque
                        caminhoArquivoDestino = Replace(Destino, "\DFT", "\PEÇAS DE ESTOQUE")
                        caminhoArquivoDestino = Path.Combine(caminhoArquivoDestino, novoNomeArquivo)
                    End If

                    ' Copia o arquivo para a pasta de destino com o novo nome
                    File.Copy(Origem, caminhoArquivoDestino, True)

                    ' MessageBox.Show("Arquivo copiado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception

                End Try

            End If

        Next

    End Function

    Private Function ImportarLXDSParaOS(ByVal ObjetoDgv As DataGridView, ByVal Pasta As String, ByVal BarraProgresso As ProgressBar)

        Dim Origem, Destino, Prefixo, MaterialSW, QtdeTotal, Espessura, tipoDesenho As String
        Dim caminhoArquivoDestino As String


        BarraProgresso.Minimum = 0
        BarraProgresso.Maximum = ObjetoDgv.RowCount

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString
        Destino = Path.Combine(Destino, Pasta)

        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1




            ' Acessando os valores diretamente da linha
            Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString
            Origem = Replace(Origem, ".SLDPRT", "." & Pasta, , , CompareMethod.Text)
            Origem = Replace(Origem, ".SLDASM", "." & Pasta, , , CompareMethod.Text)

            Try
                MaterialSW = ObjetoDgv.Rows(i).Cells("MaterialSW").Value.ToString
            Catch ex As Exception
                MaterialSW = "Sem Material"
            End Try

            Try
                QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").Value.ToString
            Catch ex As Exception
                QtdeTotal = "Sem Quantidade"
            End Try

            Try
                Espessura = ObjetoDgv.Rows(i).Cells("Espessura").Value.ToString
            Catch ex As Exception
                Espessura = "Sem Espessura"
            End Try

            Try
                tipoDesenho = ObjetoDgv.Rows(i).Cells("txtTipoDesenho").Value.ToString
            Catch ex As Exception
                tipoDesenho = "Sem Tipo Desenho"
            End Try



            Prefixo = Espessura & " - " & MaterialSW & " - " & QtdeTotal & " - "

            ' Verifica se o arquivo de origem existe
            If File.Exists(Origem) Then
                Try
                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                    ' Obtém a extensão do arquivo
                    Dim extensaoArquivo As String = Path.GetExtension(Origem)

                    Dim novoNomeArquivo As String

                    ' Verifica qual formato de exportação foi selecionado
                    If My.Settings.ParametroExportarDXF = "1" Then
                        novoNomeArquivo = $"{Espessura} - {MaterialSW} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                    ElseIf My.Settings.ParametroExportarDXF = "2" Then
                        novoNomeArquivo = $"{QtdeTotal} - {nomeArquivoSemExtensao} - {MaterialSW} - {Espessura}{extensaoArquivo}"
                    Else
                        MessageBox.Show("Nenhuma opção de exportação de LXDS selecionada. Vá nas configurações e selecione a opção desejada!")
                        Exit For ' Saída antecipada se não houver configuração válida
                    End If

                    '  Dim pdfFilePath As String = Path.ChangeExtension(ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString, ".pdf")

                    If ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        novoNomeArquivo = $"{tipoDesenho} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                    End If

                    '''''''' Verificar se o item é de estoque
                    '''''''If ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "NÃO" OrElse
                    '''''''        ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "" OrElse
                    '''''''         ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = Nothing Then
                    '''''''    ' Caminho para não itens de estoque
                    caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)
                    '''''''ElseIf ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = "SIM" Then
                    '''''''    ' Caminho para itens de estoque
                    '''''''    caminhoArquivoDestino = Replace(Destino, "\" & Pasta, "\PEÇAS DE ESTOQUE")
                    '''''''    caminhoArquivoDestino = Path.Combine(caminhoArquivoDestino, novoNomeArquivo)
                    '''''''End If

                    ' Copia o arquivo para a pasta de destino com o novo nome
                    ' If Pasta <> "PDF" Then

                    File.Copy(Origem, caminhoArquivoDestino, True)

                    'Else

                    '    pdfsinco.EscreverPdf(Origem, caminhoArquivoDestino, "Teste")

                    'End If

                    'pdfsinco.EditarPdf(caminhoArquivoDestino, "teste")


                    ' MessageBox.Show("Arquivo copiado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception

                    Continue For

                End Try



            End If

            BarraProgresso.Value = i
            Origem = ""
            Prefixo = ""
            MaterialSW = ""
            QtdeTotal = ""
            Espessura = ""
            tipoDesenho = ""


        Next

        BarraProgresso.Value = 0

    End Function

    Private Function ConsolidarMateriais(ByVal dgv As DataGridView) As System.Data.DataTable
        ' Cria um novo DataTable para armazenar os dados consolidados

        dtConsolidado.Columns.Clear()
        dtConsolidado.Rows.Clear()

        dtConsolidado.Columns.Add("IdMaterial")
        dtConsolidado.Columns.Add("QtdeTotal") ' Ou use Decimal se for um valor decimal
        dtConsolidado.Columns.Add("EnderecoArquivo") ' Adicione outras colunas conforme necessário
        dtConsolidado.Columns.Add("Material")
        dtConsolidado.Columns.Add("espessura")
        dtConsolidado.Columns.Add("txtItemEstoque")

        ' Dicionário para armazenar a soma de QtdeTotal por IdMaterial
        Dim materialDictionary As New Dictionary(Of String, DataRow)()

        ' Itera pelas linhas do DataGridView
        For Each row As DataGridViewRow In dgv.Rows
            If Not row.IsNewRow Then
                Dim idMaterial As String = row.Cells("IdMaterial").Value.ToString()
                Dim qtdeTotal As Double = Convert.ToInt32(row.Cells("QtdeTotal").Value)
                Dim EnderecoArquivo As String = row.Cells("EnderecoArquivo").Value.ToString()
                Dim Material As String = row.Cells("Material").Value.ToString()
                Dim espessura As String = row.Cells("espessura").Value.ToString()
                Dim txtItemEstoque As String = row.Cells("txtItemEstoque").Value.ToString()

                If materialDictionary.ContainsKey(idMaterial) Then
                    ' Se o material já existe, soma a quantidade
                    materialDictionary(idMaterial)("QtdeTotal") += qtdeTotal
                Else
                    ' Se não existe, cria uma nova linha no DataTable
                    Dim newRow As DataRow = dtConsolidado.NewRow()
                    newRow("IdMaterial") = idMaterial
                    newRow("QtdeTotal") = qtdeTotal
                    newRow("EnderecoArquivo") = EnderecoArquivo ' Adicione outros campos conforme necessário
                    newRow("Material") = Material
                    newRow("espessura") = espessura
                    newRow("txtItemEstoque") = txtItemEstoque

                    dtConsolidado.Rows.Add(newRow)
                    materialDictionary.Add(idMaterial, newRow)

                End If
            End If

        Next

        Return dtConsolidado

    End Function

    Private Sub btnAplicarAcabamento_Click(sender As Object, e As EventArgs) Handles btnAplicarAcabamento.Click

        If DGVListaMaterialSW.Rows.Count > 0 Then

            For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                If Convert.ToBoolean(DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value) = True Then

                    Try

                        cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ACABAMENTO", cboOpcoesAcabamento.Text.ToUpper.Trim, "IDOrdemServicoITEM", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoITEM").Value.ToString)

                        DGVListaMaterialSW.Rows(i).Cells("Acabamento").Value = cboOpcoesAcabamento.Text.ToUpper.Trim

                        DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False
                    Catch ex As Exception
                        '  MsgBox(ex.Message)
                    Finally
                    End Try
                End If

            Next
        End If

    End Sub

    Private Sub MarcarTodosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarcarTodosToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True

            End If

        Next

    End Sub

    Private Sub DesmarcarTodosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesmarcarTodosToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

            End If

        Next

    End Sub

    Private Sub InverterSeleçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InverterSeleçãoToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True

            ElseIf DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

            End If

        Next

    End Sub

    Private Sub DGVListaMaterialSW_Click(sender As Object, e As EventArgs) Handles DGVListaMaterialSW.Click
        Try

            If DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = False Then

                DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = True

            ElseIf DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = True Then

                DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = False

            End If
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub txtPesqNumeroDesenho_TextChanged(sender As Object, e As EventArgs) Handles txtPesqNumeroDesenho.TextChanged
        TimerDGVListaMaterialSW.Enabled = True
    End Sub

    Private Sub txtPesqTipoDesenho_TextChanged(sender As Object, e As EventArgs) Handles txtPesqTipoDesenho.TextChanged
        TimerDGVListaMaterialSW.Enabled = True
    End Sub

    Private Sub txtPesqAcabamentoDesenho_TextChanged(sender As Object, e As EventArgs) Handles txtPesqAcabamentoDesenho.TextChanged
        TimerDGVListaMaterialSW.Enabled = True
    End Sub

    Private Sub AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            If OrdemServico.LIBERADO_ENGENHARIA <> "" Then

                MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")

                Exit Sub
            Else

                Dim novaqtde As Double

                novaqtde = InputBox("Informe a Nova Quantidade", "Alteração de Quantidade", DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value.ToString)

                If IsNumeric(novaqtde) Then

                    Dim Peso, areapintura As String

                    Peso = (DGVListaMaterialSW.CurrentRow.Cells("Peso").Value / DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value) * novaqtde

                    areapintura = (DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Value / DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value) * novaqtde

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoITEM", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoITEM").Value.ToString)
                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Qtde", novaqtde, "IDOrdemServicoITEM", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoITEM").Value.ToString)

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", areapintura, "IDOrdemServicoITEM", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoITEM").Value.ToString)

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Peso, "IDOrdemServicoITEM", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoITEM").Value.ToString)

                    DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value = novaqtde
                    DGVListaMaterialSW.CurrentRow.Cells("Qtde").Value = novaqtde

                    DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Value = areapintura

                    DGVListaMaterialSW.CurrentRow.Cells("Peso").Value = Peso

                    DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                    DGVListaMaterialSW.CurrentRow.Cells("Qtde").Style.BackColor = Color.LightGreen

                    DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Style.BackColor = Color.LightGreen

                    DGVListaMaterialSW.CurrentRow.Cells("Peso").Style.BackColor = Color.LightGreen


                Else

                    MsgBox("O valor informado não e um numero Valido")

                End If

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ImprimirDesenhoPDFSelecionadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                Try

                    Dim ArquivoPdf As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

                    ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
                    ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

                    If File.Exists(ArquivoPdf) Then

                        Impressora.Imprimirarquivo(ArquivoPdf)

                    End If

                    DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False
                Catch ex As Exception

                    DGVListaMaterialSW.Rows(i).DefaultCellStyle.BackColor = Color.LightPink
                    DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False
                Finally

                End Try

            End If

        Next

    End Sub

    Private Sub AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Click


        Dim resultado As MsgBoxResult = MessageBox.Show("Deseja Atualizar os arquivos da OS, esta ação ira apagar todos os arquivo da pasta da Ordem de Serviço " & OrdemServico.IDOrdemServico, "Atualização", MessageBoxButtons.YesNo)

        If resultado = DialogResult.Yes Then


            ' Verifica e obtém o valor da célula "LIBERADO_ENGENHARIA"
            If dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value IsNot DBNull.Value Then
                OrdemServico.LIBERADO_ENGENHARIA = dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value.ToString()
            Else
                OrdemServico.LIBERADO_ENGENHARIA = String.Empty ' Valor padrão em caso de ausência
            End If



            If OrdemServico.LIBERADO_ENGENHARIA = "S" Then

                MsgBox("OS Já liberada, não e possivel liberar novamente!", vbInformation, "Atenção")

                Exit Sub

            Else


                Try

                    If DGVListaMaterialSW.Rows.Count > 0 Then

                        Dim diretorio As String = OrdemServico.ENDERECOOrdemServico

                        LimparDiretorio(diretorio & "\PDF")
                        LimparDiretorio(diretorio & "\DXF")
                        LimparDiretorio(diretorio & "\DFT")
                        LimparDiretorio(diretorio & "\LXDS")

                        ImportarLXDSParaOS(DGVListaMaterialSW, "DXF", ProgressBarProcessoLiberacaoOrdemServico)
                        ImportarLXDSParaOS(DGVListaMaterialSW, "PDF", ProgressBarProcessoLiberacaoOrdemServico)
                        ImportarLXDSParaOS(DGVListaMaterialSW, "DFT", ProgressBarProcessoLiberacaoOrdemServico)
                        ImportarLXDSParaOS(DGVListaMaterialSW, "LXDS", ProgressBarProcessoLiberacaoOrdemServico)

                        MsgBox("Documentos enviado com sucesso!", vbInformation, "Atenção")

                    Else

                        MsgBox("Não ha dados a serem atualizado", vbCritical, "Atenção!")

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message & " ERRO: Transferencia de arquivo - Atualização de documento da OS")
                Finally

                End Try

                'Me.lblOrdemServicoAtiva.Text = ""

                'TimerDGVListaMaterialSW.Enabled = True
                'TimerFiltroPecaAtivaOS.Enabled = True

            End If

        Else

            MsgBox("Operação Cancelada", vbCritical, "Atenção")

        End If




    End Sub

    Private Sub TimerFiltroPecaAtivaOS_Tick(sender As Object, e As EventArgs) Handles TimerFiltroPecaAtivaOS.Tick

        DGVTimerFiltroPecaAtivaOS.DataSource = cl_BancoDados.CarregarDados("SELECT IdOrdemServico, 
IdOrdemServicoItem,
Projeto, TAg, idMaterial,
CodMatFabricante, DescResumo, DescDetal,
UPPER(EnderecoArquivo) AS EnderecoArquivo
FROM ordemservicoitem
where (estatus = 'A') AND (D_E_L_E_T_E <> '*' OR D_E_L_E_T_E IS NULL)
AND (ORDEMSERVICOITEMFINALIZADO = '' OR ORDEMSERVICOITEMFINALIZADO IS NULL)
And  CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

        lblNUmeroDocumentoAtivo.Text = DadosArquivoCorrente.NomeArquivoSemExtensao

        For Each col As DataGridViewColumn In DGVTimerFiltroPecaAtivaOS.Columns
            If col.Width > 350 Then
                col.Width = 351
            End If
        Next

        TimerFiltroPecaAtivaOS.Enabled = False

    End Sub

    Private Sub DGVTimerFiltroPecaAtivaOS_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGVTimerFiltroPecaAtivaOS.DataBindingComplete

        For i As Integer = 0 To DGVTimerFiltroPecaAtivaOS.Rows.Count - 1

            Dim valorEnderecoArquivo As String = If(DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("EnderecoArquivo").Value, "").ToString()

            ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
            If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvTipoDesenhoAtualizacaoItemOs").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define outra imagem se for .SLDPRT
                DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvTipoDesenhoAtualizacaoItemOs").Value = My.Resources.IcopneMontagemPRT

            End If

        Next

    End Sub

    Private Sub DGVTimerFiltroPecaAtivaOS_Click(sender As Object, e As EventArgs) Handles DGVTimerFiltroPecaAtivaOS.Click

        If DGVTimerFiltroPecaAtivaOS.CurrentRow.Cells("dgvSelecaoAtualizacaoItemOs").Value = False Then
            DGVTimerFiltroPecaAtivaOS.CurrentRow.Cells("dgvSelecaoAtualizacaoItemOs").Value = True
        Else
            DGVTimerFiltroPecaAtivaOS.CurrentRow.Cells("dgvSelecaoAtualizacaoItemOs").Value = False

        End If

    End Sub

    Private Sub btnAtualizarDadosItemOs_Click(sender As Object, e As EventArgs) Handles btnAtualizarDadosItemOs.Click

        If DGVTimerFiltroPecaAtivaOS.Rows.Count > 0 Then

            Dim result As DialogResult = MessageBox.Show("Deseja Realmente atualizar os itens selecionado das OS'?", "Atualização os Itens da OS", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then

                AtualisarItensOrdemServico()

                TimerDGVListaMaterialSW.Enabled = True

            Else

                MsgBox("A atualização será cancelda!", vbCritical, "Atenção")

            End If

        End If

    End Sub

    Private Sub GeralExcelDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeralExcelDaOSToolStripMenuItem.Click

        Try

            If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                PadraoMetta.ExportarOrdemServicoPadraoMettaAntigo(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.ENDERECOOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, True, DGVListaMaterialSWMaterial)
            Else

                TemplatesExcel.ExportarOrdemServicoPadrao(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.ENDERECOOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, DGVListaMaterialSWMaterial)

            End If
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub ConfiguraçãoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfiguraçãoToolStripMenuItem1.Click

        If InputBox("Senha de acesso", "Administrador", "") = "99678982" Then
            Dim OpenfileConfiguracao As New OpenFileDialog

            ' Configura o diálogo para aceitar somente arquivos de texto
            OpenfileConfiguracao.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*"
            OpenfileConfiguracao.FilterIndex = 1

            ' Exibe o diálogo e verifica se o usuário selecionou um arquivo
            If OpenfileConfiguracao.ShowDialog() = DialogResult.OK Then
                Dim caminhoArquivo As String = OpenfileConfiguracao.FileName

                ' Verifica se o arquivo existe
                If File.Exists(caminhoArquivo) Then
                    Try
                        ' Variáveis para armazenar os parâmetros
                        Dim endereco, usuario, banco, senha As String
                        Dim EnderecoPastaRaizOS, EnderecoTemplateExcel, CopiaBancoDados As String
                        Dim EnderecoPastaRaizRomaneio, EnderecoTemplateExcelRomaneio, ParametroExportarDXF As String

                        ' Codificação utilizada na leitura do arquivo
                        Dim codificacao As Encoding = Encoding.GetEncoding("ISO-8859-1") ' Ajustável conforme o arquivo

                        ' Lê o arquivo linha por linha
                        Dim parametrosEncontrados As New Dictionary(Of String, String)
                        Using leitor As New StreamReader(caminhoArquivo, codificacao)
                            While Not leitor.EndOfStream
                                Dim linha As String = leitor.ReadLine()?.Trim()

                                ' Ignorar linhas em branco ou mal formadas
                                If String.IsNullOrEmpty(linha) OrElse Not linha.Contains(";") Then Continue While

                                Dim partes = linha.Split(";"c)
                                If partes.Length = 2 Then
                                    Dim chave = partes(0).Trim()
                                    Dim valor = partes(1).Trim()

                                    ' Armazena no dicionário
                                    If Not parametrosEncontrados.ContainsKey(chave) Then
                                        parametrosEncontrados(chave) = valor
                                    End If
                                End If
                            End While
                        End Using

                        ' Atribui valores ao My.Settings
                        Dim parametrosNecessarios = {"endereco", "Usuario", "Banco", "Senha", "EnderecoPastaRaizOS", "EnderecoTemplateExcelOrdemServico", "ParametroExportarDXF"}
                        For Each param In parametrosNecessarios
                            If Not parametrosEncontrados.ContainsKey(param) Then
                                MsgBox($"Erro: Parâmetro '{param}' não encontrado no arquivo de configuração!", MsgBoxStyle.Critical)
                                Return
                            End If
                        Next

                        My.Settings.MySqlBancoDados = parametrosEncontrados("Banco")
                        My.Settings.MysqlEndereco = parametrosEncontrados("endereco")
                        My.Settings.MysqlUsuario = parametrosEncontrados("Usuario")
                        My.Settings.MysqlSenha = parametrosEncontrados("Senha")
                        My.Settings.BancoDadosAtivo = parametrosEncontrados("Banco")

                        My.Settings.EnderecoPastaRaizOS = parametrosEncontrados("EnderecoPastaRaizOS")
                        My.Settings.EnderecoTemplateExcel = parametrosEncontrados("EnderecoTemplateExcelOrdemServico")
                        My.Settings.ParametroExportarDXF = parametrosEncontrados("ParametroExportarDXF")

                        ' Salva as configurações
                        My.Settings.Save()

                        MsgBox("Configurações carregadas com sucesso!", MsgBoxStyle.Information)

                    Catch ex As Exception
                        MsgBox($"Erro ao processar o arquivo de configuração: {ex.Message}", MsgBoxStyle.Critical)
                    End Try
                Else
                    MsgBox("Arquivo selecionado não encontrado!", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox("Senha inválida!", MsgBoxStyle.Exclamation)
        End If



    End Sub

    Private Sub TrocarFormatoA3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrocarFormatoA3ToolStripMenuItem.Click

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                ' Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                '  Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                '  Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPapersUserDefined, swDwgTemplates_e.swDwgTemplateCustom, 0, 0, True, My.Settings.EnderecoNovoFormatoA3.ToString, 0.385, 0.277, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If

    End Sub

    Private Sub DGVMontaPeca_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DGVMontaPeca.DataError
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub txtAuthor_DoubleClick(sender As Object, e As EventArgs) Handles txtAuthor.DoubleClick
        Try

            Me.txtAuthor.Text = Usuario.Sigla.ToString
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Click

        If OrdemServico.IDOrdemServico = Nothing Then

            MsgBox("Não há Ordem de Serviço selecionada", vbCritical, "Atenção")
        Else

            If dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value.ToString <> "S" Then

                OrdemServico.ENDERECOOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString

                Dim resultado As MsgBoxResult = MessageBox.Show("Deseja limpar a Ordem de Serviço, esta ação ira apagar todos os arquivo da pasta da Ordem de Serviço " & OrdemServico.IDOrdemServico, "Exclusão", MessageBoxButtons.YesNo)

                If resultado = DialogResult.Yes Then

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IDOrdemServico", OrdemServico.IDOrdemServico)

                    Dim diretorio As String = OrdemServico.ENDERECOOrdemServico

                    LimparDiretorio(diretorio)

                    Me.lblOrdemServicoAtiva.Text = ""

                    TimerDGVListaMaterialSW.Enabled = True
                    TimerFiltroPecaAtivaOS.Enabled = True
                Else

                    MsgBox("Operação Cancelada", vbCritical, "Atenção")

                End If
            Else

                MsgBox("Esta operação não e valida a OS: " & OrdemServico.ENDERECOOrdemServico & ", já foi liberada anteriormente!", vbInformation, "Atenção")

            End If

        End If

    End Sub

    Sub LimparDiretorio(ByVal diretorio As String)
        ' Verifica se o diretório existe
        If Directory.Exists(diretorio) Then
            ' Limpa todos os arquivos no diretório
            For Each arquivo As String In Directory.GetFiles(diretorio)
                File.Delete(arquivo)
            Next

            ' Limpa todos os subdiretórios, exceto aqueles chamados "PROJETO"
            For Each subdiretorio As String In Directory.GetDirectories(diretorio)
                ' Verifica se o nome do subdiretório é "PROJETO"
                If Path.GetFileName(subdiretorio).Equals("DXF", StringComparison.OrdinalIgnoreCase) Or
                        Path.GetFileName(subdiretorio).Equals("PDF", StringComparison.OrdinalIgnoreCase) Or
                        Path.GetFileName(subdiretorio).Equals("DFT", StringComparison.OrdinalIgnoreCase) Or
                        Path.GetFileName(subdiretorio).Equals("PUNC", StringComparison.OrdinalIgnoreCase) Or
                        Path.GetFileName(subdiretorio).Equals("LASER", StringComparison.OrdinalIgnoreCase) Then
                    LimparDiretorio(subdiretorio)

                End If
            Next
        End If

    End Sub

    Private Sub BuscarFormatoA3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarFormatoA3ToolStripMenuItem.Click

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A3 (*.slddrt)|*.slddrt"
        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA3 = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If

    End Sub

    Private Sub BUSCRAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BUSCRAToolStripMenuItem.Click

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A4 (*.slddrt)|*.slddrt"

        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA4 = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If



    End Sub

    Private Sub TrocarForrmatoA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrocarForrmatoA4ToolStripMenuItem.Click

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                '  Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                ' Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                ' Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPaperA4size, swDwgTemplates_e.swDwgTemplateCustom, 0.21, 0.297, True, My.Settings.EnderecoNovoFormatoA4.ToString, 0.21, 0.297, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception

            Finally

            End Try

        End If

    End Sub

    Private Sub txtPesqCriadoPor_DoubleClick(sender As Object, e As EventArgs) Handles txtPesqCriadoPor.DoubleClick

        Try

            Me.txtPesqCriadoPor.Text = Usuario.NomeCompleto

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Private Sub txtPesqCriadoPor_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCriadoPor.TextChanged

        Timerdgvos.Enabled = True

    End Sub

    Private Sub ConfExportarArquivoParaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfExportarArquivoParaOSToolStripMenuItem.Click

        ExportarParaOS.ShowDialog()

    End Sub

    Private Sub CancelarAFabricaçãoDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelarAFabricaçãoDaOSToolStripMenuItem.Click

        If OrdemServico.IDOrdemServico.ToString = Nothing Or OrdemServico.IDOrdemServico.ToString = "" Then

            MsgBox("Não há OS Selecionada!", vbCritical, "Atenção")

            Exit Sub

        End If

        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Excluir/Cancelar a Ordem de Serviço: " & OrdemServico.IDOrdemServico, "Cancelando Ordem de Serviço", MessageBoxButtons.YesNo)

        Dim totalExecutado As Integer

        Try

            totalExecutado = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT  count(idplanodecorte) + 
                   count(CorteTotalExecutado) + count(DobraTotalExecutado)+ count(SoldaTotalExecutado) +
                   count(PinturaTotalExecutado) +  count(MontagemTotalExecutado) as totalExecutado  
                   FROM ordemservicoitem where idordemservico  ='" & OrdemServico.IDOrdemServico & "' 
                   and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');", "totalExecutado"))

        Catch ex As Exception

            totalExecutado = 0

        Finally

        End Try

        If result = DialogResult.Yes Then

            If totalExecutado > 0 Then

                Dim dtTabelaPlanoCorte As New System.Data.DataTable()

                dtTabelaPlanoCorte = cl_BancoDados.CarregarDados("SELECT  idplanodecorte, codmatfabricante  
                     FROM ordemservicoitem where idordemservico  = '" & OrdemServico.IDOrdemServico & "' and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');")

                Dim MessagemItens As String

                For I As Integer = 0 To dtTabelaPlanoCorte.Rows.Count - 1

                    MessagemItens = MessagemItens & "PlanoCorte = " & dtTabelaPlanoCorte.Rows(I).Item("idplanodecorte").ToString &
                    " Numero Desenho: = " & dtTabelaPlanoCorte.Rows(I).Item("codmatfabricante").ToString & vbCrLf

                Next

                MsgBox("A OS Numero: " & OrdemServico.IDOrdemServico & " contem processos em andamento, por este motivo não pode ser cancelada, ver plano de corte's: " & vbCrLf & MessagemItens, vbCritical, "Atenção!")

                Exit Sub

            End If

            cl_BancoDados.AlteracaoEspecifica("ordemservico", "D_E_L_E_T_E", "*", "IDOrdemServico", OrdemServico.IDOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservico", "UsuarioD_E_L_E_T_E", Usuario.NomeCompleto, "IDOrdemServico", OrdemServico.IDOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservico", "DataD_E_L_E_T_E", Date.Now.Date, "IDOrdemServico", OrdemServico.IDOrdemServico)

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IDOrdemServico", OrdemServico.IDOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "UsuarioD_E_L_E_T_E", Usuario.NomeCompleto, "IDOrdemServico", OrdemServico.IDOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "DataD_E_L_E_T_E", Date.Now.Date, "IDOrdemServico", OrdemServico.IDOrdemServico)

            dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.Sem_Incone

            Timerdgvos.Enabled = True
            TimerDGVListaMaterialSW.Enabled = True
            TimerFiltroPecaAtivaOS.Enabled = True

        ElseIf result = DialogResult.No Then

            MsgBox("Operação Cancelada", vbCritical, "Atenção")
        End If

    End Sub

    Private Sub dgvDesenhos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDesenhos.DataBindingComplete

        Try

            For I As Integer = 0 To dgvDesenhos.Rows.Count - 1

                If dgvDesenhos.Rows(I).Cells("RNC").Value.ToString = "S" Then

                    dgvDesenhos.Rows(I).Cells("Dgvrnc").Value = My.Resources.atencao

                Else

                    dgvDesenhos.Rows(I).Cells("Dgvrnc").Value = My.Resources.verificado1

                End If

                ' Verifica se a string ".SLDASM" está contida na célula (ignora maiúsculas/minúsculas)
                If dgvDesenhos.Rows(I).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    ' Define a imagem na coluna "dgvIcone"
                    dgvDesenhos.Rows(I).Cells("dgvIcone").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone

                ElseIf dgvDesenhos.Rows(I).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then

                    dgvDesenhos.Rows(I).Cells("dgvIcone").Value = My.Resources.IcopneMontagemPRT

                    'ElseIf dgvDesenhos.Rows(I).Cells("EnderecoArquivo").Value.ToString.IndexOf(".DFT", StringComparison.OrdinalIgnoreCase) >= 0 Then

                    '    dgvDesenhos.Rows(I).Cells("dgvDesenhosDFT").Value = My.Resources.DFT

                    'ElseIf dgvDesenhos.Rows(I).Cells("EnderecoArquivo").Value.ToString.IndexOf(".PDF", StringComparison.OrdinalIgnoreCase) >= 0 Then

                    '    dgvDesenhos.Rows(I).Cells("dgvDesenhosPDF").Value = My.Resources.ficheiro_pdf

                    '  dgvDesenhosDFT

                End If

            Next

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Private Sub btnAssociarMaterial_Click(sender As Object, e As EventArgs)

        If String.IsNullOrWhiteSpace(DadosArquivoCorrente.NomeArquivoSemExtensao) Then

            MessageBox.Show("Não há desenho ativo para associar material.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Exit Sub

        Else
            Using formMateriaisAlmoxarifado As New frmMateriaisAlmoxarifado ' MateriaisAlmoxarifado

                DadosArquivoCorrente.IdMaterial = cl_BancoDados.RetornaCampoDaPesquisa("Select idMaterial from material where codMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "IdMaterial")

                formMateriaisAlmoxarifado.ShowDialog()

                TimerMontaPeca.Enabled = True

            End Using

        End If

    End Sub

    Private Sub ExcluirOMaterialDoDesenhoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcluirOMaterialDoDesenhoToolStripMenuItem.Click

        Try

            If MsgBox("Tem certeza que deseja desabilitar o Material do Desenho Selecionado?", MsgBoxStyle.YesNo, "Confirmar desabilitação!") = MsgBoxResult.No Then
                ' MsgBox("Desenho não desabilitado!")
                Exit Sub
            Else

                Try
                    ' IdMontaPeca = DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value

                    cl_BancoDados.Salvar("Delete  from montapeca where IdMontaPeca = '" & IdMontaPeca & "'")

                    TimerMontaPeca.Enabled = True

                    IdMontaPeca = 0
                Catch ex As Exception

                    MsgBox("Não ha um material valido selecionado", vbCritical, "Atenção")
                Finally

                End Try

            End If
        Catch ex As Exception
            ''' MsgBox(ex.Message)
        Finally
        End Try

        IdMontaPeca = 0

    End Sub

    Private Sub DGVMontaPeca_Click(sender As Object, e As EventArgs) Handles DGVMontaPeca.Click

        'IdMontaPeca = DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value

        ' Verifica se a linha atual não é nula e se a célula "IdMontaPeca" contém um valor válido
        If DGVMontaPeca.CurrentRow IsNot Nothing AndAlso
           DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value IsNot Nothing Then

            ' Tenta converter o valor da célula para o tipo esperado (por exemplo, Integer)
            Dim valorComoString As String = DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value.ToString()
            ' Dim idMontaPeca As Integer

            If Integer.TryParse(valorComoString, IdMontaPeca) Then
                IdMontaPeca = IdMontaPeca
            Else
                ' Trate o caso onde a conversão falha, se necessário
                MessageBox.Show("O valor da célula não é um número válido.")
            End If
            'Else
            '    ' Trate o caso onde a célula ou a linha atual são nulas
            '    MessageBox.Show("Nenhuma linha selecionada ou célula 'IdMontaPeca' está vazia.")

        End If

    End Sub

    Private Sub btnAtualizarCadastro_Click(sender As Object, e As EventArgs) Handles btnAtualizarCadastro.Click

        ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
        Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, abrindo os desenhos e atualizando os dados cadastrais. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then

            IntanciaSolidWorks.ConectarSolidWorks()

            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = dgvDesenhos.Rows.Count - 1

            dgvDesenhos.SuspendLayout()

            For i As Integer = 0 To dgvDesenhos.Rows.Count - 1

                Try

                    DadosArquivoCorrente.EnderecoArquivo = dgvDesenhos.Rows(i).Cells("EnderecoArquivo").Value.ToString

                    DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                    OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                    swModel = swapp.ActiveDoc
                    'swModel = swApparq.ActiveDoc

                    swModel.Visible = True
                    swModelDocExt = swModel.Extension

                    DadosArquivoCorrente.LendoDadosComunsPartAssembly(swModel)
                    'Lista de corte

                    ''''''If swModel.GetType() = swDocumentTypes_e.swDocPART Then
                    ''''''    DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)
                    ''''''End If

                    'dados da caixa delimitadora
                    DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

                    DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel)

                    AtualizaTela(swModel)

                    'edson 15-09-2024
                    DadosArquivoCorrente.AtualizaDesenho(swModel)

                    swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)

                    dgvDesenhos.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightGreen
                Catch ex As Exception

                    dgvDesenhos.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightPink

                    Continue For

                End Try

                ProgressBar1.Value = i

            Next

            MsgBox("Processo de Atuzlização Finalizado com sucesso!", vbInformation, "Informação")

            ProgressBar1.Value = 0
        Else

            MsgBox("Processo cancelado!", vbInformation, "Informação")

        End If

        dgvDesenhos.ResumeLayout()

    End Sub

    Private Sub btnConverterListaDXFPDF_Click(sender As Object, e As EventArgs) Handles btnConverterListaDXFPDF.Click


        If dgvDataGridBOM.Rows.Count > 0 Then

            Dim arquivoSLDDRW As String

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, abrindo os desenhos e atualizando os dados cadastrais. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarListaSW.Minimum = 0
                ProgressBarListaSW.Maximum = (dgvDataGridBOM.Rows.Count - 1) * 2

                dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                    Try

                        DadosArquivoCorrente.EnderecoArquivo = dgvDataGridBOM.Rows(i).Cells("EnderecoArquivo").Value.ToString

                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                        If chkConverterPDF.Checked Then

                            ' Verifica se EnderecoArquivo não é nulo ou vazio antes de realizar a substituição
                            If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then

                                Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()

                                ' Converte o caminho do arquivo para .SLDDRW se ele terminar com .SLDPRT ou .SLDASM
                                arquivoSLDDRW = enderecoArquivoUpper
                                If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                                    arquivoSLDDRW = arquivoSLDDRW.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                                End If

                                If File.Exists(arquivoSLDDRW) Then
                                    OpenDocumentAndWait(arquivoSLDDRW, True, swModel)
                                    DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False)
                                    ' swapp.CloseDoc(arquivoSLDDRW)
                                    dgvDataGridBOM.Rows(i).Cells("dgvIconePDF").Value = My.Resources.ficheiro_pdf

                                Else

                                    '  DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False)
                                    dgvDataGridBOM.Rows(i).Cells("dgvIconePDF").Value = My.Resources.Sem_Incone

                                End If

                            End If

                        End If


                        If chkConverterDXF.Checked Then

                            If DadosArquivoCorrente.EnderecoArquivo.ToUpper.EndsWith(".SLDPRT") Then

                                OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                                Dim enderecoArquivo As String = DadosArquivoCorrente.EnderecoArquivo

                                ' Verifica se EnderecoArquivo não é nulo e o arquivo realmente existe antes de tentar exportar
                                If Not String.IsNullOrEmpty(enderecoArquivo) AndAlso File.Exists(enderecoArquivo) Then
                                    DadosArquivoCorrente.ExportDXF(swModel, True, True)
                                    dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.arquivo_dxf

                                Else
                                    dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.Sem_Incone


                                End If
                            Else

                                dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.Sem_Incone

                            End If

                        End If


                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightGreen

                        ProgressBarListaSW.Value = i

                    Catch ex As Exception

                        dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightPink

                        '  MsgBox(ex.Message)

                        Continue For

                    End Try

                Next

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")

                ProgressBarListaSW.Value = 0

                chkConverterPDF.Checked = False
                chkConverterDXF.Checked = False


            Else

                MsgBox("Processo cancelado!", vbInformation, "Informação")

            End If

            'dgvDataGridBOM.ResumeLayout()
        End If

        If dgvDataGridBOM.Rows.Count > 0 Then

            For a As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                Try

                    If dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString().ToLower().EndsWith(".sldprt") OrElse
                  dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString().ToLower().EndsWith(".sldasm") Then

                        swapp.CloseDoc(dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString())

                    End If
                Catch ex As Exception
                    Continue For
                End Try

            Next
        End If



    End Sub

    Private Sub InformeOTituloPadrãoDoProdutoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InformeOTituloPadrãoDoProdutoToolStripMenuItem.Click

        Try

            TituloPadraoProduto = InputBox("Informe o Titulo Padrão para o Produto a ser cadastrado/Atualizado", "Atuzlização", DadosArquivoCorrente.NomeArquivoSemExtensao)
        Catch ex As Exception
        Finally

        End Try

    End Sub

    'Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs)

    '    ' Verifica se o item sendo marcado está sendo definido como "Checked"
    '    If e.NewValue = CheckState.Checked Then
    '        ' Desmarcar todos os outros itens
    '        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
    '            If i <> e.Index Then
    '                CheckedListBox1.SetItemChecked(i, False)
    '            End If
    '        Next
    '    End If

    'End Sub


    Private Sub TimerHoraServidor_Tick(sender As Object, e As EventArgs) Handles TimerHoraServidor.Tick

        Try
            HoraServidor = cl_BancoDados.RetornaCampoDaPesquisa("SELECT CURTIME() as Hora ;", "Hora")

            txtHoraServidor.Text = HoraServidor
        Catch ex As Exception
            swapp.UnloadAddIn("SwLynx_4._1")
        End Try


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        GetCutLists(swModel)
    End Sub

    Function GetCutLists(ByVal model As ModelDoc2) As List(Of Feature)

        IntanciaSolidWorks.ConectarSolidWorks()
        ' swApparq = CreateObject("SldWorks.Application")

        swModel = swapp.ActiveDoc
        'swModel = swApparq.ActiveDoc

        ' Lista para armazenar as CutLists
        Dim swCutListFeats As New List(Of Feature)
        Dim swFeat As Feature
        Dim swBodyFolder As BodyFolder
        Dim vBodies As Object
        Dim swBody As Body2

        ' Obter o primeiro recurso do modelo
        swFeat = model.FirstFeature

        ' Armazenar as features de CutListFolder a serem excluídas
        Dim cutListFoldersToDelete As New List(Of Feature)

        ' Primeiro, percorrer todas as features e identificar as Cut List Folders
        Do While swFeat IsNot Nothing
            ' Verifica se o tipo do recurso é uma pasta de Cut List
            If swFeat.GetTypeName2() = "CutListFolder" Then
                ' Armazenar a referência da feature CutListFolder para exclusão
                ' Desativa a atualização automática da lista de corte

                cutListFoldersToDelete.Add(swFeat)
            End If
            swFeat = swFeat.GetNextFeature()
        Loop

        ' Agora excluir apenas as features que são CutListFolder
        For Each cutListFeat As Feature In cutListFoldersToDelete
            cutListFeat.Select2(False, 0)
            model.EditDelete()
        Next

        Try

            ' Forçar a recriação das listas de cortes
            Dim featMgr As FeatureManager = model.FeatureManager
            featMgr.UpdateCutList()

            ' Obter o primeiro recurso novamente após a recriação das listas de cortes
            swFeat = model.FirstFeature

            ' Loop para obter as novas Cut List Folders criadas
            Do While swFeat IsNot Nothing
                ' Verifica se o tipo do recurso é uma pasta de Cut List
                If swFeat.GetTypeName2() = "CutListFolder" Then
                    swBodyFolder = swFeat.GetSpecificFeature2()
                    vBodies = swBodyFolder.GetBodies()

                    ' Verifica se há corpos na Cut List e se são de chapa metálica
                    If vBodies IsNot Nothing AndAlso vBodies.Length > 0 Then
                        swBody = CType(vBodies(0), Body2)

                        ' Verifica se o corpo é de chapa metálica
                        If swBody.IsSheetMetal() Then
                            swCutListFeats.Add(swFeat)
                        End If
                    End If
                End If
                ' Avança para o próximo recurso
                swFeat = swFeat.GetNextFeature()
            Loop

            ' Retornar a lista de Cut Lists
            Return swCutListFeats
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try

    End Function

    Private Sub OPTEstoqueNao_CheckedChanged(sender As Object, e As EventArgs) Handles OPTEstoqueNao.CheckedChanged

        'Try



        '    ' Verifica se o swModel não é nulo
        '    If swModel Is Nothing Then
        '        ' MessageBox.Show("Erro: Modelo não está aberto ou não foi carregado corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return
        '        Exit Sub
        '    End If

        '    IntanciaSolidWorks.ConectarSolidWorks()
        '    ' swApparq = CreateObject("SldWorks.Application")

        '    swModel = swapp.ActiveDoc
        '    'swModel = swApparq.ActiveDoc

        '    ' Define o valor baseado na seleção do usuário
        '    If OPTEstoqueNao.Checked = True Then
        '        DadosArquivoCorrente.ItemEstoque = "NÃO"
        '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtEstoque", DadosArquivoCorrente.ItemEstoque, DadosArquivoCorrente.ItemEstoque)
        '    Else
        '        DadosArquivoCorrente.ItemEstoque = "SIM"
        '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtEstoque", DadosArquivoCorrente.ItemEstoque, DadosArquivoCorrente.ItemEstoque)
        '    End If

        '    ' Tenta salvar o modelo silenciosamente
        '    Dim saveResult As Boolean = swModel.SaveSilent()

        '    swModel.SaveSilent()
        '    'cl_BancoDados.AlteracaoEspecifica("material", "txtitemEstoque", DadosArquivoCorrente.ItemEstoque, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)


        '    '' Verifica se o modelo foi salvo com sucesso
        '    'If Not saveResult Then
        '    '    MessageBox.Show("O modelo não pôde ser salvo.", "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    'End If

        'Catch ex As Exception
        '    ' Exibe mensagem de erro caso ocorra uma exceção
        '    ' MessageBox.Show("Erro: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally

        'End Try

    End Sub



    'Private Sub cboTipoDesenho_LostFocus(sender As Object, e As EventArgs) Handles cboTipoDesenho.LostFocus

    '    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtTipoDesenho", cboTipoDesenho.Text, cboTipoDesenho.Text)

    '    swModel.SaveSilent()

    'End Sub

    'Private Sub OPTEstoqueSim_CheckedChanged(sender As Object, e As EventArgs) Handles OPTEstoqueSim.CheckedChanged

    '    If OPTEstoqueSim.Checked = True Then

    '        DadosArquivoCorrente.ItemEstoque = "SIM"
    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtEstoque", "SIM", "SIM")

    '    Else

    '        DadosArquivoCorrente.ItemEstoque = "NÃO"
    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtEstoque", "NÃO", "NÃO")

    '    End If

    '    swModel.SaveSilent()


    'End Sub

    'Private Sub optProcessoSoldagemSim_CheckedChanged(sender As Object, e As EventArgs) Handles optProcessoSoldagemSim.CheckedChanged

    '    If optProcessoSoldagemSim.Checked = True Then

    '        DadosArquivoCorrente.soldagem = "SIM"
    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtsoldagem", "SIM", "SIM")

    '    Else

    '        DadosArquivoCorrente.soldagem = "NÃO"
    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtsoldagem", "NÃO", "NÃO")

    '    End If

    '    swModel.SaveSilent()

    'End Sub

    'Private Sub optProcessoSoldagemNao_CheckedChanged(sender As Object, e As EventArgs) Handles optProcessoSoldagemNao.CheckedChanged

    '    If optProcessoSoldagemNao.Checked = True Then

    '        DadosArquivoCorrente.soldagem = "NÃO"
    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtsoldagem", "NÃO", "NÃO")

    '    Else

    '        DadosArquivoCorrente.soldagem = "SIM"
    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtsoldagem", "SIM", "SIM")

    '    End If

    '    swModel.SaveSilent()

    'End Sub





    Private Sub cboTitulo_LostFocus(sender As Object, e As EventArgs) Handles cboTitulo.LostFocus

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then


                swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle) = Me.cboTitulo.Text

                swModel.SaveSilent()
            End If


        Catch ex As Exception
        Finally
        End Try


    End Sub



    Private Sub OPTEstoqueSim_Click(sender As Object, e As EventArgs) Handles OPTEstoqueSim.Click

        Try


            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtItemEstoque", "SIM", "SIM")
                swModel.SaveSilent()

            End If

        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub OPTEstoqueNao_Click(sender As Object, e As EventArgs) Handles OPTEstoqueNao.Click

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then


            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtItemEstoque", "NÃO", "NÃO")
            swModel.SaveSilent()

        End If


    End Sub


    Private Sub optProcessoSoldagemSim_Click(sender As Object, e As EventArgs) Handles optProcessoSoldagemSim.Click

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then


            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSoldagem", "SIM", "SIM")
            swModel.SaveSilent()

        End If


    End Sub


    Private Sub optProcessoSoldagemNao_Click(sender As Object, e As EventArgs) Handles optProcessoSoldagemNao.Click

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then


            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSoldagem", "NÃO", "NÃO")
            swModel.SaveSilent()

        End If

    End Sub


    Private Sub chkCorte_Click(sender As Object, e As EventArgs) Handles chkCorte.Click
        Try


            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.Corte = If(chkCorte.Checked, "1", "")
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtCorte", DadosArquivoCorrente.Corte, DadosArquivoCorrente.Corte)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try


    End Sub

    Private Sub chkDobra_Click(sender As Object, e As EventArgs) Handles chkDobra.Click

        Try



            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.Dobra = If(chkDobra.Checked, "1", "")
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtDobra", DadosArquivoCorrente.Dobra, DadosArquivoCorrente.Dobra)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally

        End Try

    End Sub


    Private Sub chkSolda_Click(sender As Object, e As EventArgs) Handles chkSolda.Click

        Try



            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.Solda = If(chkSolda.Checked, "1", "")
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSolda", DadosArquivoCorrente.Solda, DadosArquivoCorrente.Solda)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub
    Private Sub chkPintura_Click(sender As Object, e As EventArgs) Handles chkPintura.Click

        Try


            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.Pintura = If(chkPintura.Checked, "1", "")
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtPintura", DadosArquivoCorrente.Pintura, DadosArquivoCorrente.Pintura)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub chkMontagem_Click(sender As Object, e As EventArgs) Handles chkMontagem.Click

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.Montagem = If(chkMontagem.Checked, "1", "")
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtMontagem", DadosArquivoCorrente.Montagem, DadosArquivoCorrente.Montagem)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub AtualizarDesenhoPeloDiretorioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AtualizarDesenhoPeloDiretorioToolStripMenuItem.Click


        Dim result As DialogResult

        result = MessageBox.Show("Escolha uma opção:  SIM      1 - Atualiza a versão do SolidWork e Salva no banco de dados" & vbCrLf _
                                                   & "NÃO      2 - Somente Atualiza a versão do SolidWorks sem Salvar no Banco de dados" & vbCrLf _
                                                   & "Cancelar 3 -Finaliza a opração sem faze nenhuma alteralção", "Atualizar Versão",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1)

        If result = DialogResult.Yes Then

            ' Opção 1 - Atualizar versão do SW e Salvar no banco
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(True)

        ElseIf result = DialogResult.No Then
            ' Opção 2 - Somente atualizar versão do SW
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(False)

        ElseIf result = DialogResult.Cancel Then
            ' Opção 3 - Cancelar
            MessageBox.Show("A operação foi cancelada.", "Cancelar")

        End If

    End Sub

    Private Function SelectFolder() As String
        Using folderBrowser As New FolderBrowserDialog()
            folderBrowser.Description = "Selecione o diretório com os desenhos"
            folderBrowser.ShowNewFolderButton = False

            If folderBrowser.ShowDialog() = DialogResult.OK Then
                Return folderBrowser.SelectedPath
            End If
        End Using

        Return String.Empty
    End Function

    ' Função para abrir, salvar e fechar todos os arquivos .sldprt em um diretório escolhido pelo usuário e suas subpastas
    ' Função para abrir, salvar e fechar todos os arquivos .sldprt e .sldasm em um diretório escolhido pelo usuário e suas subpastas
    Public Sub ProcessAllFiles(ByVal SalvarBanco As Boolean)

        'Dim folderPath As String = SelectFolder()

        'If String.IsNullOrEmpty(folderPath) Then

        '    MsgBox("Nenhum diretório selecionado.")

        '    Return

        'End If

        'Try
        '    ' Obter todos os arquivos em diretório e subpastas
        '    Dim allFiles As String() = IO.Directory.GetFiles(folderPath, "*.*", IO.SearchOption.AllDirectories)

        '    ' Filtrar arquivos .sldprt e .sldasm (sem distinção de maiúsculas/minúsculas)
        '    Dim files As IEnumerable(Of String) = allFiles.Where(Function(f) f.EndsWith(".sldprt", StringComparison.OrdinalIgnoreCase) OrElse f.EndsWith(".sldasm", StringComparison.OrdinalIgnoreCase))

        '    For Each filePath In files
        '        ' Determina o tipo de documento
        '        Dim docType As swDocumentTypes_e
        '        If filePath.EndsWith(".sldprt", StringComparison.OrdinalIgnoreCase) Then
        '            docType = swDocumentTypes_e.swDocPART
        '        Else
        '            docType = swDocumentTypes_e.swDocASSEMBLY
        '        End If

        '        ' Abre o arquivo
        '        Dim swModel As ModelDoc2 = swapp.OpenDoc6(filePath, docType, 0, "", Nothing, Nothing)

        '        If swModel IsNot Nothing Then

        '            DadosArquivoCorrente.ArquivoCorrente(swModel)

        '            swModel.Save()

        '            If SalvarBanco = True Then

        '                SalvarArquivoCorrente(swModel)

        '            End If

        '            swapp.CloseDoc(filePath)

        '        End If
        '    Next

        '    MsgBox("Processamento concluído!")

        'Catch ex As Exception
        '    '  MsgBox("Erro: " & ex.Message)
        '    ' Return False
        'Finally

        'End Try

        Dim folderPath As String = ""

        ' Seleciona a pasta usando OpenFileDialog
        Using openFileDialog As New OpenFileDialog
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = True
            openFileDialog.ValidateNames = False
            openFileDialog.FileName = "Selecione uma pasta"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ' Remove o nome fictício para obter o caminho correto
                folderPath = IO.Path.GetDirectoryName(openFileDialog.FileName)
            Else
                MsgBox("Nenhum diretório selecionado.")
                Return
            End If
        End Using

        If String.IsNullOrEmpty(folderPath) Then
            MsgBox("Nenhum diretório selecionado.")
            Return
        End If

        Try
            ' Obter todos os arquivos no diretório e subpastas
            Dim allFiles As String() = IO.Directory.GetFiles(folderPath, "*.*", IO.SearchOption.AllDirectories)

            ' Filtrar arquivos .sldprt e .sldasm (sem distinção de maiúsculas/minúsculas)
            Dim files As IEnumerable(Of String) = allFiles.Where(Function(f) f.EndsWith(".sldprt", StringComparison.OrdinalIgnoreCase) OrElse f.EndsWith(".sldasm", StringComparison.OrdinalIgnoreCase))

            For Each filePath In files
                ' Determina o tipo de documento
                Dim docType As swDocumentTypes_e
                If filePath.EndsWith(".sldprt", StringComparison.OrdinalIgnoreCase) Then
                    docType = swDocumentTypes_e.swDocPART
                Else
                    docType = swDocumentTypes_e.swDocASSEMBLY
                End If

                ' Abre o arquivo
                Dim swModel As ModelDoc2 = swapp.OpenDoc6(filePath, docType, 0, "", Nothing, Nothing)

                If swModel IsNot Nothing Then
                    ' Processa o arquivo
                    DadosArquivoCorrente.ArquivoCorrente(swModel)

                    ' Salva o modelo
                    swModel.Save()

                    ' Verifica se deve salvar no banco de dados
                    If SalvarBanco = True Then
                        SalvarArquivoCorrente(swModel)
                    End If

                    ' Fecha o documento
                    swapp.CloseDoc(filePath)
                End If
            Next

            MsgBox("Processamento concluído!")

        Catch ex As Exception
            MsgBox("Erro: " & ex.Message)
        Finally
            ' Adicione qualquer lógica de limpeza aqui, se necessário
        End Try


    End Sub

    'Private Sub chkcorteArvore_Click(sender As Object, e As EventArgs)

    '    ' Verifique se o swModel foi aberto com sucesso
    '    If Not swModel Is Nothing Then


    '        If chkcorteArvore.Checked = True Then

    '            valorCorte = "1"
    '        Else
    '            valorCorte = "0"

    '        End If

    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModelComp, "txtcorte", valorCorte, valorCorte)

    '        swModelComp.SaveSilent()

    '    End If


    'End Sub

    'Private Sub chkDobraArvore_Click(sender As Object, e As EventArgs)

    '    ' Verifique se o swModel foi aberto com sucesso
    '    If Not swModel Is Nothing Then


    '        If chkDobraArvore.Checked = True Then

    '            valorDobra = "1"
    '        Else
    '            valorDobra = "0"

    '        End If

    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModelComp, "txtdobra", valorDobra, valorDobra)


    '        swModelComp.SaveSilent()

    '    End If

    'End Sub

    'Private Sub chkSoldaArvore_Click(sender As Object, e As EventArgs)

    '    ' Verifique se o swModel foi aberto com sucesso
    '    If Not swModel Is Nothing Then


    '        If chkSoldaArvore.Checked = True Then

    '            valorSolda = "1"
    '        Else
    '            valorSolda = "0"

    '        End If

    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModelComp, "txtsolda", valorSolda, valorSolda)


    '        swModelComp.SaveSilent()

    '    End If

    'End Sub

    'Private Sub chkPinturaArvore_Click(sender As Object, e As EventArgs)

    '    ' Verifique se o swModel foi aberto com sucesso
    '    If Not swModel Is Nothing Then

    '        If chkPinturaArvore.Checked = True Then

    '            valorPintura = "1"
    '        Else
    '            valorPintura = "0"

    '        End If

    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModelComp, "txtpintura", valorPintura, valorPintura)

    '        swModelComp.SaveSilent()

    '    End If


    'End Sub

    'Private Sub chkMontagemArvore_Click(sender As Object, e As EventArgs)


    '    ' Verifique se o swModel foi aberto com sucesso
    '    If Not swModel Is Nothing Then


    '        If chkMontagemArvore.Checked = True Then

    '            valorMontagem = "1"
    '        Else
    '            valorMontagem = "0"

    '        End If

    '        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModelComp, "txtMontagem", valorMontagem, valorMontagem)

    '        swModelComp.SaveSilent()
    '    End If


    'End Sub

    'Private Sub cboTipoDesenho_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoDesenho.SelectedIndexChanged

    'End Sub

    'Private Sub cboAcabamento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAcabamento.SelectedIndexChanged

    'End Sub

    Private Sub GerarArquivoEmDXFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarArquivoEmDXFToolStripMenuItem.Click

        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em DXF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    Try


                        '  If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                        DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString

                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                        Dim enderecoArquivo As String = DadosArquivoCorrente.EnderecoArquivo

                        ' Verifica se EnderecoArquivo não é nulo e o arquivo realmente existe antes de tentar exportar
                        If Not String.IsNullOrEmpty(enderecoArquivo) AndAlso File.Exists(enderecoArquivo) Then
                            DadosArquivoCorrente.ExportDXF(swModel, False, True)

                        End If

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        Dim enredecoDxf As String

                        ' Altera a extensão do arquivo para .DXF, independentemente de ser .SLDPRT ou .sldprt
                        enredecoDxf = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".DXF")

                        ' Verifica se o arquivo DXF existe
                        If File.Exists(enredecoDxf) Then
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                        Else
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.Sem_Incone
                        End If
                        ProgressBarProcessoLiberacaoOrdemServico.Value = i


                        ' DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                        '   End If

                    Catch ex As Exception
                        Continue For
                    End Try
                Next

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")

                ProgressBarProcessoLiberacaoOrdemServico.Value = 0

            End If

        End If

    End Sub

    Private Sub GerarArquivoEmPDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarArquivoEmPDFToolStripMenuItem.Click


        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em PDF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                '   IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    ' If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then


                    Try



                        DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString
                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)
                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)



                        ' Verifica se EnderecoArquivo não é nulo ou vazio antes de realizar a substituição
                        If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then
                            Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()

                            ' Converte o caminho do arquivo para .SLDDRW se ele terminar com .SLDPRT ou .SLDASM

                            If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                                enderecoArquivoUpper = enderecoArquivoUpper.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                            End If
                            If File.Exists(enderecoArquivoUpper) Then
                                OpenDocumentAndWait(enderecoArquivoUpper, True, swModel)
                                DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)
                                ' swapp.CloseDoc(arquivoSLDDRW)
                            Else

                                DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)

                            End If

                        End If


                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        Dim enredecoPDF As String

                        ' Altera a extensão do arquivo para .PDF, independentemente de ser .SLDPRT, .sldprt, .SLDASM ou .sldasm
                        enredecoPDF = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".PDF")

                        ' Verifica se o arquivo PDF existe
                        If File.Exists(enredecoPDF) Then
                            DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                        Else
                            DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.Sem_Incone
                        End If
                        ProgressBarProcessoLiberacaoOrdemServico.Value = i
                    Catch ex As Exception
                        Continue For

                    End Try

                    ' DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                Next
                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")
                ProgressBarProcessoLiberacaoOrdemServico.Value = 0
            End If

        End If

    End Sub

    Private Sub AbrirDWRDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Click
        Try
            Dim ArquivoDXF As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".SLDDRW")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    p.WaitForExit()

                    DGVListaMaterialSW.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub BuscarArquivosNosDiretorioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarArquivosNosDiretorioToolStripMenuItem.Click

        Arquivos.Show()

    End Sub

    Private Sub btnAtualizarProjeto_Click(sender As Object, e As EventArgs)

        Try

            cl_BancoDados.ComboBoxDataSet("projetos", "IdProjeto", "Projeto", cboProjeto, " WHERE Liberado = 'S'")
            cl_BancoDados.ComboBoxDataSet("acabamento", "idAcabamento", "DescAcabamento", cboOpcoesAcabamento, "")
            '  cl_BancoDados.ComboBoxDataSet("acabamento", "idAcabamento", "DescAcabamento", cboAcabamentoArvore, "")

            '  cl_BancoDados.ComboBoxDataSet("familia", "idfamilia", "Descfamilia", cboTipoDesenho, "")
            '  cl_BancoDados.ComboBoxDataSet("familia", "idfamilia", "Descfamilia", cboTipoDesenhoArvore, "")

            cl_BancoDados.ComboBoxDataSet("tipoproduto", "idtipoproduto", "tipoproduto", cboTitulo, "")
            '  cl_BancoDados.ComboBoxDataSet("tipoproduto", "idtipoproduto", "tipoproduto", cboTituloArvore, "")


        Catch ex As Exception

        Finally

        End Try

    End Sub

    Private Sub btnLimparOS_Click(sender As Object, e As EventArgs)

        Timerdgvos.Enabled = True
        DGVListaMaterialSW.DataSource = Nothing
        DGVListaMaterialSW.Refresh()


    End Sub

    Private Sub AlterarOFatorMultipçlicadorDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Click

        If DGVListaMaterialSW.Rows.Count <= 0 Then

            Exit Sub

        End If

        Try

            If OrdemServico.LIBERADO_ENGENHARIA <> "" Then

                MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")

                Exit Sub

            Else

                Dim FatorMultiplicador As Double

                FatorMultiplicador = InputBox("Informe o novo valor Multiplicador", "Alteração de Quantidade", 1)

                If IsNumeric(FatorMultiplicador) And FatorMultiplicador > 0 Then

                    For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                        Dim Peso, areapintura, qtde, QtdeTotal, Fator As String

                        Try
                            Fator = DGVListaMaterialSW.Rows(i).Cells("Fator").Value
                        Catch ex As Exception
                            Fator = 0
                        End Try

                        Try
                            qtde = DGVListaMaterialSW.Rows(i).Cells("qtde").Value
                        Catch ex As Exception
                            qtde = 0
                        End Try

                        Try
                            areapintura = DGVListaMaterialSW.Rows(i).Cells("AreaPinturaUnitario").Value
                        Catch ex As Exception
                            areapintura = DGVListaMaterialSW.Rows(i).Cells("AreaPintura").Value / qtde
                        End Try


                        Try
                            Peso = DGVListaMaterialSW.Rows(i).Cells("PesoUnitario").Value
                        Catch ex As Exception
                            Peso = DGVListaMaterialSW.Rows(i).Cells("Peso").Value / qtde
                        End Try


                        Peso = Peso * FatorMultiplicador

                        areapintura = areapintura * FatorMultiplicador

                        QtdeTotal = qtde * FatorMultiplicador

                        cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", QtdeTotal, "IDOrdemServicoITEM", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoITEM").Value.ToString)

                        cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", areapintura, "IDOrdemServicoITEM", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoITEM").Value.ToString)

                        cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Peso, "IDOrdemServicoITEM", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoITEM").Value.ToString)

                        DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Value = QtdeTotal
                        'DGVListaMaterialSW.CurrentRow.Cells("Qtde").Value = novaqtde

                        DGVListaMaterialSW.Rows(i).Cells("AreaPintura").Value = areapintura

                        DGVListaMaterialSW.Rows(i).Cells("Peso").Value = Peso

                        DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Value = QtdeTotal

                        DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                        DGVListaMaterialSW.Rows(i).Cells("AreaPintura").Style.BackColor = Color.LightGreen

                        DGVListaMaterialSW.Rows(i).Cells("Peso").Style.BackColor = Color.LightGreen

                    Next

                    Dim diretorio As String = OrdemServico.ENDERECOOrdemServico

                    LimparDiretorio(diretorio & "\PDF")
                    LimparDiretorio(diretorio & "\DXF")
                    LimparDiretorio(diretorio & "\DFT")
                    LimparDiretorio(diretorio & "\LXDS")

                    ImportarLXDSParaOS(DGVListaMaterialSW, "DXF", ProgressBarProcessoLiberacaoOrdemServico)
                    ImportarLXDSParaOS(DGVListaMaterialSW, "PDF", ProgressBarProcessoLiberacaoOrdemServico)
                    ImportarLXDSParaOS(DGVListaMaterialSW, "DFT", ProgressBarProcessoLiberacaoOrdemServico)
                    ImportarLXDSParaOS(DGVListaMaterialSW, "LXDS", ProgressBarProcessoLiberacaoOrdemServico)


                Else

                    MsgBox("O valor informado não e um numero Valido")

                End If

            End If

        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub CriarUmCopiaDaOSSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Click


        Dim NovoIdOrdemServicoDB As Integer

        Dim result As DialogResult = MessageBox.Show("Deseja criar um nova OS, com base na OS: " & Me.lblOrdemServicoAtiva.Text, "Criando Copia da OS corrente", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            If OrdemServico.IDOrdemServico = 0 Or OrdemServico.IDOrdemServico.ToString = "" Or OrdemServico.IDOrdemServico = Nothing Then
                MsgBox("Nao uma OS valida!", vbCritical)
                Exit Sub
            End If

            If DGVListaMaterialSW.Rows.Count <= 0 Then
                MsgBox("Nao há itens a serem copiados", vbCritical, "Atenção")
                Exit Sub

            End If

            If My.Settings.EnderecoPastaRaizOS.ToString = "" And System.IO.Directory.Exists(My.Settings.EnderecoPastaRaizOS) = False Then

                MsgBox("O endereço onde será criado a pasta da Ordem de Serviço  não foi informado!")
                Exit Sub

            Else

                OrdemServico.PROJETO = Me.cboProjeto.Text.ToUpper
                OrdemServico.TAG = Me.cboTag.Text.ToUpper
                OrdemServico.DESCRICAO = Me.txtDescricao.Text.ToUpper & "Esta e um OS copia da OS de referencia Numero: " & OrdemServico.IDOrdemServico
                OrdemServico.CRIADOPOR = Usuario.NomeCompleto
                OrdemServico.DATACRIACAO = Date.Now.Date
                OrdemServico.ESTATUS = "A".ToUpper
                OrdemServico.IDPROJETO = cboProjeto.SelectedValue
                OrdemServico.IDTAG = cboTag.SelectedValue
                OrdemServico.DESCEMPRESA = txtCliente.Text


                Dim idosRetono As String


                Try
                    NovoIdOrdemServicoDB = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT max(IDOrdemServico)  as NovoIdOrdemServico FROM ordemservico", "NovoIdOrdemServico")) + 1

                    NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
                Catch ex As Exception

                    ' Em caso de erro, atribuir "00001" como valor inicial
                    NovoIdOrdemServico = "00001"

                End Try

                OrdemServico.ENDERECOOrdemServico = Replace((My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico).ToString.ToUpper, "\", "##")

                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico)
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DXF")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PDF")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DFT")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PUNC")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LASER")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PROJETO")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PEÇAS DE ESTOQUE")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LXDS")



                cl_BancoDados.Salvar("insert into ordemservico(idProjeto,
PROJETO,
idTag,
TAG,
DESCRICAO,
ENDERECOOrdemServico,
CRIADOPOR,
DATACRIACAO,
ESTATUS,
D_E_L_E_T_E,
DESCEMPRESA) values
('" & OrdemServico.IDPROJETO & "','" _
    & OrdemServico.PROJETO & "','" _
    & OrdemServico.IDTAG & "','" _
    & OrdemServico.TAG & "','" _
    & OrdemServico.DESCRICAO & "','" _
    & OrdemServico.ENDERECOOrdemServico & "','" _
    & OrdemServico.CRIADOPOR.ToString().ToUpper() & "','" _
    & OrdemServico.DATACRIACAO & "','" _
    & OrdemServico.ESTATUS & "','','" _
    & OrdemServico.DESCEMPRESA & "')")


            End If


            ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
            ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count


            If DGVListaMaterialSW.Rows.Count > 0 Then

                Dim fator As String

                For A As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1


                    Try
                        OrdemServico.CodMatFabricante = DGVListaMaterialSW.Rows(A).Cells("CodMatFabricante").Value.ToString.ToUpper
                    Catch ex As Exception
                        OrdemServico.CodMatFabricante = ""
                    End Try


                    Try
                        OrdemServico.DescResumo = DGVListaMaterialSW.Rows(A).Cells("DescResumo").Value.ToString.ToUpper
                    Catch ex As Exception
                        OrdemServico.DescResumo = ""
                    End Try

                    Try
                        OrdemServico.DescDetal = DGVListaMaterialSW.Rows(A).Cells("DescDetal").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.DescDetal = ""
                    End Try

                    Try
                        OrdemServico.Autor = DGVListaMaterialSW.Rows(A).Cells("Autor").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.Autor = ""
                    End Try

                    Try
                        OrdemServico.Palavrachave = DGVListaMaterialSW.Rows(A).Cells("Palavrachave").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.Palavrachave = ""
                    End Try

                    Try
                        OrdemServico.Notas = DGVListaMaterialSW.Rows(A).Cells("Notas").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.Notas = ""
                    End Try

                    Try
                        OrdemServico.Espessura = DGVListaMaterialSW.Rows(A).Cells("Espessura").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Espessura = ""
                    End Try

                    Try
                        OrdemServico.NumeroDobras = DGVListaMaterialSW.Rows(A).Cells("NumeroDobras").Value.ToString
                    Catch ex As Exception

                        OrdemServico.NumeroDobras = ""
                    End Try

                    If DGVListaMaterialSW.Rows(A).Cells("EnderecoArquivo").Value.ToString().IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                        OrdemServico.Unidade = "CONJ"
                        OrdemServico.UnidadeSW = "CONJ"
                    Else

                        OrdemServico.Unidade = "PC"
                        OrdemServico.UnidadeSW = "PC"

                    End If

                    OrdemServico.ValorSW = ""

                    Try
                        OrdemServico.Altura = Replace(DGVListaMaterialSW.Rows(A).Cells("Altura").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.Altura = ""
                    End Try

                    Try
                        OrdemServico.Largura = Replace(DGVListaMaterialSW.Rows(A).Cells("Largura").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.Largura = ""

                    End Try

                    OrdemServico.DtCad = ""
                    OrdemServico.UsuarioCriacao = ""
                    OrdemServico.UsuarioAlteracao = ""
                    OrdemServico.DtAlteracao = ""

                    Try
                        OrdemServico.EnderecoArquivo = DGVListaMaterialSW.Rows(A).Cells("EnderecoArquivo").Value.ToString.ToUpper

                    Catch ex As Exception

                        OrdemServico.EnderecoArquivo = ""

                    End Try

                    Try
                        OrdemServico.MaterialSw = DGVListaMaterialSW.Rows(A).Cells("Material").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.MaterialSw = ""

                    End Try

                    Try
                        OrdemServico.Qtde = DGVListaMaterialSW.Rows(A).Cells("Qtde").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Qtde = 0

                    End Try

                    Try
                        OrdemServico.AreaPintura = Replace(DGVListaMaterialSW.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                        OrdemServico.AreaPintura = OrdemServico.AreaPintura
                    Catch ex As Exception

                        OrdemServico.AreaPintura = ""

                    End Try

                    Try
                        OrdemServico.AreaPinturaUnitario = Replace(DGVListaMaterialSW.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.AreaPinturaUnitario = ""

                    End Try

                    Try
                        OrdemServico.Peso = Replace(DGVListaMaterialSW.Rows(A).Cells("Peso").Value.ToString, ".", ",")

                        OrdemServico.Peso = OrdemServico.Peso
                    Catch ex As Exception

                        OrdemServico.Peso = 0
                    End Try

                    Try
                        OrdemServico.PesoUnitario = Replace(DGVListaMaterialSW.Rows(A).Cells("Peso").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.PesoUnitario = 0
                    End Try

                    Try
                        OrdemServico.txtSoldagem = DGVListaMaterialSW.Rows(A).Cells("txtSoldagem").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtSoldagem = ""

                    End Try

                    Try
                        OrdemServico.QtdeTotal = Replace(DGVListaMaterialSW.Rows(A).Cells("QtdeTotal").Value.ToString, ".", ",")
                        'OrdemServico.QtdeTotal = OrdemServico.Qtde * Fator
                    Catch ex As Exception

                        OrdemServico.QtdeTotal = 0

                    End Try

                    Try
                        OrdemServico.txtTipoDesenho = DGVListaMaterialSW.Rows(A).Cells("txtTipoDesenho").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtTipoDesenho = ""

                    End Try

                    Try
                        OrdemServico.txtCorte = DGVListaMaterialSW.Rows(A).Cells("txtCorte").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtCorte = ""

                    End Try

                    Try
                        OrdemServico.txtDobra = DGVListaMaterialSW.Rows(A).Cells("txtDobra").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtDobra = ""

                    End Try

                    Try
                        OrdemServico.txtSolda = DGVListaMaterialSW.Rows(A).Cells("txtSolda").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtSolda = ""

                    End Try

                    Try
                        OrdemServico.txtPintura = DGVListaMaterialSW.Rows(A).Cells("txtPintura").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtPintura = ""

                    End Try

                    Try
                        OrdemServico.txtMontagem = DGVListaMaterialSW.Rows(A).Cells("txtMontagem").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtMontagem = ""

                    End Try

                    Try
                        OrdemServico.Comprimentocaixadelimitadora = DGVListaMaterialSW.Rows(A).Cells("Comprimentocaixadelimitadora").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Comprimentocaixadelimitadora = ""

                    End Try

                    Try
                        OrdemServico.Larguracaixadelimitadora = DGVListaMaterialSW.Rows(A).Cells("Larguracaixadelimitadora").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Larguracaixadelimitadora = ""

                    End Try

                    Try
                        OrdemServico.Espessuracaixadelimitadora = DGVListaMaterialSW.Rows(A).Cells("Espessuracaixadelimitadora ").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Espessuracaixadelimitadora = ""

                    End Try

                    Try
                        OrdemServico.txtItemEstoque = DGVListaMaterialSW.Rows(A).Cells("txtItemEstoque").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.txtItemEstoque = ""

                    End Try

                    Try
                        OrdemServico.txtAcabamento = DGVListaMaterialSW.Rows(A).Cells("Acabamento").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.txtAcabamento = ""

                    End Try


                    Try
                        fator = DGVListaMaterialSW.Rows(A).Cells("fator").Value.ToString
                    Catch ex As Exception

                        fator = 0

                    End Try

                    OrdemServico.IDOrdemServico = NovoIdOrdemServicoDB

                    ProgressBarProcessoLiberacaoOrdemServico.Value = A

                    Dim query As String = "INSERT INTO ordemservicoitem (
                            IDOrdemServico, idProjeto, PROJETO, idTag, TAG, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CRIADOPOR, 
                            DATACRIACAO, ESTATUS, ACABAMENTO, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IDOrdemServico, @idProjeto, @PROJETO, @idTag, @TAG, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CRIADOPOR, 
                            @DATACRIACAO, @ESTATUS, @ACABAMENTO, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                    Using command As New MySqlCommand(query, myconect)
                        ' Adicionando os parâmetros
                        command.Parameters.AddWithValue("@IDOrdemServico", OrdemServico.IDOrdemServico)
                        command.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
                        command.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
                        command.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
                        command.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
                        command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.ESTATUS)
                        command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                        command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                        command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                        command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                        command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                        command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                        command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                        command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                        command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                        command.Parameters.AddWithValue("@Peso", OrdemServico.Peso)
                        command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                        command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                        command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                        command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                        command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                        command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                        command.Parameters.AddWithValue("@DtCad", "")
                        command.Parameters.AddWithValue("@UsuarioCriacao", "")
                        command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                        command.Parameters.AddWithValue("@DtAlteracao", "")
                        command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                        command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSw)
                        command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                        command.Parameters.AddWithValue("@QtdeProduzida", "")
                        command.Parameters.AddWithValue("@QtdeFaltante", "")
                        command.Parameters.AddWithValue("@CRIADOPOR", Usuario.NomeCompleto.ToString)
                        command.Parameters.AddWithValue("@DATACRIACAO", Date.Now)
                        command.Parameters.AddWithValue("@ESTATUS", "A")
                        command.Parameters.AddWithValue("@ACABAMENTO", OrdemServico.txtAcabamento)
                        command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                        command.Parameters.AddWithValue("@fator", Fator)
                        command.Parameters.AddWithValue("@qtde", OrdemServico.Qtde)
                        command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                        command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                        command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                        command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                        command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                        command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                        command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                        command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                        command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                        command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                        command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                        command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                        command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                        command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                        command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                        command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                        command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                        command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                        ' Abrir conexão e executar comando

                        'If cl_BancoDados.AbrirBanco = False Then
                        '    cl_BancoDados.AbrirBanco()

                        'End If
                        'myconect.Open()
                        command.ExecuteNonQuery()
                    End Using

                Next

            End If

            Timerdgvos.Enabled = True
            TimerDGVListaMaterialSW.Enabled = True

            For i As Integer = 0 To dgvos.Rows.Count = 1

                If NovoIdOrdemServicoDB = dgvos.Rows(i).Cells("IDOrdemServico").Value Then

                    dgvos.Rows(i).Selected = True
                    dgvos.Rows(i).DefaultCellStyle.BackColor = Color.Yellow ' Define a cor de fundo para destacar a linha
                    dgvos.FirstDisplayedScrollingRowIndex = i ' Rola o DataGridView até a linha selecionada

                    Exit Sub

                End If

            Next

            ProgressBarProcessoLiberacaoOrdemServico.Value = 0

            MessageBox.Show("Operação finalizada com sucesso, os itens foram inseridos na OS!, porem não foram importados os aquivos para OS.")

        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)


        ' Verifique se o modelo foi aberto com sucesso
        If Not swModel Is Nothing Then

            ' Usa Select Case para diferenciar o tipo do documento
            If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir o desenho corrente na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Item na OS", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then

                    ' Dim rnc As String

                    Dim novaqtde As String = 0

                    Dim PecaNova As Boolean = False

                    novaqtde = InputBox("Informe a quantidade total de peças a serem fabricadas", "Quantidade Total", 1)

                    ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                    If novaqtde = "" Or novaqtde <= 0 Then

                        MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                        Exit Sub ' Sai do procedimento
                    End If


                    If DGVListaMaterialSW.Rows.Count > 0 Then

                        ' If DGVListaMaterialSW.Rows.Count >= 1 Then

                        For b As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1


                            If DadosArquivoCorrente.NomeArquivoSemExtensao = DGVListaMaterialSW.Rows(b).Cells("CodMatFabricante").Value.ToString Then

                                Dim Peso, areapintura, fator, IDOrdemServicoITEM As String

                                IDOrdemServicoITEM = DGVListaMaterialSW.Rows(b).Cells("IDOrdemServicoITEM").Value.ToString

                                Try
                                    Peso = DadosArquivoCorrente.Massa
                                    Peso = Replace((Peso), ".", ",")
                                    Peso = Peso * novaqtde
                                    ' Peso = Replace(Peso, ",", ".")

                                Catch ex As Exception
                                    Peso = 0
                                End Try


                                fator = DGVListaMaterialSW.Rows(b).Cells("fator").Value

                                Try

                                    areapintura = DadosArquivoCorrente.AreaPintura
                                    areapintura = areapintura * novaqtde
                                    ' areapintura = Replace(areapintura, ",", ".")
                                Catch ex As Exception
                                    areapintura = 0
                                End Try

                                'areapintura = (DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitaria").Value * novaqtde)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Qtde", novaqtde, "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", Replace((areapintura), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Replace((Peso), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Fator", fator, "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "PesoUnitario", Replace((DadosArquivoCorrente.Massa), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPinturaUnitario", Replace((DadosArquivoCorrente.AreaPintura), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)


                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("Qtde").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Value = Replace((areapintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Value = Replace((Peso), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Value = Replace((DadosArquivoCorrente.AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Value = Replace((DadosArquivoCorrente.Massa), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("Fator").Value = fator

                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Qtde").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Fator").Style.BackColor = Color.LightGreen


                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Style.BackColor = Color.LightGreen


                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Style.BackColor = Color.LightGreen

                                PecaNova = False


                                DGVListaMaterialSW.Rows(b).Selected = True
                                DGVListaMaterialSW.Rows(b).DefaultCellStyle.BackColor = Color.Yellow ' Define a cor de fundo para destacar a linha
                                DGVListaMaterialSW.FirstDisplayedScrollingRowIndex = b ' Rola o DataGridView até a linha selecionada

                                Exit Sub

                            End If

                            PecaNova = True


                        Next



                    Else

                        PecaNova = True

                        If PecaNova = True Then



                            Try
                                OrdemServico.CodMatFabricante = DadosArquivoCorrente.NomeArquivoSemExtensao
                            Catch ex As Exception
                                OrdemServico.CodMatFabricante = ""
                            End Try

                            Try
                                OrdemServico.DescResumo = DadosArquivoCorrente.Titulo
                            Catch ex As Exception
                                OrdemServico.DescResumo = ""
                            End Try

                            Try
                                OrdemServico.DescDetal = DadosArquivoCorrente.AssuntoSubiTitulo
                            Catch ex As Exception

                                OrdemServico.DescDetal = ""
                            End Try

                            Try
                                OrdemServico.Autor = DadosArquivoCorrente.Author
                            Catch ex As Exception

                                OrdemServico.Autor = ""
                            End Try

                            Try
                                OrdemServico.Palavrachave = DadosArquivoCorrente.PalavraChave
                            Catch ex As Exception

                                OrdemServico.Palavrachave = ""
                            End Try

                            Try
                                OrdemServico.Notas = DadosArquivoCorrente.Comentarios
                            Catch ex As Exception

                                OrdemServico.Notas = ""
                            End Try

                            Try
                                OrdemServico.Espessura = DadosArquivoCorrente.Espessura
                            Catch ex As Exception

                                OrdemServico.Espessura = ""
                            End Try

                            Try
                                OrdemServico.NumeroDobras = DadosArquivoCorrente.NumeroDobras
                            Catch ex As Exception

                                OrdemServico.NumeroDobras = ""
                            End Try


                            Try
                                OrdemServico.EnderecoArquivo = DadosArquivoCorrente.EnderecoArquivo.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.EnderecoArquivo = ""
                            End Try


                            Try
                                OrdemServico.MaterialSw = DadosArquivoCorrente.Material.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.MaterialSw = ""
                            End Try

                            If DadosArquivoCorrente.EnderecoArquivo.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                                OrdemServico.Unidade = "CONJ"
                                OrdemServico.UnidadeSW = "CONJ"
                            Else

                                OrdemServico.Unidade = "PC"
                                OrdemServico.UnidadeSW = "PC"

                            End If

                            OrdemServico.DtCad = ""
                            OrdemServico.UsuarioCriacao = ""
                            OrdemServico.UsuarioAlteracao = ""
                            OrdemServico.DtAlteracao = ""

                            Try
                                OrdemServico.Qtde = novaqtde
                            Catch ex As Exception

                                OrdemServico.Qtde = novaqtde

                            End Try

                            Try


                                DadosArquivoCorrente.AreaPintura = Replace(DadosArquivoCorrente.AreaPintura, ".", ",")
                                OrdemServico.AreaPintura = DadosArquivoCorrente.AreaPintura * novaqtde
                                OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ".", ",")

                            Catch ex As Exception

                                OrdemServico.AreaPintura = ""

                            End Try


                            Try

                                ' OrdemServico.Peso = DadosArquivoCorrente.Massa
                                DadosArquivoCorrente.Massa = Replace(DadosArquivoCorrente.Massa, ".", ",")
                                OrdemServico.Peso = DadosArquivoCorrente.Massa * novaqtde
                                OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")

                            Catch ex As Exception

                                OrdemServico.Peso = 0
                            End Try


                            Try
                                OrdemServico.PesoUnitario = DadosArquivoCorrente.Massa
                                OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ",")

                            Catch ex As Exception

                                OrdemServico.PesoUnitario = 0
                            End Try

                            Try
                                OrdemServico.txtSoldagem = DadosArquivoCorrente.soldagem
                            Catch ex As Exception

                                OrdemServico.txtSoldagem = ""

                            End Try

                            Try
                                OrdemServico.QtdeTotal = novaqtde

                            Catch ex As Exception

                                OrdemServico.QtdeTotal = 0

                            End Try

                            Try
                                OrdemServico.txtTipoDesenho = DadosArquivoCorrente.TipoDesenho
                            Catch ex As Exception

                                OrdemServico.txtTipoDesenho = ""

                            End Try

                            Try
                                OrdemServico.txtCorte = DadosArquivoCorrente.Corte
                            Catch ex As Exception

                                OrdemServico.txtCorte = ""

                            End Try

                            Try
                                OrdemServico.txtDobra = DadosArquivoCorrente.Dobra
                            Catch ex As Exception

                                OrdemServico.txtDobra = ""

                            End Try

                            Try
                                OrdemServico.txtSolda = DadosArquivoCorrente.Solda
                            Catch ex As Exception

                                OrdemServico.txtSolda = ""

                            End Try

                            Try
                                OrdemServico.txtPintura = DadosArquivoCorrente.Pintura
                            Catch ex As Exception

                                OrdemServico.txtPintura = ""

                            End Try

                            Try
                                OrdemServico.txtMontagem = DadosArquivoCorrente.Montagem
                            Catch ex As Exception

                                OrdemServico.txtMontagem = ""

                            End Try

                            Try
                                OrdemServico.Comprimentocaixadelimitadora = DadosArquivoCorrente.Alturacaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Comprimentocaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.Larguracaixadelimitadora = DadosArquivoCorrente.Larguracaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Larguracaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.Espessuracaixadelimitadora = DadosArquivoCorrente.Profundidadeaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Espessuracaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.txtItemEstoque = DadosArquivoCorrente.ItemEstoque
                            Catch ex As Exception

                                OrdemServico.txtItemEstoque = ""

                            End Try

                            Try
                                OrdemServico.txtAcabamento = DadosArquivoCorrente.acabamento
                            Catch ex As Exception

                                OrdemServico.txtAcabamento = ""

                            End Try


                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                            OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                            ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                            Dim query As String = "INSERT INTO ordemservicoitem (
                            IDOrdemServico, idProjeto, PROJETO, idTag, TAG, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CRIADOPOR, 
                            DATACRIACAO, ESTATUS, ACABAMENTO, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IDOrdemServico, @idProjeto, @PROJETO, @idTag, @TAG, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CRIADOPOR, 
                            @DATACRIACAO, @ESTATUS, @ACABAMENTO, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                            Using command As New MySqlCommand(query, myconect)
                                ' Adicionando os parâmetros
                                command.Parameters.AddWithValue("@IDOrdemServico", OrdemServico.IDOrdemServico)
                                command.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
                                command.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
                                command.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
                                command.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
                                command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.ESTATUS)
                                command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                                command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                                command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                command.Parameters.AddWithValue("@Peso", OrdemServico.Peso)
                                command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                                command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                command.Parameters.AddWithValue("@DtCad", "")
                                command.Parameters.AddWithValue("@UsuarioCriacao", "")
                                command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                command.Parameters.AddWithValue("@DtAlteracao", "")
                                command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSw)
                                command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                command.Parameters.AddWithValue("@QtdeProduzida", "")
                                command.Parameters.AddWithValue("@QtdeFaltante", "")
                                command.Parameters.AddWithValue("@CRIADOPOR", Usuario.NomeCompleto.ToString)
                                command.Parameters.AddWithValue("@DATACRIACAO", Date.Now)
                                command.Parameters.AddWithValue("@ESTATUS", "A")
                                command.Parameters.AddWithValue("@ACABAMENTO", OrdemServico.txtAcabamento)
                                command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                command.Parameters.AddWithValue("@fator", 1)
                                command.Parameters.AddWithValue("@qtde", OrdemServico.Qtde)
                                command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                                command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                                command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                                command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                                command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                                command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                                command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                                command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                                command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                                ' Abrir conexão e executar comando

                                'If cl_BancoDados.AbrirBanco = False Then
                                '    cl_BancoDados.AbrirBanco()

                                'End If
                                'myconect.Open()
                                command.ExecuteNonQuery()
                            End Using
                            TimerDGVListaMaterialSW.Enabled = True
                        End If

                        PecaNova = False

                    End If

                    ' PecaNova = True

                    If PecaNova = True Then



                        Try
                            OrdemServico.CodMatFabricante = DadosArquivoCorrente.NomeArquivoSemExtensao
                        Catch ex As Exception
                            OrdemServico.CodMatFabricante = ""
                        End Try

                        Try
                            OrdemServico.DescResumo = DadosArquivoCorrente.Titulo
                        Catch ex As Exception
                            OrdemServico.DescResumo = ""
                        End Try

                        Try
                            OrdemServico.DescDetal = DadosArquivoCorrente.AssuntoSubiTitulo
                        Catch ex As Exception

                            OrdemServico.DescDetal = ""
                        End Try

                        Try
                            OrdemServico.Autor = DadosArquivoCorrente.Author
                        Catch ex As Exception

                            OrdemServico.Autor = ""
                        End Try

                        Try
                            OrdemServico.Palavrachave = DadosArquivoCorrente.PalavraChave
                        Catch ex As Exception

                            OrdemServico.Palavrachave = ""
                        End Try

                        Try
                            OrdemServico.Notas = DadosArquivoCorrente.Comentarios
                        Catch ex As Exception

                            OrdemServico.Notas = ""
                        End Try

                        Try
                            OrdemServico.Espessura = DadosArquivoCorrente.Espessura
                        Catch ex As Exception

                            OrdemServico.Espessura = ""
                        End Try

                        Try
                            OrdemServico.NumeroDobras = DadosArquivoCorrente.NumeroDobras
                        Catch ex As Exception

                            OrdemServico.NumeroDobras = ""
                        End Try


                        Try
                            OrdemServico.EnderecoArquivo = DadosArquivoCorrente.EnderecoArquivo.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.EnderecoArquivo = ""
                        End Try


                        Try
                            OrdemServico.MaterialSw = DadosArquivoCorrente.Material.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.MaterialSw = ""
                        End Try

                        If DadosArquivoCorrente.EnderecoArquivo.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                            OrdemServico.Unidade = "CONJ"
                            OrdemServico.UnidadeSW = "CONJ"
                        Else

                            OrdemServico.Unidade = "PC"
                            OrdemServico.UnidadeSW = "PC"

                        End If

                        OrdemServico.DtCad = ""
                        OrdemServico.UsuarioCriacao = ""
                        OrdemServico.UsuarioAlteracao = ""
                        OrdemServico.DtAlteracao = ""

                        Try
                            OrdemServico.Qtde = novaqtde
                        Catch ex As Exception

                            OrdemServico.Qtde = novaqtde

                        End Try

                        Try


                            DadosArquivoCorrente.AreaPintura = Replace(DadosArquivoCorrente.AreaPintura, ".", ",")
                            OrdemServico.AreaPintura = DadosArquivoCorrente.AreaPintura * novaqtde
                            OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ".", ",")

                        Catch ex As Exception

                            OrdemServico.AreaPintura = ""

                        End Try


                        Try

                            ' OrdemServico.Peso = DadosArquivoCorrente.Massa
                            DadosArquivoCorrente.Massa = Replace(DadosArquivoCorrente.Massa, ".", ",")
                            OrdemServico.Peso = DadosArquivoCorrente.Massa * novaqtde
                            OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")

                        Catch ex As Exception

                            OrdemServico.Peso = 0
                        End Try


                        Try
                            OrdemServico.PesoUnitario = DadosArquivoCorrente.Massa
                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ",")

                        Catch ex As Exception

                            OrdemServico.PesoUnitario = 0
                        End Try

                        Try
                            OrdemServico.txtSoldagem = DadosArquivoCorrente.soldagem
                        Catch ex As Exception

                            OrdemServico.txtSoldagem = ""

                        End Try

                        Try
                            OrdemServico.QtdeTotal = novaqtde

                        Catch ex As Exception

                            OrdemServico.QtdeTotal = 0

                        End Try

                        Try
                            OrdemServico.txtTipoDesenho = DadosArquivoCorrente.TipoDesenho
                        Catch ex As Exception

                            OrdemServico.txtTipoDesenho = ""

                        End Try

                        Try
                            OrdemServico.txtCorte = DadosArquivoCorrente.Corte
                        Catch ex As Exception

                            OrdemServico.txtCorte = ""

                        End Try

                        Try
                            OrdemServico.txtDobra = DadosArquivoCorrente.Dobra
                        Catch ex As Exception

                            OrdemServico.txtDobra = ""

                        End Try

                        Try
                            OrdemServico.txtSolda = DadosArquivoCorrente.Solda
                        Catch ex As Exception

                            OrdemServico.txtSolda = ""

                        End Try

                        Try
                            OrdemServico.txtPintura = DadosArquivoCorrente.Pintura
                        Catch ex As Exception

                            OrdemServico.txtPintura = ""

                        End Try

                        Try
                            OrdemServico.txtMontagem = DadosArquivoCorrente.Montagem
                        Catch ex As Exception

                            OrdemServico.txtMontagem = ""

                        End Try

                        Try
                            OrdemServico.Comprimentocaixadelimitadora = DadosArquivoCorrente.Alturacaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Comprimentocaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.Larguracaixadelimitadora = DadosArquivoCorrente.Larguracaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Larguracaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.Espessuracaixadelimitadora = DadosArquivoCorrente.Profundidadeaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Espessuracaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.txtItemEstoque = DadosArquivoCorrente.ItemEstoque
                        Catch ex As Exception

                            OrdemServico.txtItemEstoque = ""

                        End Try

                        Try
                            OrdemServico.txtAcabamento = DadosArquivoCorrente.acabamento
                        Catch ex As Exception

                            OrdemServico.txtAcabamento = ""

                        End Try


                        OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                        OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                        ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                        Dim query As String = "INSERT INTO ordemservicoitem (
                            IDOrdemServico, idProjeto, PROJETO, idTag, TAG, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CRIADOPOR, 
                            DATACRIACAO, ESTATUS, ACABAMENTO, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IDOrdemServico, @idProjeto, @PROJETO, @idTag, @TAG, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CRIADOPOR, 
                            @DATACRIACAO, @ESTATUS, @ACABAMENTO, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                        Using command As New MySqlCommand(query, myconect)
                            ' Adicionando os parâmetros
                            command.Parameters.AddWithValue("@IDOrdemServico", OrdemServico.IDOrdemServico)
                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
                            command.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
                            command.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
                            command.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.ESTATUS)
                            command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                            command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                            command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                            command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                            command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                            command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                            command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                            command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                            command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                            command.Parameters.AddWithValue("@Peso", OrdemServico.Peso)
                            command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                            command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                            command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                            command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                            command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                            command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                            command.Parameters.AddWithValue("@DtCad", "")
                            command.Parameters.AddWithValue("@UsuarioCriacao", "")
                            command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                            command.Parameters.AddWithValue("@DtAlteracao", "")
                            command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSw)
                            command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                            command.Parameters.AddWithValue("@QtdeProduzida", "")
                            command.Parameters.AddWithValue("@QtdeFaltante", "")
                            command.Parameters.AddWithValue("@CRIADOPOR", Usuario.NomeCompleto.ToString)
                            command.Parameters.AddWithValue("@DATACRIACAO", Date.Now)
                            command.Parameters.AddWithValue("@ESTATUS", "A")
                            command.Parameters.AddWithValue("@ACABAMENTO", OrdemServico.txtAcabamento)
                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                            command.Parameters.AddWithValue("@fator", 1)
                            command.Parameters.AddWithValue("@qtde", OrdemServico.Qtde)
                            command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                            command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                            command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                            command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                            command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                            command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                            command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                            command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                            command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                            command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                            command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                            command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                            command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                            command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                            command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                            command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                            command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                            command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                            ' Abrir conexão e executar comando

                            'If cl_BancoDados.AbrirBanco = False Then
                            '    cl_BancoDados.AbrirBanco()

                            'End If
                            'myconect.Open()
                            command.ExecuteNonQuery()
                        End Using
                        TimerDGVListaMaterialSW.Enabled = True
                    End If



                End If

            End If

        End If


    End Sub

    Private Sub GerarPDFDasLinhasSelecionadasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarPDFDasLinhasSelecionadasToolStripMenuItem.Click



        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em PDF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                '   IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                Dim novaqtde As String = 0

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                        Try

                            DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString
                            DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)
                            ' novaqtde = DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Value.ToString

                            OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)



                            ' Verifica se EnderecoArquivo não é nulo ou vazio antes de realizar a substituição
                            If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then

                                Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()

                                ' Converte o caminho do arquivo para .SLDDRW se ele terminar com .SLDPRT ou .SLDASM

                                If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                                    enderecoArquivoUpper = enderecoArquivoUpper.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                                End If

                                If File.Exists(enderecoArquivoUpper) Then
                                    OpenDocumentAndWait(enderecoArquivoUpper, True, swModel)
                                    DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)
                                    ' swapp.CloseDoc(arquivoSLDDRW)
                                Else

                                    DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)

                                End If

                            End If


                            swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                            cl_BancoDados.FecharArquivoMemoria()
                            IntanciaSolidWorks.LiberarRecurso(swModel)
                            IntanciaSolidWorks.LiberarRecurso(swPart)

                            Dim enredecoPDF As String

                            ' Altera a extensão do arquivo para .PDF, independentemente de ser .SLDPRT, .sldprt, .SLDASM ou .sldasm
                            enredecoPDF = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".PDF")

                            ' Verifica se o arquivo PDF existe
                            If File.Exists(enredecoPDF) Then
                                DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                            Else
                                DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.Sem_Incone
                            End If


                            ProgressBarProcessoLiberacaoOrdemServico.Value = i
                        Catch ex As Exception
                            Continue For

                        End Try

                        DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                        '  ImportarPDFParaOSIndividual(DadosArquivoCorrente.EnderecoArquivo, novaqtde)

                    End If

                Next

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")

                ProgressBarProcessoLiberacaoOrdemServico.Value = 0

            End If

        End If

    End Sub

    Private Sub GerarDXFDasLinhasSelecionadasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarDXFDasLinhasSelecionadasToolStripMenuItem.Click


        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em DXF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    '  Try


                    If Convert.ToBoolean(DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value) = True Then

                        DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString

                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                        Dim enderecoArquivo As String = DadosArquivoCorrente.EnderecoArquivo

                        ' Verifica se EnderecoArquivo não é nulo e o arquivo realmente existe antes de tentar exportar
                        If Not String.IsNullOrEmpty(enderecoArquivo) AndAlso File.Exists(enderecoArquivo) Then
                            DadosArquivoCorrente.ExportDXF(swModel, False, True)
                        End If

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        Dim enredecoDxf As String

                        ' Altera a extensão do arquivo para .DXF, independentemente de ser .SLDPRT ou .sldprt
                        enredecoDxf = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".DXF")

                        ' Verifica se o arquivo DXF existe
                        If File.Exists(enredecoDxf) Then
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                        Else
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.Sem_Incone
                        End If
                        ProgressBarProcessoLiberacaoOrdemServico.Value = i


                        DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                    End If

                    'Catch ex As Exception
                    '    Continue For

                    'End Try


                Next
                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")
                ProgressBarProcessoLiberacaoOrdemServico.Value = 0
            End If
        End If

    End Sub

    Private Sub txtAuthor_TextChanged(sender As Object, e As EventArgs) Handles txtAuthor.TextChanged

        Try


            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                swModel.SummaryInfo(swSummInfoField_e.swSumInfoAuthor) = Me.txtAuthor.Text
                swModel.SaveSilent()

            End If

        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub CancelarLiberaçãoDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelarLiberaçãoDaOSToolStripMenuItem.Click
        If OrdemServico.IDOrdemServico.ToString = Nothing Or OrdemServico.IDOrdemServico.ToString = "" Then

            MsgBox("Não há OS Selecionada!", vbCritical, "Atenção")
            Exit Sub
        End If
        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Cancelar a Liberação da Ordem de Serviço: " & OrdemServico.IDOrdemServico, "Cancelando Ordem de Serviço", MessageBoxButtons.YesNo)
        Dim totalExecutado As Integer
        Try

            totalExecutado = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT  count(idplanodecorte) + 
                   count(CorteTotalExecutado) + count(DobraTotalExecutado)+ count(SoldaTotalExecutado) +
                   count(PinturaTotalExecutado) +  count(MontagemTotalExecutado) as totalExecutado  
                   FROM ordemservicoitem where idordemservico  ='" & OrdemServico.IDOrdemServico & "' 
                   and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');", "totalExecutado"))

        Catch ex As Exception

            totalExecutado = 0

        Finally

        End Try

        If result = DialogResult.Yes Then

            If totalExecutado > 0 Then

                Dim dtTabelaPlanoCorte As New System.Data.DataTable()

                dtTabelaPlanoCorte = cl_BancoDados.CarregarDados("SELECT  idplanodecorte, codmatfabricante  
                     FROM ordemservicoitem where idordemservico  = '" & OrdemServico.IDOrdemServico & "' and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');")

                Dim MessagemItens As String

                For I As Integer = 0 To dtTabelaPlanoCorte.Rows.Count - 1

                    MessagemItens = MessagemItens & "PlanoCorte = " & dtTabelaPlanoCorte.Rows(I).Item("idplanodecorte").ToString &
                    " Numero Desenho: = " & dtTabelaPlanoCorte.Rows(I).Item("codmatfabricante").ToString & vbCrLf

                Next

                MsgBox("A OS Numero: " & OrdemServico.IDOrdemServico & " contem processos em andamento, por este motivo não pode ser cancelada, ver plano de corte's: " & vbCrLf & MessagemItens, vbCritical, "Atenção!")

                Exit Sub

            End If


            OrdemServico.ENDERECOOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString

            '       cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IDOrdemServico", OrdemServico.IDOrdemServico)

            Dim diretorio As String = OrdemServico.ENDERECOOrdemServico

            LimparDiretorio(diretorio & "\PDF")
            LimparDiretorio(diretorio & "\DXF")
            LimparDiretorio(diretorio & "\DFT")
            LimparDiretorio(diretorio & "\LXDS")


            cl_BancoDados.Salvar("Update ordemservico set LIBERADO_ENGENHARIA = '', 
                        DATA_LIBERACAO_ENGENHARIA = '' 
                        where IDOrdemServico = '" & OrdemServico.IDOrdemServico & "'")

            cl_BancoDados.Salvar("Update ordemservicoitem set LIBERADO_ENGENHARIA = '', 
                        DATA_LIBERACAO_ENGENHARIA = '' 
                        where IDOrdemServico = '" & OrdemServico.IDOrdemServico & "'")


            'cl_BancoDados.AlteracaoEspecifica("ordemservico", "LIBERADO_ENGENHARIA", "", "IDOrdemServico", OrdemServico.IDOrdemServico)
            'cl_BancoDados.AlteracaoEspecifica("ordemservico", "DATA_LIBERACAO_ENGENHARIA", "", "IDOrdemServico", OrdemServico.IDOrdemServico)
            'esta faltando nome usuario

            'esta faltando nome usuario

            'cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "LIBERADO_ENGENHARIA", "", "IDOrdemServico", OrdemServico.IDOrdemServico)
            'cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "DATA_LIBERACAO_ENGENHARIA", "", "IDOrdemServico", OrdemServico.IDOrdemServico)

            dgvos.CurrentRow.Cells("LIBERADO_ENGENHARIA").Value = ""
            dgvos.CurrentRow.Cells("DATA_LIBERACAO_ENGENHARIA").Value = ""
            dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.atencao
            dgvos.Refresh()


            TimerFiltroPecaAtivaOS.Enabled = True

        Else


            MsgBox("Esta opração não e valida para OS: " & OrdemServico.IDOrdemServico & ", há processo ja executados, a opção será o cancelamento!", vbCritical, "Atenção")


        End If




    End Sub

    Private Sub TrocarParaFormato4ADeitadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrocarParaFormato4ADeitadoToolStripMenuItem.Click


        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                '  Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                ' Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                ' Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                'swModelDocExt = swModel.Extension
                'swDrawing = swModel
                'sheetNames(0) = "Sheet2"
                'sheetNames(1) = "Sheet3"
                'sheetNameArray = sheetNames
                'swDrawing.SetSheetsSelected(sheetNameArray)
                'status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPaperA4size, swDwgTemplates_e.swDwgTemplateCustom, 0.297, 0.21, True, My.Settings.EnderecoNovoFormatoA4.ToString, 0.297, 0.21, "Default", True, 0, 0, 0, 0, 0, 0)


                Dim currentSheetScale As Double
                Dim scaleStatus As Boolean

                ' Obtém a escala da folha atual
                scaleStatus = swDrawing.GetCurrentSheetScale(currentSheetScale)

                If scaleStatus Then
                    ' Configura a nova folha mantendo a escala atual
                    status = swDrawing.SetupSheet6(
                                       "Sheet3",
                                       swDwgPaperSizes_e.swDwgPaperA4size,
                                       swDwgTemplates_e.swDwgTemplateCustom,
                                       0.297, 0.21,
                                       True,
                                       My.Settings.EnderecoNovoFormatoA4.ToString,
                                       0.297, 0.21,
                                       "Default",
                                       True,
                                       currentSheetScale,
                                       currentSheetScale,
                                       0, 0, 0, 0)
                Else
                    MsgBox("Falha ao obter a escala atual da folha.")
                End If


                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If



    End Sub

    Private Sub BiscarFormatoA4DeitadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BiscarFormatoA4DeitadoToolStripMenuItem.Click

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A4 (*.slddrt)|*.slddrt"

        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA4Deitado = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If


    End Sub

    Private Sub cboTitulo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTitulo.SelectedValueChanged
        Try

        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub cboTitulo_Click(sender As Object, e As EventArgs) Handles cboTitulo.Click
        Try

        Catch ex As Exception
        Finally
        End Try
    End Sub



    Private Sub btnPendencias_Click(sender As Object, e As EventArgs) Handles btnPendencias.Click



        PendenciasRNC.ShowDialog()



    End Sub

    Dim DescricaoFinalizacao As String




    Private Sub dgvDataGridBOM_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDataGridBOM.DataBindingComplete

        For Each row As DataGridViewRow In DGVListaMaterialSW.Rows
            Dim valorEnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()
            Dim valorProdutoPrincipal As String = If(row.Cells("ProdutoPrincipal").Value, "").ToString()

            ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
            If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
           valorProdutoPrincipal.IndexOf("SIM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                row.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define outra imagem se for .SLDPRT
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemPRT
            Else
                row.Cells("dgvIconeItemOS").Value = My.Resources.material_escolar_32
            End If

        Next



    End Sub

    Private Sub DGVListaMaterialSW_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVListaMaterialSW.CellContentClick

    End Sub

    Private Sub DGVListaMaterialSW_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGVListaMaterialSW.DataBindingComplete

        For Each row As DataGridViewRow In DGVListaMaterialSW.Rows
            Dim valorEnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()
            Dim valorProdutoPrincipal As String = If(row.Cells("ProdutoPrincipal").Value, "").ToString()

            ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
            If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
           valorProdutoPrincipal.IndexOf("SIM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                row.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define outra imagem se for .SLDPRT
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemPRT
            Else
                row.Cells("dgvIconeItemOS").Value = My.Resources.material_escolar_32
            End If


            Dim dxf, pdf As String


            ' Dim enderecoArquivo As String = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString()

            ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
            If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                dxf = Path.ChangeExtension(valorEnderecoArquivo, ".dxf")

                ' Verifica se o arquivo DXF existe
                If File.Exists(dxf) Then
                    row.Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                Else
                    row.Cells("DGVDXF").Value = My.Resources.Sem_Incone
                End If
            End If

            ' Altera para .pdf
            If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                pdf = Path.ChangeExtension(valorEnderecoArquivo, ".pdf")

                ' Verifica se o arquivo PDF existe
                If File.Exists(pdf) Then
                    row.Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                Else
                    row.Cells("DGVPDF").Value = My.Resources.Sem_Incone
                End If
            End If



        Next


    End Sub

    Private Sub chkBoxTipoDesenho_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chkBoxTipoDesenho.ItemCheck

        ' Obtem o CheckedListBox
        chkBoxTipoDesenho = CType(sender, CheckedListBox)

        ' Se o item está sendo marcado, desmarque os outros
        If e.NewValue = CheckState.Checked Then

            Try
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txttipodesenho", chkBoxTipoDesenho.Text, chkBoxTipoDesenho.Text)

                swModel.SaveSilent()

            Catch ex As Exception
                '   MsgBox(ex.Message)
            Finally

            End Try

            For i As Integer = 0 To chkBoxTipoDesenho.Items.Count - 1
                ' Apenas desmarque itens diferentes do que foi alterado
                If i <> e.Index Then
                    chkBoxTipoDesenho.SetItemChecked(i, False)
                End If
            Next
        End If

    End Sub

    Private Sub chkBoxAcabamento_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chkBoxAcabamento.ItemCheck

        ' Obtem o CheckedListBox
        chkBoxAcabamento = CType(sender, CheckedListBox)

        ' Se o item está sendo marcado, desmarque os outros
        If e.NewValue = CheckState.Checked Then

            Try
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtacabamento",
                                                                chkBoxAcabamento.Text, chkBoxAcabamento.Text)

                swModel.SaveSilent()

            Catch ex As Exception
                '   MsgBox(ex.Message)
            Finally

            End Try

            For i As Integer = 0 To chkBoxAcabamento.Items.Count - 1
                ' Apenas desmarque itens diferentes do que foi alterado
                If i <> e.Index Then
                    chkBoxAcabamento.SetItemChecked(i, False)
                End If
            Next
        End If

    End Sub

    Private Sub DGVMontaPeca_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVMontaPeca.CellContentClick

    End Sub

    Private Sub tsBLerDados_Click(sender As Object, e As EventArgs) Handles tsBLerDados.Click

        Try

            DadosArquivoCorrente.ArquivoCorrente(swModel)

            DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

            'dados da caixa delimitadora
            DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

            AtualizaTela(swModel)

        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub tsbSalvar_Click(sender As Object, e As EventArgs) Handles tsbSalvar.Click

        Try




            DadosArquivoCorrente.AtualizaDesenho(swModel)

            'If swModel Is Nothing Then

            '    Exit Sub

            'End If

            swModel.GraphicsRedraw2()

            ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
            swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)


            'Verifica se o desenhop atualizado esta carregado na BOM se sim, atualiza os dados do desenho'
            If dgvDataGridBOM.Rows.Count > 0 Then


                For i As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                    If DadosArquivoCorrente.NomeArquivoSemExtensao.ToString.Trim = dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Value.ToString.Trim Then

                        ' Adicione os parâmetros ao comando
                        dgvDataGridBOM.Rows(i).Cells("DescResumo").Value = Me.cboTitulo.Text ' UCase(DadosArquivoCorrente.Titulo)
                        dgvDataGridBOM.Rows(i).Cells("DescDetal").Value = Me.txtAssuntoSubiTitulo.Text ' UCase(DadosArquivoCorrente.AssuntoSubiTitulo)
                        dgvDataGridBOM.Rows(i).Cells("Autor").Value = Me.txtAuthor.Text ' UCase(DadosArquivoCorrente.Author)
                        dgvDataGridBOM.Rows(i).Cells("Palavrachave").Value = Me.txtPalavraChave.Text ' UCase(DadosArquivoCorrente.PalavraChave)
                        dgvDataGridBOM.Rows(i).Cells("Notas").Value = Me.txtComentarios.Text ' UCase(DadosArquivoCorrente.Comentarios)
                        dgvDataGridBOM.Rows(i).Cells("Espessura").Value = Me.lblEspessura.Text '  UCase(DadosArquivoCorrente.Espessura)
                        dgvDataGridBOM.Rows(i).Cells("AreaPintura").Value = Me.lblAreaPintura.Text ' UCase(DadosArquivoCorrente.AreaPintura)
                        dgvDataGridBOM.Rows(i).Cells("NumeroDobras").Value = Me.lblNumeroDobra.Text ' UCase(DadosArquivoCorrente.NumeroDobras)
                        dgvDataGridBOM.Rows(i).Cells("Peso").Value = Me.lblPeso.Text  ' UCase(DadosArquivoCorrente.Massa)
                        'dgvDataGridBOM.Rows(i).Cells("Unidade").Value = "PC"
                        dgvDataGridBOM.Rows(i).Cells("Altura").Value = Me.lblComprimento.Text ' UCase(DadosArquivoCorrente.ComprimentoBlank)
                        dgvDataGridBOM.Rows(i).Cells("Largura").Value = Me.lblLargura.Text ' UCase(DadosArquivoCorrente.LarguraBlank)
                        'dgvDataGridBOM.Rows(i).Cells("Profundidade").Value = ""
                        dgvDataGridBOM.Rows(i).Cells("Material").Value = Me.lblMaterial.Text ' UCase(DadosArquivoCorrente.Material)
                        dgvDataGridBOM.Rows(i).Cells("Acabamento").Value = UCase(DadosArquivoCorrente.acabamento)
                        dgvDataGridBOM.Rows(i).Cells("txtSoldagem").Value = UCase(DadosArquivoCorrente.soldagem)
                        dgvDataGridBOM.Rows(i).Cells("txtTipoDesenho").Value = UCase(DadosArquivoCorrente.TipoDesenho)
                        dgvDataGridBOM.Rows(i).Cells("txtCorte").Value = UCase(DadosArquivoCorrente.Corte)
                        dgvDataGridBOM.Rows(i).Cells("txtDobra").Value = UCase(DadosArquivoCorrente.Dobra)
                        dgvDataGridBOM.Rows(i).Cells("txtSolda").Value = UCase(DadosArquivoCorrente.Solda)
                        dgvDataGridBOM.Rows(i).Cells("txtPintura").Value = UCase(DadosArquivoCorrente.Pintura)
                        dgvDataGridBOM.Rows(i).Cells("txtMontagem").Value = UCase(DadosArquivoCorrente.Montagem)
                        dgvDataGridBOM.Rows(i).Cells("Comprimentocaixadelimitadora").Value = Me.lblAlturaTotalCaixaDelimitadora.Text '  DadosArquivoCorrente.Alturacaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("Larguracaixadelimitadora").Value = Me.lblProfundidadeTotalCaixaDelimitadora.Text ' DadosArquivoCorrente.Larguracaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("Espessuracaixadelimitadora").Value = Me.lblProfundidadeTotalCaixaDelimitadora.Text ' DadosArquivoCorrente.Profundidadeaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("txtItemEstoque").Value = DadosArquivoCorrente.ItemEstoque

                        dgvDataGridBOM.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                        Exit Sub

                    End If

                Next

            End If



        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub tsbConverterDXF_Click(sender As Object, e As EventArgs) Handles tsbConverterDXF.Click

        Try

            'Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                'Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                    IntanciaSolidWorks.ConectarSolidWorks()

                    swModel = swapp.ActiveDoc
                    'swModel = swApparq.ActiveDoc

                    swModel.Visible = True

                    swModelDocExt = swModel.Extension

                    Try

                        DadosArquivoCorrente.ExportDXF(swModel, True, True)

                        ' DadosArquivoCorrente.ExportSheetMetalBlankToDXF(swModel.GetPathName, Path.ChangeExtension(swModel.GetPathName, ".dxf"))

                        chkVerificarDXF.Checked = True

                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em dxf")

                    Finally

                    End Try
                End If
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub TSBConverterPDF_Click(sender As Object, e As EventArgs) Handles TSBConverterPDF.Click

        Try

            ' Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                ' Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                    Try

                        IntanciaSolidWorks.ConectarSolidWorks()
                        'swApparq = CreateObject("SldWorks.Application")

                        swModel = swapp.ActiveDoc
                        'swModel = swApparq.ActiveDoc

                        swModel.Visible = True

                        swModelDocExt = swModel.Extension

                        DadosArquivoCorrente.ExportToPDF(swModel, swModel.GetPathName.ToString, True)

                        chkVerificarPDF.Checked = True


                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em PDF")
                    Finally

                    End Try
                End If
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub TSBAssociarMaterial_Click(sender As Object, e As EventArgs) Handles TSBAssociarMaterial.Click

        If String.IsNullOrWhiteSpace(DadosArquivoCorrente.NomeArquivoSemExtensao) Then

            MessageBox.Show("Não há desenho ativo para associar material.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Exit Sub

        Else
            Using formMateriaisAlmoxarifado As New frmMateriaisAlmoxarifado ' MateriaisAlmoxarifado

                DadosArquivoCorrente.IdMaterial = cl_BancoDados.RetornaCampoDaPesquisa("Select idMaterial from material where codMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "IdMaterial")

                formMateriaisAlmoxarifado.ShowDialog()

                TimerMontaPeca.Enabled = True

            End Using

        End If
    End Sub

    Private Sub tsbInserirNaOS_Click(sender As Object, e As EventArgs) Handles tsbInserirNaOS.Click


        ' Verifique se o modelo foi aberto com sucesso
        If Not swModel Is Nothing Then

            ' Usa Select Case para diferenciar o tipo do documento
            If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir o desenho corrente na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Item na OS", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then

                    ' Dim rnc As String

                    Dim novaqtde As String = 0

                    Dim PecaNova As Boolean = False

                    novaqtde = InputBox("Informe a quantidade total de peças a serem fabricadas", "Quantidade Total", 1)

                    ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                    If novaqtde = "" Or novaqtde <= 0 Then

                        MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                        Exit Sub ' Sai do procedimento
                    End If


                    If DGVListaMaterialSW.Rows.Count > 0 Then

                        ' If DGVListaMaterialSW.Rows.Count >= 1 Then

                        For b As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1


                            If DadosArquivoCorrente.NomeArquivoSemExtensao = DGVListaMaterialSW.Rows(b).Cells("CodMatFabricante").Value.ToString Then

                                Dim Peso, areapintura, fator, IDOrdemServicoITEM As String

                                IDOrdemServicoITEM = DGVListaMaterialSW.Rows(b).Cells("IDOrdemServicoITEM").Value.ToString

                                Try
                                    Peso = DadosArquivoCorrente.Massa
                                    Peso = Replace((Peso), ".", ",")
                                    Peso = Peso * novaqtde
                                    ' Peso = Replace(Peso, ",", ".")

                                Catch ex As Exception
                                    Peso = 0
                                End Try


                                fator = DGVListaMaterialSW.Rows(b).Cells("fator").Value

                                Try

                                    areapintura = DadosArquivoCorrente.AreaPintura
                                    areapintura = areapintura * novaqtde
                                    ' areapintura = Replace(areapintura, ",", ".")
                                Catch ex As Exception
                                    areapintura = 0
                                End Try

                                'areapintura = (DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitaria").Value * novaqtde)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Qtde", novaqtde, "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", Replace((areapintura), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Replace((Peso), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Fator", fator, "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "PesoUnitario", Replace((DadosArquivoCorrente.Massa), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPinturaUnitario", Replace((DadosArquivoCorrente.AreaPintura), ",", "."), "IDOrdemServicoITEM", IDOrdemServicoITEM)


                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("Qtde").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Value = Replace((areapintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Value = Replace((Peso), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Value = Replace((DadosArquivoCorrente.AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Value = Replace((DadosArquivoCorrente.Massa), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("Fator").Value = fator

                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Qtde").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Fator").Style.BackColor = Color.LightGreen


                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Style.BackColor = Color.LightGreen


                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Style.BackColor = Color.LightGreen

                                PecaNova = False


                                DGVListaMaterialSW.Rows(b).Selected = True
                                DGVListaMaterialSW.Rows(b).DefaultCellStyle.BackColor = Color.Yellow ' Define a cor de fundo para destacar a linha
                                DGVListaMaterialSW.FirstDisplayedScrollingRowIndex = b ' Rola o DataGridView até a linha selecionada

                                Exit Sub

                            End If

                            PecaNova = True


                        Next



                    Else

                        PecaNova = True

                        If PecaNova = True Then



                            Try
                                OrdemServico.CodMatFabricante = DadosArquivoCorrente.NomeArquivoSemExtensao
                            Catch ex As Exception
                                OrdemServico.CodMatFabricante = ""
                            End Try

                            Try
                                OrdemServico.DescResumo = DadosArquivoCorrente.Titulo
                            Catch ex As Exception
                                OrdemServico.DescResumo = ""
                            End Try

                            Try
                                OrdemServico.DescDetal = DadosArquivoCorrente.AssuntoSubiTitulo
                            Catch ex As Exception

                                OrdemServico.DescDetal = ""
                            End Try

                            Try
                                OrdemServico.Autor = DadosArquivoCorrente.Author
                            Catch ex As Exception

                                OrdemServico.Autor = ""
                            End Try

                            Try
                                OrdemServico.Palavrachave = DadosArquivoCorrente.PalavraChave
                            Catch ex As Exception

                                OrdemServico.Palavrachave = ""
                            End Try

                            Try
                                OrdemServico.Notas = DadosArquivoCorrente.Comentarios
                            Catch ex As Exception

                                OrdemServico.Notas = ""
                            End Try

                            Try
                                OrdemServico.Espessura = DadosArquivoCorrente.Espessura
                            Catch ex As Exception

                                OrdemServico.Espessura = ""
                            End Try

                            Try
                                OrdemServico.NumeroDobras = DadosArquivoCorrente.NumeroDobras
                            Catch ex As Exception

                                OrdemServico.NumeroDobras = ""
                            End Try


                            Try
                                OrdemServico.EnderecoArquivo = DadosArquivoCorrente.EnderecoArquivo.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.EnderecoArquivo = ""
                            End Try


                            Try
                                OrdemServico.MaterialSw = DadosArquivoCorrente.Material.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.MaterialSw = ""
                            End Try

                            If DadosArquivoCorrente.EnderecoArquivo.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                                OrdemServico.Unidade = "CONJ"
                                OrdemServico.UnidadeSW = "CONJ"
                            Else

                                OrdemServico.Unidade = "PC"
                                OrdemServico.UnidadeSW = "PC"

                            End If

                            OrdemServico.DtCad = ""
                            OrdemServico.UsuarioCriacao = ""
                            OrdemServico.UsuarioAlteracao = ""
                            OrdemServico.DtAlteracao = ""

                            Try
                                OrdemServico.Qtde = novaqtde
                            Catch ex As Exception

                                OrdemServico.Qtde = novaqtde

                            End Try

                            Try


                                DadosArquivoCorrente.AreaPintura = Replace(DadosArquivoCorrente.AreaPintura, ".", ",")
                                OrdemServico.AreaPintura = DadosArquivoCorrente.AreaPintura * novaqtde
                                OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ".", ",")

                            Catch ex As Exception

                                OrdemServico.AreaPintura = ""

                            End Try


                            Try

                                ' OrdemServico.Peso = DadosArquivoCorrente.Massa
                                DadosArquivoCorrente.Massa = Replace(DadosArquivoCorrente.Massa, ".", ",")
                                OrdemServico.Peso = DadosArquivoCorrente.Massa * novaqtde
                                OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")

                            Catch ex As Exception

                                OrdemServico.Peso = 0
                            End Try


                            Try
                                OrdemServico.PesoUnitario = DadosArquivoCorrente.Massa
                                OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ",")

                            Catch ex As Exception

                                OrdemServico.PesoUnitario = 0
                            End Try

                            Try
                                OrdemServico.txtSoldagem = DadosArquivoCorrente.soldagem
                            Catch ex As Exception

                                OrdemServico.txtSoldagem = ""

                            End Try

                            Try
                                OrdemServico.QtdeTotal = novaqtde

                            Catch ex As Exception

                                OrdemServico.QtdeTotal = 0

                            End Try

                            Try
                                OrdemServico.txtTipoDesenho = DadosArquivoCorrente.TipoDesenho
                            Catch ex As Exception

                                OrdemServico.txtTipoDesenho = ""

                            End Try

                            Try
                                OrdemServico.txtCorte = DadosArquivoCorrente.Corte
                            Catch ex As Exception

                                OrdemServico.txtCorte = ""

                            End Try

                            Try
                                OrdemServico.txtDobra = DadosArquivoCorrente.Dobra
                            Catch ex As Exception

                                OrdemServico.txtDobra = ""

                            End Try

                            Try
                                OrdemServico.txtSolda = DadosArquivoCorrente.Solda
                            Catch ex As Exception

                                OrdemServico.txtSolda = ""

                            End Try

                            Try
                                OrdemServico.txtPintura = DadosArquivoCorrente.Pintura
                            Catch ex As Exception

                                OrdemServico.txtPintura = ""

                            End Try

                            Try
                                OrdemServico.txtMontagem = DadosArquivoCorrente.Montagem
                            Catch ex As Exception

                                OrdemServico.txtMontagem = ""

                            End Try

                            Try
                                OrdemServico.Comprimentocaixadelimitadora = DadosArquivoCorrente.Alturacaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Comprimentocaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.Larguracaixadelimitadora = DadosArquivoCorrente.Larguracaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Larguracaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.Espessuracaixadelimitadora = DadosArquivoCorrente.Profundidadeaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Espessuracaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.txtItemEstoque = DadosArquivoCorrente.ItemEstoque
                            Catch ex As Exception

                                OrdemServico.txtItemEstoque = ""

                            End Try

                            Try
                                OrdemServico.txtAcabamento = DadosArquivoCorrente.acabamento
                            Catch ex As Exception

                                OrdemServico.txtAcabamento = ""

                            End Try


                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                            OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                            ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                            Dim query As String = "INSERT INTO ordemservicoitem (
                            IDOrdemServico, idProjeto, PROJETO, idTag, TAG, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CRIADOPOR, 
                            DATACRIACAO, ESTATUS, ACABAMENTO, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IDOrdemServico, @idProjeto, @PROJETO, @idTag, @TAG, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CRIADOPOR, 
                            @DATACRIACAO, @ESTATUS, @ACABAMENTO, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                            Using command As New MySqlCommand(query, myconect)
                                ' Adicionando os parâmetros
                                command.Parameters.AddWithValue("@IDOrdemServico", OrdemServico.IDOrdemServico)
                                command.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
                                command.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
                                command.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
                                command.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
                                command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.ESTATUS)
                                command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                                command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                                command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                command.Parameters.AddWithValue("@Peso", OrdemServico.Peso)
                                command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                                command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                command.Parameters.AddWithValue("@DtCad", "")
                                command.Parameters.AddWithValue("@UsuarioCriacao", "")
                                command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                command.Parameters.AddWithValue("@DtAlteracao", "")
                                command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSw)
                                command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                command.Parameters.AddWithValue("@QtdeProduzida", "")
                                command.Parameters.AddWithValue("@QtdeFaltante", "")
                                command.Parameters.AddWithValue("@CRIADOPOR", Usuario.NomeCompleto.ToString)
                                command.Parameters.AddWithValue("@DATACRIACAO", Date.Now)
                                command.Parameters.AddWithValue("@ESTATUS", "A")
                                command.Parameters.AddWithValue("@ACABAMENTO", OrdemServico.txtAcabamento)
                                command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                command.Parameters.AddWithValue("@fator", 1)
                                command.Parameters.AddWithValue("@qtde", OrdemServico.Qtde)
                                command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                                command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                                command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                                command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                                command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                                command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                                command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                                command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                                command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                                ' Abrir conexão e executar comando

                                'If cl_BancoDados.AbrirBanco = False Then
                                '    cl_BancoDados.AbrirBanco()

                                'End If
                                'myconect.Open()
                                command.ExecuteNonQuery()
                            End Using
                            TimerDGVListaMaterialSW.Enabled = True
                        End If

                        PecaNova = False

                    End If

                    ' PecaNova = True

                    If PecaNova = True Then



                        Try
                            OrdemServico.CodMatFabricante = DadosArquivoCorrente.NomeArquivoSemExtensao
                        Catch ex As Exception
                            OrdemServico.CodMatFabricante = ""
                        End Try

                        Try
                            OrdemServico.DescResumo = DadosArquivoCorrente.Titulo
                        Catch ex As Exception
                            OrdemServico.DescResumo = ""
                        End Try

                        Try
                            OrdemServico.DescDetal = DadosArquivoCorrente.AssuntoSubiTitulo
                        Catch ex As Exception

                            OrdemServico.DescDetal = ""
                        End Try

                        Try
                            OrdemServico.Autor = DadosArquivoCorrente.Author
                        Catch ex As Exception

                            OrdemServico.Autor = ""
                        End Try

                        Try
                            OrdemServico.Palavrachave = DadosArquivoCorrente.PalavraChave
                        Catch ex As Exception

                            OrdemServico.Palavrachave = ""
                        End Try

                        Try
                            OrdemServico.Notas = DadosArquivoCorrente.Comentarios
                        Catch ex As Exception

                            OrdemServico.Notas = ""
                        End Try

                        Try
                            OrdemServico.Espessura = DadosArquivoCorrente.Espessura
                        Catch ex As Exception

                            OrdemServico.Espessura = ""
                        End Try

                        Try
                            OrdemServico.NumeroDobras = DadosArquivoCorrente.NumeroDobras
                        Catch ex As Exception

                            OrdemServico.NumeroDobras = ""
                        End Try


                        Try
                            OrdemServico.EnderecoArquivo = DadosArquivoCorrente.EnderecoArquivo.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.EnderecoArquivo = ""
                        End Try


                        Try
                            OrdemServico.MaterialSw = DadosArquivoCorrente.Material.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.MaterialSw = ""
                        End Try

                        If DadosArquivoCorrente.EnderecoArquivo.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                            OrdemServico.Unidade = "CONJ"
                            OrdemServico.UnidadeSW = "CONJ"
                        Else

                            OrdemServico.Unidade = "PC"
                            OrdemServico.UnidadeSW = "PC"

                        End If

                        OrdemServico.DtCad = ""
                        OrdemServico.UsuarioCriacao = ""
                        OrdemServico.UsuarioAlteracao = ""
                        OrdemServico.DtAlteracao = ""

                        Try
                            OrdemServico.Qtde = novaqtde
                        Catch ex As Exception

                            OrdemServico.Qtde = novaqtde

                        End Try

                        Try


                            DadosArquivoCorrente.AreaPintura = Replace(DadosArquivoCorrente.AreaPintura, ".", ",")
                            OrdemServico.AreaPintura = DadosArquivoCorrente.AreaPintura * novaqtde
                            OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ".", ",")

                        Catch ex As Exception

                            OrdemServico.AreaPintura = ""

                        End Try


                        Try

                            ' OrdemServico.Peso = DadosArquivoCorrente.Massa
                            DadosArquivoCorrente.Massa = Replace(DadosArquivoCorrente.Massa, ".", ",")
                            OrdemServico.Peso = DadosArquivoCorrente.Massa * novaqtde
                            OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")

                        Catch ex As Exception

                            OrdemServico.Peso = 0
                        End Try


                        Try
                            OrdemServico.PesoUnitario = DadosArquivoCorrente.Massa
                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ",")

                        Catch ex As Exception

                            OrdemServico.PesoUnitario = 0
                        End Try

                        Try
                            OrdemServico.txtSoldagem = DadosArquivoCorrente.soldagem
                        Catch ex As Exception

                            OrdemServico.txtSoldagem = ""

                        End Try

                        Try
                            OrdemServico.QtdeTotal = novaqtde

                        Catch ex As Exception

                            OrdemServico.QtdeTotal = 0

                        End Try

                        Try
                            OrdemServico.txtTipoDesenho = DadosArquivoCorrente.TipoDesenho
                        Catch ex As Exception

                            OrdemServico.txtTipoDesenho = ""

                        End Try

                        Try
                            OrdemServico.txtCorte = DadosArquivoCorrente.Corte
                        Catch ex As Exception

                            OrdemServico.txtCorte = ""

                        End Try

                        Try
                            OrdemServico.txtDobra = DadosArquivoCorrente.Dobra
                        Catch ex As Exception

                            OrdemServico.txtDobra = ""

                        End Try

                        Try
                            OrdemServico.txtSolda = DadosArquivoCorrente.Solda
                        Catch ex As Exception

                            OrdemServico.txtSolda = ""

                        End Try

                        Try
                            OrdemServico.txtPintura = DadosArquivoCorrente.Pintura
                        Catch ex As Exception

                            OrdemServico.txtPintura = ""

                        End Try

                        Try
                            OrdemServico.txtMontagem = DadosArquivoCorrente.Montagem
                        Catch ex As Exception

                            OrdemServico.txtMontagem = ""

                        End Try

                        Try
                            OrdemServico.Comprimentocaixadelimitadora = DadosArquivoCorrente.Alturacaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Comprimentocaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.Larguracaixadelimitadora = DadosArquivoCorrente.Larguracaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Larguracaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.Espessuracaixadelimitadora = DadosArquivoCorrente.Profundidadeaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Espessuracaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.txtItemEstoque = DadosArquivoCorrente.ItemEstoque
                        Catch ex As Exception

                            OrdemServico.txtItemEstoque = ""

                        End Try

                        Try
                            OrdemServico.txtAcabamento = DadosArquivoCorrente.acabamento
                        Catch ex As Exception

                            OrdemServico.txtAcabamento = ""

                        End Try


                        OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                        OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                        ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                        Dim query As String = "INSERT INTO ordemservicoitem (
                            IDOrdemServico, idProjeto, PROJETO, idTag, TAG, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CRIADOPOR, 
                            DATACRIACAO, ESTATUS, ACABAMENTO, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IDOrdemServico, @idProjeto, @PROJETO, @idTag, @TAG, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CRIADOPOR, 
                            @DATACRIACAO, @ESTATUS, @ACABAMENTO, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                        Using command As New MySqlCommand(query, myconect)
                            ' Adicionando os parâmetros
                            command.Parameters.AddWithValue("@IDOrdemServico", OrdemServico.IDOrdemServico)
                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
                            command.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
                            command.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
                            command.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.ESTATUS)
                            command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                            command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                            command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                            command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                            command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                            command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                            command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                            command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                            command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                            command.Parameters.AddWithValue("@Peso", OrdemServico.Peso)
                            command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                            command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                            command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                            command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                            command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                            command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                            command.Parameters.AddWithValue("@DtCad", "")
                            command.Parameters.AddWithValue("@UsuarioCriacao", "")
                            command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                            command.Parameters.AddWithValue("@DtAlteracao", "")
                            command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSw)
                            command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                            command.Parameters.AddWithValue("@QtdeProduzida", "")
                            command.Parameters.AddWithValue("@QtdeFaltante", "")
                            command.Parameters.AddWithValue("@CRIADOPOR", Usuario.NomeCompleto.ToString)
                            command.Parameters.AddWithValue("@DATACRIACAO", Date.Now)
                            command.Parameters.AddWithValue("@ESTATUS", "A")
                            command.Parameters.AddWithValue("@ACABAMENTO", OrdemServico.txtAcabamento)
                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                            command.Parameters.AddWithValue("@fator", 1)
                            command.Parameters.AddWithValue("@qtde", OrdemServico.Qtde)
                            command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                            command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                            command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                            command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                            command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                            command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                            command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                            command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                            command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                            command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                            command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                            command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                            command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                            command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                            command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                            command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                            command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                            command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                            ' Abrir conexão e executar comando

                            'If cl_BancoDados.AbrirBanco = False Then
                            '    cl_BancoDados.AbrirBanco()

                            'End If
                            'myconect.Open()
                            command.ExecuteNonQuery()
                        End Using
                        TimerDGVListaMaterialSW.Enabled = True
                    End If



                End If

            End If

        End If
    End Sub

    Private Sub ConfiguraçãoToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ConfiguraçãoToolStripMenuItem2.Click
        If InputBox("Senha de acesso", "Administrador", "") = "99678982" Then
            Dim OpenfileConfiguracao As New OpenFileDialog

            ' Configura o diálogo para aceitar somente arquivos de texto
            OpenfileConfiguracao.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*"
            OpenfileConfiguracao.FilterIndex = 1

            ' Exibe o diálogo e verifica se o usuário selecionou um arquivo
            If OpenfileConfiguracao.ShowDialog() = DialogResult.OK Then
                Dim caminhoArquivo As String = OpenfileConfiguracao.FileName

                ' Verifica se o arquivo existe
                If File.Exists(caminhoArquivo) Then
                    Try
                        ' Variáveis para armazenar os parâmetros
                        Dim endereco, usuario, banco, senha As String
                        Dim EnderecoPastaRaizOS, EnderecoTemplateExcel, CopiaBancoDados As String
                        Dim EnderecoPastaRaizRomaneio, EnderecoTemplateExcelRomaneio, ParametroExportarDXF As String

                        ' Codificação utilizada na leitura do arquivo
                        Dim codificacao As Encoding = Encoding.GetEncoding("ISO-8859-1") ' Ajustável conforme o arquivo

                        ' Lê o arquivo linha por linha
                        Dim parametrosEncontrados As New Dictionary(Of String, String)
                        Using leitor As New StreamReader(caminhoArquivo, codificacao)
                            While Not leitor.EndOfStream
                                Dim linha As String = leitor.ReadLine()?.Trim()

                                ' Ignorar linhas em branco ou mal formadas
                                If String.IsNullOrEmpty(linha) OrElse Not linha.Contains(";") Then Continue While

                                Dim partes = linha.Split(";"c)
                                If partes.Length = 2 Then
                                    Dim chave = partes(0).Trim()
                                    Dim valor = partes(1).Trim()

                                    ' Armazena no dicionário
                                    If Not parametrosEncontrados.ContainsKey(chave) Then
                                        parametrosEncontrados(chave) = valor
                                    End If
                                End If
                            End While
                        End Using

                        ' Atribui valores ao My.Settings
                        Dim parametrosNecessarios = {"endereco", "Usuario", "Banco", "Senha", "EnderecoPastaRaizOS", "EnderecoTemplateExcelOrdemServico", "ParametroExportarDXF"}
                        For Each param In parametrosNecessarios
                            If Not parametrosEncontrados.ContainsKey(param) Then
                                MsgBox($"Erro: Parâmetro '{param}' não encontrado no arquivo de configuração!", MsgBoxStyle.Critical)
                                Return
                            End If
                        Next

                        My.Settings.MySqlBancoDados = parametrosEncontrados("Banco")
                        My.Settings.MysqlEndereco = parametrosEncontrados("endereco")
                        My.Settings.MysqlUsuario = parametrosEncontrados("Usuario")
                        My.Settings.MysqlSenha = parametrosEncontrados("Senha")
                        My.Settings.BancoDadosAtivo = parametrosEncontrados("Banco")

                        My.Settings.EnderecoPastaRaizOS = parametrosEncontrados("EnderecoPastaRaizOS")
                        My.Settings.EnderecoTemplateExcel = parametrosEncontrados("EnderecoTemplateExcelOrdemServico")
                        My.Settings.ParametroExportarDXF = parametrosEncontrados("ParametroExportarDXF")

                        ' Salva as configurações
                        My.Settings.Save()

                        MsgBox("Configurações carregadas com sucesso!", MsgBoxStyle.Information)

                    Catch ex As Exception
                        MsgBox($"Erro ao processar o arquivo de configuração: {ex.Message}", MsgBoxStyle.Critical)
                    End Try
                Else
                    MsgBox("Arquivo selecionado não encontrado!", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox("Senha inválida!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub ConfExportarArquivoParaOSToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfExportarArquivoParaOSToolStripMenuItem1.Click

        ExportarParaOS.ShowDialog()
    End Sub

    Private Sub AtualizarDesenhoPeloDiretorioToolStripMenuItem1_Click(sender As Object, e As EventArgs)

        Dim result As DialogResult

        result = MessageBox.Show("Escolha uma opção:  SIM      1 - Atualiza a versão do SolidWork e Salva no banco de dados" & vbCrLf _
                                                   & "NÃO      2 - Somente Atualiza a versão do SolidWorks sem Salvar no Banco de dados" & vbCrLf _
                                                   & "Cancelar 3 -Finaliza a opração sem faze nenhuma alteralção", "Atualizar Versão",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1)

        If result = DialogResult.Yes Then

            ' Opção 1 - Atualizar versão do SW e Salvar no banco
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(True)

        ElseIf result = DialogResult.No Then
            ' Opção 2 - Somente atualizar versão do SW
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(False)

        ElseIf result = DialogResult.Cancel Then
            ' Opção 3 - Cancelar
            MessageBox.Show("A operação foi cancelada.", "Cancelar")

        End If
    End Sub

    Private Sub BuscarArquivosNosDiretorioToolStripMenuItem1_Click(sender As Object, e As EventArgs)

        Arquivos.Show()

    End Sub

    Private Sub UsarFormatoA3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsarFormatoA3ToolStripMenuItem.Click

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                ' Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                '  Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                '  Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPapersUserDefined, swDwgTemplates_e.swDwgTemplateCustom, 0, 0, True, My.Settings.EnderecoNovoFormatoA3.ToString, 0.385, 0.277, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If
    End Sub

    Private Sub UsarFormatoA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsarFormatoA4ToolStripMenuItem.Click

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                '  Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                ' Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                ' Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPaperA4size, swDwgTemplates_e.swDwgTemplateCustom, 0.21, 0.297, True, My.Settings.EnderecoNovoFormatoA4.ToString, 0.21, 0.297, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception

            Finally

            End Try

        End If
    End Sub

    Private Sub UsarFornatoA4DToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsarFornatoA4DToolStripMenuItem.Click


        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                Dim fileerror As Integer

                Dim filewarning As Integer

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                Dim currentSheetScale As Double
                Dim scaleStatus As Boolean

                ' Obtém a escala da folha atual
                scaleStatus = swDrawing.GetCurrentSheetScale(currentSheetScale)

                If scaleStatus Then
                    ' Configura a nova folha mantendo a escala atual
                    status = swDrawing.SetupSheet6(
                                       "Sheet3",
                                       swDwgPaperSizes_e.swDwgPaperA4size,
                                       swDwgTemplates_e.swDwgTemplateCustom,
                                       0.297, 0.21,
                                       True,
                                       My.Settings.EnderecoNovoFormatoA4.ToString,
                                       0.297, 0.21,
                                       "Default",
                                       True,
                                       currentSheetScale,
                                       currentSheetScale,
                                       0, 0, 0, 0)
                Else
                    MsgBox("Falha ao obter a escala atual da folha.")
                End If


                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        OrdemServico.IDOrdemServico = Nothing
        OrdemServico.PROJETO = Nothing
        OrdemServico.TAG = Nothing
        OrdemServico.DESCRICAO = Nothing
        OrdemServico.ESTATUS = Nothing
        OrdemServico.IDTAG = Nothing
        OrdemServico.IDPROJETO = Nothing
        OrdemServico.DESCEMPRESA = Nothing
        TSBSalvarOrdemServico.Enabled = True
        OrdemServico.DataPrevisao = Nothing

        Me.lblOrdemServicoAtiva.Text = ""

        Me.cboProjeto.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboTag.DropDownStyle = ComboBoxStyle.DropDownList

        Me.cboProjeto.Text = ""
        Me.cboTag.Text = ""
        Me.txtCliente.Clear()
        Me.txtDescricaoTag.Clear()
        Me.txtDescricao.Clear()
        Me.cboProjeto.Enabled = True
        Me.cboTag.Enabled = True

        Me.cboProjeto.Focus()

        TimerDGVListaMaterialSW.Enabled = True
    End Sub

    Private Sub TSBSalvarOrdemServico_Click(sender As Object, e As EventArgs) Handles TSBSalvarOrdemServico.Click

        Try
            OrdemServico.DESCRICAO = Me.txtDescricao.Text
            OrdemServico.IDTAG = cboTag.SelectedValue
            OrdemServico.IDPROJETO = cboProjeto.SelectedValue
            'OrdemServico.PrevDataEntrega

            OrdemServico.CriarOsCompleta(dgvos, Timerdgvos, TimerDGVListaMaterialSW)

            ' Seleciona a última linha do DataGridView
            If dgvos.Rows.Count > 0 Then
                ' Obtém o índice da última linha
                Dim ultimaLinha As Integer = dgvos.Rows.Count - 1

                ' Define a última linha como selecionada
                dgvos.ClearSelection() ' Limpa seleções anteriores
                dgvos.Rows(ultimaLinha).Selected = True

                ' Faz a rolagem para garantir que a linha esteja visível
                dgvos.FirstDisplayedScrollingRowIndex = ultimaLinha

                dgvos.Rows(ultimaLinha).DefaultCellStyle.BackColor = Color.LightCyan

                TimerDGVListaMaterialSW.Enabled = True

                TSBSalvarOrdemServico.Enabled = False

            End If

        Catch ex As Exception
            MsgBox("Erro ao criar OS")
        Finally
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click

        Try

            cl_BancoDados.ComboBoxDataSet("projetos", "IdProjeto", "Projeto", cboProjeto, " WHERE Liberado = 'S'")
            cl_BancoDados.ComboBoxDataSet("acabamento", "idAcabamento", "DescAcabamento", cboOpcoesAcabamento, "")
            '  cl_BancoDados.ComboBoxDataSet("acabamento", "idAcabamento", "DescAcabamento", cboAcabamentoArvore, "")

            '  cl_BancoDados.ComboBoxDataSet("familia", "idfamilia", "Descfamilia", cboTipoDesenho, "")
            '  cl_BancoDados.ComboBoxDataSet("familia", "idfamilia", "Descfamilia", cboTipoDesenhoArvore, "")

            cl_BancoDados.ComboBoxDataSet("tipoproduto", "idtipoproduto", "tipoproduto", cboTitulo, "")
            '  cl_BancoDados.ComboBoxDataSet("tipoproduto", "idtipoproduto", "tipoproduto", cboTituloArvore, "")


        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click

        dgvDataGridBOM.DataSource = Nothing
        dgvDataGridBOM.Rows.Clear()
        dgvDataGridBOM.Refresh()
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click

        Try

            'If cl_BancoDados.AbrirBanco = False Then

            '    cl_BancoDados.AbrirBanco()

            'End If


            TabelaViewMontaPeca = cl_BancoDados.CarregarDados("SELECT * FROM viewmontapeca where D_E_L_E_T_E <> '' OR D_E_L_E_T_E IS NULL")

            ' Conectar ao SolidWorks
            IntanciaSolidWorks.ConectarSolidWorks()

            ' Obter o documento ativo
            swModel = swapp.ActiveDoc


            ' Verificar se o documento foi aberto
            If swModel Is Nothing Then
                MsgBox("Nenhum documento ativo no SolidWorks.", vbCritical, "Erro")
                Exit Sub
            End If

            ' Definir modelo como visível
            'swModel.Visible = True
            swModelDocExt = swModel.Extension

            ' Verifica o tipo de documento (Desenho)
            If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                ' Obter o caminho completo do arquivo
                DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(swModel.GetPathName().ToUpper())

                ' Se a opção de converter para PDF estiver marcada
                If chkConverterPDF.Checked Then
                    ' Verificar se o arquivo existe antes de tentar converter
                    If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                        DadosArquivoCorrente.ExportToPDF(swModel, DadosArquivoCorrente.EnderecoArquivo, True)
                        iconePDF = My.Resources.ficheiro_pdf
                    Else
                        MsgBox("Arquivo não encontrado: " & DadosArquivoCorrente.EnderecoArquivo, vbCritical, "Erro")
                        Exit Sub
                    End If
                End If

                ' Alterar a extensão para SLDASM
                DadosArquivoCorrente.EnderecoArquivo = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, "SLDASM")

                ' Verificar se o arquivo SLDASM existe
                If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                    OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)
                Else
                    MsgBox("O desenho de detalhamento não pertence a um desenho de conjunto!", vbCritical, "Atenção")
                    Exit Sub
                End If

            End If

            ' Processar PART ou ASSEMBLY
            If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then



                ''retirado 03/12/2024 edson  DadosArquivoCorrente.AtualizaDesenho(swModel)


                FormatarColunaIconeDGVListaBom()

                ' Preencher o DataGridView com os dados da peça
                dgvDataGridBOM.Rows.Add(iconeDXF,
                                        iconePDF,
                                        iconeTipoArquivo,
                                    iconeAtencao,
                                    DadosArquivoCorrente.IdMaterial,
                                    DadosArquivoCorrente.NomeArquivoSemExtensao,
                                    DadosArquivoCorrente.Titulo,
                                    DadosArquivoCorrente.AssuntoSubiTitulo,
                                    DadosArquivoCorrente.Author,
                                    DadosArquivoCorrente.PalavraChave,
                                    DadosArquivoCorrente.Comentarios,
                                    DadosArquivoCorrente.Espessura,
                                    DadosArquivoCorrente.ComprimentoBlank,
                                    DadosArquivoCorrente.LarguraBlank,
                                    DadosArquivoCorrente.Material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.acabamento,
                                    DadosArquivoCorrente.soldagem,
                                    DadosArquivoCorrente.TipoDesenho,
                                    DadosArquivoCorrente.Corte,
                                    DadosArquivoCorrente.Dobra,
                                    DadosArquivoCorrente.Solda,
                                    DadosArquivoCorrente.Pintura,
                                    DadosArquivoCorrente.Montagem,
                                    DadosArquivoCorrente.rnc,
                                    DadosArquivoCorrente.Alturacaixadelimitadora,
                                    DadosArquivoCorrente.Larguracaixadelimitadora,
                                    DadosArquivoCorrente.Profundidadeaixadelimitadora,
                                    DadosArquivoCorrente.ItemEstoque,
                                    1)

                Try


                    If DadosArquivoCorrente.rnc.ToString <> "" Then

                        dgvDataGridBOM.Rows(dgvDataGridBOM.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightPink

                    End If

                Catch ex As Exception
                Finally
                End Try

                ' Ler dados da view de montagem
                LerDadosViewMontaPeca()

                Try


                    ' Fechar o documento
                    swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                    cl_BancoDados.FecharArquivoMemoria()
                    IntanciaSolidWorks.LiberarRecurso(swModel)

                Catch ex As Exception

                Finally

                End Try

                ' Processar a lista de material
                ProcessarListaMaterial(swModel)

            End If
        Catch ex As Exception
            MsgBox("Erro: " & ex.Message, vbCritical, "Erro")
            ' Fechar o documento
            swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
            cl_BancoDados.FecharArquivoMemoria()
            IntanciaSolidWorks.LiberarRecurso(swModel)

        Finally
        End Try
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click


        If dgvDataGridBOM.Rows.Count > 0 Then

            Dim arquivoSLDDRW As String

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, abrindo os desenhos e atualizando os dados cadastrais. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarListaSW.Minimum = 0
                ProgressBarListaSW.Maximum = (dgvDataGridBOM.Rows.Count - 1) * 2

                dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                    Try

                        DadosArquivoCorrente.EnderecoArquivo = dgvDataGridBOM.Rows(i).Cells("EnderecoArquivo").Value.ToString

                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                        If chkConverterPDF.Checked Then

                            ' Verifica se EnderecoArquivo não é nulo ou vazio antes de realizar a substituição
                            If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then

                                Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()

                                ' Converte o caminho do arquivo para .SLDDRW se ele terminar com .SLDPRT ou .SLDASM
                                arquivoSLDDRW = enderecoArquivoUpper
                                If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                                    arquivoSLDDRW = arquivoSLDDRW.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                                End If

                                If File.Exists(arquivoSLDDRW) Then
                                    OpenDocumentAndWait(arquivoSLDDRW, True, swModel)
                                    DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False)
                                    ' swapp.CloseDoc(arquivoSLDDRW)
                                    dgvDataGridBOM.Rows(i).Cells("dgvIconePDF").Value = My.Resources.ficheiro_pdf

                                Else

                                    '  DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False)
                                    dgvDataGridBOM.Rows(i).Cells("dgvIconePDF").Value = My.Resources.Sem_Incone

                                End If

                            End If

                        End If


                        If chkConverterDXF.Checked Then

                            If DadosArquivoCorrente.EnderecoArquivo.ToUpper.EndsWith(".SLDPRT") Then

                                OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                                Dim enderecoArquivo As String = DadosArquivoCorrente.EnderecoArquivo

                                ' Verifica se EnderecoArquivo não é nulo e o arquivo realmente existe antes de tentar exportar
                                If Not String.IsNullOrEmpty(enderecoArquivo) AndAlso File.Exists(enderecoArquivo) Then
                                    DadosArquivoCorrente.ExportDXF(swModel, True, True)
                                    dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.arquivo_dxf

                                Else
                                    dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.Sem_Incone


                                End If
                            Else

                                dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.Sem_Incone

                            End If

                        End If


                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        ' Opcional: Força a coleta de lixo para liberar os objetos COM imediatamente
                        GC.Collect()

                        GC.WaitForPendingFinalizers()

                        dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightGreen

                        ProgressBarListaSW.Value = i

                    Catch ex As Exception

                        dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightPink

                        '  MsgBox(ex.Message)

                        Continue For

                    End Try

                Next

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")

                ProgressBarListaSW.Value = 0

                chkConverterPDF.Checked = False
                chkConverterDXF.Checked = False


            Else

                MsgBox("Processo cancelado!", vbInformation, "Informação")

            End If

            'dgvDataGridBOM.ResumeLayout()
        End If

        If dgvDataGridBOM.Rows.Count > 0 Then

            For a As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                Try

                    If dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString().ToLower().EndsWith(".sldprt") OrElse
                  dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString().ToLower().EndsWith(".sldasm") Then

                        swapp.CloseDoc(dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString())

                    End If
                Catch ex As Exception
                    Continue For
                End Try

            Next
        End If

    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click


        Dim verificaRnc As Boolean = False

        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir os itens da lista BOM na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Itens da OS", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Dim rnc As String

            Dim Fator As Double
            '  Try

            ProgressBarListaSW.Minimum = 0
            ProgressBarListaSW.Maximum = dgvDataGridBOM.Rows.Count

            If dgvDataGridBOM.Rows.Count > 0 Then

                For b As Integer = 0 To dgvDataGridBOM.Rows.Count - 1
                    Try



                        Try

                            rnc = dgvDataGridBOM.Rows(b).Cells("RNC").Value.ToString

                        Catch ex As Exception
                            rnc = ""
                        End Try

                        If rnc = "S" Then

                            MsgBox("Na lista, há peças com RNC pendente; para prosseguir com o processo de liberação, é necessário remover a peça da lista ou resolver a RNC.", vbCritical, "Atenção")

                            rnc = dgvDataGridBOM.Rows(b).DefaultCellStyle.BackColor = Color.LightSalmon

                            verificaRnc = True

                            Exit Sub

                        Else

                            verificaRnc = False


                        End If

                    Catch ex As Exception
                        Continue For
                    End Try

                Next

            End If

            If verificaRnc = False Then

                If dgvDataGridBOM.Rows.Count > 0 Then

                    If OrdemServico.IDOrdemServico = Nothing Or OrdemServico.IDOrdemServico = 0 Then

                        MsgBox("A Ordem de Serviço deve ser selecionada", vbCritical, "Atenção")

                        Exit Sub
                    Else

                        Try
                            Fator = InputBox("Informe o Valor Multiplicado de fabricação", "Fator de Multiplicação", 1)

                            ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                            If Fator = "" Then

                                MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                                Exit Sub ' Sai do procedimento
                            End If

                            ' Verifica se o valor inserido é numérico e maior que 0
                            If Not IsNumeric(Fator) OrElse Convert.ToDouble(Fator) <= 0 Then

                                MsgBox("A operação foi cancelada. O valor informado não é um número válido!", vbInformation, "Atenação")

                                Exit Sub
                            End If
                        Catch ex As Exception
                            ' Fator = "1"
                        Finally
                            ' Código adicional, se necessário
                        End Try


                        For A As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                            Try

                                If dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> "" Or dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> Nothing Then


                                    Try
                                        OrdemServico.EnderecoArquivo = dgvDataGridBOM.Rows(A).Cells("EnderecoArquivo").Value.ToString.ToUpper
                                        'OrdemServico.EnderecoArquivo = Replace(OrdemServico.EnderecoArquivo, "\", "##")
                                        ' OrdemServico.EnderecoArquivo = Replace(OrdemServico.EnderecoArquivo, "/", "#")
                                    Catch ex As Exception

                                        OrdemServico.EnderecoArquivo = ""

                                    End Try


                                    Try
                                        OrdemServico.CodMatFabricante = dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString.ToUpper
                                    Catch ex As Exception
                                        OrdemServico.CodMatFabricante = ""
                                    End Try

                                    Try
                                        OrdemServico.DescResumo = dgvDataGridBOM.Rows(A).Cells("DescResumo").Value.ToString.ToUpper
                                    Catch ex As Exception
                                        OrdemServico.DescResumo = ""
                                    End Try

                                    Try
                                        OrdemServico.DescDetal = dgvDataGridBOM.Rows(A).Cells("DescDetal").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.DescDetal = ""
                                    End Try

                                    Try
                                        OrdemServico.Autor = dgvDataGridBOM.Rows(A).Cells("Autor").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Autor = ""
                                    End Try

                                    Try
                                        OrdemServico.Palavrachave = dgvDataGridBOM.Rows(A).Cells("Palavrachave").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Palavrachave = ""
                                    End Try

                                    Try
                                        OrdemServico.Notas = dgvDataGridBOM.Rows(A).Cells("Notas").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Notas = ""
                                    End Try

                                    Try
                                        OrdemServico.Espessura = dgvDataGridBOM.Rows(A).Cells("Espessura").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.Espessura = ""
                                    End Try

                                    Try
                                        OrdemServico.NumeroDobras = dgvDataGridBOM.Rows(A).Cells("NumeroDobras").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.NumeroDobras = ""
                                    End Try

                                    If dgvDesenhos.Rows(A).Cells("EnderecoArquivo").Value.ToString().IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                                        OrdemServico.Unidade = "CONJ"
                                        OrdemServico.UnidadeSW = "CONJ"
                                    Else

                                        OrdemServico.Unidade = "PC"
                                        OrdemServico.UnidadeSW = "PC"

                                    End If

                                    OrdemServico.ValorSW = ""

                                    Try
                                        OrdemServico.Altura = Replace(dgvDataGridBOM.Rows(A).Cells("Altura").Value.ToString, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Altura = ""
                                    End Try

                                    Try
                                        OrdemServico.Largura = Replace(dgvDataGridBOM.Rows(A).Cells("Largura").Value.ToString, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Largura = ""

                                    End Try

                                    OrdemServico.DtCad = ""
                                    OrdemServico.UsuarioCriacao = ""
                                    OrdemServico.UsuarioAlteracao = ""
                                    OrdemServico.DtAlteracao = ""

                                    Try
                                        OrdemServico.MaterialSw = dgvDataGridBOM.Rows(A).Cells("Material").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.MaterialSw = ""

                                    End Try

                                    Try
                                        OrdemServico.Qtde = dgvDataGridBOM.Rows(A).Cells("Qtde").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.Qtde = 0

                                    End Try

                                    Try
                                        OrdemServico.AreaPintura = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                        OrdemServico.AreaPintura = OrdemServico.AreaPintura * OrdemServico.Qtde * Fator
                                        OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ",", ".")
                                    Catch ex As Exception

                                        OrdemServico.AreaPintura = ""

                                    End Try

                                    Try
                                        OrdemServico.AreaPinturaUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                        OrdemServico.AreaPinturaUnitario = Replace(OrdemServico.AreaPinturaUnitario, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.AreaPinturaUnitario = ""

                                    End Try

                                    Try
                                        OrdemServico.Peso = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")

                                        OrdemServico.Peso = OrdemServico.Peso * OrdemServico.Qtde * Fator
                                        OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.Peso = 0
                                    End Try

                                    Try
                                        OrdemServico.PesoUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")
                                        OrdemServico.PesoUnitario = Replace(OrdemServico.PesoUnitario, ",", ".")
                                    Catch ex As Exception

                                        OrdemServico.PesoUnitario = 0
                                    End Try

                                    Try
                                        OrdemServico.txtSoldagem = dgvDataGridBOM.Rows(A).Cells("txtSoldagem").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtSoldagem = ""

                                    End Try

                                    Try
                                        OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ".", ",")
                                        OrdemServico.QtdeTotal = OrdemServico.Qtde * Fator
                                        OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.QtdeTotal = 0

                                    End Try

                                    Try
                                        OrdemServico.txtTipoDesenho = dgvDataGridBOM.Rows(A).Cells("txtTipoDesenho").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtTipoDesenho = ""

                                    End Try



                                    Try
                                        OrdemServico.txtCorte = dgvDataGridBOM.Rows(A).Cells("txtCorte").Value.ToString

                                    Catch ex As Exception

                                        OrdemServico.txtCorte = ""

                                    End Try

                                    Try
                                        OrdemServico.txtDobra = dgvDataGridBOM.Rows(A).Cells("txtDobra").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtDobra = ""

                                    End Try

                                    Try
                                        OrdemServico.txtSolda = dgvDataGridBOM.Rows(A).Cells("txtSolda").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtSolda = ""

                                    End Try

                                    Try
                                        OrdemServico.txtPintura = dgvDataGridBOM.Rows(A).Cells("txtPintura").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtPintura = ""

                                    End Try

                                    Try
                                        OrdemServico.txtMontagem = dgvDataGridBOM.Rows(A).Cells("txtMontagem").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtMontagem = ""

                                    End Try

                                    Try
                                        OrdemServico.Comprimentocaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Comprimentocaixadelimitadora").Value.ToString
                                        OrdemServico.Comprimentocaixadelimitadora = Replace(OrdemServico.Comprimentocaixadelimitadora, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Comprimentocaixadelimitadora = ""

                                    End Try

                                    Try
                                        OrdemServico.Larguracaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Larguracaixadelimitadora").Value.ToString
                                        OrdemServico.Larguracaixadelimitadora = Replace(OrdemServico.Larguracaixadelimitadora, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Larguracaixadelimitadora = ""

                                    End Try

                                    Try
                                        OrdemServico.Espessuracaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Espessuracaixadelimitadora ").Value.ToString
                                        OrdemServico.Espessuracaixadelimitadora = Replace(OrdemServico.Espessuracaixadelimitadora, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Espessuracaixadelimitadora = ""

                                    End Try

                                    Try
                                        OrdemServico.txtItemEstoque = dgvDataGridBOM.Rows(A).Cells("txtItemEstoque").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.txtItemEstoque = ""

                                    End Try


                                    Try
                                        OrdemServico.txtAcabamento = dgvDataGridBOM.Rows(A).Cells("Acabamento").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.txtAcabamento = ""

                                    End Try




                                    OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ",", "")

                                    If cl_BancoDados.AbrirBanco() = False Then

                                        cl_BancoDados.AbrirBanco()

                                    End If


                                    ProgressBarListaSW.Value = A

                                    Dim query As String = "INSERT INTO ordemservicoitem (
                            IDOrdemServico, idProjeto, PROJETO, idTag, TAG, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CRIADOPOR, 
                            DATACRIACAO, ESTATUS, ACABAMENTO, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque,DataPrevisao,
                            sttxtcorte,cortetotalexecutado, cortetotalexecutar,
                            sttxtDobra,Dobratotalexecutado, Dobratotalexecutar,
                            sttxtSolda,Soldatotalexecutado, Soldatotalexecutar,
                            sttxtPintura,Pinturatotalexecutado, Pinturatotalexecutar,
                            sttxtMontagem,Montagemtotalexecutado, Montagemtotalexecutar,
                            ORDEMSERVICOITEMFINALIZADO,IdPlanodecorte
                             ) VALUES (
                            @IDOrdemServico, @idProjeto, @PROJETO, @idTag, @TAG, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CRIADOPOR, 
                            @DATACRIACAO, @ESTATUS, @ACABAMENTO, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque,@DataPrevisao,
                            @sttxtcorte,@cortetotalexecutado, @cortetotalexecutar,
                            @sttxtDobra,@Dobratotalexecutado, @Dobratotalexecutar,
                            @sttxtSolda,@Soldatotalexecutado, @Soldatotalexecutar,
                            @sttxtPintura,@Pinturatotalexecutado, @Pinturatotalexecutar,
                            @sttxtMontagem,@Montagemtotalexecutado, @Montagemtotalexecutar,
                            @ORDEMSERVICOITEMFINALIZADO,@IdPlanodecorte);"

                                    Using command As New MySqlCommand(query, myconect)
                                        ' Adicionando os parâmetros
                                        command.Parameters.AddWithValue("@IDOrdemServico", OrdemServico.IDOrdemServico)
                                        command.Parameters.AddWithValue("@idProjeto", OrdemServico.IDPROJETO)
                                        command.Parameters.AddWithValue("@PROJETO", OrdemServico.PROJETO)
                                        command.Parameters.AddWithValue("@idTag", OrdemServico.IDTAG)
                                        command.Parameters.AddWithValue("@TAG", OrdemServico.TAG)
                                        command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.ESTATUS)
                                        command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                        command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                        command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                        command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                        command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                                        command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                        command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                        command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                                        command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                        command.Parameters.AddWithValue("@Peso", OrdemServico.Peso)
                                        command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                        command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                        command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                                        command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                        command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                        command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                        command.Parameters.AddWithValue("@DtCad", Date.Now.Date.ToShortDateString)
                                        command.Parameters.AddWithValue("@UsuarioCriacao", Usuario.NomeCompleto)
                                        command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                        command.Parameters.AddWithValue("@DtAlteracao", "")
                                        command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                        command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSw)
                                        command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                        command.Parameters.AddWithValue("@QtdeProduzida", "")
                                        command.Parameters.AddWithValue("@QtdeFaltante", "")
                                        command.Parameters.AddWithValue("@CRIADOPOR", Usuario.NomeCompleto.ToString)
                                        command.Parameters.AddWithValue("@DATACRIACAO", Date.Now)
                                        command.Parameters.AddWithValue("@ESTATUS", "A")
                                        command.Parameters.AddWithValue("@ACABAMENTO", OrdemServico.txtAcabamento)
                                        command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                        command.Parameters.AddWithValue("@fator", Fator)
                                        command.Parameters.AddWithValue("@qtde", OrdemServico.Qtde)
                                        command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                        command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                        command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                        command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                        command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                        command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                        command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                        command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                                        command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                                        command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                                        command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                                        command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                                        command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                        command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                                        command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                        command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                                        command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                                        command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)
                                        command.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)
                                        command.Parameters.AddWithValue("@sttxtcorte", "")
                                        command.Parameters.AddWithValue("@cortetotalexecutado", "")
                                        command.Parameters.AddWithValue("@cortetotalexecutar", "")

                                        command.Parameters.AddWithValue("@sttxtDobra", "")
                                        command.Parameters.AddWithValue("@Dobratotalexecutado", "")
                                        command.Parameters.AddWithValue("@Dobratotalexecutar", "")

                                        command.Parameters.AddWithValue("@sttxtSolda", "")
                                        command.Parameters.AddWithValue("@Soldatotalexecutado", "")
                                        command.Parameters.AddWithValue("@Soldatotalexecutar", "")

                                        command.Parameters.AddWithValue("@sttxtPintura", "")
                                        command.Parameters.AddWithValue("@Pinturatotalexecutado", "")
                                        command.Parameters.AddWithValue("@Pinturatotalexecutar", "")

                                        command.Parameters.AddWithValue("@sttxtMontagem", "")
                                        command.Parameters.AddWithValue("@Montagemtotalexecutado", "")
                                        command.Parameters.AddWithValue("@Montagemtotalexecutar", "")

                                        command.Parameters.AddWithValue("@ORDEMSERVICOITEMFINALIZADO", "")
                                        command.Parameters.AddWithValue("@IdPlanodecorte", "")

                                        command.ExecuteNonQuery()
                                    End Using

                                End If

                                ProgressBarListaSW.Value = A
                            Catch ex As Exception

                                ' MsgBox(ex.Message & " ERRO ao ler o arquivo: " & OrdemServico.EnderecoArquivo, MsgBoxStyle.Critical, "Atenção")

                                Continue For

                            Catch ex As MySqlException

                                cl_BancoDados.AbrirBanco()

                            End Try

                        Next A

                        ''''''''''''''cl_BancoDados.Salvar(SQL)

                        ''''''''''''''SQL = Nothing

                    End If


                End If

                MessageBox.Show("Operação finalizada com sucesso, os itens foram inseridos na OS!")

                ProgressBarListaSW.Value = 0
                TimerDGVListaMaterialSW.Enabled = True
            Else

                MsgBox("Operação cancelada, os itens não serão inseridos na OS! Existem RNC em aberto, verificar as linhas marcadas", vbCritical, "Atenção")

            End If

        End If

        'Else

        '    MsgBox("Não será possivel criar OS existe peças com RNC em aberto!", MsgBoxStyle.Critical, "Atenção")

        'End If
    End Sub
    Private Sub cboTitulo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboTitulo.KeyPress

        e.KeyChar = Char.ToUpper(e.KeyChar)

    End Sub

    Private Sub AtualizarDesenhoPeloDiretorioToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Click


        Dim result As DialogResult

        result = MessageBox.Show("Escolha uma opção:  SIM      1 - Atualiza a versão do SolidWork e Salva no banco de dados" & vbCrLf _
                                                   & "NÃO      2 - Somente Atualiza a versão do SolidWorks sem Salvar no Banco de dados" & vbCrLf _
                                                   & "Cancelar 3 -Finaliza a opração sem faze nenhuma alteralção", "Atualizar Versão",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1)

        If result = DialogResult.Yes Then

            ' Opção 1 - Atualizar versão do SW e Salvar no banco
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(True)

        ElseIf result = DialogResult.No Then
            ' Opção 2 - Somente atualizar versão do SW
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(False)

        ElseIf result = DialogResult.Cancel Then
            ' Opção 3 - Cancelar
            MessageBox.Show("A operação foi cancelada.", "Cancelar")

        End If

    End Sub

    Private Sub BuscarArquivosNosDiretorioToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles BuscarArquivosNosDiretorioToolStripMenuItem1.Click

        Arquivos.Show()

    End Sub

    Private Sub TimerpcpAgrupamentoProjeto_Tick(sender As Object, e As EventArgs) Handles TimerpcpAgrupamentoProjeto.Tick

        dgvTimerpcpAgrupamentoProjeto.DataSource = cl_BancoDados.CarregarDados("SELECT
    Projeto,
    Tag,
    CONCAT(ROUND(AVG(QtdeTotal_Finalizado), 2)) AS MediaQtdeTotalFinalizado,
    CONCAT(ROUND(AVG(QtdeTotal_Nao_Finalizado), 2)) AS MediaQtdeTotalNaoFinalizado,
    CONCAT(ROUND(AVG(QtdeTotal_Total), 2)) AS MediaQtdeTotalTotal,
    CONCAT(ROUND(AVG(Percentual_Conclusao), 2), '%') AS MediaPercentualConclusao
FROM
    ViewVisaoGeralOrdemServico where projeto = '" & cboProjetoPCP.Text & "'
GROUP BY 
    Projeto
ORDER BY 
    Projeto, Tag limit 1")

        TimerpcpAgrupamentoProjeto.Enabled = False

    End Sub



    '    Private Sub PreencherGrafico(ByVal idprojeto As String)


    '        ' Limpa qualquer série de dados existente
    '        Chart1.Series.Clear()
    '        Chart1.ChartAreas(0).AxisY.Maximum = 100 ' Ajuste conforme necessário
    '        Chart1.ChartAreas(0).AxisX.Interval = 1 ' Intervalo no eixo X para cada barra

    '        ' Configuração do gráfico para colunas verticais
    '        Chart1.Series.Add("QtdPeçasFabricadas")
    '        Chart1.Series("QtdPeçasFabricadas").ChartType = DataVisualization.Charting.SeriesChartType.Column ' Muda para gráfico de colunas
    '        Chart1.Series("QtdPeçasFabricadas").BorderWidth = 1
    '        Chart1.Series("QtdPeçasFabricadas").IsValueShownAsLabel = True
    '        Chart1.Series("QtdPeçasFabricadas").LabelForeColor = Color.Black
    '        Chart1.Series("QtdPeçasFabricadas").LegendText = "Peças Fabricadas" ' Nome da série na legenda

    '        ' Adiciona as barras de Qtdetotal
    '        Chart1.Series.Add("QtdPeçasTotal")
    '        Chart1.Series("QtdPeçasTotal").ChartType = DataVisualization.Charting.SeriesChartType.Column ' Muda para gráfico de colunas
    '        Chart1.Series("QtdPeçasTotal").Label = DataVisualization.Charting.ChartImageAlignmentStyle.Center ' DataVisualization.Charting.StringAlignment.Center ' Alinha o texto ao centro da coluna
    '        Chart1.Series("QtdPeçasTotal").BorderWidth = 1
    '        Chart1.Series("QtdPeçasTotal").IsValueShownAsLabel = True
    '        Chart1.Series("QtdPeçasTotal").LabelForeColor = Color.Black
    '        Chart1.Series("QtdPeçasTotal").LegendText = "Peças Totais" ' Nome da série na legenda

    '        ' Define as propriedades do LabelStyle para centralizar o valor dentro da barra
    '        For Each point As DataVisualization.Charting.DataPoint In Chart1.Series("QtdPeçasTotal").Points
    '            point.LabelBackColor = Color.Black ' Cor de fundo do rótulo para maior contraste
    '            point.LabelForeColor = Color.White ' Cor do texto
    '            point.LabelAngle = 180 ' Define o ângulo do rótulo (0 para horizontal)
    '            point.IsValueShownAsLabel = True ' Exibe o valor
    '        Next

    '        ' Configuração da legenda
    '        ' A propriedade "Legend" é configurada diretamente no Chart1
    '        Chart1.Legends.Clear()  ' Limpa qualquer legenda existente
    '        Chart1.Legends.Add("Legenda")  ' Adiciona uma nova legenda
    '        Chart1.Legends(0).Docking = DataVisualization.Charting.Docking.Top  ' Posiciona a legenda no topo
    '        Chart1.Legends(0).Alignment = StringAlignment.Center ' Alinha a legenda ao centro

    '        ' Conectar ao banco de dados e buscar os dados
    '        Dim query As String = "SELECT
    '    idprojeto,
    '    idtag,
    '    COALESCE(sum(totalCorte), 0) AS totalCorte,
    '    COALESCE(sum(QtdetotalCorte), 0) AS QtdetotalCorte,
    '    COALESCE(sum(totalDobra), 0) AS totalDobra,
    '    COALESCE(sum(QtdetotalDobra), 0) AS QtdetotalDobra,
    '    COALESCE(sum(totalSolda), 0) AS totalSolda,
    '    COALESCE(sum(QtdetotalSolda), 0) AS QtdetotalSolda,
    '    COALESCE(sum(totalPintura), 0) AS totalPintura,
    '    COALESCE(sum(QtdetotalPintura), 0) AS QtdetotalPintura,
    '    COALESCE(sum(totalMontagem), 0) AS totalMontagem,
    '    COALESCE(sum(QtdetotalMontagem), 0) AS QtdetotalMontagem
    'FROM
    '    viewtotalelovucaoprocessoproducao 
    'WHERE
    '    idprojeto  = '" & idprojeto & "' group by idprojeto"

    '        Dim cmd As New MySqlCommand(query, myconect)
    '        Dim da As New MySqlDataAdapter(cmd)
    '        Dim dt As New System.Data.DataTable()

    '        Try
    '            da.Fill(dt)

    '            ' Iterando os dados para adicionar ao gráfico
    '            For Each row As DataRow In dt.Rows
    '                ' Extraindo os dados e tratando valores nulos
    '                Dim totalCorte As Double = If(IsDBNull(row("totalCorte")), 0, Convert.ToDouble(row("totalCorte")))
    '                Dim QtdetotalCorte As Double = If(IsDBNull(row("QtdetotalCorte")), 0, Convert.ToDouble(row("QtdetotalCorte")))

    '                Dim totalDobra As Double = If(IsDBNull(row("totalDobra")), 0, Convert.ToDouble(row("totalDobra")))
    '                Dim QtdetotalDobra As Double = If(IsDBNull(row("QtdetotalDobra")), 0, Convert.ToDouble(row("QtdetotalDobra")))

    '                Dim totalSolda As Double = If(IsDBNull(row("totalSolda")), 0, Convert.ToDouble(row("totalSolda")))
    '                Dim QtdetotalSolda As Double = If(IsDBNull(row("QtdetotalSolda")), 0, Convert.ToDouble(row("QtdetotalSolda")))

    '                Dim totalPintura As Double = If(IsDBNull(row("totalPintura")), 0, Convert.ToDouble(row("totalPintura")))
    '                Dim QtdetotalPintura As Double = If(IsDBNull(row("QtdetotalPintura")), 0, Convert.ToDouble(row("QtdetotalPintura")))

    '                Dim totalMontagem As Double = If(IsDBNull(row("totalMontagem")), 0, Convert.ToDouble(row("totalMontagem")))
    '                Dim QtdetotalMontagem As Double = If(IsDBNull(row("QtdetotalMontagem")), 0, Convert.ToDouble(row("QtdetotalMontagem")))

    '                ' Adicionando os dados ao gráfico (para Qtdetotal e total)
    '                Chart1.Series("QtdPeçasTotal").Points.AddXY("Corte  ", QtdetotalCorte)
    '                Chart1.Series("QtdPeçasFabricadas").Points.AddXY("Corte ", totalCorte)

    '                Chart1.Series("QtdPeçasTotal").Points.AddXY("Dobra ", QtdetotalDobra)
    '                Chart1.Series("QtdPeçasFabricadas").Points.AddXY("Dobra ", totalDobra)

    '                Chart1.Series("QtdPeçasTotal").Points.AddXY("Solda ", QtdetotalSolda)
    '                Chart1.Series("QtdPeçasFabricadas").Points.AddXY("Solda ", totalSolda)

    '                Chart1.Series("QtdPeçasTotal").Points.AddXY("Pintura ", QtdetotalPintura)
    '                Chart1.Series("QtdPeçasFabricadas").Points.AddXY("Pintura ", totalPintura)

    '                Chart1.Series("QtdPeçasTotal").Points.AddXY("Montagem ", QtdetotalMontagem)
    '                Chart1.Series("QtdPeçasFabricadas").Points.AddXY("Montagem ", totalMontagem)
    '            Next

    '            ' Definindo cores para as barras
    '            Chart1.Series("QtdPeçasFabricadas").Points(0).Color = Color.LightGreen
    '            Chart1.Series("QtdPeçasFabricadas").Points(1).Color = Color.LightBlue
    '            Chart1.Series("QtdPeçasFabricadas").Points(2).Color = Color.LightCoral
    '            Chart1.Series("QtdPeçasFabricadas").Points(3).Color = Color.LightSalmon
    '            Chart1.Series("QtdPeçasFabricadas").Points(4).Color = Color.LightYellow

    '            Chart1.Series("QtdPeçasTotal").Points(0).Color = Color.MediumSeaGreen
    '            Chart1.Series("QtdPeçasTotal").Points(1).Color = Color.RoyalBlue
    '            Chart1.Series("QtdPeçasTotal").Points(2).Color = Color.OrangeRed
    '            Chart1.Series("QtdPeçasTotal").Points(3).Color = Color.Salmon
    '            Chart1.Series("QtdPeçasTotal").Points(4).Color = Color.ForestGreen

    '        Catch ex As Exception
    '        Finally

    '            ' MessageBox.Show("Erro ao carregar dados: " & ex.Message)
    '        End Try


    '    End Sub




    Private Function SafeConvertToDouble(value As String) As Double
        Dim result As Double = -1 ' Valor padrão em caso de erro
        If Not String.IsNullOrEmpty(value) Then
            Try
                value = value.Replace("%", "") ' Remove o % se existir
                result = Convert.ToDouble(value)
            Catch ex As Exception
                ' Loga erro de conversão
                Console.WriteLine("Erro de conversão: " & ex.Message)
                result = -1 ' Valor padrão em caso de erro
            End Try
        End If
        Return result
    End Function

    Private Sub cboProjetoPCP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjetoPCP.SelectedIndexChanged

        Dim idptojeto As String

        TimerpcpAgrupamentoProjeto.Enabled = True

        Try

            ' Verificar se o combo box contém algum valor selecionado
            If cboProjetoPCP.SelectedItem Is Nothing Then
                Throw New Exception("Nenhum projeto selecionado. Por favor, selecione um projeto válido.")


            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboProjetoPCP.Text) Then
                Throw New Exception("O nome do projeto não pode estar vazio. Selecione um projeto válido.")
            End If

            Try

                idptojeto = cboProjetoPCP.SelectedValue


            Catch ex As Exception
                idptojeto = ""
            End Try




            Try

                ' Tentativa de retornar o nome da empresa
                txtClientepcp.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT descempresa FROM projetos where idprojeto  = " & cboProjetoPCP.SelectedValue, "descempresa")

            Catch ex As Exception
                Me.txtClientepcp.Clear()


            End Try
        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
        End Try


        ' PreencherGrafico(idptojeto)


        dgvTimerpcpAgrupamentoProjetoDetalhamento.DataSource = cl_BancoDados.CarregarDados("SELECT idprojeto, idtag,Projeto,Tag, 
totalCorte, QtdetotalCorte, Corte, totalDobra, QtdetotalDobra, Dobra, totalSolda, QtdetotalSolda, Solda, totalPintura,
QtdetotalPintura, Pintura, totalMontagem, QtdetotalMontagem, Montagem FROM viewtotalelovucaoprocessoproducao
WHERE
    idprojeto  = '" & idptojeto & "' group by idprojeto, idtag order by Projeto, Tag")

    End Sub




    'Private Sub CarregarDadosAgrupados()
    '    Dim query As String = "SELECT IDOrdemServico, 
    '                            IDOrdemServicoITEM,
    '                            QtdeTotal,
    '                            CodMatFabricante,
    '                            DescResumo,
    '                            DescDetal,
    '                            MaterialSW,
    '                            Espessura,
    '                            Altura,
    '                            Largura,
    '                            EnderecoArquivo,
    '                            ProdutoPrincipal,
    '                            Qtde, areapintura, Peso
    '                       FROM ordemservicoitem WHERE (D_E_L_E_T_E <> '*')"


    '    ' Preencher o DataTable
    '    Dim adaptador As New MySqlDataAdapter(query, myconect)
    '    Dim tabelaDados As New System.Data.DataTable()

    '    Try
    '        adaptador.Fill(tabelaDados)
    '    Catch ex As Exception
    '        MessageBox.Show("Erro ao carregar dados do banco: " & ex.Message)
    '        Return
    '    End Try

    '    If tabelaDados.Rows.Count = 0 Then
    '        MessageBox.Show("Nenhum dado foi retornado pela consulta.")
    '        Return
    '    End If

    '    ' Agrupar dados manualmente por IDOrdemServico
    '    Dim grupos = New Dictionary(Of String, List(Of DataRow))()
    '    For Each row As DataRow In tabelaDados.Rows
    '        Dim idOrdem As String = row("IDOrdemServico").ToString()
    '        If Not grupos.ContainsKey(idOrdem) Then
    '            grupos(idOrdem) = New List(Of DataRow)()
    '        End If
    '        grupos(idOrdem).Add(row)
    '    Next

    '    ' Configurar o DataGridView
    '    DataGridView1.Columns.Clear()
    '    DataGridView1.Rows.Clear()

    '    ' Adicionar colunas
    '    DataGridView1.Columns.Add("Expandir", "+") ' Coluna para expandir/recolher grupos

    '    ' Ajustar largura e alinhamento da coluna "Expandir"
    '    DataGridView1.Columns("Expandir").Width = 100 ' Largura adequada para o ícone
    '    DataGridView1.Columns("Expandir").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '    ' Alterar a fonte da coluna "Expandir"
    '    DataGridView1.Columns("Expandir").DefaultCellStyle.Font = New System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)


    '    DataGridView1.Columns.Add("IDOrdemServico", "OS")
    '    DataGridView1.Columns.Add("IDOrdemServicoITEM", "ITEM")
    '    DataGridView1.Columns.Add("QtdeTotal", "Qtde")
    '    DataGridView1.Columns.Add("CodMatFabricante", "Código Desenho")
    '    DataGridView1.Columns.Add("DescResumo", "Descrição Resumida")
    '    DataGridView1.Columns.Add("DescDetal", "Descrição Detalhada")
    '    DataGridView1.Columns.Add("MaterialSW", "Material")
    '    DataGridView1.Columns.Add("Espessura", "Espessura")
    '    DataGridView1.Columns.Add("EnderecoArquivo", "Endereço Arquivo")
    '    DataGridView1.Columns.Add("ProdutoPrincipal", "Conjunto Principal")

    '    ' Carregar as imagens de expandir e recolher
    '    'Dim imagemExpandir As System.Drawing.Image = My.Resources.sinal_de_mais  ' System.Drawing.Image.FromFile("caminho_para_sua_imagem_expandir.png") ' Imagem de expandir
    '    ' Dim imagemRecolher As System.Drawing.Image = My.Resources.sinal_de_menos  'System.Drawing.Image.FromFile("caminho_para_sua_imagem_recolher.png") ' Imagem de recolher



    '    ' Adicionar os grupos ao DataGridView
    '    For Each grupo In grupos
    '        ' Adicionar a linha do grupo
    '        Dim indiceGrupo As Integer = DataGridView1.Rows.Add("+", grupo.Key, "------",
    '                                                            "------",
    '                                                            "------",
    '                                                            "------", "------", "------", "------", "------", "")


    '        ' DataGridView1.Rows(DataGridView1.Rows.Count - 1).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold)

    '        ' Alterando a fonte da última linha e a cor da fonte para branco
    '        Dim ultimaLinha As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Count - 1)

    '        ' Definindo a fonte e a cor da fonte (branca)
    '        ultimaLinha.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Italic)
    '        ultimaLinha.DefaultCellStyle.ForeColor = Color.White
    '        ultimaLinha.DefaultCellStyle.BackColor = Color.LightGray





    '        ' Tornar as linhas do grupo ocultas inicialmente
    '        For Each linha In grupo.Value
    '            DataGridView1.Rows.Add("", "",
    '                                   linha("IDOrdemServicoITEM"),
    '                                   linha("QtdeTotal"),
    '                                   linha("CodMatFabricante"),
    '                                   linha("DescResumo"),
    '                                   linha("DescDetal"),
    '                                   linha("MaterialSW"),
    '                                   linha("Espessura"),
    '                                   linha("EnderecoArquivo"),
    '                                   linha("ProdutoPrincipal"))

    '            DataGridView1.Rows(DataGridView1.RowCount - 1).Visible = False
    '            DataGridView1.Rows(DataGridView1.RowCount - 1).Tag = "SubLinha" ' Marcando como sublinha
    '        Next
    '    Next

    '    ' Adicionar evento de clique para expandir/recolher
    '    AddHandler DataGridView1.CellClick, AddressOf DataGridView1_CellClick
    'End Sub

    'Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    '    ' Verificar se a célula clicada é válida e está na coluna de expansão
    '    If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then
    '        Dim linha As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
    '        Dim estadoAtual As String = linha.Cells(0).Value.ToString()

    '        If estadoAtual = "+" Then
    '            ' Expandir o grupo
    '            linha.Cells(0).Value = "-"
    '            For i As Integer = e.RowIndex + 1 To DataGridView1.Rows.Count - 1
    '                If DataGridView1.Rows(i).Tag IsNot Nothing AndAlso DataGridView1.Rows(i).Tag.ToString() = "SubLinha" Then
    '                    DataGridView1.Rows(i).Visible = True
    '                Else
    '                    Exit For
    '                End If
    '            Next
    '        ElseIf estadoAtual = "-" Then
    '            ' Recolher o grupo
    '            linha.Cells(0).Value = "+"
    '            For i As Integer = e.RowIndex + 1 To DataGridView1.Rows.Count - 1
    '                If DataGridView1.Rows(i).Tag IsNot Nothing AndAlso DataGridView1.Rows(i).Tag.ToString() = "SubLinha" Then
    '                    DataGridView1.Rows(i).Visible = False
    '                Else
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '    End If
    'End Sub


End Class