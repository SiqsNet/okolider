Public Class Dysk
    Dim zasoby1 As String

    Private Sub Dysk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'czytanie konfiguracji

        On Error Resume Next
        Dim doc As New System.Xml.XmlDocument
        doc.Load("konfig.xml")
        Dim konf1 = doc.GetElementsByTagName("zasoby1")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf1
            ' tutaj wykonac akcje po wczytaniu sekcji
            On Error Resume Next
            zasoby1 = item.InnerText
        Next
        On Error Resume Next
        WebBrowser1.Navigate(zasoby1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        On Error Resume Next
        WebBrowser1.GoBack()
    End Sub

End Class