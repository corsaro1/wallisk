Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class vote
    Private Sub vote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Insert delegate list, one per line"
        Me.Button2.PerformClick()
    End Sub

    Dim seedx As String
    Dim seed2x As String

    Dim pubkeyx As String



    Public Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean

        Return True
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        Dim senderId As String = Lisk.senderId
        Dim url As String = "https://login.lisk.io/api/accounts/delegates"

        ' MsgBox(senderId)

        Dim request As HttpWebRequest

        Dim response As HttpWebResponse = Nothing

        Dim reader As StreamReader
        Dim rawresp As String


        request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts/delegates/?address=" & senderId), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())

        rawresp = reader.ReadToEnd()

        ' serve a vedere chi si è già votato
        Dim jResultsb As Object = JObject.Parse(rawresp)
        Dim testob As String = If(jResultsb("delegates") Is Nothing, "", jResultsb("delegates").ToString())
        'MsgBox(testob)




        pubkey()


        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        Dim seed2 As Object
        prompt = "What's your second signature?"
        seed2 = InputBox(prompt, title, defaultResponse)
        If seed2 Is "" Then GoTo FooError






        Const quote As String = """"

        For Each indexChecked In CheckedListBox1.CheckedIndices
            'The indexChecked variable contains the index of the item.
            '  MessageBox.Show("Index#: " + indexChecked.ToString() + ", is checked. Checked state is:" +
            '  CheckedListBox1.GetItemCheckState(indexChecked).ToString() + ".")
            CheckedListBox2.SetItemChecked(indexChecked.ToString(), True)
        Next


        Dim myAL As New ArrayList

        ' Dim a As Integer = 0
        For Each itemChecked In CheckedListBox2.CheckedItems
            '  Do While (a < CheckedListBox1.Items.Count)

            ' a = (a + 1)
            ' Loop
            ' Use the IndexOf method to get the index of an item.
            '  MessageBox.Show("Item with title: " + quote + itemChecked.ToString() + quote +
            '      ", is checked. Checked state is: " +
            '       CheckedListBox1.GetItemCheckState(CheckedListBox1.Items.IndexOf(itemChecked)).ToString() + ".")
            ' MessageBox.Show(itemChecked.ToString())


            If testob.Contains(itemChecked.ToString()) = False Then
                myAL.Add(Chr(34) & "+" & itemChecked.ToString() & Chr(34))
            Else

            End If

        Next

        Dim LineOfText As String

        LineOfText = String.Join(",", CType(myAL.ToArray(Type.GetType("System.String")), String()))
        MsgBox(LineOfText)

        If LineOfText = "" Then
            MsgBox("you have already voted all selected delegates")
            GoTo fooerror
        End If

        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seedx & Chr(34) & "," & Chr(34) & "secondSecret" & Chr(34) & ":" & Chr(34) & seed2 & Chr(34) & "," & Chr(34) & "publicKey" & Chr(34) & ":" & Chr(34) & pubkeyx & Chr(34) & "," & Chr(34) & "delegates" & Chr(34) & ":[" & LineOfText & "]" & "}"

        MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        request.Method = "PUT"
        request.ContentType = "application/json"
        request.ContentLength = arr.Length
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(arr, 0, arr.Length)
        dataStream.Close()
        Try
            Dim returnString As String = response.StatusCode.ToString()
            Dim returnString2 As String = response.ToString()
            MsgBox(returnString)


            '' https://developer.yahoo.com/dotnet/howto-xml_vb.html
            Dim result As String

            Try
                response = DirectCast(request.GetResponse(), HttpWebResponse)

                reader = New StreamReader(response.GetResponseStream())

                result = reader.ReadToEnd()
            Finally
                If Not response Is Nothing Then response.Close()
            End Try

            MsgBox(result)


        Catch
            MsgBox("tx fallita")
        End Try
        seedx = ""
        seed2 = ""




fooerror:

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim defaultResponse As String = String.Empty
        Dim title As String = String.Empty



        '  If senderId IsNot "" Or senderId IsNot Nothing Then
        'MsgBox("using your address " & senderId)
        '   Else
        '   Dim prompt As String = String.Empty
        '   prompt = "What is your address?"
        '   senderId = InputBox(prompt, title, defaultResponse)
        '  If senderId Is "" Then GoTo fooerror
        '  End If




        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader

        On Error Resume Next
        request = DirectCast(WebRequest.Create("https://login.lisk.io/api/delegates?limit=101&offset=0"), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())
        Dim rawresp As String
        rawresp = reader.ReadToEnd()
        ' MsgBox(rawresp)








        Dim jResults As Object = JObject.Parse(rawresp)
        ' Dim testo As String = If(jResults("delegates")(0)("username") Is Nothing, "", jResults("delegates")(0)("username").ToString())
        ' Dim jResults2 As Object = JObject.Parse(testo)
        ' Dim testo2 As String = If(jResults2("secondSignature") Is Nothing, "", jResults2("secondSignature").ToString())
        '  MsgBox(testo)





        Dim startNumber As Integer
        Dim endNumber As Integer
        Dim i As Integer

        startNumber = 0
        endNumber = 100

        Dim testofinale As String


        For i = startNumber To endNumber

            '   MessageBox.Show(i)
            Dim testoa As String = If(jResults("delegates")(i)("username") Is Nothing, "", jResults("delegates")(i)("username").ToString())
            Dim testoa1 As String = If(jResults("delegates")(i)("publicKey") Is Nothing, "", jResults("delegates")(i)("publicKey").ToString())
            If testoa IsNot Nothing Then
                '  MessageBox.Show(testoa)
                testofinale += testoa & vbCrLf
                CheckedListBox1.Items.Add(testoa)
                CheckedListBox2.Items.Add(testoa1)

                '                CheckedListBox1.DisplayMember = "Text"
                '                CheckedListBox1.ValueMember = "Value"
                '                CheckedListBox1.Items.Insert(0, New With {
                '                Key .Text = testoa,
                '                Key .Value = testoa1
                '})


                testoa = Nothing
                testoa1 = Nothing


            Else

            End If

        Next i

        ' MsgBox(testofinale)
        'RichTextBox1.Text = testofinale
        '   RichTextBox1.Visible = True
        '  Label19.Text = "tx to be co-signed"
        '  Label19.Visible = True
        '  CheckedListBox1.Items.Add(testofinale)

        Dim request2 As HttpWebRequest
        Dim response2 As HttpWebResponse = Nothing
        Dim reader2 As StreamReader
        request2 = DirectCast(WebRequest.Create("https://login.lisk.io/api/delegates?limit=101&offset=101"), HttpWebRequest)
        response2 = DirectCast(request2.GetResponse(), HttpWebResponse)
        reader2 = New StreamReader(response2.GetResponseStream())
        Dim rawresp2 As String
        rawresp2 = reader2.ReadToEnd()
        jResults = JObject.Parse(rawresp2)
        startNumber = 0
        endNumber = 100
        For i = startNumber To endNumber

            '   MessageBox.Show(i)
            Dim testoa As String = If(jResults("delegates")(i)("username") Is Nothing, "", jResults("delegates")(i)("username").ToString())
            Dim testoa1 As String = If(jResults("delegates")(i)("publicKey") Is Nothing, "", jResults("delegates")(i)("publicKey").ToString())
            If testoa IsNot Nothing Then
                '  MessageBox.Show(testoa)
                testofinale += testoa & vbCrLf
                CheckedListBox1.Items.Add(testoa)
                CheckedListBox2.Items.Add(testoa1)
                testoa = Nothing
                testoa1 = Nothing

            Else

            End If

        Next i



fooerror:
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim defaultResponse As String = String.Empty
        Dim title As String = String.Empty



        '  If senderId IsNot "" Or senderId IsNot Nothing Then
        'MsgBox("using your address " & senderId)
        '   Else
        '   Dim prompt As String = String.Empty
        '   prompt = "What is your address?"
        '   senderId = InputBox(prompt, title, defaultResponse)
        '  If senderId Is "" Then GoTo fooerror
        '  End If




        Dim request As HttpWebRequest

        Dim response As HttpWebResponse = Nothing

        Dim reader As StreamReader

        On Error Resume Next
        request = DirectCast(WebRequest.Create("https://liskworld.info/api/delegates/get?username=phinx"), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())

        Dim rawresp As String
        rawresp = reader.ReadToEnd()
        MsgBox(rawresp)


        Dim jResults As Object = JObject.Parse(rawresp)
        Dim testo As String = If(jResults("delegate")("publicKey") Is Nothing, "", jResults("delegate")("publicKey").ToString())
        ' Dim jResults2 As Object = JObject.Parse(testo)
        ' Dim testo2 As String = If(jResults2("secondSignature") Is Nothing, "", jResults2("secondSignature").ToString())
        MsgBox(testo)








fooerror:
    End Sub

    Public RotateCount = 0

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12

        Dim url As String = "https://login.lisk.io/api/accounts/delegates"

        Dim myAL As New ArrayList
        Dim senderId As String = Lisk.senderId


        '  MsgBox(senderId)

        Dim request As HttpWebRequest

        Dim response As HttpWebResponse = Nothing

        Dim reader As StreamReader
        Dim rawresp As String


        request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts/delegates/?address=" & senderId), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())

        rawresp = reader.ReadToEnd()

        ' serve a vedere chi si è già votato
        Dim jResultsb As Object = JObject.Parse(rawresp)
        Dim testob As String = If(jResultsb("delegates") Is Nothing, "", jResultsb("delegates").ToString())
        'MsgBox(testob)



        ' On Error  fooerror



        'inutile

        request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts?address=" & senderId), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())


        rawresp = reader.ReadToEnd()
        Dim jResults As Object = JObject.Parse(rawresp)
        Dim testo As String = If(jResults("account") Is Nothing, "", jResults("account").ToString())
        Dim jResults2 As Object = JObject.Parse(testo)
        Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())
        'inutile



        pubkey()

        '   Dim seed As Object
        '  prompt = "What's your seed?"
        '  seed = InputBox(prompt, title, defaultResponse)
        '  If seed Is "" Then GoTo fooerror
        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        Dim seed2 As Object
        prompt = "What's your second signature?"
        seed2 = InputBox(prompt, title, defaultResponse)
        If seed2 Is "" Then GoTo FooError






        For Each line In RichTextBox1.Lines
            '  MsgBox(RichTextBox1.Lines(RotateCount))




            request = DirectCast(WebRequest.Create("https://liskworld.info/api/delegates/get?username=" & RichTextBox1.Lines(RotateCount)), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            rawresp = reader.ReadToEnd()

            ' serve a vedere chi si è già votato
            jResultsb = JObject.Parse(rawresp)
            Dim testob1 As String = If(jResultsb("success") Is Nothing, "", jResultsb("success").ToString())
            '   MsgBox(testob1)

            If testob1 = "True" Then
                Dim testob2 As String = If(jResultsb("delegate")("publicKey") Is Nothing, "", jResultsb("delegate")("publicKey").ToString())
                ' MsgBox(testob2)

                If testob.Contains(testob2) = False Then
                    myAL.Add(Chr(34) & "+" & testob2 & Chr(34))
                Else

                End If

            End If



            RotateCount += 1
        Next

        ' If RotateCount = RichTextBox1.Lines.Count Then

        RotateCount = 0

        ' End If
        '  MsgBox(RichTextBox1.Lines(RotateCount))

        '  RotateCount += 1

        '  MsgBox(rtb_out.ToString)

        Dim LineOfText As String = Nothing

        If myAL.Count > 0 Then
            LineOfText = String.Join(",", CType(myAL.ToArray(Type.GetType("System.String")), String()))

            '  MsgBox(LineOfText)
        End If

        If LineOfText = "" Then
            MsgBox("you have already voted all selected delegates")
            GoTo fooerror
        End If

        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seedx & Chr(34) & "," & Chr(34) & "secondSecret" & Chr(34) & ":" & Chr(34) & seed2 & Chr(34) & "," & Chr(34) & "publicKey" & Chr(34) & ":" & Chr(34) & pubkeyx & Chr(34) & "," & Chr(34) & "delegates" & Chr(34) & ":[" & LineOfText & "]" & "}"

        MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        request.Method = "PUT"
        request.ContentType = "application/json"
        request.ContentLength = arr.Length
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(arr, 0, arr.Length)
        dataStream.Close()
        Try
            Dim returnString As String = response.StatusCode.ToString()
            Dim returnString2 As String = response.ToString()
            MsgBox(returnString)


            '' https://developer.yahoo.com/dotnet/howto-xml_vb.html
            Dim result As String

            Try
                response = DirectCast(request.GetResponse(), HttpWebResponse)

                reader = New StreamReader(response.GetResponseStream())

                result = reader.ReadToEnd()
            Finally
                If Not response Is Nothing Then response.Close()
            End Try

            MsgBox(result)


        Catch
            MsgBox("tx fallita")
        End Try
        seedx = ""
        seed2 = ""




fooerror:

    End Sub



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        ' votepools = "no"
        If MsgBox("Completing this procedure will cost you 1 LISK and you will vote ", MsgBoxStyle.OkCancel, "Title") = MsgBoxResult.Ok Then

            Dim senderId As String = Lisk.senderId

            ' MsgBox("Completing this procedure will cost you 1 LISK and you will vote all public wallet delegates: corsaro, phoenix1969, vipertdk, punkrock, hagie, gr33ndragon, bioly and gregorst, so to support this software")

            Dim defaultResponse As String = String.Empty
            Dim title As String = String.Empty

            If senderId IsNot Nothing Then
                MsgBox("using your address " & senderId)
            Else
                Dim prompt As String = String.Empty
                prompt = "What is your address?"
                senderId = InputBox(prompt, title, defaultResponse)
            End If




            Dim request As HttpWebRequest

            Dim response As HttpWebResponse = Nothing

            Dim reader As StreamReader

            On Error Resume Next
            request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts?address=" & senderId), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()
            '    MsgBox(rawresp)


            Dim jResults As Object = JObject.Parse(rawresp)
            Dim testoerr As String = If(jResults("error") Is Nothing, "", jResults("error").ToString())
            '    MsgBox(testoerr)
            If testoerr = "Account not found" Then
                '  Me.Button8.PerformClick()
                MsgBox(rawresp)
                GoTo fooerror
            Else

            End If


            Dim jResults2 As Object = JObject.Parse(rawresp)

            Dim testo As String = If(jResults2("account") Is Nothing, "", jResults2("account").ToString())

            Dim jResults3 As Object = JObject.Parse(testo)
            Dim testo2 As String = If(jResults3("secondSignature") Is Nothing, "", jResults3("secondSignature").ToString())
            If testo2 = 0 Then
                Me.Button6.PerformClick()

            Else
                Me.Button4.PerformClick()
                ' MsgBox("xxx")

            End If

        End If
fooerror:
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12

        Dim url As String = "https://login.lisk.io/api/accounts/delegates"

        Dim myAL As New ArrayList
        Dim senderId As String = Lisk.senderId


        '  MsgBox(senderId)

        Dim request As HttpWebRequest

        Dim response As HttpWebResponse = Nothing

        Dim reader As StreamReader
        Dim rawresp As String


        request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts/delegates/?address=" & senderId), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())

        rawresp = reader.ReadToEnd()

        ' serve a vedere chi si è già votato
        Dim jResultsb As Object = JObject.Parse(rawresp)
        Dim testob As String = If(jResultsb("delegates") Is Nothing, "", jResultsb("delegates").ToString())
        'MsgBox(testob)



        ' On Error  fooerror





        request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts?address=" & senderId), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())


        rawresp = reader.ReadToEnd()
        Dim jResults As Object = JObject.Parse(rawresp)
        Dim testo As String = If(jResults("account") Is Nothing, "", jResults("account").ToString())
        Dim jResults2 As Object = JObject.Parse(testo)
        Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())




        pubkey()

        '   Dim seed As Object
        '  prompt = "What's your seed?"
        '  seed = InputBox(prompt, title, defaultResponse)
        '  If seed Is "" Then GoTo fooerror
        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        'Dim seed2 As Object
        ' prompt = "What's your second signature?"
        ' seed2 = InputBox(prompt, title, defaultResponse)
        ' If seed2 Is "" Then GoTo fooerror






        For Each line In RichTextBox1.Lines
            ' MsgBox(RichTextBox1.Lines(RotateCount))




            request = DirectCast(WebRequest.Create("https://liskworld.info/api/delegates/get?username=" & RichTextBox1.Lines(RotateCount)), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            rawresp = reader.ReadToEnd()

            ' serve a vedere chi si è già votato
            jResultsb = JObject.Parse(rawresp)
            Dim testob1 As String = If(jResultsb("success") Is Nothing, "", jResultsb("success").ToString())
            ' MsgBox(testob1)

            If testob1 = "True" Then
                Dim testob2 As String = If(jResultsb("delegate")("publicKey") Is Nothing, "", jResultsb("delegate")("publicKey").ToString())
                '   MsgBox(testob2)

                If testob.Contains(testob2) = False Then
                    myAL.Add(Chr(34) & "+" & testob2 & Chr(34))
                Else

                End If

            End If



            RotateCount += 1
        Next

        ' If RotateCount = RichTextBox1.Lines.Count Then

        RotateCount = 0

        ' End If
        '  MsgBox(RichTextBox1.Lines(RotateCount))

        '  RotateCount += 1

        '  MsgBox(rtb_out.ToString)

        Dim LineOfText As String = Nothing

        If myAL.Count > 0 Then
            LineOfText = String.Join(",", CType(myAL.ToArray(Type.GetType("System.String")), String()))

            MsgBox(LineOfText)
        End If

        If LineOfText = "" Then
            MsgBox("you have already voted all selected delegates")
            GoTo fooerror
        End If

        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seedx & Chr(34) & "," & Chr(34) & "publicKey" & Chr(34) & ":" & Chr(34) & pubkeyx & Chr(34) & "," & Chr(34) & "delegates" & Chr(34) & ":[" & LineOfText & "]" & "}"

        MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        request.Method = "PUT"
        request.ContentType = "application/json"
        request.ContentLength = arr.Length
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(arr, 0, arr.Length)
        dataStream.Close()
        Try
            Dim returnString As String = response.StatusCode.ToString()
            Dim returnString2 As String = response.ToString()
            MsgBox(returnString)


            '' https://developer.yahoo.com/dotnet/howto-xml_vb.html
            Dim result As String

            Try
                response = DirectCast(request.GetResponse(), HttpWebResponse)

                reader = New StreamReader(response.GetResponseStream())

                result = reader.ReadToEnd()
            Finally
                If Not response Is Nothing Then response.Close()
            End Try

            MsgBox(result)


        Catch
            MsgBox("tx fallita")
        End Try
        seedx = ""
        'seed2 = ""




fooerror:

    End Sub

    Private Sub pubkey()
        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        Dim url As String = "https://login.lisk.io/api/accounts/generatePublicKey"


        Dim request As HttpWebRequest

        Dim responsex As HttpWebResponse = Nothing

        '    Dim readerx As StreamReader

        '  request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts?address=" & senderId), HttpWebRequest)
        '   responsex = DirectCast(request.GetResponse(), HttpWebResponse)
        '  readerx = New StreamReader(responsex.GetResponseStream())
        '  Try
        'Dim rawresp As String
        '  rawresp = readerx.ReadToEnd()
        '  Dim jResults As Object = JObject.Parse(rawresp)
        '  Dim testo As String = If(jResults("account") Is Nothing, "", jResults("account").ToString())
        '  Dim jResults2 As Object = JObject.Parse(testo)
        '  Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())
        ' MsgBox("your pubKey is " & testo2) ' mia pubkey
        '  Catch

        Dim seed As Object
        prompt = "Hello there. What's your seed?"
        seed = InputBox(prompt, title, defaultResponse)
        If seed Is "" Then GoTo fooerror
        seedx = seed
        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seed & Chr(34) & "}"

        '  MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        request.ContentLength = arr.Length
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(arr, 0, arr.Length)
        dataStream.Close()

        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)


        '' https://developer.yahoo.com/dotnet/howto-xml_vb.html

        Dim reader As StreamReader
        Dim result As String

        response = DirectCast(request.GetResponse(), HttpWebResponse)

        reader = New StreamReader(response.GetResponseStream())

        result = reader.ReadToEnd()

        Dim jResults2 As Object = JObject.Parse(result)
        Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())
        MsgBox("your pubKey is " & testo2) ' mia pubkey
        If Not response Is Nothing Then response.Close()
        pubkeyx = testo2

        '   MsgBox(result)

        '   End Try



fooerror:
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        ' votepools = "no"
        If MsgBox("Completing this procedure will cost you 1 LISK and you will vote ", MsgBoxStyle.OkCancel, "Title") = MsgBoxResult.Ok Then

            Dim senderId As String = Lisk.senderId

            ' MsgBox("Completing this procedure will cost you 1 LISK and you will vote all public wallet delegates: corsaro, phoenix1969, vipertdk, punkrock, hagie, gr33ndragon, bioly and gregorst, so to support this software")

            Dim defaultResponse As String = String.Empty
            Dim title As String = String.Empty

            If senderId IsNot Nothing Then
                MsgBox("using your address " & senderId)
            Else
                Dim prompt As String = String.Empty
                prompt = "What is your address?"
                senderId = InputBox(prompt, title, defaultResponse)
            End If




            Dim request As HttpWebRequest

            Dim response As HttpWebResponse = Nothing

            Dim reader As StreamReader

            On Error Resume Next
            request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts?address=" & senderId), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()
            MsgBox(rawresp)


            Dim jResults As Object = JObject.Parse(rawresp)
            Dim testoerr As String = If(jResults("error") Is Nothing, "", jResults("error").ToString())
            '    MsgBox(testoerr)
            If testoerr = "Account not found" Then
                '  Me.Button8.PerformClick()
                MsgBox(rawresp)
                GoTo fooerror
            Else

            End If


            Dim jResults2 As Object = JObject.Parse(rawresp)

            Dim testo As String = If(jResults2("account") Is Nothing, "", jResults2("account").ToString())

            Dim jResults3 As Object = JObject.Parse(testo)
            Dim testo2 As String = If(jResults3("secondSignature") Is Nothing, "", jResults3("secondSignature").ToString())
            MsgBox(testo2)
            If testo2 = 0 Then
                Me.Button8.PerformClick()

            Else
                Me.Button1.PerformClick()
                ' MsgBox("xxx")

            End If

        End If
fooerror:
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        Dim senderId As String = Lisk.senderId
        Dim url As String = "https://login.lisk.io/api/accounts/delegates"

        ' MsgBox(senderId)

        Dim request As HttpWebRequest

        Dim response As HttpWebResponse = Nothing

        Dim reader As StreamReader
        Dim rawresp As String


        request = DirectCast(WebRequest.Create("https://login.lisk.io/api/accounts/delegates/?address=" & senderId), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())

        rawresp = reader.ReadToEnd()

        ' serve a vedere chi si è già votato
        Dim jResultsb As Object = JObject.Parse(rawresp)
        Dim testob As String = If(jResultsb("delegates") Is Nothing, "", jResultsb("delegates").ToString())
        'MsgBox(testob)




        pubkey()









        Const quote As String = """"

        For Each indexChecked In CheckedListBox1.CheckedIndices
            'The indexChecked variable contains the index of the item.
            '  MessageBox.Show("Index#: " + indexChecked.ToString() + ", is checked. Checked state is:" +
            '  CheckedListBox1.GetItemCheckState(indexChecked).ToString() + ".")
            CheckedListBox2.SetItemChecked(indexChecked.ToString(), True)
        Next


        Dim myAL As New ArrayList

        ' Dim a As Integer = 0
        For Each itemChecked In CheckedListBox2.CheckedItems
            '  Do While (a < CheckedListBox1.Items.Count)

            ' a = (a + 1)
            ' Loop
            ' Use the IndexOf method to get the index of an item.
            '  MessageBox.Show("Item with title: " + quote + itemChecked.ToString() + quote +
            '      ", is checked. Checked state is: " +
            '       CheckedListBox1.GetItemCheckState(CheckedListBox1.Items.IndexOf(itemChecked)).ToString() + ".")
            ' MessageBox.Show(itemChecked.ToString())


            If testob.Contains(itemChecked.ToString()) = False Then
                myAL.Add(Chr(34) & "+" & itemChecked.ToString() & Chr(34))
            Else

            End If

        Next

        Dim LineOfText As String

        LineOfText = String.Join(",", CType(myAL.ToArray(Type.GetType("System.String")), String()))
        MsgBox(LineOfText)

        If LineOfText = "" Then
            MsgBox("you have already voted all selected delegates")
            GoTo fooerror
        End If

        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seedx & Chr(34) & "," & Chr(34) & "publicKey" & Chr(34) & ":" & Chr(34) & pubkeyx & Chr(34) & "," & Chr(34) & "delegates" & Chr(34) & ":[" & LineOfText & "]" & "}"

        MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        request.Method = "PUT"
        request.ContentType = "application/json"
        request.ContentLength = arr.Length
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(arr, 0, arr.Length)
        dataStream.Close()
        Try
            Dim returnString As String = response.StatusCode.ToString()
            Dim returnString2 As String = response.ToString()
            MsgBox(returnString)


            '' https://developer.yahoo.com/dotnet/howto-xml_vb.html
            Dim result As String

            Try
                response = DirectCast(request.GetResponse(), HttpWebResponse)

                reader = New StreamReader(response.GetResponseStream())

                result = reader.ReadToEnd()
            Finally
                If Not response Is Nothing Then response.Close()
            End Try

            MsgBox(result)


        Catch
            MsgBox("tx fallita")
        End Try
        seedx = ""
        '  seed2 = ""




fooerror:
    End Sub
End Class