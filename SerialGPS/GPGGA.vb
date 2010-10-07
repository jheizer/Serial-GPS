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

Public Class GPGGA
    Public UTCTime As New DateTime
    Public Longitude As Double
    Public Latitude As Double
    Public FixQuality As Integer
    Public NumberOfSats As Integer
    Public HorzDilution As Decimal
    Public Altitude As Double
    Public HeightOfGeoid As Double

    Public Sub New()
    End Sub

    Public Sub New(ByVal Pieces() As String)
        If Not Pieces(0) = "GPGGA" Then
            Return
        End If

        If Not String.IsNullOrEmpty(Pieces(1)) Then
            UTCTime = New DateTime(Now.Year, Now.Month, Now.Day, Pieces(1).Substring(0, 2), Pieces(1).Substring(2, 2), Pieces(1).Substring(4, 2))
        End If

        If Not String.IsNullOrEmpty(Pieces(2) & Pieces(3) & Pieces(4) & Pieces(5)) Then
            Latitude = PointFunctions.NMEALatToDecimal(Pieces(2), Pieces(3))
            Longitude = PointFunctions.NMEALongToDecimal(Pieces(4), Pieces(5))
        End If

        If Not String.IsNullOrEmpty(Pieces(6)) Then
            FixQuality = CInt(Pieces(6))
        End If

        If Not String.IsNullOrEmpty(Pieces(7)) Then
            NumberOfSats = CInt(Pieces(7))
        End If

        If Not String.IsNullOrEmpty(Pieces(8)) Then
            HorzDilution = Double.Parse(Pieces(8))
        End If

        If Not String.IsNullOrEmpty(Pieces(9)) Then
            Altitude = Double.Parse(Pieces(9))
        End If

        If Not String.IsNullOrEmpty(Pieces(11)) Then
            HeightOfGeoid = Double.Parse(Pieces(11))
        End If

    End Sub

End Class
