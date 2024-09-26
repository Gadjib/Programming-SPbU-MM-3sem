//namespace MatrixMultiplier;

using System;
using System.Threading;

var matrix1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
var matrix2 = new int[,] { { 3, 2, 1 }, { 6, 5, 4 }, { 9, 8, 7 } };

var columns = matrix1.GetLength(0);
var rows = matrix2.GetLength(1);
var cellsNumber = columns * rows;

var matrix3 = new int[columns, rows];

var threads = new Thread[cellsNumber];

static int countCell(int[,] matrix1, int[,] matrix2, int n, int m)
{
    int result = 0;
    for (int i = 0; i < matrix1.GetLength(1); i++)
    {
        result += matrix1[n, i] * matrix2[i, m];
    }
    return result;
}

for (var i = 0; i < cellsNumber; ++i)
{
    var currentColumn = i / rows;
    var currentRow = i % rows;
    threads[i] = new Thread(() =>
    {
        matrix3[currentColumn, currentRow] = countCell(matrix1, matrix2, currentColumn, currentRow);
    });
}

foreach (var thread in threads)
    thread.Start();
foreach (var thread in threads) 
    thread.Join();

int counter = 0;

foreach (var i in matrix3)
{
    Console.Write(i + "\t");
    counter++;
    if (counter % columns == 0)
    {
        Console.WriteLine();
    }
}