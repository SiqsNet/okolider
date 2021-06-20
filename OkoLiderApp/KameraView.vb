Imports System.IO
Imports System.Xml
Imports System.Threading



Public Class Form1
    Dim plikjpg As String
    Dim adreskamery1 As String
    Dim adreskamery2 As String
    Dim zasoby1 As String
    Dim przycisk1 As String
    Dim aktualizacja As String
    Dim adresaktualizacji As String
    Dim pomoc As String
    Dim zapisjpg As String
    Dim zapisjpginterwal As String
    Dim sciezkazapisu As String
    Dim login As String
    Dim haslo As String
    Dim trybpracy As String
    Dim programdoaktualizacji As String

    Dim kamerainfo As String

    Dim kamera1 As String
    Dim kamera2 As String
    Dim kamera3 As String
    Dim kamera4 As String
    Dim kamera5 As String
    Dim kamera6 As String
    Dim kamera7 As String
    Dim kamera8 As String

    Dim kamera1plik As String
    Dim kamera2plik As String
    Dim kamera3plik As String
    Dim kamera4plik As String
    Dim kamera5plik As String
    Dim kamera6plik As String
    Dim kamera7plik As String
    Dim kamera8plik As String

    Dim kamera1hint As String
    Dim kamera2hint As String
    Dim kamera3hint As String
    Dim kamera4hint As String
    Dim kamera5hint As String
    Dim kamera6hint As String
    Dim kamera7hint As String
    Dim kamera8hint As String

    Dim pierwszeuruchomienie As String
    Dim kolejneuruchomienie As String
    Dim adresarchiwizacji As String

    'watek
    Dim timer_save_jpg As Thread
    Public zapis_jpg_watek As System.Threading.TimerCallback
    Public zapis_jpg_timer As System.Threading.Timer

    '### Hello, my name Is Piotr And this application Is the answer to the need to have a tool that supports 
    '### camera that stream JPG Or RTMP ###

    '### The names Of variables And procedures are written In PL - don't ask why, this is what I am :) ###

    Private Sub form1_load(sender As Object, e As EventArgs) Handles MyBase.Load
        startuje_program()
    End Sub

    Sub startuje_program()

        'zaladowanie xml
        zaladowaniexml()

        'zaladowanie kamera1
        kamerainfo = kamera1plik
        czytanie_kamera_xml()

        'widzialnosc przyciskow od kamer

        If kamera1 = "false" Then
            Button10.Enabled = False
        End If

        If kamera2 = "false" Then
            Button11.Enabled = False
        End If

        If kamera3 = "false" Then
            Button12.Enabled = False
        End If

        If kamera4 = "false" Then
            Button13.Enabled = False
        End If

        If kamera5 = "false" Then
            Button14.Enabled = False
        End If

        If kamera6 = "false" Then
            Button15.Enabled = False
        End If

        If kamera7 = "false" Then
            Button16.Enabled = False
        End If

        If kamera8 = "false" Then
            Button17.Enabled = False
        End If

        'widzialnosc przegladarki dysku
        If przycisk1 = "false" Then
            Button5.Enabled = False
            PrzeglądarkaZasobówNaDyskuSDToolStripMenuItem.Enabled = False
            PrzeglądajZasobyNaKarcieSDToolStripMenuItem.Enabled = False
        End If

        'sprawdzenie aktualizacji
        aktualizacja_programu()

        'ToolTip1.SetToolTip(Button3, nazwaprzycisku2)
        Label1.Text = "Obraz: " + Button10.Text + " (" + kamera1hint + ")"

        'startuje przeglad obrazu
        PictureBox1.Show()

        'widzialnosc przegladarki wylaczona - definiowana plikiem cfg
        WebBrowser1.Visible = False

        'wycentrowanie obrazu
        'Me.PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
        'lub zoomwanie obrazu
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        'timer_save_jpg = New Thread(AddressOf Me.zaladowaniejpg)
        'timer_save_jpg.Start()

        Timer1.Interval = 1200
        Timer1.Start()

        '!!!! jezeli brak pliku cfg pokaz about blank i wyswietl alert !!!! 

        Timer2.Stop()

        'wylaczenie timer3
        Timer3.Stop()

        'czy ma byc vlc stream czy jpg stream?
        trybpracyprogramu()

        'weryfikacja autosave jpg
        If zapisjpg = True Then
            Timer2.Interval = zapisjpginterwal * 1000
            Timer2.Start()
        End If
    End Sub

    Private Sub AxVLCPlugin21_MediaPlayerEncounteredError(sender As Object, e As EventArgs) Handles AxVLCPlugin21.MediaPlayerEncounteredError
        MsgBox("Błąd podczas łączenia z Kamerą!", MsgBoxStyle.Exclamation)
    End Sub

    Sub zaladowaniejpg()
        On Error Resume Next
        'save to jpg
        My.Computer.Network.DownloadFile(address:=adreskamery1, destinationFileName:=sciezkazapisu, userName:=login, password:=haslo, showUI:=False, connectionTimeout:=10000000, overwrite:=True)
        System.Threading.Thread.Sleep(100)

        On Error Resume Next
        'odczyt pliku kamera.jpg
        PictureBox1.ImageLocation = (sciezkazapisu)
    End Sub

    Sub zaladowaniexml()

        '-----------------------------------------------------------------------------------------------'

        ' czytanie konfiguracji start xml

        '-----------------------------------------------------------------------------------------------'

        On Error Resume Next
        Dim doc100 As New System.Xml.XmlDocument
        doc100.Load("start.xml")
        Dim konf100 = doc100.GetElementsByTagName("pierwszeuruchomienie")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf100
            ' tutaj wykonac akcje po wczytaniu sekcji
            pierwszeuruchomienie = item.InnerText
        Next

        Dim doc101 As New System.Xml.XmlDocument
        doc101.Load("start.xml")
        Dim konf101 = doc101.GetElementsByTagName("adresarchiwizacji")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf101
            ' tutaj wykonac akcje po wczytaniu sekcji
            adresarchiwizacji = item.InnerText
        Next

        'operacja sprawdzenia pliku konfig.xml

        If pierwszeuruchomienie = "0" Then
            My.Computer.FileSystem.CopyFile(adresarchiwizacji, "konfig.xml", overwrite:=True)
            'wykonal wlasnie operacje kopiowania pliku xml
            Call startujepoinstalacji()
        End If


        '-----------------------------------------------------------------------------------------------'

        ' czytanie konfiguracji konfig xml

        '-----------------------------------------------------------------------------------------------'
        On Error Resume Next
        Dim doc As New System.Xml.XmlDocument
        On Error Resume Next
        doc.Load("konfig.xml")

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf6 = doc.GetElementsByTagName("przycisk1")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf6
            ' tutaj wykonac akcje po wczytaniu sekcji
            przycisk1 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf8 = doc.GetElementsByTagName("aktualizacja")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf8
            ' tutaj wykonac akcje po wczytaniu sekcji
            aktualizacja = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf9 = doc.GetElementsByTagName("adresaktualizacji")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf9
            ' tutaj wykonac akcje po wczytaniu sekcji
            adresaktualizacji = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf10 = doc.GetElementsByTagName("pomoc")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf10
            ' tutaj wykonac akcje po wczytaniu sekcji
            pomoc = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf11 = doc.GetElementsByTagName("zapisjpg")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf11
            ' tutaj wykonac akcje po wczytaniu sekcji
            zapisjpg = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf12 = doc.GetElementsByTagName("zapisjpginterwal")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf12
            ' tutaj wykonac akcje po wczytaniu sekcji
            zapisjpginterwal = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf13 = doc.GetElementsByTagName("sciezkazapisu")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf13
            ' tutaj wykonac akcje po wczytaniu sekcji
            sciezkazapisu = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf40 = doc.GetElementsByTagName("programdoaktualizacji")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf40
            ' tutaj wykonac akcje po wczytaniu sekcji
            programdoaktualizacji = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf15 = doc.GetElementsByTagName("kamera1")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf15
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera1 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf16 = doc.GetElementsByTagName("kamera2")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf16
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera2 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf17 = doc.GetElementsByTagName("kamera3")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf17
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera3 = item.InnerText
        Next


        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf18 = doc.GetElementsByTagName("kamera4")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf18
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera4 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf19 = doc.GetElementsByTagName("kamera5")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf19
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera5 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf20 = doc.GetElementsByTagName("kamera6")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf20
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera6 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf21 = doc.GetElementsByTagName("kamera7")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf21
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera7 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf22 = doc.GetElementsByTagName("kamera8")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf22
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera8 = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf23 = doc.GetElementsByTagName("kamera1plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf23
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera1plik = item.InnerText
        Next


        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf24 = doc.GetElementsByTagName("kamera2plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf24
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera2plik = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf25 = doc.GetElementsByTagName("kamera3plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf25
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera3plik = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf26 = doc.GetElementsByTagName("kamera4plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf26
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera4plik = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf27 = doc.GetElementsByTagName("kamera5plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf27
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera5plik = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf28 = doc.GetElementsByTagName("kamera6plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf28
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera6plik = item.InnerText
        Next


        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf30 = doc.GetElementsByTagName("kamera7plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf30
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera7plik = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf31 = doc.GetElementsByTagName("kamera8plik")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf31
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera8plik = item.InnerText
        Next

        ' tootipy do kamer

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf32 = doc.GetElementsByTagName("kamera1hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf32
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera1hint = item.InnerText
        Next


        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf33 = doc.GetElementsByTagName("kamera2hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf33
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera2hint = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf34 = doc.GetElementsByTagName("kamera3hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf34
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera3hint = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf35 = doc.GetElementsByTagName("kamera4hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf35
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera4hint = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf36 = doc.GetElementsByTagName("kamera5hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf36
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera5hint = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf37 = doc.GetElementsByTagName("kamera6hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf37
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera6hint = item.InnerText
        Next


        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf38 = doc.GetElementsByTagName("kamera7hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf38
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera7hint = item.InnerText
        Next

        On Error Resume Next
        doc.Load("konfig.xml")
        Dim konf39 = doc.GetElementsByTagName("kamera8hint")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf39
            ' tutaj wykonac akcje po wczytaniu sekcji
            kamera8hint = item.InnerText
        Next


        'okreslenie tooltipow

        ToolTip1.SetToolTip(Button10, kamera1hint)
        ToolTip1.SetToolTip(Button11, kamera2hint)
        ToolTip1.SetToolTip(Button12, kamera3hint)
        ToolTip1.SetToolTip(Button13, kamera4hint)
        ToolTip1.SetToolTip(Button14, kamera5hint)
        ToolTip1.SetToolTip(Button15, kamera6hint)
        ToolTip1.SetToolTip(Button16, kamera7hint)
        ToolTip1.SetToolTip(Button17, kamera8hint)

    End Sub

    Sub czytanie_kamera_xml()

        '-----------------------------------------------------------------------------------------------'

        ' czytanie konfiguracji kamera1 xml

        '-----------------------------------------------------------------------------------------------'


        On Error Resume Next
        Dim doc As New System.Xml.XmlDocument
        doc.Load(kamerainfo)
        Dim konf1 = doc.GetElementsByTagName("adreskamery1")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf1
            ' tutaj wykonac akcje po wczytaniu sekcji
            adreskamery1 = item.InnerText
        Next

        On Error Resume Next
        Dim konf3 = doc.GetElementsByTagName("adreskamery2")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf3
            ' tutaj wykonac akcje po wczytaniu sekcji
            adreskamery2 = item.InnerText
        Next

        On Error Resume Next
        Dim konf4 = doc.GetElementsByTagName("zasoby1")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf4
            ' tutaj wykonac akcje po wczytaniu sekcji
            zasoby1 = item.InnerText
        Next

        On Error Resume Next
        doc.Load(kamerainfo)
        Dim konf5 = doc.GetElementsByTagName("login")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf5
            ' tutaj wykonac akcje po wczytaniu sekcji
            login = item.InnerText
        Next

        On Error Resume Next
        doc.Load(kamerainfo)
        Dim konf6 = doc.GetElementsByTagName("haslo")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf6
            ' tutaj wykonac akcje po wczytaniu sekcji
            haslo = item.InnerText
        Next

        On Error Resume Next
        doc.Load(kamerainfo)
        Dim konf7 = doc.GetElementsByTagName("trybpracy")
        On Error Resume Next
        For Each item As System.Xml.XmlElement In konf7
            ' tutaj wykonac akcje po wczytaniu sekcji
            trybpracy = item.InnerText
        Next
    End Sub

    Sub startujepoinstalacji()


        '-----------------------------------------------------------------------------------------------'

        ' dodaje do start xml informacje 0 lub 1

        '-----------------------------------------------------------------------------------------------'


        On Error Resume Next
        kolejneuruchomienie = "1"

        'sprawdzenie czy bylo pierwsze uruchomienie
        Dim settings As New XmlWriterSettings()
        settings.Indent = True

        ' xml writter
        Dim XmlWrt As XmlWriter = XmlWriter.Create("start.xml", settings)
        On Error Resume Next
        With XmlWrt

            ' Write the Xml declaration.
            .WriteStartDocument()

            ' Write a comment.
            .WriteComment("XML Database.")

            ' Write the root element.
            .WriteStartElement("Adresy")

            ' The person nodes.

            .WriteStartElement("pierwszeuruchomienie")
            .WriteString(kolejneuruchomienie.ToString())
            .WriteEndElement()

            .WriteStartElement("adresarchiwizacji")
            .WriteString(adresarchiwizacji.ToString())
            .WriteEndElement()

            ' The end of this person.
            '.WriteEndElement()

            ' Close the XmlTextWriter.
            .WriteEndDocument()
            .Close()

        End With

        'zapisuje plik xml
        My.Computer.FileSystem.CopyFile("konfig.xml", adresarchiwizacji, overwrite:=True)
    End Sub

    Sub aktualizacja_programu()

        '-----------------------------------------------------------------------------------------------'

        ' aktualizacja programu

        '-----------------------------------------------------------------------------------------------'
        On Error Resume Next

        If aktualizacja = "true" Then
            On Error Resume Next
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://pomoclider.pl/kameraview/version.txt")
            On Error Resume Next
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            On Error Resume Next
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            On Error Resume Next
            Dim newestversion As String = sr.ReadToEnd()
            On Error Resume Next
            'sprawdzanie wersji aplikacji w assembly
            'Dim currentversion As String = Application.ProductVersion
            'ja sprawdzam w label3 wersje aplikacji
            Dim currentversion As String = Label3.Text
            If newestversion.Contains(currentversion) Then
                'oznacza ze wszystko jest aktualne
            Else
                'czyli cos jest nie aktualne
                If MessageBox.Show("Jest nowa wersja programu, czy chcesz pobrać nową wersje KameraView?", "Aktualizacja KameraView", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    'sciaganie nowej wersji programu
                    'WebBrowser2.Navigate(adresaktualizacji)
                    Process.Start(programdoaktualizacji)
                    'wylaczenie programu
                    Application.ExitThread()
                End If
            End If
        End If
    End Sub

    Sub trybpracyprogramu()

        '-----------------------------------------------------------------------------------------------'

        ' tryb pracy programu

        '-----------------------------------------------------------------------------------------------'

        If trybpracy = "1" Then
            PictureBox1.Visible = True
            AxVLCPlugin21.Visible = False
            WebBrowser1.Visible = False

            'startuje przeglad obrazu
            PictureBox1.Show()

            'wycentrowanie obrazu
            'Me.PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
            'lub zoomwanie obrazu
            Me.PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

            'timer_save_jpg = New Thread(AddressOf Me.zaladowaniejpg)
            'timer_save_jpg.Start()

            Timer1.Interval = 1200
            Timer1.Start()

        End If

        If trybpracy = "2" Then
            PictureBox1.Visible = False
            AxVLCPlugin21.Visible = True
            WebBrowser1.Visible = False
            ' wylaczam jpg stream
            Timer1.Stop()
            Timer3.Stop()
            Timer4.Stop()
            ' wlaczam vlc stream
            vlcplayer()
        End If

        If trybpracy = "3" Then
            PictureBox1.Visible = False
            AxVLCPlugin21.Visible = False
            WebBrowser1.Visible = True
            ' wylaczam jpg stream
            Timer1.Stop()
            Timer3.Stop()
            ' wlaczam webbrowser
            If adreskamery1 = "" Then
                adreskamery1 = "about_blank"
                WebBrowser1.Refresh()
                MsgBox("Uwaga, brak prawidłowego pliku konfiguracyjnego!", MsgBoxStyle.Critical)
            End If

            ' w innym przypadku zaladuj konfiguracje
            On Error Resume Next
            WebBrowser1.Navigate(adreskamery1)
            On Error Resume Next
            Timer4.Enabled = True
            Timer4.Interval = 1000
            Timer4.Start()
            On Error Resume Next
        End If
    End Sub


    Sub vlcplayer()
        'start playera
        On Error Resume Next
        AxVLCPlugin21.playlist.items.clear()
        On Error Resume Next
        AxVLCPlugin21.playlist.add(adreskamery2)
        On Error Resume Next
        AxVLCPlugin21.playlist.play()
        On Error Resume Next
    End Sub

    Sub zapiszdjecia()

        'Timer1.Stop()
        On Error Resume Next
        'save pliku jpg
        'WebBrowser1.Navigate(strona)
        'WebBrowser1.ShowSaveAsDialog()
        On Error Resume Next
        'Timer1.Start()
        ' zapis obrazu z kamery na dysk komputera jako...


        SaveFileDialog1.Title = "Zapisz plik-zdjęcie jako"
        SaveFileDialog1.InitialDirectory = "C:\"
        SaveFileDialog1.Filter = "Plik obrazu (*.jpg)|*.jpg|Wszystkie (*.*)|*.*"
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.RestoreDirectory = True
        PictureBox1.ImageLocation = ("C:\")
        On Error Resume Next
        If (SaveFileDialog1.ShowDialog() = DialogResult.OK) Then
            On Error Resume Next
            If File.Exists(SaveFileDialog1.FileName) Then
                MsgBox("Nie można nadpisać istniejącego pliku obrazu. Wpisz inną nazwe pliku. ", MsgBoxStyle.Critical)
                On Error Resume Next
                If (SaveFileDialog1.ShowDialog() = DialogResult.OK) Then
                    On Error Resume Next
                    PictureBox1.Image.Save(SaveFileDialog1.FileName())
                End If
            End If
            PictureBox1.Image.Save(SaveFileDialog1.FileName())
        Else
            MsgBox("Prawdopodobny błąd podczas zapisu pliku.", MsgBoxStyle.Critical)

        End If
    End Sub

    Sub archiwizacjaxml()


        '-----------------------------------------------------------------------------------------------'

        ' archiwizacja pliku xml

        '-----------------------------------------------------------------------------------------------'

        On Error Resume Next
        Dim currentDirName As String
        currentDirName = System.IO.Directory.GetCurrentDirectory()
        ' Gdy w folderze programu brak folderu Backup 
        If Not (System.IO.Directory.Exists(currentDirName + "\Backup\")) Then
            ' Tworzy go...
            System.IO.Directory.CreateDirectory(currentDirName + "\Backup\")
            ' i kopiuje plik konfig.xml oraz kamery XML
            My.Computer.FileSystem.CopyFile("konfig.xml", currentDirName + "\Backup\konfig.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera1.xml", currentDirName + "\Backup\kamera1.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera2.xml", currentDirName + "\Backup\kamera2.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera3.xml", currentDirName + "\Backup\kamera3.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera4.xml", currentDirName + "\Backup\kamera4.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera5.xml", currentDirName + "\Backup\kamera5.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera6.xml", currentDirName + "\Backup\kamera6.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera7.xml", currentDirName + "\Backup\kamera7.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera8.xml", currentDirName + "\Backup\kamera8.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("konfigsave.xml", currentDirName + "\Backup\konfigsave.xml", overwrite:=True)

            If (System.IO.File.Exists(currentDirName + "\Backup\konfig.xml")) Then
                MsgBox("Plik zapisany w: " + currentDirName + "\Backup", MsgBoxStyle.Information)
            Else
                MsgBox("Prawdopodobny błąd podczas zapisu w: " + currentDirName + "\Backup", MsgBoxStyle.Information)
            End If
        Else
            ' W innym przypadku kopiuje plik konfig.xml...
            My.Computer.FileSystem.CopyFile("konfig.xml", currentDirName + "\Backup\konfig.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera1.xml", currentDirName + "\Backup\kamera1.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera2.xml", currentDirName + "\Backup\kamera2.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera3.xml", currentDirName + "\Backup\kamera3.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera4.xml", currentDirName + "\Backup\kamera4.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera5.xml", currentDirName + "\Backup\kamera5.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera6.xml", currentDirName + "\Backup\kamera6.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera7.xml", currentDirName + "\Backup\kamera7.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("kamera8.xml", currentDirName + "\Backup\kamera8.xml", overwrite:=True)
            On Error Resume Next
            My.Computer.FileSystem.CopyFile("konfigsave.xml", currentDirName + "\Backup\konfigsave.xml", overwrite:=True)

            If (System.IO.File.Exists(currentDirName + "\Backup\konfig.xml")) Then
                MsgBox("Plik zapisany w: " + currentDirName + "\Backup", MsgBoxStyle.Information)
            Else
                MsgBox("Prawdopodobny błąd podczas zapisu w: " + currentDirName + "\Backup", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        '-----------------------------------------------------------------------------------------------'

        ' sciaganie co sekunde pliku jpg

        '-----------------------------------------------------------------------------------------------'

        On Error Resume Next

        timer_save_jpg = New Thread(AddressOf Me.zaladowaniejpg)
        timer_save_jpg.Start()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        On Error Resume Next
        Dim time As String
        Dim rozszerzenie As String
        Dim plik As String

        rozszerzenie = ".jpg"

        time = Format(Now, "dd-MM-yyyy--HH-mm-ss")
        plik = sciezkazapisu + ("-") + time + rozszerzenie

        My.Computer.Network.DownloadFile(adreskamery1, plik, login, haslo)
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        On Error Resume Next
        'odczyt pliku kamera.jpg, obecnie nie dziala. 
        PictureBox1.ImageLocation = (sciezkazapisu)
        System.Threading.Thread.Sleep(100)
    End Sub


    Private Sub ZapiszZdjęcieJakoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZapiszZdjęcieJakoToolStripMenuItem.Click
        zapiszdjecia()
    End Sub

    Private Sub WersjaLiveVideoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WersjaLiveVideoToolStripMenuItem.Click
        MsgBox("Opcja jest wyłączona", MsgBoxStyle.Exclamation)
    End Sub


    Private Sub PrzeglądarkaZasobówNaDyskuSDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzeglądarkaZasobówNaDyskuSDToolStripMenuItem.Click
        Dysk.Show()
        Dysk.WebBrowser1.Navigate(zasoby1)
        Dysk.Text = "Przeglądarka zasobów na karcie SD"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Dysk.Show()
        Dysk.WebBrowser1.Navigate(pomoc)
        Dysk.Text = "Opis i Pomoc"
    End Sub

    Private Sub KoniecToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KoniecToolStripMenuItem.Click
        Application.ExitThread()
    End Sub

    Private Sub KonfiguracjaProgramuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KonfiguracjaProgramuToolStripMenuItem.Click
        Process.Start("konfig.xml")
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim currentDirName As String
        currentDirName = Directory.GetCurrentDirectory()
        OpenFileDialog2.Title = "Otwórz plik-zdjęcie"
        OpenFileDialog2.InitialDirectory = currentDirName + ("\Image\")
        OpenFileDialog2.Filter = "Plik obrazu (*.jpg)|*.jpg|Wszystkie (*.*)|*.*"
        OpenFileDialog2.FileName = ("")
        OpenFileDialog2.FilterIndex = 2
        OpenFileDialog2.RestoreDirectory = True
        If Not Directory.Exists(OpenFileDialog2.InitialDirectory) Then
            MsgBox("Brak folderu ze zdjęciami", MsgBoxStyle.Critical)
        Else
            If (OpenFileDialog2.ShowDialog() = DialogResult.OK) Then
                Form2.Show()
                Form2.PictureBox1.Image = New Bitmap(OpenFileDialog2.FileName())
            Else
                MsgBox("Prawdopodobny błąd otwarcia pliku.", MsgBoxStyle.Information)

            End If
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim currentDirName As String
        Dim sciezka1 As String
        currentDirName = System.IO.Directory.GetCurrentDirectory()
        sciezka1 = currentDirName + ("\Image\")

        OpenFileDialog1 = New OpenFileDialog()
        OpenFileDialog1.Title = "Otwórz plik-zdjęcie"
        OpenFileDialog1.InitialDirectory = currentDirName
        OpenFileDialog1.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff| " + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff"
        OpenFileDialog1.FileName = sciezka1
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True
    End Sub


    Private Sub ZapiszNaKompiterZdjęcieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZapiszNaKompiterZdjęcieToolStripMenuItem.Click
        ZapisznakomputerToolStripMenuItem.PerformClick()
    End Sub

    Private Sub OtwórzZdjęcieZKomputeraToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OtwórzZdjęcieZKomputeraToolStripMenuItem.Click
        ToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)
        ToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub ZapisznakomputerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZapisznakomputerToolStripMenuItem.Click

        On Error Resume Next
        Dim time As String
        Dim rozszerzenie As String
        Dim sciezka As String
        Dim currentDirName As String
        rozszerzenie = ".jpg"
        time = Format(Now, "dd-MM-yyyy--HH-mm-ss")
        currentDirName = System.IO.Directory.GetCurrentDirectory()
        sciezka = currentDirName + ("\Image\kamera-") + time + rozszerzenie

        ' Gdy w folderze programu brak folderu Image 
        If Not (System.IO.Directory.Exists(currentDirName + "\Image\")) Then
            ' Tworzy go...
            System.IO.Directory.CreateDirectory(currentDirName + "\Image\")
            ' i zapisuje plik-obraz z kamery...
            My.Computer.Network.DownloadFile(adreskamery1, sciezka, login, haslo)
            If (System.IO.File.Exists(sciezka)) Then
                MsgBox("Plik zapisany w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            Else
                MsgBox("Prawdopodobny błąd podczas zapisu w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            End If
        Else
            My.Computer.Network.DownloadFile(adreskamery1, sciezka, login, haslo)
            If (System.IO.File.Exists(sciezka)) Then
                MsgBox("Plik zapisany w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            Else
                MsgBox("Prawdopodobny błąd podczas zapisu w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub AktualizacjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AktualizacjaToolStripMenuItem.Click
        'sprawdzenie czy aktualizacja jest wlaczona
        If aktualizacja = "false" Then
            MsgBox("Aktualizacja nie została włączona przez administratora programu.", MsgBoxStyle.Information)
        End If

        ' uruchomienie systemu aktualizacji programu
        If aktualizacja = "true" Then
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://pomoclider.pl/kameraview/version.txt")
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            Dim newestversion As String = sr.ReadToEnd()
            'sprawdzanie wersji aplikacji w assembly
            'Dim currentversion As String = Application.ProductVersion
            'ja sprawdzam w label3 wersje aplikacji
            Dim currentversion As String = Label3.Text
            If newestversion.Contains(currentversion) Then
                'oznacza ze wszystko jest aktualne
                MsgBox("Program jest aktualny", MsgBoxStyle.Information)
            Else
                'czyli cos jest nie aktualne
                If MessageBox.Show("Jest nowa wersja programu, czy chcesz pobrać nową wersje KameraView?", "Aktualizacja KameraView", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    'sciaganie nowej wersji programu
                    WebBrowser2.Navigate(adresaktualizacji)
                End If
            End If
        End If
    End Sub

    Private Sub PonownyOdczytKonfiguracjiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PonownyOdczytKonfiguracjiToolStripMenuItem.Click
        startuje_program()
        MsgBox("Zadanie wykonano z sukcesem!", MsgBoxStyle.Information)
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click

        'timer_save_jpg.Abort()
        'wylacznanie programu
        Application.ExitThread()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        kamerainfo = kamera1plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button10.Text + " (" + kamera1hint + ")"

        'czy ma byc vlc stream czy jpg stream?
        trybpracyprogramu()
    End Sub

    Private Sub Button11_Click_1(sender As Object, e As EventArgs) Handles Button11.Click
        kamerainfo = kamera2plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button11.Text + " (" + kamera2hint + ")"

        'czy ma byc vlc stream czy jpg stream? 
        'uwaga ten tryb nie wylacza jpg stream 180 mega ramu
        trybpracyprogramu()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        kamerainfo = kamera3plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button12.Text + " (" + kamera3hint + ")"

        'czy ma byc vlc stream czy jpg stream? 
        'uwaga ten tryb nie wylacza jpg stream 180 mega ramu
        trybpracyprogramu()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        kamerainfo = kamera4plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button13.Text + " (" + kamera4hint + ")"

        'czy ma byc vlc stream czy jpg stream? 
        'uwaga ten tryb nie wylacza jpg stream 180 mega ramu
        trybpracyprogramu()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        kamerainfo = kamera5plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button14.Text + " (" + kamera5hint + ")"

        'czy ma byc vlc stream czy jpg stream? 
        'uwaga ten tryb nie wylacza jpg stream 180 mega ramu
        trybpracyprogramu()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        kamerainfo = kamera6plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button15.Text + +" (" + kamera6hint + ")"

        'czy ma byc vlc stream czy jpg stream? 
        'uwaga ten tryb nie wylacza jpg stream 180 mega ramu
        trybpracyprogramu()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        kamerainfo = kamera7plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button16.Text + " (" + kamera7hint + ")"

        'czy ma byc vlc stream czy jpg stream? 
        'uwaga ten tryb nie wylacza jpg stream 180 mega ramu
        trybpracyprogramu()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        kamerainfo = kamera8plik
        czytanie_kamera_xml()

        Label1.Text = "Obraz: " + Button17.Text + " (" + kamera8hint + ")"

        'czy ma byc vlc stream czy jpg stream? 
        'uwaga ten tryb nie wylacza jpg stream 180 mega ramu
        trybpracyprogramu()
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Dysk.Show()
        Dysk.WebBrowser1.Navigate(pomoc)
        Dysk.Text = "Opis i Pomoc"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dysk.Show()
        Dysk.WebBrowser1.Navigate(zasoby1)
        Dysk.Text = "Przeglądarka zasobów na karcie SD"
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles Button9.Click

        'zapisywanie zdjecia
        On Error Resume Next
        Dim time As String
        Dim rozszerzenie As String
        Dim sciezka As String
        Dim currentDirName As String
        rozszerzenie = ".jpg"
        time = Format(Now, "dd-MM-yyyy--HH-mm-ss")
        currentDirName = System.IO.Directory.GetCurrentDirectory()
        sciezka = currentDirName + ("\Image\kamera-") + time + rozszerzenie

        ' Gdy w folderze programu brak folderu Image 
        If Not (System.IO.Directory.Exists(currentDirName + "\Image\")) Then
            ' Tworzy go...
            System.IO.Directory.CreateDirectory(currentDirName + "\Image\")
            ' i zapisuje plik-obraz z kamery...
            My.Computer.Network.DownloadFile(adreskamery1, sciezka, login, haslo)
            If (System.IO.File.Exists(sciezka)) Then
                MsgBox("Plik zapisany w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            Else
                MsgBox("Prawdopodobny błąd podczas zapisu w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            End If
        Else
            My.Computer.Network.DownloadFile(adreskamery1, sciezka, login, haslo)
            If (System.IO.File.Exists(sciezka)) Then
                MsgBox("Plik zapisany w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            Else
                MsgBox("Prawdopodobny błąd podczas zapisu w: " + sciezka + time + rozszerzenie, MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        archiwizacjaxml()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Process.Start("xml konfigurator.exe")
    End Sub

    Private Sub ArchiwizacjaUstawieńToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArchiwizacjaUstawieńToolStripMenuItem.Click
        archiwizacjaxml()
    End Sub

    Private Sub ZamknijProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZamknijProgramToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DokonajArchiwizacjiPlikuXMLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DokonajArchiwizacjiPlikuXMLToolStripMenuItem.Click
        archiwizacjaxml()
    End Sub

    Private Sub PrzeglądajZasobyNaKarcieSDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzeglądajZasobyNaKarcieSDToolStripMenuItem.Click
        Dysk.Show()
        Dysk.WebBrowser1.Navigate(zasoby1)
        Dysk.Text = "Przeglądarka zasobów na karcie SD"
    End Sub

    Private Sub UruchomPomocToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UruchomPomocToolStripMenuItem.Click
        Dysk.Show()
        Dysk.WebBrowser1.Navigate(pomoc)
        Dysk.Text = "Opis i Pomoc"
    End Sub

    Private Sub ZapiszFotografieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZapiszFotografieToolStripMenuItem.Click
        zapiszdjecia()
    End Sub

    Private Sub OpisProgramuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpisProgramuToolStripMenuItem.Click
        Dysk.Show()
        Dysk.WebBrowser1.Navigate(pomoc)
        Dysk.Text = "Opis i Pomoc"
    End Sub

    Private Sub KameraSaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KameraSaveToolStripMenuItem.Click
        On Error Resume Next
        Process.Start("KameraSave.exe")
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick

        '-----------------------------------------------------------------------------------------------'

        ' odswiezanie webrowswer

        '-----------------------------------------------------------------------------------------------'

        On Error Resume Next
        WebBrowser1.Navigate(adreskamery1)
        On Error Resume Next
    End Sub
End Class


