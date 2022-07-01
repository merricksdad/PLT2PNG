Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging

''' <summary>
''' Contains file type, version, and image dimension information
''' </summary>
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
<Serializable()>
Public Structure structPltHeader
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> Public FileType As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> Public FileVersion As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> Public Unused1 As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> Public Unused2 As String
    Public Width As Int32
    Public Height As Int32
End Structure

''' <summary>
''' Luminance-layerID pair structure
''' </summary>
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
<Serializable()>
Public Structure structPltPixel
    Public Value As Byte
    Public LayerID As Byte
End Structure

Public Class clsPLT
    ''' <summary>
    ''' Contains file type, version, and image dimension information
    ''' </summary>
    Private Header As New structPltHeader

    ''' <summary>
    ''' Contains an array of luminance-layerID pairs
    ''' </summary>
    Private PixelData() As structPltPixel

    ''' <summary>
    ''' Set to true when a PLT file is loaded
    ''' </summary>
    Private isLoaded As Boolean

    ''' <summary>
    ''' The full filename of the loaded PLT file
    ''' </summary>
    Public Filename As String

    ''' <summary>
    ''' Opens a specified PLT file and fetches all data
    ''' </summary>
    ''' <param name="sFile"></param>
    ''' <returns></returns>
    Public Function Open(sFile) As Boolean
        If IO.File.Exists(sFile) Then
            Dim binFile As New IO.FileStream(sFile, IO.FileMode.Open)
            Dim reader As New IO.BinaryReader(binFile)

            Dim headerBytes() As Byte = reader.ReadBytes(24)

            Dim handle As GCHandle = GCHandle.Alloc(headerBytes, GCHandleType.Pinned)
            Header = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), GetType(structPltHeader))
            handle.Free()

            ReDim PixelData((Header.Height * Header.Width) - 1)

            Dim pixelBytes() = reader.ReadBytes(2 * Header.Height * Header.Width)
            For c = 0 To PixelData.Length - 1
                ''this pointer method is not getting the layerID correctly
                ''handle = GCHandle.Alloc(pixelBytes(c * 2), GCHandleType.Pinned)
                ''PixelData(c) = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), GetType(structPltPixel))
                ''handle.Free()
                With PixelData(c)
                    .Value = pixelBytes(c * 2)
                    .LayerID = pixelBytes((c * 2) + 1)
                End With
            Next

            reader.Close()
            reader.Dispose()

            binFile.Close()
            binFile.Dispose()

            isLoaded = True
            Filename = sFile
        Else
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Converts PLT data to PNG output and saves the file adjacent to the input file
    ''' </summary>
    ''' <param name="colorCoded">Colorizes parts 0 to 9 using the same color scheme as PLTEditor</param>
    ''' <returns>Returns true on successful conversion</returns>
    Public Function ConvertToPNG(ByVal Optional colorCoded As Boolean = False) As Boolean
        If Not isLoaded Then Return False

        ''create a new bitmap space
        Dim myPNG As New Bitmap(Header.Width, Header.Height)

        ''0   Skin           All Channels Gray
        ''1   Hair           Red = 0
        ''2   Metal 1        Green = 0
        ''3   Metal 2        Blue = 0
        ''4   Cloth 1        Red = 255
        ''5   Cloth 2        Green = 255
        ''6   Leather 1      Blue = 255
        ''7   Leather 2      RG = 255
        ''8   Tattoo 1       RB = 255
        ''9   Tattoo 2       GB = 255

        ''create the pixel from PLT info
        Dim myPixel As structPltPixel
        Dim R, G, B As Integer
        For x = 0 To Header.Width - 1
            For y = 0 To Header.Height - 1
                myPixel = PixelData((y * Header.Width) + x)
                R = myPixel.Value
                G = R
                B = R

                ''colorize as needed
                If colorCoded Then

                    ''colors pulled from PLTEditor
                    Select Case myPixel.LayerID
                        Case 1
                            R = 0
                        Case 2
                            G = 0
                        Case 3
                            B = 0
                        Case 4
                            R = 255
                        Case 5
                            G = 255
                        Case 6
                            B = 255
                        Case 7
                            R = 255
                            G = 255
                        Case 8
                            R = 255
                            B = 255
                        Case 9
                            G = 255
                            B = 255
                        Case Else
                            ''do nothing
                    End Select
                End If

                ''remember that plt data is upside down from uv coords in png
                myPNG.SetPixel(x, (Header.Height - 1) - y, Color.FromArgb(R, G, B))
            Next
        Next


        ''convert the original filename
        Dim inf As New IO.FileInfo(Filename)
        Dim sFile As String = inf.DirectoryName & "\" & Left(inf.Name, Len(inf.Name) - Len(inf.Extension)) & ".png"

        ''output file
        myPNG.Save(sFile, ImageFormat.Png)
        Return True
    End Function
End Class
