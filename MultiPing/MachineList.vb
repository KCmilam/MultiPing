Imports System.Xml
Imports System.Xml.Serialization

Public Class MachineList
    <XmlElement("Machines")> Public Machines As New List(Of Machine)
    Private Shared mInstance As MachineList

    Public Shared ReadOnly Property Instance As MachineList
        Get
            If mInstance Is Nothing Then
                If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\MachineList.xml") Then
                    mInstance = Open()
                Else
                    mInstance = New MachineList
                End If

            End If
            Return mInstance
        End Get
    End Property

    Public Shared Function Open(Optional ByVal File As String = "") As MachineList
        Dim TextReader As Xml.XmlTextReader = Nothing
        Dim Serializer As New XmlSerializer(GetType(MachineList))
        Try
            Dim filepath As String = My.Application.Info.DirectoryPath & "\MachineList.xml"
            If File <> "" Then filepath = File
            TextReader = New Xml.XmlTextReader(filepath)

            If Serializer.CanDeserialize(TextReader) Then
                Open = Serializer.Deserialize(TextReader)
                TextReader.Close()
            Else
                Open = Nothing
                TextReader.Close()
            End If
        Catch ex As Exception
            TextReader.Close()
            Open = Nothing
        End Try
    End Function

    Public Sub Save()
        Dim TextWriter As New Xml.XmlTextWriter(My.Application.Info.DirectoryPath & "\MachineList.xml", Nothing)
        TextWriter.Formatting = Formatting.Indented
        Dim Serializer As New XmlSerializer(GetType(MachineList))
        Serializer.Serialize(TextWriter, Me)
        TextWriter.Close()
    End Sub

    Public Shared Sub SetMachines(Machines As List(Of Machine))
        Instance.Machines = Machines
    End Sub

    Public Sub SaveResults()
        Dim Count As Integer = 1
        Dim FilePath As String = My.Application.Info.DirectoryPath & "\Results_" & Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Count & ".xml"
        While My.Computer.FileSystem.FileExists(FilePath)
            Count += 1
            FilePath = My.Application.Info.DirectoryPath & "\Results_" & Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Count & ".xml"
        End While

        Dim TextWriter As New Xml.XmlTextWriter(FilePath, Nothing)
        Machine.SaveingResults = True
        TextWriter.Formatting = Formatting.Indented
        Dim Serializer As New XmlSerializer(GetType(MachineList))
        Serializer.Serialize(TextWriter, Me)
        TextWriter.Close()
        Machine.SaveingResults = False
    End Sub

    Public Shared Function AddMachine(Device As String, IP As String)
        Dim NewMachine As New Machine()
        NewMachine.Device = Device
        NewMachine.IP = IP
        Instance.Machines.Add(NewMachine)
        Return NewMachine
    End Function

    Public Shared Sub DeleteMachine(Device As String, IP As String)
        For Each mMachine As Machine In Instance.Machines
            If mMachine.Device = Device And mMachine.IP = IP Then
                Instance.Machines.Remove(mMachine)
                Exit For
            End If
        Next
    End Sub
End Class
