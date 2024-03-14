using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace JDBC.NET.Sample;

public abstract class ConsoleUtils
{
    private const int tableWidth = 100;
    private const int displayLimit = 10;

    protected static void PrintLine()
    {
        Console.WriteLine(new string('-', tableWidth));
    }

    public static string AlignCenter(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        return !string.IsNullOrEmpty(text)
            ? text.PadRight(width - (width - text.Length) / 2).PadLeft(width)
            : new string(' ', width);
    }

    public static void PrintRow(params string[] columns)
    {
        var width = (tableWidth - columns.Length) / columns.Length;
        var row = columns.Aggregate("|", (current, column) => current + (AlignCenter(column, width) + "|"));
        Console.WriteLine(row);
    }

    public static void PrintResult(DbDataReader reader)
    {
        Console.Clear();
        PrintLine();

        var columns = new List<string>();

        for (var i = 0; i < reader.FieldCount; i++)
        {
            columns.Add(reader.GetName(i));
        }

        PrintRow(columns.ToArray());
        PrintLine();

        var displayCount = 0;

        while (reader.Read())
        {
            if (displayCount >= displayLimit)
            {
                PrintRow($"Only the top {displayLimit} results were displayed.");
                PrintLine();
                break;
            }

            var items = new List<string>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                items.Add(reader.GetString(i));
            }

            PrintRow(items.ToArray());
            PrintLine();

            displayCount++;
        }
    }
}
