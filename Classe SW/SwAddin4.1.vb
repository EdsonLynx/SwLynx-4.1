Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports SolidWorks.Interop.swpublished
Imports SolidWorksTools
Imports SolidWorksTools.File


'Se você tem um suplemento que está sendo carregado automaticamente na inicialização Do SolidWorks e deseja desativá-lo via VB.NET, você pode modificar o comportamento Do suplemento alterando o estado de carregamento automático no registro Do Windows.

'O SolidWorks usa o Registro Do Windows para determinar se um suplemento deve ser carregado na inicialização. A chave de registro que controla isso pode ser encontrada em

'Caminho do Registro
'HKEY_CURRENT_USER\ Software \ SolidWorks \ AddInsStartup \ {GUID Do Add-In}


<Guid("971f984a-5563-483d-a608-2f62bda31268")>
<ComVisible(True)>
<SwAddin(
        Description:="SwLynx_4._1",
        Title:="SwLynx_4._1",
        LoadAtStartup:=True
        )>
Public Class SwAddin
    Implements SolidWorks.Interop.swpublished.SwAddin
    Implements SolidWorks.Interop.sldworks.DSldWorksEvents

    Private WithEvents selectionMgr As SelectionMgr
    Private WithEvents modelView As ModelView

#Region "Local Variables"

    Dim WithEvents iSwApp As SldWorks

    'ReadOnly iCmdMgr As ICommandManager *EDSON VAriavel original de base 21/08/24
    Dim iCmdMgr As ICommandManager

    Dim addinID As Integer
    Dim openDocs As Hashtable
    Dim SwEventPtr As SldWorks
    Dim ppage As UserPMPage
    Dim iBmp As BitmapHandler
    Dim frame As IFrame

    ' Dim bRet As Boolean
    Dim registerID As Integer

    ' Declara uma variável para armazenar uma referência à visualização do painel de tarefas do SolidWorks.
    ' A interface TaskpaneView é usada para interagir com o painel de tarefas do SolidWorks,
    ' que é uma área onde você pode adicionar controles personalizados e exibir informações adicionais.
    Dim MyTaskPaneView As SolidWorks.Interop.sldworks.TaskpaneView

    ' Declara uma variável para armazenar uma referência ao controle principal do painel de tarefas.
    ' O ControlPrincipal é uma classe personalizada que representa o conteúdo e a lógica do painel de tarefas,
    ' permitindo que você adicione e manipule os controles dentro do painel de tarefas do SolidWorks.
    Public MyTaskPanelHost As New Painel_Leitura_Dados

    Public Const mainCmdGroupID As Integer = 0
    Public Const mainItemID1 As Integer = 0
    Public Const mainItemID2 As Integer = 1
    Public Const flyoutGroupID As Integer = 91

    ' Public Properties
    ReadOnly Property SwApp() As SldWorks
        Get
            Return iSwApp
        End Get
    End Property

    ReadOnly Property CmdMgr() As ICommandManager
        Get
            Return iCmdMgr
        End Get
    End Property

    ReadOnly Property OpenDocumentsTable() As Hashtable
        Get
            Return openDocs
        End Get
    End Property

#End Region

#Region "SolidWorks Registration"

    <ComRegisterFunction()> Public Shared Sub RegisterFunction(ByVal t As Type)

        ' Get Custom Attribute: SwAddinAttribute
        Dim attributes() As Object
        Dim SWattr As SwAddinAttribute = Nothing

        attributes = System.Attribute.GetCustomAttributes(GetType(SwAddin), GetType(SwAddinAttribute))

        If attributes.Length > 0 Then
            SWattr = DirectCast(attributes(0), SwAddinAttribute)
        End If
        Try
            Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine
            Dim hkcu As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser

            Dim keyname As String = "SOFTWARE\SolidWorks\Addins\{" + t.GUID.ToString() + "}"
            Dim addinkey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey(keyname)
            addinkey.SetValue(Nothing, 0)
            addinkey.SetValue("Description", SWattr.Description)
            addinkey.SetValue("Title", SWattr.Title)

            keyname = "Software\SolidWorks\AddInsStartup\{" + t.GUID.ToString() + "}"
            addinkey = hkcu.CreateSubKey(keyname)
            addinkey.SetValue(Nothing, SWattr.LoadAtStartup, Microsoft.Win32.RegistryValueKind.DWord)
        Catch nl As System.NullReferenceException
            Console.WriteLine("There was a problem registering this dll: SWattr is null.\n " & nl.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem registering this dll: SWattr is null.\n" & nl.Message)
        Catch e As System.Exception
            Console.WriteLine("There was a problem registering this dll: " & e.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem registering this dll: " & e.Message)
        End Try
    End Sub

    <ComUnregisterFunction()> Public Shared Sub UnregisterFunction(ByVal t As Type)
        Try
            Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine
            Dim hkcu As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser

            Dim keyname As String = "SOFTWARE\SolidWorks\Addins\{" + t.GUID.ToString() + "}"
            hklm.DeleteSubKey(keyname)

            keyname = "Software\SolidWorks\AddInsStartup\{" + t.GUID.ToString() + "}"
            hkcu.DeleteSubKey(keyname)
        Catch nl As System.NullReferenceException
            Console.WriteLine("There was a problem unregistering this dll: SWattr is null.\n " & nl.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: SWattr is null.\n" & nl.Message)
        Catch e As System.Exception
            Console.WriteLine("There was a problem unregistering this dll: " & e.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: " & e.Message)
        End Try

    End Sub

#End Region

#Region "ISwAddin Implementation"

    Function ConnectToSW(ByVal ThisSW As Object, ByVal Cookie As Integer) As Boolean Implements SolidWorks.Interop.swpublished.SwAddin.ConnectToSW

        iSwApp = ThisSW
        addinID = Cookie


        If cl_BancoDados.AbrirBanco() = True Then


            AddTaskPane()
            ShowTaskPane()

            MyTaskPanelHost.Show()

            EntradaLogin.ShowDialog()

            ' Subscreve-se aos eventos do SolidWorks
            AttachEventHandlers()

            ' Chama a função para anexar os manipuladores de eventos a todos os documentos atualmente abertos
            'AttachEventsToAllDocuments()

            iCmdMgr = SwApp.GetCommandManager(Cookie)
            SwApp.SetAddinCallbackInfo2(0, Me, Cookie)

            ' Se inscrever no evento de mudança de documento ativo
            '  AddHandler SwApp.ActiveModelDocChangeNotify, AddressOf OnActiveModelDocChange

            SwApp.LoadAddIn("SwLynx_4._1")
            ConnectToSW = True



        Else

            SwApp.UnloadAddIn("SwLynx_4._1")

            ConnectToSW = False
            '  Lynx_SW_1._0

        End If

        Try


            SwApp.UnloadAddIn("Lynx_SW_1._0")
            ConnectToSW = False
        Catch ex As Exception
        Finally
            SwApp.UnloadAddIn("SwLynx_4._1")
            ConnectToSW = False
        End Try




    End Function


    Function SelectionChangeNotify() As Integer
        'MsgBox("mudou")
        ' O que fazer quando a seleção muda
        ' Pode acessar a seleção via SelectionMgr e executar lógica com base nela
        Return 0
    End Function

    ' Evento disparado quando o documento ativo muda
    Function OnActiveModelDocChange() As Integer
        ' Atualizar o modelo ativo
        swModel = SwApp.ActiveDoc

        If swModel Is Nothing Then
            'Console.WriteLine("Nenhum documento ativo no SolidWorks.")
        Else
            ' Pegar o Selection Manager para capturar a seleção
            selectionMgr = swModel.SelectionManager

            ' Verificar a seleção atual
            If selectionMgr.GetSelectedObjectCount2(-1) > 0 Then
                Dim selectedObj As Object = selectionMgr.GetSelectedObject6(1, -1)
                Dim objType As Integer = selectionMgr.GetSelectedObjectType3(1, -1)

                ' Verificar se o objeto selecionado é um componente
                If objType = swSelectType_e.swSelCOMPONENTS Then
                    Dim component As Component2 = TryCast(selectedObj, Component2)
                    If component IsNot Nothing Then
                        ' Exibir o nome e o caminho do arquivo
                        Dim fileName As String = component.GetPathName()
                        ' MsgBox("Arquivo selecionado: " & fileName)
                    End If
                    'Else
                    '    MsgBox("Outro tipo de objeto selecionado.")
                End If
            End If
        End If

        Return 0
    End Function

    ' Manipulador de eventos para o fechamento de documentos
    Private Sub OnDocumentClose(ByVal swDoc As ModelDoc2)
        ' Ação a ser tomada quando um documento é fechado
        MessageBox.Show("Documento fechado: " & swModel.GetTitle())
    End Sub

    ' Manipulador para o evento ActiveDocChange
    Private Sub OnActiveDocChange(ByVal NewActiveDoc As ModelDoc2)
        If Not NewActiveDoc Is Nothing Then
            ' Mostra o nome do documento ativo
            MsgBox("Documento ativo: " & NewActiveDoc.GetTitle())
        Else
            MsgBox("Nenhum documento ativo.")
        End If
    End Sub

    Private Sub ShowTaskPane()

        If MyTaskPanelHost IsNot Nothing Then
            ' Configura o painel de tarefas para estar visível e ativo
            MyTaskPanelHost.Visible = True
            ' SwApp.ActivateTaskPaneView(1)
        End If

    End Sub

    Function DisconnectFromSW() As Boolean Implements SolidWorks.Interop.swpublished.SwAddin.DisconnectFromSW

        RemoveTaskPane()

        ' Remove os manipuladores de eventos
        DetachEventHandlers()

        'GC.Collect()
        'GC.WaitForPendingFinalizers()

        GC.Collect()
        GC.WaitForPendingFinalizers()

        '  RemoveHandler SwApp.ActiveModelDocChangeNotify, AddressOf OnActiveModelDocChange

        Return True

        DisconnectFromSW = True
    End Function

    Public Function FileCloseNotify(FileName As String, reason As Integer) As Integer Implements DSldWorksEvents.FileCloseNotify
        Try
            ' Aqui você pode registrar o momento do fechamento.
            Dim currentTime As DateTime = DateTime.Now
            Dim message As String = $"Documento fechado: {FileName}. Momento do fechamento: {currentTime}"

            ' Exemplo: mostrar uma mensagem com o nome do arquivo e o horário de fechamento
            MessageBox.Show(message, "Fechamento de Documento", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Aqui você pode adicionar lógica adicional se necessário
        Catch ex As Exception
            ' Tratamento de exceções
            MessageBox.Show($"Erro ao processar o fechamento do documento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return 0
    End Function

#End Region

#Region "UI Methods"

    Public Sub AddTaskPane()

        MyTaskPaneView = SwApp.CreateTaskpaneView2("", "SwLynx_4._1")
        MyTaskPanelHost = MyTaskPaneView.AddControl("SwLynx_4._1", "")

        MyTaskPanelHost.getSwApp(SwApp)

    End Sub

    Public Sub RemoveTaskPane()

        Try
            MyTaskPanelHost = Nothing
            MyTaskPaneView.DeleteView()
            Marshal.ReleaseComObject(MyTaskPaneView)
            MyTaskPaneView = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Public Sub AddCommandMgr()

        Dim cmdGroup As ICommandGroup

        If iBmp Is Nothing Then
            iBmp = New BitmapHandler()
        End If

        Dim thisAssembly As Assembly

        Dim cmdIndex0 As Integer, cmdIndex1 As Integer
        Dim Title As String = "Lynx_SW_4._1"
        Dim ToolTip As String = "Lynx_SW_4._1"

        Dim docTypes() As Integer = {swDocumentTypes_e.swDocASSEMBLY,
                                       swDocumentTypes_e.swDocDRAWING,
                                       swDocumentTypes_e.swDocPART}

        thisAssembly = System.Reflection.Assembly.GetAssembly(Me.GetType())

        Dim cmdGroupErr As Integer = 0
        Dim ignorePrevious As Boolean = False

        Dim registryIDs As Object = Nothing
        Dim getDataResult As Boolean = iCmdMgr.GetGroupDataFromRegistry(mainCmdGroupID, registryIDs)

        Dim knownIDs As Integer() = New Integer(1) {mainItemID1, mainItemID2}

        If getDataResult Then
            If Not CompareIDs(registryIDs, knownIDs) Then 'if the IDs don't match, reset the commandGroup
                ignorePrevious = True
            End If
        End If

        thisAssembly = Nothing

    End Sub

    Public Sub RemoveCommandMgr()
        Try
            iBmp.Dispose()
            iCmdMgr.RemoveCommandGroup(mainCmdGroupID)
            iCmdMgr.RemoveFlyoutGroup(flyoutGroupID)
        Catch e As Exception
        End Try
    End Sub

    Function AddPMP() As Boolean
        ppage = New UserPMPage
        ppage.Init(iSwApp, Me)
    End Function

    Function RemovePMP() As Boolean
        ppage = Nothing
    End Function

    Function CompareIDs(ByVal storedIDs() As Integer, ByVal addinIDs() As Integer) As Boolean

        Dim storeList As New List(Of Integer)(storedIDs)
        Dim addinList As New List(Of Integer)(addinIDs)

        addinList.Sort()
        storeList.Sort()

        If Not addinList.Count = storeList.Count Then

            Return False
        Else

            For i As Integer = 0 To addinList.Count - 1
                If Not addinList(i) = storeList(i) Then

                    Return False
                End If
            Next
        End If

        Return True
    End Function

#End Region

#Region "Event Methods"

    '' Evento disparado quando o documento ativo muda
    'Private Function OnActiveModelDocChange() As Integer
    '    swModel = SwApp.ActiveDoc
    '    AttachSelectionEvent()
    '    Return 0
    'End Function

    '' Inscreve-se no evento de mudança de seleção
    'Private Sub AttachSelectionEvent()
    '    If swModel IsNot Nothing Then
    '        ' Inscreve-se no evento de mudança de seleção
    '        AddHandler CType(ModelDoc, DModelDoc2Events_Event).SelectionChangeNotify, AddressOf OnSelectionChanged
    '    End If
    'End Sub

    '' Função chamada quando a seleção muda
    'Private Function OnSelectionChanged() As Integer
    '    Dim selectionMgr As SelectionMgr = swModel.SelectionManager
    '    Dim selectedObject As Object = selectionMgr.GetSelectedObject6(1, -1)

    '    If selectedObject IsNot Nothing Then
    '        Dim component As Component2 = TryCast(selectedObject, Component2)
    '        If component IsNot Nothing Then
    '            Dim fileName As String = component.GetPathName()
    '            MessageBox.Show("Arquivo selecionado: " & fileName)
    '        Else
    '            Dim fileName As String = swModel.GetPathName()
    '            MessageBox.Show("Documento ativo selecionado: " & fileName)
    '        End If
    '    Else
    '        MessageBox.Show("Nenhum objeto foi selecionado.")
    '    End If
    '    Return 0
    'End Function

    Sub AttachEventHandlers()

        AttachSWEvents()

        'Listen for events on all currently open docs
        AttachEventsToAllDocuments()

        ' MsgBox(swModel.GetPathName)

    End Sub

    Sub DetachEventHandlers()
        ' Remove manipuladores de eventos globais do SolidWorks
        DetachSWEvents()

        ' Fechar a conexão com o banco de dados
        'cl_BancoDados.FechaBanco(myconect)

        Dim numKeys As Integer

        Try
            ' Verifica se há documentos abertos
            numKeys = openDocs.Count
        Catch ex As Exception
        Finally '  MyTaskPanelHost.AtualizaTela()

        End Try

        If numKeys > 0 Then
            ' Cria um array para armazenar as chaves dos documentos abertos
            Dim keys(numKeys - 1) As Object
            openDocs.Keys.CopyTo(keys, 0)

            ' Remove os manipuladores de eventos de cada documento
            For Each key As ModelDoc2 In keys
                If key IsNot Nothing Then
                    Dim docHandler As DocumentEventHandler = openDocs(key)

                    If docHandler IsNot Nothing Then
                        docHandler.DetachEventHandlers() ' Remove os manipuladores de eventos
                        docHandler = Nothing
                    End If

                    ' Remove a chave do dicionário para garantir que o recurso seja liberado
                    openDocs.Remove(key)
                    key = Nothing
                End If
            Next
        End If

        ' Limpeza de variáveis
        numKeys = 0
    End Sub

    Sub AttachSWEvents()
        Try
            AddHandler iSwApp.ActiveDocChangeNotify, AddressOf Me.SldWorks_ActiveDocChangeNotify
            AddHandler iSwApp.DocumentLoadNotify2, AddressOf Me.SldWorks_DocumentLoadNotify2
            AddHandler iSwApp.FileNewNotify2, AddressOf Me.SldWorks_FileNewNotify2
            AddHandler iSwApp.ActiveModelDocChangeNotify, AddressOf Me.SldWorks_ActiveModelDocChangeNotify
            AddHandler iSwApp.FileOpenPostNotify, AddressOf Me.SldWorks_FileOpenPostNotify
            AddHandler iSwApp.FileCloseNotify, AddressOf Me.FileCloseNotify
            ' Acompanhar eventos de seleção globais
            AddHandler iSwApp.ActiveModelDocChangeNotify, AddressOf OnActiveModelDocChange
        Catch e As Exception
            '   Console.WriteLine(e.Message)
        Finally
        End Try
    End Sub

    ' Configura os eventos ao abrir o documento
    Public Sub ConectarEventos()
        swModel = SwApp.ActiveDoc
        If swModel IsNot Nothing Then
            ' Conecta eventos ao documento ativo
            AddHandler SwApp.ActiveDocChangeNotify, AddressOf OnActiveDocChange

        End If
    End Sub

    ' Evento disparado quando o documento ativo é alterado
    Private Function OnActiveDocChange() As Integer
        Console.WriteLine("Documento ativo foi alterado.")
        ' Atualiza o modelo atual
        swModel = SwApp.ActiveDoc
        Return 0
    End Function

    Sub DetachSWEvents()
        Try
            RemoveHandler iSwApp.ActiveDocChangeNotify, AddressOf Me.SldWorks_ActiveDocChangeNotify
            RemoveHandler iSwApp.DocumentLoadNotify2, AddressOf Me.SldWorks_DocumentLoadNotify2
            RemoveHandler iSwApp.FileNewNotify2, AddressOf Me.SldWorks_FileNewNotify2
            RemoveHandler iSwApp.ActiveModelDocChangeNotify, AddressOf Me.SldWorks_ActiveModelDocChangeNotify
            RemoveHandler iSwApp.FileOpenPostNotify, AddressOf Me.SldWorks_FileOpenPostNotify
            RemoveHandler iSwApp.FileCloseNotify, AddressOf Me.FileCloseNotify

            ' MsgBox(swModel.GetPathName)
        Catch e As Exception
            'Console.WriteLine(e.Message)
        Finally
        End Try
    End Sub

    Sub AttachEventsToAllDocuments()

        ' Obtemos o primeiro documento
        swModel = SwApp.GetFirstDocument()

        '' Verificamos se modDoc é Nothing
        'If swModel Is Nothing Then
        '    ' MsgBox("Nenhum documento aberto.")
        '    Return
        'End If


        ' Conectar ao SolidWorks
        IntanciaSolidWorks.ConectarSolidWorks()

        ' Obter o documento ativo
        swModel = SwApp.ActiveDoc

        ' Usa Select Case para diferenciar o tipo do documento
        If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Or swModel Is Nothing Then

            MyTaskPanelHost.txtAssuntoSubiTitulo.Clear()

            MyTaskPanelHost.cboTitulo.Text = ""
            MyTaskPanelHost.txtAssuntoSubiTitulo.Clear()
            MyTaskPanelHost.txtComentarios.Clear()
            MyTaskPanelHost.txtAuthor.Clear()
            MyTaskPanelHost.txtPalavraChave.Clear()

            MyTaskPanelHost.lblEspessura.Text = ""
            MyTaskPanelHost.lblLargura.Text = ""
            MyTaskPanelHost.lblComprimento.Text = ""

            MyTaskPanelHost.lblNumeroDobra.Text = ""
            MyTaskPanelHost.lblPeso.Text = ""
            MyTaskPanelHost.lblMaterial.Text = ""
            MyTaskPanelHost.lblAreaPintura.Text = ""

            MyTaskPanelHost.lblAlturaTotalCaixaDelimitadora.Text = ""
            MyTaskPanelHost.lblProfundidadeTotalCaixaDelimitadora.Text = ""
            MyTaskPanelHost.lblProfundidadeTotalCaixaDelimitadora.Text = ""

            'MyTaskPanelHost.cboAcabamento.Text = ""
            'MyTaskPanelHost.cboProcessoSoldagem.Text = ""
            ' MyTaskPanelHost.cboTipoDesenho.Text = ""
            ' MyTaskPanelHost.cboItemEstoque.Text = ""

            MyTaskPanelHost.chkCorte.Checked = False

            MyTaskPanelHost.chkDobra.Checked = False

            MyTaskPanelHost.chkSolda.Checked = False

            MyTaskPanelHost.chkPintura.Checked = False

            MyTaskPanelHost.chkPintura.Checked = False

            MyTaskPanelHost.chkMontagem.Checked = False

            MyTaskPanelHost.TimerMontaPeca.Enabled = True

        Else


            Try


                ' Mensagem com o caminho do documento

                '''''''''''  ShowTaskPane()

                DadosArquivoCorrente.ArquivoCorrente(swModel)

                'dados da caixa delimitadora
                DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

                DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel)

                MyTaskPanelHost.AtualizaTela(swModel)

                'Carrega a lista de materia para a peça ativa
                MyTaskPanelHost.TimerMontaPeca.Enabled = True

                'Carrega lista de ordem de serico que mostra quais Ordem de Servico contem esta peça
                MyTaskPanelHost.TimerFiltroPecaAtivaOS.Enabled = True

                ' DadosArquivoCorrente.AtualizaDesenho(swModel)
            Catch ex As Exception

                ' MsgBox("Erro geral: " & ex.Message)
            Finally
                ' Qualquer código de limpeza ou finalização vai aqui
            End Try

        End If


    End Sub

    Function AttachModelDocEventHandler(ByVal swModel As ModelDoc2) As Boolean
        If swModel Is Nothing Then
            Return False
        End If
        Dim docHandler As DocumentEventHandler = Nothing

        If Not openDocs.Contains(swModel) Then
            Select Case swModel.GetType
                Case swDocumentTypes_e.swDocPART

                    docHandler = New PartEventHandler()
                Case swDocumentTypes_e.swDocASSEMBLY
                    docHandler = New AssemblyEventHandler()
                Case swDocumentTypes_e.swDocDRAWING
                    docHandler = New DrawingEventHandler()
            End Select

            docHandler.Init(iSwApp, Me, swModel)
            docHandler.AttachEventHandlers()
            openDocs.Add(swModel, docHandler)
        End If
    End Function

    Sub DetachModelEventHandler(ByVal modDoc As ModelDoc2)
        Dim docHandler As DocumentEventHandler
        docHandler = openDocs.Item(modDoc)
        openDocs.Remove(modDoc)
        modDoc = Nothing
        docHandler = Nothing
    End Sub

#End Region

#Region "Event Handlers"

    Public Function OnSelectionChange(ByVal SwModel As ModelDoc2) As Integer
        Try
            ' Obter o documento ativo
            SwModel = SwApp.ActiveDoc
            If SwModel Is Nothing Then Return 0

            ' Obter o gerenciador de seleção
            Dim selectionMgr As SelectionMgr = SwModel.SelectionManager
            Dim selectedObj As Object = selectionMgr.GetSelectedObject6(1, -1)

            ' Verificar se há algo selecionado
            If selectedObj IsNot Nothing Then
                ' Verificar se é um componente de montagem
                If TypeOf selectedObj Is Component2 Then
                    Dim selectedComp As Component2 = CType(selectedObj, Component2)
                    LerDadosComponentes(selectedComp, 0, SwModel)
                ElseIf TypeOf selectedObj Is ModelDoc2 Then
                    ' Caso seja uma peça ou montagem
                    Dim selectedModel As ModelDoc2 = CType(selectedObj, ModelDoc2)
                    MessageBox.Show("Arquivo selecionado: " & selectedModel.GetTitle())
                End If
            Else
                MessageBox.Show("Nenhum objeto selecionado.")
            End If
        Catch ex As Exception
            MessageBox.Show("Erro ao processar a seleção: " & ex.Message)
        End Try

        ' Retornar 0 para continuar a execução normal do SolidWorks
        Return 0
    End Function

    Public Sub LerDadosArvoreMontagem(ByVal SwModel As ModelDoc2)
        Try
            ' Obter o documento ativo

            IntanciaSolidWorks.ConectarSolidWorks()
            ' swApparq = CreateObject("SldWorks.Application")

            SwModel = SwApp.ActiveDoc

            If SwModel Is Nothing Then
                MessageBox.Show("Nenhum documento ativo encontrado.")
                Return
            End If

            ' Verifica se o documento ativo é uma montagem
            If SwModel.GetType() <> swDocumentTypes_e.swDocASSEMBLY Then
                MessageBox.Show("O documento ativo não é uma montagem.")
                Return
            End If

            ' Obter a montagem raiz
            Dim assemblyDoc As AssemblyDoc = CType(SwModel, AssemblyDoc)
            Dim rootComponent As Component2 = assemblyDoc.GetRootComponent2(True)

            ' Função recursiva para percorrer e ler os dados dos componentes
            LerDadosComponentes(rootComponent, 0, SwModel)
        Catch ex As Exception
            ' MessageBox.Show("Erro ao ler os dados da montagem: " & ex.Message)
        Finally
        End Try
    End Sub

    ' Função recursiva para percorrer os componentes da montagem
    Private Sub LerDadosComponentes(ByVal comp As Component2, ByVal nivel As Integer, ByVal swModel As ModelDoc2)
        Try
            If comp Is Nothing Then Return

            swModel = comp.GetModelDoc2()
            Dim filePath As String = comp.GetPathName()
            Dim name As String = comp.Name2

            ' Exibir informações do componente
            MessageBox.Show("Nível: " & nivel.ToString() & vbCrLf & "Nome: " & name & vbCrLf & "Caminho: " & filePath)

            ' Obter os filhos (subcomponentes) do componente atual
            Dim subComponents As Object() = comp.GetChildren()

            ' Se houver subcomponentes, chamar a função recursiva para cada um deles
            If subComponents IsNot Nothing Then
                For Each child As Component2 In subComponents
                    LerDadosComponentes(child, nivel + 1, swModel)
                Next
            End If
        Catch ex As Exception
            '  MessageBox.Show("Erro ao ler os dados do componente: " & ex.Message)
        Finally
        End Try
    End Sub

    Function SldWorks_ActiveDocChangeNotify() As Integer

        AttachEventsToAllDocuments()

        ' Verifique se o modelo foi aberto com sucesso
        If Not swModel Is Nothing Then

            ' Usa Select Case para diferenciar o tipo do documento
            If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                MyTaskPanelHost.txtAssuntoSubiTitulo.Clear()

                MyTaskPanelHost.cboTitulo.Text = ""
                MyTaskPanelHost.txtAssuntoSubiTitulo.Clear()
                MyTaskPanelHost.txtComentarios.Clear()
                MyTaskPanelHost.txtAuthor.Clear()
                MyTaskPanelHost.txtPalavraChave.Clear()

                MyTaskPanelHost.lblEspessura.Text = ""
                MyTaskPanelHost.lblLargura.Text = ""
                MyTaskPanelHost.lblComprimento.Text = ""

                MyTaskPanelHost.lblNumeroDobra.Text = ""
                MyTaskPanelHost.lblPeso.Text = ""
                MyTaskPanelHost.lblMaterial.Text = ""
                MyTaskPanelHost.lblAreaPintura.Text = ""

                MyTaskPanelHost.lblAlturaTotalCaixaDelimitadora.Text = ""
                MyTaskPanelHost.lblProfundidadeTotalCaixaDelimitadora.Text = ""
                MyTaskPanelHost.lblProfundidadeTotalCaixaDelimitadora.Text = ""

                'MyTaskPanelHost.cboAcabamento.Text = ""
                'MyTaskPanelHost.cboProcessoSoldagem.Text = ""
                ' MyTaskPanelHost.cboTipoDesenho.Text = ""
                ' MyTaskPanelHost.cboItemEstoque.Text = ""

                MyTaskPanelHost.chkCorte.Checked = False

                MyTaskPanelHost.chkDobra.Checked = False

                MyTaskPanelHost.chkSolda.Checked = False

                MyTaskPanelHost.chkPintura.Checked = False

                MyTaskPanelHost.chkPintura.Checked = False

                MyTaskPanelHost.chkMontagem.Checked = False

                MyTaskPanelHost.TimerMontaPeca.Enabled = True

            End If

        End If

        ' DadosArquivoCorrente.AtualizaDesenho()

        'TODO: Add your implementation here
    End Function

    Function SldWorks_DocumentLoadNotify2(ByVal docTitle As String, ByVal docPath As String) As Integer

    End Function

    Function SldWorks_FileNewNotify2(ByVal newDoc As Object, ByVal doctype As Integer, ByVal templateName As String) As Integer
        '''''''''''''''''''''''''''''''''''''''  AttachEventsToAllDocuments()

    End Function

    Function SldWorks_ActiveModelDocChangeNotify() As Integer

        'TODO: Add your implementation here
        'MsgBox("Arquivo Selecionado Alterado")

        'IntanciaSolidWorks.ConectarSolidWorks()
        'DadosArquivoCorrente.LendoDadosComunsPartAssembly()
        ''Lista de corte

        'If swModel.GetType() = swDocumentTypes_e.swDocPART Then
        '    DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte()
        'End If

        ''dados da caixa delimitadora
        'DadosArquivoCorrente.LerDadosCaixaDelimitadora()

        '' DadosArquivoCorrente.VerificarProcessodaPecaCorrente()

        ''Função para Salvar no Banco de dados
        'DadosArquivoCorrente.AtualizaDesenho()

    End Function

    Function SldWorks_FileOpenPostNotify(ByVal FileName As String) As Integer
        '''' AttachEventsToAllDocuments()
        ' MsgBox("Arquivo Selecionado Alterado")
    End Function

#End Region

#Region "UI Callbacks"

    Sub CreateCube()

        'make sure we have a part open
        Dim partTemplate As String
        Dim model As ModelDoc2
        Dim featMan As FeatureManager

        partTemplate = iSwApp.GetUserPreferenceStringValue(swUserPreferenceStringValue_e.swDefaultTemplatePart)
        If Not partTemplate = "" Then
            model = iSwApp.NewDocument(partTemplate, swDwgPaperSizes_e.swDwgPaperA2size, 0.0, 0.0)

            model.InsertSketch2(True)
            model.SketchRectangle(0, 0, 0, 0.1, 0.1, 0.1, False)

            'Extrude the sketch
            featMan = model.FeatureManager
            featMan.FeatureExtrusion(True,
                                      False, False,
                                      swEndConditions_e.swEndCondBlind, swEndConditions_e.swEndCondBlind,
                                      0.1, 0.0,
                                      False, False,
                                      False, False,
                                      0.0, 0.0,
                                      False, False,
                                      False, False,
                                      True,
                                      False, False)
        Else
            System.Windows.Forms.MessageBox.Show("There is no part template available. Please check your options and make sure there is a part template selected, or select a new part template.")
        End If
    End Sub

    Sub PopupCallbackFunction()
        'bRet = iSwApp.ShowThirdPartyPopupMenu(registerID, 500, 500)
    End Sub

    Function PopupEnable() As Integer
        If iSwApp.ActiveDoc Is Nothing Then
            PopupEnable = 0
        Else
            PopupEnable = 1
        End If
    End Function

    Sub TestCallback()
        Debug.Print("Test callback")
    End Sub

    Function EnableTest() As Integer
        If iSwApp.ActiveDoc Is Nothing Then
            EnableTest = 0
        Else
            EnableTest = 1
        End If
    End Function

    Sub ShowPMP()
        If Not ppage Is Nothing Then
            ppage.Show()
        End If
    End Sub

    Function PMPEnable() As Integer
        If iSwApp.ActiveDoc Is Nothing Then
            PMPEnable = 0
        Else
            PMPEnable = 1
        End If
    End Function

    Sub FlyoutCallback()

        Dim flyGroup As FlyoutGroup = iCmdMgr.GetFlyoutGroup(flyoutGroupID)
        flyGroup.RemoveAllCommandItems()

        flyGroup.AddCommandItem(System.DateTime.Now.ToLongTimeString(), "test", 0, "FlyoutCommandItem1", "FlyoutEnableCommandItem1")

    End Sub

    Function FlyoutEnable() As Integer
        Return 1
    End Function

    Sub FlyoutCommandItem1()
        iSwApp.SendMsgToUser("Flyout command 1")
    End Sub

    Function FlyoutEnableCommandItem1() As Integer
        Return 1
    End Function

    Public Function FileOpenNotify(FileName As String) As Integer Implements DSldWorksEvents.FileOpenNotify
        Throw New NotImplementedException()
    End Function

    Public Function FileNewNotify(NewDoc As Object, DocType As Integer) As Integer Implements DSldWorksEvents.FileNewNotify
        Throw New NotImplementedException()
    End Function

    Public Function DestroyNotify() As Integer Implements DSldWorksEvents.DestroyNotify
        Throw New NotImplementedException()
    End Function

    Public Function ActiveDocChangeNotify() As Integer Implements DSldWorksEvents.ActiveDocChangeNotify
        Throw New NotImplementedException()
    End Function

    Public Function ActiveModelDocChangeNotify() As Integer Implements DSldWorksEvents.ActiveModelDocChangeNotify
        Throw New NotImplementedException()
    End Function

    Public Function PropertySheetCreateNotify(Sheet As Object, sheetType As Integer) As Integer Implements DSldWorksEvents.PropertySheetCreateNotify
        Throw New NotImplementedException()
    End Function

    Public Function NonNativeFileOpenNotify(FileName As String) As Integer Implements DSldWorksEvents.NonNativeFileOpenNotify
        Throw New NotImplementedException()
    End Function

    Public Function LightSheetCreateNotify(NewSheet As Object, sheetType As Integer, LightId As Integer) As Integer Implements DSldWorksEvents.LightSheetCreateNotify
        Throw New NotImplementedException()
    End Function

    Public Function DocumentConversionNotify(FileName As String) As Integer Implements DSldWorksEvents.DocumentConversionNotify
        Throw New NotImplementedException()
    End Function

    Public Function DocumentLoadNotify(docTitle As String, docPath As String) As Integer Implements DSldWorksEvents.DocumentLoadNotify
        Throw New NotImplementedException()
    End Function

    Public Function FileNewNotify2(NewDoc As Object, DocType As Integer, TemplateName As String) As Integer Implements DSldWorksEvents.FileNewNotify2
        Throw New NotImplementedException()
    End Function

    Public Function FileOpenNotify2(FileName As String) As Integer Implements DSldWorksEvents.FileOpenNotify2
        Throw New NotImplementedException()
    End Function

    Public Function ReferenceNotFoundNotify(FileName As String) As Integer Implements DSldWorksEvents.ReferenceNotFoundNotify
        Throw New NotImplementedException()
    End Function

    Public Function PromptForFilenameNotify(openOrSave As Integer, suggestedFileName As String, DocType As Integer, cause As Integer) As Integer Implements DSldWorksEvents.PromptForFilenameNotify
        Throw New NotImplementedException()
    End Function

    Public Function BeginTranslationNotify(FileName As String, Options As Integer) As Integer Implements DSldWorksEvents.BeginTranslationNotify
        Throw New NotImplementedException()
    End Function

    Public Function EndTranslationNotify(FileName As String, Options As Integer) As Integer Implements DSldWorksEvents.EndTranslationNotify
        Throw New NotImplementedException()
    End Function

    Public Function OnIdleNotify() As Integer Implements DSldWorksEvents.OnIdleNotify
        Throw New NotImplementedException()
    End Function

    Public Function FileOpenPreNotify(FileName As String) As Integer Implements DSldWorksEvents.FileOpenPreNotify
        Throw New NotImplementedException()
    End Function

    Public Function FileOpenPostNotify(FileName As String) As Integer Implements DSldWorksEvents.FileOpenPostNotify
        Throw New NotImplementedException()
    End Function

    Public Function ReferencedFilePreNotify(FileName As String) As Integer Implements DSldWorksEvents.ReferencedFilePreNotify
        Throw New NotImplementedException()
    End Function

    Public Function BeginRecordNotify() As Integer Implements DSldWorksEvents.BeginRecordNotify
        Throw New NotImplementedException()
    End Function

    Public Function EndRecordNotify() As Integer Implements DSldWorksEvents.EndRecordNotify
        Throw New NotImplementedException()
    End Function

    Public Function FileNewPreNotify(DocType As Integer, TemplateName As String) As Integer Implements DSldWorksEvents.FileNewPreNotify
        Throw New NotImplementedException()
    End Function

    Public Function JournalWriteNotify(journalFile As String, LineCount As Integer) As Integer Implements DSldWorksEvents.JournalWriteNotify
        Throw New NotImplementedException()
    End Function

    Private Function DSldWorksEvents_DocumentLoadNotify2(docTitle As String, docPath As String) As Integer Implements DSldWorksEvents.DocumentLoadNotify2
        Throw New NotImplementedException()

    End Function

    Public Function CommandCloseNotify(Command As Integer, reason As Integer) As Integer Implements DSldWorksEvents.CommandCloseNotify
        Throw New NotImplementedException()
    End Function

    Public Function PromptForMultipleFileNamesNotify(openOrSave As Integer, ByRef suggestedFileNames As Object, ByRef DocTypes As Object, cause As Integer) As Integer Implements DSldWorksEvents.PromptForMultipleFileNamesNotify
        Throw New NotImplementedException()
    End Function

    Public Function CommandOpenPreNotify(Command As Integer, UserCommand As Integer) As Integer Implements DSldWorksEvents.CommandOpenPreNotify
        Throw New NotImplementedException()
    End Function

    'Public Function FileCloseNotify(FileName As String, reason As Integer) As Integer Implements DSldWorksEvents.FileCloseNotify
    '    Throw New NotImplementedException()
    'End Function

    Public Function BackgroundProcessingStartNotify(FileName As String) As Integer Implements DSldWorksEvents.BackgroundProcessingStartNotify
        Throw New NotImplementedException()
    End Function

    Public Function BackgroundProcessingEndNotify(FileName As String) As Integer Implements DSldWorksEvents.BackgroundProcessingEndNotify
        Throw New NotImplementedException()
    End Function

    Public Function InterfaceBrightnessThemeChangeNotify(ThemeType As Integer, ByRef Colors As Object) As Integer Implements DSldWorksEvents.InterfaceBrightnessThemeChangeNotify
        Throw New NotImplementedException()
    End Function

    Public Function ReferencedFilePreNotify2(FileName As String, FileStatus As Integer) As Integer Implements DSldWorksEvents.ReferencedFilePreNotify2
        Throw New NotImplementedException()
    End Function

    Public Function Begin3DInterconnectTranslationNotify(FileName As String) As Integer Implements DSldWorksEvents.Begin3DInterconnectTranslationNotify
        Throw New NotImplementedException()
    End Function

    Public Function End3DInterconnectTranslationNotify(FileName As String) As Integer Implements DSldWorksEvents.End3DInterconnectTranslationNotify
        Throw New NotImplementedException()
    End Function

#End Region

End Class