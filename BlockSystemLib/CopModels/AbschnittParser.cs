using Newtonsoft.Json;


public static class AbschnittParser
{
    public static List<Abschnitt> ParseAbschnitte(List<string> jsonStrings)
    {
        var abschnitte = new List<Abschnitt>();

        foreach (var json in jsonStrings)
        {
            try
            {
                var abschnitt = JsonConvert.DeserializeObject<Abschnitt>(json);
                if (abschnitt != null)
                {
                    abschnitte.Add(abschnitt);
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung, z. B. Logging
                Console.WriteLine($"Fehler beim Parsen: {ex.Message}");
            }
        }

        return abschnitte;
    }
}