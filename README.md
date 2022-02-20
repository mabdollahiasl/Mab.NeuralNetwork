## Mab.NeuralNetwork

In this library I want to write a basic library for neural network.
I choose Visual Basic because of simplicity. But I'm going to port it to C++ in the feature.
the first class that I implemented is Perceptron. In the next days I want to add other class for multi layer Neural Network and Genetic Algorithm for training it.

## Perceptron
This class implemented Perceptron.

**Sample Code:**

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
