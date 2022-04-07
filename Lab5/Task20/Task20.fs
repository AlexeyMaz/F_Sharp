open System

let Is_Simple x =
    let rec Is_Simple1 x current =
        match current with
        |0->false
        |1->true
        |current ->
            if x % current = 0 then false
            else
                let new_current = current - 1
                Is_Simple1 x new_current
    let new_x = x - 1
    Is_Simple1 x new_x

//Сумма непростых делителей числа
let Sum_notSimDiv n =
   let rec Bypass1 n cur_sum cur_divider =
       if cur_divider = 0 then cur_sum else
       let newInit = if n % cur_divider = 0 && not (Is_Simple cur_divider) then cur_sum + cur_divider else cur_sum
       let new_divider = cur_divider-1
       Bypass1 n newInit new_divider 
   Bypass1 n 0 n

//Кол-во цифр числа, меньших 3
let Kolvo_less3  x = 
   let rec Kolvo  x cur_kol =
      if x = 0 then cur_kol
      else
          let new_kol = if x % 10 < 3 then cur_kol + 1 else cur_kol
          let new_x = x / 10
          Kolvo new_x new_kol
   Kolvo x 0
      
let rec nod a b =
   let new_ab = abs(a-b)
   if a = b then a
   else 
       if a > b then nod new_ab b 
       else nod a new_ab 
       
let Method_3 x =
   let rec Sum_Simple x cur_sum = //Сумма простых цифр числа
      match x with
      |0 -> cur_sum
      |x ->
         let new_sum = if Is_Simple (x % 10) then x % 10 + cur_sum else cur_sum
         let new_x = x / 10
         Sum_Simple new_x new_sum
   
   let sum_sim = Sum_Simple x 0
   
   let rec num_kol x cur_num cur_kol =
       if cur_num <= 0 then cur_kol
       else
           let new_kol = if (x % cur_num <> 0) && (nod cur_num x <> 1) && (nod cur_num sum_sim = 1) then cur_kol + 1 else cur_kol
           let new_num = cur_num - 1
           num_kol x new_num new_kol
   let new_x = x - 1
   num_kol x new_x 0

let select_fun = function
| 1 -> Sum_notSimDiv
| 2 -> Kolvo_less3
| 3 -> Method_3

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите число:")
   let choice = (Console.ReadLine() |> Int32.Parse, Console.ReadLine() |> Int32.Parse)
   Console.WriteLine(select_fun (fst choice) (snd choice))
   0