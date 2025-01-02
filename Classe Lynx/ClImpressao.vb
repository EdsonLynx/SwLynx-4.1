Public Class ClImpressao

    Public Function Imprimirarquivo(ByVal ArquivoParaImpressao As String)

        Try

            Dim info = New ProcessStartInfo
            Dim p = New Process()
            info.Verb = "print"
            info.FileName = ArquivoParaImpressao
            'MsgBox(ArquivoParaImpressao)

            info.CreateNoWindow = True
            info.WindowStyle = ProcessWindowStyle.Hidden
            p.StartInfo = info
            p.Start()
            p.WaitForInputIdle()
            ' System.Threading.Thread.Sleep(3000)
            ' p.Dispose()
        Catch ex As Exception
        Finally
        End Try

        Imprimirarquivo = False
    End Function

End Class
