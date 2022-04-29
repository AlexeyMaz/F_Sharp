open System

let rec writeList = function
    | [] -> ()
    | head::tail -> 
        Console.WriteLine($"{head}")
        writeList tail

let remove_at index list =
   let rec rem_at1 index list cur_index new_list =
       match list with
       | [] -> new_list
       | head::tail ->
           let new_new_list = new_list @ if cur_index <> index then [head] else []
           let new_index = cur_index + 1
           rem_at1 index tail new_index new_new_list
   rem_at1 index list 0 []
      
let MedValList list =
   list |> List.sort |> List.item ((List.length list) / 2)

//Отсортировать строки в порядке увеличения медианного значения выборки строк
//(прошлое медианное значение удаляется из выборки и производится поиск нового медианного значения)
let Subtask6 list =
    let rec Subtask6_1 list sorted =
        match list with
        | [] -> sorted
        | head::tail ->
            let med = MedValList list
            let new_sorted = sorted @ [med]
            let new_list = remove_at ((List.length list) / 2) list
            Subtask6_1 new_list new_sorted
    Subtask6_1 list []

[<EntryPoint>]
let main argv =

   0