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
//1.14
let rec count_interval lst a b acc=
    match lst with
    | [] -> acc
    | head::tail when a<=head && head<=b -> count_interval tail a b (acc+1)
    | head::tail -> count_interval tail a b acc

let count_interval_wrapper lst a b=
    count_interval lst a b 0
    
let count_interval_List lst a b=
    lst
    |> List.filter(fun i -> a<=i && i<=b)
    |> List.length
//1.24
let rec get_val lst =
    match lst with
    | [] -> []
    | (v, idx) :: rest -> v :: get_val rest
    
let rec get_first_two lst (first:bool)=
    match lst with
    | [] -> []
    | head::tail when first -> head::get_first_two tail false
    | head::tail -> [head]

let find_two_max lst=
    let sorted_lst = get_val(sort_desc(map_idx lst 0))
    get_first_two sorted_lst true
    
let find_two_max_List lst=
    lst
    |>List.sortDescending
    |>List.take 2
//1.34
let rec find_interval lst a b=
    match lst with
    | [] -> []
    | head::tail when a<=head && head<=b -> head::find_interval tail a b
    | head::tail -> find_interval tail a b

let find_interval_List lst a b=
    lst
    |> List.filter(fun i -> a<=i && i<=b)
//1.44
let rec read_double_list n =
    match n with
    | _ when n=0 -> []
    | _ -> Convert.ToDouble(Console.ReadLine()) :: read_double_list (n-1)

let is_int (num:double)=
    match num with
    | _ when num - floor num = 0 -> true
    | _ -> false
    
let rec check_alternation lst integer=
    match lst with
    | [] -> true
    | head::tail when is_int head && not integer -> check_alternation tail true
    | head::tail when not(is_int head) && integer -> check_alternation tail false
    | _ -> false
    
let check_alternation_wrapper lst=
    check_alternation lst (not(is_int lst.Head))

let check_alternation_List lst=
    lst
    |> List.map is_int
    |> List.pairwise
    |> List.forall (fun (a, b) -> a <> b)
    
[<EntryPoint>]
let main(args : string[]) =
    //Task 10
    let str_lst = read_strings_list 5
    Console.Out.WriteLine(sort_strings str_lst)
    
    //Task 11-16
    let int_lst = read_int_list 5
    //1.4
    Console.Out.WriteLine(idx_desc_List int_lst)
    Console.Out.WriteLine(idx_desc int_lst)
    //1.14
    let a = Convert.ToInt32(Console.ReadLine())
    let b = Convert.ToInt32(Console.ReadLine())
    Console.Out.WriteLine(count_interval_wrapper int_lst a b)
    Console.Out.WriteLine(count_interval_List int_lst a b)
    //1.24
    Console.Out.WriteLine(find_two_max int_lst)
    Console.Out.WriteLine(find_two_max_List int_lst)
    //1.34
    Console.Out.WriteLine(find_interval int_lst a b)
    Console.Out.WriteLine(find_interval_List int_lst a b)
    //1.44
    let double_lst = read_double_list 5
    Console.Out.WriteLine(check_alternation_wrapper double_lst)
    Console.Out.WriteLine(check_alternation_List double_lst)
    0