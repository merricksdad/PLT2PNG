Imports System
Imports System.Math
Imports System.Drawing


Public Module SharedProgram
    Public proj As String

    Sub PrintTitle()
        Select Case proj
            Case "PLT2PNG"
                Console.WriteLine("PLT 2 PNG" & vbCrLf & "By MerrickDad" & vbCrLf)

            Case "PLTSCALE"
                Console.WriteLine("PLT SCALE" & vbCrLf & "By MerrickDad" & vbCrLf)

            Case "PLTTools"
                Console.WriteLine("PLT Tools" & vbCrLf & "By MerrickDad" & vbCrLf)

        End Select

    End Sub

    Public Sub PrintUsage(Optional id As String = "")
        If id = "" Then id = proj
        Select Case id
            Case "PLT2PNG"
                Console.WriteLine("  Usage:" & vbCrLf & "  PLT2PNG <filename> [-c]" & vbCrLf & "     -c: output colorcoded image" & vbCrLf)

            Case "PLTSCALE"
                Console.WriteLine("  Usage:" & vbCrLf & "  PLTSCALE <filename> <size_x> <size_y> [-i]" & vbCrLf _
                    & "     size_x and size_y should be powers of 2 (eg. 32, 64, 128, 256, 512, 1024) " & vbCrLf _
                    & "     -i: use bilinear interpolation during luminance scaling (paintID will still use no interpolation)" & vbCrLf)

            Case "PLTTools"
                PrintUsage("PLT2PNG")
                PrintUsage("PLTSCALE")

        End Select

    End Sub

    Sub Main(args As String())
        proj = "PLTTools"
        PrintTitle()
        PrintUsage()
    End Sub

    Sub Central(args As String())
        ''which project is using this file
        If proj = "PLTTools" Then
            PrintUsage()
            Return
        End If

        Dim myDir As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) & "\"

        ''args = {"C:\NWN\override\plt2tga\cloak_013.plt", "-c"}
        ''args = {"C:\NWN\override\plt2tga\cloak_013.plt", "512", "512"}

        PrintTitle()

        ''Dim tmpFile = "C:\NWN\override\plt2tga\pfh0_chest002.plt"
        If args.Count = 0 Then
            PrintUsage()
            Return
        End If


        Dim sFile As String = args(0)


        ''determine if the filename is local or specific
        Dim filelist() As String = {}
        Dim dir As String, maskpart As String
        If IO.File.Exists(sFile) Then
            ''file is specific single file
            ReDim Preserve filelist(filelist.Count)
            filelist(filelist.Count - 1) = sFile
        Else
            ''check file as local without path
            If IO.File.Exists(myDir & sFile) Then
                ''file was local single file
                ReDim Preserve filelist(filelist.Count)
                filelist(filelist.Count - 1) = sFile
            Else
                ''check that file is not a directory with mask
                dir = System.IO.Path.GetDirectoryName(sFile)
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

        ''translate arguments
        Dim colorCoded As Boolean = False
        Dim scaleMode As Boolean = 0

        Dim nScaleWidth As Integer
        Dim nScaleHeight As Integer

        Select Case proj
            Case "PLT2PNG"

                For c = 0 To args.Count - 1
                    If c < 1 Then Continue For
                    Select Case args(c).ToLower()
                        Case "-c"
                            colorCoded = True
                            Console.WriteLine("...outputting colorcoded images")
                    End Select
                Next

            Case "PLTSCALE"
                If args.Count < 2 Then
                    PrintUsage()
                    Return
                End If
                nScaleWidth = args(1)
                nScaleHeight = args(2)

                ''confirm binary numbers
                Dim binNums() As Integer = {2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096} '', 8192, 16384, 32768}
                Dim badSize As Boolean
                If Array.Find(binNums, Function(v) v = nScaleWidth) = -1 Then
                    badSize = True
                End If
                If Array.Find(binNums, Function(v) v = nScaleHeight) = -1 Then
                    badSize = True
                End If
                If badSize Then
                    Console.WriteLine("...Error: unsupported plt image size")
                    Return
                End If

                For c = 0 To args.Count - 1
                    If c < 1 Then Continue For
                    Select Case args(c).ToLower()
                        Case "-i"
                            scaleMode = 1
                            Console.WriteLine("...using bilinear interpolation on grayscale data ")
                    End Select
                Next

        End Select

        For Each sFile In filelist
            Select Case proj
                Case "PLT2PNG"
                    ConvertToPNG(sFile, colorCoded:=colorCoded)

                Case "PLTSCALE"
                    Rescale(sFile, nScaleWidth, nScaleHeight, scaleMode:=scaleMode)

                Case Else
                    Return

            End Select

        Next

        Console.WriteLine("Done")

    End Sub

    Public Sub ConvertToPNG(sfile As String, Optional colorCoded As Boolean = False)
        Dim pltFile As New clsPLT
        If pltFile.Open(sfile) Then
            If pltFile.ConvertToPNG(colorCoded:=colorCoded) Then
                Console.WriteLine("...converted " & sfile)
            Else
                Console.WriteLine("...failed to convert " & sfile)
            End If
        Else
            Console.WriteLine("...failed to open " & sfile)
        End If
        pltFile = Nothing
    End Sub

    Public Sub Rescale(sfile As String, width As Integer, height As Integer, Optional scaleMode As Integer = 0)
        Dim pltFile As New clsPLT
        If pltFile.Open(sfile) Then

            If pltFile.Rescale(width, height, scaleMode:=scaleMode) Then
                Console.WriteLine("...rescaled " & sfile)
                If pltFile.Save() Then
                    Console.WriteLine("...file saved")
                End If
            Else
                Console.WriteLine("...failed to rescale " & sfile)
            End If
        Else
            Console.WriteLine("...failed to open " & sfile)
        End If
        pltFile = Nothing
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
