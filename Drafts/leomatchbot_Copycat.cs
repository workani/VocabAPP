Console.WriteLine("Hi, welocme to my @leomatch_bot copycat. The script will ask you couple of questions about yourself and print out!");

// get all info about user
Console.WriteLine("Your age: ");
int age = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Your city: ");
string city = Console.ReadLine();

Console.WriteLine("Your name: ");
string name = Console.ReadLine();

Console.WriteLine(
    "Tell more about yourself. Who are you looking for? What do you want to do? I'll find the best matches: ");
string bio = Console.ReadLine();

Console.WriteLine($"{name}, {age}, {city}, {bio}");