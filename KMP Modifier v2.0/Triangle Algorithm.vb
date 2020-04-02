Module Triangle_Algorithm
    Function TriangleArea(ByVal X As Double(), ByVal Y As Double()) As Integer
        Try
            Return Math.Abs((X(1) * Y(0) - X(0) * Y(1)) + (X(2) * Y(1) - X(1) * Y(2)) + (X(0) * Y(2) - X(2) * Y(0))) / 2
        Catch ex As Exception : Return 0 : End Try
    End Function

    Public Function CoordinateTriangleCheck(ByVal Vertices As Point(), Y As Integer(), ByVal Coordinate As Point) As Boolean
        Try
            Dim X(3), Z(3) As Double
            X = {Vertices(0).X, Vertices(1).X, Vertices(2).X, Coordinate.X}
            Z = {Vertices(0).Y, Vertices(1).Y, Vertices(2).Y, Coordinate.Y}

            Dim Area, Tot As Double
            Area = TriangleArea(X, Z)
            
            Tot += TriangleArea({X(0), X(1), X(3)}, {Z(0), Z(1), Z(3)})
            Tot += TriangleArea({X(1), X(2), X(3)}, {Z(1), Z(2), Z(3)})
            Tot += TriangleArea({X(0), X(2), X(3)}, {Z(0), Z(2), Z(3)})

            Dim diff As Double = Math.Abs(Area - Tot)
            If diff < 2 And Not Area = 0 Then
                Return True
            End If
        Catch ex As OverflowException
            Return False
        End Try
        Return False
    End Function

    Dim Pos1_Proc, Pos1_Y, Pos1_Z, Pos2_Proc, Pos2_Y, Pos2_Z As Decimal

    Public Function CoordinateTriangleY(ByVal Vertices As Point(), Y As Single(), ByVal Coordinate As Point) As Single
        If (Coordinate.X > Vertices(0).X And Coordinate.X < Vertices(1).X) Or (Coordinate.X < Vertices(0).X And Coordinate.X > Vertices(1).X) Then
            If Vertices(0).X = Vertices(1).X Then
                Pos1_Proc = 1
            Else
                Pos1_Proc = (Coordinate.X - Vertices(1).X) / (Vertices(0).X - Vertices(1).X)
            End If
            Pos1_Y = (Y(0) - Y(1)) * Pos1_Proc + Y(1)
            Pos1_Z = (Vertices(0).Y - Vertices(1).Y) * Pos1_Proc + Vertices(1).Y

            If (Coordinate.X > Vertices(2).X And Coordinate.X < Vertices(1).X) Or (Coordinate.X < Vertices(2).X And Coordinate.X > Vertices(1).X) Then
                If Vertices(2).X = Vertices(1).X Then
                    Pos2_Proc = 1
                Else
                    Pos2_Proc = (Coordinate.X - Vertices(1).X) / (Vertices(2).X - Vertices(1).X)
                End If
                Pos2_Y = (Y(2) - Y(1)) * Pos2_Proc + Y(1)
                Pos2_Z = (Vertices(2).Y - Vertices(1).Y) * Pos2_Proc + Vertices(1).Y
            ElseIf (Coordinate.X > Vertices(2).X And Coordinate.X < Vertices(0).X) Or (Coordinate.X < Vertices(2).X And Coordinate.X > Vertices(0).X) Then
                Pos2_Proc = (Coordinate.X - Vertices(0).X) / (Vertices(2).X - Vertices(0).X)
                Pos2_Y = (Y(2) - Y(0)) * Pos2_Proc + Y(0)
                Pos2_Z = (Vertices(2).Y - Vertices(0).Y) * Pos2_Proc + Vertices(0).Y
            Else
            End If
        ElseIf (Coordinate.X > Vertices(2).X And Coordinate.X < Vertices(1).X) Or (Coordinate.X < Vertices(2).X And Coordinate.X > Vertices(1).X) Then
            If Vertices(2).X = Vertices(1).X Then
                Pos1_Proc = 1
            Else
                Pos1_Proc = (Coordinate.X - Vertices(1).X) / (Vertices(2).X - Vertices(1).X)
            End If
            Pos1_Y = (Y(2) - Y(1)) * Pos1_Proc + Y(1)
            Pos1_Z = (Vertices(2).Y - Vertices(1).Y) * Pos1_Proc + Vertices(1).Y

            If (Coordinate.X > Vertices(2).X And Coordinate.X < Vertices(1).X) Or (Coordinate.X < Vertices(2).X And Coordinate.X > Vertices(1).X) Then
                Pos2_Proc = (Coordinate.X - Vertices(1).X) / (Vertices(2).X - Vertices(1).X)
                Pos2_Y = (Y(2) - Y(1)) * Pos2_Proc + Y(1)
                Pos2_Z = (Vertices(2).Y - Vertices(1).Y) * Pos2_Proc + Vertices(1).Y
            Else
            End If
        Else
        End If
        If Pos2_Y = Pos1_Y Then Return Pos1_Y
        Dim Final_Proc As Single = (Coordinate.Y - Pos1_Z) / (Pos2_Z - Pos1_Z)
        Return (Pos2_Y - Pos1_Y) * Final_Proc + Pos1_Y

    End Function
End Module
