open System

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
    
let rec find_max_digit_not_div_3 num mx =
    match num with
    | _ when num=0 -> mx
    | _ when (num%10) > mx && (num%10)%3<>0 -> find_max_digit_not_div_3 (num/10) (num%10)
    | _ -> find_max_digit_not_div_3 (num/10) mx
    
let find_max_digit_not_div_3_wrapper num =
    find_max_digit_not_div_3 num 0

[<EntryPoint>]
let main(args : string[]) =
    let fkecw = find_k_even_coprime_wrapper 104
    let fmdnd3w = find_max_digit_not_div_3_wrapper 123654
    
    Console.WriteLine(fkecw)
    Console.WriteLine(fmdnd3w)
    0