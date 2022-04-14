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

let GetMore3List list pred = 
   let rec GetList list pred new_list = 
      match list with 
      | [] -> new_list
      | head::t ->
         let new_new_list = if pred head then new_list @ [head] else new_list
         GetList t pred new_new_list
   GetList list pred []

let remove_elements list elem = 
   GetMore3List list (fun x -> (x <> elem))

// Убрать повторы из списка
let GetUniqueList list = 
   let rec GetUniqueList1 list new_list = 
       match list with
           | [] -> new_list
           | head::t -> 
               let t_without_head = remove_elements t head
               let new_new_list = new_list @ [head]
               GetUniqueList1 t_without_head new_new_list
   GetUniqueList1 list [] 

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите кол-во элементов и список:")
   let list = readList (Console.ReadLine() |> Convert.ToInt32)
   
   let more3list = GetMore3List list (fun x -> GetElemFrequency list x > 3)
   let result = GetUniqueList more3list
   Console.WriteLine("Элементы, встречающиеся более 3 раз:")
   writeList result
   0