open System

let readArray n =
    let rec readArray1 n arr = 
        if n = 0 then
            arr
        else
            let tail = Console.ReadLine() |> Int32.Parse
            let new_arr = Array.append arr [|tail|]
            let new_n = n - 1
            readArray1 new_n new_arr
    readArray1 n Array.empty

let write_array arr =
    printfn "%A" arr

[<EntryPoint>]
let main argv =
   printfn "Введите кол-во элементов в массиве 1:"
   let n1 = Console.ReadLine() |>Int32.Parse
   printfn "Введите массив 1:"
   let arr1 = readArray n1
   printfn "Введите кол-во элементов в массиве 2:"
   let n2 = Console.ReadLine() |> Int32.Parse
   printfn "Введите массив 2:"
   let arr2 = readArray n2

   let union = Array.filter (fun x -> not (Array.contains x arr1)) arr2 |> Array.append arr1 |> Array.sort
   printfn "Объединение массивов:"
   write_array union
   0