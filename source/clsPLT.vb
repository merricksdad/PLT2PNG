Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging

''' <summary>
''' Contains file type, version, and image dimension information
''' </summary>
<StructLayout(LayoutKind.Sequential)>
<Serializable()>
Public Structure structPltHeader
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> Public FileType As Char()
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> Public FileVersion As Char()
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> Public Unused1 As Char()
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> Public Unused2 As Char()
    Public Width As Int32
    Public Height As Int32
End Structure

''' <summary>
''' Luminance-layerID pair structure
''' </summary>
<StructLayout(LayoutKind.Sequential)>
<Serializable()>
Public Structure structPltPixel
    Public Value As Byte
    Public LayerID As Byte
End Structure

Public Structure vec2Single
    Public x As Single
    Public y As Single

    Public Sub New(valueX As Single, valueY As Single)
        x = valueX
        y = valueY
    End Sub

    Public Sub New(valueXY As vec2Single)
        x = valueXY.x
        y = valueXY.y
    End Sub

    Public Property u As Single
        Get
            Return x
        End Get
        Set(value As Single)
            x = value
        End Set
    End Property
    Public Property v As Single
        Get
            Return y
        End Get
        Set(value As Single)
            y = value
        End Set
    End Property

    Public Property yx As vec2Single
        Get
            Return New vec2Single(y, x)
        End Get
        Set(xyValue As vec2Single)
            y = xyValue.x
            x = xyValue.y
        End Set
    End Property

    Public Property vu As vec2Single
        Get
            Return New vec2Single(y, x)
        End Get
        Set(xyValue As vec2Single)
            y = xyValue.x
            x = xyValue.y
        End Set
    End Property
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

    Public ReadOnly Property Width As Integer
        Get
            Return Header.Width
        End Get
    End Property

    Public ReadOnly Property Height As Integer
        Get
            Return Header.Height
        End Get
    End Property

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

    Public Function Save(Optional sFile As String = "") As Boolean
        If sFile = "" Then sFile = Filename

        Dim Ptr As IntPtr = Marshal.AllocHGlobal(24)
        '' Dim handle As GCHandle = GCHandle.Alloc(24, GCHandleType.Pinned)
        Dim outBytes(23) As Byte
        'now copy structure to Ptr pointer 
        Marshal.StructureToPtr(Header, Ptr, False)
        'copy to byte array
        Marshal.Copy(Ptr, outBytes, 0, 24)
        Marshal.FreeHGlobal(Ptr)

        ReDim Preserve outBytes(24 + (2 * Header.Width * Header.Height) - 1)

        For c = 0 To PixelData.Count - 1
            outBytes((2 * c) + 24) = PixelData(c).Value
            outBytes((2 * c) + 1 + 24) = PixelData(c).LayerID
        Next

        IO.File.WriteAllBytes(sFile, outBytes)

        Filename = sFile
        Return True
    End Function

    ''' <summary>
    ''' Converts PLT data to PNG output and saves the file adjacent to the input file
    ''' </summary>
    ''' <param name="colorCoded">Colorizes parts 0 to 9 using the same color scheme as PLTEditor</param>
    ''' <returns>Returns true on successful conversion</returns>
    Public Function ConvertToPNG(ByVal Optional colorCoded As Boolean = False, ByVal Optional increment As Integer = -1) As Boolean
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
        Dim sFile As String = inf.DirectoryName & "\" & Left(inf.Name, Len(inf.Name) - Len(inf.Extension)) & IIf(increment <> -1, "_" & increment, "") & ".png"

        ''output file
        myPNG.Save(sFile, ImageFormat.Png)
        Return True
    End Function


    Private Function xy2uv(x As Integer, y As Integer, uvScale As Size) As vec2Single
        Return New vec2Single(x / uvScale.Width, y / uvScale.Height)
    End Function

    Private Function xy2uv(x As Integer, y As Integer) As vec2Single
        Return New vec2Single(x / Header.Width, y / Header.Height)
    End Function

    Private Function xy2uv(vCoords As Point) As vec2Single
        Return New vec2Single(vCoords.X / Header.Width, vCoords.Y / Header.Height)
    End Function

    Private Function xy2uv(vCoords As Point, uvScale As Size) As vec2Single
        Return New vec2Single(vCoords.X / uvScale.Width, vCoords.Y / uvScale.Height)
    End Function


    Private Function uv2xy(u As Single, v As Single, xyScale As Size) As Point
        Return New Point(u / xyScale.Width, v / xyScale.Height)
    End Function

    Private Function uv2xy(u As Single, v As Single) As Point
        Return New Point(u * Header.Width, v * Header.Height)
    End Function

    Private Function uv2xy(vCoords As vec2Single) As Point
        Return New Point(vCoords.x * Header.Width, vCoords.y * Header.Height)
    End Function

    Private Function uv2xy(vCoords As vec2Single, xyScale As Size) As Point
        Return New Point(vCoords.x * xyScale.Width, vCoords.y * xyScale.Height)
    End Function

    ''' <summary>
    ''' This is a corrected version of the Int() function which properly rounds negative numbers
    ''' </summary>
    ''' <param name="n"></param>
    ''' <returns></returns>
    Function roundNearestInteger(n As Single) As Integer
        If (n > 0) Then
            Return Int(n)
        Else
            Return Int(n + 0.5)
        End If
    End Function

    ''' <summary>
    ''' Rounds to the nearest interval
    ''' </summary>
    ''' <param name="n"></param>
    ''' <param name="i">Supply values such as 10.0, 1.0, or 0.1</param>
    ''' <returns></returns>
    Function RoundNearestInterval(n As Single, i As Single) As Single
        Return i * roundNearestInteger(n / i)
    End Function

    Public Function Rescale(ByVal newWidth As Integer, ByVal newHeight As Integer, Optional ByVal scaleMode As Integer = 0)
        If isLoaded Then
            Dim size As Size
            size.Width = newWidth
            size.Height = newHeight

            Dim scale As Size
            scale.Width = newWidth / Header.Width
            scale.Height = newHeight / Header.Height

            Dim origMagnitude = (Header.Width ^ 2 + Header.Height ^ 2) ^ 0.5
            Dim newMagnitude = (newWidth ^ 2 + newHeight ^ 2) ^ 0.5

            ''create a new pixel buffer
            Dim newPixels() As structPltPixel
            ReDim newPixels((newWidth * newHeight) - 1)

            ''determine which approach to use
            If newMagnitude = origMagnitude Then
                ''no change
                Return False

            ElseIf newMagnitude < origMagnitude Or (newMagnitude > origMagnitude And scaleMode = 0) Then
                ''do pixel resize without interpolation
                ''for each pixel in the new sized image
                For x = 0 To newWidth - 1
                    For y = 0 To newHeight - 1
                        ''get the uv coords of the pixel
                        Dim vTexCoords As vec2Single = xy2uv(x, y, size)

                        ''use the uv coords to get the pixel data from the old image
                        ''try simple rounding
                        Dim oldXY As New Point(roundNearestInteger(vTexCoords.x * Header.Width), roundNearestInteger(vTexCoords.y * Header.Height))
                        Dim newI = (y * newWidth) + x
                        Dim oldI = (oldXY.Y * Header.Width) + oldXY.X

                        With newPixels(newI)
                            .Value = PixelData(oldI).Value
                            .LayerID = PixelData(oldI).LayerID
                        End With
                    Next
                Next
                Header.Width = newWidth
                Header.Height = newHeight
                PixelData = newPixels

            ElseIf newMagnitude > origMagnitude Then ''also assumes scale mode 1 for bilinear interpolation
                ''do bilinear interpolation of gray data
                ''do pixel interpolation of paint data
                For x = 0 To newWidth - 1
                    For y = 0 To newHeight - 1
                        ''get the uv coords of the pixel
                        Dim vTexCoords As vec2Single = xy2uv(x, y, size)

                        ''use the uv coords to get the pixel data from the old image
                        ''try simple rounding
                        Dim oldXY As New vec2Single(Header.Width * vTexCoords.x, Header.Height * vTexCoords.y)
                        Dim newI = (y * newWidth) + x

                        ''get pixels to mix, clamping to max height and width of image
                        Dim x0 = Int(oldXY.x)
                        Dim x1 = Math.Min(x0 + 1, Header.Width - 1)
                        Dim y0 = Int(oldXY.y)
                        Dim y1 = Math.Min(y0 + 1, Header.Height - 1)

                        Dim Px0y0 = (y0 * Header.Width) + x0
                        Dim Px0y1 = (y1 * Header.Width) + x0
                        Dim Px1y0 = (y0 * Header.Width) + x1
                        Dim Px1y1 = (y1 * Header.Width) + x1

                        Dim Lx0y0 = PixelData(Px0y0).Value
                        Dim Lx0y1 = PixelData(Px0y1).Value
                        Dim Lx1y0 = PixelData(Px1y0).Value
                        Dim Lx1y1 = PixelData(Px1y1).Value

                        Dim Dx1x = (x1 - oldXY.x)
                        Dim Dx1x0 = (x1 - x0)
                        Dim Dx0x = (oldXY.x - x0)
                        Dim Dy1y = (y1 - oldXY.y)
                        Dim Dy1y0 = (y1 - y0)
                        Dim Dy0y = (oldXY.y - y0)

                        Dim Ry0 = Lx0y0
                        Dim Ry1 = Lx0y1
                        If Dx1x0 <> 0 Then
                            Ry0 = (Lx0y0 * Dx1x / Dx1x0) + (Lx1y0 * Dx0x / Dx1x0)
                            Ry1 = (Lx0y1 * Dx1x / Dx1x0) + (Lx1y1 * Dx0x / Dx1x0)
                        End If

                        Dim Rxy = Ry0
                        If Dy1y0 <> 0 Then
                            Rxy = (Ry0 * Dy1y / Dy1y0) + (Ry1 * Dy0y / Dy1y0)
                        End If

                        Dim oldI = (roundNearestInteger(oldXY.y) * Header.Width) + roundNearestInteger(oldXY.x)

                        With newPixels(newI)
                            .Value = Rxy
                            .LayerID = PixelData(oldI).LayerID
                        End With
                    Next
                Next
                Header.Width = newWidth
                Header.Height = newHeight
                PixelData = newPixels
            End If
        End If
        Return True
    End Function
End Class
