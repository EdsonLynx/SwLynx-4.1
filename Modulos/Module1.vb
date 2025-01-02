Imports MySql.Data.MySqlClient
Imports SolidWorks.Interop.sldworks
Imports System.Data.OleDb


Public Module Module1

    ' Public cn As New mysqlConnection  'Variavel do banco de Dados
    ' Public myCmd As New SqlCommand  'Variavel da conexão e execução do comando do banco de dados
    ' Public dr As SqlDataReader

    Public cl_BancoDados As New ClBancoDados
    '  Public BancoDadosInstancia As BancoDadosClasse

    Public banco As BancoDadosClasse = BancoDadosClasse.GetInstance()

    Public myconect As New MySqlConnection
    Public mycomand As New MySqlCommand

    Public myconectAccess As New OleDbConnection
    Public mycomandAccess As New OleDbCommand

    Public conexao As String

    Public sModelName As String
    Public sPathName As String

    Public config As Configuration
    Public swapp As SldWorks
    'Public swApparq As New Object

    Public instance As ISldWorks
    Public swModel As ModelDoc2 'usado no arquivo corrente
    Public swModelComp As ModelDoc2 'usado na estrutura de arvore do projeto

    Public swPart As PartDoc
    Public swDocEvent As DocumentEventHandler

    Public swModelDocExt As ModelDocExtension
    Public swCustProp As CustomPropertyManager
    Public MyMassProp As MassProperty


    Public swAssembly As AssemblyDoc = Nothing
    Public swFeature As Feature = Nothing


    Public swDrawing As DrawingDoc = Nothing


    'Public bomTableFeature As Feature
    'Public bomTable As BomTable = Nothing
    Public rowCount As Integer
    Public colCount As Integer
    Public selection As Integer
    Public selectedType As String

    ' Public swDraw As Object
    'Public swFeat As Feature
    Public swBomFeat As BomTable

    Public vTableArr As Object
    Public vTable As Object
    Public vConfigArray As Object
    Public vConfig As Object
    Public ConfigName As String
    Public swTable As TableAnnotation

    Public DadosArquivoCorrente As New ClDadosArquivoCorrente

    Public MyTaskPanelHost As New Painel_Leitura_Dados

    Public OrdemServico As New CLOrdemServico

    Public Projeto As New clProjeto

    Public OrdemServicoItemPendencia As New CLOrdemServicoItemPendencia

    Public colunas() As String

    Public qtdePecaLm As Double

    Public InserirNovoTabelas As Boolean = False


    Public IntanciaSolidWorks As New ClSolidWorks

    Public NovoIdOrdemServico As String  'Recebe o novo id da nova ordem de serviço


    Public TemplatesExcel As New ClExcel


    Public Impressora As New ClImpressao

    Public Usuario As New clUsuario


    Public EntradaLogin As New frmLogin
    Public ExportarParaOS As New frmExportarParaOS
    Public MateriaisAlmoxarifado As New frmMateriaisAlmoxarifado
    Public Arquivos As New FrmBuscarProgramas
    ' Public Projeto As New frmProjetos
    Public PendenciasRNC As New frmRNC

    Public ClSwAddin As New SwAddin


    Public dtDesenhos As System.Data.DataTable
    Public TabelaViewMontaPeca As System.Data.DataTable

    Public IdMontaPeca As Integer

    Public TituloPadraoProduto As String


    Public HoraServidor As String


    Public Email As New clEmail

    '*****************************************************
    'CLASSES ESPECIFICAS DE CLIENTES

    Public PadraoMetta As New clPadraoMetta

    Public pdfsinco As New clPdf ' Trabalhando com arquivos PDF


    Public DescricaoFinalizacaoPendencia As String

    Public ExtensaoArquivoCorrente As String


End Module
