let sieve limit =
    let isPrime = Array.create (limit + 1) true
    isPrime.[0] <- false
    isPrime.[1] <- false
    for i in 2 .. (int (sqrt (float limit))) do
        if isPrime.[i] then
            for j in i * i .. i .. limit do
                isPrime.[j] <- false
    isPrime
    |> Array.mapi (fun i prime -> if prime then Some i else None)
    |> Array.choose id

let rec extended_gcd (a:int64) (b:int64) =
    match b with
    | _ when b = 0 -> (int64 a, int64 1, int64 0)
    | _ -> 
        let g, x1, y1 = extended_gcd b (a % b)
        (int64 g, int64 y1, int64 (x1 - (a / b) * y1))

let modinv (a:int64) (m:int64) =
    let g, x, _ = extended_gcd a m
    (x % m + m) % m

let rec task (S:int64) i (primes: int array) =
    match i with
    | _ when i >= primes.Length - 1 -> S
    | _ when primes.[i] > 1000000 -> S
    | _ ->
        let p1 = int64 primes.[i]
        let p2 = int64 primes.[i + 1]
        let digits = string p1 |> String.length
        let baseVal = pown 10L digits |> int64
        let baseInv = modinv baseVal p2 |> int64
        let s = (-p1 * baseInv) |> int64 |> fun x -> if x > 0 then x%p2 else (x % p2 + p2) % p2
        task (S + (p1 + baseVal * s)) (i+1) primes

[<EntryPoint>]
let main(args : string[]) =
    let primes = sieve 1001000
    let S = task 0 2 primes

    printfn "%d" S

    0