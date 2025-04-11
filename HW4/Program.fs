open System

// Task 1
let rec read_strings_list n =
    match n with
    | _ when n=0 -> []
    | _ -> Console.ReadLine() :: read_strings_list (n-1)

let sort_strings lst =
    List.sortBy (fun s -> String.length(s)) lst

[<EntryPoint>]
let main(args : string[]) =
    //Task 1
    let str_lst = read_strings_list 5
    Console.Out.WriteLine(sort_strings str_lst)
    
    0