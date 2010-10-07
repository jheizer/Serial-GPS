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
#End Region

Public Class GPGLL
    Public Longitude As Double
    Public Latitude As Double

    Public Sub New()
    End Sub

    Public Sub New(ByVal Pieces() As String)
        If Not Pieces(0) = "GPGLL" Then
            Return
        End If

        If Not String.IsNullOrEmpty(Pieces(2) & Pieces(3) & Pieces(4) & Pieces(5)) Then
            Latitude = PointFunctions.NMEALatToDecimal(Pieces(2), Pieces(3))
            Longitude = PointFunctions.NMEALongToDecimal(Pieces(4), Pieces(5))
        End If

    End Sub
End Class
