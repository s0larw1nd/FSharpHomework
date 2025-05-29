task(Edges) :-
    task_helper(Edges, [1], 1, [1-0], [1-0], [], [1-0], []).

print_block(Block) :- distinct_vert(Block, BlockVert), write(BlockVert),nl.

xor(A, B, Diff) :-
    findall(X, (member(X, A), \+ member(X, B)), ANB),
    findall(Y, (member(Y, B), \+ member(Y, A)), BNA),
    append(ANB, BNA, Diff).

replace(N-V, [], [N-V]).
replace(N-V, [N-_|Old], [N-V|Old]) :- !.
replace(N-V, [NN-VV|Old], [NN-VV|Rest]) :-
    replace(N-V, Old, Rest).

next_vert(Edges,Visited,Next) :-
    findall(F, (member(F-_,Edges);member(_-F,Edges)), Fs),
    xor(Fs,Visited,Dif),
    nth0(0,Dif,Next).

distinct_vert(List,Dist) :-
    findall(A, member(A-_,List), LA),
    findall(B, member(_-B,List), LB),
    append(LA,LB,L),
    sort(0, @<, L, Dist).
    
task_helper(Edges, Visited, Timer, Tin, Low, Parents, Stack, EdgeStack) :-
    dfs(Edges, Visited, Timer, Tin, Low, Parents, Stack, EdgeStack,
    NewLow,NewStack,NewParents,NewTin,NewTimer,NewVisited, NewEdgeStack,
    Block),
    ( distinct_vert(Block, BlockVert), length(BlockVert,L), L == 9 -> print_block(Block) ; true),!,
    ( NewStack == [] -> next_vert(Edges,NewVisited,Vert),
        task_helper(Edges, NewVisited, NewTimer, NewTin, NewLow, NewParents, [Vert-0], NewEdgeStack);
    	task_helper(Edges, NewVisited, NewTimer, NewTin, NewLow, NewParents, NewStack, NewEdgeStack)).

merge([],[],[]) :- !.
merge([],[H-V|Old],[H-V|Rest]) :-
    merge([], Old, Rest).
merge([H-V|T],Old,[H-MV|Rest]) :-
    member(H-OV,Old),
    MV is min(V,OV),
    select(H-OV,Old,NewOld),
    merge(T,NewOld,Rest).
merge([H-V|T],Old,[H-V|Rest]) :-
    \+ member(H-_,Old),
    merge(T,Old,Rest).

without_last([_], []).
without_last([X|Xs], [X|WithoutLast]) :- 
    without_last(Xs, WithoutLast).

dfs(Edges, Visited, Timer, Tin, Low, Parents, Stack, EdgeStack,
    NewLow,NewStackDH,NewParents,NewTin,NewTimer,NewVisited, NewEdgeStack,
    []) :-
    last(Stack, V-Idx),
    findall(Adj, (member(V-Adj, Edges); member(Adj-V, Edges)), AdjsUns),
    sort(AdjsUns,Adjs),
    length(Adjs,L),
    Idx < L,
    
    nth0(Idx,Adjs,To),
    NewIdx is Idx+1,
    without_last(Stack,StackWL),
    append(StackWL,[V-NewIdx],NewStack),
    dfs_helper(V,Parents,To,Visited,Low,Tin,NewStack,EdgeStack,Timer,
               NewLow,NewStackDH,NewParents,NewTin,NewTimer,NewVisited,NewEdgeStack).

dfs(Edges, Visited, Timer, Tin, Low, Parents, Stack, EdgeStack,
    Low,WLStack,Parents,Tin,Timer,Visited,EdgeStack,
    []) :-
    last(Stack, V-Idx),
    findall(Adj, (member(V-Adj, Edges)), Adjs), 
    length(Adjs,L),
    Idx >= L,
    
    without_last(Stack, WLStack),
    not(member(V-_,Parents)).

dfs(Edges, Visited, Timer, Tin, Low, Parents, Stack, EdgeStack,
    NewLow,WLStack,Parents,Tin,Timer,Visited,NewEdgeStack,
    Block) :-
    last(Stack, V-Idx),
    findall(Adj, (member(V-Adj, Edges)), Adjs), 
    length(Adjs,L),
    Idx >= L,
    
    without_last(Stack, WLStack),
    member(V-P,Parents),
    member(P-LP,Low),
    member(V-LV,Low),
    NLP is min(LP,LV),
    merge([P-NLP],Low,NewLow),
    dfs_helper4(V,P,Low,Tin,EdgeStack,Block,NewEdgeStack).

dfs_helper(V,Parents,To,Visited,Low,Tin,Stack,EdgeStack,Timer,
           NewLow,NewStack,NewParents,NewTin,NewTimer,NewVisited,NewEdgeStack) :-
    ( \+ member(V-_,Parents) ; member(V-Parent,Parents), not(To==Parent)),
    dfs_helper2(V,To,Visited,Low,Tin,Stack,EdgeStack,Parents,Timer,
                NewLow,NewStack,NewParents,NewTin,NewTimer,NewVisited,NewEdgeStack).

dfs_helper(V,Parents,To,Visited,Low,Tin,Stack,EdgeStack,Timer,
           Low,Stack,Parents,Tin,Timer,Visited,EdgeStack) :-
    member(V-Parent,Parents),
    To==Parent.

dfs_helper2(V,To,Visited,Low,Tin,Stack,EdgeStack,Parents,Timer,
          	NewLow,Stack,Parents,Tin,Timer,Visited,NewEdgeStack) :-
    member(To,Visited),
    member(V-LowV,Low),
    member(To-TinTo,Tin),
    NewLowV is min(LowV,TinTo),
    merge(Low, [V-NewLowV], NewLow),
    dfs_helper3(V, To, Tin, EdgeStack, NewEdgeStack).

dfs_helper2(V,To,Visited,Low,Tin,Stack,EdgeStack,Parents,Timer,
            NewLow,NewStack,NewParents,NewTin,NewTimer,NewVisited,NewEdgeStack) :-
    not(member(To,Visited)),
    replace(To-V, Parents, NewParents),
    merge([To-Timer],Tin, NewTin),
    merge([To-Timer],Low, NewLow),
    NewTimer is Timer+1,
    append([To],Visited,NewVisited),
    append(EdgeStack,[V-To],NewEdgeStack),
    append(Stack,[To-0],NewStack).

dfs_helper3(V,To,Tin,EdgeStack,
            NewEdgeStack) :-
    member(V-TinV,Tin), 
    member(To-TinTo,Tin),
    TinTo < TinV,
    append(EdgeStack, [V-To], NewEdgeStack).

dfs_helper3(V,To,Tin,EdgeStack,
            EdgeStack) :-
    member(V-TinV,Tin),
    member(To-TinTo,Tin),
    TinTo >= TinV.

dfs_helper4(V,P,Low,Tin,EdgeStack,
            Block,NewEdgeStack) :-
    member(V-LV,Low),
    member(P-TP,Tin),
    LV >= TP,
    dfs_helper5(V,P,EdgeStack,[],Block,NewEdgeStack).

dfs_helper4(V,P,Low,Tin,EdgeStack,
            [],EdgeStack) :-
    member(V-LV,Low),
    member(P-TP,Tin),
    LV < TP.

dfs_helper5(V,P,EdgeStack,Cur,[V-P|Cur],NewEdgeStack) :-
    last(EdgeStack, V-P),
    without_last(EdgeStack,NewEdgeStack),!.
dfs_helper5(V,P,EdgeStack,Cur,[P-V|Cur],NewEdgeStack) :-
    last(EdgeStack, P-V),
    without_last(EdgeStack,NewEdgeStack),!.
dfs_helper5(V,P,EdgeStack,Cur,Block,Result) :-
    last(EdgeStack, E1-E2),
    without_last(EdgeStack,NewEdgeStack),
    dfs_helper5(V,P,NewEdgeStack,[E1-E2|Cur],Block,Result).