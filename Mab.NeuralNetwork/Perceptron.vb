Public Class Perceptron
    Public Property Inputs As Double()
    Private Weights As Double()

    Public Sub New()


    End Sub
    Public Property Bias As Double
    Public ReadOnly Property Output As Double
        Get
            Dim Sum As Double
            For i As Integer = 0 To Inputs.Length - 1
                Sum += Inputs(i) * Weights(i)
            Next
            Sum += Bias
            Return TransferFunctions.HardLimit(Sum)
        End Get
    End Property
    Public Sub TrainByBackpropagation(LearnningData As List(Of PerceptronData), LearningRate As Double)
        Dim randomGenrator As New Random

        ReDim Inputs(LearnningData.First().Inputs.Length - 1)
        ReDim Weights(LearnningData.First().Inputs.Length - 1)
        Dim IterationCount As Integer = 0
        Do
            Console.Write($"Iteration{IterationCount}: ")

            For i As Integer = 0 To Weights.Length - 1
                Weights(i) = randomGenrator.NextDouble() * 2 - 1
                Console.Write($"Weight({i}):{Weights(i):N3} ")
            Next
            Console.Write($" Bias:{Bias:N3}")
            Console.WriteLine()

            Dim IsTrained As Boolean = True
            For li = 0 To LearnningData.Count - 1
                Dim CurrentLearningData = LearnningData(li)

                Me.Activate(CurrentLearningData.Inputs)

                Dim CurrentError = CurrentLearningData.Output - Me.Output
                If CurrentError <> 0 Then
                    IsTrained = False 'if there is an error we try for trainning again
                    For i As Integer = 0 To Weights.Length - 1
                        Weights(i) = Weights(i) + (LearningRate * CurrentLearningData.Inputs(i) * CurrentError)
                    Next
                    Bias += CurrentError
                    Exit For
                End If
            Next

            If IsTrained Then
                Exit Do
            End If
            IterationCount += 1
        Loop
    End Sub
    Public Sub Activate(SetInputs() As Double)
        For i As Integer = 0 To SetInputs.Length - 1
            Me.Inputs(i) = SetInputs(i)
            Console.Write($"{Me.Inputs(i)} ")
        Next
        Console.WriteLine($"= {Me.Output}")
    End Sub
End Class
