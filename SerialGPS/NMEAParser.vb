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

Public Class NMEAParser
    Implements IDisposable

    Public Event NewGPRMC(ByVal Data As GPRMC)
    Public Event NewGPGSV(ByVal Data As GPGSV)
    Public Event NewGPGGA(ByVal Data As GPGGA)
    Public Event NewGPGLL(ByVal Data As GPGLL)
    Public Event NewGPGSA(ByVal Data As GPGSA)

    Private m_GPS As GPSDevice
    Private m_ComPort As String
    Private m_BuildingSatView As GPGSV

    Private m_File As IO.StreamReader
    Private WithEvents m_FileReader As New System.ComponentModel.BackgroundWorker

    Public Sub New(ByVal ComPort As String)
        m_ComPort = ComPort
        m_GPS = New GPSDevice(ComPort)
        AddHandler m_GPS.NewLine, AddressOf ParseLine
    End Sub

    Public Sub New(ByVal File As IO.StreamReader)
        m_File = File
    End Sub

    Public Function StartGPS() As Boolean
        Return StartGPS(True)
    End Function

    Public Function StartGPS(ByVal CheckPortForGPSDevice As Boolean) As Boolean
        If Not m_GPS Is Nothing Then
            If CheckPortForGPSDevice Then
                If Not ScanPorts.CheckPortForGPS(m_ComPort) Then
                    Return False
                End If
            End If
            If Not m_GPS.StartGPS() Then
                Return False
            End If
        Else
            If Not m_FileReader.IsBusy Then
                m_FileReader.RunWorkerAsync()
            End If
        End If
        Return True
    End Function

    Private Sub m_FileReader_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles m_FileReader.DoWork
        While Not m_File.EndOfStream
            ParseLine(m_File.ReadLine())
            Threading.Thread.Sleep(100)
        End While
        m_File.Close()
    End Sub

    Public Sub StopGPS()
        m_GPS.StopGPS()
    End Sub

    Public Sub ParseLine(ByVal Line As String)
        Dim Pieces() As String

        If PointFunctions.ChecksumLine(Line) Then
            Line = Line.Trim
            Line = Line.Replace("$", "")
            Line = Line.Remove(Line.Length - 3, 3)
            Pieces = Line.Split(",")

            Select Case Pieces(0)
                Case Is = "GPRMC"
                    RaiseEvent NewGPRMC(New GPRMC(Pieces))

                Case Is = "GPGSV"
                    If Pieces(2) = 1 Then
                        m_BuildingSatView = New GPGSV(Pieces)
                    ElseIf Pieces(2) = m_BuildingSatView.NumberOfSentences + 1 Then
                        m_BuildingSatView.AddLine(Pieces)
                    ElseIf Pieces(2) <= m_BuildingSatView.NumberOfSentences Then
                        'we missed a part.
                        m_BuildingSatView = New GPGSV
                    End If

                    If Pieces(1) = Pieces(2) Then
                        RaiseEvent NewGPGSV(m_BuildingSatView)
                    End If

                Case Is = "GPGGA"
                    RaiseEvent NewGPGGA(New GPGGA(Pieces))

                Case Is = "GPGLL"
                    RaiseEvent NewGPGLL(New GPGLL(Pieces))

                Case Is = "GPGSA"
                    RaiseEvent NewGPGSA(New GPGSA(Pieces))

            End Select

        End If

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Try
            m_GPS.StopGPS()
        Catch ex As Exception
        End Try
    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            m_GPS.StopGPS()
            m_GPS.Dispose()
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
