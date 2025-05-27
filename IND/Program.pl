task(Edges) :-
    findall(X, member(1-X, Edges), ToVisit),
    dfs(Edges, [], ToVisit, 1, [1-0], [1-0], [], [1-0], []).

merge([],_,[]).
merge([H-V|T],Old,[H-MV|Rest]) :-
    member(H-OV,Old),
    MV is min(V,OV),
    merge(T,Old,Rest).
merge([H-V|T],Old,[H-V|Rest]) :-
    \+ member(H-_,Old),
    merge(T,Old,Rest).
    
dfs(Edges, Visited, ToVisit, Timer, Tin, Low, Parents, [V-Idx|Stack], EdgeStack) :-
    findall(Adj, (member(V-Adj, Edges)), Adjs), 
    length(Adjs,L), 
    Idx < L,
    nth0(Idx,Adjs,To), 
    append([V-(Idx+1)],Stack,NewStack),
    dfs_helper(V,Parents,To,Visited,Low,Tin,Stack,EdgeStack,  NewLow,NewEdgeStack).

dfs_helper(V,Parents,To,Visited,Low,Tin,Stack,EdgeStack, NewLow,NewEdgeStack) :-
    member(V-Parent,Parents),
    not(To==Parent),
    dfs_helper2(V,To,Visited,Low,Tin,Stack,EdgeStack,Parents, NewLow,NewEdgeStack).
dfs_helper(V,Parents,To,_,Low,_,_,EdgeStack,   Low,EdgeStack) :-
    member(V-Parent,Parents),
    To==Parent.

dfs_helper2(V,To,Visited,Low,Tin,_,EdgeStack,_,   NewLow,NewEdgeStack) :-
    member(To,Visited),
    member(V-LowV,Low),
    member(To-TinTo,Tin),
    NewLowV is min(LowV,TinTo),
    merge(Low, [V-NewLowV], NewLow),
    dfs_helper3(V, To, Tin, EdgeStack, NewEdgeStack).

dfs_helper2(V,To,Visited,Low,Tin,Stack,EdgeStack,Parents,   NewLow,NewStack) :-
    not(member(To,Visited)),
    .

dfs_helper3(V,To,Tin,EdgeStack,   NewEdgeStack) :-
    member(V-TinV,Tin), 
    member(To-TinTo,Tin),
    TinTo < TinV,
    append(EdgeStack, [V-To], NewEdgeStack).
dfs_helper3(V,To,Tin,EdgeStack,   EdgeStack) :-
    member(V-TinV,Tin), 
    member(To-TinTo,Tin),
    TinTo >= TinV.