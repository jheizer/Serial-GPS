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

Public Class Form1
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim file As New IO.StreamReader("e:\gpstest.txt")
        Dim nmea As New SerialGPS.NMEAParser("COM3")
        AddHandler nmea.NewGPRMC, AddressOf GotNewGPRMC
        AddHandler nmea.NewGPGSV, AddressOf GotNewGPGSV
        AddHandler nmea.NewGPGGA, AddressOf GotNewGPGGA
        AddHandler nmea.NewGPGLL, AddressOf GotNewGPGLL
        AddHandler nmea.NewGPGSA, AddressOf GotNewGPGsa

        nmea.StartGPS()
    End Sub

    Private Sub GotNewGPRMC(ByVal data As SerialGPS.GPRMC)
        Debug.WriteLine("GPRMC - " & data.Latitude & "  " & data.Longitude & "  " & data.Bearing & "  " & data.Speed & "  " & data.Speed)
    End Sub

    Private Sub GotNewGPGSV(ByVal data As SerialGPS.GPGSV)
        Debug.WriteLine("GPGSV - " & data.Sats.Count) '& "  " & data.Sats(0).code & "  " & data.Sats(0).Azimuth & "  " & data.Sats(0).Elevation & "  " & data.Sats(0).SNR)
    End Sub

    Private Sub GotNewGPGGA(ByVal data As SerialGPS.GPGGA)
        Debug.WriteLine("GPGGA - " & data.NumberOfSats & "  " & data.Altitude & "  " & data.UTCTime)
    End Sub

    Private Sub GotNewGPGLL(ByVal data As SerialGPS.GPGLL)
        Debug.WriteLine("GPGLL - " & data.Latitude & "  " & data.Longitude)
    End Sub

    Private Sub GotNewGPGsa(ByVal data As SerialGPS.GPGSA)
        Debug.WriteLine("GPGSA - " & data.ListOfSats.Count & "  " & data.PDOP & "  " & data.HDOP & "  " & data.VDOP)
    End Sub
End Class
