nodes(N, Nodes) :- findall(I, between(1, N, I), Nodes).

all_pairs(N, Pairs) :-
    nodes(N, Ns),
    findall(U-V, (member(U, Ns), member(V, Ns), U<V), Pairs).

comb(0, _, []).
comb(K, [X|Xs], [X|Ys]) :-
    K > 0,
    K1 is K - 1,
    comb(K1, Xs, Ys).
comb(K, [_|Xs], Ys) :-
    K > 0,
    comb(K, Xs, Ys).

max_edges(N, M) :- M is N*(N-1)/2,!.

generate_graph(N, K, Edges) :-
    all_pairs(N, Pairs),
    comb(K, Pairs, Edges).

generate_graph(N, G) :-
    max_edges(N, M),
    between(0, M, K),
    generate_graph(N, K, G).

combination(0, _, []).
combination(N, [X|T], [X|Comb]) :-
    N > 0,
    N1 is N - 1,
    combination(N1, T, Comb).
combination(N, [_|T], Comb) :-
    N > 0,
    combination(N, T, Comb).

check_cond(Edges) :-
    findall(Komb, combination(4, [1,2,3,4], Komb), Kombs),
    forall(member(Komb,Kombs),proverka(Edges,Komb)).

dva_max_ravny(List) :-
    max_list(List, Max),
    Max < 99,
    count_occurrences(Max, List, Count),
    Count >= 2.

count_occurrences(_, [], 0).
count_occurrences(Elem, [Elem|T], Count) :-
    count_occurrences(Elem, T, Count1),
    Count is Count1 + 1.
count_occurrences(Elem, [_|T], Count) :-
    count_occurrences(Elem, T, Count).

min_path(A,B,Edges,Result) :-
    findall(X-99, between(1, 4, X), List),
    merge(List,[A-0],NewList),
    sort(2, @=<, NewList, NewListSorted),
    dijkstra(B,Edges,NewListSorted,Result),!.

merge([],_,[]).
merge([H-V|T],Old,[H-MV|Rest]) :-
    member(H-OV,Old),
    MV is min(V,OV),
    merge(T,Old,Rest).
merge([H-V|T],Old,[H-V|Rest]) :-
    \+ member(H-_,Old),
    merge(T,Old,Rest).
          
dijkstra(B,_,[B-Value|_],Value) :- !.
dijkstra(B,Edges,[Node-Value|Rest],Result) :-
    findall(Adj, (member(Node-Adj,Edges);member(Adj-Node,Edges)), Adjacent),
    findall(AdjNode-AdjValM, (member(AdjNode,Adjacent), member(AdjNode-AdjVal,[Node-Value|Rest]), AdjValM is min(AdjVal,Value + 1)), AdjValues),
    merge(Rest,AdjValues,NewRest),
    sort(2, @=<, NewRest, NewRestSorted),
    dijkstra(B,Edges,NewRestSorted,Result).

proverka(Edges, [U,V,X,Y]) :-
    min_path(U,V,Edges,LenUV),
    min_path(X,Y,Edges,LenXY),

    min_path(U,X,Edges,LenUX),
    min_path(V,Y,Edges,LenVY),

    min_path(U,Y,Edges,LenUY),
    min_path(V,X,Edges,LenVX),
    
    Rast1 is LenUV+LenXY,
    Rast2 is LenUX+LenVY,
    Rast3 is LenUY+LenVX,
    
    dva_max_ravny([Rast1, Rast2, Rast3]),!.

task(N) :-
    generate_graph(N,G),
    check_cond(G),
    write(G),nl,
    fail.