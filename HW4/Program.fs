open System

// Task 10
let rec read_strings_list n =
    match n with
    | _ when n=0 -> []
    | _ -> Console.ReadLine() :: read_strings_list (n-1)

let sort_strings lst =
    List.sortBy (fun s -> String.length(s)) lst
    
// Tasks 11-16
let rec read_int_list n =
    match n with
    | _ when n=0 -> []
    | _ -> Convert.ToInt32(Console.ReadLine()) :: read_int_list (n-1)
// 1.4
let rec map_idx (lst:int list) i =
    match lst with 
    | [] -> []
    | lst::rest -> (lst, i) :: map_idx rest (i + 1)
    
let rec insert_desc x lst =
    match lst with
    | [] -> [x]
    | (v, idx) :: rest when fst x >= v -> x :: lst
    | (v, idx) :: rest -> (v, idx) :: insert_desc x rest

let rec sort_desc lst =
    match lst with
    | [] -> []
    | head :: tail -> insert_desc head (sort_desc tail)
    
let rec get_idx lst =
    match lst with
    | [] -> []
    | (v, idx) :: rest -> idx :: get_idx rest
 
let idx_desc (lst:int list) =
    get_idx(sort_desc(map_idx lst 0))

let idx_desc_List lst =
    lst
    |> List.mapi (fun i x -> (x, i))
    |> List.sortByDescending fst
    |> List.map snd
    
[<EntryPoint>]
let main(args : string[]) =
    //Task 10
    let str_lst = read_strings_list 5
    Console.Out.WriteLine(sort_strings str_lst)
    
    //Task 11-16
    let int_lst = read_int_list 5
    Console.Out.WriteLine(idx_desc_List int_lst)
    Console.Out.WriteLine(idx_desc int_lst)
    
    0