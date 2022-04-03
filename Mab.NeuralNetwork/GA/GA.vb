Public Class GA
    Public Const DefaultMutationRate As Double = 0.05
    Private Const DefaultPopulationCount As Integer = 100
    Private ReadOnly _RandomGen As Random




    Public Property PopulationCount As Integer = 100

    Public Property MutationRate As Double = 0.02

    Public Property MutationRangeLength As Integer

    Public Property MaxGenration As Integer

    Public ReadOnly Property ChromosomeLength As Integer
    Public ReadOnly Property MinGeneValue As Integer
    Public ReadOnly Property MaxGeneValue As Integer

    Public Property FitnessFunction As Func(Of Chromosome, Double)
    Public Property EndFunction As Func(Of Chromosome, Integer, Boolean)

    Public Event GenerationProduced As EventHandler

    Public Property FittnessGoal As Double
    Public ReadOnly Property CrossOverMethods As List(Of ICrossOver)
    Public ReadOnly Property MutationMethods As List(Of IMutation)

    Private _Population As List(Of Chromosome)
    Public Property Population As List(Of Chromosome)
        Get
            Return _Population
        End Get
        Private Set(value As List(Of Chromosome))
            _Population = value
        End Set
    End Property

    Private _BestGenerations As List(Of Chromosome)
    Public ReadOnly Property BestGenerations As IReadOnlyCollection(Of Chromosome)
        Get
            Return _BestGenerations
        End Get
    End Property

    Public Sub New(ChromosomeLength As Integer, MinGeneValue As Integer, MaxGeneValue As Integer)
        Me.ChromosomeLength = ChromosomeLength
        Me.MinGeneValue = MinGeneValue
        Me.MaxGeneValue = MaxGeneValue

        _RandomGen = New Random()
        _Population = New List(Of Chromosome)
        _BestGenerations = New List(Of Chromosome)
        CrossOverMethods = New List(Of ICrossOver)
        MutationMethods = New List(Of IMutation)

    End Sub






    Protected Sub SwapMutation(Selected As Chromosome, GeneCount As Integer)
        For i = 0 To GeneCount - 1 'swap the range
            Dim FirstGeneIndex = _RandomGen.Next(ChromosomeLength)
            Dim SecoundGeneIndex = _RandomGen.Next(ChromosomeLength)

            Dim Temp = Selected.Genes(FirstGeneIndex)
            Selected.Genes(FirstGeneIndex) = Selected.Genes(SecoundGeneIndex)
            Selected.Genes(SecoundGeneIndex) = Temp
        Next
    End Sub
    Protected Overridable Sub UpdateFittness()
        If FitnessFunction = Nothing Then
            Throw New ArgumentNullException("fittness function not defind!")
        End If
        For Each chromosome In Population
            chromosome.Fitness = FitnessFunction(chromosome)
        Next
    End Sub
    Protected Overridable Sub GenerateFirstPopulation()
        Population = New List(Of Chromosome)
        For i = 0 To PopulationCount - 1
            Population.Add(GenerateRandomChromosome())
        Next
    End Sub
    Protected Overridable Function GenerateRandomChromosome() As Chromosome
        Dim rndChromosome As New Chromosome(Me.ChromosomeLength)
        For i = 0 To ChromosomeLength - 1
            rndChromosome.Genes(i) = _RandomGen.Next(MinGeneValue, MaxGeneValue)
        Next
        Return rndChromosome
    End Function
    Protected Function GenerateUniqeRandomArrayOfIntegers(Count As Integer) As Integer()
        Return GenerateUniqeRandomArrayOfIntegers(Count, Count)
    End Function
    Protected Function GenerateUniqeRandomArrayOfIntegers(Count As Integer, Max As Integer) As Integer()
        Dim numbers As New HashSet(Of Integer)
        Do Until numbers.Count = Count
            Dim Current As Integer = _RandomGen.Next(Max)
            numbers.Add(Current)
        Loop
        Return numbers.ToArray()
    End Function
    Protected Sub DoCrossOver()
        Dim NewGenration As New List(Of Chromosome)

        Dim RandomChromosomesIndex = GenerateUniqeRandomArrayOfIntegers(PopulationCount)

        For i As Integer = 0 To PopulationCount - 1 Step 2
            Dim f = RandomChromosomesIndex(i)
            Dim l = RandomChromosomesIndex(i + 1)
            Dim female = Population(f)
            Dim male = Population(l)
            Population.AddRange(DoCrossOver(female, male))
        Next
    End Sub
    Public Sub Start()
        If EndFunction = Nothing Then
            Throw New ArgumentNullException("end function not defind!")
        End If
        Dim RepeatCount = 0
        Population = New List(Of Chromosome)()
        GenerateFirstPopulation()

        Do
            DoCrossOver()
            DoMutation()
            UpdateFittness()
            Population = Population.OrderBy(Function(ch) ch.Fitness).Take(PopulationCount).ToList()
            _BestGenerations.Add(Population.First())
            RepeatCount += 1
            RaiseEvent GenerationProduced(Me, Nothing)
        Loop Until EndFunction(Population.First(), RepeatCount)
    End Sub
    Protected Function DoCrossOver(ch1 As Chromosome, ch2 As Chromosome) As Chromosome()
        Dim SelectedMethodIndex = _RandomGen.Next(CrossOverMethods.Count)
        Return CrossOverMethods(SelectedMethodIndex).DoCrossOver(ch1, ch2, _RandomGen)
    End Function

    Protected Sub DoMutation()
        Dim MutationCount As Integer = Population.Count * MutationRate
        Dim MutationIndex = GenerateUniqeRandomArrayOfIntegers(MutationCount, PopulationCount)
        For Each index In MutationIndex
            Dim SelectedChromosome = Population(index)
            Dim SelectedMethodIndex = _RandomGen.Next(MutationMethods.Count)
            MutationMethods(SelectedMethodIndex).DoMutation(SelectedChromosome, MutationRangeLength, MinGeneValue, MaxGeneValue, _RandomGen)
        Next
    End Sub
    Protected Sub MakeGenarationPool()
        Population = Population.OrderBy(Function(p) p.Fitness).ToList()
        If Population.Count = PopulationCount Then
            Return
        End If
        Population = Population.Take(PopulationCount).ToList()
    End Sub

End Class
