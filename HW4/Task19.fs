open System
let is_palindrome (str: string) =
    let arr_str = str |> Seq.toArray
    let rev_arr_str = Array.rev arr_str
    arr_str = rev_arr_str
    
[<EntryPoint>]
let main(args : string[]) =
    printfn "%A" (is_palindrome "TEST")
    printfn "%A" (is_palindrome "TESET")
    0