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



//Совмещает 2 карты, значения дублирующихся ключей складывает
let mergeMaps map1 map2 =
    Map.fold (fun res_map key value ->
        match Map.tryFind key res_map with
        | Some v -> Map.add key (value + v) res_map
        | None -> Map.add key value res_map) map1 map2

//Для строки возвращает карту: (буква -> кол-во)
let makeStrMap (s:string) = 
    let result = Map.empty

    let sanitized = s.Replace(" ", "").ToLower()
    let char_list = Seq.toList sanitized
    let occur_tuples = List.countBy id char_list // Создает список кортежей: (буква, кол-во)

    Map.ofList occur_tuples

//Для алфавита возвращает карту (буква -> кол-во)
let makeAlphabetMap strings_list =
    let rec makeAlphabetMap1 str_list map =
        match str_list with
        | [] -> map
        | head::tail ->
            let str_map = makeStrMap head
            let new_map = mergeMaps map str_map
            makeAlphabetMap1 tail new_map
    makeAlphabetMap1 strings_list Map.empty

//Для алфавита возвращает карту (буква -> частота)
let makeAlphabetFreqMap list = 
    let total_length = List.fold (fun accum (str:string) -> accum + str.Replace(" ", "").Length) 0 list
    let map = makeAlphabetMap list
    Map.map (fun k v -> (double) v / (double) total_length) map

//Вернуть самый частый символ в строке
let mostFreqSymbol str =
    let chars = Seq.toList str
    let str_map = makeAlphabetMap [str]
    List.sortByDescending (fun ch -> str_map.[ch]) chars |> List.item 0
 
//Метод 3
let Subtask3 list =
    let alphabetFreq = makeAlphabetFreqMap list
    List.sortBy (fun str -> Math.Abs((makeAlphabetFreqMap [str]).[mostFreqSymbol str] - alphabetFreq.[mostFreqSymbol str])) list

[<EntryPoint>]
let main argv =

   0