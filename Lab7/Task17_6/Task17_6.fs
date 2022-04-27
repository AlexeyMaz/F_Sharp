open System

let read_tuple5() =
   let nums = Console.ReadLine().Split(",") |> Array.toList |> List.map (fun x -> Int32.Parse x)
   (nums.[0], nums.[1], nums.[2], nums.[3], nums.[4])

let rec read_tuples5List n = 
   if n=0 then []
   else
       let Head = read_tuple5()
       let Tail = read_tuples5List (n-1)
       Head::Tail

let rec writeList = function
    | [] -> ()
    | head::tail -> 
        Console.WriteLine($"{head}")
        writeList tail

let is_digit x =
   x >= 0 && x <= 9

// Получить index-ый элемент кортежа
let unpack5 tuple index =
    match index, tuple with
    | 0, (a,_,_,_,_) -> a
    | 1, (_,b,_,_,_) -> b
    | 2, (_,_,c,_,_) -> c
    | 3, (_,_,_,d,_) -> d
    | 4, (_,_,_,_,e) -> e
    | _, _ -> failwith (sprintf "Несуществующий индекс %i" index)

// Проверить, состоит ли кортеж только из цифр
let IsDigital5 tuple =
    let rec IsDigital5_1 tuple index =
        if index = 5 then
            true
        else if not (is_digit (unpack5 tuple index)) then
            false
        else
            let new_index = index + 1
            IsDigital5_1 tuple new_index
    IsDigital5_1 tuple 0

let TupleToInt tuple =
    let rec TupleToInt1 tuple number index =
        if index = 5 then
            number
        else
            let new_number = number * 10 + unpack5 tuple index
            let new_index = index + 1
            TupleToInt1 tuple new_number new_index
    TupleToInt1 tuple (unpack5 tuple 0) 1

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите кол-во кортежей:")
   let n = Console.ReadLine() |> Int32.Parse
   Console.WriteLine($"Введите {n} кортежей длины 5, разделяя числа запятыми:")
   let tuples = read_tuples5List n

   let dig_tuples = List.filter (fun x -> IsDigital5 x) tuples
   let result = List.map (fun x -> TupleToInt x) (List.sort dig_tuples)
   Console.WriteLine("Результат:")
   writeList result
   0