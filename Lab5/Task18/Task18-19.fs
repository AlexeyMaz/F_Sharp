open System

let Is_Simple x =
    let rec Is_Simple1 x current =
        match current with
        |0->false
        |1->true
        |current->
            if x % current = 0 then false
            else
                let new_current = current - 1
                Is_Simple1 x new_current
    let new_x = x - 1
    Is_Simple1 x new_x

//Сумма непростых делителей числа
let Sum_notSimDiv n func cur_sum =
   let rec Bypass1 n func cur_sum cur_divider =
       if cur_divider = 0 then cur_sum else
       let newInit = if n % cur_divider = 0 && not (Is_Simple cur_divider) then func cur_sum cur_divider else cur_sum
       let new_divider = cur_divider-1
       Bypass1 n func newInit new_divider 
   Bypass1 n func cur_sum n

//Кол-во цифр числа, меньших 3
let rec Kolvo_less3 n cur_kol =
   let new_n = n / 10
   let new_kol = cur_kol + 1
   match n with
   |0 -> cur_kol
   |n when n % 10 < 3 -> Kolvo_less3 new_n new_kol
   |n -> Kolvo_less3 new_n cur_kol
      
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

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите число:")
   let x = Convert.ToInt32(Console.ReadLine())//30
   Console.WriteLine("Сумма непростых делителей числа: {0}", Sum_notSimDiv x (fun x y -> x + y) 0)//1 6 10 15 30
   Console.WriteLine("Кол-во цифр числа, меньших 3: {0}", Kolvo_less3 x 0)
   Console.WriteLine("Количество чисел, не являющихся делителями исходного
числа, не взамно простых с ним и взаимно простых с суммой простых
цифр этого числа: {0}", Method_3 x)
   0