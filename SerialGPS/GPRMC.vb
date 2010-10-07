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

Public Class GPRMC
    Public Longitude As Double
    Public Latitude As Double
    Public Speed As Double
    Public Bearing As Double
    Public SatFix As Boolean = False

    Public Sub New()
    End Sub

    Public Sub New(ByVal Pieces() As String)

        If Not String.IsNullOrEmpty(Pieces(2)) Then
            If Pieces(2) = "A" Then
                SatFix = True
            End If
        End If

        If Not String.IsNullOrEmpty(Pieces(3) & Pieces(4) & Pieces(5) & Pieces(6)) Then
            Latitude = PointFunctions.NMEALatToDecimal(Pieces(3), Pieces(4))
            Longitude = PointFunctions.NMEALongToDecimal(Pieces(5), Pieces(6))
        End If

        If Not String.IsNullOrEmpty(Pieces(7)) Then
            Speed = Double.Parse(Pieces(7)) * 1.15077945
        End If

        If Not String.IsNullOrEmpty(Pieces(8)) Then
            Bearing = Double.Parse(Pieces(8))
        End If


    End Sub

End Class
