<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Painel_Leitura_Dados
    Inherits System.Windows.Forms.UserControl

    'O UserControl substitui o descarte para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Painel_Leitura_Dados))
        Me.tpgPrincipal = New System.Windows.Forms.TabControl()
        Me.tpgFolhaDados = New System.Windows.Forms.TabPage()
        Me.btnIsometrico = New System.Windows.Forms.Button()
        Me.btnFichaTecnica = New System.Windows.Forms.Button()
        Me.txtIsometrico = New System.Windows.Forms.TextBox()
        Me.txtFichaTecnica = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblProfundidadeTotalCaixaDelimitadora = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAlturaTotalCaixaDelimitadora = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblLarguraTotalCaixaDelimitadora = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblAreaPintura = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblNumeroDobra = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblComprimento = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblLargura = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblEspessura = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chkBoxTipoDesenho = New System.Windows.Forms.CheckedListBox()
        Me.BnPrincipal = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.tsBLerDados = New System.Windows.Forms.ToolStripButton()
        Me.tsbSalvar = New System.Windows.Forms.ToolStripButton()
        Me.tsbConverterDXF = New System.Windows.Forms.ToolStripButton()
        Me.TSBConverterPDF = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.TSBAssociarMaterial = New System.Windows.Forms.ToolStripButton()
        Me.tsbInserirNaOS = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbConfiguracoes = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ConfiguraçãoToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfExportarArquivoParaOSToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbFerramentas = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarArquivosNosDiretorioToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsarFormatoA3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsarFormatoA4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsarFornatoA4DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator25 = New System.Windows.Forms.ToolStripSeparator()
        Me.TsbInspecaoQualidade = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator39 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.lblQtdeEstoque = New System.Windows.Forms.ToolStripLabel()
        Me.tslVersaoSistema = New System.Windows.Forms.ToolStripLabel()
        Me.txtNomeArquivo = New System.Windows.Forms.ToolStripTextBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.chkBoxAcabamento = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.optProcessoSoldagemSim = New System.Windows.Forms.RadioButton()
        Me.optProcessoSoldagemNao = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkCorte = New System.Windows.Forms.CheckBox()
        Me.chkDobra = New System.Windows.Forms.CheckBox()
        Me.chkSolda = New System.Windows.Forms.CheckBox()
        Me.chkPintura = New System.Windows.Forms.CheckBox()
        Me.chkMontagem = New System.Windows.Forms.CheckBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.OPTEstoqueSim = New System.Windows.Forms.RadioButton()
        Me.OPTEstoqueNao = New System.Windows.Forms.RadioButton()
        Me.btnPendencias = New System.Windows.Forms.Button()
        Me.chkVerificarLXDS = New System.Windows.Forms.CheckBox()
        Me.chkVerificarDFT = New System.Windows.Forms.CheckBox()
        Me.cboTitulo = New System.Windows.Forms.ComboBox()
        Me.DGVMontaPeca = New System.Windows.Forms.DataGridView()
        Me.mnuDGVMontaPeca = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.chkVerificarDXF = New System.Windows.Forms.CheckBox()
        Me.chkVerificarPDF = New System.Windows.Forms.CheckBox()
        Me.txtAssuntoSubiTitulo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtComentarios = New System.Windows.Forms.TextBox()
        Me.txtPalavraChave = New System.Windows.Forms.TextBox()
        Me.txtAuthor = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mnuPrincipal = New System.Windows.Forms.MenuStrip()
        Me.ConfiguraçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraçãoToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfExportarArquivoParaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarArquivosNosDiretorioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CadastroDeProjetosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FerramentasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrocarFormatoA3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrocarForrmatoA4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrocarParaFormato4ADeitadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarFormatoA3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BUSCRAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BiscarFormatoA4DeitadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtHoraServidor = New System.Windows.Forms.ToolStripTextBox()
        Me.TSTVersaoSistema = New System.Windows.Forms.ToolStripTextBox()
        Me.tpgDesenhosCadstrados = New System.Windows.Forms.TabPage()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TxtPesqSubtitulo3 = New System.Windows.Forms.TextBox()
        Me.TxtPesqSubtitulo2 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TxtPesqSubtitulo = New System.Windows.Forms.TextBox()
        Me.TxtPesgTitulo = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TxtPesgNomeDesenho = New System.Windows.Forms.TextBox()
        Me.dgvDesenhos = New System.Windows.Forms.DataGridView()
        Me.dgvIcone = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Dgvrnc = New System.Windows.Forms.DataGridViewImageColumn()
        Me.btnAtualizarCadastro = New System.Windows.Forms.Button()
        Me.mnubtnListaMaterial = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tpgBom = New System.Windows.Forms.TabPage()
        Me.chkConverterPDF = New System.Windows.Forms.CheckBox()
        Me.chkConverterDXF = New System.Windows.Forms.CheckBox()
        Me.BindingNavigator2 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator28 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator37 = New System.Windows.Forms.ToolStripSeparator()
        Me.TsbAtualizarBOM = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator38 = New System.Windows.Forms.ToolStripSeparator()
        Me.ProgressBarListaSW = New System.Windows.Forms.ProgressBar()
        Me.lblOrdemServicoAtiva = New System.Windows.Forms.Label()
        Me.dgvDataGridBOM = New System.Windows.Forms.DataGridView()
        Me.DGVIconeLXDS = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DGVIconeDXF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvIconePDF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnudgvDataGridBOM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator34 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator33 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator35 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator36 = New System.Windows.Forms.ToolStripSeparator()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnConverterListaDXFPDF = New System.Windows.Forms.Button()
        Me.btnListaMaterial = New System.Windows.Forms.Button()
        Me.btnLimparBom = New System.Windows.Forms.Button()
        Me.btnInserirItensOrdemServico = New System.Windows.Forms.Button()
        Me.tpgOrdemServico = New System.Windows.Forms.TabPage()
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.TSBSalvarOrdemServico = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.txtPesqAcabamentoDesenho = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtPesqTipoDesenho = New System.Windows.Forms.TextBox()
        Me.txtPesqCriadoPor = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPesqNumeroDesenho = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cboOpcoesAcabamento = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.ProgressBarProcessoLiberacaoOrdemServico = New System.Windows.Forms.ProgressBar()
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.DGVListaMaterialSW = New System.Windows.Forms.DataGridView()
        Me.dgvSelecao = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgvIconeItemOS = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvDXF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvPDF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnuDGVListaMaterialSW = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MarcarTodosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesmarcarTodosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InverterSeleçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.chkMostraLiberadasPelaEngenharia = New System.Windows.Forms.CheckBox()
        Me.dgvos = New System.Windows.Forms.DataGridView()
        Me.dgvStatus = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnudgvos = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelarLiberaçãoDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.GeralExcelDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarArquivoEmDXFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarArquivoEmPDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtDescricaoTag = New System.Windows.Forms.TextBox()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.cboTag = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cboProjeto = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.DGVListaMaterialSWMaterial = New System.Windows.Forms.DataGridView()
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.btnAplicarAcabamento = New System.Windows.Forms.Button()
        Me.tpgListaRNCPecaCorrente = New System.Windows.Forms.TabPage()
        Me.btnAtualizarDadosItemOs = New System.Windows.Forms.Button()
        Me.lblNUmeroDocumentoAtivo = New System.Windows.Forms.Label()
        Me.DGVTimerFiltroPecaAtivaOS = New System.Windows.Forms.DataGridView()
        Me.dgvSelecaoAtualizacaoItemOs = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgvTipoDesenhoAtualizacaoItemOs = New System.Windows.Forms.DataGridViewImageColumn()
        Me.tpgPCP = New System.Windows.Forms.TabPage()
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento = New System.Windows.Forms.DataGridView()
        Me.txtClientepcp = New System.Windows.Forms.TextBox()
        Me.cboProjetoPCP = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dgvTimerpcpAgrupamentoProjeto = New System.Windows.Forms.DataGridView()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.dgvTimerProdutosItens = New System.Windows.Forms.DataGridView()
        Me.dgvIconeItemOSProduto = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnudgvTimerProdutosItens = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator30 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator31 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtPesqCodOmie4 = New System.Windows.Forms.TextBox()
        Me.txPesqCodDesenhoProduto4 = New System.Windows.Forms.TextBox()
        Me.txtPesqCodOmie3 = New System.Windows.Forms.TextBox()
        Me.txPesqCodDesenhoProduto3 = New System.Windows.Forms.TextBox()
        Me.txtPesqCodOmie2 = New System.Windows.Forms.TextBox()
        Me.txPesqCodDesenhoProduto2 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricaoProduto4 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricaoProduto3 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricaoProduto2 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricaoProduto1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPesqCodOmie1 = New System.Windows.Forms.TextBox()
        Me.txPesqCodDesenhoProduto1 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dgvTimerProdutos = New System.Windows.Forms.DataGridView()
        Me.MNUdgvTimerProdutos = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirPDFFichaTecnicaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator29 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirPDFIsometricoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator32 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditarProdutoExistenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerdgvDesenhos = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMontaPeca = New System.Windows.Forms.Timer(Me.components)
        Me.Timerdgvos = New System.Windows.Forms.Timer(Me.components)
        Me.TimerDGVListaMaterialSW = New System.Windows.Forms.Timer(Me.components)
        Me.TimerFiltroPecaAtivaOS = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTipAjuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.TimerHoraServidor = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.TimerpcpAgrupamentoProjeto = New System.Windows.Forms.Timer(Me.components)
        Me.TimerProdutos = New System.Windows.Forms.Timer(Me.components)
        Me.TimerProdutoItens = New System.Windows.Forms.Timer(Me.components)
        Me.tpgPrincipal.SuspendLayout()
        Me.tpgFolhaDados.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.BnPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BnPrincipal.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.DGVMontaPeca, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuDGVMontaPeca.SuspendLayout()
        Me.mnuPrincipal.SuspendLayout()
        Me.tpgDesenhosCadstrados.SuspendLayout()
        CType(Me.dgvDesenhos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnubtnListaMaterial.SuspendLayout()
        Me.tpgBom.SuspendLayout()
        CType(Me.BindingNavigator2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator2.SuspendLayout()
        CType(Me.dgvDataGridBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnudgvDataGridBOM.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.tpgOrdemServico.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.DGVListaMaterialSW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuDGVListaMaterialSW.SuspendLayout()
        CType(Me.dgvos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnudgvos.SuspendLayout()
        CType(Me.DGVListaMaterialSWMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpgListaRNCPecaCorrente.SuspendLayout()
        CType(Me.DGVTimerFiltroPecaAtivaOS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpgPCP.SuspendLayout()
        CType(Me.dgvTimerpcpAgrupamentoProjetoDetalhamento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTimerpcpAgrupamentoProjeto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvTimerProdutosItens, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnudgvTimerProdutosItens.SuspendLayout()
        CType(Me.dgvTimerProdutos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MNUdgvTimerProdutos.SuspendLayout()
        Me.SuspendLayout()
        '
        'tpgPrincipal
        '
        Me.tpgPrincipal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tpgPrincipal.Controls.Add(Me.tpgFolhaDados)
        Me.tpgPrincipal.Controls.Add(Me.tpgDesenhosCadstrados)
        Me.tpgPrincipal.Controls.Add(Me.tpgBom)
        Me.tpgPrincipal.Controls.Add(Me.tpgOrdemServico)
        Me.tpgPrincipal.Controls.Add(Me.tpgListaRNCPecaCorrente)
        Me.tpgPrincipal.Controls.Add(Me.tpgPCP)
        Me.tpgPrincipal.Controls.Add(Me.TabPage1)
        Me.tpgPrincipal.Location = New System.Drawing.Point(4, 4)
        Me.tpgPrincipal.Margin = New System.Windows.Forms.Padding(4)
        Me.tpgPrincipal.Name = "tpgPrincipal"
        Me.tpgPrincipal.SelectedIndex = 0
        Me.tpgPrincipal.Size = New System.Drawing.Size(908, 880)
        Me.tpgPrincipal.TabIndex = 0
        '
        'tpgFolhaDados
        '
        Me.tpgFolhaDados.Controls.Add(Me.btnIsometrico)
        Me.tpgFolhaDados.Controls.Add(Me.btnFichaTecnica)
        Me.tpgFolhaDados.Controls.Add(Me.txtIsometrico)
        Me.tpgFolhaDados.Controls.Add(Me.txtFichaTecnica)
        Me.tpgFolhaDados.Controls.Add(Me.Label32)
        Me.tpgFolhaDados.Controls.Add(Me.Label31)
        Me.tpgFolhaDados.Controls.Add(Me.GroupBox5)
        Me.tpgFolhaDados.Controls.Add(Me.GroupBox4)
        Me.tpgFolhaDados.Controls.Add(Me.BnPrincipal)
        Me.tpgFolhaDados.Controls.Add(Me.GroupBox10)
        Me.tpgFolhaDados.Controls.Add(Me.GroupBox8)
        Me.tpgFolhaDados.Controls.Add(Me.GroupBox2)
        Me.tpgFolhaDados.Controls.Add(Me.GroupBox7)
        Me.tpgFolhaDados.Controls.Add(Me.btnPendencias)
        Me.tpgFolhaDados.Controls.Add(Me.chkVerificarLXDS)
        Me.tpgFolhaDados.Controls.Add(Me.chkVerificarDFT)
        Me.tpgFolhaDados.Controls.Add(Me.cboTitulo)
        Me.tpgFolhaDados.Controls.Add(Me.DGVMontaPeca)
        Me.tpgFolhaDados.Controls.Add(Me.chkVerificarDXF)
        Me.tpgFolhaDados.Controls.Add(Me.chkVerificarPDF)
        Me.tpgFolhaDados.Controls.Add(Me.txtAssuntoSubiTitulo)
        Me.tpgFolhaDados.Controls.Add(Me.Label4)
        Me.tpgFolhaDados.Controls.Add(Me.Label5)
        Me.tpgFolhaDados.Controls.Add(Me.txtComentarios)
        Me.tpgFolhaDados.Controls.Add(Me.txtPalavraChave)
        Me.tpgFolhaDados.Controls.Add(Me.txtAuthor)
        Me.tpgFolhaDados.Controls.Add(Me.Label3)
        Me.tpgFolhaDados.Controls.Add(Me.Label2)
        Me.tpgFolhaDados.Controls.Add(Me.Label1)
        Me.tpgFolhaDados.Controls.Add(Me.mnuPrincipal)
        Me.tpgFolhaDados.Location = New System.Drawing.Point(4, 25)
        Me.tpgFolhaDados.Margin = New System.Windows.Forms.Padding(4)
        Me.tpgFolhaDados.Name = "tpgFolhaDados"
        Me.tpgFolhaDados.Padding = New System.Windows.Forms.Padding(4)
        Me.tpgFolhaDados.Size = New System.Drawing.Size(900, 851)
        Me.tpgFolhaDados.TabIndex = 0
        Me.tpgFolhaDados.Text = "Dados Principais"
        Me.tpgFolhaDados.UseVisualStyleBackColor = True
        '
        'btnIsometrico
        '
        Me.btnIsometrico.Location = New System.Drawing.Point(775, 299)
        Me.btnIsometrico.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnIsometrico.Name = "btnIsometrico"
        Me.btnIsometrico.Size = New System.Drawing.Size(20, 23)
        Me.btnIsometrico.TabIndex = 35
        Me.btnIsometrico.Text = "..."
        Me.btnIsometrico.UseVisualStyleBackColor = True
        '
        'btnFichaTecnica
        '
        Me.btnFichaTecnica.Location = New System.Drawing.Point(363, 300)
        Me.btnFichaTecnica.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnFichaTecnica.Name = "btnFichaTecnica"
        Me.btnFichaTecnica.Size = New System.Drawing.Size(20, 23)
        Me.btnFichaTecnica.TabIndex = 34
        Me.btnFichaTecnica.Text = "..."
        Me.btnFichaTecnica.UseVisualStyleBackColor = True
        '
        'txtIsometrico
        '
        Me.txtIsometrico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIsometrico.Location = New System.Drawing.Point(396, 300)
        Me.txtIsometrico.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIsometrico.Name = "txtIsometrico"
        Me.txtIsometrico.Size = New System.Drawing.Size(372, 22)
        Me.txtIsometrico.TabIndex = 33
        Me.ToolTipAjuda.SetToolTip(Me.txtIsometrico, "Dê um duplo clique para carregar a sigla do projetista atual. A sigla foi cadastr" &
        "ada no " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sistema SINCO através do formulário de cadastro de usuário!")
        '
        'txtFichaTecnica
        '
        Me.txtFichaTecnica.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFichaTecnica.Location = New System.Drawing.Point(11, 300)
        Me.txtFichaTecnica.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFichaTecnica.Name = "txtFichaTecnica"
        Me.txtFichaTecnica.Size = New System.Drawing.Size(345, 22)
        Me.txtFichaTecnica.TabIndex = 32
        Me.ToolTipAjuda.SetToolTip(Me.txtFichaTecnica, "Dê um duplo clique para carregar a sigla do projetista atual. A sigla foi cadastr" &
        "ada no " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sistema SINCO através do formulário de cadastro de usuário!")
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(396, 281)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(130, 16)
        Me.Label32.TabIndex = 31
        Me.Label32.Text = "Desenho Isometrico:"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(12, 281)
        Me.Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(95, 16)
        Me.Label31.TabIndex = 30
        Me.Label31.Text = "Ficha Tecnica:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblProfundidadeTotalCaixaDelimitadora)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.lblAlturaTotalCaixaDelimitadora)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.lblPeso)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Controls.Add(Me.lblMaterial)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.lblLarguraTotalCaixaDelimitadora)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.lblAreaPintura)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.lblNumeroDobra)
        Me.GroupBox5.Controls.Add(Me.Label23)
        Me.GroupBox5.Controls.Add(Me.lblComprimento)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.lblLargura)
        Me.GroupBox5.Controls.Add(Me.Label21)
        Me.GroupBox5.Controls.Add(Me.lblEspessura)
        Me.GroupBox5.Controls.Add(Me.Label17)
        Me.GroupBox5.Location = New System.Drawing.Point(8, 471)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(215, 300)
        Me.GroupBox5.TabIndex = 29
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Processos:"
        '
        'lblProfundidadeTotalCaixaDelimitadora
        '
        Me.lblProfundidadeTotalCaixaDelimitadora.AutoSize = True
        Me.lblProfundidadeTotalCaixaDelimitadora.BackColor = System.Drawing.Color.White
        Me.lblProfundidadeTotalCaixaDelimitadora.Location = New System.Drawing.Point(85, 271)
        Me.lblProfundidadeTotalCaixaDelimitadora.Name = "lblProfundidadeTotalCaixaDelimitadora"
        Me.lblProfundidadeTotalCaixaDelimitadora.Size = New System.Drawing.Size(21, 16)
        Me.lblProfundidadeTotalCaixaDelimitadora.TabIndex = 25
        Me.lblProfundidadeTotalCaixaDelimitadora.Text = "00"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(43, 25)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 16)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Esp.:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAlturaTotalCaixaDelimitadora
        '
        Me.lblAlturaTotalCaixaDelimitadora.AutoSize = True
        Me.lblAlturaTotalCaixaDelimitadora.BackColor = System.Drawing.Color.White
        Me.lblAlturaTotalCaixaDelimitadora.Location = New System.Drawing.Point(85, 217)
        Me.lblAlturaTotalCaixaDelimitadora.Name = "lblAlturaTotalCaixaDelimitadora"
        Me.lblAlturaTotalCaixaDelimitadora.Size = New System.Drawing.Size(21, 16)
        Me.lblAlturaTotalCaixaDelimitadora.TabIndex = 24
        Me.lblAlturaTotalCaixaDelimitadora.Text = "00"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(39, 50)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 16)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Larg.:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPeso
        '
        Me.lblPeso.AutoSize = True
        Me.lblPeso.BackColor = System.Drawing.Color.White
        Me.lblPeso.Location = New System.Drawing.Point(85, 162)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(21, 16)
        Me.lblPeso.TabIndex = 23
        Me.lblPeso.Text = "00"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(5, 110)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 16)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Nº Dobras:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMaterial
        '
        Me.lblMaterial.AutoSize = True
        Me.lblMaterial.BackColor = System.Drawing.Color.White
        Me.lblMaterial.Location = New System.Drawing.Point(85, 188)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.Size = New System.Drawing.Size(21, 16)
        Me.lblMaterial.TabIndex = 22
        Me.lblMaterial.Text = "00"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(21, 135)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 16)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Area M²:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLarguraTotalCaixaDelimitadora
        '
        Me.lblLarguraTotalCaixaDelimitadora.AutoSize = True
        Me.lblLarguraTotalCaixaDelimitadora.BackColor = System.Drawing.Color.White
        Me.lblLarguraTotalCaixaDelimitadora.Location = New System.Drawing.Point(85, 242)
        Me.lblLarguraTotalCaixaDelimitadora.Name = "lblLarguraTotalCaixaDelimitadora"
        Me.lblLarguraTotalCaixaDelimitadora.Size = New System.Drawing.Size(21, 16)
        Me.lblLarguraTotalCaixaDelimitadora.TabIndex = 21
        Me.lblLarguraTotalCaixaDelimitadora.Text = "00"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(29, 80)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 16)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Comp.:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAreaPintura
        '
        Me.lblAreaPintura.AutoSize = True
        Me.lblAreaPintura.BackColor = System.Drawing.Color.White
        Me.lblAreaPintura.Location = New System.Drawing.Point(85, 135)
        Me.lblAreaPintura.Name = "lblAreaPintura"
        Me.lblAreaPintura.Size = New System.Drawing.Size(21, 16)
        Me.lblAreaPintura.TabIndex = 20
        Me.lblAreaPintura.Text = "00"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(17, 162)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 16)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Kg/Peso:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNumeroDobra
        '
        Me.lblNumeroDobra.AutoSize = True
        Me.lblNumeroDobra.BackColor = System.Drawing.Color.White
        Me.lblNumeroDobra.Location = New System.Drawing.Point(85, 110)
        Me.lblNumeroDobra.Name = "lblNumeroDobra"
        Me.lblNumeroDobra.Size = New System.Drawing.Size(21, 16)
        Me.lblNumeroDobra.TabIndex = 19
        Me.lblNumeroDobra.Text = "00"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(35, 217)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(44, 16)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Altura:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblComprimento
        '
        Me.lblComprimento.AutoSize = True
        Me.lblComprimento.BackColor = System.Drawing.Color.White
        Me.lblComprimento.Location = New System.Drawing.Point(85, 80)
        Me.lblComprimento.Name = "lblComprimento"
        Me.lblComprimento.Size = New System.Drawing.Size(21, 16)
        Me.lblComprimento.TabIndex = 18
        Me.lblComprimento.Text = "00"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(23, 242)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 16)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "Largura:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLargura
        '
        Me.lblLargura.AutoSize = True
        Me.lblLargura.BackColor = System.Drawing.Color.White
        Me.lblLargura.Location = New System.Drawing.Point(85, 50)
        Me.lblLargura.Name = "lblLargura"
        Me.lblLargura.Size = New System.Drawing.Size(21, 16)
        Me.lblLargura.TabIndex = 17
        Me.lblLargura.Text = "00"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(20, 271)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(59, 16)
        Me.Label21.TabIndex = 9
        Me.Label21.Text = "Profund.:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEspessura
        '
        Me.lblEspessura.AutoSize = True
        Me.lblEspessura.BackColor = System.Drawing.Color.White
        Me.lblEspessura.Location = New System.Drawing.Point(85, 25)
        Me.lblEspessura.Name = "lblEspessura"
        Me.lblEspessura.Size = New System.Drawing.Size(21, 16)
        Me.lblEspessura.TabIndex = 16
        Me.lblEspessura.Text = "00"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(21, 188)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 16)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "material:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.chkBoxTipoDesenho)
        Me.GroupBox4.Location = New System.Drawing.Point(229, 608)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(663, 162)
        Me.GroupBox4.TabIndex = 28
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Tipo Desenho"
        '
        'chkBoxTipoDesenho
        '
        Me.chkBoxTipoDesenho.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBoxTipoDesenho.CheckOnClick = True
        Me.chkBoxTipoDesenho.FormattingEnabled = True
        Me.chkBoxTipoDesenho.Location = New System.Drawing.Point(11, 15)
        Me.chkBoxTipoDesenho.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkBoxTipoDesenho.Name = "chkBoxTipoDesenho"
        Me.chkBoxTipoDesenho.ScrollAlwaysVisible = True
        Me.chkBoxTipoDesenho.Size = New System.Drawing.Size(643, 123)
        Me.chkBoxTipoDesenho.Sorted = True
        Me.chkBoxTipoDesenho.TabIndex = 16
        '
        'BnPrincipal
        '
        Me.BnPrincipal.AddNewItem = Nothing
        Me.BnPrincipal.AutoSize = False
        Me.BnPrincipal.CountItem = Nothing
        Me.BnPrincipal.DeleteItem = Nothing
        Me.BnPrincipal.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.BnPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBLerDados, Me.tsbSalvar, Me.tsbConverterDXF, Me.TSBConverterPDF, Me.ToolStripButton2, Me.ToolStripButton8, Me.TSBAssociarMaterial, Me.tsbInserirNaOS, Me.ToolStripSeparator23, Me.tsbConfiguracoes, Me.ToolStripSeparator24, Me.tsbFerramentas, Me.ToolStripSeparator25, Me.TsbInspecaoQualidade, Me.ToolStripSeparator39, Me.ToolStripButton9, Me.ToolStripLabel1, Me.lblQtdeEstoque, Me.tslVersaoSistema, Me.txtNomeArquivo})
        Me.BnPrincipal.Location = New System.Drawing.Point(4, 4)
        Me.BnPrincipal.MoveFirstItem = Nothing
        Me.BnPrincipal.MoveLastItem = Nothing
        Me.BnPrincipal.MoveNextItem = Nothing
        Me.BnPrincipal.MovePreviousItem = Nothing
        Me.BnPrincipal.Name = "BnPrincipal"
        Me.BnPrincipal.PositionItem = Nothing
        Me.BnPrincipal.Size = New System.Drawing.Size(892, 31)
        Me.BnPrincipal.TabIndex = 27
        Me.BnPrincipal.Text = "BindingNavigator1"
        '
        'tsBLerDados
        '
        Me.tsBLerDados.CheckOnClick = True
        Me.tsBLerDados.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBLerDados.Image = Global.SwLynx_4._1.My.Resources.Resources.leitura
        Me.tsBLerDados.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBLerDados.Name = "tsBLerDados"
        Me.tsBLerDados.Size = New System.Drawing.Size(29, 28)
        Me.tsBLerDados.Text = "Ler Dados"
        Me.tsBLerDados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBLerDados.ToolTipText = "Leitura de dados do Arquivo Corrente"
        '
        'tsbSalvar
        '
        Me.tsbSalvar.CheckOnClick = True
        Me.tsbSalvar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSalvar.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.tsbSalvar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSalvar.Name = "tsbSalvar"
        Me.tsbSalvar.Size = New System.Drawing.Size(29, 28)
        Me.tsbSalvar.Text = "Salvar"
        Me.tsbSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbSalvar.ToolTipText = "Salva os dados do Arquivo Corrente"
        '
        'tsbConverterDXF
        '
        Me.tsbConverterDXF.CheckOnClick = True
        Me.tsbConverterDXF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbConverterDXF.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf
        Me.tsbConverterDXF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConverterDXF.Name = "tsbConverterDXF"
        Me.tsbConverterDXF.Size = New System.Drawing.Size(29, 28)
        Me.tsbConverterDXF.Text = "DXF"
        Me.tsbConverterDXF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbConverterDXF.ToolTipText = "Se for um arquivo part do tipo Chapa irá converter o blank em dxf"
        '
        'TSBConverterPDF
        '
        Me.TSBConverterPDF.CheckOnClick = True
        Me.TSBConverterPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBConverterPDF.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.TSBConverterPDF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBConverterPDF.Name = "TSBConverterPDF"
        Me.TSBConverterPDF.Size = New System.Drawing.Size(29, 28)
        Me.TSBConverterPDF.Text = "PDF"
        Me.TSBConverterPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSBConverterPDF.ToolTipText = "So o Arquivo Corrente for um Detalhamento irá converter em PDF"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.SwLynx_4._1.My.Resources.Resources.CYPCUT
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(29, 28)
        Me.ToolStripButton2.Text = "Abrir Arquivo LXDS"
        Me.ToolStripButton2.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Abrir Arquivo LXDS, Normalmente e um extenção do CypCut, corte a L" &
    "aser."
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = Global.SwLynx_4._1.My.Resources.Resources.arquivo_dxf
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(29, 28)
        Me.ToolStripButton8.Text = "Abrir Arquivo DXF"
        Me.ToolStripButton8.ToolTipText = "Dica de Uso" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Abrir Arquivo DXF, abre os arquivos em DXF no seu programa padrão de" &
    " edição. "
        '
        'TSBAssociarMaterial
        '
        Me.TSBAssociarMaterial.CheckOnClick = True
        Me.TSBAssociarMaterial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBAssociarMaterial.Image = Global.SwLynx_4._1.My.Resources.Resources.construcao
        Me.TSBAssociarMaterial.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBAssociarMaterial.Name = "TSBAssociarMaterial"
        Me.TSBAssociarMaterial.Size = New System.Drawing.Size(29, 28)
        Me.TSBAssociarMaterial.Text = "material"
        Me.TSBAssociarMaterial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSBAssociarMaterial.ToolTipText = "Abre o formulario para composição da peças corrente/Associação de material"
        '
        'tsbInserirNaOS
        '
        Me.tsbInserirNaOS.CheckOnClick = True
        Me.tsbInserirNaOS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbInserirNaOS.Image = Global.SwLynx_4._1.My.Resources.Resources.ordem_de_servico
        Me.tsbInserirNaOS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbInserirNaOS.Name = "tsbInserirNaOS"
        Me.tsbInserirNaOS.Size = New System.Drawing.Size(29, 28)
        Me.tsbInserirNaOS.Text = "Inserir na OS"
        Me.tsbInserirNaOS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbInserirNaOS.ToolTipText = "Inseri na Ordem de Serviço Selecionada a peças corrente, a quantidade deve ser in" &
    "formada."
        '
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        Me.ToolStripSeparator23.Size = New System.Drawing.Size(6, 31)
        '
        'tsbConfiguracoes
        '
        Me.tsbConfiguracoes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbConfiguracoes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem2, Me.ConfExportarArquivoParaOSToolStripMenuItem1, Me.ToolStripSeparator21})
        Me.tsbConfiguracoes.Image = Global.SwLynx_4._1.My.Resources.Resources.configuracoes
        Me.tsbConfiguracoes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfiguracoes.Name = "tsbConfiguracoes"
        Me.tsbConfiguracoes.Size = New System.Drawing.Size(34, 28)
        Me.tsbConfiguracoes.Text = "Configurações"
        Me.tsbConfiguracoes.ToolTipText = "Opções de Configurações do sistema "
        '
        'ConfiguraçãoToolStripMenuItem2
        '
        Me.ConfiguraçãoToolStripMenuItem2.Name = "ConfiguraçãoToolStripMenuItem2"
        Me.ConfiguraçãoToolStripMenuItem2.Size = New System.Drawing.Size(497, 26)
        Me.ConfiguraçãoToolStripMenuItem2.Text = "Configuração de Parametros Banco de Dados & Pastas Padrões"
        '
        'ConfExportarArquivoParaOSToolStripMenuItem1
        '
        Me.ConfExportarArquivoParaOSToolStripMenuItem1.Name = "ConfExportarArquivoParaOSToolStripMenuItem1"
        Me.ConfExportarArquivoParaOSToolStripMenuItem1.Size = New System.Drawing.Size(497, 26)
        Me.ConfExportarArquivoParaOSToolStripMenuItem1.Text = "Configurar Parametros do Sistema"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(494, 6)
        '
        'ToolStripSeparator24
        '
        Me.ToolStripSeparator24.Name = "ToolStripSeparator24"
        Me.ToolStripSeparator24.Size = New System.Drawing.Size(6, 31)
        '
        'tsbFerramentas
        '
        Me.tsbFerramentas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbFerramentas.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator22, Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1, Me.BuscarArquivosNosDiretorioToolStripMenuItem1, Me.UsarFormatoA3ToolStripMenuItem, Me.UsarFormatoA4ToolStripMenuItem, Me.UsarFornatoA4DToolStripMenuItem})
        Me.tsbFerramentas.Image = Global.SwLynx_4._1.My.Resources.Resources.ferramentas
        Me.tsbFerramentas.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFerramentas.Name = "tsbFerramentas"
        Me.tsbFerramentas.Size = New System.Drawing.Size(34, 28)
        Me.tsbFerramentas.Text = "Ferramentas"
        Me.tsbFerramentas.ToolTipText = "Ferramentas do Lynx"
        '
        'ToolStripSeparator22
        '
        Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
        Me.ToolStripSeparator22.Size = New System.Drawing.Size(308, 6)
        '
        'AtualizarDesenhoPeloDiretorioToolStripMenuItem1
        '
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Name = "AtualizarDesenhoPeloDiretorioToolStripMenuItem1"
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Size = New System.Drawing.Size(311, 26)
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Text = "Atualizar Desenho pelo Diretorio"
        '
        'BuscarArquivosNosDiretorioToolStripMenuItem1
        '
        Me.BuscarArquivosNosDiretorioToolStripMenuItem1.Name = "BuscarArquivosNosDiretorioToolStripMenuItem1"
        Me.BuscarArquivosNosDiretorioToolStripMenuItem1.Size = New System.Drawing.Size(311, 26)
        Me.BuscarArquivosNosDiretorioToolStripMenuItem1.Text = "Buscar Arquivos nos Diretorio"
        '
        'UsarFormatoA3ToolStripMenuItem
        '
        Me.UsarFormatoA3ToolStripMenuItem.Name = "UsarFormatoA3ToolStripMenuItem"
        Me.UsarFormatoA3ToolStripMenuItem.Size = New System.Drawing.Size(311, 26)
        Me.UsarFormatoA3ToolStripMenuItem.Text = "Usar Formato A3"
        '
        'UsarFormatoA4ToolStripMenuItem
        '
        Me.UsarFormatoA4ToolStripMenuItem.Name = "UsarFormatoA4ToolStripMenuItem"
        Me.UsarFormatoA4ToolStripMenuItem.Size = New System.Drawing.Size(311, 26)
        Me.UsarFormatoA4ToolStripMenuItem.Text = "Usar Formato A4"
        '
        'UsarFornatoA4DToolStripMenuItem
        '
        Me.UsarFornatoA4DToolStripMenuItem.Name = "UsarFornatoA4DToolStripMenuItem"
        Me.UsarFornatoA4DToolStripMenuItem.Size = New System.Drawing.Size(311, 26)
        Me.UsarFornatoA4DToolStripMenuItem.Text = "Usar Fornato A4D"
        '
        'ToolStripSeparator25
        '
        Me.ToolStripSeparator25.Name = "ToolStripSeparator25"
        Me.ToolStripSeparator25.Size = New System.Drawing.Size(6, 31)
        '
        'TsbInspecaoQualidade
        '
        Me.TsbInspecaoQualidade.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsbInspecaoQualidade.Image = Global.SwLynx_4._1.My.Resources.Resources.inspecao
        Me.TsbInspecaoQualidade.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbInspecaoQualidade.Name = "TsbInspecaoQualidade"
        Me.TsbInspecaoQualidade.Size = New System.Drawing.Size(29, 28)
        Me.TsbInspecaoQualidade.Text = "Ficha para Controle Dimencional"
        Me.TsbInspecaoQualidade.ToolTipText = "Ficha para Controle Dimencional:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Informe as principais medidas com controel dime" &
    "ncional da peças."
        '
        'ToolStripSeparator39
        '
        Me.ToolStripSeparator39.Name = "ToolStripSeparator39"
        Me.ToolStripSeparator39.Size = New System.Drawing.Size(6, 31)
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.CheckOnClick = True
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(29, 28)
        Me.ToolStripButton9.Text = "Atualizar"
        Me.ToolStripButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton9.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Caso um novo cadastro de Acabamento, Tipo Desenho, Projeto e/ou Ta" &
    "g seja inserido e não " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "apareça no ComboBox, basta clicar aqui para atualizar os" &
    " dados."
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.ForeColor = System.Drawing.Color.Red
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(143, 28)
        Me.ToolStripLabel1.Text = "qtde Peças Estoque:"
        '
        'lblQtdeEstoque
        '
        Me.lblQtdeEstoque.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQtdeEstoque.ForeColor = System.Drawing.Color.Red
        Me.lblQtdeEstoque.Name = "lblQtdeEstoque"
        Me.lblQtdeEstoque.Size = New System.Drawing.Size(28, 28)
        Me.lblQtdeEstoque.Text = "00"
        '
        'tslVersaoSistema
        '
        Me.tslVersaoSistema.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tslVersaoSistema.Name = "tslVersaoSistema"
        Me.tslVersaoSistema.Size = New System.Drawing.Size(27, 28)
        Me.tslVersaoSistema.Text = "---"
        '
        'txtNomeArquivo
        '
        Me.txtNomeArquivo.AutoSize = False
        Me.txtNomeArquivo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtNomeArquivo.Name = "txtNomeArquivo"
        Me.txtNomeArquivo.Size = New System.Drawing.Size(250, 31)
        '
        'GroupBox10
        '
        Me.GroupBox10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox10.Controls.Add(Me.chkBoxAcabamento)
        Me.GroupBox10.Location = New System.Drawing.Point(229, 391)
        Me.GroupBox10.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox10.Size = New System.Drawing.Size(663, 210)
        Me.GroupBox10.TabIndex = 20
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Acabamento"
        '
        'chkBoxAcabamento
        '
        Me.chkBoxAcabamento.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBoxAcabamento.CheckOnClick = True
        Me.chkBoxAcabamento.FormattingEnabled = True
        Me.chkBoxAcabamento.Location = New System.Drawing.Point(5, 21)
        Me.chkBoxAcabamento.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkBoxAcabamento.Name = "chkBoxAcabamento"
        Me.chkBoxAcabamento.ScrollAlwaysVisible = True
        Me.chkBoxAcabamento.Size = New System.Drawing.Size(651, 174)
        Me.chkBoxAcabamento.Sorted = True
        Me.chkBoxAcabamento.TabIndex = 18
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.optProcessoSoldagemSim)
        Me.GroupBox8.Controls.Add(Me.optProcessoSoldagemNao)
        Me.GroupBox8.Location = New System.Drawing.Point(123, 391)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox8.Size = New System.Drawing.Size(101, 80)
        Me.GroupBox8.TabIndex = 16
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Soldagem:"
        '
        'optProcessoSoldagemSim
        '
        Me.optProcessoSoldagemSim.AutoSize = True
        Me.optProcessoSoldagemSim.Location = New System.Drawing.Point(5, 21)
        Me.optProcessoSoldagemSim.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.optProcessoSoldagemSim.Name = "optProcessoSoldagemSim"
        Me.optProcessoSoldagemSim.Size = New System.Drawing.Size(51, 20)
        Me.optProcessoSoldagemSim.TabIndex = 13
        Me.optProcessoSoldagemSim.TabStop = True
        Me.optProcessoSoldagemSim.Text = "SIM"
        Me.optProcessoSoldagemSim.UseVisualStyleBackColor = True
        '
        'optProcessoSoldagemNao
        '
        Me.optProcessoSoldagemNao.AutoSize = True
        Me.optProcessoSoldagemNao.Checked = True
        Me.optProcessoSoldagemNao.Location = New System.Drawing.Point(5, 46)
        Me.optProcessoSoldagemNao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.optProcessoSoldagemNao.Name = "optProcessoSoldagemNao"
        Me.optProcessoSoldagemNao.Size = New System.Drawing.Size(57, 20)
        Me.optProcessoSoldagemNao.TabIndex = 14
        Me.optProcessoSoldagemNao.TabStop = True
        Me.optProcessoSoldagemNao.Text = "NÃO"
        Me.optProcessoSoldagemNao.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.chkCorte)
        Me.GroupBox2.Controls.Add(Me.chkDobra)
        Me.GroupBox2.Controls.Add(Me.chkSolda)
        Me.GroupBox2.Controls.Add(Me.chkPintura)
        Me.GroupBox2.Controls.Add(Me.chkMontagem)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 329)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(881, 57)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Processos"
        '
        'chkCorte
        '
        Me.chkCorte.AutoSize = True
        Me.chkCorte.Image = Global.SwLynx_4._1.My.Resources.Resources.chapa_de_aco_32x32
        Me.chkCorte.Location = New System.Drawing.Point(13, 20)
        Me.chkCorte.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkCorte.Name = "chkCorte"
        Me.chkCorte.Size = New System.Drawing.Size(87, 26)
        Me.chkCorte.TabIndex = 1
        Me.chkCorte.Text = "Corte"
        Me.chkCorte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkCorte.UseVisualStyleBackColor = True
        '
        'chkDobra
        '
        Me.chkDobra.AutoSize = True
        Me.chkDobra.Image = Global.SwLynx_4._1.My.Resources.Resources.dobra
        Me.chkDobra.Location = New System.Drawing.Point(133, 20)
        Me.chkDobra.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkDobra.Name = "chkDobra"
        Me.chkDobra.Size = New System.Drawing.Size(93, 26)
        Me.chkDobra.TabIndex = 0
        Me.chkDobra.Text = "Dodra"
        Me.chkDobra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkDobra.UseVisualStyleBackColor = True
        '
        'chkSolda
        '
        Me.chkSolda.AutoSize = True
        Me.chkSolda.Image = Global.SwLynx_4._1.My.Resources.Resources.de_solda
        Me.chkSolda.Location = New System.Drawing.Point(267, 20)
        Me.chkSolda.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkSolda.Name = "chkSolda"
        Me.chkSolda.Size = New System.Drawing.Size(91, 26)
        Me.chkSolda.TabIndex = 2
        Me.chkSolda.Text = "Solda"
        Me.chkSolda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkSolda.UseVisualStyleBackColor = True
        '
        'chkPintura
        '
        Me.chkPintura.AutoSize = True
        Me.chkPintura.Image = Global.SwLynx_4._1.My.Resources.Resources.rolo_de_pintura
        Me.chkPintura.Location = New System.Drawing.Point(397, 20)
        Me.chkPintura.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkPintura.Name = "chkPintura"
        Me.chkPintura.Size = New System.Drawing.Size(132, 26)
        Me.chkPintura.TabIndex = 3
        Me.chkPintura.Text = "Acabamento"
        Me.chkPintura.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkPintura.UseVisualStyleBackColor = True
        '
        'chkMontagem
        '
        Me.chkMontagem.AutoSize = True
        Me.chkMontagem.Image = Global.SwLynx_4._1.My.Resources.Resources.montagem
        Me.chkMontagem.Location = New System.Drawing.Point(577, 20)
        Me.chkMontagem.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkMontagem.Name = "chkMontagem"
        Me.chkMontagem.Size = New System.Drawing.Size(119, 26)
        Me.chkMontagem.TabIndex = 4
        Me.chkMontagem.Text = "Montagem"
        Me.chkMontagem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkMontagem.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.OPTEstoqueSim)
        Me.GroupBox7.Controls.Add(Me.OPTEstoqueNao)
        Me.GroupBox7.Location = New System.Drawing.Point(11, 391)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox7.Size = New System.Drawing.Size(96, 80)
        Me.GroupBox7.TabIndex = 15
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Estoque:"
        '
        'OPTEstoqueSim
        '
        Me.OPTEstoqueSim.AutoSize = True
        Me.OPTEstoqueSim.Location = New System.Drawing.Point(5, 21)
        Me.OPTEstoqueSim.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.OPTEstoqueSim.Name = "OPTEstoqueSim"
        Me.OPTEstoqueSim.Size = New System.Drawing.Size(51, 20)
        Me.OPTEstoqueSim.TabIndex = 13
        Me.OPTEstoqueSim.TabStop = True
        Me.OPTEstoqueSim.Text = "SIM"
        Me.OPTEstoqueSim.UseVisualStyleBackColor = True
        '
        'OPTEstoqueNao
        '
        Me.OPTEstoqueNao.AutoSize = True
        Me.OPTEstoqueNao.Checked = True
        Me.OPTEstoqueNao.Location = New System.Drawing.Point(5, 46)
        Me.OPTEstoqueNao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.OPTEstoqueNao.Name = "OPTEstoqueNao"
        Me.OPTEstoqueNao.Size = New System.Drawing.Size(57, 20)
        Me.OPTEstoqueNao.TabIndex = 14
        Me.OPTEstoqueNao.TabStop = True
        Me.OPTEstoqueNao.Text = "NÃO"
        Me.OPTEstoqueNao.UseVisualStyleBackColor = True
        '
        'btnPendencias
        '
        Me.btnPendencias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPendencias.Enabled = False
        Me.btnPendencias.Image = Global.SwLynx_4._1.My.Resources.Resources.atencao
        Me.btnPendencias.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPendencias.Location = New System.Drawing.Point(5, 42)
        Me.btnPendencias.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPendencias.Name = "btnPendencias"
        Me.btnPendencias.Size = New System.Drawing.Size(891, 50)
        Me.btnPendencias.TabIndex = 23
        Me.btnPendencias.Text = " Pendências"
        Me.btnPendencias.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnPendencias.UseVisualStyleBackColor = True
        '
        'chkVerificarLXDS
        '
        Me.chkVerificarLXDS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarLXDS.AutoSize = True
        Me.chkVerificarLXDS.Image = Global.SwLynx_4._1.My.Resources.Resources.CYPCUT
        Me.chkVerificarLXDS.Location = New System.Drawing.Point(793, 241)
        Me.chkVerificarLXDS.Margin = New System.Windows.Forms.Padding(4)
        Me.chkVerificarLXDS.Name = "chkVerificarLXDS"
        Me.chkVerificarLXDS.Size = New System.Drawing.Size(96, 34)
        Me.chkVerificarLXDS.TabIndex = 22
        Me.chkVerificarLXDS.Text = "LXDS"
        Me.chkVerificarLXDS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarLXDS.UseVisualStyleBackColor = True
        '
        'chkVerificarDFT
        '
        Me.chkVerificarDFT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarDFT.AutoSize = True
        Me.chkVerificarDFT.Image = Global.SwLynx_4._1.My.Resources.Resources.DFT
        Me.chkVerificarDFT.Location = New System.Drawing.Point(795, 155)
        Me.chkVerificarDFT.Margin = New System.Windows.Forms.Padding(4)
        Me.chkVerificarDFT.Name = "chkVerificarDFT"
        Me.chkVerificarDFT.Size = New System.Drawing.Size(81, 28)
        Me.chkVerificarDFT.TabIndex = 20
        Me.chkVerificarDFT.Text = "DFT"
        Me.chkVerificarDFT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarDFT.UseVisualStyleBackColor = True
        '
        'cboTitulo
        '
        Me.cboTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTitulo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboTitulo.FormattingEnabled = True
        Me.cboTitulo.Location = New System.Drawing.Point(95, 206)
        Me.cboTitulo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboTitulo.Name = "cboTitulo"
        Me.cboTitulo.Size = New System.Drawing.Size(672, 23)
        Me.cboTitulo.TabIndex = 6
        Me.ToolTipAjuda.SetToolTip(Me.cboTitulo, "O usuário pode digitar diretamente ou selecionar um tipo de produto que foi previ" &
        "amente" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "cadastrado no sistema SINCO através do formulário de tipos de produto.")
        '
        'DGVMontaPeca
        '
        Me.DGVMontaPeca.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVMontaPeca.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.DGVMontaPeca.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.DGVMontaPeca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMontaPeca.ContextMenuStrip = Me.mnuDGVMontaPeca
        Me.DGVMontaPeca.Location = New System.Drawing.Point(7, 780)
        Me.DGVMontaPeca.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGVMontaPeca.Name = "DGVMontaPeca"
        Me.DGVMontaPeca.RowHeadersWidth = 51
        Me.DGVMontaPeca.RowTemplate.Height = 24
        Me.DGVMontaPeca.Size = New System.Drawing.Size(885, 65)
        Me.DGVMontaPeca.TabIndex = 15
        '
        'mnuDGVMontaPeca
        '
        Me.mnuDGVMontaPeca.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuDGVMontaPeca.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcluirOMaterialDoDesenhoToolStripMenuItem})
        Me.mnuDGVMontaPeca.Name = "mnuDGVMontaPeca"
        Me.mnuDGVMontaPeca.Size = New System.Drawing.Size(282, 30)
        '
        'ExcluirOMaterialDoDesenhoToolStripMenuItem
        '
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.excluir
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Name = "ExcluirOMaterialDoDesenhoToolStripMenuItem"
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Size = New System.Drawing.Size(281, 26)
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Text = "Excluir o material do Desenho"
        '
        'chkVerificarDXF
        '
        Me.chkVerificarDXF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarDXF.AutoSize = True
        Me.chkVerificarDXF.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf
        Me.chkVerificarDXF.Location = New System.Drawing.Point(795, 190)
        Me.chkVerificarDXF.Margin = New System.Windows.Forms.Padding(4)
        Me.chkVerificarDXF.Name = "chkVerificarDXF"
        Me.chkVerificarDXF.Size = New System.Drawing.Size(89, 42)
        Me.chkVerificarDXF.TabIndex = 13
        Me.chkVerificarDXF.Text = "DXF"
        Me.chkVerificarDXF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarDXF.UseVisualStyleBackColor = True
        '
        'chkVerificarPDF
        '
        Me.chkVerificarPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarPDF.AutoSize = True
        Me.chkVerificarPDF.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.chkVerificarPDF.Location = New System.Drawing.Point(795, 103)
        Me.chkVerificarPDF.Margin = New System.Windows.Forms.Padding(4)
        Me.chkVerificarPDF.Name = "chkVerificarPDF"
        Me.chkVerificarPDF.Size = New System.Drawing.Size(92, 34)
        Me.chkVerificarPDF.TabIndex = 12
        Me.chkVerificarPDF.Text = "PDF"
        Me.chkVerificarPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarPDF.UseVisualStyleBackColor = True
        '
        'txtAssuntoSubiTitulo
        '
        Me.txtAssuntoSubiTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAssuntoSubiTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAssuntoSubiTitulo.Location = New System.Drawing.Point(95, 244)
        Me.txtAssuntoSubiTitulo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAssuntoSubiTitulo.Name = "txtAssuntoSubiTitulo"
        Me.txtAssuntoSubiTitulo.Size = New System.Drawing.Size(672, 22)
        Me.txtAssuntoSubiTitulo.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 247)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "SubTitulo:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(48, 209)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 16)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Titulo:"
        '
        'txtComentarios
        '
        Me.txtComentarios.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComentarios.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComentarios.Location = New System.Drawing.Point(95, 140)
        Me.txtComentarios.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComentarios.Multiline = True
        Me.txtComentarios.Name = "txtComentarios"
        Me.txtComentarios.Size = New System.Drawing.Size(672, 54)
        Me.txtComentarios.TabIndex = 5
        '
        'txtPalavraChave
        '
        Me.txtPalavraChave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPalavraChave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPalavraChave.Location = New System.Drawing.Point(327, 103)
        Me.txtPalavraChave.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPalavraChave.Name = "txtPalavraChave"
        Me.txtPalavraChave.Size = New System.Drawing.Size(440, 22)
        Me.txtPalavraChave.TabIndex = 4
        '
        'txtAuthor
        '
        Me.txtAuthor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAuthor.Location = New System.Drawing.Point(95, 103)
        Me.txtAuthor.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.Size = New System.Drawing.Size(103, 22)
        Me.txtAuthor.TabIndex = 3
        Me.ToolTipAjuda.SetToolTip(Me.txtAuthor, "Dê um duplo clique para carregar a sigla do projetista atual. A sigla foi cadastr" &
        "ada no " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sistema SINCO através do formulário de cadastro de usuário!")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 140)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Comentários:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(205, 107)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Palavras Chaves:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(43, 107)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Author:"
        '
        'mnuPrincipal
        '
        Me.mnuPrincipal.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem, Me.FerramentasToolStripMenuItem, Me.txtHoraServidor, Me.TSTVersaoSistema})
        Me.mnuPrincipal.Location = New System.Drawing.Point(4, 4)
        Me.mnuPrincipal.Name = "mnuPrincipal"
        Me.mnuPrincipal.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.mnuPrincipal.Size = New System.Drawing.Size(796, 31)
        Me.mnuPrincipal.TabIndex = 17
        Me.mnuPrincipal.Text = "MenuStrip1"
        Me.mnuPrincipal.Visible = False
        '
        'ConfiguraçãoToolStripMenuItem
        '
        Me.ConfiguraçãoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem1, Me.ConfExportarArquivoParaOSToolStripMenuItem, Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem, Me.BuscarArquivosNosDiretorioToolStripMenuItem, Me.CadastroDeProjetosToolStripMenuItem})
        Me.ConfiguraçãoToolStripMenuItem.Name = "ConfiguraçãoToolStripMenuItem"
        Me.ConfiguraçãoToolStripMenuItem.Size = New System.Drawing.Size(112, 27)
        Me.ConfiguraçãoToolStripMenuItem.Text = "Configuração"
        '
        'ConfiguraçãoToolStripMenuItem1
        '
        Me.ConfiguraçãoToolStripMenuItem1.Name = "ConfiguraçãoToolStripMenuItem1"
        Me.ConfiguraçãoToolStripMenuItem1.Size = New System.Drawing.Size(311, 26)
        Me.ConfiguraçãoToolStripMenuItem1.Text = "Configuração"
        '
        'ConfExportarArquivoParaOSToolStripMenuItem
        '
        Me.ConfExportarArquivoParaOSToolStripMenuItem.Name = "ConfExportarArquivoParaOSToolStripMenuItem"
        Me.ConfExportarArquivoParaOSToolStripMenuItem.Size = New System.Drawing.Size(311, 26)
        Me.ConfExportarArquivoParaOSToolStripMenuItem.Text = "Conf. Exportar Arquivo para OS"
        '
        'AtualizarDesenhoPeloDiretorioToolStripMenuItem
        '
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem.Name = "AtualizarDesenhoPeloDiretorioToolStripMenuItem"
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem.Size = New System.Drawing.Size(311, 26)
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem.Text = "Atualizar Desenho pelo Diretorio"
        '
        'BuscarArquivosNosDiretorioToolStripMenuItem
        '
        Me.BuscarArquivosNosDiretorioToolStripMenuItem.Name = "BuscarArquivosNosDiretorioToolStripMenuItem"
        Me.BuscarArquivosNosDiretorioToolStripMenuItem.Size = New System.Drawing.Size(311, 26)
        Me.BuscarArquivosNosDiretorioToolStripMenuItem.Text = "Buscar Arquivos nos Diretorio"
        '
        'CadastroDeProjetosToolStripMenuItem
        '
        Me.CadastroDeProjetosToolStripMenuItem.Enabled = False
        Me.CadastroDeProjetosToolStripMenuItem.Name = "CadastroDeProjetosToolStripMenuItem"
        Me.CadastroDeProjetosToolStripMenuItem.Size = New System.Drawing.Size(311, 26)
        Me.CadastroDeProjetosToolStripMenuItem.Text = "Cadastro de Projetos"
        '
        'FerramentasToolStripMenuItem
        '
        Me.FerramentasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TrocarFormatoA3ToolStripMenuItem, Me.TrocarForrmatoA4ToolStripMenuItem, Me.TrocarParaFormato4ADeitadoToolStripMenuItem, Me.BuscarFormatoA3ToolStripMenuItem, Me.BUSCRAToolStripMenuItem, Me.BiscarFormatoA4DeitadoToolStripMenuItem})
        Me.FerramentasToolStripMenuItem.Name = "FerramentasToolStripMenuItem"
        Me.FerramentasToolStripMenuItem.Size = New System.Drawing.Size(104, 27)
        Me.FerramentasToolStripMenuItem.Text = "Ferramentas"
        '
        'TrocarFormatoA3ToolStripMenuItem
        '
        Me.TrocarFormatoA3ToolStripMenuItem.Name = "TrocarFormatoA3ToolStripMenuItem"
        Me.TrocarFormatoA3ToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.TrocarFormatoA3ToolStripMenuItem.Text = "Trocar  para Formato A3"
        '
        'TrocarForrmatoA4ToolStripMenuItem
        '
        Me.TrocarForrmatoA4ToolStripMenuItem.Name = "TrocarForrmatoA4ToolStripMenuItem"
        Me.TrocarForrmatoA4ToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.TrocarForrmatoA4ToolStripMenuItem.Text = "Trocar para Formato A4"
        '
        'TrocarParaFormato4ADeitadoToolStripMenuItem
        '
        Me.TrocarParaFormato4ADeitadoToolStripMenuItem.Name = "TrocarParaFormato4ADeitadoToolStripMenuItem"
        Me.TrocarParaFormato4ADeitadoToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.TrocarParaFormato4ADeitadoToolStripMenuItem.Text = "Trocar para Formato A4 Deitado"
        '
        'BuscarFormatoA3ToolStripMenuItem
        '
        Me.BuscarFormatoA3ToolStripMenuItem.Name = "BuscarFormatoA3ToolStripMenuItem"
        Me.BuscarFormatoA3ToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.BuscarFormatoA3ToolStripMenuItem.Text = "Buscar Formato A3"
        '
        'BUSCRAToolStripMenuItem
        '
        Me.BUSCRAToolStripMenuItem.Name = "BUSCRAToolStripMenuItem"
        Me.BUSCRAToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.BUSCRAToolStripMenuItem.Text = "Buscar Formato A4"
        '
        'BiscarFormatoA4DeitadoToolStripMenuItem
        '
        Me.BiscarFormatoA4DeitadoToolStripMenuItem.Name = "BiscarFormatoA4DeitadoToolStripMenuItem"
        Me.BiscarFormatoA4DeitadoToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.BiscarFormatoA4DeitadoToolStripMenuItem.Text = "Buscar Formato A4 Deitado"
        '
        'txtHoraServidor
        '
        Me.txtHoraServidor.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtHoraServidor.Name = "txtHoraServidor"
        Me.txtHoraServidor.Size = New System.Drawing.Size(100, 27)
        '
        'TSTVersaoSistema
        '
        Me.TSTVersaoSistema.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TSTVersaoSistema.AutoSize = False
        Me.TSTVersaoSistema.Enabled = False
        Me.TSTVersaoSistema.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TSTVersaoSistema.Name = "TSTVersaoSistema"
        Me.TSTVersaoSistema.Size = New System.Drawing.Size(200, 27)
        '
        'tpgDesenhosCadstrados
        '
        Me.tpgDesenhosCadstrados.Controls.Add(Me.ProgressBar1)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.TxtPesqSubtitulo3)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.TxtPesqSubtitulo2)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.Label16)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.TxtPesqSubtitulo)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.TxtPesgTitulo)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.Label18)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.Label19)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.TxtPesgNomeDesenho)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.dgvDesenhos)
        Me.tpgDesenhosCadstrados.Controls.Add(Me.btnAtualizarCadastro)
        Me.tpgDesenhosCadstrados.Location = New System.Drawing.Point(4, 25)
        Me.tpgDesenhosCadstrados.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tpgDesenhosCadstrados.Name = "tpgDesenhosCadstrados"
        Me.tpgDesenhosCadstrados.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tpgDesenhosCadstrados.Size = New System.Drawing.Size(900, 851)
        Me.tpgDesenhosCadstrados.TabIndex = 2
        Me.tpgDesenhosCadstrados.Text = "Desenhos Cadastrados"
        Me.tpgDesenhosCadstrados.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(5, 62)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(887, 23)
        Me.ProgressBar1.TabIndex = 107
        '
        'TxtPesqSubtitulo3
        '
        Me.TxtPesqSubtitulo3.BackColor = System.Drawing.SystemColors.Info
        Me.TxtPesqSubtitulo3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqSubtitulo3.Location = New System.Drawing.Point(525, 33)
        Me.TxtPesqSubtitulo3.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqSubtitulo3.Name = "TxtPesqSubtitulo3"
        Me.TxtPesqSubtitulo3.Size = New System.Drawing.Size(112, 22)
        Me.TxtPesqSubtitulo3.TabIndex = 105
        '
        'TxtPesqSubtitulo2
        '
        Me.TxtPesqSubtitulo2.BackColor = System.Drawing.SystemColors.Info
        Me.TxtPesqSubtitulo2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqSubtitulo2.Location = New System.Drawing.Point(407, 33)
        Me.TxtPesqSubtitulo2.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqSubtitulo2.Name = "TxtPesqSubtitulo2"
        Me.TxtPesqSubtitulo2.Size = New System.Drawing.Size(112, 22)
        Me.TxtPesqSubtitulo2.TabIndex = 104
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(284, 11)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 16)
        Me.Label16.TabIndex = 103
        Me.Label16.Text = "SubTítulo"
        '
        'TxtPesqSubtitulo
        '
        Me.TxtPesqSubtitulo.BackColor = System.Drawing.SystemColors.Info
        Me.TxtPesqSubtitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqSubtitulo.Location = New System.Drawing.Point(287, 33)
        Me.TxtPesqSubtitulo.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqSubtitulo.Name = "TxtPesqSubtitulo"
        Me.TxtPesqSubtitulo.Size = New System.Drawing.Size(112, 22)
        Me.TxtPesqSubtitulo.TabIndex = 102
        '
        'TxtPesgTitulo
        '
        Me.TxtPesgTitulo.BackColor = System.Drawing.SystemColors.Info
        Me.TxtPesgTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesgTitulo.Location = New System.Drawing.Point(161, 33)
        Me.TxtPesgTitulo.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesgTitulo.Name = "TxtPesgTitulo"
        Me.TxtPesgTitulo.Size = New System.Drawing.Size(116, 22)
        Me.TxtPesgTitulo.TabIndex = 101
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(157, 11)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(40, 16)
        Me.Label18.TabIndex = 99
        Me.Label18.Text = "Título"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(8, 11)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(90, 16)
        Me.Label19.TabIndex = 98
        Me.Label19.Text = "Numerto doc.:"
        '
        'TxtPesgNomeDesenho
        '
        Me.TxtPesgNomeDesenho.BackColor = System.Drawing.SystemColors.Info
        Me.TxtPesgNomeDesenho.Location = New System.Drawing.Point(9, 33)
        Me.TxtPesgNomeDesenho.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesgNomeDesenho.Name = "TxtPesgNomeDesenho"
        Me.TxtPesgNomeDesenho.Size = New System.Drawing.Size(143, 22)
        Me.TxtPesgNomeDesenho.TabIndex = 100
        '
        'dgvDesenhos
        '
        Me.dgvDesenhos.AllowUserToAddRows = False
        Me.dgvDesenhos.AllowUserToDeleteRows = False
        Me.dgvDesenhos.AllowUserToOrderColumns = True
        Me.dgvDesenhos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDesenhos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvDesenhos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDesenhos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvIcone, Me.Dgvrnc})
        Me.dgvDesenhos.Location = New System.Drawing.Point(5, 90)
        Me.dgvDesenhos.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvDesenhos.MultiSelect = False
        Me.dgvDesenhos.Name = "dgvDesenhos"
        Me.dgvDesenhos.ReadOnly = True
        Me.dgvDesenhos.RowHeadersWidth = 51
        Me.dgvDesenhos.RowTemplate.Height = 24
        Me.dgvDesenhos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDesenhos.Size = New System.Drawing.Size(887, 757)
        Me.dgvDesenhos.TabIndex = 0
        '
        'dgvIcone
        '
        Me.dgvIcone.Frozen = True
        Me.dgvIcone.HeaderText = "Tipo"
        Me.dgvIcone.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvIcone.MinimumWidth = 6
        Me.dgvIcone.Name = "dgvIcone"
        Me.dgvIcone.ReadOnly = True
        Me.dgvIcone.Width = 6
        '
        'Dgvrnc
        '
        Me.Dgvrnc.Frozen = True
        Me.Dgvrnc.HeaderText = "RNC"
        Me.Dgvrnc.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Dgvrnc.MinimumWidth = 6
        Me.Dgvrnc.Name = "Dgvrnc"
        Me.Dgvrnc.ReadOnly = True
        Me.Dgvrnc.Width = 6
        '
        'btnAtualizarCadastro
        '
        Me.btnAtualizarCadastro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAtualizarCadastro.ContextMenuStrip = Me.mnubtnListaMaterial
        Me.btnAtualizarCadastro.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.btnAtualizarCadastro.Location = New System.Drawing.Point(659, 10)
        Me.btnAtualizarCadastro.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAtualizarCadastro.Name = "btnAtualizarCadastro"
        Me.btnAtualizarCadastro.Size = New System.Drawing.Size(139, 48)
        Me.btnAtualizarCadastro.TabIndex = 106
        Me.btnAtualizarCadastro.Text = "Atualizar"
        Me.btnAtualizarCadastro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.btnAtualizarCadastro, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Esta funcionalidade percorre todos os documentos exibidos no Grid," &
        " processa a leitura dos " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "dados e realiza a atualização correspondente no banco " &
        "de dados.")
        Me.btnAtualizarCadastro.UseVisualStyleBackColor = True
        Me.btnAtualizarCadastro.Visible = False
        '
        'mnubtnListaMaterial
        '
        Me.mnubtnListaMaterial.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnubtnListaMaterial.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem})
        Me.mnubtnListaMaterial.Name = "mnubtnListaMaterial"
        Me.mnubtnListaMaterial.Size = New System.Drawing.Size(315, 28)
        '
        'InformeOTituloPadrãoDoProdutoToolStripMenuItem
        '
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem.Name = "InformeOTituloPadrãoDoProdutoToolStripMenuItem"
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem.Size = New System.Drawing.Size(314, 24)
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem.Text = "Informe o Titulo Padrão do Produto"
        '
        'tpgBom
        '
        Me.tpgBom.Controls.Add(Me.chkConverterPDF)
        Me.tpgBom.Controls.Add(Me.chkConverterDXF)
        Me.tpgBom.Controls.Add(Me.BindingNavigator2)
        Me.tpgBom.Controls.Add(Me.ProgressBarListaSW)
        Me.tpgBom.Controls.Add(Me.lblOrdemServicoAtiva)
        Me.tpgBom.Controls.Add(Me.dgvDataGridBOM)
        Me.tpgBom.Controls.Add(Me.GroupBox3)
        Me.tpgBom.Location = New System.Drawing.Point(4, 25)
        Me.tpgBom.Margin = New System.Windows.Forms.Padding(4)
        Me.tpgBom.Name = "tpgBom"
        Me.tpgBom.Padding = New System.Windows.Forms.Padding(4)
        Me.tpgBom.Size = New System.Drawing.Size(900, 851)
        Me.tpgBom.TabIndex = 1
        Me.tpgBom.Text = "BOM Lista de material"
        Me.tpgBom.UseVisualStyleBackColor = True
        '
        'chkConverterPDF
        '
        Me.chkConverterPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkConverterPDF.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf_16_161
        Me.chkConverterPDF.Location = New System.Drawing.Point(738, 6)
        Me.chkConverterPDF.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkConverterPDF.Name = "chkConverterPDF"
        Me.chkConverterPDF.Size = New System.Drawing.Size(151, 25)
        Me.chkConverterPDF.TabIndex = 9
        Me.chkConverterPDF.Text = "Converter PDF"
        Me.chkConverterPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.chkConverterPDF, "Esta opção geras nos arquivo PDF dos desenhos de detalhamento.")
        Me.chkConverterPDF.UseVisualStyleBackColor = True
        '
        'chkConverterDXF
        '
        Me.chkConverterDXF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkConverterDXF.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf_16_16
        Me.chkConverterDXF.Location = New System.Drawing.Point(567, 8)
        Me.chkConverterDXF.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkConverterDXF.Name = "chkConverterDXF"
        Me.chkConverterDXF.Size = New System.Drawing.Size(152, 23)
        Me.chkConverterDXF.TabIndex = 8
        Me.chkConverterDXF.Text = "Converter DXF"
        Me.chkConverterDXF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.chkConverterDXF, "Marque esta opção para gerar arquivos DXF, esta opção paga  os arquivo XLSD e DFT" &
        ".")
        Me.chkConverterDXF.UseVisualStyleBackColor = True
        '
        'BindingNavigator2
        '
        Me.BindingNavigator2.AddNewItem = Nothing
        Me.BindingNavigator2.CountItem = Nothing
        Me.BindingNavigator2.DeleteItem = Nothing
        Me.BindingNavigator2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.BindingNavigator2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton4, Me.ToolStripSeparator26, Me.ToolStripButton5, Me.ToolStripSeparator28, Me.ToolStripButton6, Me.ToolStripSeparator27, Me.ToolStripButton7, Me.ToolStripSeparator37, Me.TsbAtualizarBOM, Me.ToolStripSeparator38})
        Me.BindingNavigator2.Location = New System.Drawing.Point(4, 4)
        Me.BindingNavigator2.MoveFirstItem = Nothing
        Me.BindingNavigator2.MoveLastItem = Nothing
        Me.BindingNavigator2.MoveNextItem = Nothing
        Me.BindingNavigator2.MovePreviousItem = Nothing
        Me.BindingNavigator2.Name = "BindingNavigator2"
        Me.BindingNavigator2.PositionItem = Nothing
        Me.BindingNavigator2.Size = New System.Drawing.Size(892, 27)
        Me.BindingNavigator2.TabIndex = 29
        Me.BindingNavigator2.Text = "BindingNavigator2"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.CheckOnClick = True
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = Global.SwLynx_4._1.My.Resources.Resources.Limpar5
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(29, 24)
        Me.ToolStripButton4.Text = "Limpar a BOM"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton4.ToolTipText = "Limpa o Grid - Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Limpa o Grid da Lista de Materiais antes da leitura" &
    " da próxima lista. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "No entanto, é possível carregar várias listas para a mesma " &
    "Ordem de Serviço (OS)."
        '
        'ToolStripSeparator26
        '
        Me.ToolStripSeparator26.Name = "ToolStripSeparator26"
        Me.ToolStripSeparator26.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.CheckOnClick = True
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.SwLynx_4._1.My.Resources.Resources.ler
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(29, 24)
        Me.ToolStripButton5.Text = "Ler BOM"
        Me.ToolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton5.ToolTipText = resources.GetString("ToolStripButton5.ToolTipText")
        '
        'ToolStripSeparator28
        '
        Me.ToolStripSeparator28.Name = "ToolStripSeparator28"
        Me.ToolStripSeparator28.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.CheckOnClick = True
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = Global.SwLynx_4._1.My.Resources.Resources.processo
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(29, 24)
        Me.ToolStripButton6.Text = "Processar a Lista de material"
        Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton6.ToolTipText = resources.GetString("ToolStripButton6.ToolTipText")
        '
        'ToolStripSeparator27
        '
        Me.ToolStripSeparator27.Name = "ToolStripSeparator27"
        Me.ToolStripSeparator27.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.CheckOnClick = True
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = Global.SwLynx_4._1.My.Resources.Resources.ordem_de_servico
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(29, 24)
        Me.ToolStripButton7.Text = "Inserir Lista de material na Ordem de Serviço"
        Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton7.ToolTipText = "Inserir na Ordem de Serviço - Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Insira na Ordem de Serviço (OS) a li" &
    "sta de materiais carregada no Grid. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Neste ponto, é necessário informar o fator" &
    " multiplicador."
        '
        'ToolStripSeparator37
        '
        Me.ToolStripSeparator37.Name = "ToolStripSeparator37"
        Me.ToolStripSeparator37.Size = New System.Drawing.Size(6, 27)
        '
        'TsbAtualizarBOM
        '
        Me.TsbAtualizarBOM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsbAtualizarBOM.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.TsbAtualizarBOM.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbAtualizarBOM.Name = "TsbAtualizarBOM"
        Me.TsbAtualizarBOM.Size = New System.Drawing.Size(29, 24)
        Me.TsbAtualizarBOM.Text = "Atualizar"
        Me.TsbAtualizarBOM.ToolTipText = "Clique aqui para atualizar a verofovcação dos tipos de arquivos já existentes"
        '
        'ToolStripSeparator38
        '
        Me.ToolStripSeparator38.Name = "ToolStripSeparator38"
        Me.ToolStripSeparator38.Size = New System.Drawing.Size(6, 27)
        '
        'ProgressBarListaSW
        '
        Me.ProgressBarListaSW.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBarListaSW.Location = New System.Drawing.Point(5, 81)
        Me.ProgressBarListaSW.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ProgressBarListaSW.Name = "ProgressBarListaSW"
        Me.ProgressBarListaSW.Size = New System.Drawing.Size(885, 27)
        Me.ProgressBarListaSW.TabIndex = 12
        '
        'lblOrdemServicoAtiva
        '
        Me.lblOrdemServicoAtiva.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOrdemServicoAtiva.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblOrdemServicoAtiva.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrdemServicoAtiva.Location = New System.Drawing.Point(5, 44)
        Me.lblOrdemServicoAtiva.Name = "lblOrdemServicoAtiva"
        Me.lblOrdemServicoAtiva.Size = New System.Drawing.Size(885, 34)
        Me.lblOrdemServicoAtiva.TabIndex = 10
        Me.lblOrdemServicoAtiva.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTipAjuda.SetToolTip(Me.lblOrdemServicoAtiva, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Exibe o nome do arquivo atual.")
        '
        'dgvDataGridBOM
        '
        Me.dgvDataGridBOM.AllowUserToAddRows = False
        Me.dgvDataGridBOM.AllowUserToDeleteRows = False
        Me.dgvDataGridBOM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDataGridBOM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvDataGridBOM.ColumnHeadersHeight = 29
        Me.dgvDataGridBOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvDataGridBOM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DGVIconeLXDS, Me.DGVIconeDXF, Me.dgvIconePDF})
        Me.dgvDataGridBOM.ContextMenuStrip = Me.mnudgvDataGridBOM
        Me.dgvDataGridBOM.Location = New System.Drawing.Point(5, 114)
        Me.dgvDataGridBOM.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvDataGridBOM.MultiSelect = False
        Me.dgvDataGridBOM.Name = "dgvDataGridBOM"
        Me.dgvDataGridBOM.ReadOnly = True
        Me.dgvDataGridBOM.RowHeadersWidth = 51
        Me.dgvDataGridBOM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDataGridBOM.Size = New System.Drawing.Size(884, 729)
        Me.dgvDataGridBOM.TabIndex = 0
        '
        'DGVIconeLXDS
        '
        Me.DGVIconeLXDS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DGVIconeLXDS.Frozen = True
        Me.DGVIconeLXDS.HeaderText = "LXDS"
        Me.DGVIconeLXDS.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DGVIconeLXDS.MinimumWidth = 6
        Me.DGVIconeLXDS.Name = "DGVIconeLXDS"
        Me.DGVIconeLXDS.ReadOnly = True
        Me.DGVIconeLXDS.Width = 50
        '
        'DGVIconeDXF
        '
        Me.DGVIconeDXF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DGVIconeDXF.Frozen = True
        Me.DGVIconeDXF.HeaderText = "DXF"
        Me.DGVIconeDXF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DGVIconeDXF.MinimumWidth = 6
        Me.DGVIconeDXF.Name = "DGVIconeDXF"
        Me.DGVIconeDXF.ReadOnly = True
        Me.DGVIconeDXF.Width = 50
        '
        'dgvIconePDF
        '
        Me.dgvIconePDF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.dgvIconePDF.Frozen = True
        Me.dgvIconePDF.HeaderText = "PDF"
        Me.dgvIconePDF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvIconePDF.MinimumWidth = 6
        Me.dgvIconePDF.Name = "dgvIconePDF"
        Me.dgvIconePDF.ReadOnly = True
        Me.dgvIconePDF.Width = 50
        '
        'mnudgvDataGridBOM
        '
        Me.mnudgvDataGridBOM.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvDataGridBOM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator34, Me.ToolStripMenuItem3, Me.ToolStripSeparator33, Me.ToolStripMenuItem2, Me.ToolStripSeparator35, Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator36})
        Me.mnudgvDataGridBOM.Name = "mnudgvDataGridBOM"
        Me.mnudgvDataGridBOM.Size = New System.Drawing.Size(300, 106)
        '
        'ToolStripSeparator34
        '
        Me.ToolStripSeparator34.Name = "ToolStripSeparator34"
        Me.ToolStripSeparator34.Size = New System.Drawing.Size(296, 6)
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(299, 26)
        Me.ToolStripMenuItem3.Text = "Abrir PDF da Linha Selecionada"
        '
        'ToolStripSeparator33
        '
        Me.ToolStripSeparator33.Name = "ToolStripSeparator33"
        Me.ToolStripSeparator33.Size = New System.Drawing.Size(296, 6)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.SwLynx_4._1.My.Resources.Resources.arquivo_dxf
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(299, 26)
        Me.ToolStripMenuItem2.Text = "Abrir DXF da Linha Selecionada"
        '
        'ToolStripSeparator35
        '
        Me.ToolStripSeparator35.Name = "ToolStripSeparator35"
        Me.ToolStripSeparator35.Size = New System.Drawing.Size(296, 6)
        '
        'AbrirLXDSDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.CYPCUT
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirLXDSDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(299, 26)
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir LXDS da Linha Selecionada"
        '
        'ToolStripSeparator36
        '
        Me.ToolStripSeparator36.Name = "ToolStripSeparator36"
        Me.ToolStripSeparator36.Size = New System.Drawing.Size(296, 6)
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.btnConverterListaDXFPDF)
        Me.GroupBox3.Controls.Add(Me.btnListaMaterial)
        Me.GroupBox3.Controls.Add(Me.btnLimparBom)
        Me.GroupBox3.Controls.Add(Me.btnInserirItensOrdemServico)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 144)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(908, 82)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Leitura  da BOM"
        Me.GroupBox3.Visible = False
        '
        'btnConverterListaDXFPDF
        '
        Me.btnConverterListaDXFPDF.Location = New System.Drawing.Point(431, 21)
        Me.btnConverterListaDXFPDF.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnConverterListaDXFPDF.Name = "btnConverterListaDXFPDF"
        Me.btnConverterListaDXFPDF.Size = New System.Drawing.Size(145, 46)
        Me.btnConverterListaDXFPDF.TabIndex = 14
        Me.btnConverterListaDXFPDF.Text = "Processar a Lista BOM"
        Me.btnConverterListaDXFPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConverterListaDXFPDF.UseVisualStyleBackColor = True
        '
        'btnListaMaterial
        '
        Me.btnListaMaterial.Location = New System.Drawing.Point(157, 21)
        Me.btnListaMaterial.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnListaMaterial.Name = "btnListaMaterial"
        Me.btnListaMaterial.Size = New System.Drawing.Size(145, 46)
        Me.btnListaMaterial.TabIndex = 5
        Me.btnListaMaterial.Text = "Ler BOM"
        Me.btnListaMaterial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnListaMaterial.UseVisualStyleBackColor = True
        '
        'btnLimparBom
        '
        Me.btnLimparBom.Location = New System.Drawing.Point(5, 21)
        Me.btnLimparBom.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnLimparBom.Name = "btnLimparBom"
        Me.btnLimparBom.Size = New System.Drawing.Size(145, 46)
        Me.btnLimparBom.TabIndex = 6
        Me.btnLimparBom.Text = "Limpar BOM"
        Me.btnLimparBom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLimparBom.UseVisualStyleBackColor = True
        '
        'btnInserirItensOrdemServico
        '
        Me.btnInserirItensOrdemServico.Location = New System.Drawing.Point(581, 21)
        Me.btnInserirItensOrdemServico.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInserirItensOrdemServico.Name = "btnInserirItensOrdemServico"
        Me.btnInserirItensOrdemServico.Size = New System.Drawing.Size(145, 46)
        Me.btnInserirItensOrdemServico.TabIndex = 11
        Me.btnInserirItensOrdemServico.Text = "Inserir Itens na OS :"
        Me.btnInserirItensOrdemServico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnInserirItensOrdemServico.UseVisualStyleBackColor = True
        '
        'tpgOrdemServico
        '
        Me.tpgOrdemServico.Controls.Add(Me.BindingNavigator1)
        Me.tpgOrdemServico.Controls.Add(Me.txtPesqAcabamentoDesenho)
        Me.tpgOrdemServico.Controls.Add(Me.Label29)
        Me.tpgOrdemServico.Controls.Add(Me.txtPesqTipoDesenho)
        Me.tpgOrdemServico.Controls.Add(Me.txtPesqCriadoPor)
        Me.tpgOrdemServico.Controls.Add(Me.Label28)
        Me.tpgOrdemServico.Controls.Add(Me.txtPesqNumeroDesenho)
        Me.tpgOrdemServico.Controls.Add(Me.Label30)
        Me.tpgOrdemServico.Controls.Add(Me.Label27)
        Me.tpgOrdemServico.Controls.Add(Me.cboOpcoesAcabamento)
        Me.tpgOrdemServico.Controls.Add(Me.Label26)
        Me.tpgOrdemServico.Controls.Add(Me.ProgressBarProcessoLiberacaoOrdemServico)
        Me.tpgOrdemServico.Controls.Add(Me.txtDescricao)
        Me.tpgOrdemServico.Controls.Add(Me.Label25)
        Me.tpgOrdemServico.Controls.Add(Me.DGVListaMaterialSW)
        Me.tpgOrdemServico.Controls.Add(Me.chkMostraLiberadasPelaEngenharia)
        Me.tpgOrdemServico.Controls.Add(Me.dgvos)
        Me.tpgOrdemServico.Controls.Add(Me.txtDescricaoTag)
        Me.tpgOrdemServico.Controls.Add(Me.txtCliente)
        Me.tpgOrdemServico.Controls.Add(Me.cboTag)
        Me.tpgOrdemServico.Controls.Add(Me.Label24)
        Me.tpgOrdemServico.Controls.Add(Me.cboProjeto)
        Me.tpgOrdemServico.Controls.Add(Me.Label20)
        Me.tpgOrdemServico.Controls.Add(Me.DGVListaMaterialSWMaterial)
        Me.tpgOrdemServico.Controls.Add(Me.btnAplicarAcabamento)
        Me.tpgOrdemServico.Location = New System.Drawing.Point(4, 25)
        Me.tpgOrdemServico.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tpgOrdemServico.Name = "tpgOrdemServico"
        Me.tpgOrdemServico.Size = New System.Drawing.Size(900, 851)
        Me.tpgOrdemServico.TabIndex = 3
        Me.tpgOrdemServico.Text = "Ordem de Serviço"
        Me.tpgOrdemServico.UseVisualStyleBackColor = True
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.CountItem = Nothing
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.TSBSalvarOrdemServico, Me.ToolStripButton3})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator1.MoveFirstItem = Nothing
        Me.BindingNavigator1.MoveLastItem = Nothing
        Me.BindingNavigator1.MoveNextItem = Nothing
        Me.BindingNavigator1.MovePreviousItem = Nothing
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Nothing
        Me.BindingNavigator1.Size = New System.Drawing.Size(900, 27)
        Me.BindingNavigator1.TabIndex = 28
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.CheckOnClick = True
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.SwLynx_4._1.My.Resources.Resources.novo_arquivo
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(29, 24)
        Me.ToolStripButton1.Text = "Nova OS"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Clique aqui para criar uma nova Ordem de Serviço (OS)."
        '
        'TSBSalvarOrdemServico
        '
        Me.TSBSalvarOrdemServico.CheckOnClick = True
        Me.TSBSalvarOrdemServico.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBSalvarOrdemServico.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.TSBSalvarOrdemServico.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBSalvarOrdemServico.Name = "TSBSalvarOrdemServico"
        Me.TSBSalvarOrdemServico.Size = New System.Drawing.Size(29, 24)
        Me.TSBSalvarOrdemServico.Text = "Salvar"
        Me.TSBSalvarOrdemServico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSBSalvarOrdemServico.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Após selecionar o Projeto e a Tag, clique aqui para salvar uma nov" &
    "a Ordem de Serviço (OS)."
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.CheckOnClick = True
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(29, 24)
        Me.ToolStripButton3.Text = "Atualizar"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton3.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Caso um novo cadastro de Projeto e/ou Tag seja inserido e não " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ap" &
    "areça no ComboBox, basta clicar aqui para atualizar os dados."
        '
        'txtPesqAcabamentoDesenho
        '
        Me.txtPesqAcabamentoDesenho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqAcabamentoDesenho.Location = New System.Drawing.Point(435, 574)
        Me.txtPesqAcabamentoDesenho.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqAcabamentoDesenho.Name = "txtPesqAcabamentoDesenho"
        Me.txtPesqAcabamentoDesenho.Size = New System.Drawing.Size(211, 22)
        Me.txtPesqAcabamentoDesenho.TabIndex = 23
        Me.ToolTipAjuda.SetToolTip(Me.txtPesqAcabamentoDesenho, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(432, 551)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(118, 16)
        Me.Label29.TabIndex = 22
        Me.Label29.Text = "Tipo Acabamento:"
        Me.ToolTipAjuda.SetToolTip(Me.Label29, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'txtPesqTipoDesenho
        '
        Me.txtPesqTipoDesenho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqTipoDesenho.Location = New System.Drawing.Point(216, 574)
        Me.txtPesqTipoDesenho.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqTipoDesenho.Name = "txtPesqTipoDesenho"
        Me.txtPesqTipoDesenho.Size = New System.Drawing.Size(211, 22)
        Me.txtPesqTipoDesenho.TabIndex = 21
        Me.ToolTipAjuda.SetToolTip(Me.txtPesqTipoDesenho, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'txtPesqCriadoPor
        '
        Me.txtPesqCriadoPor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCriadoPor.Location = New System.Drawing.Point(355, 206)
        Me.txtPesqCriadoPor.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqCriadoPor.Name = "txtPesqCriadoPor"
        Me.txtPesqCriadoPor.Size = New System.Drawing.Size(168, 22)
        Me.txtPesqCriadoPor.TabIndex = 25
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(215, 551)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(96, 16)
        Me.Label28.TabIndex = 20
        Me.Label28.Text = "Tipo Desenho:"
        Me.ToolTipAjuda.SetToolTip(Me.Label28, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'txtPesqNumeroDesenho
        '
        Me.txtPesqNumeroDesenho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqNumeroDesenho.Location = New System.Drawing.Point(11, 574)
        Me.txtPesqNumeroDesenho.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqNumeroDesenho.Name = "txtPesqNumeroDesenho"
        Me.txtPesqNumeroDesenho.Size = New System.Drawing.Size(196, 22)
        Me.txtPesqNumeroDesenho.TabIndex = 19
        Me.ToolTipAjuda.SetToolTip(Me.txtPesqNumeroDesenho, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(275, 208)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(74, 16)
        Me.Label30.TabIndex = 24
        Me.Label30.Text = "Criado Por:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(11, 551)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(116, 16)
        Me.Label27.TabIndex = 18
        Me.Label27.Text = "Numero Desenho:"
        Me.ToolTipAjuda.SetToolTip(Me.Label27, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'cboOpcoesAcabamento
        '
        Me.cboOpcoesAcabamento.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOpcoesAcabamento.FormattingEnabled = True
        Me.cboOpcoesAcabamento.Location = New System.Drawing.Point(104, 518)
        Me.cboOpcoesAcabamento.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboOpcoesAcabamento.Name = "cboOpcoesAcabamento"
        Me.cboOpcoesAcabamento.Size = New System.Drawing.Size(532, 24)
        Me.cboOpcoesAcabamento.TabIndex = 18
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(11, 522)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(87, 16)
        Me.Label26.TabIndex = 17
        Me.Label26.Text = "Acabamento:"
        '
        'ProgressBarProcessoLiberacaoOrdemServico
        '
        Me.ProgressBarProcessoLiberacaoOrdemServico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBarProcessoLiberacaoOrdemServico.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ProgressBarProcessoLiberacaoOrdemServico.Location = New System.Drawing.Point(11, 231)
        Me.ProgressBarProcessoLiberacaoOrdemServico.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ProgressBarProcessoLiberacaoOrdemServico.Name = "ProgressBarProcessoLiberacaoOrdemServico"
        Me.ProgressBarProcessoLiberacaoOrdemServico.Size = New System.Drawing.Size(868, 26)
        Me.ProgressBarProcessoLiberacaoOrdemServico.TabIndex = 15
        '
        'txtDescricao
        '
        Me.txtDescricao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.Location = New System.Drawing.Point(77, 132)
        Me.txtDescricao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtDescricao.MaxLength = 200
        Me.txtDescricao.Multiline = True
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(801, 61)
        Me.txtDescricao.TabIndex = 13
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(8, 135)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 16)
        Me.Label25.TabIndex = 12
        Me.Label25.Text = "Descrição:"
        '
        'DGVListaMaterialSW
        '
        Me.DGVListaMaterialSW.AllowUserToAddRows = False
        Me.DGVListaMaterialSW.AllowUserToDeleteRows = False
        Me.DGVListaMaterialSW.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVListaMaterialSW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.DGVListaMaterialSW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVListaMaterialSW.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvSelecao, Me.dgvIconeItemOS, Me.dgvDXF, Me.dgvPDF})
        Me.DGVListaMaterialSW.ContextMenuStrip = Me.mnuDGVListaMaterialSW
        Me.DGVListaMaterialSW.Location = New System.Drawing.Point(11, 603)
        Me.DGVListaMaterialSW.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGVListaMaterialSW.Name = "DGVListaMaterialSW"
        Me.DGVListaMaterialSW.ReadOnly = True
        Me.DGVListaMaterialSW.RowHeadersWidth = 51
        Me.DGVListaMaterialSW.RowTemplate.Height = 24
        Me.DGVListaMaterialSW.Size = New System.Drawing.Size(868, 246)
        Me.DGVListaMaterialSW.TabIndex = 9
        '
        'dgvSelecao
        '
        Me.dgvSelecao.Frozen = True
        Me.dgvSelecao.HeaderText = "dgvSelecao"
        Me.dgvSelecao.MinimumWidth = 6
        Me.dgvSelecao.Name = "dgvSelecao"
        Me.dgvSelecao.ReadOnly = True
        Me.dgvSelecao.Width = 6
        '
        'dgvIconeItemOS
        '
        Me.dgvIconeItemOS.Frozen = True
        Me.dgvIconeItemOS.HeaderText = "dgvIconeItemOS"
        Me.dgvIconeItemOS.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvIconeItemOS.MinimumWidth = 6
        Me.dgvIconeItemOS.Name = "dgvIconeItemOS"
        Me.dgvIconeItemOS.ReadOnly = True
        Me.dgvIconeItemOS.Width = 6
        '
        'dgvDXF
        '
        Me.dgvDXF.HeaderText = "DXF"
        Me.dgvDXF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvDXF.MinimumWidth = 6
        Me.dgvDXF.Name = "dgvDXF"
        Me.dgvDXF.ReadOnly = True
        Me.dgvDXF.Width = 6
        '
        'dgvPDF
        '
        Me.dgvPDF.HeaderText = "PDF"
        Me.dgvPDF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvPDF.MinimumWidth = 6
        Me.dgvPDF.Name = "dgvPDF"
        Me.dgvPDF.ReadOnly = True
        Me.dgvPDF.Width = 6
        '
        'mnuDGVListaMaterialSW
        '
        Me.mnuDGVListaMaterialSW.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuDGVListaMaterialSW.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator2, Me.MarcarTodosToolStripMenuItem, Me.DesmarcarTodosToolStripMenuItem, Me.InverterSeleçãoToolStripMenuItem, Me.ToolStripSeparator6, Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator1, Me.ToolStripSeparator15, Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem, Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator3, Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator5, Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem, Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem, Me.ToolStripSeparator7, Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator8, Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem, Me.ToolStripSeparator18, Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem, Me.ToolStripSeparator19, Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem})
        Me.mnuDGVListaMaterialSW.Name = "ContextMenuStrip1"
        Me.mnuDGVListaMaterialSW.Size = New System.Drawing.Size(500, 402)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(496, 6)
        '
        'MarcarTodosToolStripMenuItem
        '
        Me.MarcarTodosToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.marcado
        Me.MarcarTodosToolStripMenuItem.Name = "MarcarTodosToolStripMenuItem"
        Me.MarcarTodosToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.MarcarTodosToolStripMenuItem.Text = "Marcar Todos"
        '
        'DesmarcarTodosToolStripMenuItem
        '
        Me.DesmarcarTodosToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.desmarcado
        Me.DesmarcarTodosToolStripMenuItem.Name = "DesmarcarTodosToolStripMenuItem"
        Me.DesmarcarTodosToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.DesmarcarTodosToolStripMenuItem.Text = "Desmarcar Todos"
        '
        'InverterSeleçãoToolStripMenuItem
        '
        Me.InverterSeleçãoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.alterar
        Me.InverterSeleçãoToolStripMenuItem.Name = "InverterSeleçãoToolStripMenuItem"
        Me.InverterSeleçãoToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.InverterSeleçãoToolStripMenuItem.Text = "Inverter Seleção"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(496, 6)
        '
        'AbrirPDFDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirPDFDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir PDF da Linha Selecionada"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(496, 6)
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(496, 6)
        '
        'AbrirDXFDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirDXFDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir DXF da Linha Selecionada"
        '
        'AbrirDWRDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirDWRDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir DRW da Linha Selecionada"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(496, 6)
        '
        'ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem
        '
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.excluir
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Name = "ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem"
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Text = "Excluir o Documento da Linha Selecionada"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(496, 6)
        '
        'MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem
        '
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.IconeswPrincipal
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Name = "MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem"
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Text = "Marcar como Conjunto Principal da Ordem de Serviço"
        '
        'DesmarcarComoConjuntoPrincipalToolStripMenuItem
        '
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.IcopneMontagemSW
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Name = "DesmarcarComoConjuntoPrincipalToolStripMenuItem"
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Text = "Desmarcar Como Conjunto Principal da Ordem de Serviço"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(496, 6)
        '
        'AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.multiply
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Name = "AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem"
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Text = "Alterar a quantidade de peças/Fabricação da linha selecionada"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(496, 6)
        '
        'ImprimirDesenhoPDFSelecionadoToolStripMenuItem
        '
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Name = "ImprimirDesenhoPDFSelecionadoToolStripMenuItem"
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Text = "Selecione as Linhas para impressão dos arquivos em PDF"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(496, 6)
        '
        'GerarPDFDasLinhasSelecionadasToolStripMenuItem
        '
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem.Name = "GerarPDFDasLinhasSelecionadasToolStripMenuItem"
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem.Text = "Gerar PDF das Linhas Selecionadas"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(496, 6)
        '
        'GerarDXFDasLinhasSelecionadasToolStripMenuItem
        '
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem.Name = "GerarDXFDasLinhasSelecionadasToolStripMenuItem"
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem.Size = New System.Drawing.Size(499, 26)
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem.Text = "Gerar DXF das Linhas Selecionadas"
        '
        'chkMostraLiberadasPelaEngenharia
        '
        Me.chkMostraLiberadasPelaEngenharia.AutoSize = True
        Me.chkMostraLiberadasPelaEngenharia.Location = New System.Drawing.Point(13, 207)
        Me.chkMostraLiberadasPelaEngenharia.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkMostraLiberadasPelaEngenharia.Name = "chkMostraLiberadasPelaEngenharia"
        Me.chkMostraLiberadasPelaEngenharia.Size = New System.Drawing.Size(251, 20)
        Me.chkMostraLiberadasPelaEngenharia.TabIndex = 8
        Me.chkMostraLiberadasPelaEngenharia.Text = "Mostar OS Liberada pela Engenharia"
        Me.ToolTipAjuda.SetToolTip(Me.chkMostraLiberadasPelaEngenharia, resources.GetString("chkMostraLiberadasPelaEngenharia.ToolTip"))
        Me.chkMostraLiberadasPelaEngenharia.UseVisualStyleBackColor = True
        '
        'dgvos
        '
        Me.dgvos.AllowUserToAddRows = False
        Me.dgvos.AllowUserToDeleteRows = False
        Me.dgvos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvStatus})
        Me.dgvos.ContextMenuStrip = Me.mnudgvos
        Me.dgvos.Location = New System.Drawing.Point(11, 261)
        Me.dgvos.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvos.Name = "dgvos"
        Me.dgvos.ReadOnly = True
        Me.dgvos.RowHeadersWidth = 51
        Me.dgvos.RowTemplate.Height = 24
        Me.dgvos.Size = New System.Drawing.Size(868, 254)
        Me.dgvos.TabIndex = 7
        '
        'dgvStatus
        '
        Me.dgvStatus.Frozen = True
        Me.dgvStatus.HeaderText = "Status"
        Me.dgvStatus.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvStatus.MinimumWidth = 6
        Me.dgvStatus.Name = "dgvStatus"
        Me.dgvStatus.ReadOnly = True
        Me.dgvStatus.Width = 6
        '
        'mnudgvos
        '
        Me.mnudgvos.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem, Me.ToolStripSeparator4, Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem, Me.CancelarLiberaçãoDaOSToolStripMenuItem, Me.ToolStripSeparator9, Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem, Me.ToolStripSeparator10, Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem, Me.ToolStripSeparator14, Me.GeralExcelDaOSToolStripMenuItem, Me.ToolStripSeparator11, Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem, Me.ToolStripSeparator17, Me.CancelarAFabricaçãoDaOSToolStripMenuItem, Me.ToolStripSeparator12, Me.GerarArquivoEmDXFToolStripMenuItem, Me.ToolStripSeparator13, Me.GerarArquivoEmPDFToolStripMenuItem, Me.ToolStripSeparator16, Me.ToolStripSeparator20, Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem, Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem})
        Me.mnudgvos.Name = "mnudgvos"
        Me.mnudgvos.Size = New System.Drawing.Size(544, 376)
        '
        'AbrirPastaDaOrdemDeServiçoToolStripMenuItem
        '
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pasta
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Name = "AbrirPastaDaOrdemDeServiçoToolStripMenuItem"
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Text = "Abrir Pasta da Ordem de Serviço"
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.ToolTipText = "Abre a Pasta da OS Selecionada!"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(540, 6)
        '
        'LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem
        '
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.verificado
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Name = "LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem"
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Text = "Liberar Ordem de Serviço para Produção"
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.ToolTipText = "Aqui você libera a OS para o processo de fabricação pelo SINCO e inportas os arqu" &
    "ivo em PDF's e DXF's para as pastas da OS."
        '
        'CancelarLiberaçãoDaOSToolStripMenuItem
        '
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.cancelar
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Name = "CancelarLiberaçãoDaOSToolStripMenuItem"
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Text = "Cancelar Liberação da Ordem de Serviço"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(540, 6)
        '
        'AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem
        '
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Name = "AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem"
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Text = "Atualizar PDF's, DXF's/LXDS's e DTF's  na pasta da OS"
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.ToolTipText = "Atenção: Esta função exclui todos os arquivos da OS e os atualizas com os documen" &
    "to da lista de materiais da OS."
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(540, 6)
        '
        'AlterarOFatorMultipçlicadorDaOSToolStripMenuItem
        '
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.multiply
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Name = "AlterarOFatorMultipçlicadorDaOSToolStripMenuItem"
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Text = "Alterar o Fator Multiplicador da OS"
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.ToolTipText = "A alteração do Fator multiplicador irá ajustar todas as quantidades de totas as p" &
    "eças da OS, é com isso irá atualizar todos os arquivos da pasta da OS."
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(540, 6)
        '
        'GeralExcelDaOSToolStripMenuItem
        '
        Me.GeralExcelDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.icons8_exportação_excel_48
        Me.GeralExcelDaOSToolStripMenuItem.Name = "GeralExcelDaOSToolStripMenuItem"
        Me.GeralExcelDaOSToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.GeralExcelDaOSToolStripMenuItem.Text = "Gerar Excel da OS sem Liberação para  Produção"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(540, 6)
        '
        'LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem
        '
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.Limpar5
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Name = "LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem"
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Text = "Limpar OS"
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.ToolTipText = "Esta função limpar todos os desenhos da OS e excluir todos os arquivos de desenho" &
    "s da pasta da OS."
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(540, 6)
        '
        'CancelarAFabricaçãoDaOSToolStripMenuItem
        '
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.excluir
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Name = "CancelarAFabricaçãoDaOSToolStripMenuItem"
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Text = "Excluir Ordem de Serviço"
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.ToolTipText = "Cancela todos o processo de produção da OS."
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(540, 6)
        '
        'GerarArquivoEmDXFToolStripMenuItem
        '
        Me.GerarArquivoEmDXFToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf
        Me.GerarArquivoEmDXFToolStripMenuItem.Name = "GerarArquivoEmDXFToolStripMenuItem"
        Me.GerarArquivoEmDXFToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.GerarArquivoEmDXFToolStripMenuItem.Text = "Gerar arquivo em DXF"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(540, 6)
        '
        'GerarArquivoEmPDFToolStripMenuItem
        '
        Me.GerarArquivoEmPDFToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.GerarArquivoEmPDFToolStripMenuItem.Name = "GerarArquivoEmPDFToolStripMenuItem"
        Me.GerarArquivoEmPDFToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.GerarArquivoEmPDFToolStripMenuItem.Text = "Gerar arquivo em PDF"
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(540, 6)
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(540, 6)
        '
        'CriarUmCopiaDaOSSelecionadaToolStripMenuItem
        '
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Name = "CriarUmCopiaDaOSSelecionadaToolStripMenuItem"
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Text = "Criar uma copia da OS Selecionada"
        '
        'TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem
        '
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.produtos_32x32
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Name = "TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem"
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Size = New System.Drawing.Size(543, 26)
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Text = "Transformar esta Ordem de Serviço em Referencia de produto Padrão"
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.ToolTipText = resources.GetString("TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.ToolTip" &
        "Text")
        '
        'txtDescricaoTag
        '
        Me.txtDescricaoTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricaoTag.Enabled = False
        Me.txtDescricaoTag.Location = New System.Drawing.Point(355, 66)
        Me.txtDescricaoTag.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtDescricaoTag.Multiline = True
        Me.txtDescricaoTag.Name = "txtDescricaoTag"
        Me.txtDescricaoTag.Size = New System.Drawing.Size(524, 61)
        Me.txtDescricaoTag.TabIndex = 6
        '
        'txtCliente
        '
        Me.txtCliente.Enabled = False
        Me.txtCliente.Location = New System.Drawing.Point(77, 66)
        Me.txtCliente.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCliente.Multiline = True
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(231, 61)
        Me.txtCliente.TabIndex = 5
        '
        'cboTag
        '
        Me.cboTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTag.FormattingEnabled = True
        Me.cboTag.Location = New System.Drawing.Point(355, 38)
        Me.cboTag.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboTag.Name = "cboTag"
        Me.cboTag.Size = New System.Drawing.Size(524, 24)
        Me.cboTag.TabIndex = 4
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(315, 42)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(35, 16)
        Me.Label24.TabIndex = 3
        Me.Label24.Text = "Tag:"
        '
        'cboProjeto
        '
        Me.cboProjeto.FormattingEnabled = True
        Me.cboProjeto.Location = New System.Drawing.Point(77, 38)
        Me.cboProjeto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboProjeto.Name = "cboProjeto"
        Me.cboProjeto.Size = New System.Drawing.Size(231, 24)
        Me.cboProjeto.TabIndex = 2
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(27, 42)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 16)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Projeto:"
        '
        'DGVListaMaterialSWMaterial
        '
        Me.DGVListaMaterialSWMaterial.AllowUserToAddRows = False
        Me.DGVListaMaterialSWMaterial.AllowUserToDeleteRows = False
        Me.DGVListaMaterialSWMaterial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVListaMaterialSWMaterial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.DGVListaMaterialSWMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVListaMaterialSWMaterial.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewImageColumn2})
        Me.DGVListaMaterialSWMaterial.Location = New System.Drawing.Point(11, 709)
        Me.DGVListaMaterialSWMaterial.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGVListaMaterialSWMaterial.Name = "DGVListaMaterialSWMaterial"
        Me.DGVListaMaterialSWMaterial.ReadOnly = True
        Me.DGVListaMaterialSWMaterial.RowHeadersWidth = 51
        Me.DGVListaMaterialSWMaterial.RowTemplate.Height = 24
        Me.DGVListaMaterialSWMaterial.Size = New System.Drawing.Size(747, 96)
        Me.DGVListaMaterialSWMaterial.TabIndex = 16
        Me.DGVListaMaterialSWMaterial.Visible = False
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.Frozen = True
        Me.DataGridViewImageColumn2.HeaderText = "dgvIconeItemOS"
        Me.DataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn2.MinimumWidth = 6
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.ReadOnly = True
        Me.DataGridViewImageColumn2.Width = 6
        '
        'btnAplicarAcabamento
        '
        Me.btnAplicarAcabamento.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAplicarAcabamento.Location = New System.Drawing.Point(645, 518)
        Me.btnAplicarAcabamento.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAplicarAcabamento.Name = "btnAplicarAcabamento"
        Me.btnAplicarAcabamento.Size = New System.Drawing.Size(139, 43)
        Me.btnAplicarAcabamento.TabIndex = 19
        Me.btnAplicarAcabamento.Text = "Aplicar"
        Me.btnAplicarAcabamento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.btnAplicarAcabamento, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione as peças nas quais deseja aplicar o Acabamento, escolha " &
        "a opção" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "desejada no ComboBox e clique no botão Aplicar.")
        Me.btnAplicarAcabamento.UseVisualStyleBackColor = True
        '
        'tpgListaRNCPecaCorrente
        '
        Me.tpgListaRNCPecaCorrente.Controls.Add(Me.btnAtualizarDadosItemOs)
        Me.tpgListaRNCPecaCorrente.Controls.Add(Me.lblNUmeroDocumentoAtivo)
        Me.tpgListaRNCPecaCorrente.Controls.Add(Me.DGVTimerFiltroPecaAtivaOS)
        Me.tpgListaRNCPecaCorrente.Location = New System.Drawing.Point(4, 25)
        Me.tpgListaRNCPecaCorrente.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tpgListaRNCPecaCorrente.Name = "tpgListaRNCPecaCorrente"
        Me.tpgListaRNCPecaCorrente.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tpgListaRNCPecaCorrente.Size = New System.Drawing.Size(900, 851)
        Me.tpgListaRNCPecaCorrente.TabIndex = 4
        Me.tpgListaRNCPecaCorrente.Text = "Detalhe Peça Corrente"
        Me.tpgListaRNCPecaCorrente.UseVisualStyleBackColor = True
        '
        'btnAtualizarDadosItemOs
        '
        Me.btnAtualizarDadosItemOs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAtualizarDadosItemOs.Location = New System.Drawing.Point(5, 42)
        Me.btnAtualizarDadosItemOs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAtualizarDadosItemOs.Name = "btnAtualizarDadosItemOs"
        Me.btnAtualizarDadosItemOs.Size = New System.Drawing.Size(887, 49)
        Me.btnAtualizarDadosItemOs.TabIndex = 12
        Me.btnAtualizarDadosItemOs.Text = "Atualizar os itens da OSM conforme os dados originais dos desenhos."
        Me.btnAtualizarDadosItemOs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAtualizarDadosItemOs.UseVisualStyleBackColor = True
        '
        'lblNUmeroDocumentoAtivo
        '
        Me.lblNUmeroDocumentoAtivo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNUmeroDocumentoAtivo.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblNUmeroDocumentoAtivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNUmeroDocumentoAtivo.Location = New System.Drawing.Point(5, 2)
        Me.lblNUmeroDocumentoAtivo.Name = "lblNUmeroDocumentoAtivo"
        Me.lblNUmeroDocumentoAtivo.Size = New System.Drawing.Size(887, 34)
        Me.lblNUmeroDocumentoAtivo.TabIndex = 11
        Me.lblNUmeroDocumentoAtivo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DGVTimerFiltroPecaAtivaOS
        '
        Me.DGVTimerFiltroPecaAtivaOS.AllowUserToAddRows = False
        Me.DGVTimerFiltroPecaAtivaOS.AllowUserToDeleteRows = False
        Me.DGVTimerFiltroPecaAtivaOS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVTimerFiltroPecaAtivaOS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.DGVTimerFiltroPecaAtivaOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTimerFiltroPecaAtivaOS.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvSelecaoAtualizacaoItemOs, Me.dgvTipoDesenhoAtualizacaoItemOs})
        Me.DGVTimerFiltroPecaAtivaOS.Location = New System.Drawing.Point(5, 96)
        Me.DGVTimerFiltroPecaAtivaOS.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGVTimerFiltroPecaAtivaOS.Name = "DGVTimerFiltroPecaAtivaOS"
        Me.DGVTimerFiltroPecaAtivaOS.ReadOnly = True
        Me.DGVTimerFiltroPecaAtivaOS.RowHeadersWidth = 51
        Me.DGVTimerFiltroPecaAtivaOS.RowTemplate.Height = 24
        Me.DGVTimerFiltroPecaAtivaOS.Size = New System.Drawing.Size(887, 751)
        Me.DGVTimerFiltroPecaAtivaOS.TabIndex = 0
        '
        'dgvSelecaoAtualizacaoItemOs
        '
        Me.dgvSelecaoAtualizacaoItemOs.Frozen = True
        Me.dgvSelecaoAtualizacaoItemOs.HeaderText = "dgvSelecaoAtualizacaoItemOs"
        Me.dgvSelecaoAtualizacaoItemOs.MinimumWidth = 6
        Me.dgvSelecaoAtualizacaoItemOs.Name = "dgvSelecaoAtualizacaoItemOs"
        Me.dgvSelecaoAtualizacaoItemOs.ReadOnly = True
        Me.dgvSelecaoAtualizacaoItemOs.Width = 25
        '
        'dgvTipoDesenhoAtualizacaoItemOs
        '
        Me.dgvTipoDesenhoAtualizacaoItemOs.Frozen = True
        Me.dgvTipoDesenhoAtualizacaoItemOs.HeaderText = "dgvTipoDesenhoAtualizacaoItemOs"
        Me.dgvTipoDesenhoAtualizacaoItemOs.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvTipoDesenhoAtualizacaoItemOs.MinimumWidth = 6
        Me.dgvTipoDesenhoAtualizacaoItemOs.Name = "dgvTipoDesenhoAtualizacaoItemOs"
        Me.dgvTipoDesenhoAtualizacaoItemOs.ReadOnly = True
        Me.dgvTipoDesenhoAtualizacaoItemOs.Width = 25
        '
        'tpgPCP
        '
        Me.tpgPCP.Controls.Add(Me.dgvTimerpcpAgrupamentoProjetoDetalhamento)
        Me.tpgPCP.Controls.Add(Me.txtClientepcp)
        Me.tpgPCP.Controls.Add(Me.cboProjetoPCP)
        Me.tpgPCP.Controls.Add(Me.Label9)
        Me.tpgPCP.Controls.Add(Me.dgvTimerpcpAgrupamentoProjeto)
        Me.tpgPCP.Location = New System.Drawing.Point(4, 25)
        Me.tpgPCP.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tpgPCP.Name = "tpgPCP"
        Me.tpgPCP.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tpgPCP.Size = New System.Drawing.Size(900, 851)
        Me.tpgPCP.TabIndex = 5
        Me.tpgPCP.Text = "Visão do PCP"
        Me.tpgPCP.UseVisualStyleBackColor = True
        '
        'dgvTimerpcpAgrupamentoProjetoDetalhamento
        '
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.AllowUserToAddRows = False
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.AllowUserToDeleteRows = False
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.AllowUserToOrderColumns = True
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.Location = New System.Drawing.Point(5, 127)
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.Name = "dgvTimerpcpAgrupamentoProjetoDetalhamento"
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.ReadOnly = True
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.RowHeadersWidth = 51
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.RowTemplate.Height = 24
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.Size = New System.Drawing.Size(887, 718)
        Me.dgvTimerpcpAgrupamentoProjetoDetalhamento.TabIndex = 9
        '
        'txtClientepcp
        '
        Me.txtClientepcp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtClientepcp.Enabled = False
        Me.txtClientepcp.Location = New System.Drawing.Point(301, 14)
        Me.txtClientepcp.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtClientepcp.Name = "txtClientepcp"
        Me.txtClientepcp.Size = New System.Drawing.Size(591, 22)
        Me.txtClientepcp.TabIndex = 8
        '
        'cboProjetoPCP
        '
        Me.cboProjetoPCP.FormattingEnabled = True
        Me.cboProjetoPCP.Location = New System.Drawing.Point(65, 14)
        Me.cboProjetoPCP.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboProjetoPCP.Name = "cboProjetoPCP"
        Me.cboProjetoPCP.Size = New System.Drawing.Size(231, 24)
        Me.cboProjetoPCP.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 16)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Projeto:"
        '
        'dgvTimerpcpAgrupamentoProjeto
        '
        Me.dgvTimerpcpAgrupamentoProjeto.AllowUserToAddRows = False
        Me.dgvTimerpcpAgrupamentoProjeto.AllowUserToDeleteRows = False
        Me.dgvTimerpcpAgrupamentoProjeto.AllowUserToOrderColumns = True
        Me.dgvTimerpcpAgrupamentoProjeto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTimerpcpAgrupamentoProjeto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTimerpcpAgrupamentoProjeto.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvTimerpcpAgrupamentoProjeto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimerpcpAgrupamentoProjeto.Location = New System.Drawing.Point(5, 42)
        Me.dgvTimerpcpAgrupamentoProjeto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvTimerpcpAgrupamentoProjeto.Name = "dgvTimerpcpAgrupamentoProjeto"
        Me.dgvTimerpcpAgrupamentoProjeto.ReadOnly = True
        Me.dgvTimerpcpAgrupamentoProjeto.RowHeadersWidth = 51
        Me.dgvTimerpcpAgrupamentoProjeto.RowTemplate.Height = 24
        Me.dgvTimerpcpAgrupamentoProjeto.Size = New System.Drawing.Size(887, 79)
        Me.dgvTimerpcpAgrupamentoProjeto.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label34)
        Me.TabPage1.Controls.Add(Me.Label33)
        Me.TabPage1.Controls.Add(Me.dgvTimerProdutosItens)
        Me.TabPage1.Controls.Add(Me.txtPesqCodOmie4)
        Me.TabPage1.Controls.Add(Me.txPesqCodDesenhoProduto4)
        Me.TabPage1.Controls.Add(Me.txtPesqCodOmie3)
        Me.TabPage1.Controls.Add(Me.txPesqCodDesenhoProduto3)
        Me.TabPage1.Controls.Add(Me.txtPesqCodOmie2)
        Me.TabPage1.Controls.Add(Me.txPesqCodDesenhoProduto2)
        Me.TabPage1.Controls.Add(Me.txtPesqDescricaoProduto4)
        Me.TabPage1.Controls.Add(Me.txtPesqDescricaoProduto3)
        Me.TabPage1.Controls.Add(Me.txtPesqDescricaoProduto2)
        Me.TabPage1.Controls.Add(Me.txtPesqDescricaoProduto1)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.txtPesqCodOmie1)
        Me.TabPage1.Controls.Add(Me.txPesqCodDesenhoProduto1)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.dgvTimerProdutos)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage1.Size = New System.Drawing.Size(900, 851)
        Me.TabPage1.TabIndex = 6
        Me.TabPage1.Text = "OSProd."
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 371)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(181, 16)
        Me.Label34.TabIndex = 27
        Me.Label34.Text = "Lista de peças dos Produtos:"
        '
        'Label33
        '
        Me.Label33.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(2, 161)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(257, 16)
        Me.Label33.TabIndex = 26
        Me.Label33.Text = "Lista de Produtos Prontos Para Produção:"
        '
        'dgvTimerProdutosItens
        '
        Me.dgvTimerProdutosItens.AllowUserToAddRows = False
        Me.dgvTimerProdutosItens.AllowUserToDeleteRows = False
        Me.dgvTimerProdutosItens.AllowUserToOrderColumns = True
        Me.dgvTimerProdutosItens.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTimerProdutosItens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvTimerProdutosItens.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dgvTimerProdutosItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimerProdutosItens.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvIconeItemOSProduto})
        Me.dgvTimerProdutosItens.ContextMenuStrip = Me.mnudgvTimerProdutosItens
        Me.dgvTimerProdutosItens.Location = New System.Drawing.Point(9, 401)
        Me.dgvTimerProdutosItens.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvTimerProdutosItens.Name = "dgvTimerProdutosItens"
        Me.dgvTimerProdutosItens.ReadOnly = True
        Me.dgvTimerProdutosItens.RowHeadersWidth = 51
        Me.dgvTimerProdutosItens.RowTemplate.Height = 24
        Me.dgvTimerProdutosItens.Size = New System.Drawing.Size(885, 447)
        Me.dgvTimerProdutosItens.TabIndex = 25
        '
        'dgvIconeItemOSProduto
        '
        Me.dgvIconeItemOSProduto.Frozen = True
        Me.dgvIconeItemOSProduto.HeaderText = "dgvIconeItemOSProduto"
        Me.dgvIconeItemOSProduto.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvIconeItemOSProduto.MinimumWidth = 6
        Me.dgvIconeItemOSProduto.Name = "dgvIconeItemOSProduto"
        Me.dgvIconeItemOSProduto.ReadOnly = True
        Me.dgvIconeItemOSProduto.Width = 6
        '
        'mnudgvTimerProdutosItens
        '
        Me.mnudgvTimerProdutosItens.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvTimerProdutosItens.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator30, Me.ToolStripMenuItem1, Me.ToolStripSeparator31, Me.ToolStripMenuItem5, Me.ToolStripMenuItem6})
        Me.mnudgvTimerProdutosItens.Name = "MNUdgvTimerProdutos"
        Me.mnudgvTimerProdutosItens.Size = New System.Drawing.Size(467, 94)
        '
        'ToolStripSeparator30
        '
        Me.ToolStripSeparator30.Name = "ToolStripSeparator30"
        Me.ToolStripSeparator30.Size = New System.Drawing.Size(463, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(466, 26)
        Me.ToolStripMenuItem1.Text = "Abrir PDF da Linha Selecionada"
        '
        'ToolStripSeparator31
        '
        Me.ToolStripSeparator31.Name = "ToolStripSeparator31"
        Me.ToolStripSeparator31.Size = New System.Drawing.Size(463, 6)
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Image = Global.SwLynx_4._1.My.Resources.Resources.IconeswPrincipal
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(466, 26)
        Me.ToolStripMenuItem5.Text = "Marcar como Conjunto Principal da Ordem de Serviço"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Image = Global.SwLynx_4._1.My.Resources.Resources.IcopneMontagemSW
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(466, 26)
        Me.ToolStripMenuItem6.Text = "Desmarcar Como Conjunto Principal da Ordem de Serviço"
        '
        'txtPesqCodOmie4
        '
        Me.txtPesqCodOmie4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCodOmie4.Location = New System.Drawing.Point(444, 74)
        Me.txtPesqCodOmie4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqCodOmie4.Name = "txtPesqCodOmie4"
        Me.txtPesqCodOmie4.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqCodOmie4.TabIndex = 24
        '
        'txPesqCodDesenhoProduto4
        '
        Me.txPesqCodDesenhoProduto4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txPesqCodDesenhoProduto4.Location = New System.Drawing.Point(444, 30)
        Me.txPesqCodDesenhoProduto4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txPesqCodDesenhoProduto4.Name = "txPesqCodDesenhoProduto4"
        Me.txPesqCodDesenhoProduto4.Size = New System.Drawing.Size(140, 22)
        Me.txPesqCodDesenhoProduto4.TabIndex = 23
        '
        'txtPesqCodOmie3
        '
        Me.txtPesqCodOmie3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCodOmie3.Location = New System.Drawing.Point(299, 74)
        Me.txtPesqCodOmie3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqCodOmie3.Name = "txtPesqCodOmie3"
        Me.txtPesqCodOmie3.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqCodOmie3.TabIndex = 22
        '
        'txPesqCodDesenhoProduto3
        '
        Me.txPesqCodDesenhoProduto3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txPesqCodDesenhoProduto3.Location = New System.Drawing.Point(299, 30)
        Me.txPesqCodDesenhoProduto3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txPesqCodDesenhoProduto3.Name = "txPesqCodDesenhoProduto3"
        Me.txPesqCodDesenhoProduto3.Size = New System.Drawing.Size(140, 22)
        Me.txPesqCodDesenhoProduto3.TabIndex = 21
        '
        'txtPesqCodOmie2
        '
        Me.txtPesqCodOmie2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCodOmie2.Location = New System.Drawing.Point(152, 74)
        Me.txtPesqCodOmie2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqCodOmie2.Name = "txtPesqCodOmie2"
        Me.txtPesqCodOmie2.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqCodOmie2.TabIndex = 20
        '
        'txPesqCodDesenhoProduto2
        '
        Me.txPesqCodDesenhoProduto2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txPesqCodDesenhoProduto2.Location = New System.Drawing.Point(152, 30)
        Me.txPesqCodDesenhoProduto2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txPesqCodDesenhoProduto2.Name = "txPesqCodDesenhoProduto2"
        Me.txPesqCodDesenhoProduto2.Size = New System.Drawing.Size(140, 22)
        Me.txPesqCodDesenhoProduto2.TabIndex = 19
        '
        'txtPesqDescricaoProduto4
        '
        Me.txtPesqDescricaoProduto4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricaoProduto4.Location = New System.Drawing.Point(444, 126)
        Me.txtPesqDescricaoProduto4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqDescricaoProduto4.Name = "txtPesqDescricaoProduto4"
        Me.txtPesqDescricaoProduto4.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqDescricaoProduto4.TabIndex = 18
        '
        'txtPesqDescricaoProduto3
        '
        Me.txtPesqDescricaoProduto3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricaoProduto3.Location = New System.Drawing.Point(299, 126)
        Me.txtPesqDescricaoProduto3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqDescricaoProduto3.Name = "txtPesqDescricaoProduto3"
        Me.txtPesqDescricaoProduto3.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqDescricaoProduto3.TabIndex = 17
        '
        'txtPesqDescricaoProduto2
        '
        Me.txtPesqDescricaoProduto2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricaoProduto2.Location = New System.Drawing.Point(152, 126)
        Me.txtPesqDescricaoProduto2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqDescricaoProduto2.Name = "txtPesqDescricaoProduto2"
        Me.txtPesqDescricaoProduto2.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqDescricaoProduto2.TabIndex = 16
        '
        'txtPesqDescricaoProduto1
        '
        Me.txtPesqDescricaoProduto1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricaoProduto1.Location = New System.Drawing.Point(5, 126)
        Me.txtPesqDescricaoProduto1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqDescricaoProduto1.Name = "txtPesqDescricaoProduto1"
        Me.txtPesqDescricaoProduto1.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqDescricaoProduto1.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(5, 106)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(140, 16)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Descrição do produto:"
        '
        'txtPesqCodOmie1
        '
        Me.txtPesqCodOmie1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCodOmie1.Location = New System.Drawing.Point(5, 74)
        Me.txtPesqCodOmie1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesqCodOmie1.Name = "txtPesqCodOmie1"
        Me.txtPesqCodOmie1.Size = New System.Drawing.Size(140, 22)
        Me.txtPesqCodOmie1.TabIndex = 13
        '
        'txPesqCodDesenhoProduto1
        '
        Me.txPesqCodDesenhoProduto1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txPesqCodDesenhoProduto1.Location = New System.Drawing.Point(5, 30)
        Me.txPesqCodDesenhoProduto1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txPesqCodDesenhoProduto1.Name = "txPesqCodDesenhoProduto1"
        Me.txPesqCodDesenhoProduto1.Size = New System.Drawing.Size(140, 22)
        Me.txPesqCodDesenhoProduto1.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 55)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 16)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Codigo OMIE:"
        '
        'Label15
        '
        Me.Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(5, 11)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(180, 16)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Codigo Desenho do produto:"
        '
        'dgvTimerProdutos
        '
        Me.dgvTimerProdutos.AllowUserToAddRows = False
        Me.dgvTimerProdutos.AllowUserToDeleteRows = False
        Me.dgvTimerProdutos.AllowUserToOrderColumns = True
        Me.dgvTimerProdutos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTimerProdutos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvTimerProdutos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dgvTimerProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimerProdutos.ContextMenuStrip = Me.MNUdgvTimerProdutos
        Me.dgvTimerProdutos.Location = New System.Drawing.Point(5, 194)
        Me.dgvTimerProdutos.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvTimerProdutos.Name = "dgvTimerProdutos"
        Me.dgvTimerProdutos.ReadOnly = True
        Me.dgvTimerProdutos.RowHeadersWidth = 51
        Me.dgvTimerProdutos.RowTemplate.Height = 24
        Me.dgvTimerProdutos.Size = New System.Drawing.Size(889, 156)
        Me.dgvTimerProdutos.TabIndex = 0
        '
        'MNUdgvTimerProdutos
        '
        Me.MNUdgvTimerProdutos.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MNUdgvTimerProdutos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirPDFFichaTecnicaToolStripMenuItem, Me.ToolStripSeparator29, Me.AbrirPDFIsometricoToolStripMenuItem, Me.ToolStripSeparator32, Me.EditarProdutoExistenteToolStripMenuItem})
        Me.MNUdgvTimerProdutos.Name = "MNUdgvTimerProdutos"
        Me.MNUdgvTimerProdutos.Size = New System.Drawing.Size(242, 94)
        '
        'AbrirPDFFichaTecnicaToolStripMenuItem
        '
        Me.AbrirPDFFichaTecnicaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.AbrirPDFFichaTecnicaToolStripMenuItem.Name = "AbrirPDFFichaTecnicaToolStripMenuItem"
        Me.AbrirPDFFichaTecnicaToolStripMenuItem.Size = New System.Drawing.Size(241, 26)
        Me.AbrirPDFFichaTecnicaToolStripMenuItem.Text = "Abrir PDF Ficha Tecnica"
        '
        'ToolStripSeparator29
        '
        Me.ToolStripSeparator29.Name = "ToolStripSeparator29"
        Me.ToolStripSeparator29.Size = New System.Drawing.Size(238, 6)
        '
        'AbrirPDFIsometricoToolStripMenuItem
        '
        Me.AbrirPDFIsometricoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.AbrirPDFIsometricoToolStripMenuItem.Name = "AbrirPDFIsometricoToolStripMenuItem"
        Me.AbrirPDFIsometricoToolStripMenuItem.Size = New System.Drawing.Size(241, 26)
        Me.AbrirPDFIsometricoToolStripMenuItem.Text = "Abrir PDF Isometrico"
        '
        'ToolStripSeparator32
        '
        Me.ToolStripSeparator32.Name = "ToolStripSeparator32"
        Me.ToolStripSeparator32.Size = New System.Drawing.Size(238, 6)
        '
        'EditarProdutoExistenteToolStripMenuItem
        '
        Me.EditarProdutoExistenteToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.IconeswPrincipal
        Me.EditarProdutoExistenteToolStripMenuItem.Name = "EditarProdutoExistenteToolStripMenuItem"
        Me.EditarProdutoExistenteToolStripMenuItem.Size = New System.Drawing.Size(241, 26)
        Me.EditarProdutoExistenteToolStripMenuItem.Text = "Editar Produto Existente"
        '
        'TimerdgvDesenhos
        '
        Me.TimerdgvDesenhos.Interval = 700
        '
        'TimerMontaPeca
        '
        '
        'Timerdgvos
        '
        '
        'TimerDGVListaMaterialSW
        '
        '
        'TimerFiltroPecaAtivaOS
        '
        '
        'ToolTipAjuda
        '
        Me.ToolTipAjuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTipAjuda.ToolTipTitle = "Dicas de Uso:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'TimerHoraServidor
        '
        Me.TimerHoraServidor.Interval = 300000
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.Frozen = True
        Me.DataGridViewImageColumn1.HeaderText = "Tipo"
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Width = 6
        '
        'TimerpcpAgrupamentoProjeto
        '
        '
        'TimerProdutos
        '
        Me.TimerProdutos.Interval = 500
        '
        'TimerProdutoItens
        '
        Me.TimerProdutoItens.Interval = 500
        '
        'Painel_Leitura_Dados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.tpgPrincipal)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Painel_Leitura_Dados"
        Me.Size = New System.Drawing.Size(916, 889)
        Me.tpgPrincipal.ResumeLayout(False)
        Me.tpgFolhaDados.ResumeLayout(False)
        Me.tpgFolhaDados.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.BnPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BnPrincipal.ResumeLayout(False)
        Me.BnPrincipal.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.DGVMontaPeca, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuDGVMontaPeca.ResumeLayout(False)
        Me.mnuPrincipal.ResumeLayout(False)
        Me.mnuPrincipal.PerformLayout()
        Me.tpgDesenhosCadstrados.ResumeLayout(False)
        Me.tpgDesenhosCadstrados.PerformLayout()
        CType(Me.dgvDesenhos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnubtnListaMaterial.ResumeLayout(False)
        Me.tpgBom.ResumeLayout(False)
        Me.tpgBom.PerformLayout()
        CType(Me.BindingNavigator2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator2.ResumeLayout(False)
        Me.BindingNavigator2.PerformLayout()
        CType(Me.dgvDataGridBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnudgvDataGridBOM.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.tpgOrdemServico.ResumeLayout(False)
        Me.tpgOrdemServico.PerformLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        CType(Me.DGVListaMaterialSW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuDGVListaMaterialSW.ResumeLayout(False)
        CType(Me.dgvos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnudgvos.ResumeLayout(False)
        CType(Me.DGVListaMaterialSWMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpgListaRNCPecaCorrente.ResumeLayout(False)
        CType(Me.DGVTimerFiltroPecaAtivaOS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpgPCP.ResumeLayout(False)
        Me.tpgPCP.PerformLayout()
        CType(Me.dgvTimerpcpAgrupamentoProjetoDetalhamento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTimerpcpAgrupamentoProjeto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgvTimerProdutosItens, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnudgvTimerProdutosItens.ResumeLayout(False)
        CType(Me.dgvTimerProdutos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MNUdgvTimerProdutos.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tpgPrincipal As Windows.Forms.TabControl
    Friend WithEvents tpgFolhaDados As Windows.Forms.TabPage
    Friend WithEvents tpgBom As Windows.Forms.TabPage
    Friend WithEvents txtComentarios As Windows.Forms.TextBox
    Friend WithEvents txtPalavraChave As Windows.Forms.TextBox
    Friend WithEvents txtAuthor As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtAssuntoSubiTitulo As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents Label23 As Windows.Forms.Label
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents chkMontagem As Windows.Forms.CheckBox
    Friend WithEvents chkPintura As Windows.Forms.CheckBox
    Friend WithEvents chkSolda As Windows.Forms.CheckBox
    Friend WithEvents chkCorte As Windows.Forms.CheckBox
    Friend WithEvents chkDobra As Windows.Forms.CheckBox
    Friend WithEvents dgvDataGridBOM As Windows.Forms.DataGridView
    Friend WithEvents btnListaMaterial As Windows.Forms.Button
    Friend WithEvents chkVerificarDXF As Windows.Forms.CheckBox
    Friend WithEvents chkVerificarPDF As Windows.Forms.CheckBox
    Friend WithEvents btnLimparBom As Windows.Forms.Button
    Friend WithEvents tpgDesenhosCadstrados As Windows.Forms.TabPage
    Friend WithEvents dgvDesenhos As Windows.Forms.DataGridView
    Friend WithEvents TimerdgvDesenhos As Windows.Forms.Timer
    Friend WithEvents TxtPesqSubtitulo3 As Windows.Forms.TextBox
    Friend WithEvents TxtPesqSubtitulo2 As Windows.Forms.TextBox
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents TxtPesqSubtitulo As Windows.Forms.TextBox
    Friend WithEvents TxtPesgTitulo As Windows.Forms.TextBox
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents TxtPesgNomeDesenho As Windows.Forms.TextBox
    Friend WithEvents mnuDGVListaMaterialSW As Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirPDFDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AbrirDXFDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpgOrdemServico As Windows.Forms.TabPage
    Friend WithEvents cboProjeto As Windows.Forms.ComboBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents txtDescricaoTag As Windows.Forms.TextBox
    Friend WithEvents txtCliente As Windows.Forms.TextBox
    Friend WithEvents cboTag As Windows.Forms.ComboBox
    Friend WithEvents Label24 As Windows.Forms.Label
    Friend WithEvents chkConverterDXF As Windows.Forms.CheckBox
    Friend WithEvents chkConverterPDF As Windows.Forms.CheckBox
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents DataGridViewImageColumn1 As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DGVMontaPeca As Windows.Forms.DataGridView
    Friend WithEvents TimerMontaPeca As Windows.Forms.Timer
    Friend WithEvents dgvos As Windows.Forms.DataGridView
    Friend WithEvents chkMostraLiberadasPelaEngenharia As Windows.Forms.CheckBox
    Friend WithEvents Timerdgvos As Windows.Forms.Timer
    Friend WithEvents dgvStatus As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DGVListaMaterialSW As Windows.Forms.DataGridView
    Friend WithEvents TimerDGVListaMaterialSW As Windows.Forms.Timer
    Friend WithEvents txtDescricao As Windows.Forms.TextBox
    Friend WithEvents Label25 As Windows.Forms.Label
    Friend WithEvents lblOrdemServicoAtiva As Windows.Forms.Label
    Friend WithEvents btnInserirItensOrdemServico As Windows.Forms.Button
    Friend WithEvents ProgressBarListaSW As Windows.Forms.ProgressBar
    Friend WithEvents mnudgvos As Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirPastaDaOrdemDeServiçoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As Windows.Forms.ToolStripSeparator
    Friend WithEvents LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgressBarProcessoLiberacaoOrdemServico As Windows.Forms.ProgressBar
    Friend WithEvents DGVListaMaterialSWMaterial As Windows.Forms.DataGridView
    Friend WithEvents DataGridViewImageColumn2 As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ToolStripSeparator5 As Windows.Forms.ToolStripSeparator
    Friend WithEvents MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesmarcarComoConjuntoPrincipalToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboOpcoesAcabamento As Windows.Forms.ComboBox
    Friend WithEvents Label26 As Windows.Forms.Label
    Friend WithEvents btnAplicarAcabamento As Windows.Forms.Button
    Friend WithEvents MarcarTodosToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesmarcarTodosToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents InverterSeleçãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As Windows.Forms.ToolStripSeparator
    Friend WithEvents txtPesqAcabamentoDesenho As Windows.Forms.TextBox
    Friend WithEvents Label29 As Windows.Forms.Label
    Friend WithEvents txtPesqTipoDesenho As Windows.Forms.TextBox
    Friend WithEvents Label28 As Windows.Forms.Label
    Friend WithEvents txtPesqNumeroDesenho As Windows.Forms.TextBox
    Friend WithEvents Label27 As Windows.Forms.Label
    Friend WithEvents ToolStripSeparator7 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ImprimirDesenhoPDFSelecionadoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpgListaRNCPecaCorrente As Windows.Forms.TabPage
    Friend WithEvents TimerFiltroPecaAtivaOS As Windows.Forms.Timer
    Friend WithEvents DGVTimerFiltroPecaAtivaOS As Windows.Forms.DataGridView
    Friend WithEvents lblNUmeroDocumentoAtivo As Windows.Forms.Label
    Friend WithEvents dgvSelecaoAtualizacaoItemOs As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgvTipoDesenhoAtualizacaoItemOs As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents btnAtualizarDadosItemOs As Windows.Forms.Button
    Friend WithEvents GeralExcelDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPrincipal As Windows.Forms.MenuStrip
    Friend WithEvents FerramentasToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfiguraçãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfiguraçãoToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrocarFormatoA3ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboTitulo As Windows.Forms.ComboBox
    Friend WithEvents ToolTipAjuda As Windows.Forms.ToolTip
    Friend WithEvents ToolStripSeparator11 As Windows.Forms.ToolStripSeparator
    Friend WithEvents LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrocarForrmatoA4ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarFormatoA3ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BUSCRAToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents txtPesqCriadoPor As Windows.Forms.TextBox
    Friend WithEvents Label30 As Windows.Forms.Label
    Friend WithEvents ConfExportarArquivoParaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CancelarAFabricaçãoDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDGVMontaPeca As Windows.Forms.ContextMenuStrip
    Friend WithEvents ExcluirOMaterialDoDesenhoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAtualizarCadastro As Windows.Forms.Button
    Friend WithEvents ProgressBar1 As Windows.Forms.ProgressBar
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents btnConverterListaDXFPDF As Windows.Forms.Button
    Friend WithEvents mnubtnListaMaterial As Windows.Forms.ContextMenuStrip
    Friend WithEvents InformeOTituloPadrãoDoProdutoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox8 As Windows.Forms.GroupBox
    Friend WithEvents optProcessoSoldagemSim As Windows.Forms.RadioButton
    Friend WithEvents optProcessoSoldagemNao As Windows.Forms.RadioButton
    Friend WithEvents GroupBox7 As Windows.Forms.GroupBox
    Friend WithEvents OPTEstoqueSim As Windows.Forms.RadioButton
    Friend WithEvents OPTEstoqueNao As Windows.Forms.RadioButton
    Friend WithEvents TimerHoraServidor As Windows.Forms.Timer
    Friend WithEvents txtHoraServidor As Windows.Forms.ToolStripTextBox
    Friend WithEvents AtualizarDesenhoPeloDiretorioToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkVerificarDFT As Windows.Forms.CheckBox
    Friend WithEvents GerarArquivoEmDXFToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GerarArquivoEmPDFToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvSelecao As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgvIconeItemOS As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvDXF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvPDF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ToolStripSeparator14 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator13 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator15 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AbrirDWRDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarArquivosNosDiretorioToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlterarOFatorMultipçlicadorDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CriarUmCopiaDaOSSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator18 As Windows.Forms.ToolStripSeparator
    Friend WithEvents GerarPDFDasLinhasSelecionadasToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GerarDXFDasLinhasSelecionadasToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CancelarLiberaçãoDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As Windows.Forms.ToolStripSeparator
    Friend WithEvents chkVerificarLXDS As Windows.Forms.CheckBox
    Friend WithEvents TrocarParaFormato4ADeitadoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BiscarFormatoA4DeitadoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSTVersaoSistema As Windows.Forms.ToolStripTextBox
    Friend WithEvents btnPendencias As Windows.Forms.Button
    Friend WithEvents CadastroDeProjetosToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkBoxTipoDesenho As Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents BackgroundWorker1 As ComponentModel.BackgroundWorker
    Friend WithEvents chkBoxAcabamento As Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox10 As Windows.Forms.GroupBox
    Friend WithEvents BnPrincipal As Windows.Forms.BindingNavigator
    Friend WithEvents tsBLerDados As Windows.Forms.ToolStripButton
    Friend WithEvents tsbSalvar As Windows.Forms.ToolStripButton
    Friend WithEvents tsbConverterDXF As Windows.Forms.ToolStripButton
    Friend WithEvents TSBConverterPDF As Windows.Forms.ToolStripButton
    Friend WithEvents TSBAssociarMaterial As Windows.Forms.ToolStripButton
    Friend WithEvents tsbInserirNaOS As Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfiguracoes As Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ConfiguraçãoToolStripMenuItem2 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfExportarArquivoParaOSToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbFerramentas As Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator22 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AtualizarDesenhoPeloDiretorioToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarArquivosNosDiretorioToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsarFormatoA3ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsarFormatoA4ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsarFornatoA4DToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator23 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator24 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator25 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents lblQtdeEstoque As Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigator1 As Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents TSBSalvarOrdemServico As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As Windows.Forms.ToolStripButton
    Friend WithEvents dgvIcone As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Dgvrnc As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents BindingNavigator2 As Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripButton4 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton6 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton7 As Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents lblEspessura As Windows.Forms.Label
    Friend WithEvents lblPeso As Windows.Forms.Label
    Friend WithEvents lblMaterial As Windows.Forms.Label
    Friend WithEvents lblLarguraTotalCaixaDelimitadora As Windows.Forms.Label
    Friend WithEvents lblAreaPintura As Windows.Forms.Label
    Friend WithEvents lblNumeroDobra As Windows.Forms.Label
    Friend WithEvents lblComprimento As Windows.Forms.Label
    Friend WithEvents lblLargura As Windows.Forms.Label
    Friend WithEvents lblProfundidadeTotalCaixaDelimitadora As Windows.Forms.Label
    Friend WithEvents lblAlturaTotalCaixaDelimitadora As Windows.Forms.Label
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents tslVersaoSistema As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator26 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator28 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator27 As Windows.Forms.ToolStripSeparator
    Friend WithEvents TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpgPCP As Windows.Forms.TabPage
    Friend WithEvents dgvTimerpcpAgrupamentoProjeto As Windows.Forms.DataGridView
    Friend WithEvents TimerpcpAgrupamentoProjeto As Windows.Forms.Timer
    Friend WithEvents txtClientepcp As Windows.Forms.TextBox
    Friend WithEvents cboProjetoPCP As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents dgvTimerpcpAgrupamentoProjetoDetalhamento As Windows.Forms.DataGridView
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents dgvTimerProdutos As Windows.Forms.DataGridView
    Friend WithEvents TimerProdutos As Windows.Forms.Timer
    Friend WithEvents txtPesqDescricaoProduto1 As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents txtPesqCodOmie1 As Windows.Forms.TextBox
    Friend WithEvents txPesqCodDesenhoProduto1 As Windows.Forms.TextBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents txtPesqCodOmie4 As Windows.Forms.TextBox
    Friend WithEvents txPesqCodDesenhoProduto4 As Windows.Forms.TextBox
    Friend WithEvents txtPesqCodOmie3 As Windows.Forms.TextBox
    Friend WithEvents txPesqCodDesenhoProduto3 As Windows.Forms.TextBox
    Friend WithEvents txtPesqCodOmie2 As Windows.Forms.TextBox
    Friend WithEvents txPesqCodDesenhoProduto2 As Windows.Forms.TextBox
    Friend WithEvents txtPesqDescricaoProduto4 As Windows.Forms.TextBox
    Friend WithEvents txtPesqDescricaoProduto3 As Windows.Forms.TextBox
    Friend WithEvents txtPesqDescricaoProduto2 As Windows.Forms.TextBox
    Friend WithEvents dgvTimerProdutosItens As Windows.Forms.DataGridView
    Friend WithEvents TimerProdutoItens As Windows.Forms.Timer
    Friend WithEvents dgvIconeItemOSProduto As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents MNUdgvTimerProdutos As Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirPDFFichaTecnicaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator29 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AbrirPDFIsometricoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnudgvTimerProdutosItens As Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator30 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem5 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator31 As Windows.Forms.ToolStripSeparator
    Friend WithEvents txtFichaTecnica As Windows.Forms.TextBox
    Friend WithEvents Label32 As Windows.Forms.Label
    Friend WithEvents Label31 As Windows.Forms.Label
    Friend WithEvents txtIsometrico As Windows.Forms.TextBox
    Friend WithEvents btnIsometrico As Windows.Forms.Button
    Friend WithEvents btnFichaTecnica As Windows.Forms.Button
    Friend WithEvents txtNomeArquivo As Windows.Forms.ToolStripTextBox
    Friend WithEvents Label34 As Windows.Forms.Label
    Friend WithEvents Label33 As Windows.Forms.Label
    Friend WithEvents ToolStripSeparator32 As Windows.Forms.ToolStripSeparator
    Friend WithEvents EditarProdutoExistenteToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnudgvDataGridBOM As Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator34 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem3 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator33 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator35 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AbrirLXDSDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DGVIconeLXDS As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DGVIconeDXF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvIconePDF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ToolStripSeparator36 As Windows.Forms.ToolStripSeparator
    Friend WithEvents TsbAtualizarBOM As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator37 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator38 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton9 As Windows.Forms.ToolStripButton
    Friend WithEvents TsbInspecaoQualidade As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator39 As Windows.Forms.ToolStripSeparator
End Class
