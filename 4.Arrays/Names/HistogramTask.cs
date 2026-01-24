namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var birthCounts = new double[31];
        foreach (var person in names)
        {
            if (person.Name == name && person.BirthDate.Day != 1)
            {
                birthCounts[person.BirthDate.Day - 1]++;
            }
        }

        var dayLabels = GenerateDayLabels();

        return new HistogramData(
            $"Рождаемость людей с именем '{name}'",
            dayLabels,
            birthCounts);
    }

    private static string[] GenerateDayLabels()
    {
        var dayLabels = new string[31];
        for (var i = 0; i < 31; i++)
            dayLabels[i] = (i + 1).ToString();
        return dayLabels;
    }
}
