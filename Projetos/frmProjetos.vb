Public Class frmProjetos
    Private Sub frmProjetos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            cl_BancoDados.ComboBoxDataSet("pessoajuridica", "idpessoa", "nomefantasia", cboCliente, "")

        Catch ex As Exception
        Finally
        End Try

    End Sub
End Class