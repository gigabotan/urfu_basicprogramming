namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var days = new double[31];
        foreach (var n in names)
        {
            if (n.Name == name && n.BirthDate.Day != 1)
            {
                days[n.BirthDate.Day - 1]++;
            }
        }

        var dayLabels = new string[31];
        for (var i = 0; i < 31; i++)
            dayLabels[i] = (i + 1).ToString();

        return new HistogramData(
            $"Рождаемость людей с именем '{name}'", 
            dayLabels, 
            days);
    }
}