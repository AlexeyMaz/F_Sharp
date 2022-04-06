open System

//Произведение цифр числа с помощью рекурсии вверх
let rec proz_up x =
    match x with
    |0->1
    |x->x%10 * proz_up(x/10)
 
//Произведение цифр числа с помощью хвостовой рекурсии
let rec proz_tail x a =
     match x with 
     | x when x=0 -> a
     | x->
        let b = x/10
        let c = a*(x%10)
        proz_tail b c
 
//Поиск максимальной цифры числа с помощью рекурсии вверх
let rec max_up x =
    match x with
    |x when x<10-> x
    |x->max (x % 10) (max_up (x/10))

//Поиск максимальной цифры с помощью хвостовой рекурсии
let rec max_tail x a =
    let b = x/10
    match x with 
    |x when x=0 -> a
    |x when (x%10) > a ->
        let c = x%10
        max_tail b c
    |x -> max_tail b a
 
//Поиск минимальной цифры числа с помощью рекурсии вверх
let rec min_up x =
    match x with
    |x when x<10-> x
    |x->min (x % 10) (min_up (x/10))
 
//Поиск минимальной цифры с помощью хвостовой рекурсии
let rec min_tail x a =
    let b = x/10
    match x with 
    |x when x=0 -> a
    |x when (x%10) < a ->
        let c = x%10
        min_tail b c
    |x -> min_tail b a
 
[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите число:")
    let x = Convert.ToInt32(Console.ReadLine())
    Console.WriteLine("Произведение рекурсией вверх:{0}", proz_up x)
    Console.WriteLine("Произведение хвостовой рекурсией:{0}", proz_tail x 1)
    Console.WriteLine("Максимум рекурсией вверх:{0}", max_up x)
    Console.WriteLine("Максимум хвостовой рекурсией:{0}", max_tail x 0)
    Console.WriteLine("Минимум рекурсией вверх:{0}", min_up x)
    Console.WriteLine("Минимум хвостовой рекурсией:{0}", min_tail x 9)
    0