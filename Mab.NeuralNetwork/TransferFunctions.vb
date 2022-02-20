Public Class TransferFunctions
    Public Shared Function Linear(x As Double) As Double
        Return x
    End Function
    Public Shared Function HardLimit(x As Double) As Double
        If x > 0 Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Shared Function StepHalf(x As Double) As Double
        If x > 0.5 Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Shared Function TanSig(x As Double) As Double
        Return (2 / (1 + Math.Exp(-2 * x)) - 1)
    End Function
    Public Shared Function Sigmoid(x As Double) As Double
        Return (1.0 / (1.0 + Math.Exp(-x)))
    End Function
End Class
