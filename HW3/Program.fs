open System

// Method 1
let rec is_coprime digit1 digit2 del =
    match del with
    | _ when del>digit1 -> true
    | _ when del>digit2 -> true
    | _ when (digit1%del = 0 && digit2%del = 0) -> false
    | _ -> is_coprime digit1 digit2 (del+1)
    
let is_coprime_wrapper digit1 digit2 =
    is_coprime digit1 digit2 2
    
let rec find_k_even_coprime number temp acc =
    match temp with
    | _ when temp=number -> acc
    | _ when temp%2=0 && not (is_coprime_wrapper number temp) -> find_k_even_coprime number (temp+1) (acc+1)
    | _ -> find_k_even_coprime number (temp+1) acc
    
let find_k_even_coprime_wrapper number =
    find_k_even_coprime number 2 0

// Method 2
let rec find_max_digit_not_div_3 num mx =
    match num with
    | _ when num=0 -> mx
    | _ when (num%10) > mx && (num%10)%3<>0 -> find_max_digit_not_div_3 (num/10) (num%10)
    | _ -> find_max_digit_not_div_3 (num/10) mx
    
let find_max_digit_not_div_3_wrapper num =
    find_max_digit_not_div_3 num 0
    
// Method 3
let rec find_min_del num del mn =
    match del with
    | _ when del = 1 -> mn
    | _ when num%del=0 -> find_min_del num (del-1) del
    | _ -> find_min_del num (del-1) mn
    
let find_min_del_wrapper num =
    find_min_del num num num
    
let rec find_sum_dig_less_5 num =
    match num with
    | _ when num < 5 -> num
    | _ when num < 10 -> 0
    | _ when (num%10) < 5 -> (num%10) + find_sum_dig_less_5 (num/10)
    | _ -> find_sum_dig_less_5 (num/10)

let rec find_max_not_coprime_not_div num temp mx min_del =
    match temp with
    | _ when temp = num -> mx
    | _ when not (is_coprime_wrapper num temp) && temp > mx && temp%min_del<>0 -> find_max_not_coprime_not_div num (temp+1) temp min_del
    | _ -> find_max_not_coprime_not_div num (temp+1) mx min_del
    
let find_max_not_coprime_not_div_wrapper num =
    find_max_not_coprime_not_div num 2 0 (find_min_del_wrapper num)
    
let task_5 num =
    (find_max_not_coprime_not_div_wrapper num) * (find_sum_dig_less_5 num)

[<EntryPoint>]
let main(args : string[]) =
    Console.WriteLine("Hello World")
    
    let fkecw = find_k_even_coprime_wrapper 104
    let fmdnd3w = find_max_digit_not_div_3_wrapper 123654
    let t5 = task_5 130
    
    Console.WriteLine(fkecw)
    Console.WriteLine(fmdnd3w)
    Console.WriteLine(t5)
    0