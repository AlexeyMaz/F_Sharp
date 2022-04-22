open System

let rec readList n = 
    if n = 0 then []
    else
        let Head = Console.ReadLine() |> Convert.ToInt32
        let Tail = readList (n-1)
        Head::Tail

let Is_Simple x =
   let rec Is_Simple1 x current =
       match current with
       |0 -> false
       |1 -> true
       |current ->
           if x % current = 0 then false
           else
               let new_current = current - 1
               Is_Simple1 x new_current
   let new_x = x - 1
   Is_Simple1 x new_x

let GetSum_and_Count pred list =
   List.fold (fun a x -> if pred (fst x) then (fst a + fst x, snd a + 1) else a) (0, 0) (List.zip list list)

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)

    // Сумма и кол-во простых чисел
    let simpleSum_and_Count = GetSum_and_Count (fun x -> Is_Simple x) list
    // Среднее значение простых элементов списка
    let simpleAvg = (double) (fst simpleSum_and_Count) / (double) (snd simpleSum_and_Count)

    // Сумма и кол-во непростых элементов, больших simpleAvg
    let result = GetSum_and_Count (fun x -> not (Is_Simple x) && (double)x > simpleAvg) list
    // Среднее непростых элементов, больших simpleAvg
    let result1 = (double) (fst result) / (double) (snd result) 
    
    Console.WriteLine($"Среднее простых: {simpleAvg}")
    Console.WriteLine($"Cреднее непростых элементов, больших, чем среднее простых: {result1}")
    0