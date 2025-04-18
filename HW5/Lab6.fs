module Lab6
open FParsec

type AlgebraicType =
    | IntegerNumber of num: int
    | FloatNumber of num: float

let run_wrapper p str =
    match run p str with
    | Success(result, _, _) ->
        match box result with
        | :? int as i -> IntegerNumber i
        | :? float as f -> FloatNumber f
        | _ -> failwithf "Unexpected type: expected int or float"
    | Failure(errorMsg, _, _) -> failwithf "Failure: %s" errorMsg

let mainLab6() =
    printfn "%A" (run_wrapper pint32 "333")
    printfn "%A" (run_wrapper pfloat "333.333")
    0