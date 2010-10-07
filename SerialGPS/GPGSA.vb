#Region "GPL"
'    This file is part of SerialGPS.

'    SerialGPS is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.

'    SerialGPS is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.

'    You should have received a copy of the GNU General Public License
'    along with SerialGPS.  If not, see <http://www.gnu.org/licenses/>.

'    Copyright 2010 Jonathan Heizer jheizer@gmail.com
#End Region

Public Class GPGSA

    Public AutoSelection As String
    Public FixType As Integer
    Public ListOfSats As New Generic.List(Of String)
    Public PDOP As Double
    Public HDOP As Double
    Public VDOP As Double

    Public Sub New()
    End Sub

    Public Sub New(ByVal Pieces() As String)
        If Not Pieces(0) = "GPGSA" Then
            Return
        End If

        AutoSelection = Pieces(1)

        If Not String.IsNullOrEmpty(Pieces(2)) Then
            FixType = CInt(Pieces(2))
        End If

        For i As Integer = 3 To 14
            If Not String.IsNullOrEmpty(Pieces(i)) Then
                ListOfSats.Add(Pieces(i))
            End If
        Next

        If Not String.IsNullOrEmpty(Pieces(15)) Then
            PDOP = Double.Parse(Pieces(15))
        End If

        If Not String.IsNullOrEmpty(Pieces(16)) Then
            HDOP = Double.Parse(Pieces(16))
        End If

        If Not String.IsNullOrEmpty(Pieces(17)) Then
            VDOP = Double.Parse(Pieces(17))
        End If

    End Sub

End Class
