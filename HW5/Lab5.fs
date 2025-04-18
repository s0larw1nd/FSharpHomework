module Lab5

type Maybe<'a> =
| Just of 'a
| Nothing

let mapMaybe f maybe =
    match maybe with
    | Just a -> Just (f a)
    | Nothing -> Nothing

let applyMaybe maybe_f maybe_v =
    match maybe_f, maybe_v with
    | Just f, Just a -> Just (f a)
    | _ -> Nothing

let applyMaybeReverse maybe_f maybe_v =
    match maybe_f, maybe_v with
    | Just a, Just f -> Just (f a)
    | _ -> Nothing

let bindMaybe f maybe =
    match maybe with
    | Just a -> f a
    | Nothing -> Nothing

let mainLab5() =
    let id x = x
    printfn "Result (Law 1): %A = %A" (mapMaybe id (Just 5)) (id 5)

    let f x = x + 1
    let g x = x * 2
    let v1 = mapMaybe (f >> g) (Just 5)
    let v2 = mapMaybe g (mapMaybe f (Just 5))
    printfn "Result (Law 2): %A = %A" v1 v2

    let af = applyMaybe (Just (fun x -> x+1)) (Just 2)
    printfn "Applicative functor: %A" af
    
    printfn "Result (Law 1 (Applicative)): %A = %A" (applyMaybe (Just id) (Just 5)) (id 5)
    printfn "Result (Law 2 (Applicative)): %A = %A" (applyMaybe (Just (fun x -> x+1)) (Just 5)) (Just ((fun x -> x+1) 5))
    printfn "Result (Law 3 (Applicative)): %A = %A" (applyMaybe (Just (fun x -> x+1)) (Just 5)) (applyMaybeReverse (Just 5) (Just (fun x -> x+1)))
    0