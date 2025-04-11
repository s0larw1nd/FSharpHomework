open System
//Task 1
let reverse arr=
    Array.rev arr

//Task 2
let copy_last (arr1:int array) (arr2:int array)=
    Array.append arr1 [|arr2.[arr2.Length-1]|]

//Task 3
let concat arr1 arr2 =
    Array.append arr1 arr2
    
//Task 4
let filter_by_div_3 arr1 =
    Array.filter (fun x -> x%3=0) arr1
    
//Task 5
let rec read_arr n res=
    match n with
    | _ when n<=0 -> res
    | _ -> read_arr (n-1) (Array.append res [|Console.ReadLine() |> int|])

let check_interval arr=
    Array.exists (fun x -> x>10 || x<0) arr
            
let to_num arr=
    arr
    |> Array.map string
    |> String.concat ""
    |> int
    
let arr_diff arr1 arr2 =
    if check_interval arr1 || check_interval arr2 then failwith "Элементы должны быть в интервале [0;10)"
    to_num arr1 - to_num arr2
    
//Task 6
let check_not_desc arr=
    arr
    |> Array.pairwise
    |> Array.forall (fun (a, b) -> a <= b)
   
let find_union arr1 arr2=
    if not(check_not_desc arr1) || arr1.Length=0 || not(check_not_desc arr2) || arr2.Length=0 then failwith "Элементы должны быть >0 и неубывающими"
    Array.distinct(Array.append arr1 arr2)
    
//Task 7
let find_intersection arr1 arr2=
    if not(check_not_desc arr1) || arr1.Length=0 || not(check_not_desc arr2) || arr2.Length=0 then failwith "Элементы должны быть >0 и неубывающими"
    arr1 |> Array.filter (fun x -> Array.contains x arr2)
    
//Task 8
let find_XOR arr1 arr2=
    if not(check_not_desc arr1) || arr1.Length=0 || not(check_not_desc arr2) || arr2.Length=0 then failwith "Элементы должны быть >0 и неубывающими"
    arr1 |> Array.filter (fun x -> not(Array.contains x arr2)) |> Array.append (Array.filter (fun x -> not(Array.contains x arr1)) arr2)

//Task 9
let first_100_with_div=
    [| 0 .. 100 |]
    |> Array.filter (fun x -> x % 13 = 0 || x % 17 = 0)
    
//Task 10
let rec find_del num del arr=
    match del with
    | _ when del>num -> arr
    | _ when num%del=0 -> find_del num (del+1) (Array.append arr [|del|])
    | _ -> find_del num (del+1) arr
    
let find_intersection_no_restr arr1 arr2=
    arr1 |> Array.filter (fun x -> Array.contains x arr2)
    
let is_coprime num1 num2=
    match find_intersection_no_restr (find_del num1 2 [||]) (find_del num2 2 [||]) with
    | [||] -> true
    | _ -> false
    
let find_rational_roots (coef:int array)=
    [for p in find_del (coef.[coef.Length-1]) 1 [||] do
        for q in find_del (coef.[0]) 1 [||] do
            if is_coprime p q then
                yield p/q]
    
[<EntryPoint>]
let main(args : string[]) =
    
    0