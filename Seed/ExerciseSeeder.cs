using System.Text.Json;
using FitnessManager.Data;
using FitnessManager.Models;

public static class ExerciseSeeder
{
    public static void Seed(DataContext context)
    {
        Console.WriteLine("🚀 Ejecutando seeder de ejercicios...");

        if (context.Exercises.Any()) return; // Ya cargado

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "free-exercise-db", "dist", "exercises.json");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"⚠️ No se encontró el archivo en: {filePath}");
            return;
        }

        var jsonData = File.ReadAllText(filePath);

        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var exercises = JsonSerializer.Deserialize<List<ExerciseJson>>(jsonData, jsonOptions);
        //chequear si exercises es null o vacío
        if (exercises == null || exercises.Count == 0)
        {
            Console.WriteLine("⚠️ No se encontraron ejercicios en el archivo JSON.");
            return;
        }
        else
        {
            Console.WriteLine($"📦 Se encontraron {exercises.Count} ejercicios para cargar.");
        }

        foreach (var e in exercises)
        {
            Console.WriteLine($"   - Cargando ejercicio: {e.Name}");
            context.Exercises.Add(new Exercise
            {
                Name = e.Name,
                Force = e.Force,
                Level = e.Level,
                Mechanic = e.Mechanic,
                Equipment = e.Equipment,
                PrimaryMuscles = string.Join(", ", e.PrimaryMuscles ?? []),
                SecondaryMuscles = string.Join(", ", e.SecondaryMuscles ?? []),
                Instructions = string.Join(" ", e.Instructions ?? []),
                Category = e.Category,
                Image = $"https://www.exerciseapi.com/images/exercises/{e.Name.ToLower().Replace(" ", "-")}.gif"
            });
        }

        context.SaveChanges();
    }

    private class ExerciseJson
    {
        public string Name { get; set; }
        public string Force { get; set; }
        public string Level { get; set; }
        public string Mechanic { get; set; }
        public string Equipment { get; set; }
        public List<string> PrimaryMuscles { get; set; }
        public List<string> SecondaryMuscles { get; set; }
        public List<string> Instructions { get; set; }
        public string Category { get; set; }
    }
}