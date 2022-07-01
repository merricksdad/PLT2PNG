Imports System
Imports System.Math
Imports System.Drawing


Module Program

    Sub PrintUsage()
        Console.WriteLine("  Usage:" & vbCrLf & "  PLT2PNG <filename> [-c]" & vbCrLf & "     -c: output colorcoded image")
    End Sub
    Sub Main(args As String())
        Dim myDir As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) & "\"

        ''args = {"C:\NWN\override\plt2tga\pfh0*.plt", "-c"}

        Console.WriteLine("PLT 2 PNG" & vbCrLf & "By MerrickDad" & vbCrLf)

        ''Dim tmpFile = "C:\NWN\override\plt2tga\pfh0_chest002.plt"
        If args.Count = 0 Then
            PrintUsage()
            Return
        End If

        Dim sFile As String = args(0)
        Dim colorCoded As Boolean = False
        For c = 0 To args.Count - 1
            If c = 0 Then Continue For
            If args(c).ToLower() = "-c" Then
                colorCoded = True
                Console.WriteLine("...outputting colorcoded images")
            End If
        Next

        ''determine if the filename is local or specific
        Dim filelist() As String = {}
        Dim dir As String, maskpart As String
        If IO.File.Exists(sFile) Then
            ''file is specific single file
            ReDim Preserve filelist(filelist.Count)
            filelist(filelist.Count) = sFile
        Else
            ''check file as local without path
            If IO.File.Exists(mydir & sFile) Then
                ''file was local single file
                ReDim Preserve filelist(filelist.Count)
                filelist(filelist.Count) = sFile
            Else
                ''check that file is not a directory with mask
                Dir = System.IO.Path.GetDirectoryName(sFile)
                If dir <> "" Then
                    ''was at least a directory structure with mask
                    dir &= "\"
                    maskpart = Right(sFile, Len(sFile) - Len(dir))
                    filelist = IO.Directory.EnumerateFiles(dir, maskpart).ToArray()
                    If filelist.Count > 0 Then
                        ''was a successful directory with file mask
                        Console.WriteLine("..." & filelist.Count & " files found matching " & maskpart)
                    End If
                Else
                    ''does not have a directory structure
                    maskpart = sFile
                    If maskpart <> "" Then
                        ''might have a valid mask though, so check local + mask
                        filelist = IO.Directory.EnumerateFiles(myDir, maskpart).ToArray()
                        If filelist.Count > 0 Then
                            Console.WriteLine("..." & filelist.Count & " files found matching " & maskpart)
                            ''was a successful local file mask
                        End If
                    End If
                End If
            End If
        End If

        If filelist.Count = 0 Then
            PrintUsage()
            Return
        End If

        Dim pltFile As New clsPLT
        For Each sFile In filelist
            If pltFile.Open(sFile) Then
                If pltFile.ConvertToPNG(colorCoded) Then
                    Console.WriteLine("...converted " & sFile)
                Else
                    Console.WriteLine("...failed to convert " & sFile)
                End If
            Else
                Console.WriteLine("...failed to open " & sFile)
            End If
        Next

        Console.WriteLine("Done")

    End Sub

    Public Function HSL2RGB(ByVal H As Double, ByVal S As Double, ByVal L As Double) As Color
        Dim p1 As Double
        Dim p2 As Double

        Dim R, G, B As Double

        If L <= 0.5 Then
            p2 = L * (1 + S)
        Else
            p2 = L + S - L * S
        End If
        p1 = 2 * L - p2
        If S = 0 Then
            R = L
            G = L
            B = L
        Else
            R = Qqh2RGB(p1, p2, H + 120)
            G = Qqh2RGB(p1, p2, H)
            B = Qqh2RGB(p1, p2, H - 120)
        End If

        HSL2RGB = Color.FromArgb(R, G, B)
    End Function

    Private Function Qqh2RGB(ByVal q1 As Double, ByVal q2 As _
        Double, ByVal hue As Double) As Double
        If hue > 360 Then
            hue = hue - 360
        ElseIf hue < 0 Then
            hue = hue + 360
        End If
        If hue < 60 Then
            Qqh2RGB = q1 + (q2 - q1) * hue / 60
        ElseIf hue < 180 Then
            Qqh2RGB = q2
        ElseIf hue < 240 Then
            Qqh2RGB = q1 + (q2 - q1) * (240 - hue) / 60
        Else
            Qqh2RGB = q1
        End If
    End Function
End Module
