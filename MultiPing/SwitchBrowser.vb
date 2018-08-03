Public Class SwitchBrowser
    Public Sub SetURL(ByVal url As String)
        Browser.Url = New Uri(url)
    End Sub
End Class