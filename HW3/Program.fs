open System

// Task 2
let solve_quadratic (a:float) (b:float) (c:float) =
    let D : float = b*b - 4.0 * a * c
    match D with
    | _ when D < 0 -> failwith "Incorrect input"
    | _ -> (-b+sqrt(D)) / (2.0 * a), (-b-sqrt(D)) / (2.0 * a)
    
// Task 3
let circleVolume radius =
    let pi = 3.14159
    pi * radius * radius
    
let cylinderVolume_superposition radius length =
    let cyl_vol cir_vol = cir_vol * length
    let vol = circleVolume >> cyl_vol
    vol radius

let cylinderVolume_currying radius length =
    let cir_vol = circleVolume radius
    length * cir_vol
    
// Task 4,5
let rec digSumTop digit =
    if digit < 10 then digit
    else digit % 10 + digSumTop (digit / 10)
        
let rec digSumTail digit acc=
    if digit < 10 then acc + digit
    else
        digSumTail (digit / 10) (acc + digit % 10)
        
let digSumTail_Wrapper digit =
    digSumTail digit 0
    
// Task 6
let rec factorial num acc =
    match num with
    | _ when num=0 -> acc
    | _ -> factorial (num-1) (acc*num)

let factorial_wrapper num =
    factorial num 1
    
let choose_func (typ:bool) num =
    match typ with
    | true -> digSumTail_Wrapper num
    | false -> factorial_wrapper num
    
// Task 7
let rec func_num_traversal (num:int) (func:int->int->int) (init:int) :int =
    match num with
    | _ when num<10 -> func num init
    | _ -> func_num_traversal (num/10) func (func (num%10) init)
    
// Task 8
let rec func_num_traversal_cond (num:int) (func:int->int->int) (init:int) (cond:int->bool) :int =
    match num with
    | _ when num=0 -> init
    | _ when cond (num%10) -> func_num_traversal_cond (num/10) func (func (num%10) init) cond
    | _ -> func_num_traversal_cond (num/10) func init cond
    
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
    // Task 1
    Console.WriteLine("Задание 1")
    Console.WriteLine("Hello World")
    
    // Task 2
    Console.WriteLine("Задание 2")
    let r = solve_quadratic 3 -14 -5
    Console.WriteLine(r)
    
    // Task 3
    Console.WriteLine("Задание 3")
    printfn "Введите радиус: "
    let radius_line = Console.ReadLine()
    let radius = float radius_line
    
    printfn "Введите высоту: "
    let length_line = Console.ReadLine()
    let length = float length_line
    
    let answ_super = cylinderVolume_superposition radius length
    Console.WriteLine(answ_super)
    
    let answ_curr = cylinderVolume_currying radius length
    Console.WriteLine(answ_curr)
    
    // Task 4,5
    Console.WriteLine("Задания 4,5")
    let dst = digSumTop 123456
    let dstl = digSumTail_Wrapper 123456
    Console.WriteLine(dst)
    Console.WriteLine(dstl)
    
    // Task 6
    Console.WriteLine("Задание 6")
    Console.WriteLine("Сумма цифр: ")
    Console.WriteLine(choose_func true 12)
    Console.WriteLine("Факториал: ")
    Console.WriteLine(choose_func false 12)
    
    // Task 7,8
    Console.WriteLine("Задания 7,8")
    Console.WriteLine(func_num_traversal 1234 (fun a b -> a + b) 0)
    Console.WriteLine(func_num_traversal 1234 (fun a b -> a * b) 1)
    Console.WriteLine(func_num_traversal 12343 (fun a b -> max a b) 0)
    Console.WriteLine(func_num_traversal 12343 (fun a b -> min a b) 10)
    
    // Task 9,10
    Console.WriteLine("Задания 9,10")
    Console.WriteLine(func_num_traversal_cond 1234 (fun a b -> a + b) 0 (fun a -> a > 2))
    Console.WriteLine(func_num_traversal_cond 12343 (fun a b -> max a b) 0 (fun a -> a % 2 = 1))
    Console.WriteLine(func_num_traversal_cond 12343 (fun a b -> min a b) 10 (fun a -> a % 2 = 0))
    
    let fkecw = find_k_even_coprime_wrapper 104
    let fmdnd3w = find_max_digit_not_div_3_wrapper 123654
    let t5 = task_5 130
    
    Console.WriteLine(fkecw)
    Console.WriteLine(fmdnd3w)
    Console.WriteLine(t5)
    0