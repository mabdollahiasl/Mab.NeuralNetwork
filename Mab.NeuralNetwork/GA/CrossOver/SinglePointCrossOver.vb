Public Class SinglePointCrossOver
    Implements ICrossOver

    Public Function DoCrossOver(Father As Chromosome, Mother As Chromosome, RandomGenerator As Random) As Chromosome() Implements ICrossOver.DoCrossOver
        Dim ChromosomeLength As Integer = Father.Genes.Length
        Dim Male As New Chromosome(ChromosomeLength)
        Dim FeMale As New Chromosome(ChromosomeLength)

        Dim SelectedPoint As Integer
        SelectedPoint = RandomGenerator.Next(ChromosomeLength)

        For i = 0 To ChromosomeLength - 1
            If i < SelectedPoint Then
                Male.Genes(i) = Father.Genes(i)
                FeMale.Genes(i) = Mother.Genes(i)
            Else
                FeMale.Genes(i) = Father.Genes(i)
                Male.Genes(i) = Mother.Genes(i)
            End If
        Next
        Return New Chromosome() {FeMale, Male}
    End Function
End Class
