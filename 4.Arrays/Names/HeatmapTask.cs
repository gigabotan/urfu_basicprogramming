namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        var heat = new double[30, 12];
        for (var i = 0; i < names.Length; i++)
        {
            var birthdate = names[i].BirthDate;
            if (birthdate.Day == 1)
                continue;
            heat[birthdate.Day - 2, birthdate.Month - 1]++;
        }

        var xLabels = new string[30];
        for (var i = 0; i < 30; i++)
        {
            xLabels[i] = (i + 2).ToString();
        }

        var yLabels = new string[12];
        for (var i = 0; i < 12; i++)
        {
            yLabels[i] = (i + 1).ToString();
        }

        return new HeatmapData("Карта интенсивностей рождаемости", heat, xLabels, yLabels);
    }
}