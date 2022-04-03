Imports System

Module Program
    Sub Main(args As String())
        TestGA()
    End Sub

    Private Sub TestPerceptron()
        Dim db As New List(Of PerceptronData)
        Dim pr1 As New PerceptronData(0, 0, 0) ' add and oprator as a test data for our Perceptron
        Dim pr2 As New PerceptronData(0, 0, 1)
        Dim pr3 As New PerceptronData(0, 1, 0)
        Dim pr4 As New PerceptronData(1, 1, 1)

        db.Add(pr1)
        db.Add(pr2)
        db.Add(pr3)
        db.Add(pr4)

        Dim Machine As New Perceptron()
        Machine.TrainByBackpropagation(db, 0.2) 'train our preseptron by our simple database :)

        Console.ReadKey()
    End Sub

    Public Sub TestGA()
        Dim gaSolver As New GA(16, 0, 16) With {
            .FitnessFunction = Function(ch)
                                   Dim CurQueen As Integer
                                   Dim TestQueen As Integer
                                   Dim confilect As Integer
                                   For i As Integer = 0 To ch.Genes.Length - 1
                                       CurQueen = ch.Genes(i)
                                       For j As Integer = 0 To ch.Genes.Length - 1
                                           TestQueen = ch.Genes(j)
                                           If (Not (i = j)) AndAlso ((TestQueen = CurQueen) OrElse (j - i) = (CurQueen - TestQueen) OrElse (i - j) = (CurQueen - TestQueen)) Then
                                               confilect += 1
                                           End If
                                       Next
                                   Next
                                   Return confilect
                               End Function,
            .EndFunction = Function(ch, geneCount)
                               Return ch.Fitness = 0
                           End Function,
            .MutationRangeLength = 3,
            .MutationRate = 0.05,
            .PopulationCount = 100
        }
        gaSolver.MutationMethods.Add(New RandomResettingMutation())

        gaSolver.CrossOverMethods.Add(New TwoPointCrossOver())
        gaSolver.CrossOverMethods.Add(New UniformCrossOver())

        AddHandler gaSolver.GenerationProduced, Sub()
                                                    Console.CursorVisible = False
                                                    Console.SetCursorPosition(0, 0)
                                                    Console.Write(New String(" ", Console.WindowWidth))
                                                    Console.SetCursorPosition(0, 0)
                                                    Console.WriteLine($"Generation:{gaSolver.BestGenerations.Count} Fittnes:{gaSolver.BestGenerations.Last().Fitness}")
                                                    Dim best = gaSolver.BestGenerations.Last()
                                                    For i = 0 To gaSolver.MaxGeneValue - 1
                                                        For j = 0 To gaSolver.MaxGeneValue - 1
                                                            If j = best.Genes(i) Then
                                                                Console.Write("1 ")
                                                            Else
                                                                Console.Write("0 ")
                                                            End If
                                                        Next
                                                        Console.WriteLine()
                                                    Next
                                                    Console.WriteLine()
                                                End Sub

        gaSolver.Start()


    End Sub

    Private Sub Test_ceck(sender As Object, e As EventArgs)

    End Sub
End Module
