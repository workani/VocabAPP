Console.Write("Enter a number: ");
int num = Convert.ToInt32(Console.ReadLine());

if ((num % 5) == 0 && (num % 3) == 0)
    Console.WriteLine("fizzbuzz");
else if((num % 3) == 0)
    Console.WriteLine("fizz");
else if((num % 5) == 0)
    Console.WriteLine("buzz");
else
    Console.Write(num);