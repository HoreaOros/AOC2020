#region Input parsing
using System.ComponentModel;

string text = File.ReadAllText("input.txt");
string[] lines = text.Split(Environment.NewLine);

//mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
//trh fvjkl sbzzf mxmxvkd (contains dairy)
//sqjhc fvjkl(contains soy)
//sqjhc mxmxvkd sbzzf (contains fish)
List<(List<string> ingredients, List<string> alergens)> foods = new();
foreach (string line in lines)
{
    string[] t = line.Split(" (contains ");
    string[] t1 = t[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    string[] t2 = t[1].Split(new char[] {' ' , ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
    foods.Add((new List<string>(t1), new List<string>(t2)));
}
#endregion

#region Part1
Dictionary<string, HashSet<string>> dict = new();
HashSet<string> allIngredients = new();
foreach(var food in foods)
{
    foreach(var alergen in food.alergens)
        dict[alergen] = new HashSet<string>();
    foreach(var ingrediet in food.ingredients)
        allIngredients.Add(ingrediet);
}
foreach (var food in foods)
{
    HashSet<string> ingredients = new HashSet<string>(food.ingredients);
    foreach(var alergen in food.alergens)
        if(dict[alergen].Count == 0)
            dict[alergen] = ingredients;
        else
            dict[alergen] = new HashSet<string>(dict[alergen].Intersect(ingredients).ToList());
}
Dictionary<string, string> ingredientsWithAlergens = new (); // map the ingredient on the alergen
bool done = false;


while(!done)
{
    done = true;
    foreach (var alergen in dict.Keys)
        if (dict[alergen].Count == 1)
        {
            string ing = dict[alergen].First();
            ingredientsWithAlergens[ing] = alergen;
            foreach(var item in dict.Keys)
                if (dict[item].Contains(ing))
                {
                    dict[item].Remove(ing);
                    done = false;
                }
            break;
        }

}
long r1 = 0;
foreach(var food in foods)
{
    foreach (var ingredient in food.ingredients)
        if (!ingredientsWithAlergens.Keys.Contains(ingredient))
            r1++;
}
Console.WriteLine(r1);
#endregion

#region Part2
List<(string alergen, string ingredient)> lst = new();
foreach (var item in ingredientsWithAlergens)
{
    lst.Add((item.Value, item.Key));
}


lst.Sort((x, y) => x.alergen.CompareTo(y.alergen));
Console.WriteLine(string.Join(",", lst.Select(x => x.ingredient)));
#endregion