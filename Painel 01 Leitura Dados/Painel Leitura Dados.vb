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


Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas
Imports iText.Kernel.Font
Imports iText.Kernel.Pdf.Xobject
Imports iText.Layout.Element

Imports System.Runtime.CompilerServices.Unsafe

Imports Google.Protobuf.WellKnownTypes

Imports System.Net.Mail
Imports System.Net.Mime
Imports System.IO.Compression

Imports netDxf
Imports netDxf.Entities
Imports netDxf.Tables

Imports iText.IO.Font.Constants
Imports System.ComponentModel
Imports System.Threading.Tasks
Imports Rectangle = System.Drawing.Rectangle
Imports SolidWorks.Interop.dsgnchk
Imports Assembly = System.Reflection.Assembly
Imports iText.Commons.Bouncycastle
Imports System.Data.SqlClient
Imports System.Threading
Imports iText.StyledXmlParser.Jsoup







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
    Public iconeLXDS As System.Drawing.Image



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
                txtNomeArquivo.Text = DadosArquivoCorrente.NomeArquivoSemExtensao

                'Lista de Corte
                Me.lblEspessura.Text = DadosArquivoCorrente.Espessura
                Me.lblLargura.Text = DadosArquivoCorrente.LarguraBlank
                Me.lblComprimento.Text = DadosArquivoCorrente.ComprimentoBlank

                Me.lblNumeroDobra.Text = DadosArquivoCorrente.NumeroDobras
                Me.lblPeso.Text = DadosArquivoCorrente.Massa.ToString
                Me.lblMaterial.Text = DadosArquivoCorrente.material.ToString
                Me.lblAreaPintura.Text = DadosArquivoCorrente.AreaPintura

                Me.lblAlturaTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Alturacaixadelimitadora
                Me.lblLarguraTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Larguracaixadelimitadora
                Me.lblProfundidadeTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Profundidadeaixadelimitadora

                optProcessoSoldagemSim.Checked = (DadosArquivoCorrente.soldagem = "SIM")
                If optProcessoSoldagemSim.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSoldagem", "NÃO", "NÃO")
                End If


                Me.txtIsometrico.Text = DadosArquivoCorrente.EnderecoIsometrico
                Me.txtFichaTecnica.Text = DadosArquivoCorrente.EnderecoFichaTecnica

                '  optProcessoSoldagemNao.Checked = Not optProcessoSoldagemSim.Checked




                OPTEstoqueSim.Checked = (DadosArquivoCorrente.ItemEstoque = "SIM")
                If OPTEstoqueSim.Checked = False Then
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtItemEstoque", "NÃO", "NÃO")
                End If


                '   OPTEstoqueNao.Checked = Not OPTEstoqueSim.Checked

                Me.lblPeso.Text = DadosArquivoCorrente.Massa

                Me.lblMaterial.Text = DadosArquivoCorrente.material.ToString


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
        $"SELECT Sobra_Fabrica FROM  " & ComplementoTipoBanco & "material WHERE CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'",
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
        $"SELECT RNC FROM  " & ComplementoTipoBanco & "material WHERE CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "RNC")

                    If Not String.IsNullOrEmpty(DadosArquivoCorrente.rnc) Then
                        '                ' Recuperar DescricaoPendencia se RNC não estiver vazio
                        '                DadosArquivoCorrente.DescricaoPendencia = cl_BancoDados.RetornaCampoDaPesquisa(
                        '    $"SELECT DescricaoPendencia FROM ordemservicoitempendencia 
                        '      WHERE CodMatFabricante = '{DadosArquivoCorrente.NomeArquivoSemExtensao}' 
                        '      AND Estatus = 'PENDENCIA' 
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
                    If DadosArquivoCorrente.Acabamento.ToString = chkBoxAcabamento.Items(i).ToString Then
                        chkBoxAcabamento.Text = DadosArquivoCorrente.Acabamento
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
                        dgvDataGridBOM.Rows(i).Cells("material").Value = Me.lblMaterial.Text ' UCase(DadosArquivoCorrente.material)
                        dgvDataGridBOM.Rows(i).Cells("Acabamento").Value = UCase(DadosArquivoCorrente.Acabamento)
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

        ordemservicoitem = "UPDATE " & ComplementoTipoBanco & " ordemservicoitem Set DescResumo = @DescResumo, DescDetal = @DescDetal, " &
                "Autor = @Autor, Palavrachave = @Palavrachave, Notas = @Notas, Espessura = @Espessura, AreaPintura = @AreaPintura, " &
                "NumeroDobras = @NumeroDobras, Peso = @Peso, Unidade = @Unidade, Altura = @Altura, Largura = @Largura, " &
                "MaterialSW = @MaterialSW, EnderecoArquivo = @EnderecoArquivo, Acabamento = @Acabamento, " &
                "txtSoldagem = @txtSoldagem, txtTipoDesenho = @txtTipoDesenho, txtCorte = @txtCorte, txtDobra = @txtDobra, " &
                "txtSolda = @txtSolda, txtPintura = @txtPintura, txtMontagem = @txtMontagem, Comprimentocaixadelimitadora = @Compcx, " &
                "Larguracaixadelimitadora = @Largcx, Espessuracaixadelimitadora = @Espcx, txtItemEstoque = @txtItemEstoque " &
                " WHERE IDOrdemServicoItem = @IDOrdemServicoItem"

        '  DGVTimerFiltroPecaAtivaOS.Refresh()

        DadosArquivoCorrente.ArquivoCorrente(swModel)

        DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

        'dados da caixa delimitadora
        DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

        If TipoBanco = "MYSQL" Then

            For i As Integer = 0 To DGVTimerFiltroPecaAtivaOS.Rows.Count - 1

                Using cmdidordemservicoitem As New MySqlCommand(ordemservicoitem, myconect)

                    If Convert.ToBoolean(DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value) = True Then

                        Dim IDOrdemServicoItem As String = DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("IDOrdemServicoItem").Value.ToString


                        ' Adicione os parâmetros ao comando
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Autor", UCase(DadosArquivoCorrente.Author))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Peso", UCase(DadosArquivoCorrente.Massa))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Unidade", "PC")
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Profundidade", String.Empty)
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
                        DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@IDOrdemServicoItem", IDOrdemServicoItem)

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

        ElseIf TipoBanco = "SQL" Then

            For i As Integer = 0 To DGVTimerFiltroPecaAtivaOS.Rows.Count - 1

                Using cmdidordemservicoitem As New SqlCommand(ordemservicoitem, myconectSQL)

                    If Convert.ToBoolean(DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value) = True Then

                        Dim IDOrdemServicoItem As String = DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("IDOrdemServicoItem").Value.ToString


                        ' Adicione os parâmetros ao comando
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Autor", UCase(DadosArquivoCorrente.Author))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Peso", UCase(DadosArquivoCorrente.Massa))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Unidade", "PC")
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Profundidade", String.Empty)
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
                        DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@IDOrdemServicoItem", IDOrdemServicoItem)

                        Try

                            ' Antes de executar o comando, exibir a SQL para verificação
                            Dim sqlComando As String = cmdidordemservicoitem.CommandText

                            ' Loop pelos parâmetros para exibir seus valores
                            For Each param As SqlParameter In cmdidordemservicoitem.Parameters
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
                        Catch ex As SqlException

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

        End If

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


            TabelaViewMontaPeca = cl_BancoDados.CarregarDados("SELECT * FROM  " & ComplementoTipoBanco & "viewmontapeca where D_E_L_E_T_E <> '' OR D_E_L_E_T_E IS NULL")

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
                                    DadosArquivoCorrente.material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.Acabamento,
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
                                    "",'material,
                                    "",'AreaPintura,
                                    "",'NumeroDobras,
                                    Trim(TabelaViewMontaPeca.Rows(i).Item("Peso").ToString) * qtdePecaLm,'Peso,
                                    "",'EnderecoArquivo,
                                    "",'Acabamento,
                                    "",'soldagem,
                                    "material",'TipoDesenho,
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
                                    Trim(TabelaViewMontaPeca.Rows(i).Item("PecaQtde").ToString * qtdePecaLm)) 'qtde,

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
        iconeLXDS = My.Resources.Sem_Incone

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

                '  If tempTable.Rows(a)("EnderecoArquivo").ToString().ToLower().EndsWith(".sldprt") OrElse
                '  tempTable.Rows(a)("EnderecoArquivo").ToString().ToLower().EndsWith(".sldasm") Then

                ' Definir uma lista de extensões permitidas
                Dim extensoesPermitidas As String() = {".sldprt", ".sldasm"}

                ' Obter o caminho do arquivo
                Dim caminhoArquivo As String = tempTable.Rows(a)("EnderecoArquivo").ToString()

                ' Verificar se o arquivo termina com uma das extensões permitidas, ignorando maiúsculas/minúsculas
                If extensoesPermitidas.Any(Function(extensao) caminhoArquivo.EndsWith(extensao, StringComparison.OrdinalIgnoreCase)) Then
                    ' Código para arquivos com extensões permitidas


                    DadosArquivoCorrente.EnderecoArquivo = tempTable.Rows(a)("EnderecoArquivo").ToString()
                    OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, True, swModel)


                    If chkConverterDXF.Checked = True Then

                        '  OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, True, swModel)

                        DadosArquivoCorrente.ExportDXF(swModel, True, True)

                        iconeDXF = My.Resources.arquivo_dxf

                        DadosArquivoCorrente.EnderecoArquivo = tempTable.Rows(a)("EnderecoArquivo").ToString

                    End If

                    'System.Threading.Thread.Sleep(200) ' Delay de 100 milissegundos - edson 19/01/2025

                    Dim pdf As String


                    If chkConverterPDF.Checked = True Then

                        pdf = DadosArquivoCorrente.EnderecoArquivo.ToUpper.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")

                        If File.Exists(pdf) Then

                            OpenDocumentAndWait(pdf, True, swModel)

                            ' Esperar xx segundos após o final do bloco Using
                            Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

                            DadosArquivoCorrente.ExportToPDF(swModel, pdf, False)

                            ' Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

                            iconePDF = My.Resources.ficheiro_pdf

                        End If

                    End If


                    If DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, False) = False Then

                        DadosArquivoCorrente.rnc = "S"

                    End If

                    FormatarColunaIconeDGVListaBom() 'EDSON 18/01/2025



                    dgvDataGridBOM.Rows.Add(My.Resources.Sem_Incone, iconeDXF,
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
                                                DadosArquivoCorrente.material,
                                                DadosArquivoCorrente.AreaPintura,
                                                DadosArquivoCorrente.NumeroDobras,
                                                DadosArquivoCorrente.Massa,
                                                DadosArquivoCorrente.EnderecoArquivo,
                                                DadosArquivoCorrente.Acabamento,
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
                    DadosArquivoCorrente.qtde = tempTable.Rows(a)("qtdePeca")





                    ' ' Condição 2: Arquivo é uma peça, TipoDesenho está vazio, e Liberação é "NÃO"
                    ' If swModel.GetType() = swDocumentTypes_e.swDocPART AndAlso
                    'String.IsNullOrEmpty(DadosArquivoCorrente.TipoDesenho.ToString) AndAlso
                    'My.Settings.LiberaOSsemMaterial = "NÃO" Then

                    '     dgvDataGridBOM.Rows(a).Cells("RNC").Value = "S"
                    '     AlterarEstiloDataGrid(a, dgvDataGridBOM, "txtTipoDesenho", Color.Red, Color.LightPink)
                    ' End If

                    ' ' Condição 3: Arquivo é uma peça, material está vazio, Número de Dobras não está vazio, e Liberação é "NÃO"
                    ' If swModel.GetType() = swDocumentTypes_e.swDocPART AndAlso
                    'String.IsNullOrEmpty(DadosArquivoCorrente.material.ToString) AndAlso
                    'Not String.IsNullOrEmpty(DadosArquivoCorrente.NumeroDobras.ToString) AndAlso
                    'My.Settings.LiberaOSsemMaterial = "NÃO" Then

                    '     dgvDataGridBOM.Rows(a).Cells("RNC").Value = "S"
                    '     AlterarEstiloDataGrid(a, dgvDataGridBOM, "MaterialSW", Color.Red, Color.LightPink)
                    ' End If

                    ' ' Condição 4: Arquivo é uma peça, Espessura não está vazia, material está vazio,
                    ' ' Tipo de Desenho está vazio e Liberação é "NÃO".
                    ' If swModel.GetType() = swDocumentTypes_e.swDocPART AndAlso
                    'Not String.IsNullOrEmpty(DadosArquivoCorrente.Espessura) AndAlso
                    'My.Settings.LiberaOSsemMaterial = "NÃO" AndAlso
                    'String.IsNullOrEmpty(DadosArquivoCorrente.material) AndAlso
                    'String.IsNullOrEmpty(DadosArquivoCorrente.TipoDesenho) Then

                    '     ' Define o valor da célula "RNC" como "S"
                    '     dgvDataGridBOM.Rows(a).Cells("RNC").Value = "S"

                    '     ' Altera o estilo da célula e da linha no DataGridView
                    '     AlterarEstiloDataGrid(a, dgvDataGridBOM, "txtTipoDesenho", Color.Red, Color.LightPink)
                    ' End If



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



                'processedCount = processedCount + 1

                '' Verifica se chegou ao tamanho do lote
                'If processedCount >= 10 Then
                '    ' Reiniciar o SolidWorks
                '    RestartSolidWorks()
                '    processedCount = 0 ' Reiniciar contador

                'End If

                ' Adicionar um pequeno delay para liberar recursos
                System.Threading.Thread.Sleep(My.Settings.TempoRespostaServidor) ' Delay de 100 milissegundos

            Catch ex As Exception

                ' Fechar o documento
                swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                cl_BancoDados.FecharArquivoMemoria()
                IntanciaSolidWorks.LiberarRecurso(swModel)

                '  MsgBox(ex.Message & "Erro na listura de material")

                Continue For

            End Try

            ProgressBarListaSW.Value = a

        Next


        ProgressBarListaSW.Value = 0


        ProgressBarListaSW.Maximum = dgvDataGridBOM.Rows.Count


        For aa As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

            Try


                'Cria as Colunas para ler a lista de material
                ' DadosArquivoCorrente.IdMaterial = dgvDataGridBOM.Rows(aa).Cells("IdMaterial").Value
                DadosArquivoCorrente.NomeArquivoSemExtensao = dgvDataGridBOM.Rows(aa).Cells("CodMatFabricante").Value.ToString
                DadosArquivoCorrente.TipoDesenho = dgvDataGridBOM.Rows(aa).Cells("DescResumo").Value.ToString
                DadosArquivoCorrente.AssuntoSubiTitulo = dgvDataGridBOM.Rows(aa).Cells("DescDetal").Value.ToString
                DadosArquivoCorrente.Author = dgvDataGridBOM.Rows(aa).Cells("Autor").Value.ToString
                DadosArquivoCorrente.PalavraChave = dgvDataGridBOM.Rows(aa).Cells("PalavraChave").Value.ToString
                DadosArquivoCorrente.Comentarios = dgvDataGridBOM.Rows(aa).Cells("Notas").Value.ToString
                DadosArquivoCorrente.Espessura = dgvDataGridBOM.Rows(aa).Cells("Espessura").Value.ToString
                DadosArquivoCorrente.ComprimentoBlank = dgvDataGridBOM.Rows(aa).Cells("Altura").Value.ToString
                DadosArquivoCorrente.LarguraBlank = dgvDataGridBOM.Rows(aa).Cells("Largura").Value.ToString
                DadosArquivoCorrente.material = dgvDataGridBOM.Rows(aa).Cells("material").Value.ToString
                DadosArquivoCorrente.AreaPintura = dgvDataGridBOM.Rows(aa).Cells("AreaPintura").Value.ToString
                DadosArquivoCorrente.NumeroDobras = dgvDataGridBOM.Rows(aa).Cells("NumeroDobras").Value.ToString
                DadosArquivoCorrente.Massa = dgvDataGridBOM.Rows(aa).Cells("Peso").Value.ToString
                DadosArquivoCorrente.EnderecoArquivo = dgvDataGridBOM.Rows(aa).Cells("EnderecoArquivo").Value.ToString
                DadosArquivoCorrente.Acabamento = dgvDataGridBOM.Rows(aa).Cells("Acabamento").Value.ToString
                DadosArquivoCorrente.soldagem = dgvDataGridBOM.Rows(aa).Cells("txtSoldagem").Value.ToString
                DadosArquivoCorrente.TipoDesenho = dgvDataGridBOM.Rows(aa).Cells("txtTipoDesenho").Value.ToString
                DadosArquivoCorrente.Corte = dgvDataGridBOM.Rows(aa).Cells("txtCorte").Value.ToString
                DadosArquivoCorrente.Dobra = dgvDataGridBOM.Rows(aa).Cells("txtDobra").Value.ToString
                DadosArquivoCorrente.Solda = dgvDataGridBOM.Rows(aa).Cells("txtSolda").Value.ToString
                DadosArquivoCorrente.Pintura = dgvDataGridBOM.Rows(aa).Cells("txtPintura").Value.ToString
                DadosArquivoCorrente.Montagem = dgvDataGridBOM.Rows(aa).Cells("txtMontagem").Value.ToString
                DadosArquivoCorrente.rnc = dgvDataGridBOM.Rows(aa).Cells("rnc").Value.ToString
                DadosArquivoCorrente.Alturacaixadelimitadora = dgvDataGridBOM.Rows(aa).Cells("Comprimentocaixadelimitadora").Value.ToString
                DadosArquivoCorrente.Larguracaixadelimitadora = dgvDataGridBOM.Rows(aa).Cells("Larguracaixadelimitadora").Value.ToString
                DadosArquivoCorrente.Profundidadeaixadelimitadora = dgvDataGridBOM.Rows(aa).Cells("Espessuracaixadelimitadora").Value.ToString
                DadosArquivoCorrente.ItemEstoque = dgvDataGridBOM.Rows(aa).Cells("txtItemEstoque").Value.ToString
                DadosArquivoCorrente.qtde = dgvDataGridBOM.Rows(aa).Cells("qtde").Value.ToString

                If TipoBanco = "MYSQL" Then

                    ' Verifica se o registro já existe
                    Dim checkCommand As New MySqlCommand("SELECT COUNT(CodMatFabricante) as CodMatFabricante FROM  " & ComplementoTipoBanco & "material WHERE PecaManuFat = 'S' AND CodMatFabricante = @valor1", myconect)
                    checkCommand.Parameters.AddWithValue("@valor1", DadosArquivoCorrente.NomeArquivoSemExtensao)

                    Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())

                    If count > 0 Then


                        Try


                            ' Se existir, faz um UPDATE
                            Dim updateCommand As New MySqlCommand("UPDATE material SET DescResumo = @DescResumo, DescDetal = @DescDetal, PecaManuFat = @PecaManuFat, " &
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
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@PecaManuFat", "S")
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Autor", UCase(DadosArquivoCorrente.Author))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Peso", UCase(DadosArquivoCorrente.Massa))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Unidade", "PC")
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Profundidade", String.Empty)
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@DtCad", DateTime.Now.Date) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@CodigoJuridicoMat", String.Empty)
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@StatusMat", "A")
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterMysql(updateCommand, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)

                            'If cl_BancoDados.AbrirBanco = False Then

                            '    cl_BancoDados.AbrirBanco()

                            'End If
                            updateCommand.ExecuteNonQuery()

                        Catch ex As Exception
                            MsgBox(ex.Message & " Erro Update - arquivo: " & UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))

                        End Try

                    Else

                        Try


                            ' Se não existir, faz um INSERT
                            Dim insertCommand As New MySqlCommand("INSERT INTO material (DescResumo, DescDetal, PecaManuFat, Autor, Palavrachave, Notas, Espessura, AreaPintura, NumeroDobras, Peso, " &
            "Unidade, Altura, Largura, Profundidade, CodMatFabricante, DtCad, UsuarioCriacao, UsuarioAlteracao, DtAlteracao, CodigoJuridicoMat, " &
            "StatusMat, MaterialSW, EnderecoArquivo, Acabamento, txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, txtPintura, " &
            "txtMontagem, Comprimentocaixadelimitadora, Larguracaixadelimitadora, Espessuracaixadelimitadora, txtItemEstoque) " &
            "VALUES (@DescResumo, @DescDetal, @PecaManuFat, @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, @NumeroDobras, @Peso, @Unidade, " &
            "@Altura, @Largura, @Profundidade, @CodMatFabricante, @DtCad, @UsuarioCriacao, @UsuarioAlteracao, @DtAlteracao, @CodigoJuridicoMat, " &
            "@StatusMat, @MaterialSW, @EnderecoArquivo, @Acabamento, @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, @txtPintura, " &
            "@txtMontagem, @Compcx, @Largcx, @Espcx, @txtItemEstoque)", myconect)
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@PecaManuFat", "S")
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Autor", UCase(DadosArquivoCorrente.Author))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Peso", UCase(DadosArquivoCorrente.Massa))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Unidade", "PC")
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Profundidade", String.Empty)
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@DtCad", DateTime.Now.Date) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@CodigoJuridicoMat", String.Empty)
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@StatusMat", "A")
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterMysql(insertCommand, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)

                            'If cl_BancoDados.AbrirBanco = False Then

                            '    cl_BancoDados.AbrirBanco()

                            'End If

                            insertCommand.ExecuteNonQuery()


                        Catch ex As Exception
                            MsgBox(ex.Message & " Erro Insert - arquivo: " & UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                        End Try


                    End If

                ElseIf TipoBanco = "SQL" Then

                    ' Verifica se o registro já existe
                    Dim checkCommand As New SqlCommand("SELECT COUNT(CodMatFabricante) as CodMatFabricante FROM  " & ComplementoTipoBanco & "material WHERE PecaManuFat = 'S' AND CodMatFabricante = @valor1", myconectSQL)
                    checkCommand.Parameters.AddWithValue("@valor1", DadosArquivoCorrente.NomeArquivoSemExtensao)

                    Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())

                    If count > 0 Then


                        Try


                            ' Se existir, faz um UPDATE
                            Dim updateCommand As New SqlCommand("UPDATE " & ComplementoTipoBanco & "material SET DescResumo = @DescResumo, DescDetal = @DescDetal, PecaManuFat = @PecaManuFat, " &
            "Autor = @Autor, Palavrachave = @Palavrachave, Notas = @Notas, Espessura = @Espessura, AreaPintura = @AreaPintura, " &
            "NumeroDobras = @NumeroDobras, Peso = @Peso, Unidade = @Unidade, Altura = @Altura, Largura = @Largura, " &
            "Profundidade = @Profundidade, UsuarioAlteracao = @UsuarioAlteracao, DtAlteracao = @DtCad, CodigoJuridicoMat = @CodigoJuridicoMat, " &
            "StatusMat = @StatusMat, MaterialSW = @MaterialSW, EnderecoArquivo = @EnderecoArquivo, Acabamento = @Acabamento, " &
            "txtSoldagem = @txtSoldagem, txtTipoDesenho = @txtTipoDesenho, txtCorte = @txtCorte, txtDobra = @txtDobra, " &
            "txtSolda = @txtSolda, txtPintura = @txtPintura, txtMontagem = @txtMontagem, Comprimentocaixadelimitadora = @Compcx, " &
            "Larguracaixadelimitadora = @Largcx, Espessuracaixadelimitadora = @Espcx, txtItemEstoque = @txtItemEstoque " &
            "WHERE CodMatFabricante = @CodMatFabricante", myconectSQL)

                            '    na linha 1387 ,1388 ,1391 tem um parentese so por exemplo       edinho

                            ' Adicione os parâmetros ao comando
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@PecaManuFat", "S")
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Autor", UCase(DadosArquivoCorrente.Author))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Peso", UCase(DadosArquivoCorrente.Massa))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Unidade", "PC")
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Profundidade", String.Empty)
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@DtCad", DateTime.Now.Date) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@CodigoJuridicoMat", String.Empty)
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@StatusMat", "A")
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterSql(updateCommand, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)

                            'If cl_BancoDados.AbrirBanco = False Then

                            '    cl_BancoDados.AbrirBanco()

                            'End If
                            updateCommand.ExecuteNonQuery()

                        Catch ex As Exception
                            MsgBox(ex.Message & " Erro Update - arquivo: " & UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))

                        End Try

                    Else

                        Try


                            ' Se não existir, faz um INSERT
                            Dim insertCommand As New SqlCommand("INSERT INTO  " & ComplementoTipoBanco & "material (DescResumo, DescDetal, PecaManuFat, Autor, Palavrachave, Notas, Espessura, AreaPintura, NumeroDobras, Peso, " &
            "Unidade, Altura, Largura, Profundidade, CodMatFabricante, DtCad, UsuarioCriacao, UsuarioAlteracao, DtAlteracao, CodigoJuridicoMat, " &
            "StatusMat, MaterialSW, EnderecoArquivo, Acabamento, txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, txtPintura, " &
            "txtMontagem, Comprimentocaixadelimitadora, Larguracaixadelimitadora, Espessuracaixadelimitadora, txtItemEstoque) " &
            "VALUES (@DescResumo, @DescDetal, @PecaManuFat, @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, @NumeroDobras, @Peso, @Unidade, " &
            "@Altura, @Largura, @Profundidade, @CodMatFabricante, @DtCad, @UsuarioCriacao, @UsuarioAlteracao, @DtAlteracao, @CodigoJuridicoMat, " &
            "@StatusMat, @MaterialSW, @EnderecoArquivo, @Acabamento, @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, @txtPintura, " &
            "@txtMontagem, @Compcx, @Largcx, @Espcx, @txtItemEstoque)", myconectSQL)
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@PecaManuFat", "S")
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Autor", UCase(DadosArquivoCorrente.Author))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Peso", UCase(DadosArquivoCorrente.Massa))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Unidade", "PC")
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Profundidade", String.Empty)
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@DtCad", DateTime.Now.Date) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@CodigoJuridicoMat", String.Empty)
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@StatusMat", "A")
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                            DadosArquivoCorrente.AddTextParameterSql(insertCommand, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)

                            'If cl_BancoDados.AbrirBanco = False Then

                            '    cl_BancoDados.AbrirBanco()

                            'End If

                            insertCommand.ExecuteNonQuery()


                        Catch ex As Exception
                            MsgBox(ex.Message & " Erro Insert - arquivo: " & UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                        End Try


                    End If


                End If


            Catch ex As Exception

                '  MsgBox(ex.Message & " ERRO NO ARQUIVO: " & UCase(DadosArquivoCorrente.EnderecoArquivo))
                Continue For

            End Try

            ProgressBarListaSW.Value = aa

        Next

        ProgressBarListaSW.Value = 0


        MsgBox("Lista de material Processada com Sucesso!", vbInformation, "Atenção")
        ProgressBarListaSW.Value = 0
        'DisconnectSolidWorks()


        ProgressBarListaSW.Maximum = dgvDataGridBOM.Rows.Count

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


                ProgressBarListaSW.Value = a

            Next

            ProgressBarListaSW.Value = 0

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

            ClasseEmail.EmailTratamentoErro(ex.Message)

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

        dgv.Columns("IdMaterial").Visible = False ''edson 19/01/2025


    End Sub

    ' Oculta uma TabPage
    Public Sub OcultarTabPage(tabControl As TabControl, tabPage As TabPage)
        If tabControl.TabPages.Contains(tabPage) Then
            tabControl.TabPages.Remove(tabPage)
        End If
    End Sub

    ' Exibe uma TabPage
    Public Sub ExibirTabPage(tabControl As TabControl, tabPage As TabPage)
        If Not tabControl.TabPages.Contains(tabPage) Then
            tabControl.TabPages.Add(tabPage)
        End If
    End Sub

    ' Private WithEvents bgWorker As New BackgroundWorker()
    Private Sub Painel_Leitura_Dados_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Configuração do BackgroundWorker
        ' bgWorker.WorkerReportsProgress = True
        'bgWorker.WorkerSupportsCancellation = True

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
               "material",'
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
"qtde"}



        ' Chama a função para criar as colunas
        CriarColunasDataGridView(dgvDataGridBOM, colunas)

        TimerdgvDesenhos.Enabled = True

        ' TipoBanco = "SQLCLIENTE"
        If TipoBanco = "SQL" Then

            cl_BancoDados.ComboBoxDataSet("[View_SZ1010_GESTAO]", "Z1_NUM", "Z1_NUM", cboProjeto, "", "[MP12OFICIAL].[dbo].")

            cl_BancoDados.ComboBoxDataSet("tratamento", "Id_Tratamento", "Tratamento", cboOpcoesAcabamento, "", "[MP12OFICIAL].[dbo].")

            ' Chama a função para carregar os dados no CheckedListBox
            PreencherCheckedListBox("Select DescFamilia from " & ComplementoTipoBanco & "familia WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescFamilia", chkBoxTipoDesenho)

            ' Chama a função para carregar os dados no CheckedListBox
            PreencherCheckedListBox("Select DescAcabamento from " & ComplementoTipoBanco & "acabamento WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescAcabamento ", chkBoxAcabamento)


        ElseIf TipoBanco = "MYSQL" Then

            cl_BancoDados.ComboBoxDataSet("projetos", "idProjeto", "Projeto", cboProjeto, " WHERE (D_E_L_E_T_E Is NULL Or D_E_L_E_T_E = '') and (Finalizado = '' OR Finalizado Is NULL)")
            cl_BancoDados.ComboBoxDataSet("acabamento", "IdAcabamento", "DescAcabamento", cboOpcoesAcabamento, "WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')")
            ' Chama a função para carregar os dados no CheckedListBox
            PreencherCheckedListBox("Select DescFamilia from " & ComplementoTipoBanco & "familia WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescFamilia", chkBoxTipoDesenho)

            ' Chama a função para carregar os dados no CheckedListBox
            PreencherCheckedListBox("Select DescAcabamento from " & ComplementoTipoBanco & "acabamento WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescAcabamento ", chkBoxAcabamento)

        End If



        ' Ocultar tabPage1
        OcultarTabPage(tpgPrincipal, tpgPCP)

        'edson 20-01-2025
        'para verificar a necessidade processamento na abertura de cada arquivo
        ' Timerdgvos.Enabled = True

        'edson 20-01-2025
        'para verificar a necessidade processamento na abertura de cada arquivo
        ' TimerDGVListaMaterialSW.Enabled = True

        Dim version As Version = Assembly.GetExecutingAssembly().GetName().Version

        tslVersaoSistema.Text = My.Settings.BancoDadosAtivo.ToString & ": " & version.ToString()

        '   TimerpcpAgrupamentoProjeto.Enabled = True


        'edson 20-01-2025
        'para verificar a necessidade processamento na abertura de cada arquivo
        ' TimerProdutos.Enabled = True





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
            .Acabamento = Nothing
            .TipoDesenho = Nothing
            .Corte = Nothing
            .Dobra = Nothing
            .Solda = Nothing
            .Pintura = Nothing
            .Montagem = Nothing
            .ItemEstoque = Nothing
            .rnc = Nothing
            .qtde = Nothing

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
            .material = Nothing
            .AreaPintura = Nothing
        End With
    End Sub


    Public Sub PreencherCheckedListBox(ByVal query As String, ByVal clb As CheckedListBox)
        ' Limpa o CheckedListBox antes de preencher
        clb.Items.Clear()

        ' Chama a função CarregarDados para obter os dados
        Dim dt As System.Data.DataTable = cl_BancoDados.CarregarDados(query)

        Try


            ' Verifica se há dados retornados
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' Itera sobre as linhas do DataTable e adiciona ao CheckedListBox
                For Each row As DataRow In dt.Rows
                    ' Supondo que a primeira coluna do DataTable seja o que você quer exibir
                    clb.Items.Add(row(0).ToString())
                Next
                ' Else
                ' MessageBox.Show("Nenhum dado encontrado.")
            End If

        Catch ex As Exception
        Finally
        End Try
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
               MaterialSW,
               Altura,
               Largura,
               AreaPintura,
               NumeroDobras,
               Peso,
                UPPER(RTrim(Replace(EnderecoArquivo, '##', '\\'))) AS EnderecoArquivo,
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
from  " & ComplementoTipoBanco & "material where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')
AND (DescResumo LIKE '%" & Me.TxtPesgTitulo.Text & "%')
and (CodMatFabricante LIKE '%" & Me.TxtPesgNomeDesenho.Text & "%')
and (DescDetal LIKE '%" & Me.TxtPesqSubtitulo.Text & "%')
and (DescDetal LIKE '%" & Me.TxtPesqSubtitulo2.Text & "%')
and (DescDetal LIKE '%" & Me.TxtPesqSubtitulo3.Text & "%')
and   (EnderecoArquivo <> '' AND StatusMat = 'A')
       ORDER BY 
    CASE 
        WHEN RNC IS NOT NULL AND RNC <> '' THEN 0 
        ELSE 1 
    END,
    CodMatFabricante, 
    DescResumo limit 100;")

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

        If OrdemServico.Liberado_Engenharia <> "" Then

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
D_E_L_E_T_E, Valor, pecaqtde, Peso
From  " & ComplementoTipoBanco & " viewmontapeca1
Where NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'
And (D_E_L_E_T_E IS NOT NULL or D_E_L_E_T_E is null)
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
            DGVMontaPeca.Columns("D_E_L_E_T_E").Visible = False

        End If



        'edson 20-01-2025
        'para verificar a necessidade processamento na abertura de cada arquivo
        '''For Each col As DataGridViewColumn In DGVMontaPeca.Columns
        '''    If col.Width > 400 Then
        '''        col.Width = 401
        '''    End If
        '''Next

        TimerMontaPeca.Enabled = False

    End Sub

    Private Sub cboProjeto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjeto.SelectedIndexChanged


        Try

            'OrdemServico.Projeto = cboProjeto.Text
            'OrdemServico.idProjeto = Convert.ToInt32(cboProjeto.SelectedValue)

            ' Verificar se o combo box contém algum valor selecionado
            If cboProjeto.SelectedItem Is Nothing Then
                Throw New Exception("Nenhum Projeto selecionado. Por favor, selecione um Projeto válido.")
            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboProjeto.Text) Then
                Throw New Exception("O nome do Projeto não pode estar vazio. Selecione um Projeto válido.")
            End If

            ' Atribuir valores ao objeto OrdemServico
            OrdemServico.Projeto = cboProjeto.Text

            Try

                ' Tentar converter o valor selecionado para inteiro
                ' Dim idProjeto As Integer
                If Integer.TryParse(cboProjeto.SelectedValue?.ToString(), OrdemServico.idProjeto) Then

                    If TipoBanco = "MYSQL" Then

                        OrdemServico.idProjeto = OrdemServico.idProjeto

                        ' Preenchendo o ComboBox com os dados do banco
                        cl_BancoDados.ComboBoxDataSet("tags", "idTag", "Tag", cboTag, " where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')   and (Finalizado = '' OR Finalizado Is NULL) AND idProjeto = '" & OrdemServico.idProjeto & "'")

                        ' Tentativa de retornar o nome da empresa
                        txtCliente.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DescEmpresa FROM  " & ComplementoTipoBanco & "projetos where idProjeto  = " & OrdemServico.idProjeto, "DescEmpresa")


                    ElseIf TipoBanco = "SQL" Then

                        'codificação codigo protheus
                        OrdemServico.idProjeto = cl_BancoDados.FormatarPara6Caracteres(OrdemServico.idProjeto)

                        ' Preenchendo o ComboBox com os dados do banco
                        cl_BancoDados.ComboBoxDataSet("View_SZ2010_GESTAO", "Z2_PRODUTO", "Z2_DESC", cboTag, " where Z1_NUM = '" & OrdemServico.idProjeto & "'", "[MP12OFICIAL].[dbo].")

                        ' Tentativa de retornar o nome da empresa
                        txtCliente.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT Z1_DESCLI FROM  " & "[MP12OFICIAL].[dbo].[View_SZ1010_GESTAO] where Z1_NUM  = " & OrdemServico.idProjeto, "Z1_DESCLI")


                    End If

                End If



                OrdemServico.Tag = cboTag.Text

                OrdemServico.DescEmpresa = txtCliente.Text

            Catch ex As Exception
                Me.txtCliente.Clear()
                OrdemServico.Projeto = Nothing
                OrdemServico.Tag = Nothing
                OrdemServico.DescEmpresa = Nothing

            End Try
        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
        End Try

    End Sub

    Private Sub cboTag_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTag.SelectedIndexChanged



        Try

            'OrdemServico.Tag = cboTag.Text
            'OrdemServico.idTag = cboTag.SelectedValue

            ' Verificar se o combo box contém algum valor selecionado
            If cboTag.SelectedItem Is Nothing Then
                Throw New Exception("Nenhuma Tag selecionada. Por favor, selecione uma Tag válida.")
            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboTag.Text) Then
                Throw New Exception("O nome da Tag não pode estar vazio. Selecione uma Tag válida.")
            End If

            ' Atribuir o texto da Tag ao objeto OrdemServico
            OrdemServico.Tag = cboTag.Text

            ' Tentar converter o valor selecionado para inteiro
            ' Dim idTag As Integer
            If Integer.TryParse(cboTag.SelectedValue?.ToString(), OrdemServico.idTag) Then

                ' OrdemServico.idTag = cl_BancoDados.FormatarPara7Caracteres(OrdemServico.idTag)

                OrdemServico.idTag = OrdemServico.idTag

                '    OrdemServico.idTag = idTag
                ' Else
                '   Throw New Exception("O ID da Tag selecionada não é válido. Por favor, verifique.")
            End If


            If TipoBanco = "SQL" Then

                ' Tenta retornar a descrição da Tag
                txtDescricaoTag.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DISTINCT(Z2_DESC) FROM [MP12OFICIAL].[dbo].[View_SZ2010_GESTAO] where Z2_PRODUTO = '" & OrdemServico.idTag & "'", "Z2_DESC")

                Try
                    OrdemServico.DataPrevisao = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DataPrevisao FROM [MP12OFICIAL].[dbo].[View_SZ2010_GESTAO] where Z2_PRODUTO = '" & OrdemServico.idTag & "'", "DataPrevisao")

                Catch ex As Exception

                    OrdemServico.DataPrevisao = ""

                End Try

            ElseIf TipoBanco = "MYSQL" Then

                ' Tenta retornar a descrição da Tag
                txtDescricaoTag.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DescTag FROM  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "DescTag")



                Try
                    OrdemServico.DataPrevisao = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DataPrevisao FROM  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "DataPrevisao")

                Catch ex As Exception

                    OrdemServico.DataPrevisao = ""

                End Try

            End If

            ' TipoBanco = "SQL"


        Catch ex As Exception
            ' Em caso de erro, limpa o campo de descrição
            Me.txtDescricaoTag.Clear()

            OrdemServico.Tag = Nothing
            OrdemServico.idTag = Nothing

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
            Filtro = " AND (Liberado_Engenharia = '' OR Liberado_Engenharia IS NULL )"
        End If

        dgvos.DataSource = cl_BancoDados.CarregarDados("SELECT IdOrdemServico,
                                                idProjeto,
                                                Projeto,
                                                Tag,
                                                idTag,
                                                Descricao,
                                                DescEmpresa,
                                                replace(EnderecoOrdemServico,'##','\\') as ENDERECO,
                                                CriadoPor as USUARIO,
                                                DataCriacao as DATA,
                                                Liberado_Engenharia,
                                                Data_Liberacao_Engenharia,
                                                Estatus,
                                                DataPrevisao,ProdutoPadrao
                                                FROM  " & ComplementoTipoBanco & "ordemservico WHERE (D_E_L_E_T_E <> '*' or D_E_L_E_T_E is null)" & Filtro &
                                                " AND CriadoPor LIKE '%" & Me.txtPesqCriadoPor.Text & "%'")

        ' Configura a visibilidade das colunas
        With dgvos.Columns
            .Item("USUARIO").Visible = True
            .Item("DATA").Visible = False
            .Item("ENDERECO").Visible = True
            .Item("idTag").Visible = False
            .Item("idProjeto").Visible = False
            .Item("Estatus").Visible = False
        End With

        Timerdgvos.Enabled = False

    End Sub

    Private Sub chkMostraLiberadasPelaEngenharia_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostraLiberadasPelaEngenharia.CheckedChanged
        Timerdgvos.Enabled = True
    End Sub

    Private Sub dgvos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvos.CellFormatting

        If dgvos.Columns(e.ColumnIndex).Name = "Liberado_Engenharia" AndAlso e.Value IsNot Nothing Then
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
            If dgvos.CurrentRow IsNot Nothing AndAlso dgvos.CurrentRow.Cells("IdOrdemServico").Value IsNot Nothing Then

                OrdemServico.IdOrdemServico = dgvos.CurrentRow.Cells("IdOrdemServico").Value.ToString
                OrdemServico.idProjeto = dgvos.CurrentRow.Cells("idProjeto").Value.ToString
                OrdemServico.idTag = dgvos.CurrentRow.Cells("idTag").Value.ToString

                OrdemServico.Projeto = dgvos.CurrentRow.Cells("Projeto").Value.ToString
                OrdemServico.Tag = dgvos.CurrentRow.Cells("Tag").Value.ToString

                OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString

                OrdemServico.Estatus = dgvos.CurrentRow.Cells("Estatus").Value.ToString

                OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("ENDERECO").Value.ToString

                OrdemServico.DataPrevisao = dgvos.CurrentRow.Cells("DataPrevisao").Value.ToString

                Me.lblOrdemServicoAtiva.Text = "Projeto: " & OrdemServico.Projeto & " - Tag: " & OrdemServico.Tag & " - OS: " & OrdemServico.IdOrdemServico

                Me.cboProjeto.Text = OrdemServico.Projeto
                Me.txtCliente.Text = dgvos.CurrentRow.Cells("DescEmpresa").Value.ToString
                Me.cboTag.Text = OrdemServico.Tag
                Me.txtDescricaoTag.Text = OrdemServico.Tag
                Me.txtDescricao.Text = dgvos.CurrentRow.Cells("Descricao").Value.ToString




            End If
        Catch ex As Exception

            OrdemServico.IdOrdemServico = Nothing
            ' Exibe uma mensagem de erro caso ocorra alguma exceção
            'MessageBox.Show("Erro ao selecionar Ordem de Serviço: " & ex.Message)
        Finally

        End Try

        If OrdemServico.IdOrdemServico.ToString <> "" And OrdemServico.Liberado_Engenharia.ToString = "S" Then

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







            DGVListaMaterialSW.DataSource = cl_BancoDados.CarregarDados("SELECT 
            IdOrdemServicoItem,
            IdOrdemServico,
            Projeto,
            Tag,
            CodMatFabricante,
            Fator,
            qtde,
            QtdeTotal,
            DescResumo,
            DescDetal,
            Estatus_OrdemServico,
            IdMaterial, 
            CriadoPor,
            DataCriacao,
            Estatus,
            Acabamento,
            D_E_L_E_T_E,
            OrdemServicoItemFinalizado,
            IdEmpresa,
            idProjeto,
            IdTag,
            Autor,
            Palavrachave,
            Notas,
            Espessura,
            MaterialSW,
            AreaPintura,
            NumeroDobras,
            Peso,
            Unidade,
            UnidadeSW,
            ValorSW,
            Altura,
            Largura,
            DtCad,
            UsuarioCriacao,
            UsuarioAlteracao,
            DtAlteracao,
            UPPER(RTrim(Replace(EnderecoArquivo, '##', '\\'))) AS EnderecoArquivo,
            txtSoldagem,
            txtTipoDesenho,
            txtCorte,
            txtDobra,
            txtSolda,
            txtPintura,
            txtMontagem,
            QtdeRomaneio, 
            Liberado_Engenharia, 
            Data_Liberacao_Engenharia,
            descempresa, 
            ProdutoPrincipal, 
            RNC, 
            Comprimentocaixadelimitadora,
            Larguracaixadelimitadora,
            Espessuracaixadelimitadora, 
            txtItemEstoque,
            AreaPinturaUnitario,
            PesoUnitario, 
            DataPrevisao
                           FROM
                 " & ComplementoTipoBanco & "ordemservicoitem
                WHERE
                (D_E_L_E_T_E <> '*')
                AND (IdOrdemServico = " & OrdemServico.IdOrdemServico & ")
                AND (CodMatFabricante LIKE '%" & Me.txtPesqNumeroDesenho.Text & "%')
                AND (Acabamento LIKE '%" & Me.txtPesqAcabamentoDesenho.Text & "%')
                ORDER BY
                    IDOrdemServicoItem")


            DGVListaMaterialSW.Columns("IdOrdemServicoItem").Visible = False 
            DGVListaMaterialSW.Columns("IdOrdemServico").Visible = False
            DGVListaMaterialSW.Columns("Projeto").Visible = False
            DGVListaMaterialSW.Columns("Tag").Visible = False
            DGVListaMaterialSW.Columns("Estatus_OrdemServico").Visible = False 
             DGVListaMaterialSW.Columns("IdMaterial").Visible = False  
            DGVListaMaterialSW.Columns("QtdeTotal").Visible = True 
             DGVListaMaterialSW.Columns("CriadoPor").Visible = False 
             DGVListaMaterialSW.Columns("DataCriacao").Visible = False 
             DGVListaMaterialSW.Columns("Estatus").Visible = False 
             DGVListaMaterialSW.Columns("Acabamento").Visible = True 
             DGVListaMaterialSW.Columns("D_E_L_E_T_E").Visible = False 
             DGVListaMaterialSW.Columns("OrdemServicoItemFinalizado").Visible = False 
             DGVListaMaterialSW.Columns("IdEmpresa").Visible = False 
             DGVListaMaterialSW.Columns("idProjeto").Visible = False 
             DGVListaMaterialSW.Columns("IdTag").Visible = False 
             DGVListaMaterialSW.Columns("DescResumo").Visible = True 
             DGVListaMaterialSW.Columns("DescDetal").Visible = True 
             DGVListaMaterialSW.Columns("Autor").Visible = False 
             DGVListaMaterialSW.Columns("Palavrachave").Visible = False 
             DGVListaMaterialSW.Columns("Notas").Visible = False 
             DGVListaMaterialSW.Columns("Espessura").Visible = True 
             DGVListaMaterialSW.Columns("AreaPintura").Visible = False 
             DGVListaMaterialSW.Columns("NumeroDobras").Visible = False 
             DGVListaMaterialSW.Columns("Peso").Visible = False 
             DGVListaMaterialSW.Columns("Unidade").Visible = False 
             DGVListaMaterialSW.Columns("UnidadeSW").Visible = False 
             DGVListaMaterialSW.Columns("ValorSW").Visible = False 
             DGVListaMaterialSW.Columns("Altura").Visible = False 
             DGVListaMaterialSW.Columns("Largura").Visible = False 
             DGVListaMaterialSW.Columns("CodMatFabricante").Visible = True 
             DGVListaMaterialSW.Columns("DtCad").Visible = False 
             DGVListaMaterialSW.Columns("UsuarioCriacao").Visible = False 
             DGVListaMaterialSW.Columns("UsuarioAlteracao").Visible = False 
             DGVListaMaterialSW.Columns("DtAlteracao").Visible = False 

             DGVListaMaterialSW.Columns("MaterialSW").Visible = False 
             DGVListaMaterialSW.Columns("Fator").Visible = True 
             DGVListaMaterialSW.Columns("qtde").Visible = True
            DGVListaMaterialSW.Columns("txtSoldagem").Visible = False
            DGVListaMaterialSW.Columns("txtTipoDesenho").Visible = False
            DGVListaMaterialSW.Columns("txtCorte").Visible = True 
             DGVListaMaterialSW.Columns("txtDobra").Visible = True 
             DGVListaMaterialSW.Columns("txtSolda").Visible = True 
             DGVListaMaterialSW.Columns("txtPintura").Visible = True 
             DGVListaMaterialSW.Columns("txtMontagem").Visible = True 
             DGVListaMaterialSW.Columns("QtdeRomaneio").Visible = False
            DGVListaMaterialSW.Columns("Liberado_Engenharia").Visible = False
            DGVListaMaterialSW.Columns("Data_Liberacao_Engenharia").Visible = False
            DGVListaMaterialSW.Columns("descempresa").Visible = False
            DGVListaMaterialSW.Columns("ProdutoPrincipal").Visible = False
            DGVListaMaterialSW.Columns("RNC").Visible = False
            DGVListaMaterialSW.Columns("Comprimentocaixadelimitadora").Visible = False
            DGVListaMaterialSW.Columns("Larguracaixadelimitadora").Visible = False
            DGVListaMaterialSW.Columns("Espessuracaixadelimitadora").Visible = False
            DGVListaMaterialSW.Columns("txtItemEstoque").Visible = False
            DGVListaMaterialSW.Columns("AreaPinturaUnitario").Visible = False
            DGVListaMaterialSW.Columns("PesoUnitario").Visible = False
            DGVListaMaterialSW.Columns("DataPrevisao").Visible = False

            ' DGVListaMaterialSW.Columns("IdOrdemServico").Visible = False



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
        IDOrdemServicoItem,
          qtdeTotal,
         UPPER(RTrim(CodMatFabricante)) As CodMatFabricante,
         UPPER(RTrim(DescResumo)) As DescResumo,
                    UPPER(RTrim(DescDetal)) As DescDetal,
                    UPPER(RTrim(Autor)) As Autor,
                    UPPER(RTrim(Palavrachave)) As Palavrachave,
                    UPPER(RTrim(Notas)) As Notas,
                    Espessura,
                    Altura,
                    Largura,
                    Replace(AreaPintura, ',', '.') AS AreaPintura,
                    AreaPinturaUnitario,
                    NumeroDobras,
                    Peso,
                    PesoUnitario,
                    UPPER(RTrim(Unidade)) As Unidade,
                    IdOrdemServico,
                    UPPER(RTrim(Projeto)) As Projeto,
                    UPPER(RTrim(Tag)) As Tag,
                    UPPER(RTrim(ESTATUS_OrdemServico)) As ESTATUS_OrdemServico,
                    IdMaterial,
                    qtdeProduzida,
                    qtdeFaltante,
                    UPPER(RTrim(CriadoPor)) As CriadoPor,
                    DataCriacao,
                    UPPER(RTrim(Estatus)) As Estatus,
                    D_E_L_E_T_E,
                    UPPER(RTrim(ORDEMSERVICOITEMFINALIZADO)) As ORDEMSERVICOITEMFINALIZADO,
                    IdEmpresa,
                    idProjeto,
                    idTag,
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
                    UPPER(RTrim(Acabamento)) As Acabamento,
                    UPPER(RTrim(txtTipoDesenho)) As TipoDesenho,
                    UPPER(RTrim(MaterialSW)) As material,
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
             FROM  " & ComplementoTipoBanco & "ordemservicoitem
             WHERE D_E_L_E_T_E <> '*'
               AND txtTipoDesenho <> 'material'
               AND IdOrdemServico = @IdOrdemServico
               AND CodMatFabricante LIKE @NumeroDesenho
               AND Acabamento LIKE @Acabamento
             ORDER BY IDOrdemServicoItem"

            Dim parametrosListaMaterial = New Dictionary(Of String, Object) From {
            {"@IdOrdemServico", OrdemServico.IdOrdemServico},
            {"@NumeroDesenho", $"%{Me.txtPesqNumeroDesenho.Text.Trim()}%"},
            {"@Acabamento", $"%{Me.txtPesqAcabamentoDesenho.Text.Trim()}%"}
        }

            Dim sqlListaMaterialSW As String =
            "SELECT IdOrdemServico,
                    CodMatFabricante,
                    DescResumo,
                    DescDetal,
                    SUM(REPLACE(QtdeTotal, ',', '.')) AS QtdeTotal,
                    txtTipoDesenho
             FROM  " & ComplementoTipoBanco & "ordemservicoitem
             WHERE D_E_L_E_T_E <> '*'
               AND IdOrdemServico = @IdOrdemServico
               AND txtTipoDesenho = 'material'
             GROUP BY IdOrdemServico,
                      CodMatFabricante,
                      DescResumo,
                      DescDetal,
                      txtTipoDesenho
             ORDER BY IdOrdemServico"

            Dim parametrosListaMaterialSW = New Dictionary(Of String, Object) From {
            {"@IdOrdemServico", OrdemServico.IdOrdemServico}
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
        "IdOrdemServico", "Projeto", "Tag", "ESTATUS_OrdemServico", "IdMaterial",
        "QtdeProduzida", "QtdeFaltante", "CriadoPor", "DataCriacao", "Estatus",
        "D_E_L_E_T_E", "ORDEMSERVICOITEMFINALIZADO", "IdEmpresa", "idProjeto",
        "idTag", "Autor", "Palavrachave", "Notas", "AreaPinturaUnitario", "NumeroDobras",
        "PesoUnitario", "Unidade", "UnidadeSW", "ValorSW", "DtCad", "UsuarioCriacao",
        "UsuarioAlteracao", "DtAlteracao", "EnderecoArquivo", "MaterialSW", "Fator",
        "qtde", "material", "ProdutoPrincipal", "txtItemEstoque", "txtSoldagem",
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

        OrdemServico.IdOrdemServico = Nothing
        OrdemServico.Projeto = Nothing
        OrdemServico.Tag = Nothing
        OrdemServico.Descricao = Nothing
        OrdemServico.Estatus = Nothing
        OrdemServico.idTag = Nothing
        OrdemServico.idProjeto = Nothing
        OrdemServico.DescEmpresa = Nothing

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
            OrdemServico.Descricao = Me.txtDescricao.Text
            OrdemServico.idTag = cboTag.SelectedValue
            OrdemServico.idProjeto = cboProjeto.SelectedValue

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

                    If OrdemServico.IdOrdemServico = Nothing Or OrdemServico.IdOrdemServico = 0 Then

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
                                        OrdemServico.MaterialSW = dgvDataGridBOM.Rows(A).Cells("material").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.MaterialSW = ""

                                    End Try

                                    Try
                                        OrdemServico.qtde = dgvDataGridBOM.Rows(A).Cells("qtde").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.qtde = 0

                                    End Try

                                    Try
                                        OrdemServico.AreaPintura = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                        OrdemServico.AreaPintura = OrdemServico.AreaPintura * OrdemServico.qtde * Fator
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

                                        OrdemServico.Peso = OrdemServico.Peso * OrdemServico.qtde * Fator
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
                                        OrdemServico.QtdeTotal = OrdemServico.qtde * Fator
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
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                                    Using command As New MySqlCommand(query, myconect)
                                        ' Adicionando os parâmetros
                                        command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                        command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                        command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                        command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                        command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                        command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                                        command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                        command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                        command.Parameters.AddWithValue("@QtdeProduzida", "")
                                        command.Parameters.AddWithValue("@QtdeFaltante", "")
                                        command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                        command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                                        command.Parameters.AddWithValue("@Estatus", "A")
                                        command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                        command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                        command.Parameters.AddWithValue("@fator", Fator)
                                        command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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
            OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString()

            If Not String.IsNullOrWhiteSpace(OrdemServico.EnderecoOrdemServico) Then
                Process.Start("Explorer", OrdemServico.EnderecoOrdemServico)
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

        Cursor.Current = Cursors.WaitCursor

        '
        Try
            ' Verifica se há uma linha selecionada no DataGridView
            If dgvos.CurrentRow IsNot Nothing Then
                ' Verifica e obtém o valor da célula "Estatus"
                If dgvos.CurrentRow.Cells("Estatus") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Estatus").Value IsNot DBNull.Value Then
                    OrdemServico.Estatus = dgvos.CurrentRow.Cells("Estatus").Value.ToString()
                Else
                    OrdemServico.Estatus = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "ENDERECO"
                If dgvos.CurrentRow.Cells("ENDERECO") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("ENDERECO").Value IsNot DBNull.Value Then
                    OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("ENDERECO").Value.ToString()
                Else
                    OrdemServico.EnderecoOrdemServico = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "Liberado_Engenharia"
                If dgvos.CurrentRow.Cells("Liberado_Engenharia") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Liberado_Engenharia").Value IsNot DBNull.Value Then
                    OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString()
                Else
                    OrdemServico.Liberado_Engenharia = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "Descricao"
                If dgvos.CurrentRow.Cells("Descricao") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Descricao").Value IsNot DBNull.Value Then
                    OrdemServico.Descricao = dgvos.CurrentRow.Cells("Descricao").Value.ToString()
                Else
                    OrdemServico.Descricao = String.Empty ' Valor padrão em caso de ausência
                End If
            Else
                ' Tratar caso não exista uma linha selecionada
                Throw New Exception("Nenhuma linha selecionada no DataGridView.")
            End If


            If OrdemServico.Liberado_Engenharia = "S" Then

                MsgBox("OS Já liberada, não e possivel liberar novamente!", vbInformation, "Atenção")

                Exit Sub

            Else

                Dim diretorio As String = OrdemServico.EnderecoOrdemServico

                LimparDiretorio(diretorio & "\PDF")
                LimparDiretorio(diretorio & "\DXF")
                LimparDiretorio(diretorio & "\DFT")
                LimparDiretorio(diretorio & "\LXDS")



                ImportarLXDSParaOS(DGVListaMaterialSW, "DXF", ProgressBarProcessoLiberacaoOrdemServico)
                ImportarLXDSParaOS(DGVListaMaterialSW, "PDF", ProgressBarProcessoLiberacaoOrdemServico)
                ImportarLXDSParaOS(DGVListaMaterialSW, "DFT", ProgressBarProcessoLiberacaoOrdemServico)
                ImportarLXDSParaOS(DGVListaMaterialSW, "LXDS", ProgressBarProcessoLiberacaoOrdemServico)

                ' Try

                cl_BancoDados.Salvar("Update ordemservico set Liberado_Engenharia = 'S', 
                        Data_Liberacao_Engenharia = '" & Date.Now & "' 
                        where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")

                cl_BancoDados.Salvar("Update ordemservicoitem set Liberado_Engenharia = 'S', 
                        Data_Liberacao_Engenharia = '" & Date.Now & "' 
                        where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")
                dgvos.CurrentRow.Cells("Liberado_Engenharia").Value = "S"
                dgvos.CurrentRow.Cells("Data_Liberacao_Engenharia").Value = Date.Now
                dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.verificado1
                dgvos.Refresh()

                Try

                    If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                        PadraoMetta.ExportarOrdemServicoPadraoMettaAntigo(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, True, DGVListaMaterialSWMaterial)

                    Else

                        TemplatesExcel.ExportarOrdemServicoPadrao(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, DGVListaMaterialSWMaterial)

                    End If

                Catch ex As Exception

                Finally

                End Try

                If Usuario.EnviarEmailLiberacaoOS <> "" Then

                    Dim resultado As MsgBoxResult = MessageBox.Show("Deseja enviar o e-mail para o PCP, de comunicado de Liberação da Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Liberação", MessageBoxButtons.YesNo)

                    If resultado = DialogResult.Yes Then

                        ClasseEmail.EmailLiberacaoOS()

                    End If

                End If

            End If

        Catch ex As Exception


        Finally

        End Try

        ''''  Email.EnviarEmailComOs01(OrdemServico.EnderecoOrdemServico, OrdemServico.Descricao, Date.Now)
        ' Retornar o cursor ao normal
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Click

        Try
            OrdemServico.IDOrdemServicoItem = DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "SIM", "IDOrdemServicoItem", OrdemServico.IDOrdemServicoItem)

            DGVListaMaterialSW.CurrentRow.Cells("ProdutoPrincipal").Value = "SIM".ToUpper

            DGVListaMaterialSW.CurrentRow.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal
        Catch ex As Exception

            OrdemServico.IDOrdemServicoItem = Nothing

            MsgBox("Item da Ordem de Serviço não Valido", vbCritical, "Atenção")

        End Try

    End Sub

    Private Sub DesmarcarComoConjuntoPrincipalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesmarcarComoConjuntoPrincipalToolStripMenuItem.Click

        Try
            OrdemServico.IDOrdemServicoItem = DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "", "IDOrdemServicoItem", OrdemServico.IDOrdemServicoItem)

            DGVListaMaterialSW.CurrentRow.Cells("ProdutoPrincipal").Value = "SIM".ToUpper

            DGVListaMaterialSW.CurrentRow.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW
        Catch ex As Exception

            OrdemServico.IDOrdemServicoItem = Nothing

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
                    MaterialSW = ObjetoDgv.Rows(i).Cells("material").Value.ToString
                Catch ex As Exception
                    MaterialSW = "Sem material"
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

        NumeroOS = dgvos.CurrentRow.Cells("IdOrdemServico").Value.ToString().ToUpper().Trim()

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

    Private Function ImportarPDFParaOSIndividual(ByVal Endereco As String, ByVal qtde As String) As Boolean

        ImportarPDFParaOSIndividual = False

        Dim Origem, Destino, Prefixo, QtdeTotal, OS, TextoPdf As String
        Dim caminhoArquivoDestino As String

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
        Destino = Path.Combine(Destino, "PDF") ' Usar Path.Combine para melhor manipulação de caminhos

        OS = dgvos.CurrentRow.Cells("IdOrdemServico").Value.ToString()

        Try

            ' Acessando os valores diretamente da linha
            Origem = Endereco

            Origem = Replace(Origem, ".SLDPRT", ".PDF", , , CompareMethod.Text)
            Origem = Replace(Origem, ".SLDASM", ".PDF", , , CompareMethod.Text)

            ' Tentar obter QtdeTotal com tratamento de exceção
            Try
                QtdeTotal = qtde
            Catch ex As Exception
                QtdeTotal = "Sem Quantidade"
            End Try

            Prefixo = $"{QtdeTotal} - "
            TextoPdf = "OS: " & OS & " qtde: " & qtde


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
                MaterialSW = ObjetoDgv.Rows(i).Cells("material").ToString
            Catch ex As Exception
                MaterialSW = "Sem material"
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
                MaterialSW = "Sem material"
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

                ' Obtém o nome do arquivo sem a extensão
                Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                ' Obtém a extensão do arquivo
                Dim extensaoArquivo As String = Path.GetExtension(Origem)

                Dim novoNomeArquivo As String

                ' Verifica qual formato de exportação foi selecionado
                If My.Settings.ParametroExportarDXF = "1" Then
                    novoNomeArquivo = $"OS_{OrdemServico.IdOrdemServico} - {Espessura} - {MaterialSW} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                ElseIf My.Settings.ParametroExportarDXF = "2" Then
                    novoNomeArquivo = $"OS_{OrdemServico.IdOrdemServico} -{QtdeTotal} - {nomeArquivoSemExtensao} - {MaterialSW} - {Espessura}{extensaoArquivo}"
                Else
                    MessageBox.Show("Nenhuma opção de exportação de LXDS selecionada. Vá nas configurações e selecione a opção desejada!")
                    Exit For ' Saída antecipada se não houver configuração válida
                End If

                If ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    novoNomeArquivo = $"OS_{OrdemServico.IdOrdemServico} - {tipoDesenho} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                End If

                caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)

                If Pasta = "PDF" Then

                    ' EditarPDF(Origem, caminhoArquivoDestino, novoNomeArquivo)

                    File.Copy(Origem, caminhoArquivoDestino, True)

                Else

                    ' Copia o arquivo para o destino diretamente se não for PDF
                    File.Copy(Origem, caminhoArquivoDestino, True)

                End If


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

    Public Sub EditarPDF(Origem As String, caminhoArquivoDestino As String, novoNomeArquivo As String)
        Dim tentativas As Integer = 3 ' Número de tentativas em caso de erro
        Dim sucesso As Boolean = False
        Dim tentativasFeitas As Integer = 0

        ' Loop para tentar a operação várias vezes
        While tentativasFeitas < tentativas And Not sucesso
            Try
                ' Tenta abrir o arquivo com bloqueio exclusivo
                Using fs As New FileStream(Origem, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                    ' Arquivo pode ser manipulado, sem que outros processos possam acessá-lo simultaneamente
                    ' Se a execução chegar aqui, significa que o arquivo está livre para ser editado

                    ' Agora, abre o PDF de origem diretamente no caminho de destino
                    Using pdfReader As New PdfReader(Origem)
                        ' Inicializa o escritor para sobrescrever o arquivo no destino
                        Using pdfWriter As New PdfWriter(caminhoArquivoDestino)
                            ' Abre o documento PDF para edição
                            Using pdfDocument As New PdfDocument(pdfReader, pdfWriter)
                                ' Acessa a primeira página do PDF
                                Dim page As PdfPage = pdfDocument.GetPage(1)
                                ' Cria um Canvas para desenhar na página
                                Dim canvas As New PdfCanvas(page)

                                ' Configura a fonte e insere o texto
                                Dim font As PdfFont = PdfFontFactory.CreateFont()
                                canvas.BeginText()
                                canvas.SetFontAndSize(font, 12)
                                canvas.MoveText(50, 800) ' Posição do texto no PDF
                                canvas.ShowText(novoNomeArquivo & " - Data de Emissão do desenho: " & Date.Now.ToString("dd/MM/yyyy"))
                                canvas.EndText()

                                ' Fecha o documento, salvando as alterações
                                pdfDocument.Close()
                            End Using
                        End Using
                    End Using

                    ' Se chegou até aqui, a operação foi bem-sucedida
                    Console.WriteLine("Texto inserido com sucesso!")
                    sucesso = True ' Operação bem-sucedida

                End Using ' FileStream

            Catch ex As IOException
                ' Caso o arquivo esteja em uso, aguarda 3 segundos e tenta novamente
                tentativasFeitas += 1
                Console.WriteLine($"Erro ao manipular o arquivo PDF: {ex.Message}. Tentando novamente em 3 segundos...")
                File.Copy(Origem, caminhoArquivoDestino, True)
                ' Espera 3 segundos antes de tentar novamente
                '  Thread.Sleep(3000)

            Catch ex As Exception
                ' Tratar outras exceções
                Console.WriteLine($"Erro inesperado: {ex.Message}")
                tentativasFeitas += 1
                Thread.Sleep(3000)
            End Try
        End While

        ' Se o número máximo de tentativas for atingido e não tiver sido bem-sucedido
        If Not sucesso Then
            Console.WriteLine("Falha ao processar o arquivo após várias tentativas.")
        End If
    End Sub

    ' Função que verifica se o arquivo está em uso
    Private Function IsFileInUse(filePath As String) As Boolean
        Try
            ' Tenta abrir o arquivo com exclusividade
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                Return False ' O arquivo não está em uso
            End Using
        Catch ex As IOException
            ' O arquivo está em uso, retorna True
            Return True
        End Try
    End Function

    ' Função para tentar fechar o arquivo, liberando-o para uso
    Private Sub CloseFile(filePath As String)
        Try
            ' Tenta fechar o arquivo, liberando-o para uso
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                fs.Close() ' Fecha explicitamente o arquivo
            End Using
            Console.WriteLine("Arquivo fechado com sucesso.")
        Catch ex As Exception
            ' Caso haja falha ao tentar fechar o arquivo
            Console.WriteLine("Erro ao tentar fechar o arquivo: " & ex.Message)
        End Try
    End Sub


    Private Function ConsolidarMateriais(ByVal dgv As DataGridView) As System.Data.DataTable
        ' Cria um novo DataTable para armazenar os dados consolidados

        dtConsolidado.Columns.Clear()
        dtConsolidado.Rows.Clear()

        dtConsolidado.Columns.Add("IdMaterial")
        dtConsolidado.Columns.Add("QtdeTotal") ' Ou use Decimal se for um valor decimal
        dtConsolidado.Columns.Add("EnderecoArquivo") ' Adicione outras colunas conforme necessário
        dtConsolidado.Columns.Add("material")
        dtConsolidado.Columns.Add("espessura")
        dtConsolidado.Columns.Add("txtItemEstoque")

        ' Dicionário para armazenar a soma de QtdeTotal por IdMaterial
        Dim materialDictionary As New Dictionary(Of String, DataRow)()

        ' Itera pelas linhas do DataGridView
        For Each row As DataGridViewRow In dgv.Rows
            If Not row.IsNewRow Then
                Dim IdMaterial As String = row.Cells("IdMaterial").Value.ToString()
                Dim qtdeTotal As Double = Convert.ToInt32(row.Cells("QtdeTotal").Value)
                Dim EnderecoArquivo As String = row.Cells("EnderecoArquivo").Value.ToString()
                Dim material As String = row.Cells("material").Value.ToString()
                Dim espessura As String = row.Cells("espessura").Value.ToString()
                Dim txtItemEstoque As String = row.Cells("txtItemEstoque").Value.ToString()

                If materialDictionary.ContainsKey(IdMaterial) Then
                    ' Se o material já existe, soma a quantidade
                    materialDictionary(IdMaterial)("QtdeTotal") += qtdeTotal
                Else
                    ' Se não existe, cria uma nova linha no DataTable
                    Dim newRow As DataRow = dtConsolidado.NewRow()
                    newRow("IdMaterial") = IdMaterial
                    newRow("QtdeTotal") = qtdeTotal
                    newRow("EnderecoArquivo") = EnderecoArquivo ' Adicione outros campos conforme necessário
                    newRow("material") = material
                    newRow("espessura") = espessura
                    newRow("txtItemEstoque") = txtItemEstoque

                    dtConsolidado.Rows.Add(newRow)
                    materialDictionary.Add(IdMaterial, newRow)

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

                        cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Acabamento", cboOpcoesAcabamento.Text.ToUpper.Trim, "IDOrdemServicoItem", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoItem").Value.ToString)

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

            If OrdemServico.Liberado_Engenharia <> "" Then

                MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")

                Exit Sub
            Else

                Dim novaqtde As Double

                novaqtde = InputBox("Informe a Nova Quantidade", "Alteração de Quantidade", DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value.ToString)

                If IsNumeric(novaqtde) Then

                    Dim Peso, AreaPintura As String

                    Peso = (DGVListaMaterialSW.CurrentRow.Cells("Peso").Value / DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value) * novaqtde

                    AreaPintura = (DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Value / DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value) * novaqtde

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)
                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "qtde", novaqtde, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", AreaPintura, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Peso, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)

                    DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value = novaqtde
                    DGVListaMaterialSW.CurrentRow.Cells("qtde").Value = novaqtde

                    DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Value = AreaPintura

                    DGVListaMaterialSW.CurrentRow.Cells("Peso").Value = Peso

                    DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                    DGVListaMaterialSW.CurrentRow.Cells("qtde").Style.BackColor = Color.LightGreen

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


        Dim resultado As MsgBoxResult = MessageBox.Show("Deseja Atualizar os arquivos da OS, esta ação ira apagar todos os arquivo da pasta da Ordem de Serviço " & OrdemServico.IdOrdemServico, "Atualização", MessageBoxButtons.YesNo)

        If resultado = DialogResult.Yes Then


            ' Verifica e obtém o valor da célula "Liberado_Engenharia"
            If dgvos.CurrentRow.Cells("Liberado_Engenharia") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Liberado_Engenharia").Value IsNot DBNull.Value Then
                OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString()
            Else
                OrdemServico.Liberado_Engenharia = String.Empty ' Valor padrão em caso de ausência
            End If



            If OrdemServico.Liberado_Engenharia = "S" Then

                MsgBox("OS Já liberada, não e possivel liberar novamente!", vbInformation, "Atenção")

                Exit Sub

            Else


                Try

                    If DGVListaMaterialSW.Rows.Count > 0 Then

                        Dim diretorio As String = OrdemServico.EnderecoOrdemServico

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
IDOrdemServicoItem,
Projeto, Tag, IdMaterial,
CodMatFabricante, DescResumo, DescDetal,
UPPER(EnderecoArquivo) AS EnderecoArquivo
FROM  " & ComplementoTipoBanco & "ordemservicoitem
where (Estatus = 'A') AND (D_E_L_E_T_E <> '*' OR D_E_L_E_T_E IS NULL)
AND (ORDEMSERVICOITEMFINALIZADO = '' OR ORDEMSERVICOITEMFINALIZADO IS NULL)
And  CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

        lblNUmeroDocumentoAtivo.Text = DadosArquivoCorrente.NomeArquivoSemExtensao


        'edson 20-01-2025
        'para verificar a necessidade processamento na abertura de cada arquivo
        'For Each col As DataGridViewColumn In DGVTimerFiltroPecaAtivaOS.Columns
        '    If col.Width > 350 Then
        '        col.Width = 351
        '    End If
        'Next

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

            DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value = True

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

                PadraoMetta.ExportarOrdemServicoPadraoMettaAntigo(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, True, DGVListaMaterialSWMaterial)
            Else

                TemplatesExcel.ExportarOrdemServicoPadrao(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, DGVListaMaterialSWMaterial)

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

        If OrdemServico.IdOrdemServico = Nothing Then

            MsgBox("Não há Ordem de Serviço selecionada", vbCritical, "Atenção")
        Else

            If dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString <> "S" Then

                OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString

                Dim resultado As MsgBoxResult = MessageBox.Show("Deseja limpar a Ordem de Serviço, esta ação ira apagar todos os arquivo da pasta da Ordem de Serviço " & OrdemServico.IdOrdemServico, "Exclusão", MessageBoxButtons.YesNo)

                If resultado = DialogResult.Yes Then

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)

                    Dim diretorio As String = OrdemServico.EnderecoOrdemServico

                    LimparDiretorio(diretorio)

                    Me.lblOrdemServicoAtiva.Text = ""

                    TimerDGVListaMaterialSW.Enabled = True
                    TimerFiltroPecaAtivaOS.Enabled = True
                Else

                    MsgBox("Operação Cancelada", vbCritical, "Atenção")

                End If
            Else

                MsgBox("Esta operação não e valida a OS: " & OrdemServico.EnderecoOrdemServico & ", já foi liberada anteriormente!", vbInformation, "Atenção")

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

            ' Limpa todos os subdiretórios, exceto aqueles chamados "Projeto"
            For Each subdiretorio As String In Directory.GetDirectories(diretorio)
                ' Verifica se o nome do subdiretório é "Projeto"
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


        If Usuario.NomeCompleto <> dgvos.CurrentRow.Cells("CriadoPor").Value.ToString() Then

            Dim resultUsuario As DialogResult = MessageBox.Show("O Usuario: " & dgvos.CurrentRow.Cells("CriadoPor").Value.ToString() &
                 " foi o Criador desta OS, mesmo assim você gostaria de Excluir a OS Selecionada", "Exclusão Ordem de Serviço", MessageBoxButtons.YesNo)

            If resultUsuario = DialogResult.No Then

                Exit Sub

            End If

        End If
        If OrdemServico.IdOrdemServico.ToString = Nothing Or OrdemServico.IdOrdemServico.ToString = "" Then

            MsgBox("Não há OS Selecionada!", vbCritical, "Atenção")

            Exit Sub

        End If

        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Excluir/Cancelar a Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Cancelando Ordem de Serviço", MessageBoxButtons.YesNo)

        Dim totalExecutado As Integer

        Try

            totalExecutado = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT  count(idplanodecorte) + 
                   count(CorteTotalExecutado) + count(DobraTotalExecutado)+ count(SoldaTotalExecutado) +
                   count(PinturaTotalExecutado) +  count(MontagemTotalExecutado) as totalExecutado  
                   FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  ='" & OrdemServico.IdOrdemServico & "' 
                   and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');", "totalExecutado"))

        Catch ex As Exception

            totalExecutado = 0

        Finally

        End Try

        If result = DialogResult.Yes Then

            If totalExecutado > 0 Then

                Dim dtTabelaPlanoCorte As New System.Data.DataTable()

                dtTabelaPlanoCorte = cl_BancoDados.CarregarDados("SELECT  idplanodecorte, CodMatFabricante  
                     FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  = '" & OrdemServico.IdOrdemServico & "' and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');")

                Dim MessagemItens As String

                For I As Integer = 0 To dtTabelaPlanoCorte.Rows.Count - 1

                    MessagemItens = MessagemItens & "PlanoCorte = " & dtTabelaPlanoCorte.Rows(I).Item("idplanodecorte").ToString &
                    " Numero Desenho: = " & dtTabelaPlanoCorte.Rows(I).Item("CodMatFabricante").ToString & vbCrLf

                Next

                MsgBox("A OS Numero: " & OrdemServico.IdOrdemServico & " contem processos em andamento, por este motivo não pode ser cancelada, ver plano de corte's: " & vbCrLf & MessagemItens, vbCritical, "Atenção!")

                Exit Sub

            End If

            cl_BancoDados.AlteracaoEspecifica("ordemservico", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservico", "UsuarioD_E_L_E_T_E", Usuario.NomeCompleto, "IdOrdemServico", OrdemServico.IdOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservico", "DataD_E_L_E_T_E", Date.Now.Date, "IdOrdemServico", OrdemServico.IdOrdemServico)

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "UsuarioD_E_L_E_T_E", Usuario.NomeCompleto, "IdOrdemServico", OrdemServico.IdOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "DataD_E_L_E_T_E", Date.Now.Date, "IdOrdemServico", OrdemServico.IdOrdemServico)

            dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.Sem_Incone

            Timerdgvos.Enabled = True
            TimerDGVListaMaterialSW.Enabled = True
            TimerFiltroPecaAtivaOS.Enabled = True

        ElseIf result = DialogResult.No Then

            MsgBox("Operação Cancelada", vbCritical, "Atenção")

        End If

    End Sub

    Private Sub dgvDesenhos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDesenhos.DataBindingComplete

        'Try

        '    For I As Integer = 0 To dgvDesenhos.Rows.Count - 1

        '        If dgvDesenhos.Rows(I).Cells("RNC").Value.ToString = "S" Then

        '            dgvDesenhos.Rows(I).Cells("Dgvrnc").Value = My.Resources.atencao

        '        Else

        '            dgvDesenhos.Rows(I).Cells("Dgvrnc").Value = My.Resources.verificado1

        '        End If

        '        ' Verifica se a string ".SLDASM" está contida na célula (ignora maiúsculas/minúsculas)
        '        If dgvDesenhos.Rows(I).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
        '            ' Define a imagem na coluna "dgvIcone"
        '            dgvDesenhos.Rows(I).Cells("dgvIcone").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone

        '        ElseIf dgvDesenhos.Rows(I).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then

        '            dgvDesenhos.Rows(I).Cells("dgvIcone").Value = My.Resources.IcopneMontagemPRT



        '        End If

        '    Next

        'Catch ex As Exception

        'Finally

        'End Try

        Try
            ' Verifica cada linha da DataGridView
            For Each row As DataGridViewRow In dgvDesenhos.Rows
                ' Verifica se a célula "RNC" contém valor e trata conforme a lógica
                Dim rncValue As String = row.Cells("RNC")?.Value?.ToString()

                ' Define o ícone correspondente com base no valor
                If String.IsNullOrEmpty(rncValue) OrElse Not rncValue.Equals("S", StringComparison.OrdinalIgnoreCase) Then
                    ' Se o valor for nulo, vazio ou diferente de "S", define como "verificado1"
                    row.Cells("Dgvrnc").Value = My.Resources.verificado1
                Else
                    ' Caso contrário, define como "atencao"
                    row.Cells("Dgvrnc").Value = My.Resources.atencao
                End If

                ' Verifica se a célula "EnderecoArquivo" contém um valor válido
                Dim enderecoArquivo As String = row.Cells("EnderecoArquivo")?.Value?.ToString()
                If Not String.IsNullOrEmpty(enderecoArquivo) Then
                    ' Define o ícone apropriado com base no tipo de arquivo
                    If enderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        row.Cells("dgvIcone").Value = My.Resources.IcopneMontagemSW
                    ElseIf enderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        row.Cells("dgvIcone").Value = My.Resources.IcopneMontagemPRT
                    End If
                End If
            Next
        Catch ex As Exception
            ' Exibe mensagem de erro em caso de exceção
            MessageBox.Show($"Ocorreu um erro ao atualizar os desenhos: {ex.Message}",
                            "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnAssociarMaterial_Click(sender As Object, e As EventArgs)

        If String.IsNullOrWhiteSpace(DadosArquivoCorrente.NomeArquivoSemExtensao) Then

            MessageBox.Show("Não há desenho ativo para associar material.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Exit Sub

        Else
            Using formMateriaisAlmoxarifado As New frmMateriaisAlmoxarifado ' MateriaisAlmoxarifado

                DadosArquivoCorrente.IdMaterial = cl_BancoDados.RetornaCampoDaPesquisa("Select IdMaterial from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "IdMaterial")

                formMateriaisAlmoxarifado.ShowDialog()

                TimerMontaPeca.Enabled = True

            End Using

        End If

    End Sub

    Private Sub ExcluirOMaterialDoDesenhoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcluirOMaterialDoDesenhoToolStripMenuItem.Click

        Try

            If MsgBox("Tem certeza que deseja desabilitar o material do Desenho Selecionado?", MsgBoxStyle.YesNo, "Confirmar desabilitação!") = MsgBoxResult.No Then
                ' MsgBox("Desenho não desabilitado!")
                Exit Sub
            Else

                Try
                    ' IdMontaPeca = DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value

                    cl_BancoDados.Salvar("Delete  from  " & ComplementoTipoBanco & "montapeca where IdMontaPeca = '" & IdMontaPeca & "'")

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

                    DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, True)

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

            cl_BancoDados.ComboBoxDataSet("projetos", "idProjeto", "Projeto", cboProjeto, " WHERE Liberado = 'S'")
            cl_BancoDados.ComboBoxDataSet("Acabamento", "idAcabamento", "DescAcabamento", cboOpcoesAcabamento, "")
            '  cl_BancoDados.ComboBoxDataSet("Acabamento", "idAcabamento", "DescAcabamento", cboAcabamentoArvore, "")

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

            If OrdemServico.Liberado_Engenharia <> "" Then

                MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")

                Exit Sub

            Else

                Dim FatorMultiplicador As Double

                FatorMultiplicador = InputBox("Informe o novo valor Multiplicador", "Alteração de Quantidade", 1)

                If IsNumeric(FatorMultiplicador) And FatorMultiplicador > 0 Then

                    For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                        Dim Peso, AreaPintura, qtde, QtdeTotal, Fator As String

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
                            AreaPintura = DGVListaMaterialSW.Rows(i).Cells("AreaPinturaUnitario").Value
                        Catch ex As Exception
                            AreaPintura = DGVListaMaterialSW.Rows(i).Cells("AreaPintura").Value / qtde
                        End Try


                        Try
                            Peso = DGVListaMaterialSW.Rows(i).Cells("PesoUnitario").Value
                        Catch ex As Exception
                            Peso = DGVListaMaterialSW.Rows(i).Cells("Peso").Value / qtde
                        End Try


                        OrdemServico.IDOrdemServicoItem = DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoItem").Value.ToString


                        Peso = Peso * FatorMultiplicador

                        AreaPintura = AreaPintura * FatorMultiplicador

                        QtdeTotal = qtde * FatorMultiplicador

                        ' cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", QtdeTotal, "IDOrdemServicoItem", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoItem").Value.ToString)
                        ' cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", AreaPintura, "IDOrdemServicoItem", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoItem").Value.ToString)
                        ' cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Peso, "IDOrdemServicoItem", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoItem").Value.ToString)

                        cl_BancoDados.AlteracaoEspecificaDadosOS(OrdemServico.IDOrdemServicoItem, QtdeTotal, AreaPintura, Peso)

                        DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Value = QtdeTotal
                        'DGVListaMaterialSW.CurrentRow.Cells("qtde").Value = novaqtde

                        DGVListaMaterialSW.Rows(i).Cells("AreaPintura").Value = AreaPintura

                        DGVListaMaterialSW.Rows(i).Cells("Peso").Value = Peso

                        DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Value = QtdeTotal

                        DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                        DGVListaMaterialSW.Rows(i).Cells("AreaPintura").Style.BackColor = Color.LightGreen

                        DGVListaMaterialSW.Rows(i).Cells("Peso").Style.BackColor = Color.LightGreen

                    Next

                    Dim diretorio As String = OrdemServico.EnderecoOrdemServico

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

            If OrdemServico.IdOrdemServico = 0 Or OrdemServico.IdOrdemServico.ToString = "" Or OrdemServico.IdOrdemServico = Nothing Then
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

                OrdemServico.Projeto = Me.cboProjeto.Text.ToUpper
                OrdemServico.Tag = Me.cboTag.Text.ToUpper
                OrdemServico.Descricao = Me.txtDescricao.Text.ToUpper & "Esta e um OS copia da OS de referencia Numero: " & OrdemServico.IdOrdemServico
                OrdemServico.CriadoPor = Usuario.NomeCompleto
                OrdemServico.DataCriacao = Date.Now.Date
                OrdemServico.Estatus = "A".ToUpper
                OrdemServico.idProjeto = cboProjeto.SelectedValue
                OrdemServico.idTag = cboTag.SelectedValue
                OrdemServico.DescEmpresa = txtCliente.Text


                Dim idosRetono As String


                Try
                    NovoIdOrdemServicoDB = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT max(IdOrdemServico)  as NovoIdOrdemServico FROM  " & ComplementoTipoBanco & "ordemservico", "NovoIdOrdemServico")) + 1

                    NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
                Catch ex As Exception

                    ' Em caso de erro, atribuir "00001" como valor inicial
                    NovoIdOrdemServico = "00001"

                End Try

                OrdemServico.EnderecoOrdemServico = Replace((My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico).ToString.ToUpper, "\", "##")

                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico)
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DXF")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PDF")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DFT")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PUNC")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LASER")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\Projeto")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PEÇAS DE ESTOQUE")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LXDS")



                cl_BancoDados.Salvar("insert into ordemservico(idProjeto,
Projeto,
idTag,
Tag,
Descricao,
EnderecoOrdemServico,
CriadoPor,
DataCriacao,
Estatus,
D_E_L_E_T_E,
DescEmpresa) values
('" & OrdemServico.idProjeto & "','" _
    & OrdemServico.Projeto & "','" _
    & OrdemServico.idTag & "','" _
    & OrdemServico.Tag & "','" _
    & OrdemServico.Descricao & "','" _
    & OrdemServico.EnderecoOrdemServico & "','" _
    & OrdemServico.CriadoPor.ToString().ToUpper() & "','" _
    & OrdemServico.DataCriacao & "','" _
    & OrdemServico.Estatus & "','','" _
    & OrdemServico.DescEmpresa & "')")


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
                        OrdemServico.MaterialSW = DGVListaMaterialSW.Rows(A).Cells("MaterialSW").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.MaterialSW = ""

                    End Try

                    Try
                        OrdemServico.qtde = DGVListaMaterialSW.Rows(A).Cells("qtde").Value.ToString
                    Catch ex As Exception

                        OrdemServico.qtde = 0

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
                        'OrdemServico.QtdeTotal = OrdemServico.qtde * Fator
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

                    OrdemServico.IdOrdemServico = NovoIdOrdemServicoDB

                    ProgressBarProcessoLiberacaoOrdemServico.Value = A

                    Dim query As String = "INSERT INTO ordemservicoitem (
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque,OrdemServicoItemFinalizado
                           ) VALUES (
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque,@OrdemServicoItemFinalizado
                           );"

                    Using command As New MySqlCommand(query, myconect)
                        ' Adicionando os parâmetros
                        command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                        command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                        command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                        command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                        command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                        command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                        command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                        command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                        command.Parameters.AddWithValue("@QtdeProduzida", "")
                        command.Parameters.AddWithValue("@QtdeFaltante", "")
                        command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                        command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                        command.Parameters.AddWithValue("@Estatus", "A")
                        command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                        command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                        command.Parameters.AddWithValue("@fator", fator)
                        command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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
                        command.Parameters.AddWithValue("@OrdemServicoItemFinalizado", "")

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

                If NovoIdOrdemServicoDB = dgvos.Rows(i).Cells("IdOrdemServico").Value Then

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

                                Dim Peso, AreaPintura, fator, IDOrdemServicoItem As String

                                IDOrdemServicoItem = DGVListaMaterialSW.Rows(b).Cells("IDOrdemServicoItem").Value.ToString

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

                                    AreaPintura = DadosArquivoCorrente.AreaPintura
                                    AreaPintura = AreaPintura * novaqtde
                                    ' AreaPintura = Replace(AreaPintura, ",", ".")
                                Catch ex As Exception
                                    AreaPintura = 0
                                End Try

                                'AreaPintura = (DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitaria").Value * novaqtde)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "qtde", novaqtde, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", Replace((AreaPintura), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Replace((Peso), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Fator", fator, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "PesoUnitario", Replace((DadosArquivoCorrente.Massa), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPinturaUnitario", Replace((DadosArquivoCorrente.AreaPintura), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)


                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("qtde").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Value = Replace((AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Value = Replace((Peso), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Value = Replace((DadosArquivoCorrente.AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Value = Replace((DadosArquivoCorrente.Massa), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("Fator").Value = fator

                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("qtde").Style.BackColor = Color.LightGreen

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
                                OrdemServico.MaterialSW = DadosArquivoCorrente.material.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.MaterialSW = ""
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
                                OrdemServico.qtde = novaqtde
                            Catch ex As Exception

                                OrdemServico.qtde = novaqtde

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
                                OrdemServico.txtAcabamento = DadosArquivoCorrente.Acabamento
                            Catch ex As Exception

                                OrdemServico.txtAcabamento = ""

                            End Try


                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                            OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                            ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                            Dim query As String = "INSERT INTO ordemservicoitem (
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                            Using command As New MySqlCommand(query, myconect)
                                ' Adicionando os parâmetros
                                command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                                command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                command.Parameters.AddWithValue("@QtdeProduzida", "")
                                command.Parameters.AddWithValue("@QtdeFaltante", "")
                                command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                                command.Parameters.AddWithValue("@Estatus", "A")
                                command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                command.Parameters.AddWithValue("@fator", 1)
                                command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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
                            OrdemServico.MaterialSW = DadosArquivoCorrente.material.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.MaterialSW = ""
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
                            OrdemServico.qtde = novaqtde
                        Catch ex As Exception

                            OrdemServico.qtde = novaqtde

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
                            OrdemServico.txtAcabamento = DadosArquivoCorrente.Acabamento
                        Catch ex As Exception

                            OrdemServico.txtAcabamento = ""

                        End Try


                        OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                        OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                        ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                        Dim query As String = "INSERT INTO ordemservicoitem (
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                        Using command As New MySqlCommand(query, myconect)
                            ' Adicionando os parâmetros
                            command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                            command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                            command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                            command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                            command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                            command.Parameters.AddWithValue("@QtdeProduzida", "")
                            command.Parameters.AddWithValue("@QtdeFaltante", "")
                            command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                            command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                            command.Parameters.AddWithValue("@Estatus", "A")
                            command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                            command.Parameters.AddWithValue("@fator", 1)
                            command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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

        If OrdemServico.IdOrdemServico.ToString = Nothing Or OrdemServico.IdOrdemServico.ToString = "" Then

            MsgBox("Não há OS Selecionada!", vbCritical, "Atenção")
            Exit Sub
        End If
        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Cancelar a Liberação da Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Cancelando Ordem de Serviço", MessageBoxButtons.YesNo)
        Dim totalExecutado As Integer
        Try

            totalExecutado = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT  count(idplanodecorte) + 
                   count(CorteTotalExecutado) + count(DobraTotalExecutado)+ count(SoldaTotalExecutado) +
                   count(PinturaTotalExecutado) +  count(MontagemTotalExecutado) as totalExecutado  
                   FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  ='" & OrdemServico.IdOrdemServico & "' 
                   and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');", "totalExecutado"))

        Catch ex As Exception

            totalExecutado = 0

        Finally

        End Try

        If result = DialogResult.Yes Then

            If totalExecutado > 0 Then

                Dim dtTabelaPlanoCorte As New System.Data.DataTable()

                dtTabelaPlanoCorte = cl_BancoDados.CarregarDados("SELECT  idplanodecorte, CodMatFabricante  
                     FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  = '" & OrdemServico.IdOrdemServico & "' and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');")

                Dim MessagemItens As String

                For I As Integer = 0 To dtTabelaPlanoCorte.Rows.Count - 1

                    MessagemItens = MessagemItens & "PlanoCorte = " & dtTabelaPlanoCorte.Rows(I).Item("idplanodecorte").ToString &
                    " Numero Desenho: = " & dtTabelaPlanoCorte.Rows(I).Item("CodMatFabricante").ToString & vbCrLf

                Next

                MsgBox("A OS Numero: " & OrdemServico.IdOrdemServico & " contem processos em andamento, por este motivo não pode ser cancelada, ver plano de corte's: " & vbCrLf & MessagemItens, vbCritical, "Atenção!")

                Exit Sub

            End If


            OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString

            '       cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)

            Dim diretorio As String = OrdemServico.EnderecoOrdemServico

            LimparDiretorio(diretorio & "\PDF")
            LimparDiretorio(diretorio & "\DXF")
            LimparDiretorio(diretorio & "\DFT")
            LimparDiretorio(diretorio & "\LXDS")

            cl_BancoDados.Salvar("Update ordemservico set Liberado_Engenharia = '', 
                        Data_Liberacao_Engenharia = '' 
                        where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")

            cl_BancoDados.Salvar("Update ordemservicoitem set Liberado_Engenharia = '', 
                        Data_Liberacao_Engenharia = '' 
                        where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")

            dgvos.CurrentRow.Cells("Liberado_Engenharia").Value = ""
            dgvos.CurrentRow.Cells("Data_Liberacao_Engenharia").Value = ""
            dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.atencao
            dgvos.Refresh()

            If Usuario.EnviarEmailLiberacaoOS <> "" Then

                Dim resultado As MsgBoxResult = MessageBox.Show("Deseja enviar o e-mail para o PCP, de comunicado o cancelamento da Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Cancelamento", MessageBoxButtons.YesNo)

                If resultado = DialogResult.Yes Then

                    ClasseEmail.EmailCancelamentoOS()

                End If

            End If

            TimerFiltroPecaAtivaOS.Enabled = True

        Else

            MsgBox("Esta opração não e valida para OS: " & OrdemServico.IdOrdemServico & ", há processo ja executados, a opção será o cancelamento!", vbCritical, "Atenção")

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

        AtualizarIcones()

    End Sub


    Private Sub AtualizarIcones()

        If DGVListaMaterialSW.Rows.Count > 0 Then

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
                ' Verificar "rnc" e definir o ícone correspondente

                If valorEnderecoArquivo.IndexOf(".LXDS", StringComparison.OrdinalIgnoreCase) >= 0 Then

                    row.Cells("DGVIconeLXDS").Value = My.Resources.CYPCUT  ' Substitua pelo seu ícone
                Else
                    row.Cells("DGVIconeLXDS").Value = My.Resources.Sem_Incone
                End If


            Next

        End If


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

            DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, True)


            AtualizaTela(swModel)

        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub tsbSalvar_Click(sender As Object, e As EventArgs) Handles tsbSalvar.Click

        If DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, True) = True Then

            DadosArquivoCorrente.AtualizaDesenho(swModel)

            'If swModel Is Nothing Then

            '    Exit Sub

            'End If

            '    swModel.GraphicsRedraw2()

            ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
            '  swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)



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
                        dgvDataGridBOM.Rows(i).Cells("material").Value = Me.lblMaterial.Text ' UCase(DadosArquivoCorrente.material)
                        dgvDataGridBOM.Rows(i).Cells("Acabamento").Value = UCase(DadosArquivoCorrente.Acabamento)
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

                        dgvDataGridBOM.Rows(i).Cells("RNC").Value = ""

                        dgvDataGridBOM.Rows(i).Cells("Dgvrnc").Value = My.Resources.verificado1

                        ' dgvDataGridBOM.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                        Exit Sub

                    End If

                Next

            End If

        Else

            ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
            swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)


        End If



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

                MsgBox("O arquivo foi exportado com sucesso", vbInformation, "Conversão em DXF concluida com sucesso")

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


                    MsgBox("O arquivo foi exportado com sucesso", vbInformation, "Conversão em PDF concluida com sucesso")


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

                DadosArquivoCorrente.IdMaterial = cl_BancoDados.RetornaCampoDaPesquisa("Select IdMaterial from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "IdMaterial")

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

                                Dim Peso, AreaPintura, fator, IDOrdemServicoItem As String

                                IDOrdemServicoItem = DGVListaMaterialSW.Rows(b).Cells("IDOrdemServicoItem").Value.ToString

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

                                    AreaPintura = DadosArquivoCorrente.AreaPintura
                                    AreaPintura = AreaPintura * novaqtde
                                    ' AreaPintura = Replace(AreaPintura, ",", ".")
                                Catch ex As Exception
                                    AreaPintura = 0
                                End Try

                                'AreaPintura = (DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitaria").Value * novaqtde)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "qtde", novaqtde, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", Replace((AreaPintura), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Replace((Peso), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Fator", fator, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "PesoUnitario", Replace((DadosArquivoCorrente.Massa), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPinturaUnitario", Replace((DadosArquivoCorrente.AreaPintura), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)


                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("qtde").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Value = Replace((AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Value = Replace((Peso), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Value = Replace((DadosArquivoCorrente.AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Value = Replace((DadosArquivoCorrente.Massa), ",", ".")


                                DGVListaMaterialSW.Rows(b).Cells("Fator").Value = fator

                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("qtde").Style.BackColor = Color.LightGreen

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
                                OrdemServico.MaterialSW = DadosArquivoCorrente.material.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.MaterialSW = ""
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
                                OrdemServico.qtde = novaqtde
                            Catch ex As Exception

                                OrdemServico.qtde = novaqtde

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
                                OrdemServico.txtAcabamento = DadosArquivoCorrente.Acabamento
                            Catch ex As Exception

                                OrdemServico.txtAcabamento = ""

                            End Try


                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                            OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                            ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                            Dim query As String = "INSERT INTO ordemservicoitem (
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                            Using command As New MySqlCommand(query, myconect)
                                ' Adicionando os parâmetros
                                command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                                command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                command.Parameters.AddWithValue("@QtdeProduzida", "")
                                command.Parameters.AddWithValue("@QtdeFaltante", "")
                                command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                                command.Parameters.AddWithValue("@Estatus", "A")
                                command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                command.Parameters.AddWithValue("@fator", 1)
                                command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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
                            OrdemServico.MaterialSW = DadosArquivoCorrente.material.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.MaterialSW = ""
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
                            OrdemServico.qtde = novaqtde
                        Catch ex As Exception

                            OrdemServico.qtde = novaqtde

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
                            OrdemServico.txtAcabamento = DadosArquivoCorrente.Acabamento
                        Catch ex As Exception

                            OrdemServico.txtAcabamento = ""

                        End Try


                        OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                        OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                        ''''''''''''''''''''  ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)



                        Dim query As String = "INSERT INTO ordemservicoitem (
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
                            txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, 
                            txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda, 
                            tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora, 
                            Larguracaixadelimitadora, Espessuracaixadelimitadora, 
                            AreaPinturaUnitario, PesoUnitario, txtItemEstoque
                           ) VALUES (
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
                            @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, 
                            @txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda, 
                            @tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora, 
                            @Larguracaixadelimitadora, @Espessuracaixadelimitadora, 
                            @AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
                           );"

                        Using command As New MySqlCommand(query, myconect)
                            ' Adicionando os parâmetros
                            command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                            command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                            command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                            command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                            command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                            command.Parameters.AddWithValue("@QtdeProduzida", "")
                            command.Parameters.AddWithValue("@QtdeFaltante", "")
                            command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                            command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                            command.Parameters.AddWithValue("@Estatus", "A")
                            command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                            command.Parameters.AddWithValue("@fator", 1)
                            command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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

        If File.Exists(My.Settings.EnderecoNovoFormatoA4) = False Then

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


        If File.Exists(My.Settings.EnderecoNovoFormatoA4Deitado) = False Then

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

        OrdemServico.IdOrdemServico = Nothing
        OrdemServico.Projeto = Nothing
        OrdemServico.Tag = Nothing
        OrdemServico.Descricao = Nothing
        OrdemServico.Estatus = Nothing
        OrdemServico.idTag = Nothing
        OrdemServico.idProjeto = Nothing
        OrdemServico.DescEmpresa = Nothing
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
            OrdemServico.Descricao = Me.txtDescricao.Text
            OrdemServico.idTag = cboTag.SelectedValue
            OrdemServico.idProjeto = cboProjeto.SelectedValue
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

        AtualziarDadosSinco()

    End Sub



    Private Sub AtualziarDadosSinco()
        Try

            If TipoBanco = "SQL" Then

                cl_BancoDados.ComboBoxDataSet("[View_SZ1010_GESTAO]", "Z1_NUM", "Z1_NUM", cboProjeto, "", "[MP12OFICIAL].[dbo].")

                cl_BancoDados.ComboBoxDataSet("tratamento", "Id_Tratamento", "Tratamento", cboOpcoesAcabamento, "", "[MP12OFICIAL].[dbo].")

                ' Chama a função para carregar os dados no CheckedListBox
                PreencherCheckedListBox("Select DescFamilia from " & ComplementoTipoBanco & "familia WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescFamilia", chkBoxTipoDesenho)

                ' Chama a função para carregar os dados no CheckedListBox
                PreencherCheckedListBox("Select DescAcabamento from " & ComplementoTipoBanco & "acabamento WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescAcabamento ", chkBoxAcabamento)


            ElseIf TipoBanco = "MYSQL" Then

                cl_BancoDados.ComboBoxDataSet("projetos", "idProjeto", "Projeto", cboProjeto, " WHERE (D_E_L_E_T_E Is NULL Or D_E_L_E_T_E = '')  AND (liberado = 'S')  and (Finalizado = '' OR Finalizado Is NULL)")
                cl_BancoDados.ComboBoxDataSet("acabamento", "IdAcabamento", "DescAcabamento", cboOpcoesAcabamento, "WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')")
                ' Chama a função para carregar os dados no CheckedListBox
                PreencherCheckedListBox("Select DescFamilia from " & ComplementoTipoBanco & "familia WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescFamilia", chkBoxTipoDesenho)

                ' Chama a função para carregar os dados no CheckedListBox
                PreencherCheckedListBox("Select DescAcabamento from " & ComplementoTipoBanco & "acabamento WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescAcabamento ", chkBoxAcabamento)

            End If


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

        Cursor.Current = Cursors.WaitCursor

        Try


            TabelaViewMontaPeca = cl_BancoDados.CarregarDados("SELECT * FROM  " & ComplementoTipoBanco & "viewmontapeca where D_E_L_E_T_E <> '' OR D_E_L_E_T_E IS NULL")

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


                If DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, False) = False Then

                    DadosArquivoCorrente.rnc = "S"

                End If

                FormatarColunaIconeDGVListaBom()




                ' Preencher o DataGridView com os dados da peça
                dgvDataGridBOM.Rows.Add(My.Resources.Sem_Incone,
                                        iconeDXF,
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
                                    DadosArquivoCorrente.material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.Acabamento,
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


        For I As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

            Dim valorEnderecoArquivo As String = dgvDataGridBOM.Rows(I).Cells("EnderecoArquivo").Value.ToString
            valorEnderecoArquivo = Replace(valorEnderecoArquivo, ".SLDPRT", ".LXDS")
            valorEnderecoArquivo = Replace(valorEnderecoArquivo, ".SLDASM", ".LXDS")

            If File.Exists(valorEnderecoArquivo) Then

                dgvDataGridBOM.Rows(I).Cells("DGVIconeLXDS").Value = My.Resources.CYPCUT

            Else

                dgvDataGridBOM.Rows(I).Cells("DGVIconeLXDS").Value = My.Resources.Sem_Incone

            End If

        Next


        ' Retornar o cursor ao normal
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click

        Cursor.Current = Cursors.WaitCursor

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

                        '     DadosArquivoCorrente.AtualizaDesenho(swModel)

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

                        MsgBox(ex.Message)

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

        TimerdgvDesenhos.Enabled = True

        ' Retornar o cursor ao normal
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click

        Cursor.Current = Cursors.WaitCursor

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
                        rnc = dgvDataGridBOM.Rows(b).Cells("RNC").Value.ToString
                    Catch ex As Exception
                        rnc = ""
                    End Try
                    If rnc = "S" Then

                        MsgBox("Na lista, há peças com RNC pendente; para prosseguir com o processo de liberação, é necessário remover a peça da lista ou resolver a RNC.", vbCritical, "Atenção")

                        rnc = dgvDataGridBOM.Rows(b).DefaultCellStyle.BackColor = Color.LightSalmon

                        verificaRnc = True

                        Exit For

                    End If

                Next

            End If

            If verificaRnc = False Then

                If dgvDataGridBOM.Rows.Count > 0 Then

                    If OrdemServico.IdOrdemServico = Nothing Or OrdemServico.IdOrdemServico = 0 Then

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

                                    Catch ex As Exception

                                        OrdemServico.EnderecoArquivo = ""

                                    End Try


                                    Try
                                        OrdemServico.CodMatFabricante = dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString.ToUpper
                                    Catch ex As Exception
                                        OrdemServico.CodMatFabricante = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.DescResumo = dgvDataGridBOM.Rows(A).Cells("DescResumo").Value.ToString.ToUpper
                                    Catch ex As Exception
                                        OrdemServico.DescResumo = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("DescResumo").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.DescDetal = dgvDataGridBOM.Rows(A).Cells("DescDetal").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.DescDetal = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("DescDetal").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.Autor = dgvDataGridBOM.Rows(A).Cells("Autor").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Autor = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Autor").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.Palavrachave = dgvDataGridBOM.Rows(A).Cells("Palavrachave").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Palavrachave = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Palavrachave").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.Notas = dgvDataGridBOM.Rows(A).Cells("Notas").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.Notas = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Notas").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.Espessura = dgvDataGridBOM.Rows(A).Cells("Espessura").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.Espessura = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Espessura").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.NumeroDobras = dgvDataGridBOM.Rows(A).Cells("NumeroDobras").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.NumeroDobras = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("NumeroDobras").Value.ToString.ToUpper)

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
                                        'MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Altura").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.Largura = Replace(dgvDataGridBOM.Rows(A).Cells("Largura").Value.ToString, ",", "")
                                    Catch ex As Exception

                                        OrdemServico.Largura = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Largura").Value.ToString.ToUpper)

                                    End Try

                                    OrdemServico.DtCad = ""
                                    OrdemServico.UsuarioCriacao = ""
                                    OrdemServico.UsuarioAlteracao = ""
                                    OrdemServico.DtAlteracao = ""

                                    Try
                                        OrdemServico.MaterialSW = dgvDataGridBOM.Rows(A).Cells("material").Value.ToString.ToUpper
                                    Catch ex As Exception

                                        OrdemServico.MaterialSW = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("material").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.qtde = dgvDataGridBOM.Rows(A).Cells("qtde").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.qtde = 0
                                        'MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("qtde").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.AreaPintura = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                        OrdemServico.AreaPintura = OrdemServico.AreaPintura * OrdemServico.qtde * Fator
                                        OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ",", ".")
                                    Catch ex As Exception

                                        OrdemServico.AreaPintura = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.AreaPinturaUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                        OrdemServico.AreaPinturaUnitario = Replace(OrdemServico.AreaPinturaUnitario, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.AreaPinturaUnitario = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.Peso = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")

                                        OrdemServico.Peso = OrdemServico.Peso * OrdemServico.qtde * Fator
                                        OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.Peso = 0
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.PesoUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")
                                        OrdemServico.PesoUnitario = Replace(OrdemServico.PesoUnitario, ",", ".")
                                    Catch ex As Exception

                                        OrdemServico.PesoUnitario = 0
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.txtSoldagem = dgvDataGridBOM.Rows(A).Cells("txtSoldagem").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtSoldagem = ""
                                        ' MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("txtSoldagem").Value.ToString.ToUpper)
                                    End Try

                                    Try
                                        OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ".", ",")
                                        OrdemServico.QtdeTotal = OrdemServico.qtde * Fator
                                        OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ",", ".")

                                    Catch ex As Exception

                                        OrdemServico.QtdeTotal = 0

                                        '  MsgBox(ex.Message & ": qtdetotal")

                                    End Try

                                    Try
                                        OrdemServico.txtTipoDesenho = dgvDataGridBOM.Rows(A).Cells("txtTipoDesenho").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtTipoDesenho = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("txtTipoDesenho").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.txtCorte = dgvDataGridBOM.Rows(A).Cells("txtCorte").Value.ToString

                                    Catch ex As Exception

                                        OrdemServico.txtCorte = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("txtCorte").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.txtDobra = dgvDataGridBOM.Rows(A).Cells("txtDobra").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtDobra = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("txtDobra").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.txtSolda = dgvDataGridBOM.Rows(A).Cells("txtSolda").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtSolda = ""
                                        '   MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("txtSolda").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.txtPintura = dgvDataGridBOM.Rows(A).Cells("txtPintura").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtPintura = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("txtPintura").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.txtMontagem = dgvDataGridBOM.Rows(A).Cells("txtMontagem").Value.ToString
                                    Catch ex As Exception

                                        OrdemServico.txtMontagem = ""
                                        '  MsgBox(ex.Message & ": " & dgvDataGridBOM.Rows(A).Cells("txtMontagem").Value.ToString.ToUpper)

                                    End Try

                                    Try
                                        OrdemServico.Comprimentocaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Comprimentocaixadelimitadora").Value.ToString
                                        '  OrdemServico.Comprimentocaixadelimitadora = Replace(OrdemServico.Comprimentocaixadelimitadora, ",", "")
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

                                    If TipoBanco = "MYSQL" Then

                                        Dim query As String = "INSERT INTO ordemservicoitem (
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
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
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
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
                                            command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                            command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                            command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                            command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                            command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                            command.Parameters.AddWithValue("@QtdeProduzida", "")
                                            command.Parameters.AddWithValue("@QtdeFaltante", "")
                                            command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                            command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                                            command.Parameters.AddWithValue("@Estatus", "A")
                                            command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                            command.Parameters.AddWithValue("@fator", Fator)
                                            command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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

                                            ' command.ExecuteNonQuery()
                                            ' Tentativas de execução
                                            Dim maxTentativas As Integer = 3 ' Quantidade máxima de tentativas
                                            Dim tentativaAtual As Integer = 0
                                            Dim sucesso As Boolean = False

                                            Do While Not sucesso And tentativaAtual < maxTentativas
                                                Try
                                                    command.ExecuteNonQuery()
                                                    sucesso = True ' Se chegou aqui, a execução foi bem-sucedida
                                                Catch ex As Exception
                                                    tentativaAtual += 1
                                                    If tentativaAtual < maxTentativas Then
                                                        ' MsgBox($"Erro na execução. Tentando novamente em 30 segundos... ({tentativaAtual}/{maxTentativas})")



                                                        Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))
                                                        cl_BancoDados.AbrirBanco()


                                                    End If
                                                End Try
                                            Loop

                                        End Using

                                        ' Esperar 30 segundos após o final do bloco Using
                                        Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

                                    ElseIf TipoBanco = "SQL" Then

                                        Dim query As String = "INSERT INTO " & ComplementoTipoBanco & "ordemservicoitem (
                            IdOrdemServico, idProjeto, Projeto, idTag, Tag, 
                            ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal, 
                            Autor, Palavrachave, Notas, Espessura, AreaPintura, 
                            NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura, 
                            Largura, CodMatFabricante, DtCad, UsuarioCriacao, 
                            UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW, 
                            QtdeTotal, QtdeProduzida, QtdeFaltante, CriadoPor, 
                            DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde, 
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
                            @IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag, 
                            @ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal, 
                            @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, 
                            @NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura, 
                            @Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao, 
                            @UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW, 
                            @QtdeTotal, @QtdeProduzida, @QtdeFaltante, @CriadoPor, 
                            @DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde, 
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

                                        Using command As New SqlCommand(query, myconectSQL)
                                            ' Adicionando os parâmetros
                                            command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                            command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                            command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                            command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
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
                                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                            command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                            command.Parameters.AddWithValue("@QtdeProduzida", "")
                                            command.Parameters.AddWithValue("@QtdeFaltante", "")
                                            command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                            command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                                            command.Parameters.AddWithValue("@Estatus", "A")
                                            command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                            command.Parameters.AddWithValue("@fator", Fator)
                                            command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
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

                                            ' command.ExecuteNonQuery()
                                            ' Tentativas de execução
                                            Dim maxTentativas As Integer = 3 ' Quantidade máxima de tentativas
                                            Dim tentativaAtual As Integer = 0
                                            Dim sucesso As Boolean = False

                                            Do While Not sucesso And tentativaAtual < maxTentativas
                                                Try
                                                    command.ExecuteNonQuery()
                                                    sucesso = True ' Se chegou aqui, a execução foi bem-sucedida
                                                Catch ex As Exception
                                                    tentativaAtual += 1
                                                    If tentativaAtual < maxTentativas Then
                                                        'MsgBox($"Erro na execução. Tentando novamente em 30 segundos... ({tentativaAtual}/{maxTentativas})")
                                                        Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))
                                                        cl_BancoDados.AbrirBanco()
                                                    End If
                                                End Try
                                            Loop

                                        End Using

                                        ' Esperar 30 segundos após o final do bloco Using
                                        Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

                                    ElseIf TipoBanco = "ACCESS" Then


                                    End If

                                End If

                                ProgressBarListaSW.Value = A
                            Catch ex As Exception

                                MsgBox(ex.Message & " ERRO ao ler o arquivo: " & OrdemServico.EnderecoArquivo, MsgBoxStyle.Critical, "Atenção")

                                Continue For

                                'Catch ex As MySqlException

                                '    MsgBox(ex.Message)

                                '    '  cl_BancoDados.AbrirBanco()

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

        ' Retornar o cursor ao normal
        Cursor.Current = Cursors.Default


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
     " & ComplementoTipoBanco & "ViewVisaoGeralOrdemServico where Projeto = '" & cboProjetoPCP.Text & "'
GROUP BY 
    Projeto
ORDER BY 
    Projeto, Tag limit 1")

        TimerpcpAgrupamentoProjeto.Enabled = False

    End Sub



    '    Private Sub PreencherGrafico(ByVal idProjeto As String)


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
    '    idProjeto,
    '    idTag,
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
    '    idProjeto  = '" & idProjeto & "' group by idProjeto"

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
                Throw New Exception("Nenhum Projeto selecionado. Por favor, selecione um Projeto válido.")


            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboProjetoPCP.Text) Then
                Throw New Exception("O nome do Projeto não pode estar vazio. Selecione um Projeto válido.")
            End If

            Try

                idptojeto = cboProjetoPCP.SelectedValue


            Catch ex As Exception
                idptojeto = ""
            End Try




            Try

                ' Tentativa de retornar o nome da empresa
                txtClientepcp.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DescEmpresa FROM  " & ComplementoTipoBanco & "projetos where idProjeto  = " & cboProjetoPCP.SelectedValue, "DescEmpresa")

            Catch ex As Exception
                Me.txtClientepcp.Clear()


            End Try
        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
        End Try


        ' PreencherGrafico(idptojeto)


        dgvTimerpcpAgrupamentoProjetoDetalhamento.DataSource = cl_BancoDados.CarregarDados("SELECT idProjeto, idTag,Projeto,Tag, 
totalCorte, QtdetotalCorte, Corte, totalDobra, QtdetotalDobra, Dobra, totalSolda, QtdetotalSolda, Solda, totalPintura,
QtdetotalPintura, Pintura, totalMontagem, QtdetotalMontagem, Montagem FROM  " & ComplementoTipoBanco & "viewtotalelovucaoprocessoproducao
WHERE
    idProjeto  = '" & idptojeto & "' group by idProjeto, idTag order by Projeto, Tag")

    End Sub

    Private Sub TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Click

        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Transformar a OS : " & OrdemServico.IdOrdemServico & " em um produto padrão, o PCP podera liberar para produção sem consultar a engenharia!", "Conserção de Ordem de Serviço em produto.", MessageBoxButtons.YesNo)


        If result = DialogResult.Yes Then

            If OrdemServico.Liberado_Engenharia <> "S" Then

                MsgBox("Para Converter a OS em produto ela deve estar liberada para produção!", vbInformation, "Atenção")

                Exit Sub

            Else

                OrdemServico.IdOrdemServico = dgvos.CurrentRow.Cells("IdOrdemServico").Value.ToString
                CriaProdutos.ShowDialog()

                'dgvos.CurrentRow.Cells("ProdutoPadrao").Value = "SIM"
                'dgvos.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen

            End If

        End If



    End Sub

    Private Sub TimerProdutos_Tick(sender As Object, e As EventArgs) Handles TimerProdutos.Tick


        Dim sql As String = "SELECT IdOrdemServico,CodDesenhoProduto, CodOmie, 
DescricaoProduto, 
EnderecoFichaTecnica, EnderecoIsometrico,
ProdutoCriadoPor, DataCriacaoProduto,
RTRIM(UPPER(replace(EnderecoOrdemServico,'##','\\'))) as EnderecoOrdemServico ,
ProdutoPadrao
FROM  " & ComplementoTipoBanco & "ordemservico WHERE D_E_L_E_T_E <> '*' and
ProdutoPadrao = 'SIM' and 
CodDesenhoProduto like '%" & txPesqCodDesenhoProduto1.Text & "%' and
CodDesenhoProduto like '%" & txPesqCodDesenhoProduto2.Text & "%' and
CodDesenhoProduto like '%" & txPesqCodDesenhoProduto3.Text & "%' and
CodDesenhoProduto like '%" & txPesqCodDesenhoProduto4.Text & "%' and
CodOmie like '%" & txtPesqCodOmie1.Text & "%' and
CodOmie like '%" & txtPesqCodOmie2.Text & "%' and
CodOmie like '%" & txtPesqCodOmie3.Text & "%' and
CodOmie like '%" & txtPesqCodOmie4.Text & "%' and
DescricaoProduto like '%" & txtPesqDescricaoProduto1.Text & "%' and
DescricaoProduto like '%" & txtPesqDescricaoProduto2.Text & "%' and 
DescricaoProduto like '%" & txtPesqDescricaoProduto3.Text & "%' and
DescricaoProduto like '%" & txtPesqDescricaoProduto4.Text & "%'
ORDER BY CodDesenhoProduto"

        dgvTimerProdutos.DataSource = cl_BancoDados.CarregarDados(sql)

        dgvTimerProdutos.Columns("CodDesenhoProduto").Frozen = True
        dgvTimerProdutos.Columns("ProdutoCriadoPor").Visible = False
        dgvTimerProdutos.Columns("DataCriacaoProduto").Visible = False
        dgvTimerProdutos.Columns("EnderecoOrdemServico").Visible = False
        dgvTimerProdutos.Columns("IdOrdemServico").Visible = False


        TimerProdutos.Enabled = False

    End Sub

    Private Sub dgvos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvos.CellContentClick

    End Sub

    Private Sub txPesqCodDesenhoProduto1_TextChanged(sender As Object, e As EventArgs) Handles txPesqCodDesenhoProduto1.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txPesqCodDesenhoProduto2_TextChanged(sender As Object, e As EventArgs) Handles txPesqCodDesenhoProduto2.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txPesqCodDesenhoProduto3_TextChanged(sender As Object, e As EventArgs) Handles txPesqCodDesenhoProduto3.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txPesqCodDesenhoProduto4_TextChanged(sender As Object, e As EventArgs) Handles txPesqCodDesenhoProduto4.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie1_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCodOmie1.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie2_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCodOmie2.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie3_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCodOmie3.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie4_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCodOmie4.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto1_TextChanged(sender As Object, e As EventArgs) Handles txtPesqDescricaoProduto1.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto2_TextChanged(sender As Object, e As EventArgs) Handles txtPesqDescricaoProduto2.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto3_TextChanged(sender As Object, e As EventArgs) Handles txtPesqDescricaoProduto3.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto4_TextChanged(sender As Object, e As EventArgs) Handles txtPesqDescricaoProduto4.TextChanged
        TimerProdutos.Enabled = True
    End Sub

    Private Sub TimerProdutoItens_Tick(sender As Object, e As EventArgs) Handles TimerProdutoItens.Tick

        Try
            'If cl_BancoDados.AbrirBanco = False Then

            '    cl_BancoDados.AbrirBanco()

            'End If



            dgvTimerProdutosItens.DataSource = cl_BancoDados.CarregarDados("SELECT IDOrdemServicoItem,
    QtdeTotal,
    CodMatFabricante,
    DescResumo,
    DescDetal,
    MaterialSW,
    Espessura,
    Altura,
    Largura,
    Unidade,
    txtItemEstoque,
    Acabamento,
    txtTipoDesenho,
    RTRIM(UPPER(replace(EnderecoArquivo,'##','\\'))) as  EnderecoArquivo,
    ProdutoPrincipal,
    qtde, AreaPintura, Peso
                FROM
                 " & ComplementoTipoBanco & "ordemservicoitem
                WHERE
                (D_E_L_E_T_E <> '*')
                AND (IdOrdemServico = " & idOrdemServidoProduto & ")
                ORDER BY
                    IDOrdemServicoItem")


            dgvTimerProdutosItens.Columns("EnderecoArquivo").Frozen = False
            dgvTimerProdutosItens.Columns("ProdutoPrincipal").Frozen = False
            dgvTimerProdutosItens.Columns("qtde").Frozen = False
            dgvTimerProdutosItens.Columns("AreaPintura").Frozen = False
            dgvTimerProdutosItens.Columns("Peso").Frozen = False


        Catch ex As Exception
        Finally

        End Try

        'CarregarDadosDGV()

        TimerProdutoItens.Enabled = False


    End Sub

    Private Sub dgvTimerProdutos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTimerProdutos.CellContentClick




    End Sub

    Dim idOrdemServidoProduto As Integer
    Private Sub dgvTimerProdutos_Click(sender As Object, e As EventArgs) Handles dgvTimerProdutos.Click


        Try
            idOrdemServidoProduto = dgvTimerProdutos.CurrentRow.Cells("IdOrdemServico").Value.ToString
            OrdemServico.IdOrdemServico = idOrdemServidoProduto

        Catch ex As Exception

            idOrdemServidoProduto = 0
            OrdemServico.IdOrdemServico = 0

        End Try



        TimerProdutoItens.Enabled = True

    End Sub

    Private Sub dgvTimerProdutosItens_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTimerProdutosItens.CellContentClick



    End Sub

    Private Sub dgvTimerProdutosItens_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvTimerProdutosItens.DataBindingComplete

        For Each row As DataGridViewRow In dgvTimerProdutosItens.Rows
            Dim valorEnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()
            Dim valorProdutoPrincipal As String = If(row.Cells("ProdutoPrincipal").Value, "").ToString()

            ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
            If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
           valorProdutoPrincipal.IndexOf("SIM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                row.Cells("dgvIconeItemOSProduto").Value = My.Resources.IconeswPrincipal ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                row.Cells("dgvIconeItemOSProduto").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define outra imagem se for .SLDPRT
                row.Cells("dgvIconeItemOSProduto").Value = My.Resources.IcopneMontagemPRT
            Else
                row.Cells("dgvIconeItemOSProduto").Value = My.Resources.material_escolar_32
            End If

        Next

    End Sub

    Private Sub dgvTimerProdutosItens_DoubleClick(sender As Object, e As EventArgs) Handles dgvTimerProdutosItens.DoubleClick


        Try
            ' Abre o documento SolidWorks e aguarda até que ele esteja totalmente carregado
            Dim filePath As String = dgvTimerProdutosItens.CurrentRow.Cells("EnderecoArquivo").Value.ToString()
            Dim CodMatFabricante As String = dgvTimerProdutosItens.CurrentRow.Cells("CodMatFabricante").Value.ToString()

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

    Private Sub dgvTimerProdutos_DoubleClick(sender As Object, e As EventArgs) Handles dgvTimerProdutos.DoubleClick


        OrdemServico.IdOrdemServico = dgvTimerProdutos.CurrentRow.Cells("IdOrdemServico").Value.ToString()

        OrdemServico.ProdutoPadrao = "SIM"
        OrdemServico.CodDesenhoProduto = dgvTimerProdutos.CurrentRow.Cells("CodDesenhoProduto").Value.ToString()
        OrdemServico.CodOmie = dgvTimerProdutos.CurrentRow.Cells("CodOmie").Value.ToString()
        OrdemServico.DescricaoProduto = dgvTimerProdutos.CurrentRow.Cells("DescricaoProduto").Value.ToString()
        OrdemServico.EnderecoFichaTecnica = dgvTimerProdutos.CurrentRow.Cells("EnderecoFichaTecnica").Value.ToString()
        OrdemServico.EnderecoIsometrico = dgvTimerProdutos.CurrentRow.Cells("EnderecoIsometrico").Value.ToString()
        OrdemServico.ProdutoCriadoPor = Usuario.NomeCompleto
        OrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString

        CriaProdutos.txCodDesenhoProduto.Text = OrdemServico.CodDesenhoProduto
        CriaProdutos.txtCodOmie.Text = OrdemServico.CodOmie
        CriaProdutos.txtDescricaoProduto.Text = OrdemServico.DescricaoProduto
        CriaProdutos.lblEnderecoFichaTecnica.Text = OrdemServico.EnderecoFichaTecnica
        CriaProdutos.lblEnderecoIsometrico.Text = OrdemServico.EnderecoIsometrico


        CriaProdutos.ShowDialog()

    End Sub

    Private Sub AbrirPDFFichaTecnicaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFFichaTecnicaToolStripMenuItem.Click


        Try
            Dim ArquivoPdf As String = dgvTimerProdutos.CurrentRow.Cells("EnderecoFichaTecnica").Value.ToString

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

                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub AbrirPDFIsometricoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFIsometricoToolStripMenuItem.Click

        Try
            Dim ArquivoPdf As String = dgvTimerProdutos.CurrentRow.Cells("EnderecoIsometrico").Value.ToString
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

                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try


    End Sub




    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click

        Try
            OrdemServico.IDOrdemServicoItem = dgvTimerProdutosItens.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "SIM", "IDOrdemServicoItem", OrdemServico.IDOrdemServicoItem)

            '  dgvTimerProdutosItens.CurrentRow.Cells("dgvIconeItemOSProduto").Value = "SIM".ToUpper

            dgvTimerProdutosItens.CurrentRow.Cells("dgvIconeItemOSProduto").Value = My.Resources.IconeswPrincipal

            OrdemServico.IDOrdemServicoItem = 0

        Catch ex As Exception

            OrdemServico.IDOrdemServicoItem = Nothing

            MsgBox("Item da Ordem de Serviço não Valido", vbCritical, "Atenção")

        End Try

    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click

        Try
            OrdemServico.IDOrdemServicoItem = dgvTimerProdutosItens.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "", "IDOrdemServicoItem", OrdemServico.IDOrdemServicoItem)

            ' dgvTimerProdutosItens.CurrentRow.Cells("dgvIconeItemOSProduto").Value = "SIM".ToUpper

            dgvTimerProdutosItens.CurrentRow.Cells("dgvIconeItemOSProduto").Value = My.Resources.IcopneMontagemSW

        Catch ex As Exception

            OrdemServico.IDOrdemServicoItem = Nothing

            MsgBox("Item da Ordem de Serviço não Valido", vbCritical, "Atenção")

        End Try

    End Sub

    Private Sub mnuDGVListaMaterialSW_Opening(sender As Object, e As CancelEventArgs) Handles mnuDGVListaMaterialSW.Opening

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click


        Try

            Dim ArquivoPdf As String = dgvTimerProdutosItens.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

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

                    dgvTimerProdutosItens.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try


    End Sub

    Private Sub btnIsometrico_Click(sender As Object, e As EventArgs) Handles btnIsometrico.Click

        Dim caminhoArquivo As String = cl_BancoDados.SelecionarArquivoPDF()

        If Not String.IsNullOrEmpty(caminhoArquivo) Then
            OrdemServico.EnderecoIsometrico = caminhoArquivo
            Me.txtIsometrico.Text = caminhoArquivo
            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cl_BancoDados.AlteracaoEspecifica("material", "EnderecoIsometrico", caminhoArquivo, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

        Else
            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If



    End Sub

    Private Sub btnFichaTecnica_Click(sender As Object, e As EventArgs) Handles btnFichaTecnica.Click

        Dim caminhoArquivo As String = cl_BancoDados.SelecionarArquivoPDF()

        If Not String.IsNullOrEmpty(caminhoArquivo) Then
            OrdemServico.EnderecoFichaTecnica = caminhoArquivo
            Me.txtFichaTecnica.Text = caminhoArquivo
            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)

            cl_BancoDados.AlteracaoEspecifica("material", "EnderecoFichaTecnica", caminhoArquivo, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

        Else
            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub


    Private Sub chkBoxAcabamento_Click(sender As Object, e As EventArgs) Handles chkBoxAcabamento.Click


        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.Acabamento = Me.chkBoxAcabamento.Text
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtacabamento", DadosArquivoCorrente.Acabamento, DadosArquivoCorrente.Acabamento)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try


    End Sub
    Private Sub chkBoxTipoDesenho_Click(sender As Object, e As EventArgs) Handles chkBoxTipoDesenho.Click

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.TipoDesenho = chkBoxTipoDesenho.Text
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txttipodesenho", DadosArquivoCorrente.TipoDesenho, DadosArquivoCorrente.TipoDesenho)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub
    'Private Sub chkConverterDXF_Validating(sender As Object, e As CancelEventArgs) Handles chkConverterDXF.Validating

    '    ' Verifica se o CheckBox está marcado
    '    If Not chkConverterDXF.Checked Then
    '        ' Define a mensagem de erro para o CheckBox
    '        ErrorProvider1.SetError(chkConverterDXF, "Com esta opção selecionada os arquvo LXSD & DFT serão apagados!.")
    '        ' Cancela a validação para evitar a mudança de foco
    '        e.Cancel = True
    '    Else
    '        ' Remove a mensagem de erro
    '        ErrorProvider1.SetError(chkConverterDXF, "")
    '    End If

    'End Sub

    'Private Sub chkConverterPDF_Validating(sender As Object, e As CancelEventArgs) Handles chkConverterPDF.Validating

    '    ' Verifica se o CheckBox está marcado
    '    If Not chkConverterPDF.Checked Then
    '        ' Define a mensagem de erro para o CheckBox
    '        ErrorProvider1.SetError(chkConverterPDF, "Com esta opção selecionada os arquvo DFT serão atualizados!.")
    '        ' Cancela a validação para evitar a mudança de foco
    '        e.Cancel = True
    '    Else
    '        ' Remove a mensagem de erro
    '        ErrorProvider1.SetError(chkConverterPDF, "")
    '    End If

    'End Sub


    Private Sub tpgPrincipal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tpgPrincipal.SelectedIndexChanged

        Try



            If tpgPrincipal.SelectedTab Is tpgOrdemServico Then
                Timerdgvos.Enabled = True

            End If

            If tpgPrincipal.SelectedTab Is tpgListaRNCPecaCorrente Then
                TimerFiltroPecaAtivaOS.Enabled = True

            End If

        Catch ex As Exception
        Finally

        End Try




    End Sub

    Private Sub EditarProdutoExistenteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarProdutoExistenteToolStripMenuItem.Click

        Try

            idOrdemServidoProduto = dgvTimerProdutos.CurrentRow.Cells("IdOrdemServico").Value.ToString
            OrdemServico.IdOrdemServico = idOrdemServidoProduto

        Catch ex As Exception

            idOrdemServidoProduto = 0
        Finally

        End Try


        If idOrdemServidoProduto <> 0 Then


            CriaProdutos.lblEnderecoFichaTecnica.Text = dgvTimerProdutos.CurrentRow.Cells("EnderecoFichaTecnica").Value.ToString
            CriaProdutos.lblEnderecoIsometrico.Text = dgvTimerProdutos.CurrentRow.Cells("EnderecoIsometrico").Value.ToString
            CriaProdutos.txtCodOmie.Text = dgvTimerProdutos.CurrentRow.Cells("CodOmie").Value.ToString
            CriaProdutos.txtDescricaoProduto.Text = dgvTimerProdutos.CurrentRow.Cells("DescricaoProduto").Value.ToString
            CriaProdutos.txCodDesenhoProduto.Text = dgvTimerProdutos.CurrentRow.Cells("CodDesenhoProduto").Value.ToString

            CriaProdutos.ShowDialog()

        End If

    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click


        Try

            Dim ArquivoPdf As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

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

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        Try

            Dim ArquivoDXF As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

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

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub AbrirLXDSDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            Dim ArquivoLXDS As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoLXDS = Path.ChangeExtension(ArquivoLXDS, ".LXDS")

            ' Obtém o caminho completo
            ArquivoLXDS = Path.GetFullPath(ArquivoLXDS)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoLXDS) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoLXDS)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub
    Private Sub TsbAtualizarBOM_Click(sender As Object, e As EventArgs) Handles TsbAtualizarBOM.Click
        Try
            AtualizarIcones()
        Catch ex As Exception
        Finally
        End Try


    End Sub

    Private Sub chkVerificarPDF_DoubleClick(sender As Object, e As EventArgs) Handles chkVerificarPDF.DoubleClick

        Try

            Dim ArquivoPdf As String = DadosArquivoCorrente.EnderecoArquivo ' dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

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

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub chkVerificarDXF_DoubleClick(sender As Object, e As EventArgs) Handles chkVerificarDXF.DoubleClick

        Try

            Dim ArquivoDXF As String = DadosArquivoCorrente.EnderecoArquivo  ' dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

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

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub chkVerificarLXDS_DoubleClick(sender As Object, e As EventArgs) Handles chkVerificarLXDS.DoubleClick

        Try

            Dim ArquivoLXDS As String = DadosArquivoCorrente.EnderecoArquivo  'dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoLXDS = Path.ChangeExtension(ArquivoLXDS, ".LXDS")

            ' Obtém o caminho completo
            ArquivoLXDS = Path.GetFullPath(ArquivoLXDS)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoLXDS) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoLXDS)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click

        Try

            Dim ArquivoLXDS As String = DadosArquivoCorrente.EnderecoArquivo  'dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoLXDS = Path.ChangeExtension(ArquivoLXDS, ".LXDS")

            ' Obtém o caminho completo
            ArquivoLXDS = Path.GetFullPath(ArquivoLXDS)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoLXDS) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoLXDS)

                    p.Start()
                    'p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            '  MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub chkVerificarDXF_CheckedChanged(sender As Object, e As EventArgs) Handles chkVerificarDXF.CheckedChanged

    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click

        Try

            Dim ArquivoDXF As String = DadosArquivoCorrente.EnderecoArquivo  ' dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".DXF")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    ' p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            ' MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub


    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click

        AtualziarDadosSinco()

    End Sub


    Private Sub txtPalavraChave_DoubleClick(sender As Object, e As EventArgs) Handles txtPalavraChave.DoubleClick

        Me.txtPalavraChave.Text = DadosArquivoCorrente.EnderecoArquivo.ToUpper

    End Sub

    Private Sub TsbInspecaoQualidade_Click(sender As Object, e As EventArgs) Handles TsbInspecaoQualidade.Click

        If swModel Is Nothing Then

            Exit Sub

        Else

            If DadosArquivoCorrente.NomeArquivoSemExtensao.ToString <> "" Then

                InspecaoQualidade.ShowDialog()

            End If

        End If

    End Sub

End Class