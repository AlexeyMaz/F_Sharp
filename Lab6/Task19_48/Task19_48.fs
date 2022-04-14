//
open System

let rec readList n = 
    if n = 0 then []
    else
        let Head = Console.ReadLine() |> Convert.ToInt32
        let Tail = readList (n-1)
        Head::Tail

let rec writeList = function
    | [] -> ()
    | head::tail -> 
        Console.WriteLine($"{head}")
        writeList tail

// Сколько раз элемент встречается в списке
let GetElemFrequency list elem =
    let rec GetElemFreq list elem count =
        match list with
        | [] -> count
        | head::t -> 
            let new_count = if head = elem then count + 1 else count
            GetElemFreq t elem new_count
    GetElemFreq list elem 0

// Найти самый частый элемент в списке
let GetMostFreqElem list =
    let rec GetMostFreqElem1 list cur_max_elem cur_max_freq =
        match list with
        | [] -> cur_max_elem
        | head::t -> 
            let head_freq = GetElemFrequency list head
            let new_max = if head_freq > cur_max_freq then head_freq else cur_max_freq
            let new_elem = if head_freq > cur_max_freq then head else cur_max_elem
            GetMostFreqElem1 t new_elem new_max
    GetMostFreqElem1 list 0 0

// Найти индексы, где стоит элемент
let GetIndexList list elem =
    let rec GetIndexList1 list elem new_list index =
        match list with
        | [] -> new_list
        | head::t ->
            let new_new_list = if head = elem then new_list @ [index] else new_list
            let new_index = index + 1
            GetIndexList1 t elem new_new_list new_index
    GetIndexList1 list elem [] 0

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите кол-во элементов и список:")
   let list = readList (Console.ReadLine() |> Convert.ToInt32)
   
   let most_freq_elem = GetMostFreqElem list
   let elem_indexes = GetIndexList list most_freq_elem
   Console.WriteLine($"Самый частый элемент: {most_freq_elem}")
   Console.WriteLine("Его индексы:")
   writeList elem_indexes
   0