Public Class ScrambleMutation
    Implements IMutation

    Public Sub DoMutation(Selected As Chromosome, RangeLength As Integer, MinGeneValue As Integer, MaxGeneValue As Integer, RandomGenerator As Random) Implements IMutation.DoMutation
        Dim MaxGeneIndex As Integer = Selected.Genes.Length - RangeLength
        Dim StartIndex As Integer = RandomGenerator.Next(0, MaxGeneIndex)
        Dim EndIndex As Integer = StartIndex + RangeLength

        For i = 0 To RangeLength - 1 'scramble the range
            Dim FirstGeneIndex = RandomGenerator.Next(StartIndex, EndIndex + 1)
            Dim SecoundGeneIndex = RandomGenerator.Next(StartIndex, EndIndex + 1)

            Dim Temp = Selected.Genes(FirstGeneIndex)
            Selected.Genes(FirstGeneIndex) = Selected.Genes(SecoundGeneIndex)
            Selected.Genes(SecoundGeneIndex) = Temp
        Next
    End Sub
End Class
