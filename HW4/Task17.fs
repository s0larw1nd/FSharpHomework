open System
let rec read_int_list n =
    match n with
    | _ when n=0 -> []
    | _ -> Convert.ToInt32(Console.ReadLine()) :: read_int_list (n-1)
    
// Task 1
let rec longest_common_seq list1 list2 =
    match list1, list2 with
    | [], list2 -> []
    | list1, [] -> []
    | head_x::tail_x, head_y::tail_y when head_x = head_y -> head_x :: longest_common_seq tail_x tail_y
    | head_x::tail_x, head_y::tail_y ->
            let lcs1 = longest_common_seq tail_x list2
            let lcs2 = longest_common_seq list1 tail_y
            if List.length lcs1 > List.length lcs2 then lcs1 else lcs2

// Task 2
let create_5_lst lst =
    let lst1 = List.map (fun x -> x / 2) (List.filter (fun x -> x%2 = 0) lst)
    let lst2 = List.map (fun x -> x / 3) (List.filter (fun x -> x%3 = 0) lst1)
    let lst3 = List.map (fun x -> x * x) lst2
    let lst4 = List.filter (fun x -> List.contains x lst1) lst3
    
    (lst1, lst2, lst3, lst4, lst2@lst3@lst4)
    
// Task 3
let rec GCD a b =
    match b with
    | _ when b=0 -> a
    | _ -> GCD b (a%b)
    
let n_tuple n =
    [
      for x in 1 .. n do
        if n % x = 0 then
            let y = n / x
            let d = GCD x y
            yield (x / d, y / d)
    ]
    |> List.distinct

// Task 4
let find_pythagorean_triples lst =
    lst |> List.collect (fun x ->
    lst |> List.collect (fun y ->
    lst |> List.map (fun z -> (x, y, z))))
    |> List.filter (fun (a,b,c) -> a<b && b<c && a*a+b*b=c*c)
    
// Task 5
let rec is_prime num del=
    match del with
    | _ when del>=num -> true
    | _ when num%del <> 0 -> is_prime num (del+1)
    | _ -> false
    
let rec prime_del num del:int list =
    match del with
    | _ when del=num -> []
    | _ when num%del=0 && is_prime del 2 -> del::prime_del num (del+1)
    | _ -> prime_del num (del+1)

let list_all_prime_del (lst:int list)=
   List.filter (fun x -> List.forall (fun y -> List.contains y lst) (prime_del x 2)) lst

// Task 6
let rec read_loop acc n =
    match n with
    | _ when n=0 -> acc
    | _ -> 
        let line = System.Console.ReadLine()
        let parts = line.Split([|' '|])
        let nums = parts |> Array.map int
        let tuple = (nums.[0], nums.[1], nums.[2], nums.[3], nums.[4])
        read_loop (tuple :: acc) (n-1)
        
let read_tuples n=
    read_loop [] n
    
let sort_convert_to_num (list: (int * int * int * int * int) list) =
    list
    |> List.filter (fun (a, b, c, d, e) -> 0 < a && a < 10 && 0 < b && b < 10 && 0 < c && c < 10 && 0 < d && d < 10 && 0 < e && e < 10)
    |> List.sort
    |> List.map (fun (a, b, c, d, e) -> int (string a + string b + string c + string d + string e))

[<EntryPoint>]
let main(args : string[]) =
    System.Console.WriteLine "Выберите задачу (1-6):"
    match Convert.ToInt32(Console.ReadLine()) with
    | 1 ->
        System.Console.WriteLine "Введите число элементов:"
        let n = Convert.ToInt32(Console.ReadLine())
        let lst1 = read_int_list n
        let lst2 = read_int_list n
        let res = longest_common_seq lst1 lst2
        printfn "Наибольшая общая подпоследовательность: %A" res
    | 2 ->
        System.Console.WriteLine "Введите число элементов:"
        let n = Convert.ToInt32(Console.ReadLine())
        let lst = read_int_list n
        let res = create_5_lst lst
        printfn "5 списков: %A" res
    | 3 ->
        let n = Convert.ToInt32(Console.ReadLine())
        let pairs = n_tuple n
        printfn "Пары: %A" pairs
    | 4 ->
        System.Console.WriteLine "Введите число элементов:"
        let n = Convert.ToInt32(Console.ReadLine())
        let lst = read_int_list n
        let trp = find_pythagorean_triples lst
        printfn "Пифагоровы тройки: %A" trp
    | 5 ->
        System.Console.WriteLine "Введите число элементов:"
        let n = Convert.ToInt32(Console.ReadLine())
        let lst = read_int_list n
        let result = list_all_prime_del lst
        printfn "Элементы со всеми простыми делителями: %A" result
    | 6 ->
        System.Console.WriteLine "Введите число элементов:"
        let n = Convert.ToInt32(Console.ReadLine())
        let tupl = read_tuples n
        let res = sort_convert_to_num tupl
        printfn "Сортировка списка картежей: %A" res
    0