using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Solving Linear System using Augmented Matrix and \"Gauss Jordan Elimination\"\n");

        // Get the dimensions of the linear system
        Console.WriteLine("Enter number of rows and columns");

        int rows, columns;

        while (true)
        {
            Console.Write("Enter rows: ");
            if (int.TryParse(Console.ReadLine(), out rows))
            {
                Console.Write("Enter columns: ");
                if (int.TryParse(Console.ReadLine(), out columns))
                {
                    break;
                }
            }

            Console.WriteLine("Please Enter positive integers only");
        }

        // Initialize the augmented matrix and linear equation array
        double[][] augmentedMatrix = new double[rows][];
        double[] linearEq = new double[columns];

        // Input coefficients for each linear equation
        for (int r = 0; r < rows; r++)
        {
            Console.WriteLine($"\nEnter values of linear equation {r + 1}: ");
            for (int c = 0; c < columns; c++)
            {
                double val;
                while (true)
                {
                    Console.Write($"Enter v{r + 1} {c + 1}: ");
                    if (double.TryParse(Console.ReadLine(), out val))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter numbers only");
                    }
                }

                linearEq[c] = val;
            }

            // Copy the linear equation values to the augmented matrix
            augmentedMatrix[r] = new double[columns];
            Array.Copy(linearEq, augmentedMatrix[r], columns);
        }

        Console.WriteLine("\nEntered Augmented Matrix:\n");

        // Display the entered augmented matrix
        for (int r = 0; r < rows; r++)
        {
            Console.WriteLine(string.Join(" ", augmentedMatrix[r]));
        }

        Console.WriteLine("\n");

        // Convert Augmented Matrix to Reduced Row-Echelon Form
        for (int level = 0; level < augmentedMatrix.Length; level++)
        {
            int currentRow = -1;

            // Find the first non-zero element in the current column
            for (int c = 0; c < augmentedMatrix[0].Length - 1; c++)
            {
                for (int r = level; r < augmentedMatrix.Length; r++)
                {
                    if (augmentedMatrix[r][c] != 0)
                    {
                        currentRow = r;
                        break;
                    }
                }

                // Swap rows if a non-zero element is found
                if (currentRow != -1)
                {
                    double[] temp = augmentedMatrix[level];
                    augmentedMatrix[level] = augmentedMatrix[currentRow];
                    augmentedMatrix[currentRow] = temp;

                    // Display the matrix after row swapping
                    DisplayMatrix(augmentedMatrix);

                    // Scale the current row to make the pivot element 1
                    if (augmentedMatrix[level][c] != 1)
                    {
                        double dividend = augmentedMatrix[level][c];
                        for (int i = 0; i < augmentedMatrix[level].Length; i++)
                        {
                            augmentedMatrix[level][i] /= dividend;
                        }
                    }

                    // Eliminate other non-zero elements in the current column
                    for (int i = 0; i < augmentedMatrix.Length; i++)
                    {
                        if (augmentedMatrix[i][c] == 0 || i == level)
                        {
                            continue;
                        }

                        double numOfMultiply = (augmentedMatrix[i][c] / augmentedMatrix[level][c]) * -1;

                        for (int j = 0; j < augmentedMatrix[0].Length; j++)
                        {
                            augmentedMatrix[i][j] = (augmentedMatrix[level][j] * numOfMultiply) + augmentedMatrix[i][j];
                        }
                    }

                    // Display the matrix after elimination
                    DisplayMatrix(augmentedMatrix);

                    break;
                }
            }
        }

        // Check solutions of the Reduced Row-Echelon Matrix
        if (augmentedMatrix[rows - 1][columns - 2] == 0 && augmentedMatrix[rows - 1][columns - 1] == 0)
        {
            Console.WriteLine("Linear System has Infinite Solutions");
        }
        else if (augmentedMatrix[rows - 1][columns - 2] == 0 && augmentedMatrix[rows - 1][columns - 1] != 0)
        {
            Console.WriteLine("Linear System has No Solutions");
        }
        else
        {
            Console.WriteLine("The results are");
            // Display the solutions
            DisplaySolutions(augmentedMatrix);
        }
    }

    // Helper method to display the augmented matrix
    static void DisplayMatrix(double[][] matrix)
    {
    
        for (int i = 0; i < matrix.Length; i++)
        {
            Console.WriteLine(string.Join(" ", matrix[i]));
        }

        Console.WriteLine("\n");
    }

    // Helper method to display the solutions
    static void DisplaySolutions(double[][] augmentedMatrix)
    {
        for (int r = 0; r < augmentedMatrix.Length; r++)
        {
            string equation = "";
            for (int c = 0; c < augmentedMatrix[r].Length - 1; c++)
            {
                double coefficient = augmentedMatrix[r][c];
                if (coefficient != 0)
                {
                    // Build the equation string
                    equation += $"{(coefficient != 1 ? coefficient.ToString() : "")}X{c + 1}  ";
                }
            }

            double constantTerm = augmentedMatrix[r][augmentedMatrix[r].Length - 1];

            // Append the constant term to the equation string
            equation += $"= {constantTerm}";

            Console.WriteLine(equation);
        }
    }
}
