Random dice = new Random();

int roll1 = dice.Next(1, 7);
int roll2 = dice.Next(1, 7);
int roll3 = dice.Next(1, 7);

int total = roll1 + roll2 + roll3;

Console.WriteLine($"Dice roll: {roll1} + {roll2} + {roll3} = {total}");

// check if player gets any bonuses
if (roll1 == roll2 || roll1 == roll3 || roll2 == roll3)
{
    if (roll1 == roll2 && roll2 == roll3)
    {
        Console.WriteLine("You rolled triples! +6 bonus to total!");
        total += 6;
    }
    else
    {
        Console.WriteLine("You rolled doubles! +2 bonus to total!");
        total += 2;   
    }
}


// check if player won or lose
if (total >= 16)
{
    Console.WriteLine("You won a new car!");
}
else if(total >= 10)
{
    Console.WriteLine("You won a new laptop!");    
}
else if (total >= 7)
{
    Console.WriteLine("You won a trip!");
}
else
{
    Console.WriteLine("You won a kitten!");
}
    

