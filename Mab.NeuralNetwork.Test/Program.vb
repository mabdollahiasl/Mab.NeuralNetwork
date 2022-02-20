Imports System

Module Program
    Sub Main(args As String())
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
End Module
