Public Class InversionMutation
    Implements IMutation

    Public Sub DoMutation(Selected As Chromosome, RangeLength As Integer, MinGeneValue As Integer, MaxGeneValue As Integer, RandomGenerator As Random) Implements IMutation.DoMutation
        Dim MaxGeneIndex As Integer = Selected.Genes.Length - RangeLength
        Dim StartIndex As Integer = RandomGenerator.Next(0, MaxGeneIndex)

        Dim GeneRange(RangeLength - 1) As Integer
        Array.Copy(Selected.Genes, StartIndex, GeneRange, 0, RangeLength)
        Array.Reverse(GeneRange)

        Array.Copy(GeneRange, 0, Selected.Genes, StartIndex, RangeLength)
    End Sub
End Class
