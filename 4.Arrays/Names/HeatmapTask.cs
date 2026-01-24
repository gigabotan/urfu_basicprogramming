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

        var xLabels = GenerateNumberArray(30, 2);
        var yLabels = GenerateNumberArray(12, 1);

        return new HeatmapData("Карта интенсивностей рождаемости", heat, xLabels, yLabels);
    }

    private static string[] GenerateNumberArray(int count, int startValue)
    {
        var array = new string[count];
        for (var i = 0; i < count; i++)
        {
            array[i] = (i + startValue).ToString();
        }
        return array;
    }
}
