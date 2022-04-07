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
let rec Kolvo_less3 n kol =
   let b = n / 10
   let new_kol = kol + 1
   match n with
   |0 -> kol
   |n when n % 10 < 3 -> Kolvo_less3 b new_kol
   |n -> Kolvo_less3 b kol
      


[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите число:")
   let x = Convert.ToInt32(Console.ReadLine())//30
   Console.WriteLine("Сумма непростых делителей числа: {0}", Sum_notSimDiv x (fun x y -> x + y) 0)//1 6 10 15 30
   Console.WriteLine("Кол-во цифр числа, меньших 3: {0}", Kolvo_less3 x 0)
   0